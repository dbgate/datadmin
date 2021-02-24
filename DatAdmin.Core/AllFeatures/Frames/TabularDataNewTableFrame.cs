using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class TabularDataNewTableFrame : UserControl
    {
        GenericTabularDataStore m_obj;
        public TabularDataNewTableFrame(GenericTabularDataStore obj)
        {
            InitializeComponent();
            m_obj = obj;
            tbxDatabase.Text = String.Format("{0} ({1})", obj.dbname, obj.Connection);
            tableFromTabularDataFrame1.SetDatabase(obj.Connection.PhysicalFactory.CreateDatabaseSource(obj.Connection, obj.dbname));
        }

        private void tableFromTabularDataFrame1_ChangedProperties(object sender, EventArgs e)
        {
            var name = tableFromTabularDataFrame1.FullName;
            m_obj.tblname = name.Name;
            m_obj.schema = name.Schema;
        }
    }
}
