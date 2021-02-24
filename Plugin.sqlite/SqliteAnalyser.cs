using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data;

namespace Plugin.sqlite
{
    public class SqliteAnalyser : DatabaseAnalyser
    {
        // dict (table,column)=>ispk
        Dictionary<Tuple<string, string>, bool> m_pkcols = new Dictionary<Tuple<string, string>, bool>();

        /// <summary>
        /// determines SQLite type affinity
        /// </summary>
        /// <param name="row">row from GetSchema(columns) call</param>
        /// <returns></returns>
        protected override DbTypeBase AnalyseType(IDataRecord row, IPhysicalConnection conn, bool isdomain)
        {
            SqliteTypeBase res = MakeSpecificSqliteType(row);
            return res.ToGenericType();
        }

        private SqliteTypeBase MakeSpecificSqliteType(IDataRecord row)
        {
            bool nullable = (bool)row["IS_NULLABLE"];
            string dt = row.DataTypeName().ToUpper();
            long len = row.CharLength();
            int ilen = 50;
            if (len > 0 && len <= 1000) ilen = (int)len;
            if (dt.IndexOf("INT") >= 0) return new SqliteTypeInt();
            if (dt.IndexOf("VARCHAR") >= 0) return new SqliteTypeText();
            if (dt.IndexOf("CHAR") >= 0) return new SqliteTypeText();
            if (dt.IndexOf("CLOB") >= 0) return new SqliteTypeText();
            if (dt.IndexOf("TEXT") >= 0) return new SqliteTypeText();
            if (dt.IndexOf("BLOB") >= 0) return new SqliteTypeBlob();
            if (dt.IndexOf("REAL") >= 0) return new SqliteTypeReal();
            if (dt.IndexOf("FLOAT") >= 0) return new SqliteTypeReal();
            if (dt.IndexOf("DOUBLE") >= 0) return new SqliteTypeReal();
            if (dt.IndexOf("DECIMAL") >= 0) return new SqliteTypeNumeric();
            if (dt.IndexOf("NUMERIC") >= 0) return new SqliteTypeNumeric();
            if (dt.IndexOf("LOGICAL") >= 0) return new SqliteTypeLogical();
            if (dt.IndexOf("DATE") >= 0) return new SqliteTypeDateTime();
            return new SqliteTypeText { SpecificCode = dt };
        }

        protected override void LoadTableColumn(ColumnStructure col, DataRow row)
        {
            base.LoadTableColumn(col, row);
            try
            {
                if ((bool)row["PRIMARY_KEY"]) m_pkcols[new Tuple<string, string>(col.Table.FullName.Name, col.ColumnName)] = true;
            }
            catch (Exception err)
            {
                Logging.Warning("Error loading SQLite PK:" + err.ToString());
            }
        }

        protected override bool WantLoadTableColumns()
        {
            return base.WantLoadTableColumns() || m_members.TableMembers.Contains(TableStructureMembers.PrimaryKey);
        }

        protected override void LoadTableColumns(TableStructure table)
        {
            base.LoadTableColumns(table);
            PrimaryKey pk = new PrimaryKey();
            //pk.Table = table.FullName;
            foreach (var col in table.Columns)
            {
                if (m_pkcols.ContainsKey(new Tuple<string, string>(table.FullName.Name, col.ColumnName)))
                {
                    pk.Columns.Add(new ColumnReference(col.ColumnName));
                }
            }
            if (pk.Columns.Count > 0)
            {
                table._Constraints.Add(pk);
                if (pk.Columns.Count == 1)
                {
                    table.Columns[pk.Columns[0].ColumnName].DataType.SetAutoincrement(true);
                }
            }
        }

        protected override bool IsColumnNullable(IDataRecord row)
        {
            return (bool)row["IS_NULLABLE"];
        }

        protected override void LoadIndexes(TableStructure table)
        {
            base.LoadIndexes(table);

            TableAnalyser tad = new TableAnalyser();

            DataTable idxs = GetDbConn().GetSchema("Indexes");
            foreach (DataRow row in idxs.Rows)
            {
                TableAnalyser.Key key = new TableAnalyser.Key();
                if ((bool)row["PRIMARY_KEY"]) continue;
                key.tblname = row["TABLE_NAME"].ToString();
                key.keyname = row["INDEX_NAME"].ToString();
                key.keytype = "INDEX";
                tad.keys.Add(key);
            }

            DataTable idxcols = GetDbConn().GetSchema("IndexColumns");
            foreach (DataRow row in idxcols.Rows)
            {
                TableAnalyser.Col col = new TableAnalyser.Col();
                col.colname = row["COLUMN_NAME"].ToString();
                col.tblname = row["TABLE_NAME"].ToString();
                col.keyname = row["CONSTRAINT_NAME"].ToString();
                col.ordinal = row["ORDINAL_POSITION"].ToString();
                col.keytype = "INDEX";
                tad.cols.Add(col);
            }
            tad.SaveConstraints(table, this);
            table._Constraints.RemoveIf(cnt => cnt.Name != null && cnt.Name.StartsWith("sqlite_"));
        }

        protected override void LoadConstraints(TableStructure table)
        {
            base.LoadConstraints(table);

            TableAnalyser tad = new TableAnalyser();
            DataTable fks = GetDbConn().GetSchema("ForeignKeys");
            foreach (DataRow row in fks.Rows)
            {
                TableAnalyser.Col col = new TableAnalyser.Col();
                col.colname = row.SafeString("FKEY_FROM_COLUMN");
                col.dstcolname = row.SafeString("FKEY_TO_COLUMN");
                col.tblname = row.SafeString("TABLE_NAME");
                col.dsttblname = row.SafeString("FKEY_TO_TABLE");
                if (col.dsttblname != null && col.dsttblname.StartsWith("\"") && col.dsttblname.EndsWith("\""))
                {
                    col.dsttblname = col.dsttblname.Substring(1, col.dsttblname.Length - 2);
                }
                col.ordinal = row.SafeString("FKEY_FROM_ORDINAL_POSITION");
                col.keyname = row.SafeString("CONSTRAINT_NAME");
                col.keytype = "FOREIGN KEY";
                tad.cols.Add(col);
            }
            tad.SaveConstraints(table, this);
        }

        protected void LoadTrigger(DataRow row, SpecificObjectStructure obj)
        {
            obj.RelatedTable = new NameWithSchema(row.SafeString("TABLE_NAME"));
            obj.CreateSql = row.SafeString("TRIGGER_DEFINITION");
        }

        protected override void LoadSpecificObjects()
        {
            base.LoadSpecificObjects();

            LoadSpecificObjectListAndDetail("trigger", "@Triggers", null, "TRIGGER_NAME", LoadTrigger);
        }

        protected void LoadView(DataRow row, SpecificObjectStructure obj)
        {
            obj.CreateSql = "CREATE VIEW \"" + obj.ObjectName.Name + "\"\nAS\n" + row.SafeString("VIEW_DEFINITION");
        }

        protected override void LoadViews(bool listOnly)
        {
            LoadSpecificObjectListAndDetail("view", "@Views", null, "TABLE_NAME", LoadView);
        }
    }
}
