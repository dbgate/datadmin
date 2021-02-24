using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Windows.Forms;

namespace DatAdmin
{
    public static class DXDbCreator
    {
        private static void TestAllowCreateStructure()
        {
            if (MessageBox.Show(Texts.Get("s_allow_create_d2dx_structure"), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                throw new Exception("DAE-00228 " + Texts.Get("s_d2dx_structure_cannot_be_created"));
            }
        }

        public static void CreateDbStructure(DbConnection conn, ISqlDialect dialect)
        {
            if (dialect.DialectCaps.NestedTransactions)
            {
                using (DbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        DbConnectionExtension.ExecuteScalar(conn, "SELECT COUNT(*) FROM d2dx_info", tran);
                    }
                    catch (Exception)
                    {
                        TestAllowCreateStructure();
                        DbConnectionExtension.ExecuteNonQueries(conn, DatAdmin.DataSources.D2DX.SqlScripts.update_01, dialect, tran, null);
                    }
                    tran.Commit();
                }
            }
            else
            {
                try
                {
                    DbConnectionExtension.ExecuteScalar(conn, "SELECT COUNT(*) FROM d2dx_info");
                }
                catch (Exception)
                {
                    TestAllowCreateStructure();
                    DbConnectionExtension.ExecuteNonQueries(conn, DatAdmin.DataSources.D2DX.SqlScripts.update_01, dialect);
                }
            }

            // handle higher versions
            ProcesssUpdate(2, DatAdmin.DataSources.D2DX.SqlScripts.update_02, conn, dialect);
        }

        private static void ProcesssUpdate(int version, string updateSql, DbConnection conn, ISqlDialect dialect)
        {
            if (Int32.Parse(DbConnectionExtension.ExecuteScalar(conn, "SELECT par_value FROM d2dx_info WHERE par_name='dbversion'").ToString()) < version)
            {
                if (dialect.DialectCaps.NestedTransactions)
                {
                    using (DbTransaction tran = conn.BeginTransaction())
                    {
                        DbConnectionExtension.ExecuteNonQueries(conn, updateSql, dialect, tran, null);
                        tran.Commit();
                    }
                }
                else
                {
                    DbConnectionExtension.ExecuteNonQueries(conn, updateSql, dialect);
                }
            }
        }

        public static bool HasDXStructure(DbConnection conn)
        {
            try
            {
                DbConnectionExtension.ExecuteScalar(conn, "SELECT COUNT(*) FROM d2dx_info");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
