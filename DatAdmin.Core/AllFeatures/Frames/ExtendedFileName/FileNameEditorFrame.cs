using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace DatAdmin
{
    public partial class FileNameEditorFrame : UserControl, ITabbedEditor
    {
        IExtendedFileNameHolderInfo m_obj;
        PropertyInfo m_prop;
        static Dictionary<bool, string> m_lastSelectedTypes = new Dictionary<bool, string>();
        //IFilePlace m_name;
        //ExtendedFileName m_name = new ExtendedFileName();

        public FileNameEditorFrame()
        {
            InitializeComponent();
            Translating.TranslateControl(this);
            this.Disposed += new EventHandler(FileNameEditorFrame_Disposed);
        }

        void FileNameEditorFrame_Disposed(object sender, EventArgs e)
        {
            var place = (IFilePlace)addonSelectFrame1.SelectedObject;
            if (place != null && !String.IsNullOrEmpty(place.GetVirtualFile()))
            {
                m_lastSelectedTypes[m_obj.DirectionIsSave] = AddonTool.ExtractAddonName(place);
            }
        }

        private void DoReloadAddons()
        {
            addonSelectFrame1.Reload(false);
        }

        #region ITabbedEditor Members

        public void LoadFromObject(object obj, PropertyInfo prop)
        {
            m_obj = (IExtendedFileNameHolderInfo)obj;
            m_prop = prop;
            var place = (IFilePlace)prop.CallGet(obj);
            addonSelectFrame1.SelectObject(place);
            if (place == null || String.IsNullOrEmpty(place.ToString()))
            {
                if (m_lastSelectedTypes.ContainsKey(m_obj.DirectionIsSave)) addonSelectFrame1.DefaultAddonHolder = m_lastSelectedTypes[m_obj.DirectionIsSave];
                MainWindow.Instance.RunInMainWindow(DoReloadAddons);
            }
            else
            {
                addonSelectFrame1.DefaultAddonHolder = null;
            }
        }

        public string PageTitle
        {
            get { return m_obj.DirectionIsSave ? "s_target" : "s_source"; }
        }

        #endregion

        //private void SaveToObject()
        //{
        //    var place = (IFilePlace)addonSelectFrame1.SelectedObject;
        //    m_prop.CallSet(m_obj, place);
            //if (rbtClipboard.Checked) m_name.FileType = ExtendedFileName.FileTypeEnum.Clipboard;
            //else if (rbtCommandLine.Checked)
            //{
            //    m_name.FileType = ExtendedFileName.FileTypeEnum.CommandLine;
            //    m_name.FilePath = tbxApplication.Text;
            //    m_name.Arguments = tbxArguments.Text;
            //}
            //else if (rbtFile.Checked)
            //{
            //    m_name.FileType = ExtendedFileName.FileTypeEnum.FileSystem;
            //    m_name.FilePath = tbxFileName.Text;
            //}
            //else if (rbtOpenExternal.Checked) m_name.FileType = ExtendedFileName.FileTypeEnum.OpenExternal;
            //else if (rbtOpenInNewWindow.Checked) m_name.FileType = ExtendedFileName.FileTypeEnum.OpenInternal;
            //else if (rbtWeb.Checked)
            //{
            //    m_name.FileType = ExtendedFileName.FileTypeEnum.Web;
            //    m_name.FilePath = tbxWebAddress.Text;
            //}
            //m_prop.CallSet(m_obj, m_name.VirtualFileName);
        //}

        private void addonSelectFrame1_ChangedSelectedObject(object sender, EventArgs e)
        {
            var place = (IFilePlace)addonSelectFrame1.SelectedObject;
            m_prop.CallSet(m_obj, place);
            place.SetFileHolderInfo(m_obj);
        }

        private void addonSelectFrame1_FilterAddon(object sender, FilterAddonEventArgs e)
        {
            if (m_obj == null) return;
            var place = (IFilePlace)e.InstanceModel;
            if (m_obj.DirectionIsSave && !place.SupportsSave(null)) e.Skip = true;
            if (!m_obj.DirectionIsSave && !place.SupportsLoad(null)) e.Skip = true;
        }

        //private void rbtFile_CheckedChanged(object sender, EventArgs e)
        //{
        //    tbxFileName.Enabled = rbtFile.Checked;
        //    tbxArguments.Enabled = tbxApplication.Enabled = rbtCommandLine.Checked;
        //    tbxWebAddress.Enabled = rbtWeb.Checked;
        //    SaveToObject();
        //}

        //private void btnBrowse_Click(object sender, EventArgs e)
        //{
        //    FileDialog dlg = null;
        //    if (m_obj.DirectionIsSave) dlg = saveFileDialog1;
        //    else dlg = openFileDialog1;
        //    dlg.Filter = m_obj.Filter;
        //    dlg.FileName = tbxFileName.Text;
        //    if (dlg.ShowDialogEx() == DialogResult.OK)
        //    {
        //        tbxFileName.Text = dlg.FileName;
        //    }
        //}

        //private void btnBrowseApp_Click(object sender, EventArgs e)
        //{
        //    openFileDialogExe.Filter = Texts.Replace("{s_applications (*.exe)}|*.exe");
        //    openFileDialogExe.FileName = tbxApplication.Text;
        //    if (openFileDialogExe.ShowDialogEx() == DialogResult.OK)
        //    {
        //        tbxApplication.Text = openFileDialogExe.FileName;
        //    }
        //}
    }
}
