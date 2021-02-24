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
    public class ColumnStructure : TableObjectStructure, IColumnStructure
    {
        string m_columnName;
        DbTypeBase m_dataType;
        bool m_isNullable = true;
        SqlExpression m_defaultValue;
        string m_characterSet;
        string m_collation;
        //public TableStructure Table;
        //public NameWithSchema TableName;
        public NameWithSchema m_domain;

        public ColumnStructure(IColumnStructure src)
            : base(src)
        {
            m_columnName = src.ColumnName;
            m_dataType = src.DataType.Clone();
            m_isNullable = src.IsNullable;
            m_defaultValue = src.DefaultValue;
            m_characterSet = src.CharacterSet;
            m_collation = src.Collation;
            SpecificData.AddAll(src.SpecificData);
            //TableName = src.Table;
            //Table = src.Table;
            Domain = src.Domain;
        }

        //public ColumnStructure(TableStructure table, IColumnStructure src)
        //    : this(src)
        //{
        //    Table = table;
        //}

        //public ColumnStructure(TableStructure table)
        //{
        //    Table = table;
        //}

        public ColumnStructure() { }

        public void SetSpecificAttribute(string dialect, string name, string value)
        {
            string key = dialect + "." + name;
            SpecificData[key] = value;
        }

        [ShowInGrid]
        [DisplayName("s_name")]
        public string NameForGrid
        {
            get { return ColumnName; }
            set
            {
                if (Table != null)
                {
                    var db = Table.Database as DatabaseStructure;
                    if (db != null) db.RenameColumn(this, value);
                }
            }
        }

        #region IColumnStructure Members

        [XmlAttrib("name")]
        public string ColumnName
        {
            get { return m_columnName; }
            set { m_columnName = value; }
        }

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
        
        public NameWithSchema Domain
        {
            get { return m_domain; }
            set { m_domain = value; }
        }

        public DbTypeBase DataType
        {
            get { return m_dataType; }
            set { m_dataType = value; }
        }

        public SqlExpression DefaultValue
        {
            get { return m_defaultValue; }
            set { m_defaultValue = value; }
        }

        public int ColumnOrder
        {
            get
            {
                if (Table == null) return -1;
                return ((ColumnCollection)Table.Columns).IndexOfEx(this);
            }
        }

        [DisplayName("s_data_type")]
        [ShowInGrid]
        public string DataTypeName
        {
            get
            {
                return DataType.ToString();
            }
        }

        [XmlAttrib("nullable")]
        [DisplayName("s_nullable")]
        [ShowInGrid]
        public bool IsNullable
        {
            get { return m_isNullable; }
            set { m_isNullable = value; }
        }

        #endregion

        [DisplayName("s_references")]
        [ShowInGrid]
        public string ReferencesTo
        {
            get
            {
                var tbl = Parent as ITableStructure;
                if (tbl == null) return null;
                var fk = tbl.GetKeyWithColumn<IForeignKey>(this);
                if (fk != null && fk.Columns.Count == 1)
                {
                    return fk.PrimaryKeyTable.ToString();
                }
                return null;
            }
            set
            {
                var tbl = Parent as TableStructure;
                if (tbl == null) return;
                var db = tbl.Parent as DatabaseStructure;
                if (db == null) return;
                var tpk = db.FindTable(new NameWithSchema(value));
                if (tpk == null) return;
                var pk = tpk.FindConstraint<IPrimaryKey>();
                if (pk == null) return;
                if (pk.Columns.Count != 1) return;
                var oldfk = tbl.GetKeyWithColumn<ForeignKey>(this);
                if (oldfk != null) tbl._Constraints.Remove(oldfk);
                var newfk = new ForeignKey();
                newfk.Columns.Add(new ColumnReference(this.ColumnName));
                newfk.PrimaryKeyTable = tpk.FullName;
                newfk.PrimaryKeyColumns.Add(pk.Columns[0]);
                newfk.Name = DbObjectNameTool.FkName(tbl.FullName, newfk.Columns);
                newfk.OnDeleteAction = oldfk.OnDeleteAction;
                newfk.OnUpdateAction = oldfk.OnUpdateAction;
                tbl._Constraints.Add(newfk);
            }
        }

        public void Save(XmlElement xml)
        {
            SaveBase(xml);
            this.SavePropertiesCore(xml);
            SaveInternal(xml);
        }
        public ColumnStructure(XmlElement xml)
            : base(xml)
        {
            this.LoadPropertiesCore(xml);
            LoadInternal(xml);
        }

        private void SaveInternal(XmlElement xml)
        {
            m_dataType.SaveToXml(xml);
            XmlTool.SaveSpecificAttributes(SpecificData, "colspec.", xml);
            if (Domain != null) Domain.SaveToXml(xml, "domain.");
            if (m_defaultValue != null) m_defaultValue.SaveToXml(xml.AddChild("Default"));
        }

        private void LoadInternal(XmlElement xml)
        {
            m_dataType = DbTypeBase.Load(xml);
            XmlTool.LoadSpecificAttributes(SpecificData, "colspec.", xml);
            var defval = xml.FindElement("Default");
            if (defval != null) m_defaultValue = SqlExpression.Load(defval);
            Domain = NameWithSchema.LoadFromXml(xml, "domain.");
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            LoadInternal(xml);
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            SaveInternal(xml);
        }

        public override string ToString()
        {
            if (Table != null) return Table.FullName.ToString() + "." + ColumnName;
            return ColumnName;
        }

        public override void NotifyRenameColumn(NameWithSchema table, string oldcol, string newcol)
        {
            if (Table.FullName == table && ColumnName == oldcol) ColumnName = newcol;
        }

        public override void RenameObject(string newname)
        {
            ColumnName = newname;
        }

        public override void NotifyRenameDomain(NameWithSchema oldName, NameWithSchema newName)
        {
            if (Domain == oldName) Domain = newName;
        }

        public override void SetDummyTable(NameWithSchema name)
        {
            TableStructure table = new TableStructure();
            table.FullName = name;
            table._Columns.Add(this);
        }

        public override void AssignFrom(IAbstractObjectStructure obj)
        {
            base.AssignFrom(obj);
            var col = obj as IColumnStructure;
            Domain = col.Domain;
            DefaultValue = col.DefaultValue;
            DataType = col.DataType;
        }

        public override FullDatabaseRelatedName GetName()
        {
            return new FullDatabaseRelatedName
            {
                ObjectName = Table != null ? Table.FullName : null,
                ObjectType = "column",
                SubName = ColumnName
            };
        }
    }
}
