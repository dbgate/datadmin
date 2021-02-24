using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;
using System.Data.Common;

namespace Plugin.genericsql
{
    public partial class GenericConnEditor : PropertyGridConnFrame
    {
        DbConnectionStringBuilder m_builder;
        GenericSqlStoredConnection m_conn;

        public GenericConnEditor(GenericSqlStoredConnection conn)
        {
            InitializeComponent();

            m_conn = conn;
            DbProviderFactory factory = conn.GetFactory();
            m_builder = factory.CreateConnectionStringBuilder();
            m_builder.ConnectionString = conn.ConnectionString;

            SelectedObject = m_builder;
        }

        public override void SaveConnection()
        {
            m_conn.ConnectionString = m_builder.ConnectionString;
        }
    }
}
