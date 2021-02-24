using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class MessageLogFrame : UserControl
    {
        ILogMessageSource m_source;
        ImageCache m_imgCache;
        int m_traceCount;
        LogRecordDetailForm m_detailForm;
        bool m_disposed;
        MessageFrameStyle m_style = MessageFrameStyle.None;

        public MessageLogFrame()
        {
            InitializeComponent();
            codeEditor1.Dock = DockStyle.Fill;
            lvMessages.Dock = DockStyle.Fill;
            Disposed += new EventHandler(MessageLogFrame_Disposed);
            cbxStyle.Items.Add(Texts.Get("s_log_as_list"));
            cbxStyle.Items.Add(Texts.Get("s_log_as_trace"));
            Style = MessageFrameStyle.List;
            m_imgCache = new ImageCache(imageList1, Color.White);
        }

        public event LogMessageEventHandler MessageDoubleClick;

        void MessageLogFrame_Disposed(object sender, EventArgs e)
        {
            m_disposed = true;
            Source = null;
            if (m_detailForm != null) m_detailForm.Dispose();
        }

        public ILogMessageSource Source
        {
            get
            {
                return m_source;
            }
            set
            {
                if (m_detailForm != null)
                {
                    m_detailForm.Dispose();
                    m_detailForm = null;
                }
                if (m_source != null) m_source.OnMessage -= new LogMessageEventHandler(m_source_OnMessage);
                m_source = value;
                if (m_source != null) m_source.OnMessage += new LogMessageEventHandler(m_source_OnMessage);
                FillMessages();
            }
        }

        bool IsTargetList { get { return m_style == MessageFrameStyle.List || m_style == MessageFrameStyle.Simple; } }
        bool IsTargetTrace { get { return m_style == MessageFrameStyle.Trace; } }

        private void FillMessages()
        {
            if (m_disposed) return;
            m_traceCount = 0;
            if (IsTargetList)
            {
                lvMessages.Items.Clear();
                try
                {
                    lvMessages.BeginUpdate();
                    if (m_source != null)
                    {
                        var msgs = m_source.GetMessages();
                        foreach (var msg in msgs)
                        {
                            AddLogMessage(msg);
                        }
                    }
                }
                finally
                {
                    lvMessages.EndUpdate();
                }
            }
            if (IsTargetTrace)
            {
                StringBuilder sb = new StringBuilder();
                if (m_source != null)
                {
                    var msgs = m_source.GetMessages();
                    foreach (var msg in msgs)
                    {
                        sb.AppendLine(msg.Message);
                        m_traceCount++;
                    }
                }
                codeEditor1.Document.TextContent = sb.ToString();
            }
        }

        private void AddLogMessage(LogMessageRecord msg)
        {
            if (IsTargetList)
            {
                if (m_source.Capacity != null)
                {
                    while (lvMessages.Items.Count > m_source.Capacity) lvMessages.Items.RemoveAt(0);
                }
                ListViewItem added = lvMessages.Items.Add(Texts.Get(msg.Level.GetTitle()), m_imgCache.GetImageIndex(msg.Level.GetImage()));
                added.SubItems.Add(Texts.Get(msg.Category));
                added.SubItems.Add(msg.Message);
                added.Tag = msg;
                if (btnAutoScroll.Checked) added.EnsureVisible();
            }
            if (IsTargetTrace)
            {
                codeEditor1.ReadOnly = false;
                codeEditor1.Document.Insert(codeEditor1.Document.TextLength, msg.Message);
                codeEditor1.Document.Insert(codeEditor1.Document.TextLength, "\n");
                if (btnAutoScroll.Checked) codeEditor1.ActiveTextAreaControl.Caret.Line = codeEditor1.Document.LineSegmentCollection.Count - 1;
                codeEditor1.ReadOnly = true;
                m_traceCount++;
                if (m_source.Capacity != null && m_traceCount > m_source.Capacity * 2)
                {
                    FillMessages();
                }
            }
        }

        void m_source_OnMessage(object sender, LogMessageEventArgs e)
        {
            try { Invoke((Action)(() => { AddLogMessage(e.LogRecord); })); }
            catch { }
        }

        private LogMessageRecord SelectedRecord
        {
            get
            {
                if (lvMessages.SelectedItems.Count > 0)
                {
                    return lvMessages.SelectedItems[0].Tag as LogMessageRecord;
                }
                return null;
            }
        }

        private void lvMessages_DoubleClick(object sender, EventArgs e)
        {
            var msg = SelectedRecord;
            if (msg == null) return;
            LogMessageEventArgs ev = new LogMessageEventArgs
            {
                LogRecord = msg,
                Handled = false
            };
            if (MessageDoubleClick != null)
            {
                MessageDoubleClick(sender, ev);
            }
            if (!ev.Handled)
            {
                ShowDetail(msg);
            }
        }

        private void ShowDetail(LogMessageRecord rec)
        {
            if (rec == null) return;
            if (m_detailForm == null)
            {
                m_detailForm = new LogRecordDetailForm(m_source);
                m_detailForm.Disposed += new EventHandler(m_detailForm_Disposed);
            }
            m_detailForm.Show();
            m_detailForm.Current = rec;
        }

        void m_detailForm_Disposed(object sender, EventArgs e)
        {
            m_detailForm = null;
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var msgs = m_source.GetMessages();
            foreach (var msg in msgs)
            {
                sb.AppendFormat("{0}|{1}|{2}\r\n", msg.Category, msg.Level, msg.Message);
            }
            if (sb.Length > 0) Clipboard.SetText(sb.ToString());
        }

        private void cbxStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_afterChangingStyle) return;
            Style = cbxStyle.SelectedIndex == 0 ? MessageFrameStyle.List : MessageFrameStyle.Trace;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            codeEditor1.ShowFindDialog();
        }

        bool m_afterChangingStyle = false;
        private void AfterChangeStyle()
        {
            m_afterChangingStyle = true;
            try
            {
                switch (m_style)
                {
                    case MessageFrameStyle.List:
                        cbxStyle.SelectedIndex = 0;
                        break;
                    case MessageFrameStyle.Trace:
                        cbxStyle.SelectedIndex = 1;
                        break;
                    default:
                        cbxStyle.SelectedIndex = -1;
                        break;
                }

                toolStrip1.Visible = m_style != MessageFrameStyle.Simple;
                codeEditor1.Visible = IsTargetTrace;
                lvMessages.Visible = IsTargetList;
                if (lvMessages.Visible) lvMessages.BringToFront();
                if (codeEditor1.Visible) codeEditor1.BringToFront();
                lvMessages.HeaderStyle = m_style == MessageFrameStyle.List ? ColumnHeaderStyle.Nonclickable : ColumnHeaderStyle.None;
                FillMessages();
                btnFind.Enabled = codeEditor1.Visible;
                btnCopyToClipboard.Enabled = lvMessages.Visible;
                btnShowDetail.Enabled = lvMessages.Visible;
            }
            finally
            {
                m_afterChangingStyle = false;
            }
        }

        public MessageFrameStyle Style
        {
            get
            {
                return m_style;
            }
            set
            {
                if (m_style != value)
                {
                    m_style = value;
                    AfterChangeStyle();
                }
            }
        }

        private void btnShowDetail_Click(object sender, EventArgs e)
        {
            ShowDetail(SelectedRecord);
        }
    }

    public enum MessageFrameStyle { None, List, Trace, Simple }
}
