using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DatAdmin
{
    public partial class LogRecordDetailForm : FormEx
    {
        ILogMessageSource m_source;
        LogMessageRecord m_current;

        public LogRecordDetailForm(ILogMessageSource source)
        {
            InitializeComponent();
            m_source = source;
        }

        public LogMessageRecord Current
        {
            get { return m_current; }
            set
            {
                m_current = m_source.SeekRecord(value, SeekOrigin.Current, 0, false);
                LoadCurrent();
            }
        }

        private void LoadCurrent()
        {
            if (m_current == null)
            {
                tbxCategory.Text = "";
                tbxCreated.Text = "";
                tbxDetail.Text = "";
                tbxMessage.Text = "";
                tbxSeverity.Text = "";
                pictureBox1.Image = null;
            }
            else
            {
                tbxCategory.Text = Texts.Get(m_current.Category);
                tbxCreated.Text = m_current.Created.ToString();
                tbxDetail.Text = m_current.Detail;
                tbxMessage.Text = m_current.Message;
                tbxSeverity.Text = Texts.Get(m_current.Level.GetTitle());
                pictureBox1.Image = m_current.Level.GetImage();
            }
        }

        private void Seek(SeekOrigin origin, int pos)
        {
            if (m_source != null)
            {
                m_current = m_source.SeekRecord(m_current, origin, pos, true);
                LoadCurrent();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            Seek(SeekOrigin.Current, -1);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Seek(SeekOrigin.Current, 1);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            Seek(SeekOrigin.Begin, 0);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            Seek(SeekOrigin.End, 0);
        }

        private void LogRecordDetailForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (tbxMessage.Focused) return;
            if (tbxDetail.Focused) return;
            if (e.KeyCode == Keys.PageUp) Seek(SeekOrigin.Current, -10);
            if (e.KeyCode == Keys.PageDown) Seek(SeekOrigin.Current, 10);
            if (e.KeyCode == Keys.Up) Seek(SeekOrigin.Current, -1);
            if (e.KeyCode == Keys.Down) Seek(SeekOrigin.Current, 1);
            if (e.KeyCode == Keys.Home) Seek(SeekOrigin.Begin, 0);
            if (e.KeyCode == Keys.End) Seek(SeekOrigin.End, 0);
            if (e.KeyCode == Keys.Escape) Close();
        }
    }
}
