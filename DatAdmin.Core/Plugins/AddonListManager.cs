using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;

namespace DatAdmin
{
    public abstract class AddonListManager
    {
        List<IAddonInstance> m_addons;

        public List<IAddonInstance> Addons
        {
            get
            {
                WantAddons();
                return m_addons;
            }
        }

        private void WantAddons()
        {
            if (m_addons != null) return;
            m_addons = new List<IAddonInstance>();
            string dir = AddonType.GetDirectory();
            try { Directory.CreateDirectory(dir); }
            catch { }
            foreach (string fn in AddonType.GetFiles())
            {
                var doc = new XmlDocument();
                doc.Load(fn);
                var addon = AddonType.LoadAddon(doc.DocumentElement);
                var fitem = addon as IFileBasedAddonInstance;
                if (fitem != null) fitem.AddonFileName = fn;
                m_addons.Add(addon);
            }
        }

        public void SaveAllItems()
        {
            foreach (var addon in Addons)
            {
                var fitem = addon as IFileBasedAddonInstance;
                if (fitem != null && fitem.AddonFileName != null && !fitem.IsInLibDirectory())
                {
                    addon.SaveToFile(fitem.AddonFileName);
                }
            }
        }

        public abstract AddonType AddonType { get; }
    }

    public abstract class AddonListManagerEditor : DirectoryAddonListEditorBase
    {
        AddonListManager m_adlist;
        public AddonListManagerEditor(AddonListManager adlist)
            : base(adlist.AddonType)
        {
            m_adlist = adlist;
        }

        public override IList ReloadItems(IList origItems)
        {
            return m_adlist.Addons;
        }

        public override object CreateNew()
        {
            string newname = InputBox.Run(Texts.Get("s_new_name"), "new");
            if (newname == null) return null;
            var obj = CreateNewAddon();
            if (obj == null) return null;
            var fitem = obj as IFileBasedAddonInstance;
            string dir = m_adtype.GetDirectory();
            string fn = Path.Combine(dir, newname + m_adtype.FileExtension);
            if (fitem != null) fitem.AddonFileName = fn;
            obj.SaveToFile(fn);
            return obj;
        }

        protected override string GetFileName(object item)
        {
            var fitem = item as IFileBasedAddonInstance;
            if (fitem == null) return null;
            return fitem.AddonFileName;
        }
    }
}
