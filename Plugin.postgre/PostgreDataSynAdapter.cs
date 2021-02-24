using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.postgre
{
    public class PostgreDataSynAdapter : DataSynAdapterBase
    {
        public PostgreDataSynAdapter(ISqlDialect dialect)
            : base(dialect)
        {
        }

        public override string Md5(string expr)
        {
            return String.Format("md5({0})", expr);
        }

        public override string Concat(IEnumerable<string> exprs)
        {
            return exprs.CreateDelimitedText(" || ");
        }

        public override string GetHashableString(string expr, DbTypeBase type)
        {
            if (type is DbTypeDatetime)
            {
                return String.Format("(extract(year from {0}) || '-' || "
                    + "extract(month from {0}) || '-' || "
                    + "extract(day from {0}) || '-' || "
                    + "extract(hour from {0}) || '-' || "
                    + "extract(minute from {0}) || '-' || "
                    + "extract(second from {0}))", expr);
            }
            return String.Format("({0} || '')", expr);
        }

        //public override void SendScript(IPhysicalConnection conn, List<string> script, List<string> batchBegin, List<string> batchEnd)
        //{
        //    foreach (string line in script)
        //    {
        //        var fullcmd = new List<string>();
        //        if (batchBegin != null) fullcmd.AddRange(batchBegin);
        //        fullcmd.Add(line);
        //        if (batchEnd != null) fullcmd.AddRange(batchEnd);
        //        using (var cmd = conn.SystemConnection.CreateCommand())
        //        {
        //            cmd.CommandText = fullcmd.CreateDelimitedText(";");
        //            try
        //            {
        //                cmd.ExecuteNonQuery();
        //            }
        //            catch
        //            {
        //                int x = 0;
        //            }
        //        }
        //    }
        //}
    }
}
