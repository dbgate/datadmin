using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.textio
{
    public partial class MultiTableDatabaseWriterFrame : UserControl
    {
        MultiTableDatabaseWriter m_writer;
        bool m_created;

        public MultiTableDatabaseWriterFrame(MultiTableDatabaseWriter writer)
        {
            InitializeComponent();
            m_writer = writer;
            dataStoreFrame1.DataStoreMode = TabularDataStoreMode.WriteStream;
            if (m_writer.DataStore != null)
            {
                dataStoreFrame1.SetDataStore(m_writer.DataStore);
            }
            tbxFileName.Text = m_writer.FileNameTemplate;
            m_created = true;
            Translating.TranslateControl(this);
        }

        private void dataStoreFrame1_ChangedDataStore(object sender, EventArgs e)
        {
            if (!m_created) return;
            m_writer.DataStore = dataStoreFrame1.DataStore;
        }

        private void tbxFileName_TextChanged(object sender, EventArgs e)
        {
            if (!m_created) return;
            m_writer.FileNameTemplate = tbxFileName.Text;
        }
    }
}
