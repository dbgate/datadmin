using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace DatAdmin
{
    public partial class HtmlWidgetFrame : WidgetBaseFrame
    {
        string m_text;

        public HtmlWidgetFrame(IHtmlWidget widget)
            : base(widget)
        {
            InitializeComponent();
        }

        IHtmlWidget Widget { get { return (IHtmlWidget)m_widget; } }

        protected override void DoLoadData()
        {
            htmlPanel1.Procedures.Clear();
            if (m_appobj == null) m_text = "";
            else m_text = Widget.CreateHtml(m_appobj, ConnPack, htmlPanel1.Procedures);
        }

        protected override void ShowDataInGui()
        {
            htmlPanel1.Text = m_text;
        }
    }
}
