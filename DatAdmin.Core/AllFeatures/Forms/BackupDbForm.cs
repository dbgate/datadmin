using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class BackupDbForm : FormEx
    {
        IBackupFormat m_format;
        IDatabaseSource m_source;
        DbDefChooseTreeNode m_chooseNode;

        public BackupDbForm(IDatabaseSource source)
        {
            InitializeComponent();

            m_source = source;
            m_format = (IBackupFormat)addonSelectFrame1.SelectedObject;

            // set here, not in designer so that filtering is applied
            addonSelectFrame1.AddonTypeName = "bakformat";


            //selectTemplateComboFrame1.Configure(BackupFormatAddonType.Instance.CommonSpace, null, "dbk_backup", null);
            //selectTemplateComboFrame1.Enabled = true;
            //RecreateBackupFormat();
            //selectTemplateComboFrame1.SelectedIndex = 0;
        }

        //private void selectTemplateComboFrame1_ChangedSelectedItem(object sender, EventArgs e)
        //{
        //    RecreateBackupFormat();
        //}

        //private void RecreateBackupFormat()
        //{
        //    AddonHolder item = selectTemplateComboFrame1.SelectedAddonHolder;
        //    if (item != null) m_format = (IBackupFormat)item.CreateInstance();
        //    else m_format = new DataArchiveBackupFormat();
        //    propertyFrame1.SelectedObject = m_format;
        //}

        private void InitPropertiesPage()
        {
            try
            {
                Async.SafeOpen(m_source.Connection);
                DatabaseStructureMembers dbmem = new DatabaseStructureMembers
                {
                    TableList = true,
                    SpecificObjectList = true,
                    SchemaList = true,
                    IgnoreSystemObjects = true,
                };
                IDatabaseStructure dbs = m_source.InvokeLoadStructure(dbmem, null);
                if (m_chooseNode == null)
                {
                    m_chooseNode = new DbDefChooseTreeNode(dbs);
                    treObjects.Root = m_chooseNode;
                }
            }
            catch (Exception err)
            {
                Errors.Report(err);
                wizard1.Back();
            }
        }

        private Job CreateJob()
        {
            //return new BackupDbJobCommand(m_source, m_format, m_chooseNode.GetChoosedMembers());
            return BackupDbJob.CreateJob(m_source, m_format, m_chooseNode.GetChoosedMembers(), null);
        }

        private void wpProps_ShowFromNext(object sender, EventArgs e)
        {
            InitPropertiesPage();
        }

        private void wizard1_OnTranslate(object sender, Gui.Wizard.TranslateTextEventArgs e)
        {
            e.TranslatedText = Texts.Get(e.OriginalText);
        }

        private void btnExportAsJob_Click(object sender, EventArgs e)
        {
            Job.AskAndExportToFile(CreateJob);
        }

        private void wpFinish_CloseFromNext(object sender, Gui.Wizard.PageEventArgs e)
        {
            CreateJob().StartProcess();
        }

        public static void Run(IDatabaseSource source)
        {
            BackupDbForm win = new BackupDbForm(source);
            win.ShowDialogEx();
        }

        private void wpTarget_CloseFromNext(object sender, Gui.Wizard.PageEventArgs e)
        {
            try
            {
                m_format.CheckBackupConfiguration(m_source);
            }
            catch (Exception err)
            {
                Errors.Report(err);
                e.Page = wpTarget;
            }
        }

        private void wpFinish_ShowFromNext(object sender, EventArgs e)
        {
            labSource.Text = m_source.ToString();
            labTarget.Text = m_format.ToString();
            labProperties.Text = m_chooseNode.GetChoosedMembers().ToString().Replace("\n", "\r\n");
        }

        private void addonSelectFrame1_ChangedSelectedObject(object sender, EventArgs e)
        {
            m_format = (IBackupFormat)addonSelectFrame1.SelectedObject;
        }

        private void addonSelectFrame1_FilterAddon(object sender, FilterAddonEventArgs e)
        {
            var fmt = (IBackupFormat)e.InstanceModel;
            if (fmt != null && !fmt.BackupSuitableFor(m_source)) e.Skip = true;
        }

        private void BackupDbForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainWindow.Instance.RunInMainWindow(Dispose);
        }
    }
}
