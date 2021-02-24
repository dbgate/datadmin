using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class ImportSqlDumpForm : FormEx
    {
        IDatabaseSource m_dst;
        List<IDatabaseLoader> m_loaders;

        public ImportSqlDumpForm(IDatabaseSource dst, List<IDatabaseLoader> loaders)
        {
            InitializeComponent();
            m_loaders = loaders;
            m_dst = dst;
            foreach (var lod in loaders)
            {
                cbxLoader.Items.Add(Texts.Get(lod.GetTitle()));
            }
            cbxLoader.SelectedIndex = 0;
        }

        public static void Run(IDatabaseSource dst)
        {
            var loaders = new List<IDatabaseLoader>();
            if (dst.DatabaseCaps.ExecuteSql && dst.Dialect != null)
            {
                var sl = dst.Dialect.CreateSpecificDatabaseLoader();
                if (sl != null) loaders.Add(sl);
            }
            foreach (var item in DatabaseLoaderAddonType.Instance.CommonSpace.GetFilteredAddons(RegisterItemUsage.DirectUse))
            {
                var loader = (IDatabaseLoader)item.CreateInstance();
                if (!loader.IsSqlDumpLoader) continue;
                if (!loader.SuitableFor(dst)) continue;
                loaders.Add(loader);
            }

            if (loaders.Count == 0)
            {
                StdDialog.ShowError("s_no_dump_loader_available");
                return;
            }

            ImportSqlDumpForm win = new ImportSqlDumpForm(dst, loaders);
            win.ShowDialogEx();
        }

        private void btnSqlFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = tbxFileName.Text;
            dlg.Filter = SelectedLoader.Filter;
            if (dlg.ShowDialogEx() == DialogResult.OK)
            {
                tbxFileName.Text = dlg.FileName;
                SelectedLoader.Filename = tbxFileName.Text;
            }
        }

        private Job CreateJob()
        {
            JobProperties jobProps = new JobProperties
            {
                ContinueOnErrors = cbxContinueOnErrors.Checked
            };
            return ImportSqlDumpJob.CreateJob(SelectedLoader, m_dst, jobProps);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                SelectedLoader.CheckConfiguration(m_dst);
            }
            catch (Exception err)
            {
                Errors.Report(err);
                return;
            }
            Close();
            CreateJob().StartProcess();
        }

        private void btnExportAsJob_Click(object sender, EventArgs e)
        {
            Job.AskAndExportToFile(CreateJob);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbxLoader_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyFrame1.SelectedObject = SelectedLoader;
            if (SelectedLoader != null)
            {
                SelectedLoader.Filename = tbxFileName.Text;
            }
        }

        private IDatabaseLoader SelectedLoader
        {
            get
            {
                if (cbxLoader.SelectedIndex < 0) return null;
                return m_loaders[cbxLoader.SelectedIndex];
            }
        }
    }
}
