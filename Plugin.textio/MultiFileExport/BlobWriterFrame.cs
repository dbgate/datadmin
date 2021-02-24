using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Plugin.textio
{
    public partial class BlobWriterFrame : FileWriterFrame
    {
        public BlobWriterFrame(PolyFileDbWriterFrame frame)
            : base(frame)
        {
            InitializeComponent();
        }

        public override string ComboTitle()
        {
            return "BLOB";
        }

        public override void LoadContent()
        {
            tbxQuery.Text = ((BlobFileContent)m_content).Sql;
        }

        private void tbxQuery_TextChanged(object sender, EventArgs e)
        {
            if (m_loading) return;
            ((BlobFileContent)m_content).Sql = tbxQuery.Text;
        }

        public override WriterFileContentBase CreateContent()
        {
            Content = new BlobFileContent();
            return Content;
        }
    }
}
