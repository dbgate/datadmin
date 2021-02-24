using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.apps
{
    public partial class AppPageFrame : UserControl
    {
        AppEnv m_env = new AppEnv();

        public AppPageFrame(AppPage page)
        {
            InitializeComponent();
            appDesigner1.Page = page;
        }

        public AppPage Page
        {
            get { return appDesigner1.Page; }
            set { appDesigner1.Page = value; }
        }

        public IDatabaseSource Database { get; set; }

        public AppDesigner Designer { get { return appDesigner1; } }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                if (Database == null)
                {
                    StdDialog.ShowError("s_please_select_database");
                    tabControl1.SelectedIndex = 0;
                    return;
                }
                foreach (Control ctrl in tabControl1.TabPages[1].Controls) ctrl.Dispose();
                var page = appDesigner1.Page.CreateControl(Database, m_env);
                tabControl1.TabPages[1].Controls.Add(page);
                //page.Width = tabControl1.TabPages[1].Width;
                //page.Height = tabControl1.TabPages[1].Height;
                page.Dock = DockStyle.Fill;
            }
        }
    }
}
