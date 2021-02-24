using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DatAdmin
{
    //public enum DbCopyMode { Structure, StructureAndData }
    // default schema can be reached, if ExplicitSchema is null
    public enum DbCopyDataMode { None, All, Selected, Unselected }
    public enum DbCopySchemaMode { Original, Explicit }

    public class DatabaseCopyOptions
    {
        public DatabaseStructureMembers CopyMembers;
        public TableCopyOptions TableOptions = new TableCopyOptions();

        //[XmlElem]
        //public DbCopyMode Mode { get; set; }

        [XmlElem]
        public bool CopyStructure { get; set; }

        [XmlElem]
        public DbCopyDataMode DataMode { get; set; }

        [XmlElem]
        public DbCopySchemaMode SchemaMode { get; set; }

        [XmlElem]
        public string ExplicitSchema { get; set; }

        public List<NameWithSchema> DataCopyTables { get; set; }

        public IMigrationProfile MigrationProfile { get; set; }

        public DatabaseCopyOptions()
        {
            DataMode = DbCopyDataMode.All;
            SchemaMode = DbCopySchemaMode.Explicit;
            CopyStructure = true;
            ExplicitSchema = null;
            DataCopyTables = new List<NameWithSchema>();
        }

        public bool CopyTableStructure(NameWithSchema name)
        {
            return CopyMembers.HasTableDetails(name);
        }

        public bool CopyTableData(NameWithSchema name)
        {
            switch (DataMode)
            {
                case DbCopyDataMode.All:
                    return true;
                case DbCopyDataMode.None:
                    return false;
                case DbCopyDataMode.Selected:
                    return DataCopyTables.Contains(name);
                case DbCopyDataMode.Unselected:
                    return !DataCopyTables.Contains(name);
            }
            return false;
        }

        //public NameWithSchema GetMappedTableName(NameWithSchema name)
        //{
        //    switch (SchemaMode)
        //    {
        //        case DbCopySchemaMode.Explicit:
        //            return new NameWithSchema(ExplicitSchema, name.Name);
        //        case DbCopySchemaMode.Original:
        //            return name;
        //    }
        //    return name;
        //}

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (CopyStructure && CopyMembers != null)
            {
                sb.AppendFormat("{0}:\n", Texts.Get("s_structure"));
                foreach (string line in CopyMembers.ToString().Split('\n'))
                {
                    sb.AppendFormat("  {0}:\n", line.TrimEnd());
                }
            }
            sb.AppendFormat("{0}:\n", Texts.Get("s_table_data"));
            switch (DataMode)
            {
                case DbCopyDataMode.All:
                    sb.AppendFormat("  {0}\n", Texts.Get("s_all"));
                    break;
                case DbCopyDataMode.None:
                    sb.AppendFormat("  {0}\n", Texts.Get("s_none"));
                    break;
                case DbCopyDataMode.Selected:
                    foreach (var tbl in DataCopyTables) sb.AppendFormat("  {0}\n", tbl);
                    break;
                case DbCopyDataMode.Unselected:
                    sb.AppendFormat(" ({0})\n", Texts.Get("s_all_except_selected"));
                    foreach (var tbl in DataCopyTables) sb.AppendFormat("  {0}\n", tbl);
                    break;
            }
            return sb.ToString();
        }

        public void SaveToXml(XmlElement xml)
        {
            CopyMembers.SaveToXml_ForJob(xml.AddChild("Members"));
            TableOptions.SaveToXml(xml.AddChild("TableOptions"));
            if (DataCopyTables.Count > 0)
            {
                var t = xml.AddChild("DataCopyTables");
                foreach (var tbl in DataCopyTables)
                {
                    tbl.SaveToXml(t.AddChild("Table"));
                }
            }
            this.SavePropertiesCore(xml);
        }

        public void LoadFromXml(XmlElement xml)
        {
            CopyMembers = new DatabaseStructureMembers();
            CopyMembers.LoadFromXml_ForJob(xml.FindElement("Members"));
            TableOptions = new TableCopyOptions();
            TableOptions.LoadFromXml(xml.FindElement("TableOptions"));
            foreach (XmlElement tbl in xml.SelectNodes("DataCopyTables/Table"))
            {
                DataCopyTables.Add(NameWithSchema.LoadFromXml(tbl));
            }
            this.LoadPropertiesCore(xml);
        }
    }

    public class TableCopyOptions
    {
        public TableCopyOptions()
        {
            AllowBulkCopy = true;
            TruncateBeforeCopy = false;
            DisableConstraints = false;
        }

        [XmlElem]
        public bool AllowBulkCopy { get; set; }

        [XmlElem]
        public bool TruncateBeforeCopy { get; set; }

        [XmlElem]
        public bool DisableConstraints { get; set; }

        public void SaveToXml(XmlElement xml)
        {
            this.SavePropertiesCore(xml);
        }

        public void LoadFromXml(XmlElement xml)
        {
            this.LoadPropertiesCore(xml);
        }

        public TableCopyOptions Clone()
        {
            return (TableCopyOptions)MemberwiseClone();
        }
    }
}
