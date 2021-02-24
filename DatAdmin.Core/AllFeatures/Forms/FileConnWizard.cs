using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DatAdmin;
using System.Data.Common;

namespace DatAdmin
{
    public partial class FileConnWizard : FormEx
    {
        protected string m_name;
        protected ITreeNode m_parent;
        protected string m_filename;
        int m_fullHeight;
        protected Control m_createNewExFrame;
        protected Control m_openExistingExFrame;
        Control m_shownExFrame;

        public FileConnWizard()
        {
            InitializeComponent();
            m_fullHeight = Height;
        }

        public FileConnWizard(ITreeNode parent, string name)
        {
            InitializeComponent();
            m_fullHeight = Height;
            rbnOpenExisting.Checked = true;
            m_name = name;
            m_parent = parent;
            CreateCore();
        }

        private void CreateCore()
        {
            foreach (string version in Versions) cbxVersion.Items.Add(version);
            string defver = DefaultVersion;
            if (defver != null)
            {
                cbxVersion.SelectedIndex = cbxVersion.Items.IndexOf(defver);
            }
            if (cbxVersion.SelectedIndex < 0 && cbxVersion.Items.Count > 0) cbxVersion.SelectedIndex = 0;
        }

        public FileConnWizard(string filename)
        {
            InitializeComponent();
            m_fullHeight = Height;
            m_filename = filename;
            rbnCreateNew.Checked = true;
            rbnOpenExisting.Enabled = false;
            CreateCore();
        }

        public string Filename { get { return tbFileName.Text; } }
        //public bool UseAutentization { get { return cbxUseAutentization.Checked; } set { cbxUseAutentization.Checked = value; } }
        //public string Login { get { return tbxLogin.Text; } set { tbxLogin.Text = value; } }
        //public string Password { get { return tbxPassword.Text; } set { tbxPassword.Text = value; } }

        private void ChangedCreateNew(object sender, EventArgs e)
        {
            tbFileName.Enabled = btnBrowse.Enabled = !CreateNew;
            cbxVersion.Enabled = CreateNew;
            RefreshAuthMode();
        }

        public bool CreateNew { get { return rbnCreateNew.Checked; } }

        public virtual void DoCreateFile() { throw new NotImplementedError("DAE-00078"); }
        public virtual void DoCreateFile(string filename) { throw new NotImplementedError("DAE-00079"); }
        //public abstract DbConnection CreateConnection();
        //public abstract string GenerateOpenConnectionString();
        public virtual IEnumerable<string> Versions { get { throw new NotImplementedError("DAE-00080"); } }
        public virtual string DefaultVersion { get { return null; } }
        public virtual string OpenFileDialogFilter { get { throw new NotImplementedError("DAE-00081"); } }
        public virtual void DoOpenFile() { throw new NotImplementedError("DAE-00082"); }

        protected string SelectedVersion { get { return cbxVersion.SelectedItem.SafeToString(); } }

        protected string OpenFileName { get { return tbFileName.Text; } }

        protected string GetNewFilePath(string extWithDot)
        {
            return Path.Combine(m_parent.FileSystemPath, m_name + extWithDot);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (CreateNew)
                {
                    if (m_filename != null)
                    {
                        DoCreateFile(m_filename);
                    }
                    else
                    {
                        DoCreateFile();
                    }
                }
                else
                {
                    DoOpenFile();
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception err)
            {
                Errors.Report(err);
                return;
            }
        }

        private void btbrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = OpenFileDialogFilter;
            openFileDialog1.FileName = tbFileName.Text;
            if (openFileDialog1.ShowDialogEx() == DialogResult.OK)
            {
                tbFileName.Text = openFileDialog1.FileName;
            }
        }

        //private void cbxUseAutentization_CheckedChanged(object sender, EventArgs e)
        //{
        //    tbxLogin.Enabled = tbxPassword.Enabled = cbxUseAutentization.Checked;
        //}

        private void PlaceFrame(Control frame)
        {
            Controls.Add(frame);
            frame.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            frame.Top = panelPlaceholder.Top;
            frame.Width = panelPlaceholder.Width;
            frame.Left = panelPlaceholder.Left;
            m_shownExFrame = frame;
        }

        protected void RefreshAuthMode()
        {
            if (m_shownExFrame != null)
            {
                Controls.Remove(m_shownExFrame);
                m_shownExFrame = null;
            }

            if (CreateNew && m_createNewExFrame != null)
            {
                PlaceFrame(m_createNewExFrame);
            }
            if (!CreateNew && m_openExistingExFrame != null)
            {
                PlaceFrame(m_openExistingExFrame);
            }
            Height = m_fullHeight + (m_shownExFrame != null ? m_shownExFrame.Height : 0);

            //var mode = CreateNew ? AuthModeCreate : AuthModeOpen;

            //cbxUseAutentization.Visible = mode >= FileAuthMode.Password;
            //labLogin.Visible = tbxLogin.Visible = mode == FileAuthMode.Full;
            //labPassword.Visible = tbxPassword.Visible = mode >= FileAuthMode.Password;
            //switch (mode)
            //{
            //    case FileAuthMode.Full:
            //        Height = m_fullHeight;
            //        break;
            //    case FileAuthMode.Password:
            //        Height = m_fullHeight - 26;
            //        break;
            //    case FileAuthMode.None:
            //        Height = m_fullHeight - 80;
            //        break;
            //}
        }

        //public FileAuthMode AuthModeOpen
        //{
        //    get
        //    {
        //        return m_authModeOpen;
        //    }
        //    set
        //    {
        //        m_authModeOpen = value;
        //        RefreshAuthMode();
        //    }
        //}

        //public FileAuthMode AuthModeCreate
        //{
        //    get
        //    {
        //        return m_authModeCreate;
        //    }
        //    set
        //    {
        //        m_authModeCreate = value;
        //        RefreshAuthMode();
        //    }
        //}
    }

    //public enum FileAuthMode { None, Password, Full  }
}
