using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.apps
{
    public partial class AppDesignForm : FormEx
    {
        string m_appfile;
        Application m_app;

        public AppDesignForm(string appfile)
        {
            InitializeComponent();
            m_appfile = appfile;
            m_app = new Application();
            m_app.LoadFromFile(appfile);
            if (m_app.Database != null) tbxDatabase.Text = m_app.Database.ToString("S");
            daTreeView1.Root = m_app.Root;
            ReloadPages();
        }

        public static void Run(string appfile)
        {
            var win = new AppDesignForm(appfile);
            win.Show();
        }

        private void scloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            m_app.SaveToFile(m_appfile);
        }

        private void btnAddPage_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null)
            {
                string title = InputBox.Run("s_name_of_new_page", "newpage");
                if (title != null)
                {
                    SelectedNode.Pages.Add(new AppPage { Title = title });
                    ReloadPages();
                }
            }
        }

        public AppTreeTplNode SelectedNode
        {
            get
            {
                return daTreeView1.Selected as AppTreeTplNode;
            }
        }

        public AppDesigner SelectedDesigner
        {
            get
            {
                if (tabControl1.SelectedTab == null) return null;
                foreach (Control ctrl in tabControl1.SelectedTab.Controls)
                {
                    if (ctrl is AppPageFrame) return ((AppPageFrame)ctrl).Designer;
                }
                return null;
            }
        }

        private void ReloadPages()
        {
            foreach (TabPage page in tabControl1.TabPages)
            {
                foreach (Control ctrl in page.Controls)
                {
                    if (ctrl is AppPageFrame) ((AppPageFrame)ctrl).Designer.ChangedSelectedWidget -= Designer_ChangedSelectedWidget;
                    ctrl.Dispose();
                }
            }
            tabControl1.TabPages.Clear();
            propertyFrame1.SelectedObject = null;
            if (SelectedNode != null)
            {
                foreach (var page in SelectedNode.Pages)
                {
                    var tab = new TabPage(page.Title);
                    tabControl1.TabPages.Add(tab);
                    AppPageFrame frm = new AppPageFrame(page);
                    frm.Database = m_app.Database;
                    tab.Controls.Add(frm);
                    frm.Designer.ChangedSelectedWidget += Designer_ChangedSelectedWidget;
                    frm.Designer.Click += Designer_ChangedSelectedWidget;
                    frm.Dock = DockStyle.Fill;
                }
            }
        }

        void Designer_ChangedSelectedWidget(object sender, EventArgs e)
        {
            var dsgn = (AppDesigner)sender;
            propertyFrame1.SelectedObject = dsgn.SelectedWidget;
        }

        private void daTreeView1_ActiveNodeChange(object sender, EventArgs e)
        {
            ReloadPages();
        }

        private void btnChangeDatabase_Click(object sender, EventArgs e)
        {
            var db = TreeSelectForm.SelectDatabase();
            if (db != null)
            {
                m_app.Database = db;
                tbxDatabase.Text = m_app.Database.ToString("S");
                foreach (TabPage page in tabControl1.TabPages)
                {
                    foreach (Control ctrl in page.Controls)
                    {
                        if (ctrl is AppPageFrame) ((AppPageFrame)ctrl).Database = m_app.Database;
                    }
                }
            }
        }

        private void btnRemovePage_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null)
            {
                AppPage page = SelectedNode.Pages[tabControl1.SelectedIndex];
                if (MessageBox.Show(Texts.Get("s_really_remove$page", "page", page.Title), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SelectedNode.Pages.RemoveAt(tabControl1.SelectedIndex);
                    ReloadPages();
                }
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex >= 0 && SelectedNode != null)
            {
                propertyFrame1.SelectedObject = SelectedNode.Pages[tabControl1.SelectedIndex];
            }
        }

        private void daTreeView1_Click(object sender, EventArgs e)
        {
            propertyFrame1.SelectedObject = daTreeView1.Selected;
        }

        private void snextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedDesigner != null) SelectedDesigner.ToggleNextWidget();
        }
    }
}
