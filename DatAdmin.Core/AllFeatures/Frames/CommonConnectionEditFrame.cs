using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;

namespace DatAdmin
{
    public partial class CommonConnectionEditFrame : UserControl
    {
        IStoredConnection m_conn;
        ConnectionEditFrame m_frame;
        public CommonConnectionEditFrame(IStoredConnection conn)
        {
            InitializeComponent();
            InitDialects();
            Connection = conn;
        }
        public CommonConnectionEditFrame()
        {
            InitializeComponent();
            InitDialects();
        }

        private void InitDialects()
        {
            cbxDialect.Items.Add(String.Format("({0})", Texts.Get("s_default")));
            foreach (var item in DialectAddonType.Instance.CommonSpace.GetFilteredAddons(RegisterItemUsage.DirectUse))
            {
                cbxDialect.Items.Add(item);
            }
            cbxDialect.SelectedIndex = 0;
        }

        public IStoredConnection Connection
        {
            get { return m_conn; }
            set
            {
                m_conn = value;
                lbdatabase.Connection = m_conn;
                if (m_conn.DialectOverride != null)
                {
                    foreach (object item in cbxDialect.Items)
                    {
                        if (item is AddonHolder && ((AddonHolder)item).Name == m_conn.DialectOverride)
                        {
                            cbxDialect.SelectedItem = item;
                        }
                    }
                }
            }
        }

        public void LinkToSpecific(ConnectionEditFrame frame)
        {
            frame.ConnectionChanged += new EventHandler(frame_OnConnectionChanged);
            m_frame = frame;
            frame_OnConnectionChanged(this, EventArgs.Empty);
        }

        void frame_OnConnectionChanged(object sender, EventArgs e)
        {
            m_frame.SaveConnection();
            connectionstring.Text = m_conn.GenerateConnectionString(false);
        }

        public void SaveConnection()
        {
            lbdatabase.SaveToConnection();
            if (cbxDialect.SelectedItem is AddonHolder)
            {
                m_conn.DialectOverride = ((AddonHolder)cbxDialect.SelectedItem).Name;
            }
            else
            {
                m_conn.DialectOverride = null;
            }
        }

        private DbConnection CreateConnection()
        {
            DbConnection conn = m_conn.CreateSystemConnection();
            conn.ConnectionString = m_conn.GenerateConnectionString(true);
            return conn;
        }
    }
}
