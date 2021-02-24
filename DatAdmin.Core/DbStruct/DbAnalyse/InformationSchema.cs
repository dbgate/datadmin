using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;

namespace DatAdmin
{
    public class InformationSchema
    {
        public class View
        {
            public string Name;
            public List<string> Columns = new List<string>();
            public override string ToString()
            {
                return Name;
            }
        }
        public List<View> Views = new List<View>();
        public Dictionary<string, View> ViewByName = new Dictionary<string, View>();
        public void Load(DbConnection conn)
        {
            try
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT table_name FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA LIKE '%information_schema%' OR TABLE_SCHEMA LIKE '%INFORMATION_SCHEMA%'";
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            View v = new View();
                            v.Name = reader[0].ToString().ToLower();
                            Views.Add(v);
                            ViewByName[v.Name] = v;
                        }
                    }
                }
            }
            catch
            {
                // database does not support information schema
            }
            if (Views.Count == 0)
            {
                DetectionTrialAndError(conn);
            }
            else
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT table_name, column_name FROM INFORMATION_SCHEMA.COLUMNS WHERE table_schema LIKE '%information_schema%' OR TABLE_SCHEMA LIKE '%INFORMATION_SCHEMA%'";
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            View v = ViewByName[reader[0].ToString().ToLower()];
                            v.Columns.Add(reader[1].ToString().ToLower());
                        }
                    }
                }
            }
        }

        private void DetectionTrialAndError(DbConnection conn)
        {
            DetectView(conn, "tables");
            DetectView(conn, "columns");
            DetectView(conn, "table_constraints");
            DetectView(conn, "key_column_usage");
            DetectView(conn, "referential_constraints");
            DetectView(conn, "check_constraints");
        }

        private void DetectView(DbConnection conn, string name)
        {
            try
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM INFORMATION_SCHEMA." + name.ToUpper();
                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.SchemaOnly))
                    {
                        View v = new View();
                        v.Name = name;
                        Views.Add(v);
                        ViewByName[v.Name] = v;
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            v.Columns.Add(reader.GetName(i).ToLower());
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        static Dictionary<string, InformationSchema> m_schemaByGroupId = new Dictionary<string, InformationSchema>();

        public static InformationSchema LoadSchemaOnce(Func<DbConnection> conn, string groupId)
        {
            lock (m_schemaByGroupId)
            {
                if (m_schemaByGroupId.ContainsKey(groupId)) return m_schemaByGroupId[groupId];
            }
            InformationSchema res = new InformationSchema();
            res.Load(conn());
            lock (m_schemaByGroupId)
            {
                m_schemaByGroupId[groupId] = res;
            }
            return res;
        }

        public bool HasColumn(string table, string column)
        {
            if (!ViewByName.ContainsKey(table)) return false;
            return ViewByName[table].Columns.Contains(column);
        }

        public bool HasView(string table)
        {
            return ViewByName.ContainsKey(table);
        }
    }
}
