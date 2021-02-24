using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public abstract class DataSynAdapterBase : IDataSynAdapter
    {
        protected ISqlDialect m_dialect;
        public DataSynAdapterBase(ISqlDialect dialect)
        {
            m_dialect = dialect;
        }

        #region IDataSynAdapter Members

        public virtual string Concat(IEnumerable<string> exprs)
        {
            return exprs.CreateDelimitedText("+");
        }

        public abstract string Md5(string expr);

        public virtual string GetHashableString(string expr, DbTypeBase type)
        {
            return expr;
        }

        public virtual string ReadHash(IBedValueReader reader, BedValueConvertor auxConv, BedValueHolder auxHolder)
        {
            switch (reader.GetFieldType())
            {
                case TypeStorage.String:
                    return reader.GetString().ToLower();
                case TypeStorage.ByteArray:
                    {
                        byte[] data = reader.GetByteArray();
                        if (data.Length == 32) return Encoding.UTF8.GetString(data);
                        return StringTool.EncodeHex(data).ToLower();
                    }
            }
            auxConv.ConvertValue(reader, TypeStorage.String, auxHolder);
            return auxHolder.GetString();
        }

        public virtual void SendScript(IPhysicalConnection conn, List<string> script, List<string> batchBegin, List<string> batchEnd, DataSynOperation operation, IDataSynReportEnv repenv)
        {
            var fullcmd = new List<string>();
            if (batchBegin != null) fullcmd.AddRange(batchBegin);
            fullcmd.AddRange(script);
            if (batchEnd != null) fullcmd.AddRange(batchEnd);
            repenv.SendScriptWrapper(new DataSynScriptWrapper_ExecuteNonQuery(fullcmd.CreateDelimitedText(";\n"), operation, conn.SystemConnection));
        }

        public virtual string Coalesce(string val1, string val2)
        {
            return String.Format("COALESCE({0}, {1})", val1, val2);
        }

        #endregion
    }
}
