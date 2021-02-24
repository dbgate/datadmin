using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.Common;

namespace DatAdmin.selftest
{
    public static class TestPrepare
    {
        public static void ClearDirs()
        {
            TestLog.Info("Clearing directory " + Config.Instance.AppDataDirectory);
            Directory.Delete(Config.Instance.AppDataDirectory, true);
        }

        public static void CreateConnections()
        {
            foreach (var con in Config.Instance.Connections)
            {
                TestTool.RunMacro(con.CreateMacro);
            }
        }

        public static void DropDbs()
        {
            foreach (var con in Config.Instance.Connections)
            {
                string fn = Path.Combine(Config.Instance.AppDataDirectory, "data/" + con.ConFileName);
                IStoredConnection sc = StoredConnection.LoadFromFile(fn);
                if (sc == null)
                {
                    TestLog.Info("Cannot connect to database " + con.ConFileName);
                    continue;
                }
                using (DbConnection dbconn = sc.CreateSystemConnection())
                {
                    dbconn.Open();
                    try
                    {
                        TestLog.Info("Deleting testing database");
                        dbconn.ExecuteNonQuery("DROP DATABASE testingdb", new GenericDialect());
                    }
                    catch (Exception e)
                    {
                        TestLog.Error("Error creating database", e);
                    }
                }
            }
        }
    }
}
