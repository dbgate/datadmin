using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace DatAdmin
{
    public abstract class AbstractObjectStructure : IAbstractObjectStructure, IExplicitXmlPersistent
    {
        //bool m_isFreezed;
        //AbstractObjectStructure m_freezedCopy;

        //protected void BeforeEdit()
        //{
        //    if (m_isFreezed) throw new InternalError("DAE-00034 Cannot modify freezed object:" + ToString());
        //    m_freezedCopy = null;
        //}

        protected AbstractObjectStructure()
        {
            m_groupId = Guid.NewGuid().ToString();
        }
        protected AbstractObjectStructure(XmlElement xml)
        {
            LoadBase(xml);
        }
        protected void LoadBase(XmlElement xml)
        {
            if (xml.HasAttribute("groupid")) m_groupId = xml.GetAttribute("groupid");
            else m_groupId = Guid.NewGuid().ToString();
            this.LoadPropertiesCore(xml);
        }
        protected AbstractObjectStructure(IAbstractObjectStructure src)
        {
            m_groupId = src.GroupId;
        }

        string m_groupId;
        Dictionary<string, string> m_specificData = new Dictionary<string, string>();
        public string Comment { get; set; }

        public Dictionary<string, string> SpecificData
        {
            get { return m_specificData; }
            set { m_specificData = value; }
        }

        public string GroupId
        {
            get { return m_groupId; }
            set
            {
                Errors.Assert(value != null, "DAE-00242 AbstractObjectStructure.SetGroupId: GroupId cannot be null");
                m_groupId = value;
            }
        }

        protected void SaveBase(XmlElement xml)
        {
            xml.SetAttribute("groupid", m_groupId);
            this.SavePropertiesCore(xml);
        }

        public virtual void AddAllObjects(List<IAbstractObjectStructure> res)
        {
            res.Add(this);
        }

        public virtual void NotifyRenameTable(NameWithSchema oldName, NameWithSchema newName) { }
        public virtual void NotifyDropColumn(IColumnStructure column) { }
        public virtual void NotifyRenameColumn(NameWithSchema table, string oldcol, string newcol) { }
        public virtual void NotifyRenameDomain(NameWithSchema oldName, NameWithSchema newName) { }

        public virtual void RenameObject(string newname) { }
        public virtual void SetObjectSchema(string newschema) { }
        public abstract FullDatabaseRelatedName GetName();

        //public virtual void NotifyChangeColumnType_RefsOnly(IColumnStructure newcol) { }

        //public bool IsFreezed { get { return m_isFreezed; } }
        //public IAbstractObjectStructure FreezeUntyped()
        //{
        //    if (IsFreezed) return this;
        //    if (m_freezedCopy != null) return m_freezedCopy;
        //    m_freezedCopy = (AbstractObjectStructure)CloneUntyped();
        //    m_freezedCopy.m_isFreezed = true;
        //    return m_freezedCopy;
        //}
        //public abstract IAbstractObjectStructure CloneUntyped();

        protected void CopyFromObject(IAbstractObjectStructure source)
        {
            // groupid is NOT merged
            m_specificData.Clear();
            m_specificData.AddAll(source.SpecificData);
            Comment = source.Comment;
            XmlTool.CopyProperties(source, this);
        }

        public virtual void MergeFrom(IAbstractObjectStructure source)
        {
            AssignFrom(source);
        }
        public virtual void AssignFrom(IAbstractObjectStructure source)
        {
            CopyFromObject(source);
        }

        [DisplayName("s_type")]
        [ShowInGrid(Order = -100)]
        [Browsable(false)]
        public string TypeCaption { get { return GetTypeCaption(); } }

        protected virtual string GetTypeCaption()
        {
            var res = GetType().Name;
            if (res.EndsWith("Structure")) res = res.Substring(0, res.Length - "Structure".Length);
            return res;
        }

        #region IExplicitXmlPersistent Members

        public virtual void SaveToXml(XmlElement xml)
        {
            SaveBase(xml);
        }

        public virtual void LoadFromXml(XmlElement xml)
        {
            LoadBase(xml);
        }

        #endregion
    }

    public abstract class AttachableObjectStructure : AbstractObjectStructure, IAttachableObjectStructure
    {
        public IAbstractObjectStructure Parent
        {
            get;
            internal set;
        }
        protected AttachableObjectStructure() { }
        protected AttachableObjectStructure(XmlElement xml) : base(xml) { }
        protected AttachableObjectStructure(IAttachableObjectStructure src) : base(src) { }
    }

    public abstract class TableObjectStructure : AttachableObjectStructure, ITableObjectStructure
    {
        public ITableStructure Table
        {
            get { return (ITableStructure)Parent; }
        }
        protected TableObjectStructure() { }
        protected TableObjectStructure(XmlElement xml) : base(xml) { }
        protected TableObjectStructure(IAttachableObjectStructure src) : base(src) { }
        public abstract void SetDummyTable(NameWithSchema name);

        [Browsable(false)]
        [DisplayName("s_table")]
        [ShowInGrid]
        public NameWithSchema TableName
        {
            get
            {
                if (Table != null) return Table.FullName;
                return null;
            }
        }
    }

    public abstract class DatabaseObjectStructure : AttachableObjectStructure, IDatabaseObjectStructure
    {
        public IDatabaseStructure Database
        {
            get { return (IDatabaseStructure)Parent; }
        }
        protected DatabaseObjectStructure() { }
        protected DatabaseObjectStructure(XmlElement xml) : base(xml) { }
        protected DatabaseObjectStructure(IAttachableObjectStructure src) : base(src) { }
    }
}
