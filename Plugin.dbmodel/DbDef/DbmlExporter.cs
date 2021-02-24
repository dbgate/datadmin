using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using DatAdmin;
using System.Xml;
using System.ComponentModel;

namespace Plugin.dbmodel
{
    [DatabaseWriter(Name = "dbml_writer", Title = "s_dbml_writer", Description = "s_dbml_writer_desc", SupportsCreateTemplate = false)]
    public class DbmlExporter : FileDatabaseWriter
    {
        public override string FileExtension
        {
            get { return ".dbml"; }
        }

        [XmlElem]
        [DatAdmin.DisplayName("s_entity_namespace")]
        public string EntityNamespace { get; set; }

        [XmlElem]
        [DatAdmin.DisplayName("s_context_namespace")]
        public string ContextNamespace { get; set; }

        [XmlElem]
        [DatAdmin.DisplayName("s_class_name")]
        public string ClassName { get; set; }

        [XmlElem]
        [DatAdmin.DisplayName("s_database_name")]
        public string DatabaseName { get; set; }

        private ISqlDialect m_dialect = new GenericDialect();

        [Category("SQL")]
        [DatAdmin.DisplayName("s_dialect")]
        [TypeConverter(typeof(DialectTypeConverter))]
        [XmlElem("Dialect")]
        public ISqlDialect Dialect
        {
            get { return m_dialect; }
            set { m_dialect = value; }
        }


        private string GetLinqType(DbTypeBase type)
        {
            if (type is DbTypeXml) return "System.Xml.Linq.XElement";
            return type.DotNetType.FullName;
        }

        private string GetMemberName(HashSetEx<string> members, string name, string altnamebase)
        {
            name = name.Replace(" ", "_");
            if (altnamebase != null) altnamebase = altnamebase.Replace(" ", "_");
            if (!members.Contains(name))
            {
                members.Add(name);
                return name;
            }
            if (altnamebase != null && altnamebase.EndsWith("_ID") && !members.Contains(altnamebase.Substring(0, altnamebase.Length - 2)))
            {
                string n=altnamebase.Substring(0,altnamebase.Length-2);
                members.Add(n);
                return n;
            }
            string name0 = name;
            int idx = 1;
            while (members.Contains(name))
            {
                name = name0 + idx.ToString();
                idx++;
            }
            members.Add(name);
            return name;
        }

        public override void WriteStructureAfterData(IDatabaseStructure db)
        {
            HashSetEx<string> usedTypes = new HashSetEx<string> { "String", "Int", "DateTime" };
            Dictionary<NameWithSchema, string> tableTypes = new Dictionary<NameWithSchema, string>();
            foreach (var tbl in db.Tables)
            {
                tableTypes[tbl.FullName] = GetMemberName(usedTypes, tbl.FullName.Name, null);
            }

            ISqlDialect dialect = m_dialect;
            using (XmlWriter xw = XmlWriter.Create(GetWorkingFileName(), new XmlWriterSettings { Indent = true }))
            {
                string XMLNS = "http://schemas.microsoft.com/linqtosql/dbml/2007";
                xw.WriteStartDocument();
                xw.WriteStartElement("Database", XMLNS);
                xw.WriteAttributeString("xmlns", XMLNS);
                xw.WriteAttributeString("Name", DatabaseName);
                xw.WriteAttributeString("Class", ClassName);
                xw.WriteAttributeString("ContextNamespace", ContextNamespace);
                xw.WriteAttributeString("EntityNamespace", EntityNamespace);
                foreach (var tbl in db.Tables)
                {
                    HashSetEx<string> tblmembers = new HashSetEx<string>();
                    string tblmember = tbl.FullName.Name;
                    tblmembers.Add(tblmember);
                    xw.WriteStartElement("Table");
                    xw.WriteAttributeString("Name", tbl.FullName.ToString());
                    xw.WriteAttributeString("Member", tblmember);
                    xw.WriteStartElement("Type");
                    xw.WriteAttributeString("Name", tableTypes[tbl.FullName]);
                    IPrimaryKey pk = tbl.FindConstraint<IPrimaryKey>();
                    foreach (var col in tbl.Columns)
                    {
                        string colmember = GetMemberName(tblmembers, col.ColumnName, null);
                        tblmembers.Add(colmember);
                        xw.WriteStartElement("Column");
                        if (col.ColumnName != colmember) xw.WriteAttributeString("Member", colmember);
                        if (pk != null && pk.Columns.Any(c => c.ColumnName == col.ColumnName)) xw.WriteAttributeString("IsPrimaryKey", "true");
                        xw.WriteAttributeString("Name", col.ColumnName);
                        xw.WriteAttributeString("CanBeNull", col.IsNullable ? "true" : "false");
                        xw.WriteAttributeString("Type", GetLinqType(col.DataType));
                        xw.WriteAttributeString("DbType", dialect.GenericTypeToSpecific(col.DataType).ToString());
                        if (col.DataType.IsAutoIncrement()) xw.WriteAttributeString("IsDbGenerated", "true");
                        xw.WriteEndElement(); // Column
                    }
                    foreach (var fk in tbl.GetConstraints<IForeignKey>())
                    {
                        if (fk.Columns.Count != 1) continue;
                        xw.WriteStartElement("Association");
                        xw.WriteAttributeString("Name", fk.Name);
                        xw.WriteAttributeString("Member", GetMemberName(tblmembers, fk.PrimaryKeyTable.Name, fk.Columns[0].ColumnName));
                        xw.WriteAttributeString("Type", tableTypes[fk.PrimaryKeyTable]);
                        xw.WriteAttributeString("ThisKey", fk.Columns[0].ColumnName);
                        xw.WriteAttributeString("OtherKey", fk.PrimaryKeyColumns[0].ColumnName);
                        xw.WriteAttributeString("IsForeignKey", "true");
                        xw.WriteEndElement(); // Association
                    }
                    xw.WriteEndElement(); // Type
                    xw.WriteEndElement(); // Table
                }
                xw.WriteEndElement(); // Database
                xw.WriteEndDocument();
            }
        }

        public override void CloseConnection()
        {
            base.CloseConnection();
            FinalizeFileName();
        }
    }
}
