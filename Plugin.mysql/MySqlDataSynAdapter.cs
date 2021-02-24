using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.mysql
{
    public class MySqlDataSynAdapter : DataSynAdapterBase
    {
        public MySqlDataSynAdapter(ISqlDialect dialect)
            : base(dialect)
        {
        }

        public override string Md5(string expr)
        {
            return String.Format("md5({0})", expr);
        }

        public override string Concat(IEnumerable<string> exprs)
        {
            return String.Format("concat({0})", exprs.CreateDelimitedText(","));
        }

        public override string GetHashableString(string expr, DbTypeBase type)
        {
            if (type is DbTypeDatetime)
            {
                return String.Format("concat(year({0}), '-',month({0}), '-',day({0}), '-',hour({0}), '-',minute({0}), '-',second({0}))", expr);
            }
            return base.GetHashableString(expr, type);
        }

        public override void SendScript(IPhysicalConnection conn, List<string> script, List<string> batchBegin, List<string> batchEnd, DataSynOperation operation, IDataSynReportEnv repenv)
        {
            int maxpacket = Int32.Parse(conn.SystemConnection.ExecuteScalar("select @@max_allowed_packet").ToString());

            string oldmode = conn.SystemConnection.ExecuteScalar("SELECT @@SQL_MODE").ToString();

            string begin = "SET SQL_MODE='NO_AUTO_VALUE_ON_ZERO';\n";
            foreach (string s in batchBegin) begin += s + ";\n";
            string end = "SET SQL_MODE='" + oldmode + "';\n";
            foreach (string s in batchEnd) end += s + ";\n";

            //try
            //{
            //    conn.SystemConnection.ExecuteNonQuery("SET SQL_MODE='NO_AUTO_VALUE_ON_ZERO'");
            var sb = new StringBuilder();
            foreach (string sql in script)
            {
                if (sb.Length + sql.Length + begin.Length + end.Length > maxpacket / 4)
                {
                    repenv.SendScriptWrapper(new DataSynScriptWrapper_ExecuteNonQuery(begin + sb.ToString() + end, operation, conn.SystemConnection));
                    sb = new StringBuilder();
                }
                sb.Append(sql);
                sb.Append(";");
            }
            if (sb.Length > 0)
            {
                conn.SystemConnection.ExecuteNonQuery(begin + sb.ToString() + end);
            }
            //}
            //finally
            //{
            //    conn.SystemConnection.ExecuteNonQuery("SET SQL_MODE='" + oldmode + "'");
            //}
        }
    }
}
