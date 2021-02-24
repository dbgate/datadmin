using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;

namespace DatAdmin
{
    public class ConstraintsLoader
    {
        DbConnection m_conn;
        InformationSchema m_infoSchema;
        TableAnalyser m_analyser;
        NameWithSchema m_table;
        Condition m_dbnamecond;

        bool m_loadAllConstraints = false;
        bool m_postgreRefs = false;
        bool m_loadCheckConstraints = false;
        TableStructureMembers m_members;

        public delegate void ProcessRecordDelegate<T>(T dst, IDataRecord rec);

        public static void ReadValue(IDataRecord rec, string field, ref string data)
        {
            for (int i = 0; i < rec.FieldCount; i++)
            {
                if (String.Compare(rec.GetName(i), field, true) == 0)
                {
                    object value = rec.GetValue(i);
                    if (value == null) data = null;
                    else data = value.ToString();
                }
            }
        }

        public abstract class Condition
        {
            public abstract string Generate(ConstraintsLoader loader, string table, ref bool cancel);
        }
        public class EqualCondition : Condition
        {
            string m_name;
            string m_value;
            bool m_mandatory;

            public EqualCondition(string name, string value, bool mandatory)
            {
                m_name = name;
                m_value = value;
                m_mandatory = mandatory;
            }

            public override string Generate(ConstraintsLoader loader, string table, ref bool cancel)
            {
                if (!loader.m_infoSchema.HasColumn(table, m_name))
                {
                    if (m_mandatory) cancel = true;
                    return null;
                }
                return String.Format("{0}='{1}'", m_name, m_value);
            }
        }
        public class SchemaCondition : Condition
        {
            string m_schema;

            public SchemaCondition(string schema)
            {
                m_schema = schema;
            }

            public override string Generate(ConstraintsLoader loader, string table, ref bool cancel)
            {
                if (m_schema == null) return null;
                if (loader.m_infoSchema.HasColumn(table, "table_schema")) return String.Format("table_schema='{0}'", m_schema);
                if (loader.m_infoSchema.HasColumn(table, "constraint_schema")) return String.Format("constraint_schema='{0}'", m_schema);
                return null;
            }
        }
        public abstract class PolyCondition : Condition
        {
            public List<Condition> Conditions = new List<Condition>();
            public PolyCondition(params Condition[] src){Conditions.AddRange(src);}
            public abstract string SqlName { get;}
            public override string Generate(ConstraintsLoader loader, string table, ref bool cancel)
            {
                StringBuilder sb = new StringBuilder();
                bool wascond = false;
                sb.Append("(");
                foreach (Condition cond in Conditions)
                {
                    string conds = cond.Generate(loader, table, ref cancel);
                    if (cancel) return null;
                    if (conds == null) continue;
                    if (wascond)
                    {
                        sb.Append(" " + SqlName + " ");
                    }
                    sb.Append(conds);
                    wascond = true;
                }
                sb.Append(")");
                if (!wascond) return null;
                return sb.ToString();

            }
        }
        public class OrCondition : PolyCondition
        {
            public override string SqlName { get { return "OR"; } }
            public OrCondition(params Condition[] src) : base(src) { }
        }
        public class AndCondition : PolyCondition
        {
            public override string SqlName { get { return "AND"; } }
            public AndCondition(params Condition[] src) : base(src) { }
        }

        public ConstraintsLoader(DbConnection conn, InformationSchema infoSchema, TableAnalyser analyser, NameWithSchema table, TableStructureMembers members, Condition dbnamecond)
        {
            m_conn = conn;
            m_infoSchema = infoSchema;
            m_table = table;
            m_analyser = analyser;
            m_members = members;
            m_dbnamecond = dbnamecond;
        }
        private void DetectAlgorithm()
        {
            if (m_infoSchema.HasView("referential_constraints") && !m_infoSchema.HasColumn("key_column_usage", "referenced_table_name")) m_postgreRefs = true;
            if (m_postgreRefs) m_loadAllConstraints = true;
            m_loadCheckConstraints = m_infoSchema.HasView("check_constraints") && m_members.Contains(TableStructureMembers.Checks);
        }
        public void Run()
        {
            DetectAlgorithm();

            List<Condition> conds = new List<Condition>();
            if (m_table != null && !m_loadAllConstraints) conds.Add(new SchemaCondition(m_table.Schema));
            if (m_table != null && !m_loadAllConstraints) conds.Add(new EqualCondition("table_name", m_table.Name, false));
            Condition cond = new AndCondition(conds.ToArray());

            if (m_members.ContainsAny(TableStructureMembers.ConstraintsNoIndexesNoRefs) || (m_members.ContainsAny(TableStructureMembers.ReferencedFrom) && m_loadAllConstraints))
            {
                LoadConstraints(cond);
                if (m_loadCheckConstraints) LoadCheckConstraints(cond);
            }

            if (m_postgreRefs)
            {
                if (m_members.ContainsAny(TableStructureMembers.ForeignKeys | TableStructureMembers.ReferencedFrom))
                {
                    LoadKeyColumnUsage(cond);
                    LoadReferentialConstraints(cond);
                }
                else
                {
                    LoadKeyColumnUsage(cond);
                }
            }
            else
            {
                if (m_members.ContainsAny(TableStructureMembers.ReferencedFrom))
                {
                    List<Condition> conds2 = new List<Condition>();
                    if (m_table != null) conds2.Add(new SchemaCondition(m_table.Schema));
                    if (m_table != null) conds2.Add(new EqualCondition("referenced_table_name", m_table.Name, false));
                    Condition orcond = new OrCondition(cond, new AndCondition(conds2.ToArray()));
                    LoadKeyColumnUsage(orcond);
                }
            }
            if (m_infoSchema.HasView("referential_constraints") && m_members.ContainsAny(TableStructureMembers.ReferencedFrom | TableStructureMembers.ForeignKeys))
            {
                LoadForeignKeyRules(cond);
            }
        }

