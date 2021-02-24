using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Plugin.textio
{
    public partial class DataStoreWriterFrame : FileWriterFrame
    {
        public DataStoreWriterFrame(PolyFileDbWriterFrame frame)
            : base(frame)
        {
            InitializeComponent();
            dataStoreFrame1.DataStoreMode = DatAdmin.TabularDataStoreMode.WriteStream;
        }

        public override string ComboTitle()
        {
            return "s_datastore";
        }

        public override void LoadContent()
        {
            var ds = m_content as DataStoreFileContent;
            if (ds != null)
            {
                tbxQuery.Text = ds.Sql;
                dataStoreFrame1.SetDataStore(ds.DataStore);
            }
        }

        private void tbxQuery_TextChanged(object sender, EventArgs e)
        {
            if (m_loading) return;
            ((DataStoreFileContent)m_content).Sql = tbxQuery.Text;
        }

        private void dataStoreFrame1_ChangedDataStore(object sender, EventArgs e)
        {
            if (m_loading) return;
            if (m_content == null) return;
            ((DataStoreFileContent)m_content).DataStore = dataStoreFrame1.DataStore;
        }

        public override WriterFileContentBase CreateContent()
        {
            Content = new DataStoreFileContent();
            return Content;
        }
    }
}
