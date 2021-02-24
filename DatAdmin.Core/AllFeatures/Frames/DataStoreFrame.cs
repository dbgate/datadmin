using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DatAdmin
{
    public partial class DataStoreFrame : UserControl
    {
        //ITabularDataStore m_store;
        TabularDataStoreMode m_dataStoreMode;
        DataStoreChooser m_chooser;
        NewTableChooser m_newTableChooser;
        bool m_fixedMode;
        //ITableStructure m_sourceStructure;

        static Dictionary<TabularDataStoreMode, string> m_lastSelectedAddons = new Dictionary<TabularDataStoreMode, string>();

        public event EventHandler ChangedDataStore;

        public TabularDataStoreMode DataStoreMode
        {
            get { return m_dataStoreMode; }
            set
            {
                m_dataStoreMode = value;
                if (m_lastSelectedAddons.ContainsKey(value)) addonSelectFrame1.DefaultAddonHolder = m_lastSelectedAddons[value];
                UpdateMode();
                if (!DesignMode)
                {
                    addonSelectFrame1.Reload(false);
                }
                //if (m_store != null) m_store.Mode = value;
                //selectTemplateCombo1.AllowSpecItems = value != TabularDataStoreMode.WriteStream;
                //selectTemplateCombo1.ClearCache();
            }
        }

        [Browsable(false)]
        public ITabularDataStore DataStore
        {
            get
            {
                if (FixedMode)
                {
                    return (ITabularDataStore)propertyFrame1.SelectedObject;
                }
                if (addonSelectFrame1.SelectedObject is ITabularDataStore)
                {
                    return (ITabularDataStore)addonSelectFrame1.SelectedObject;
                }
                if (addonSelectFrame1.SelectedObject is DataStoreChooser)
                {
                    return ((DataStoreChooser)addonSelectFrame1.SelectedObject).GetDataStore();
                }
                if (addonSelectFrame1.SelectedObject is NewTableChooser)
                {
                    return ((NewTableChooser)addonSelectFrame1.SelectedObject).GetDataStore();
                }
                return null;
            }
        }

        public void SetDataStore(ITabularDataStore value)
        {
            if (FixedMode) propertyFrame1.SelectedObject = value;
            else addonSelectFrame1.SelectObject(value);
            UpdateMode();
        }

        private void UpdateMode()
        {
            var ds = addonSelectFrame1.SelectedObject as ITabularDataStore;
            if (ds != null && m_dataStoreMode != TabularDataStoreMode.Unknown)
            {
                ds.Mode = m_dataStoreMode;
            }
        }

        public DataStoreFrame()
        {
            InitializeComponent();
            // filter should be set before Register

            //if (m_store != null)
            //{
            //    selectTemplateCombo1.Enabled = false;
            //}
            //else
            //{
            //    try
            //    {
            //        selectTemplateCombo1.Configure(TabularDataStoreAddonType.Instance.CommonSpace, SupportsMode, "csv", new List<string> { "s_select_in_tree" });
            //        selectTemplateCombo1.Enabled = true;
            //        RecreateStore();
            //    }
            //    catch { }
            //}
            Disposed += new EventHandler(DataStoreFrame_Disposed);
        }

        void DataStoreFrame_Disposed(object sender, EventArgs e)
        {
            var ds = DataStore;
            if (ds != null)
            {
                m_lastSelectedAddons[ds.Mode] = AddonTool.ExtractAddonName(ds);
            }
            //if (m_chooser != null) m_chooser.Dispose();
        }

        //private void RecreateStore()
        //{
        //    //string specitem = selectTemplateCombo1.SpecialSelectedItem;
        //    //if (specitem == "s_select_in_tree")
        //    //{
        //    //    if (m_chooser == null) m_chooser = new DataStoreChooser();
        //    //    propertyFrame1.SelectedObject = m_chooser;
        //    //    m_store = null;
        //    //}
        //    //else
        //    //{
        //    //    AddonHolder item = selectTemplateCombo1.SelectedAddonHolder;
        //    //    if (item != null)
        //    //    {
        //    //        m_store = (ITabularDataStore)item.CreateInstance();
        //    //        m_store.Mode = DataStoreMode;
        //    //        propertyFrame1.SelectedObject = m_store;
        //    //    }
        //    //}
        //    //if (ChangedDataStore != null) ChangedDataStore(this, EventArgs.Empty);
        //}

        //private void selectTemplateComboFrame1_ChangedSelectedItem(object sender, EventArgs e)
        //{
        //    RecreateStore();
        //}

        //private void btsaveastemplate_Click(object sender, EventArgs e)
        //{
        //    //if (TabularDataStoreAddonType.Instance.SaveCommonTemplate((IAddonInstance)propertyFrame1.SelectedObject, selectTemplateCombo1.SelectedTemplateFileName))
        //    //{
        //    //    selectTemplateCombo1.ClearCache();
        //    //}
        //}

        private void addonSelectFrame1_GetSpecialItems(object sender, GetSpecialItemsEventArgs e)
        {
            e.SpecialItems.Add("s_existing_table");
            if (DataStoreMode == TabularDataStoreMode.Write || DataStoreMode == TabularDataStoreMode.WriteStream)
            {
                e.SpecialItems.Add("s_new_table");
            }
        }

        private void addonSelectFrame1_CreateSpecialItem(object sender, CreateSpecialItemEventArgs e)
        {
            if (e.SpecialItem == "s_existing_table")
            {
                m_chooser = new DataStoreChooser();
                e.Instance = m_chooser;
            }
            if (e.SpecialItem == "s_new_table")
            {
                m_newTableChooser = new NewTableChooser();
                e.Instance = m_newTableChooser;
            }
        }

        private void addonSelectFrame1_FilterAddon(object sender, FilterAddonEventArgs e)
        {
            if (e.InstanceModel is ITabularDataStore)
            {
                var ds = (ITabularDataStore)e.InstanceModel;
                if (!ds.SupportsMode(m_dataStoreMode)) e.Skip = true;
            }
        }

        public bool CompactDesign
        {
            get { return addonSelectFrame1.CompactDesign; }
            set { addonSelectFrame1.CompactDesign = value; }
        }

        private void addonSelectFrame1_ChangedSelectedObject(object sender, EventArgs e)
        {
            if (ChangedDataStore != null) ChangedDataStore(sender, e);
            UpdateMode();
        }

        public bool FixedMode
        {
            get { return m_fixedMode; }
            set
            {
                if (m_fixedMode != value)
                {
                    m_fixedMode = value;
                    if (value)
                    {
                        propertyFrame1.BringToFront();
                        propertyFrame1.SelectedObject = addonSelectFrame1.SelectedObject;
                    }
                    else
                    {
                        propertyFrame1.SendToBack();
                    }
                }
            }
        }

        //public void SetSourceDataStore(ITabularDataStore source)
        //{
        //    if (source != null)
        //    {
        //        Async.SafeOpen(source.Connection);
        //        try
        //        {
        //            IAsyncResult res1 = source.BeginGetRowFormat(null);
        //            Async.WaitFor(res1);
        //            m_sourceStructure = source.EndGetRowFormat(res1);
        //        }
        //        catch (Exception err)
        //        {
        //            throw new BulkCopyInputError("DAE-00163", err);
        //        }
        //    }
        //    else
        //    {
        //        m_sourceStructure = null;
        //    }
        //    if (m_newTableChooser != null) m_newTableChooser.SetStructure(m_sourceStructure);
        //}
    }

    [Description("s_data_store_chooser_desc")]
    public class DataStoreChooser : ICustomPropertyPage
    {
        ITableSource m_selectedTable;

        public DataStoreChooser()
        {
        }

        #region ICustomPropertyPage Members

        public System.Windows.Forms.Control CreatePropertyPage()
        {
            var editor = new DATreeView();
            editor.TreeStyle = TreeStyle.SelectOne;
            editor.DialogLabel = "s_select_table";
            editor.TreeBehaviour.ShowFilter = node => node.IsTableNodeOrParent(true);
            editor.RootPath = "data:";
            editor.ActiveNodeChange += new EventHandler(editor_ActiveNodeChange);
            return editor;
        }

        void editor_ActiveNodeChange(object sender, EventArgs e)
        {
            var editor = (DATreeView)sender;
            var node = editor.Selected as Table_SourceTreeNode;
            m_selectedTable = null;
            if (node != null) m_selectedTable = node.TableSource;
        }

        #endregion

        public ITabularDataStore GetDataStore()
        {
            if (m_selectedTable == null) return null;
            return m_selectedTable.GetDataStoreAndClone();
        }
    }

    [Description("s_new_table_data_store_chooser_desc")]
    public class NewTableChooser : ICustomPropertyPage
    {
        NameWithSchema m_name;
        IDatabaseSource m_db;

        #region ICustomPropertyPage Members

        public Control CreatePropertyPage()
        {
            var editor = new NewTableChooserFrame();
            editor.ChangedProperties += new EventHandler(editor_ChangedProperties);
            return editor;
        }

        void editor_ChangedProperties(object sender, EventArgs e)
        {
            var editor = (NewTableChooserFrame)sender;
            m_name = editor.FullName;
            m_db = editor.Database;
        }

        #endregion

        public ITabularDataStore GetDataStore()
        {
            var res = new GenericTabularDataStore(m_db.Connection.Clone(), m_db.DatabaseName, m_name.Schema, m_name.Name);
            res.create_table = true;
            return res;
        }
    }
}
