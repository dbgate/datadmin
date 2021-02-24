using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.sqlite
{
    public class SqliteDumpLoader : DumpLoaderBase
    {
        public SqliteDumpLoader(ISqlDialect dialect)
            : base(dialect)
        {
        }

        protected override void ExecuteDumpQuery(string sql)
        {
            if (Transaction == null)
            {
                // optimalization - create transaction for dump command, is much quicker
                var tran = Connection.BeginTransaction();
                try
                {
                    Connection.ExecuteNonQuery(sql, m_dialect, tran, null);
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
            else
            {
                base.ExecuteDumpQuery(sql);
            }
        }
    }
}
