using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;

namespace DatAdmin
{
    public class TunnelTransaction : DbTransaction
    {
        TunnelConnection m_conn;
        public TunnelTransaction(TunnelConnection conn)
        {
            m_conn = conn;
        }
        public override void Commit()
        {
        }

        protected override DbConnection DbConnection
        {
            get { return m_conn; }
        }

        public override IsolationLevel IsolationLevel
        {
            get { return IsolationLevel.ReadCommitted; }
        }

        public override void Rollback()
        {
        }
    }
}
