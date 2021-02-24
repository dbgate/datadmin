using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DatAdmin
{
    public class DatabaseStructureMembers
    {
        public TableStructureMembers TableMembers = TableStructureMembers.None;
        /// <summary>
        /// only properties of this table is returned, table list is not influenced
        /// </summary>
        public List<NameWithSchema> TableFilter = null;

        public List<NameWithSchema> ViewAsTableFilter = null;

        /// <summary>
        /// dict : object type => object members (+ SchemaFilter if schema is important)
        /// </summary>
        public Dictionary<string, SpecificObjectMembers> SpecificObjectOverride = new Dictionary<string, SpecificObjectMembers>();

        public bool SchemaList { get; set; }
        [XmlAttrib("schema")]
        public bool SchemaDetails { get; set; }

        public bool ViewAsTables { get; set; }

        public bool TableList { get; set; }

        public bool DomainList { get; set; }
        [XmlAttrib("domain")]
        public bool DomainDetails { get; set; }

        public bool CharacterSetList { get; set; }
        public bool CharacterSetDetails { get; set; }

        public bool CollationList { get; set; }
        public bool CollationDetails { get; set; }

        // generic behaviour for all specific objects, can be overriden in SpecificObjectOverride
        public bool SpecificObjectList { get; set; }
        [XmlAttrib("specobject")]
        public bool SpecificObjectDetails { get; set; }

        public bool IgnoreSystemObjects = false;

        [XmlElem]
        public bool LoadDependencies { get; set; }

        public bool DatabaseOptions { get; set; }

        public DatabaseStructureMembers Clone()
        {
            DatabaseStructureMembers res = (DatabaseStructureMembers)MemberwiseClone();
            if (TableFilter != null) res.TableFilter = new List<NameWithSchema>(TableFilter);
            res.SpecificObjectOverride = new Dictionary<string, SpecificObjectMembers>();
            res.SpecificObjectOverride.AddAllMapped(SpecificObjectOverride, m => m.Clone());
            return res;
        }

        public bool HasTableDetails(NameWithSchema name)
        {
            if ((TableMembers & TableStructureMembers.AllNoRefs) == 0) return false;
            if (TableFilter != null) return TableFilter.Contains(name);
            return true;
        }

        public bool HasSpecificObjectDetails(string objtype, NameWithSchema name)
        {
            if (SpecificObjectOverride.ContainsKey(objtype))
            {
                var mem = SpecificObjectOverride[objtype];
                if (!mem.ObjectDetail) return false;
                if (mem.ObjectFilter != null) return mem.ObjectFilter.Contains(name);
                return true;
            }
            return SpecificObjectDetails;
        }

        public SpecificObjectMembers this[string objtype]
        {
            get
            {
                if (SpecificObjectOverride.ContainsKey(objtype)) return SpecificObjectOverride[objtype];
                return new SpecificObjectMembers
                {
                    ObjectDetail = SpecificObjectDetails,
                    ObjectList = SpecificObjectList,
                };
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if ((TableMembers & TableStructureMembers.AllNoRefs) != 0)
            {
                if (TableFilter != null)
                {
                    sb.AppendFormat("{0}:\n", Texts.Get("s_tables"));
                    foreach (var name in TableFilter)
                    {
                        sb.AppendFormat("    {0}\n", name);
                    }
                }
                else
                {
                    sb.AppendFormat("{0}: {1}\n", Texts.Get("s_tables"), Texts.Get("s_all"));
                }
            }
            foreach (string objtype in SpecificObjectOverride.Keys)
            {
                var mem = this[objtype];
                var repr = SpecificRepresentationAddonType.Instance.FindRepresentation(objtype);
                if (mem.ObjectFilter != null)
                {
                    sb.AppendFormat("{0}:\n", Texts.Get(repr.TitlePlural));
                    foreach (var name in mem.ObjectFilter)
                    {
                        sb.AppendFormat("    {0}\n", name);
                    }
                }
                else
                {
                    sb.AppendFormat("{0}: {1}\n", Texts.Get(repr.TitlePlural), Texts.Get("s_all"));
                }
            }
            return sb.ToString();
        }

        public void SaveToXml_ForJob(XmlElement xml)
        {
            if (TableMembers != TableStructureMembers.None) xml.SetAttribute("table", "1");
            if (TableFilter != null)
            {
                foreach (var name in TableFilter)
                {
                    name.SaveToXml(xml.AddChild("Table"));
                }
            }
            foreach (var spectype in SpecificObjectOverride.Keys)
            {
                string xmlname = SpecificRepresentationAddonType.Instance.FindRepresentation(spectype).XmlElementName;
                if (SpecificObjectOverride[spectype].ObjectDetail) xml.SetAttribute("detail." + spectype, "1");
                if (SpecificObjectOverride[spectype].ObjectFilter != null)
                {
                    foreach (var name in SpecificObjectOverride[spectype].ObjectFilter)
                    {
                        name.SaveToXml(xml.AddChild(xmlname));
                    }
                }
            }
            this.SavePropertiesCore(xml);
        }

        public void LoadFromXml_ForJob(XmlElement xml)
        {
            if (xml.GetAttribute("table") == "1") TableMembers = TableStructureMembers.AllNoRefs;
            if (xml.SelectSingleNode("Table") != null) TableFilter = new List<NameWithSchema>();
            foreach (XmlElement x in xml.SelectNodes("Table")) TableFilter.Add(NameWithSchema.LoadFromXml(x));
            foreach (XmlAttribute attr in xml.Attributes)
            {
                if (!attr.Name.StartsWith("detail.")) continue;
                string type = attr.Name.Substring("detail.".Length);
                SpecificObjectOverride[type] = new SpecificObjectMembers();
                SpecificObjectOverride[type].ObjectDetail = true;
            }
            foreach (XmlElement child in xml)
            {
                if (child.Name == "Table") continue;
                var repr = SpecificRepresentationAddonType.Instance.FindByElement(child.Name);
                if (repr == null) continue;
                if (SpecificObjectOverride[repr.ObjectType] == null) SpecificObjectOverride[repr.ObjectType] = new SpecificObjectMembers();
                if (SpecificObjectOverride[repr.ObjectType].ObjectFilter == null) SpecificObjectOverride[repr.ObjectType].ObjectFilter = new List<NameWithSchema>();
                SpecificObjectOverride[repr.ObjectType].ObjectFilter.Add(NameWithSchema.LoadFromXml(child));
            }
            this.LoadPropertiesCore(xml);
        }

        public static DatabaseStructureMembers FullStructure
        {
            get
            {
                return new DatabaseStructureMembers
                {
                    TableList = true,
                    DomainList = true,
                    TableMembers = TableStructureMembers.AllNoRefs,
                    SpecificObjectList = true,
                    SpecificObjectDetails = true,
                    DatabaseOptions = true,
                    LoadDependencies = true,
                };
            }
        }
    }

    public class SpecificObjectMembers
    {
        public List<NameWithSchema> ObjectFilter;
        public bool ObjectList;
        public bool ObjectDetail;

        public SpecificObjectMembers Clone()
        {
            var res = (SpecificObjectMembers)MemberwiseClone();
            if (ObjectFilter != null) res.ObjectFilter = new List<NameWithSchema>(ObjectFilter);
            return res;
        }
    }
}
