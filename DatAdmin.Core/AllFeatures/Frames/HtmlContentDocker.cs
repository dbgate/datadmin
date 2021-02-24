using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class HtmlContentDocker : DockerBase
    {
        bool m_loaded;

        public HtmlContentDocker(IDockerFactory fact)
            : base(fact)
        {
            InitializeComponent();
        }

        public HtmlContentDocker()
        {
            InitializeComponent();
        }

        public override void DockerVisibleChanged(bool visible)
        {
            base.DockerVisibleChanged(visible);
            if (visible && !m_loaded) RefreshHtml();
        }

        public void RefreshHtml()
        {
            htmlPanel1.Text = GetHtml();
            m_loaded = true;
        }

        public virtual string GetHtml()
        {
            return "";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshHtml();
        }

        protected Dictionary<string, object> Procedures
        {
            get { return htmlPanel1.Procedures; }
        }
    }
}
