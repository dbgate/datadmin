using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Data;
using System.Xml;
using System.ComponentModel;

namespace Plugin.textio
{
    public enum TextSourceType { File, Url, Text }

    public enum RowDelimiterType { Lines, Separator, Limitation, Regex }

    [TabularDataStore(Name = "textimport", Title = "s_import_from_text", Description = "s_textimport_desc", RequiredFeature = TextImportFeature.Test)]
    public class TextImportDataStore : StreamDataStoreBase, ICustomPropertyPage
    {
        //DataFormatSettings m_formatSettings = new DataFormatSettings();

        //[TabbedProperty("s_format_settings")]
        //[Browsable(false)]
        //[XmlSubElem("DataFormat")]
        //public DataFormatSettings FormatSettings
        //{
        //    get { return m_formatSettings; }
        //    set { m_formatSettings = value; }
        //}

        string m_loadedSource;

        [XmlElem]
        public string Source { get; set; }
        [XmlElem]
        public TextSourceType SourceType { get; set; }

        [XmlElem]
        public RowDelimiterType RowDelimiter { get; set; }
        [XmlElem]
        public string LineSeparator { get; set; }
        [XmlElem]
        public bool LineSeparatowIsRegex { get; set; }
        [XmlElem]
        public string LineLimitBegin { get; set; }
        [XmlElem]
        public string LineLimitEnd { get; set; }
        [XmlElem]
        public string LineRegex { get; set; }
        [XmlElem]
        public string LineRegexGroup { get; set; }

        [XmlElem]
        public Encoding SourceEncoding { get; set; }

        public FieldAnalyser FieldAnalyser = new WholeLineAnalyser();

        public TextImportDataStore()
        {
            Source = "";
            SourceType = TextSourceType.File;
            SourceEncoding = Encoding.UTF8;
        }

        public override bool SupportsMode(TabularDataStoreMode mode)
        {
            return mode == TabularDataStoreMode.Read;
        }

        protected override void DoRead(DatAdmin.IDataQueue queue)
        {
            try
            {
                foreach (var row in GetTable().Rows)
                {
                    queue.PutRecord(row);
                }
                queue.PutEof();
            }
            catch (Exception err)
            {
                Errors.Report(err);
                queue.PutError(err);
            }
            finally
            {
                queue.CloseWriting();
            }
        }

        protected override ITableStructure DoGetRowFormat()
        {
            return GetTable(false).Structure;
        }

        #region ICustomPropertyPage Members

        public Control CreatePropertyPage()
        {
            return new TextImportFrame(this);
        }

        #endregion

        public override void ClearLoadCaches()
        {
            base.ClearLoadCaches();
            m_loadedSource = null;
        }

        protected override void DoWrite(DatAdmin.IDataQueue queue)
        {
            throw new NotImplementedError("DAE-00150");
        }

        public string LoadSource()
        {
            switch (SourceType)
            {
                case TextSourceType.File:
                    using (StreamReader sr = new StreamReader(Source, SourceEncoding))
                    {
                        return sr.ReadToEnd();
                    }
                case TextSourceType.Text:
                    return Source;
                case TextSourceType.Url:
                    WebRequest req = WebRequest.Create(Source);
                    using (WebResponse resp = req.GetResponse())
                    {
                        using (Stream fr = resp.GetResponseStream())
                        {
                            using (StreamReader sr = new StreamReader(fr, SourceEncoding))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
            }
            throw new InternalError("DAE-00062 Unknown source type:" + SourceType.ToString());
        }

        public string GetInputText()
        {
            if (m_loadedSource == null) m_loadedSource = LoadSource();
            return m_loadedSource;
        }

        private IEnumerable<string> EnumRows_Lines()
        {
            string input = GetInputText();
            using (StringReader sr = new StringReader(input))
            {
                for (; ; )
                {
                    string line = sr.ReadLine();
                    if (line == null) yield break;
                    yield return line;
                }
            }
        }

        private IEnumerable<string> EnumRows_Separator()
        {
            return TextTool.SplitBy(GetInputText(), LineSeparator, LineSeparatowIsRegex);
        }

        private IEnumerable<string> EnumRows_Limitation()
        {
            string input = GetInputText();
            int index = 0;
            while (index < input.Length)
            {
                int nextindex = input.IndexOf(LineLimitBegin, index);
                if (nextindex < 0) yield break;
                int nstart = nextindex + LineLimitBegin.Length;
                int nextend = input.IndexOf(LineLimitEnd, nstart);
                if (nextend < 0) yield break;
                yield return input.Substring(nstart, nextend - nstart);
                index = nextend + LineLimitEnd.Length;
            }
        }

        private IEnumerable<string> EnumRows_Regex()
        {
            string input = GetInputText();
            int index = 0;
            Regex reg = new Regex(LineRegex, RegexOptions.Compiled);
            while (index < input.Length)
            {
                var m = reg.Match(input, index);
                if (!m.Success) yield break;
                yield return m.GetGroupValue(LineRegexGroup);
                index = m.Index + m.Length;
            }
        }

        public IEnumerable<string> EnumRows()
        {
            switch (RowDelimiter)
            {
                case RowDelimiterType.Lines:
                    return EnumRows_Lines();
                case RowDelimiterType.Separator:
                    return EnumRows_Separator();
                case RowDelimiterType.Limitation:
                    return EnumRows_Limitation();
                case RowDelimiterType.Regex:
                    return EnumRows_Regex();
            }
            throw new InternalError("DAE-00063 Unknown row delimiter:" + RowDelimiter.ToString());
        }

        public InMemoryTable GetTable()
        {
            return GetTable(true);
        }

        public InMemoryTable GetTable(bool wantdata)
        {
            List<DataRecord> records = new List<DataRecord>();
            foreach (string row in EnumRows())
            {
                records.Add(FieldAnalyser.AnalyseRecord(row));
            }
            var ts = new TableStructure();
            Dictionary<string, int> colindexes = new Dictionary<string, int>();
            // get column collection
            foreach (var rec in records)
            {
                foreach (var fld in rec.Fields)
                {
                    if (colindexes.ContainsKey(fld.Name)) continue;
                    var col = new ColumnStructure();
                    col.ColumnName = fld.Name;
                    col.DataType = new DbTypeString();
                    colindexes[fld.Name] = ts._Columns.Count;
                    ts._Columns.Add(col);
                }
            }
            if (!wantdata) return new InMemoryTable(ts);
            var recs = new List<ArrayDataRecord>();
            foreach (var rec in records)
            {
                var row = new ArrayDataRecord(ts);
                foreach (var fld in rec.Fields)
                {
                    row.SeekValue(colindexes[fld.Name]);
                    row.SetString(fld.Value);
                }
                recs.Add(row);
            }
            return InMemoryTable.FromEnumerable(ts, recs);
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            FieldAnalyser.SaveToXml(xml.AddChild("FieldAnalyser"));
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            FieldAnalyser = Plugin.textio.FieldAnalyser.LoadFromXml(xml.FindElement("FieldAnalyser"));
        }
    }

    public class DataRecord
    {
        public class Field
        {
            public string Name;
            public string Value;
        }
        public List<Field> Fields = new List<Field>();

        public void AddField(string name, string value)
        {
            Fields.Add(new Field { Name = name, Value = value });
        }
    }
}
