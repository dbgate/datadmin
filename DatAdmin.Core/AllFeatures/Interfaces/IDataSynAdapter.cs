using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace DatAdmin
{
    public enum DataSynOperation { Delete, Insert, Update }

    public interface IDataSynReportEnv
    {
        void SendScriptWrapper(IDataSynScriptWrapper wrapper);
    }

    public interface IDataSynAdapter
    {
        string Concat(IEnumerable<string> exprs);
        string Md5(string expr);
        string GetHashableString(string expr, DbTypeBase type);
        string ReadHash(IBedValueReader reader, BedValueConvertor auxConv, BedValueHolder auxHolder);
        void SendScript(IPhysicalConnection conn, List<string> script, List<string> batchBegin, List<string> batchEnd, DataSynOperation operation, IDataSynReportEnv repenv);
        string Coalesce(string val1, string val2);
    }

    public interface IDataSynScriptWrapper
    {
        DataSynOperation Operation { get; }
        string Script { get; }
        void Run();
    }

    public class DataSynScriptWrapper_ExecuteNonQuery : IDataSynScriptWrapper
    {
        DbConnection m_conn;
        public DataSynOperation Operation { get; private set; }
        public string Script { get; private set; }

        public DataSynScriptWrapper_ExecuteNonQuery(string script, DataSynOperation operation, DbConnection conn)
        {
            Operation = operation;
            Script = script;
            m_conn = conn;
        }

        public void Run()
        {
            using (var cmd = m_conn.CreateCommand())
            {
                cmd.CommandText = Script;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
