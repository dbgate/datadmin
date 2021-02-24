using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DatAdmin
{
    public class ConnectionWrapperUsage : IConnectionUsage
    {
        IPhysicalConnection m_conn;

        public ConnectionWrapperUsage(IPhysicalConnection conn)
        {
            m_conn = conn;
        }
        #region IConnectionUsage Members

        [Browsable(false)]
        public IPhysicalConnection Connection
        {
            get { return m_conn; }
            set { PhysicalConnectionExtension.SafeChangeConnection(ref m_conn, value); }
        }

        #endregion
    }
}
