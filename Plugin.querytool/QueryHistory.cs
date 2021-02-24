using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.querytool
{
    public class QueryHistory
    {
        public static QueryHistory Instance = new QueryHistory();
        public QueryDbConnection Connection = new QueryDbConnection();

        private bool m_cleared;

        public void OnQueryExecute(QueryExecuteParams ep)
        {
            lock (this)
            {
                try
                {
                    if (!m_cleared && QueryHistorySettings.Page.DeleteAfterDays > 0)
                    {
                        int tm = (DateTime.Now - TimeSpan.FromDays(QueryHistorySettings.Page.DeleteAfterDays)).GetUnixTimestamp();
                        Connection.ExecuteNonQuery("delete from QueryExecute where ExecutedAtTimestamp < @p1", tm);
                        Connection.ExecuteNonQuery("delete from QueryText where not exists (select * from QueryExecute where QueryExecute.QueryText_ID = QueryText.ID)");
                        m_cleared = true;
                    }
                    if (QueryHistorySettings.Page.UseQueryHistory)
                    {
                        int hash = ep.Sql.GetHashCode();
                        string codeid = Connection.ExecuteScalar("select ID from QueryText where HashCode=@p1", hash).SafeToString();
                        if (String.IsNullOrEmpty(codeid))
                        {
                            Connection.ExecuteScalar("insert into QueryText (QueryText, HashCode) values (@p1, @p2)", ep.Sql, hash);
                            codeid = Connection.GetInsertId().ToString();
                        }
                        Connection.ExecuteNonQuery(
                            "insert into QueryExecute (ExecutedAt, ExecutedAtTimestamp, Dialect, QueryText_ID, DbServer, DbName, QueryContext, FileName, DurationInMilisecs) values "
                            + "(@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)", ep.ExecutedAt.ToString("s"), ep.ExecutedAt.GetUnixTimestamp(), ep.Dialect.DialectName, codeid, ep.DbServer, ep.DbName, ep.QueryContext, ep.FileName, ep.DurationInMilisecs);
                    }
                }
                catch (Exception err)
                {
                    Logging.Warning("Error saving query to history:" + err.Message);
                }
            }
        }
    }

    [PluginHandler]
    public class QueryHistoryPluginHandler : PluginHandlerBase
    {
        public override void OnLoadedAllPlugins()
        {
            try
            {
                HQuery.QueryExecute += QueryHistory.Instance.OnQueryExecute;
            }
            catch (Exception err)
            {
                Logging.Warning("Error creating query connection:" + err.Message);
            }
        }
    }
}
