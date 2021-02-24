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
    public partial class CopyDbWizard : FormEx
    {
        IDatabaseSource m_source;
        IDatabaseWriter m_target;
        DatabaseWriterChooser m_writerChooser;
        DbDefChooseTreeNode m_chooseNode;
        IDatabaseStructure m_structure;
        bool m_dataPageInitialized;
        //bool m_created;
        bool m_isSourcePage = true, m_isTargetPage = true, m_isDataPage = true, m_isStructPage = true;
        static string m_lastSelectedAddon;

        public CopyDbWizard(IDatabaseSource source, IDatabaseWriter target)
        {
            InitializeComponent();
            addonSelectFrame1.Reload(true);
            m_source = source;
            m_target = target;

            //selectTemplateComboFrame1.Configure(DatabaseWriterAddonType.Instance.CommonSpace, null, m_target != null ? ReflTools.GetRegisterType(m_target) : "dbk_writer", new List<string> { "s_select_in_tree" });
            //selectTemplateComboFrame1.Enabled = true;

            if (m_lastSelectedAddon != null) addonSelectFrame1.DefaultAddonHolder = m_lastSelectedAddon;

            treSource.TreeBehaviour.ShowFilter = node => node.IsDatabaseNodeOrParent();
            if (m_source != null)
            {
                wizard1.Pages.Remove(wpSource);
                if (m_source.Connection != null) m_source.Connection.Owner = this;
                m_isSourcePage = false;
            }
            else
            {
                treSource.RootPath = "data:";
            }
            if (m_target != null)
            {
                //m_target.OpenConnection();
                if (m_target.ConfigurationNeeded)
                {
                    addonSelectFrame1.SelectObjectImportant(m_target);
                    //propertyFrame1.SelectedObject = m_target;
                }
                else
                {
                    wizard1.Pages.Remove(wpTarget);
                    m_isTargetPage = false;
                }
                if (!m_target.WriterCaps.AcceptData)
                {
                    wizard1.Pages.Remove(wpDataProps);
                    m_isDataPage = false;
                }
                if (!m_target.WriterCaps.AcceptStructure)
                {
                    wizard1.Pages.Remove(wpStructProps);
                    m_isStructPage = false;
                }
            }

            Disposed += new EventHandler(CopyDbWizard_Disposed);

            //m_created = true;
            //if (m_target == null) RecreateWriter();
            if (!m_isSourcePage && !m_isTargetPage && m_isStructPage) InitStructPage();
            if (!m_isSourcePage && !m_isTargetPage && !m_isStructPage && m_isDataPage) InitDataPage();
        }

        //public CopyDbWizard(CopyDbJobCommand command)
        //{
        //    InitializeComponent();
        //    m_source = command.Source;
        //    m_target = command.Target;
        //}

        void CopyDbWizard_Disposed(object sender, EventArgs e)
        {
            if (m_source != null) Async.SafeClose(m_source.Connection);
            if (addonSelectFrame1.SelectedObject != null) m_lastSelectedAddon = AddonTool.ExtractAddonName(addonSelectFrame1.SelectedObject);
        }

        public static void Run(IDatabaseSource source, IDatabaseWriter target)
        {
            CopyDbWizard wizard = new CopyDbWizard(source, target);
            wizard.ShowDialogEx();
        }

        //public static bool Run(CopyDbJobCommand command)
        //{
        //    CopyDbWizard wizard = new CopyDbWizard(command);
        //    return wizard.ShowDialogEx() == DialogResult.OK;
        //}

        private void wpSource_CloseFromNext(object sender, Gui.Wizard.PageEventArgs e)
        {
            IDatabaseTreeNode node = treSource.Selected as IDatabaseTreeNode;
            if (node == null)
            {
                StdDialog.ShowError("s_please_select_database");
                e.Page = wpSource;
                return;
            }
            m_source = node.DatabaseConnection.CloneSource();
            if (m_source.Connection != null) m_source.Connection.Owner = this;
            m_chooseNode = null;
            m_structure = null;
            m_dataPageInitialized = false;
        }

        //private void RecreateWriter()
        //{
        //    if (!m_created) return;
        //    string specitem = selectTemplateComboFrame1.SpecialSelectedItem;
        //    if (specitem == "s_select_in_tree")
        //    {
        //        if (m_writerChooser == null) m_writerChooser = new DatabaseWriterChooser();
        //        propertyFrame1.SelectedObject = m_writerChooser;
        //        m_target = null;
        //    }
        //    else
        //    {
        //        AddonHolder item = selectTemplateComboFrame1.SelectedAddonHolder;
        //        if (item != null)
        //        {
        //            m_target = (IDatabaseWriter)item.CreateInstance();
        //            m_target.InitializeFromInput(m_source);
        //            propertyFrame1.SelectedObject = m_target;
        //        }
        //    }
        //}

        //private void selectTemplateComboFrame1_ChangedSelectedItem(object sender, EventArgs e)
        //{
        //    RecreateWriter();
        //}

        private void wpProps_ShowFromNext(object sender, EventArgs e)
        {
            if (!m_target.WriterCaps.AcceptStructure)
            {
                wizard1.Next();
                return;
            }
            try
            {
                wizard1.NextEnabled = false;
                InitStructPage();
            }
            finally
            {
                wizard1.NextEnabled = true;
            }
        }

        private void WantStructure()
        {
            Async.SafeOpen(m_source.Connection);
            DatabaseStructureMembers dbmem = new DatabaseStructureMembers
            {
                TableList = true,
                DomainList = true,
                SpecificObjectList = true,
                SchemaList = true,
                IgnoreSystemObjects = true,
            };
            m_structure = m_source.InvokeLoadStructure(dbmem, null);
        }

        private void InitStructPage()
        {
            try
            {
                WantStructure();
                chbCopyStructure.Enabled = m_target.WriterCaps.AllowDataOnly;
                if (m_chooseNode == null)
                {
                    m_chooseNode = new DbDefChooseTreeNode(m_structure);
                    treObjects.Root = m_chooseNode;
                }
                if (m_target.WriterCaps.MultipleSchema)
                {
                    // originalni schemata muzeme udrzet jen v pripade, ze nejaka existuji
                    rbOriginalSchema.Enabled = m_chooseNode.m_db.Schemata.Count > 0;

                    if (m_target.WriterCaps.HasStructure)
                    {
                        rbExplicitSchema.Enabled = true;
                        cbxExplicitSchema.Items.Clear();
                        m_target.OpenConnection();
                        foreach (var name in StructLoader.SchemaNames(mem2 => m_target.InvokeLoadStructure(mem2, null))) cbxExplicitSchema.Items.Add(name);
                        if (cbxExplicitSchema.Items.Count > 0) cbxExplicitSchema.SelectedIndex = 0;
                    }
                    else
                    {
                        rbExplicitSchema.Enabled = false;
                    }
                }
                else
                {
                    rbExplicitSchema.Enabled = rbOriginalSchema.Enabled = false;
                }
                if (!rbOriginalSchema.Enabled && rbOriginalSchema.Checked) rbDefaultSchema.Checked = true;
                if (!rbExplicitSchema.Enabled && rbExplicitSchema.Checked) rbDefaultSchema.Checked = true;
                cbxExplicitSchema.Enabled = rbExplicitSchema.Checked;
            }
            catch (Exception err)
            {
                Errors.Report(err);
                wizard1.Back();
            }
        }

        private void InitDataPage()
        {
            infoBoxFrameNoStructureWarning.Visible = m_isStructPage && !chbCopyStructure.Checked;
            if (m_dataPageInitialized) return;
            try
            {
                rbtCopyXXX_CheckedChanged(this, EventArgs.Empty);
                if (m_target.WriterCaps.PreferStructure)
                {
                    rbtCopyNone.Checked = true;
                }
                else
                {
                    rbtCopyAll.Checked = true;
                }
                WantStructure();
                lbxAllTables.Items.Clear();
                foreach (ITableStructure tbl in m_structure.Tables.SortedByKey<ITableStructure, NameWithSchema>(tbl => tbl.FullName))
                {
                    lbxAllTables.Items.Add(tbl.FullName);
                }
                m_dataPageInitialized = true;
            }
            catch (Exception err)
            {
                Errors.Report(err);
                wizard1.Back();
            }
        }

        private void wpTarget_CloseFromNext(object sender, Gui.Wizard.PageEventArgs e)
        {
            if (addonSelectFrame1.SelectedObject is IDatabaseWriter)
            {
                m_target = (IDatabaseWriter)addonSelectFrame1.SelectedObject;
            }
            else if (addonSelectFrame1.SelectedObject is DatabaseWriterChooser)
            {
                m_target = m_writerChooser.GetDatabaseWriter();
            }
            if (m_target != null)
            {
                try
                {
                    m_target.CheckConfiguration(m_source);
                }
                catch (Exception err)
                {
                    Errors.Report(err);
                    e.Page = wpTarget;
                    return;
                }
            }
            else
            {
                StdDialog.ShowError("s_please_select_database");
                e.Page = wpTarget;
            }
        }

        private DatabaseCopyOptions CreateOptions()
        {
            DatabaseCopyOptions opts = new DatabaseCopyOptions();
            if (m_chooseNode != null) opts.CopyMembers = m_chooseNode.GetChoosedMembers();
            if (m_target.Dialect != null) opts.MigrationProfile = m_target.Dialect.CreateMigrationProfile();
            if (rbDefaultSchema.Checked) opts.SchemaMode = DbCopySchemaMode.Explicit;
            if (rbExplicitSchema.Checked)
            {
                opts.SchemaMode = DbCopySchemaMode.Explicit;
                opts.ExplicitSchema = cbxExplicitSchema.Text;
            }
            if (rbOriginalSchema.Checked) opts.SchemaMode = DbCopySchemaMode.Original;
            opts.CopyStructure = chbCopyStructure.Checked;
            opts.DataMode = DbCopyDataMode.None;
            if (rbtCopyAll.Checked) opts.DataMode = DbCopyDataMode.All;
            if (rbtCopySelected.Checked) opts.DataMode = DbCopyDataMode.Selected;
            if (rbtCopyUnselected.Checked) opts.DataMode = DbCopyDataMode.Unselected;
            foreach (NameWithSchema tbl in lbxSelectedTables.Items)
            {
                opts.DataCopyTables.Add(tbl);
            }
            opts.TableOptions = tableCopyOptionsFrame1.GetOptions();
            return opts;
        }

        private Job CreateJob()
        {
            //return new CopyDbJobCommand(m_source, m_target, CreateOptions());
            return CopyDbJob.CreateJob(m_source.CloneSource(), m_target, CreateOptions(), null);
        }

        private void btnExportAsJob_Click(object sender, EventArgs e)
        {
            Job.AskAndExportToFile(CreateJob);
        }

        private void wpFinish_CloseFromNext(object sender, Gui.Wizard.PageEventArgs e)
        {
            CreateJob().StartProcess();
        }

        private void wpFinish_ShowFromNext(object sender, EventArgs e)
        {
            labSource.Text = m_source.ToString();
            labTarget.Text = m_target.ToString();
            labProperties.Text = CreateOptions().ToString().Replace("\n", "\r\n");
        }

        private void rbExplicitSchema_CheckedChanged(object sender, EventArgs e)
        {
            cbxExplicitSchema.Enabled = rbExplicitSchema.Checked;
        }

        public static void Run(IDatabaseSource src, IDatabaseSource dst)
        {
            Run(src, new DatabaseSourceWriter(dst));
        }

        public static void RunExport(IDatabaseSource src)
        {
            Run(src, (IDatabaseWriter)null);
        }

        private void wizard1_OnTranslate(object sender, Gui.Wizard.TranslateTextEventArgs e)
        {
            e.TranslatedText = Texts.Get(e.OriginalText);
        }

        private void btnSaveAsTemplate_Click(object sender, EventArgs e)
        {
            //if (DatabaseWriterAddonType.Instance.SaveCommonTemplate((IAddonInstance)m_target, selectTemplateComboFrame1.SelectedTemplateFileName))
            //{
            //    selectTemplateComboFrame1.ClearCache();
            //}
        }

        private void addonSelectFrame1_GetSpecialItems(object sender, GetSpecialItemsEventArgs e)
        {
            e.SpecialItems.Add("s_select_database");
        }

        private void addonSelectFrame1_CreateSpecialItem(object sender, CreateSpecialItemEventArgs e)
        {
            if (e.SpecialItem == "s_select_database")
            {
                m_writerChooser = new DatabaseWriterChooser();
                e.Instance = m_writerChooser;
            }
        }

        private void chbCopyStructure_CheckedChanged(object sender, EventArgs e)
        {
            treObjects.Enabled = chbCopyStructure.Checked;
            groupBox2.Controls.SetEnabled(chbCopyStructure.Checked);
            groupBox2.Enabled = chbCopyStructure.Checked;
        }

        private void rbtCopyXXX_CheckedChanged(object sender, EventArgs e)
        {
            lbxAllTables.Enabled = lbxSelectedTables.Enabled =
                btnAddAllTables.Enabled = btnAddTable.Enabled =
                btnRemoveAllTables.Enabled = btnRemoveTable.Enabled =
                rbtCopySelected.Checked || rbtCopyUnselected.Checked;
            btnMoveTableDown.Enabled = btnMoveTableUp.Enabled = rbtCopySelected.Checked;
        }

        private void wpDataProps_ShowFromNext(object sender, EventArgs e)
        {
            if (!m_target.WriterCaps.AcceptData)
            {
                wizard1.Next();
                return;
            }
            try
            {
                wizard1.NextEnabled = false;
                InitDataPage();
            }
            finally
            {
                wizard1.NextEnabled = true;
            }
        }

        private void wpStructProps_ShowFromBack(object sender, EventArgs e)
        {
            if (!m_target.WriterCaps.AcceptStructure) wizard1.Back();
        }

        private void wpDataProps_ShowFromBack(object sender, EventArgs e)
        {
            if (!m_target.WriterCaps.AcceptData) wizard1.Back();
        }

        private void addonSelectFrame1_CreatedObject(object sender, EventArgs e)
        {
            var writer = sender as IDatabaseWriter;
            if (writer != null) writer.InitializeFromInput(m_source);
        }

        private void btnMoveTableUp_Click(object sender, EventArgs e)
        {
            if (lbxSelectedTables.SelectedIndex > 0)
            {
                lbxSelectedTables.Items.Exchange(lbxSelectedTables.SelectedIndex, lbxSelectedTables.SelectedIndex - 1);
                lbxSelectedTables.SelectedIndex -= 1;
            }
        }

        private void btnMoveTableDown_Click(object sender, EventArgs e)
        {
            if (lbxSelectedTables.SelectedIndex < lbxSelectedTables.Items.Count - 1)
            {
                lbxSelectedTables.Items.Exchange(lbxSelectedTables.SelectedIndex, lbxSelectedTables.SelectedIndex + 1);
                lbxSelectedTables.SelectedIndex += 1;
            }
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            foreach (var item in lbxAllTables.SelectedItems)
            {
                if (!lbxSelectedTables.Items.Contains(item))
                {
                    lbxSelectedTables.Items.Add(item);
                }
            }
        }

        private void btnAddAllTables_Click(object sender, EventArgs e)
        {
            foreach (object tbl in lbxAllTables.Items)
            {
                if (!lbxSelectedTables.Items.Contains(tbl))
                {
                    lbxSelectedTables.Items.Add(tbl);
                }
            }
        }

        private void btnRemoveTable_Click(object sender, EventArgs e)
        {
            foreach (var item in lbxSelectedTables.SelectedItems)
            {
                lbxSelectedTables.Items.Remove(item);
            }
        }

        private void btnRemoveAllTables_Click(object sender, EventArgs e)
        {
            lbxSelectedTables.Items.Clear();
        }

        private void CopyDbWizard_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainWindow.Instance.RunInMainWindow(Dispose);
        }
    }

    [Description("s_database_writer_chooser_desc")]
    public class DatabaseWriterChooser : ICustomPropertyPage
    {
        IDatabaseSource m_selectedDatabase;

        public DatabaseWriterChooser()
        {
        }

        #region ICustomPropertyPage Members

        public System.Windows.Forms.Control CreatePropertyPage()
        {
            var editor = new DATreeView();
            editor.TreeStyle = TreeStyle.SelectOne;
            editor.TreeBehaviour.ShowFilter = node => node.IsDatabaseNodeOrParent();
            editor.RootPath = "data:";
            editor.ActiveNodeChange += new EventHandler(editor_ActiveNodeChange);
            return editor;
        }

        void editor_ActiveNodeChange(object sender, EventArgs e)
        {
            var editor = (DATreeView)sender;
            var node = editor.Selected as IDatabaseTreeNode;
            m_selectedDatabase = null;
            if (node != null) m_selectedDatabase = node.DatabaseConnection.CloneSource();
        }

        #endregion

        public IDatabaseWriter GetDatabaseWriter()
        {
            if (m_selectedDatabase == null) return null;
            return new DatabaseSourceWriter(m_selectedDatabase);
        }
    }
}
