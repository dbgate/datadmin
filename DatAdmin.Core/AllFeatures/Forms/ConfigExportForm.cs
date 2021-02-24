using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class ConfigExportForm : FormEx, IExtendedFileNameHolderInfo
    {
        IVirtualFileSystem m_fs;

        public ConfigExportForm(IVirtualFileSystem fs)
        {
            InitializeComponent();
            m_fs = fs;
            configSelectionFrame1.FileSystem = m_fs;
            addonSelectFrame1.Reload(false);
        }

        public static bool Run(IVirtualFileSystem fs)
        {
            var win = new ConfigExportForm(fs);
            return win.ShowDialogEx() == DialogResult.OK;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var place = (IFilePlace)addonSelectFrame1.SelectedObject;
            try
            {
                place.CheckOutput();
            }
            catch (Exception err)
            {
                Errors.Report(err);
                return;
            }
            string fn = place.GetWorkingFileName();
            using (var zipfs = new ZipFileSystem(fn))
            {
                configSelectionFrame1.Root.CopyCheckedTo(zipfs, true, null);
                zipfs.Flush();
            }
            place.FinalizeFileName();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void addonSelectFrame1_ChangedSelectedObject(object sender, EventArgs e)
        {
            if (addonSelectFrame1.SelectedObject != null) ((IFilePlace)addonSelectFrame1.SelectedObject).SetFileHolderInfo(this);
        }


        #region IExtendedFileNameHolderInfo Members

        public string Filter
        {
            get { return "{s_datadmin_configuration_archive} (*.dca)|*.dca"; }
        }

        public string FileExtension
        {
            get { return "dca"; }
        }

        public bool DirectionIsSave
        {
            get { return true; }
        }

        [Browsable(false)]
        public AppObject RelatedObject { get { return null; } }

        [Browsable(false)]
        public IPhysicalConnectionFactory RelatedConnection { get { return null; } }

        [Browsable(false)]
        public string RelatedDatabase { get { return null; } }

        #endregion

        private void addonSelectFrame1_FilterAddon(object sender, FilterAddonEventArgs e)
        {
            var place = (IFilePlace)e.InstanceModel;
            if (!place.SupportsSave(null)) e.Skip = true;
        }
    }
}
