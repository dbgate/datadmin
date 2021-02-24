using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DatAdmin
{
    public partial class BulkCopyWizard : FormEx
    {
        ITabularDataStore m_source;
        ITabularDataStore m_target;
        bool m_showSource = true;
        bool m_showTarget = true;
        bool m_wpcolmapError = false;

        public BulkCopyWizard(ITabularDataStore source, ITabularDataStore target)
        {
            InitializeComponent();
            m_source = source;
            m_target = target;

            bool isConfig = false;
            if (m_source != null)
            {
                m_source.Mode = TabularDataStoreMode.Read;
                if (!m_source.ConfigurationNeeded)
                {
                    wizard1.Pages.Remove(wpsource);
                    m_showSource = false;
                }
                else
                {
                    isConfig = true;
                    this.source.SetDataStore(m_source);
                }
            }

            if (m_target != null)
            {
                m_target.Mode = TabularDataStoreMode.Write;
                if (!m_target.ConfigurationNeeded)
                {
                    wizard1.Pages.Remove(wptarget);
                    m_showTarget = false;
                }
                else
                {
                    isConfig = true;
                }
                if (m_target != null) this.target.SetDataStore(m_target);
            }
            if (!isConfig && m_source != null && m_target != null)
            {
                InitColumnMap();
            }
            wizard1.PageIndex = 0;
        }

        bool m_sourceCacheCleared;

        private void PrepareSource()
        {
            if (m_showSource)
            {
                m_source = source.DataStore;
                Async.SafeOpen(m_source.Connection);
            }
            if (m_source != null && !m_sourceCacheCleared)
            {
                m_source.ClearCaches();
                m_sourceCacheCleared = true;
            }
        }

        public bool FixedSource
        {
            get { return source.FixedMode; }
            set { source.FixedMode = value; }
        }

        public bool FixedTarget
        {
            get { return target.FixedMode; }
            set { target.FixedMode = value; }
        }

        private void InitColumnMap()
        {
            PrepareSource();
            if (m_showTarget)
            {
                m_target = target.DataStore;
                Async.SafeOpen(m_target.Connection);
            }
            if (m_target != null) m_target.ClearCaches();
            columnMapFrame1.SetBindings(m_source, m_target);
        }

        private void wpcolmap_ShowFromNext(object sender, EventArgs e)
        {
            try
            {
                wizard1.NextEnabled = false;
                if (m_wpcolmapError)
                {
                    m_wpcolmapError = false;
                    return;
                }

                try
                {
                    InitColumnMap();
                }
                catch (BulkCopyInputError err)
                {
                    Errors.Report(err);
                    wizard1.Back();
                    if (m_showSource && m_showTarget) wizard1.Back();
                }
                catch (Exception err)
                {
                    Errors.Report(err);
                    wizard1.Back();
                }
            }
            finally
            {
                wizard1.NextEnabled = true;
            }
        }

        private Job CreateJob()
        {
            return BulkCopyJob.Create(m_source, m_target, columnMapFrame1.GetTransform(), tableCopyOptionsFrame1.GetOptions(), null);
        }

        private void wpfinish_CloseFromNext(object sender, Gui.Wizard.PageEventArgs e)
        {
            if (!m_target.AvailableRowFormat) m_target.SetRowFormat(columnMapFrame1.GetTargetRowFormat());
            CreateJob().StartProcess();
            //BulkCopyProcess.StartProcess(m_source, m_target, columnMapFrame1.GetTransform());
        }

        private void wpsource_CloseFromNext(object sender, Gui.Wizard.PageEventArgs e)
        {
             if (source.DataStore != null)
            {
                try
                {
                    source.DataStore.CheckConfiguration();
                }
                catch (Exception err)
                {
                    Errors.Report(err);
                    e.Page = wpsource;
                }
            }
            else
            {
                StdDialog.ShowError("s_please_select_valid_source");
                e.Page = wpsource;
            }
             m_sourceCacheCleared = false;
        }

        private void wptarget_CloseFromNext(object sender, Gui.Wizard.PageEventArgs e)
        {
            if (target.DataStore != null)
            {
                try
                {
                    target.DataStore.CheckConfiguration();
                }
                catch (Exception err)
                {
                    Errors.Report(err);
                    e.Page = wptarget;
                }
            }
            else
            {
                StdDialog.ShowError("s_please_select_valid_target");
                e.Page = wptarget;
            }
        }

        private void wpfinish_ShowFromNext(object sender, EventArgs e)
        {
            labSource.Text = m_source.ToString();
            labTarget.Text = m_target.ToString();
        }

        public static void Run(ITabularDataStore source, ITabularDataStore target)
        {
            BulkCopyWizard wizard = new BulkCopyWizard(source, target);
            wizard.ShowDialogEx();
        }

        private void btnExportAsJob_Click(object sender, EventArgs e)
        {
            Job.AskAndExportToFile(CreateJob);
        }

        private void BulkCopyWizard_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_source != null) m_source.CloseAllResources();
            if (m_target != null) m_target.CloseAllResources();
            MainWindow.Instance.RunInMainWindow(Dispose);
        }

        private void wizard1_OnTranslate(object sender, Gui.Wizard.TranslateTextEventArgs e)
        {
            e.TranslatedText = Texts.Get(e.OriginalText);
        }

        private void wpcolmap_CloseFromNext(object sender, Gui.Wizard.PageEventArgs e)
        {
            try
            {
                var tran = columnMapFrame1.GetTransform();
            }
            catch (Exception err)
            {
                StdDialog.ShowError(err.Message);
                m_wpcolmapError = true;
                e.Page = wpcolmap;
            }
        }

        private void wptarget_ShowFromNext(object sender, EventArgs e)
        {
            PrepareSource();
            //target.SetSourceDataStore(m_source);
        }
    }
}
