using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class QueryExecuteParams
    {
        public DateTime ExecutedAt;
        public ISqlDialect Dialect;
        public string Sql;
        public string DbServer;
        public string DbName;
        public string QueryContext;
        public string FileName;
        public int DurationInMilisecs;
    }

    public static class HQuery
    {
        public static void CallQueryExecute(QueryExecuteParams pars)
        {
            if (QueryExecute != null) QueryExecute(pars);
        }

        public static event Action<QueryExecuteParams> QueryExecute;
    }
}
