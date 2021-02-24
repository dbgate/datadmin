using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using DatAdmin;

namespace DatAdmin
{

    public abstract class TunnelCommand : DbCommand
    {
        public enum GetResultCaller { ExecuteNonQuery, ExecuteScalar, ExecuteReader }
        TunnelConnection m_conn;
        string m_sql;
        TunnelParameterCollection m_params = new TunnelParameterCollection();

        public TunnelCommand(TunnelConnection conn) { m_conn = conn; }
        public TunnelCommand() { }

        public override void Cancel()
        {
            throw new NotImplementedError("DAE-00302");
        }

        public override string CommandText
        {
            get { return m_sql; }
            set { m_sql = value; }
        }

        public override int CommandTimeout
        {
            get { return 0; }
            set { }
        }

        public override CommandType CommandType
        {
            get { return CommandType.Text; }
            set { }
        }

        protected override DbParameter CreateDbParameter()
        {
            return new TunnelParameter();
        }

        protected override DbConnection DbConnection
        {
            get { return m_conn; }
            set { m_conn = (TunnelConnection)value; }
        }

        protected override DbParameterCollection DbParameterCollection
        {
            get { return m_params; }
        }

        protected override DbTransaction DbTransaction
        {
            get { return null; }
            set { }
        }

        public override bool DesignTimeVisible
        {
            get { return false; }
            set { }
        }

        protected abstract ITunnelResultSet GetResult(CommandBehavior behaviour, GetResultCaller caller);

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            ITunnelResultSet res = GetResult(behavior, GetResultCaller.ExecuteReader);
            return new TunnelDataReader(res);
        }

        public override int ExecuteNonQuery()
        {
            using (ITunnelResultSet res = GetResult(CommandBehavior.Default, GetResultCaller.ExecuteNonQuery))
            {
                return res.RecordsAffected;
            }
        }

        public override object ExecuteScalar()
        {
            using (ITunnelResultSet res = GetResult(CommandBehavior.Default, GetResultCaller.ExecuteScalar))
            {
                return res.FetchRow()[0];
            }
        }

        public override void Prepare()
        {
        }

        public override UpdateRowSource UpdatedRowSource
        {
            get { return UpdateRowSource.None; }
            set { }
        }
    }
}
