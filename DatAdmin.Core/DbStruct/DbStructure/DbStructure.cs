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
    public class DomainStructure : DatabaseObjectStructure, IDomainStructure, IComparable
    {
        string m_characterSet;
        string m_collation;

        public void Save(XmlElement xml)
        {
            SaveBase(xml);
            //this.SaveProperties(xml);
            XmlTool.SaveParameters(xml, SpecificData);
            if (FullName != null) FullName.SaveToXml(xml);
            //XmlTool.SaveNameWithSchema(xml, FullName);
            DataType.SaveToXml(xml);
            if (DefaultValue != null) DefaultValue.SaveToXml(xml.AddChild("Default"));
        }

        public DomainStructure(IDomainStructure src)
            : base(src)
        {
            FullName = src.FullName;
            DataType = src.DataType;
            IsNullable = src.IsNullable;
            DefaultValue = src.DefaultValue;
            CheckExpr = src.CheckExpr;
            Collation = src.Collation;
            CharacterSet = src.CharacterSet;
            SpecificData.AddAll(src.SpecificData);
        }

        public DomainStructure(XmlElement xml)
            : base(xml)
        {
            //this.LoadProperties(xml);
            SpecificData = XmlTool.LoadParameters(xml);
            FullName = NameWithSchema.LoadFromXml(xml);
            DataType = DbTypeBase.Load(xml);
            var defval = xml.FindElement("Default");
            if (defval != null) DefaultValue = SqlExpression.Load(defval);
        }

        public DomainStructure() { }

        public override void AssignFrom(IAbstractObjectStructure source)
        {
            base.AssignFrom(source);
            var dom = source as IDomainStructure;
            DataType = dom.DataType;
        }

        #region IDomainStructure Members

        public NameWithSchema FullName { get; set; }
        public DbTypeBase DataType { get; set; }
        [XmlAttrib("nullable")]
        public bool IsNullable { get; set; }
        public SqlExpression DefaultValue { get; set; }
        [XmlAttrib("checkexpr")]
        public string CheckExpr { get; set; }

        [XmlAttrib("collation")]
        public string Collation
        {
            get { return m_collation; }
            set { m_collation = String.IsNullOrEmpty(value) ? null : value; }
        }
        [XmlAttrib("charset")]
        public string CharacterSet
        {
            get { return m_characterSet; }
            set { m_characterSet = String.IsNullOrEmpty(value) ? null : value; }
        }

        #endregion

        #region IComparable Members

        public int CompareTo(object obj)
        {
            var other = obj as IDomainStructure;
            if (other != null)
            {
                return String.Compare(FullName.ToString(), other.FullName.ToString(), true);
            }
            return 0;
        }

        #endregion

        public override string ToString()
        {
            return FullName.ToString();
        }

        public override void NotifyRenameDomain(NameWithSchema oldName, NameWithSchema newName)
        {
            if (FullName == oldName) FullName = newName;
        }

        public override void RenameObject(string newname)
        {
            FullName = new NameWithSchema(FullName.Schema, newname);
        }

        public override void SetObjectSchema(string newschema)
        {
            FullName = new NameWithSchema(newschema, FullName.Name);
        }

        public override FullDatabaseRelatedName GetName()
        {
            return new FullDatabaseRelatedName
            {
                ObjectName = FullName,
                ObjectType = "domain",
            };
        }
    }

    public class SchemaStructure : DatabaseObjectStructure, ISchemaStructure, IComparable
    {
        public void Save(XmlElement xml)
        {
            SaveBase(xml);
            //this.SaveProperties(xml);
            XmlTool.SaveParameters(xml, SpecificData);
        }

        public SchemaStructure(XmlElement xml)
            : base(xml)
        {
            //this.LoadProperties(xml);
            SpecificData = XmlTool.LoadParameters(xml);
        }

        public SchemaStructure(ISchemaStructure schema)
            : base(schema)
        {
            SpecificData.AddAll(schema.SpecificData);
            SchemaName = schema.SchemaName;
        }

        public SchemaStructure() { }

        public override void RenameObject(string newname)
        {
            SchemaName = newname;
        }

        #region ISchemaStructure Members

        [XmlAttrib("name")]
        public string SchemaName { get; set; }

        #endregion

        public override string ToString()
        {
            return SchemaName;
        }

        public override FullDatabaseRelatedName GetName()
        {
            return new FullDatabaseRelatedName
            {
                ObjectName = new NameWithSchema(SchemaName),
                ObjectType = "schema",
            };
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            return String.Compare(ToString(), obj.SafeToString(), true);
        }

        #endregion
    }

    public class CharacterSetStructure : DatabaseObjectStructure, ICharacterSetStructure
    {
        public void Save(XmlElement xml)
        {
            SaveBase(xml);
            this.SavePropertiesCore(xml);
            XmlTool.SaveParameters(xml, SpecificData);
        }

        public CharacterSetStructure() { }

        public CharacterSetStructure(XmlElement xml)
            : base(xml)
        {
            this.LoadPropertiesCore(xml);
            SpecificData = XmlTool.LoadParameters(xml);
        }

        public CharacterSetStructure(ICharacterSetStructure src)
            : base(src)
        {
            SpecificData.AddAll(src.SpecificData);
            Name = src.Name;
            DefaultCollation = src.DefaultCollation;
        }

        public override FullDatabaseRelatedName GetName()
        {
            return new FullDatabaseRelatedName
            {
                ObjectName = new NameWithSchema(Name),
                ObjectType = "character_set",
            };
        }

        #region ICharacterSetStructure Members

        [XmlAttrib("name")]
        public string Name { get; set; }

        [XmlAttrib("defcollate")]
        public string DefaultCollation { get; set; }

        #endregion
    }

    public class CollationStructure : DatabaseObjectStructure, ICollationStructure
    {
        public void Save(XmlElement xml)
        {
            SaveBase(xml);
            this.SavePropertiesCore(xml);
            XmlTool.SaveParameters(xml, SpecificData);
        }

        public CollationStructure() { }

        public CollationStructure(XmlElement xml)
            : base(xml)
        {
            this.LoadPropertiesCore(xml);
            SpecificData = XmlTool.LoadParameters(xml);
        }

        public CollationStructure(ICollationStructure src)
        {
            SpecificData.AddAll(src.SpecificData);
            Name = src.Name;
            CharacterSet = src.CharacterSet;
        }

        public override FullDatabaseRelatedName GetName()
        {
            return new FullDatabaseRelatedName
            {
                ObjectName = new NameWithSchema(Name),
                ObjectType = "collation",
            };
        }

        #region ISchemaStructure Members

        [XmlAttrib("name")]
        public string Name { get; set; }

        [XmlAttrib("charset")]
        public string CharacterSet { get; set; }

        #endregion
    }

    public class SpecificObjectStructure : DatabaseObjectStructure, ISpecificObjectStructure
    {
        public List<DependencyItem> DependsOn { get; set; }

        public void Save(XmlElement xml)
        {
            SaveBase(xml);
            this.SavePropertiesCore(xml);
            XmlTool.SaveParameters(xml, SpecificData);
            if (ObjectName != null) ObjectName.SaveToXml(xml);
            if (RelatedTable != null) RelatedTable.SaveToXml(xml, "relschema", "reltable");
            //XmlTool.SaveNameWithSchema(xml, RelatedTable, "relschema", "reltable");
            if (DependsOn != null)
            {
                XmlElement deps = xml.AddChild("DependsOn");
                foreach (var item in DependsOn)
                {
                    var repr = SpecificRepresentationAddonType.Instance.FindRepresentation(item.ObjectType);
                    XmlElement it = deps.AddChild(repr.XmlElementName);
                    if (item.Name != null) item.Name.SaveToXml(it);
                }
            }
        }

        public SpecificObjectStructure() { }

        public SpecificObjectStructure(ISpecificObjectStructure src)
            : base(src)
        {
            CreateSql = src.CreateSql;
            ObjectType = src.ObjectType;
            SpecificDialect = src.SpecificDialect;
            ObjectName = src.ObjectName;
            RelatedTable = src.RelatedTable;
            SpecificData.AddAll(src.SpecificData);
            if (src.DependsOn != null)
            {
                DependsOn = new List<DependencyItem>();
                foreach (var p in src.DependsOn)
                {
                    DependsOn.Add(new DependencyItem(p));
                }
            }
        }

        public SpecificObjectStructure(XmlElement xml, string objtype)
            : base(xml)
        {
            this.LoadPropertiesCore(xml);
            ObjectType = objtype;
            SpecificData = XmlTool.LoadParameters(xml);
            ObjectName = NameWithSchema.LoadFromXml(xml);
            RelatedTable = NameWithSchema.LoadFromXml(xml, "relschema", "reltable");
            var deps = xml.FindElement("DependsOn");
            if (deps != null)
            {
                DependsOn = new List<DependencyItem>();
                foreach (XmlElement child in deps)
                {
                    var repr = SpecificRepresentationAddonType.Instance.FindByElement(child.Name);
                    DependsOn.Add(new DependencyItem { Name = NameWithSchema.LoadFromXml(child), ObjectType = repr.ObjectType });
                }
            }
        }

        public override void AssignFrom(IAbstractObjectStructure source)
        {
            base.AssignFrom(source);
            var obj = source as ISpecificObjectStructure;
            ObjectName = obj.ObjectName;
            ObjectType = obj.ObjectType;
            // CreateSql and SpecificDialect are merged using XmlTool.CopyProperties
        }

        public override string ToString()
        {
            return ObjectName.ToString();
        }

        public override void RenameObject(string newname)
        {
            ObjectName = new NameWithSchema(ObjectName.Schema, newname);
        }

        public override void SetObjectSchema(string newschema)
        {
            ObjectName = new NameWithSchema(newschema, ObjectName.Name);
        }

        protected override string GetTypeCaption()
        {
            return ObjectType.Capitalize();
        }

        [ShowInGrid]
        [DisplayName("s_name")]
        public string Name
        {
            get { return ObjectName.Name; }
            set { ObjectName = new NameWithSchema(ObjectName.Schema, value); }
        }

        [ShowInGrid]
        [DisplayName("s_schema")]
        public string SchemaName
        {
            get { return ObjectName.Schema; }
            set { ObjectName = new NameWithSchema(value, ObjectName.Name); }
        }

        public override FullDatabaseRelatedName GetName()
        {
            return new FullDatabaseRelatedName
            {
                ObjectName = ObjectName,
                ObjectType = ObjectType,
            };
        }

        #region ISpecificDbObjectStructure Members

        [XmlElem("DDL")]
        public string CreateSql { get; set; }
        public NameWithSchema ObjectName { get; set; }
        public NameWithSchema RelatedTable { get; set; }
        [XmlAttrib("dialect")]
        public string SpecificDialect { get; set; }
        public string ObjectType { get; set; }

        #endregion
    }
}
