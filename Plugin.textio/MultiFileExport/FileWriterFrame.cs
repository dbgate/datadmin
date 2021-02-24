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
    public partial class FileWriterFrame : UserControl
    {
        protected PolyFileDbWriterFrame m_frame;
        protected WriterFileContentBase m_content;
        protected bool m_loading;

        public FileWriterFrame()
        {
            InitializeComponent();
        }

        public FileWriterFrame(PolyFileDbWriterFrame frame)
        {
            InitializeComponent();
            m_frame = frame;
        }

        public WriterFileContentBase Content
        {
            get { return m_content; }
            set
            {
                m_content = value;
                m_loading = true;
                LoadContent();
                m_loading = false;
            }
        }

        public virtual void LoadContent() { }

        public virtual string ComboTitle() { return null; }

        public virtual WriterFileContentBase CreateContent()
        {
            return null;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            Translating.TranslateControl(this);
        }
    }
}