        private void LoadForeignKeyRules(Condition cond)
        {
            using (DbCommand cmd = m_conn.CreateCommand())
            {
                cmd.CommandText = CreateQuery("referential_constraints",
                    new string[] { "constraint_schema", "constraint_name", "table_schema", "table_name", "update_rule", "delete_rule" },
                    cond);
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TableAnalyser.Key key = new TableAnalyser.Key();
                        ReadValue(reader, "table_schema", ref key.tblschema);
                        ReadValue(reader, "table_name", ref key.tblname);
                        ReadValue(reader, "constraint_schema", ref key.keyschema);
                        ReadValue(reader, "constraint_name", ref key.keyname);
                        ReadValue(reader, "update_rule", ref key.updaterule);
                        ReadValue(reader, "delete_rule", ref key.deleterule);
                        key.keytype = "FOREIGN KEY";
                        m_analyser.keys.Add(key);
                    }
                }
            }
        }

        private void LoadCheckConstraints(Condition cond)
        {
            using (DbCommand cmd = m_conn.CreateCommand())
            {
                cmd.CommandText = CreateQuery("check_constraints",
                    new string[] { "constraint_schema", "constraint_name", "table_schema", "table_name", "check_clause" },
                    cond);
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TableAnalyser.Key key = new TableAnalyser.Key();
                        ReadValue(reader, "table_schema", ref key.tblschema);
                        ReadValue(reader, "table_name", ref key.tblname);
                        ReadValue(reader, "constraint_schema", ref key.keyschema);
                        ReadValue(reader, "constraint_name", ref key.keyname);
                        ReadValue(reader, "check_clause", ref key.checkexpr);
                        key.keytype = "CHECK";
                        m_analyser.keys.Add(key);
                    }
                }
            }
        }

        private void LoadReferentialConstraints(Condition cond)
        {
            using (DbCommand cmd = m_conn.CreateCommand())
            {
                cmd.CommandText = CreateQuery("referential_constraints",
                    new string[] { "constraint_schema", "constraint_name", "unique_constraint_schema", "unique_constraint_name",
                                   "update_rule", "delete_rule" },
                    cond);
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string fkschema = null, fkname = null;
                        string pkschema = null, pkname = null;

                        ReadValue(reader, "constraint_schema", ref fkschema);
                        ReadValue(reader, "constraint_name", ref fkname);
                        ReadValue(reader, "unique_constraint_schema", ref pkschema);
                        ReadValue(reader, "unique_constraint_name", ref pkname);

                        TableAnalyser.Key fk = m_analyser.FindKey(fkschema, fkname);
                        TableAnalyser.Key pk = m_analyser.FindKey(pkschema, pkname);

                        if (pk == null || fk == null) continue;
                        fk.dstpkschema = pk.keyschema;
                        fk.dstpkname = pk.keyname;

                        List<TableAnalyser.Col> pkcols = new List<TableAnalyser.Col>(), fkcols = new List<TableAnalyser.Col>();
                        fkcols.AddRange(m_analyser.GetCols(fk));
                        pkcols.AddRange(m_analyser.GetCols(pk));
                        if (fkcols.Count == pkcols.Count)
                        {
                            fk.dsttblschema = pk.tblschema;
                            fk.dsttblname = pk.tblname;
                            for (int i = 0; i < fkcols.Count; i++)
                            {
                                fkcols[i].dstcolname = pkcols[i].colname;
                            }
                        }
                    }
                }
            }
        }

        private void LoadConstraints(Condition cond)
        {
            using (DbCommand cmd = m_conn.CreateCommand())
            {
                string sql = CreateQuery("table_constraints",
                    new string[] { "constraint_schema", "constraint_name", "table_schema", "table_name", "constraint_type" },
                    cond);
                if (sql == null) return;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TableAnalyser.Key key = new TableAnalyser.Key();
                        ReadValue(reader, "table_schema", ref key.tblschema);
                        ReadValue(reader, "table_name", ref key.tblname);
                        ReadValue(reader, "constraint_schema", ref key.keyschema);
                        ReadValue(reader, "constraint_name", ref key.keyname);
                        ReadValue(reader, "constraint_type", ref key.keytype);
                        m_analyser.keys.Add(key);
                    }
                }
            }
        }

        private void LoadKeyColumnUsage(Condition cond){
            using (DbCommand cmd = m_conn.CreateCommand())
            {
                string sql = CreateQuery("key_column_usage",
                    new string[] { "constraint_schema", "constraint_name", "table_schema", "table_name",
                               "ordinal_position", "column_name",
                               "referenced_table_schema", "referenced_table_name", "referenced_column_name" },
                    cond);
                if (sql == null) return;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TableAnalyser.Col col = new TableAnalyser.Col();
                        ReadValue(reader, "table_schema", ref col.tblschema);
                        ReadValue(reader, "table_name", ref col.tblname);
                        ReadValue(reader, "constraint_schema", ref col.keyschema);
                        ReadValue(reader, "constraint_name", ref col.keyname);
                        ReadValue(reader, "ordinal_position", ref col.ordinal);
                        ReadValue(reader, "column_name", ref col.colname);
                        ReadValue(reader, "referenced_table_schema", ref col.dsttblschema);
                        ReadValue(reader, "referenced_table_name", ref col.dsttblname);
                        ReadValue(reader, "referenced_column_name", ref col.dstcolname);
                        m_analyser.cols.Add(col);
                    }
                }
            }

            //LoadNewList("table_constraints",
            //    new string[] { "constraint_schema", "constraint_name", "table_schema", "table_name" },
            //    m_analyser.pks,
            //    delegate(TableAnalyser.Pk pk, IDataRecord rec)
            //    {
            //        ReadValue(rec, "table_schema", ref pk.tblschema);
            //        ReadValue(rec, "constraint_name", ref pk.pkname);
            //        ReadValue(rec, "table_name", ref pk.tblname);
            //    },
            //    new Condition[] {
            //        new EqualCondition("constraint_type", "PRIMARY KEY", true),
            //        new SchemaCondition(m_table.Schema)
            //    }
            //    );
        }
        //private void LoadKeyColumnUsage()
        //{
        //    LoadNewList("key_column_usage",
        //        new string[] { "constraint_schema", "constraint_name", "table_schema", "table_name",
        //                       "ordinal_position", 
        //                       "referenced_table_schema", "referenced_table_name", "referenced_column_name" },
        //        m_analyser.fkcols,
        //        delegate(TableAnalyser.FkCol col, IDataRecord rec)
        //        {
        //            ReadValue(rec, "table_schema", ref pk.tblschema);
        //            ReadValue(rec, "constraint_name", ref pk.pkname);
        //            ReadValue(rec, "table_name", ref pk.tblname);
        //        },
        //        new Condition[] {
        //            new EqualCondition("constraint_type", "PRIMARY KEY", true),
        //            new SchemaCondition(m_table.Schema)
        //        }
        //        );
        //}

        //private void LoadNewList<T>(string table, string[] colums, List<T> list, ProcessRecordDelegate<T> processRecord, Condition[] conditions)
        //{
        //    string sql = CreateQuery(table, colums, conditions);
        //    if (sql == null) return;
        //}

        private string CreateQuery(string table, string[] colums, Condition condition)
        {
            if (!m_infoSchema.ViewByName.ContainsKey(table)) return null;
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            InformationSchema.View v = m_infoSchema.ViewByName[table];
            bool wascol = false;
            foreach (string col in colums)
            {
                if (!v.Columns.Contains(col)) continue;
                if (wascol) sb.Append(",");
                sb.Append(col);
                wascol = true;
            }
            if (!wascol) return null;
            sb.Append(" FROM INFORMATION_SCHEMA.");
            sb.Append(table.ToUpper());
            bool cancel = false;
            if (m_dbnamecond != null) condition = new AndCondition(m_dbnamecond, condition);
            string conds = condition.Generate(this, table, ref cancel);
            if (cancel) return null;
            if (conds != null)
            {
                sb.Append(" WHERE ");
                sb.Append(conds);
            }
            return sb.ToString();
        }
    }
}
