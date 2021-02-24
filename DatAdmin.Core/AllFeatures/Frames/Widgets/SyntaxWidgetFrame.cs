using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class SyntaxWidgetFrame : WidgetBaseFrame
    {
        string m_text;

        public SyntaxWidgetFrame(ISyntaxTextWidget view)
            : base(view)
        {
            InitializeComponent();
        }

        private ISyntaxTextWidget Widget
        {
            get { return (ISyntaxTextWidget)m_widget; }
        }

        protected override void LoadFromObject(AppObject appobj)
        {
            codeEditor1.Language = Widget.ViewLanguage;
            codeEditor1.Dialect = Widget.GetDialect(appobj, ConnPack);
        }

        protected override void DoLoadData()
        {
            if (m_appobj == null) m_text = "";
            else m_text = Widget.CreateText(m_appobj, ConnPack);
        }

        protected override void ShowDataInGui()
        {
            codeEditor1.Text = m_text;
        }
    }
}
