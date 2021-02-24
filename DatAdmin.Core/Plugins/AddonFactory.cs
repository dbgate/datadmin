using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace DatAdmin
{
    public class AddonCreateFactoryItem : CreateFactoryItemBase
    {
        AddonHolder  m_item;
        string m_group;
        string m_grpname;
        Bitmap m_bitmap;

        public AddonCreateFactoryItem(AddonHolder item, string group, string grpname, Bitmap bitmap)
        {
            m_item = item;
            m_group = group;
            m_grpname = grpname;
            m_bitmap = bitmap;
        }

        public override string Title
        {
            get { return m_item.Title; }
        }

        public override string Name
        {
            get { return m_item.Name; }
        }

        public override string Group
        {
            get { return m_group; }
        }

        public override string GroupName
        {
            get { return m_grpname; }
        }

        public override Bitmap Bitmap
        {
            get { return m_bitmap; }
        }

        public override bool Create(ITreeNode parent, string name)
        {
            string path = parent.FileSystemPath;
            object addon = m_item.CreateInstance();
            ((IAddonInstance)addon).SaveToFile(Path.Combine(path, name + ".adx"));
            return true;
        }
    }

    public abstract class AddonCreateFactory : CreateFactoryBase
    {
        string m_group;
        string m_grpname;
        Bitmap m_bitmap;
        AddonType m_adtype;

        public AddonCreateFactory(AddonType adtype, string group, string grpname, Bitmap bitmap)
        {
            m_group = group;
            m_grpname = grpname;
            m_bitmap = bitmap;
            m_adtype = adtype;
        }

        public override ICreateFactoryItem[] GetItems(ITreeNode parent)
        {
            List<ICreateFactoryItem> res = new List<ICreateFactoryItem>();
            foreach (var item in m_adtype.StaticSpace.GetFilteredAddons(RegisterItemUsage.CreateTemplate))
            {
                res.Add(new AddonCreateFactoryItem(item, m_group, m_grpname, m_bitmap));
            }
            return res.ToArray();
        }
    }
}
