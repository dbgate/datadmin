using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Xml;
using System.Data.Common;
using System.Data;
using System.ComponentModel;

namespace Plugin.datasyn
{
    public class DataSynOptions : PropertyPageBase
    {
        [Category("s_allowed_operations")]
        [DatAdmin.DisplayName("INSERT")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [XmlElem]
        public bool? Insert { get; set; }

        [Category("s_allowed_operations")]
        [DatAdmin.DisplayName("DELETE")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [XmlElem]
        public bool? Delete { get; set; }

        [Category("s_allowed_operations")]
        [DatAdmin.DisplayName("UPDATE")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [XmlElem]
        public bool? Update { get; set; }

        [DatAdmin.DisplayName("s_disable_constraints")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [XmlElem]
        public bool? DisableConstraints { get; set; }

        public static DataSynOptions CreateDefault()
        {
            return new DataSynOptions
            {
                Delete = true,
                Insert = true,
                Update = true,
                DisableConstraints = false,
            };
        }

        public void Override(DataSynOptions spec)
        {
            if (spec.Insert != null) Insert = spec.Insert;
            if (spec.Update != null) Update = spec.Update;
            if (spec.Delete != null) Delete = spec.Delete;
            if (spec.DisableConstraints != null) DisableConstraints = spec.DisableConstraints;
        }
    }

    public class DataSynDef
    {
        public List<DataSynDefItem> Items = new List<DataSynDefItem>();
        public DataSynOptions Options = new DataSynOptions();

        public void LoadFromXml(XmlElement xml)
        {
            Options.LoadProperties(xml.FindElement("Options"));
            foreach (XmlElement elem in xml.SelectNodes("Item"))
            {
                var item = new DataSynDefItem(this);
                item.LoadFromXml(elem);
                Items.Add(item);
            }
        }

        public void SaveToXml(XmlElement xml)
        {
            Options.SaveProperties(xml.AddChild("Options"));
            foreach (var item in Items) item.SaveToXml(xml.AddChild("Item"));
        }

        public static DataSynDef CreateEmpty()
        {
            return new DataSynDef();
        }

        public void NotifyChangeSourceDatabase(IDatabaseSource src)
        {
            var names = src.InvokeLoadFullTableNames();
            foreach (var item in Items) item.NotifyChangeSourceDatabase(src);
        }

        public void NotifyChangeTargetDatabase(IDatabaseSource src)
        {
            foreach (var item in Items) item.NotifyChangeTargetDatabase(src);
        }

        public void NotifyChangeSourceModel(DatabaseStructure model)
        {
            foreach (var item in Items) item.NotifyChangeSourceModel(model);
        }

        public void NotifyChangeTargetModel(DatabaseStructure model)
        {
            foreach (var item in Items) item.NotifyChangeTargetModel(model);
        }
    }

    public abstract class DataSynDataSource
    {
        [XmlElem("SqlCondition")]
        public string SqlCondition { get; set; }

        public virtual ITableStructure GetModel(IDatabaseSource conn)
        {
            conn.Connection.SystemConnection.SafeChangeDatabase(conn.DatabaseName);
            using (DbCommand cmd = conn.Connection.SystemConnection.CreateCommand())
            {
                string sql = GetReadQuery(conn.Dialect);
                if (conn.Dialect.DialectCaps.LimitSelect)
                {
                    sql = conn.Dialect.GetLimitSelect(sql, 1);
                }
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
                {
                    return reader.GetTableStructure(conn.Connection.Dialect);
                }
            }
        }
        public abstract string GetReadQuery(ISqlDialect dialect);
        public abstract SelectQueryBase GetReadQuery(ISqlDialect dialect, IEnumerable<string> columns);

        public abstract bool StaticValid();
        public abstract string XmlType { get; }

        public virtual void SaveToXml(XmlElement xml)
        {
            this.SavePropertiesCore(xml);
            xml.SetAttribute("type", XmlType);
        }

        public static DataSynDataSource LoadFromXml(XmlElement xml)
        {
            switch (xml.GetAttribute("type"))
            {
                case "table":
                    return new DataSynTableSource(xml);
                case "view":
                    return new DataSynViewSource(xml);
                case "query":
                    return new DataSynQuerySource(xml);
            }
            throw new InternalError("DAE-00066 Unknown synsource type:" + xml.GetAttribute("type"));
        }

        public DataSynDataSource() { }
        public DataSynDataSource(XmlElement xml)
        {
            this.LoadPropertiesCore(xml);
        }

        public virtual void NotifyChangeDatabase(IDatabaseSource src)
        {
        }

        public virtual void NotifyChangeModel(DatabaseStructure model)
        {
        }
    }

    public abstract class DataSynTableOrViewSource : DataSynDataSource
    {
        public NameWithSchema Name { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }

        public override void NotifyChangeDatabase(IDatabaseSource src)
        {
            if (src.Dialect.DialectCaps.UseDatabaseAsSchema)
            {
                Name = new NameWithSchema(src.DatabaseName, Name.Name);
            }
        }

        public override string GetReadQuery(ISqlDialect dialect)
        {
            return String.Format("SELECT * FROM {0}", dialect.QuoteFullName(Name));
        }

        public override SelectQueryBase GetReadQuery(ISqlDialect dialect, IEnumerable<string> columns)
        {
            return new SelectFromTableQuery(Name, columns);
            //return String.Format("SELECT {0} FROM {1}",
            //    (from c in columns select dialect.QuoteIdentifier(c)).CreateDelimitedText(","),
            //    dialect.QuoteFullName(Name));
        }

        public override bool StaticValid()
        {
            return Name != null;
        }

        public DataSynTableOrViewSource() { }
        public DataSynTableOrViewSource(XmlElement xml)
            : base(xml)
        {
            Name = NameWithSchema.LoadFromXml(xml.FindElement("Name"));
        }
        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            if (Name != null) Name.SaveToXml(xml.AddChild("Name"));
        }
    }

    public class DataSynTableSource : DataSynTableOrViewSource
    {
        public override string XmlType
        {
            get { return "table"; }
        }

        public DataSynTableSource() { }

        public DataSynTableSource(XmlElement xml)
            : base(xml)
        {
        }

        public override ITableStructure GetModel(IDatabaseSource conn)
        {
            var dbmem = new DatabaseStructureMembers
            {
                TableFilter = new List<NameWithSchema> { Name },
                TableMembers = TableStructureMembers.Columns | TableStructureMembers.PrimaryKey
            };
            var dbs = conn.LoadDatabaseStructure(dbmem, null);
            return dbs.Tables[Name];
        }

        public override void NotifyChangeModel(DatabaseStructure model)
        {
            Name = Name.GetOnlyCaseDifferentName(from t in model.Tables select t.FullName);
        }
    }

    public class DataSynViewSource : DataSynTableOrViewSource
    {
        public override string XmlType
        {
            get { return "view"; }
        }

        public DataSynViewSource() { }

        public DataSynViewSource(XmlElement xml)
            : base(xml)
        {
        }

        public override void NotifyChangeModel(DatabaseStructure model)
        {
            Name = Name.GetOnlyCaseDifferentName(from v in model.SpecByType("view") select v.ObjectName);
        }
    }

    public class DataSynQuerySource : DataSynDataSource
    {
        [XmlElem("Query")]
        public string Query { get; set; }

        public override string ToString()
        {
            return "(" + Texts.Get("s_query") + ")";
        }

        public override string GetReadQuery(ISqlDialect dialect)
        {
            return Query;
        }

        public override bool StaticValid()
        {
            return !Query.SafeToString().IsEmpty();
        }

        public override SelectQueryBase GetReadQuery(ISqlDialect dialect, IEnumerable<string> columns)
        {
            return new SelectQuerySql(Query, columns);
            //return String.Format("SELECT {0} FROM ({1}) subq",
            //    (from c in columns select dialect.QuoteIdentifier(c)).CreateDelimitedText(","),
            //    Query);
        }

        public override string XmlType
        {
            get { return "query"; }
        }

        public DataSynQuerySource() { }

        public DataSynQuerySource(XmlElement xml)
            : base(xml)
        {
        }
    }

    public class DataSynTarget
    {
        [XmlElem("SqlCondition")]
        public string SqlCondition { get; set; }

        public NameWithSchema Table { get; set; }

        public override string ToString()
        {
            return Table.ToString();
        }

        public ITableStructure GetModel(IDatabaseSource conn)
        {
            var dbmem = new DatabaseStructureMembers
            {
                TableFilter = new List<NameWithSchema> { Table },
                TableMembers = TableStructureMembers.Columns | TableStructureMembers.PrimaryKey
            };
            var dbs = conn.LoadDatabaseStructure(dbmem, null);
            return dbs.Tables[Table];
        }

        public string GetReadQuery(ISqlDialect dialect)
        {
            return String.Format("SELECT * FROM {0}", dialect.QuoteFullName(Table));
        }

        public SelectQueryBase GetReadQuery(ISqlDialect dialect, IEnumerable<string> columns)
        {
            return new SelectFromTableQuery(Table, columns);
            //return String.Format("SELECT {0} FROM {1}",
            //    (from c in columns select dialect.QuoteIdentifier(c)).CreateDelimitedText(","),
            //    dialect.QuoteFullName(Table));
        }

        public void SaveToXml(XmlElement xml)
        {
            this.SavePropertiesCore(xml);
            if (Table != null) Table.SaveToXml(xml.AddChild("Table"));
        }

        public void LoadFromXml(XmlElement xml)
        {
            this.LoadPropertiesCore(xml);
            Table = NameWithSchema.LoadFromXml(xml.FindElement("Table"));
        }

        public void NotifyChangeDatabase(IDatabaseSource src)
        {
            if (src.Dialect.DialectCaps.UseDatabaseAsSchema)
            {
                Table = new NameWithSchema(src.DatabaseName, Table.Name);
            }
        }

        public void NotifyChangeModel(DatabaseStructure model)
        {
            Table = Table.GetOnlyCaseDifferentName(from t in model.Tables select t.FullName);
        }
    }

    public class DataSynDefItem
    {
        public enum ColumnMode { All, Selected, AllExceptSelected, CustomMapping }
        public DataSynDataSource Source;
        public DataSynTarget Target;
        [XmlElem("ColumnMode")]
        public ColumnMode ColMode { get; set; }
        [XmlElem("CompareColMode")]
        public ColumnMode CompareColMode { get; set; }
        public List<string> KeyColsOverride;
        public List<string> CompareColsOverride;
        public List<string> CompareNoColsOverride;
        public List<string> SelectedColumns = new List<string>();
        public List<string> SelectedNoColumns = new List<string>();
        public Dictionary<string, string> SelectedMapping = new Dictionary<string, string>(); // map[targetcol]=sourcecol
        public Dictionary<string, string> DefaultValues = new Dictionary<string, string>();
        [XmlSubElem("Options")]
        public DataSynOptions Options { get; set; }
        DataSynDef m_syndef;
        [XmlElem]
        public bool IsChecked { get; set; }

        public DataSynDefItem(DataSynDef syndef)
        {
            ColMode = ColumnMode.All;
            CompareColMode = ColumnMode.All;
            Options = new DataSynOptions();
            IsChecked = true;
            m_syndef = syndef;
        }

        public DataSynOptions GetOptions()
        {
            var res = DataSynOptions.CreateDefault();
            res.Override(m_syndef.Options);
            res.Override(Options);
            return res;
        }

        public string ColumnsDescription
        {
            get
            {
                switch (ColMode)
                {
                    case ColumnMode.All:
                        return Texts.Replace("({s_all})");
                    case ColumnMode.AllExceptSelected:
                        return Texts.Get("s_no") + ": " + SelectedNoColumns.CreateDelimitedText(", ");
                    case ColumnMode.Selected:
                        return SelectedColumns.CreateDelimitedText(", ");
                    case ColumnMode.CustomMapping:
                        return (from m in SelectedMapping select m.Value + "=>" + m.Key).CreateDelimitedText(", ");
                }
                return "";
            }
        }

        public void BuildInfo(IDatabaseSource srcConn, IDatabaseSource dstConn, out SynSourceInfo source, out SynSourceInfo target)
        {
            source = new SynSourceInfo();
            target = new SynSourceInfo();

            if (!(Source is DataSynQuerySource))
            {
                source.SqlCondition = Source.SqlCondition;
            }
            target.SqlCondition = Target.SqlCondition;

            source.Model = Source.GetModel(srcConn);
            target.Model = Target.GetModel(dstConn);

            if (KeyColsOverride != null)
            {
                target.KeyCols = KeyColsOverride.ToArray();
            }
            else
            {
                target.KeyCols = target.Model.GetPkColumns().GetNames();
            }
            if (target.KeyCols.Length == 0)
            {
                throw new DataSynError("DAE-00319 Table " + target.Model.FullName.ToString() + " has empty synchronize key or table has not primary key");
            }

            SynColMapping map = null;
            if (ColMode != ColumnMode.CustomMapping) map = SynColMapping.CreateMapping(source.Model, target.Model);
            else map = SynColMapping.CreateMapping(SelectedMapping);
            switch (ColMode)
            {
                case ColumnMode.All:
                    AssignColsAndQuery(source, target, srcConn, dstConn, map.SourceCols, map.TargetCols);
                    break;
                case ColumnMode.Selected:
                    AssignColsAndQuery(source, target, srcConn, dstConn, map[SelectedColumns], SelectedColumns.ToArray());
                    break;
                case ColumnMode.AllExceptSelected:
                    string[] selcols = map.Complement(SelectedNoColumns);
                    AssignColsAndQuery(source, target, srcConn, dstConn, map[selcols], selcols);
                    break;
                case ColumnMode.CustomMapping:
                    AssignColsAndQuery(source, target, srcConn, dstConn, SelectedMapping.Values.ToArray(), SelectedMapping.Keys.ToArray());
                    break;
            }
            switch (CompareColMode)
            {
                case ColumnMode.All:
                    AssignCompareCols(source, target, srcConn, dstConn, source.DataCols, target.DataCols);
                    break;
                case ColumnMode.Selected:
                    AssignCompareCols(source, target, srcConn, dstConn, map[CompareColsOverride], CompareColsOverride.ToArray());
                    break;
                case ColumnMode.AllExceptSelected:
                    string[] ccols = ArrayTool.Difference(target.DataCols, CompareNoColsOverride);
                    AssignCompareCols(source, target, srcConn, dstConn, map[ccols], ccols.ToArray());
                    break;
            }
        }

        private void AssignCompareCols(SynSourceInfo source, SynSourceInfo target, IDatabaseSource srcConn, IDatabaseSource dstConn, string[] srcCols, string[] dstCols)
        {
            source.CompareCols = srcCols;
            target.CompareCols = dstCols;
        }

        private void AssignColsAndQuery(SynSourceInfo source, SynSourceInfo target, IDatabaseSource srcConn, IDatabaseSource dstConn, string[] srcCols, string[] dstCols)
        {
            source.Query = Source.GetReadQuery(srcConn.Dialect, srcCols);
            target.Query = Target.GetReadQuery(dstConn.Dialect, dstCols);
            var map = new Dictionary<string, string>();
            for (int i = 0; i < srcCols.Length; i++) map[dstCols[i]] = srcCols[i];
            foreach (var c in target.KeyCols) if (!map.ContainsKey(c)) throw new DataSynError(String.Format("DAE-00320 Key column {0} of table {1} is not synchronized", c, Target.Table));
            source.KeyCols = (from c in target.KeyCols select map[c]).ToArray();
            target.DataCols = (from c in dstCols where !target.KeyCols.Contains(c) select c).ToArray();
            source.DataCols = (from c in target.DataCols select map[c]).ToArray();
        }

        private void LoadList(XmlElement xml, ref List<string> list, string elemname)
        {
            foreach (XmlElement elem in xml.SelectNodes(elemname))
            {
                if (list == null) list = new List<string>();
                list.Add(elem.GetAttribute("name"));
            }
        }

        private void LoadDict(XmlElement xml, Dictionary<string, string> dict, string elemname, string keyname, string valname)
        {
            foreach (XmlElement elem in xml.SelectNodes(elemname))
            {
                dict[elem.GetAttribute(keyname)] = elem.GetAttribute(valname);
            }
        }

        private void SaveList(XmlElement xml, List<string> list, string elemname)
        {
            if (list == null) return;
            foreach (string elem in list)
            {
                xml.AddChild(elemname).SetAttribute("name", elem);
            }
        }

        private void SaveDict(XmlElement xml, Dictionary<string, string> dict, string elemname, string keyname, string valname)
        {
            if (dict == null) return;
            foreach (var tuple in dict)
            {
                var elem = xml.AddChild(elemname);
                elem.SetAttribute(keyname, tuple.Key);
                elem.SetAttribute(valname, tuple.Value);
            }
        }

        public void LoadFromXml(XmlElement xml)
        {
            this.LoadPropertiesCore(xml);
            var xs = xml.FindElement("Source");
            if (xs != null)
            {
                Source = DataSynDataSource.LoadFromXml(xs);
            }
            var xt = xml.FindElement("Target");
            if (xt != null)
            {
                Target = new DataSynTarget();
                Target.LoadFromXml(xt);
            }

            LoadList(xml, ref KeyColsOverride, "KeyCol");
            LoadList(xml, ref SelectedColumns, "SelectedColumn");
            LoadList(xml, ref SelectedNoColumns, "SelectedNoColumn");
            LoadList(xml, ref CompareColsOverride, "CompareColsOverride");
            LoadList(xml, ref CompareNoColsOverride, "CompareNoColsOverride");
            LoadDict(xml, SelectedMapping, "ColumnMapping", "dst", "src");
            LoadDict(xml, DefaultValues, "DefaultValue", "dst", "value");
        }

        public void SaveToXml(XmlElement xml)
        {
            this.SavePropertiesCore(xml);
            if (Source != null) Source.SaveToXml(xml.AddChild("Source"));
            if (Target != null) Target.SaveToXml(xml.AddChild("Target"));

            SaveList(xml, KeyColsOverride, "KeyCol");
            SaveList(xml, SelectedColumns, "SelectedColumn");
            SaveList(xml, SelectedNoColumns, "SelectedNoColumn");
            SaveList(xml, CompareColsOverride, "CompareColsOverride");
            SaveList(xml, CompareNoColsOverride, "CompareNoColsOverride");
            SaveDict(xml, SelectedMapping, "ColumnMapping", "dst", "src");
            SaveDict(xml, DefaultValues, "DefaultValue", "dst", "value");
        }

        public void NotifyChangeSourceDatabase(IDatabaseSource src)
        {
            if (Source != null) Source.NotifyChangeDatabase(src);
        }

        public void NotifyChangeTargetDatabase(IDatabaseSource src)
        {
            if (Target != null) Target.NotifyChangeDatabase(src);
        }

        public void NotifyChangeSourceModel(DatabaseStructure model)
        {
            if (Source != null) Source.NotifyChangeModel(model);
        }

        public void NotifyChangeTargetModel(DatabaseStructure model)
        {
            if (Target != null) Target.NotifyChangeModel(model);
        }
    }
}
