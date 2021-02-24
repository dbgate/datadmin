using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using System.Drawing;

namespace Plugin.apps
{
    public class AppTreeTplNode : TreeNodeBase
    {
        public string m_title;
        public List<AppTreeTplNode> m_children = new List<AppTreeTplNode>();
        public List<AppPage> Pages = new List<AppPage>();
        public Bitmap m_icon;

        public AppTreeTplNode(ITreeNode parent)
            : base(parent, Guid.NewGuid().ToString())
        {
        }

        public AppTreeTplNode()
            : base("app")
        {
        }

        public override ITreeNode[] GetChildren()
        {
            return m_children.ToArray();
        }

        public override string Title
        {
            get { return m_title ?? "s_database"; }
        }

        [Browsable(false)]
        public override string TypeTitle
        {
            get { return m_title ?? "s_database"; }
        }

        public override bool AllowRename()
        {
            return m_title != null;
        }
        public override void RenameNode(string newname)
        {
            m_title = newname;
        }

        [PopupMenu("s_add_child", ImageName = CoreIcons.addName)]
        public void AddChild()
        {
            string name = InputBox.Run("s_name_of_new_child", "newnode");
            if (name != null)
            {
                var node = new AppTreeTplNode(this);
                node.m_title = name;
                m_children.Add(node);
                this.CompleteRefresh();
            }
        }

        public override bool AllowDelete()
        {
            return m_parent is AppTreeTplNode;
        }
        public override bool DoDelete()
        {
            if (MessageBox.Show(Texts.Get("s_really_drop$node", "node", Title), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var parent = m_parent as AppTreeTplNode;
                parent.m_children.Remove(this);
                Parent.CompleteRefresh();
                return true;
            }
            return false;
        }

        public void SaveToXml(XmlElement xml)
        {
            if (m_title != null) xml.SetAttribute("title", m_title);
            foreach (var child in m_children)
            {
                var x = xml.AddChild("Node");
                child.SaveToXml(x);
            }
            foreach (var page in Pages)
            {
                var x = xml.AddChild("Page");
                page.SaveToXml(x);
            }
            if (m_icon != null)
            {
                xml.AddChild("Icon").InnerText = IOTool.ImageToText(m_icon);
            }
        }
        public void LoadFromXml(XmlElement xml)
        {
            if (xml.HasAttribute("title")) m_title = xml.GetAttribute("title");
            foreach (XmlElement chx in xml.SelectNodes("Node"))
            {
                var node = new AppTreeTplNode(this);
                node.LoadFromXml(chx);
                m_children.Add(node);
            }
            foreach (XmlElement chx in xml.SelectNodes("Page"))
            {
                var pg = new AppPage();
                pg.LoadFromXml(chx);
                Pages.Add(pg);
            }
            if (xml.FindElement("Icon") != null)
            {
                m_icon = IOTool.ImageFromText(xml.FindElement("Icon").InnerText);
            }
        }

        public void GetInstanceNodes(ITreeNode parent, List<ITreeNode> children)
        {
            foreach (var child in m_children) children.Add(new AppInstanceTreeNode(parent, child));
        }

        public Bitmap Icon
        {
            get { return m_icon; }
            set { m_icon = value; }
        }

        public override Bitmap Image
        {
            get { return m_icon; }
        }
        public override Bitmap ExpandedImage
        {
            get { return m_icon; }
        }
    }

    public class AppInstanceTreeNode : TreeNodeBase
    {
        internal AppTreeTplNode m_tpl;

        public AppInstanceTreeNode(ITreeNode parent, AppTreeTplNode tpl)
            : base(parent, tpl.Title)
        {
            m_tpl = tpl;
        }

        public override string Title
        {
            get { return m_tpl.Title; }
        }

        public override string TypeTitle
        {
            get { return m_tpl.Title; }
        }

        public override Bitmap Image
        {
            get { return m_tpl.Image; }
        }

        public override Bitmap ExpandedImage
        {
            get { return m_tpl.Image; }
        }

        public override ITreeNode[] GetChildren()
        {
            var res = new List<ITreeNode>();
            foreach (var child in m_tpl.m_children)
            {
                res.Add(new AppInstanceTreeNode(this, child));
            }
            return res.ToArray();
        }
    }
}
