using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DatAdmin
{
    public abstract class QuickExportBase : AddonBase, IQuickExport
    {
        public override AddonType AddonType
        {
            get { return QuickExportAddonType.Instance; }
        }

        #region IQuickExport Members

        public abstract ITabularDataStore GetDataStore();

        #endregion
    }

    [QuickExport(Name = "template", Title = "Template", SupportsDirectUse = false)]
    public class TemplateQuickExport : QuickExportBase, ICustomPropertyPage
    {
        ITabularDataStore m_dataStore = new GenericXmlDataStore();

        public ITabularDataStore DataStore
        {
            get { return m_dataStore; }
            set { m_dataStore = value; }
        }

        #region ICustomPropertyPage Members

        public Control CreatePropertyPage()
        {
            var res = new AddonSelectFrame();
            res.AddonTypeName = "datastore";
            res.CanSaveAsTemplate = false;
            res.SelectObject(DataStore);
            res.ChangedSelectedObject += new EventHandler(res_ChangedSelectedObject);
            return res;
        }

        void res_ChangedSelectedObject(object sender, EventArgs e)
        {
            DataStore = (ITabularDataStore)((AddonSelectFrame)sender).SelectedObject;
        }

        #endregion

        public override ITabularDataStore GetDataStore()
        {
            return DataStore;
        }

        public override void LoadFromXml(System.Xml.XmlElement xml)
        {
            base.LoadFromXml(xml);
            DataStore = (ITabularDataStore)TabularDataStoreAddonType.Instance.LoadAddon(xml.FindElement("DataStore"));
        }

        public override void SaveToXml(System.Xml.XmlElement xml)
        {
            base.SaveToXml(xml);
            DataStore.SaveToXml(xml.AddChild("DataStore"));
        }

        public static void RunManageDialog()
        {
            var editor = new QuickExportAddonListEditor();
            editor.ShowDialog("s_quick_export_manager");
            HQuickExport.CallChangedExports();
        }
    }

    public class QuickExportAddonListEditor : DirectoryAddonListEditor
    {
        public QuickExportAddonListEditor()
            : base(QuickExportAddonType.Instance)
        {
        }
        protected override IAddonInstance CreateNewAddon()
        {
            return new TemplateQuickExport();
        }
        public override void SaveItem(object item, object editem)
        {
            var qe = (IQuickExport)editem;
            string fn = Path.Combine(QuickExportAddonType.Instance.GetDirectory(), item.ToString() + ".adx");
            qe.SaveToFile(fn);
        }
    }
}
