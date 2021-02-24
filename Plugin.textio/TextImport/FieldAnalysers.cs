using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Linq;
using System.Text.RegularExpressions;
using IronPython.Hosting;
using System.Xml;

namespace Plugin.textio
{
    public abstract class FieldAnalyser
    {
        public abstract DataRecord AnalyseRecord(string data);
        public abstract void SaveToXml(XmlElement xml);
        public static FieldAnalyser LoadFromXml(XmlElement xml)
        {
            switch (xml.GetAttribute("type"))
            {
                case "regex": return new RegexFieldAnalyser(xml);
                case "separated": return new SeparatedFieldsAnalyser(xml);
                case "line": return new WholeLineAnalyser(xml);
                case "script": return new ScriptAnalyser(xml);
            }
            throw new InternalError("DAE-00061 Invalid field analyser type:" + xml.GetAttribute("type"));
        }
    }

    public class RegexFieldAnalyser : FieldAnalyser
    {
        public class Field
        {
            private string m_pattern;

            [XmlElem]
            public string Pattern
            {
                get { return m_pattern; }
                set
                {
                    m_pattern = value;
                    try
                    {
                        m_regex = new Regex(value, RegexOptions.Compiled);
                    }
                    catch
                    {
                        m_regex = null;
                    }
                }
            }

            internal Regex m_regex;

            [XmlElem]
            public string Group { get; set; }
            [XmlElem]
            public string Skip { get; set; }
            [XmlElem]
            public string FieldName { get; set; }
        }

        public List<Field> Fields = new List<Field>();

        public override DataRecord AnalyseRecord(string data)
        {
            DataRecord res = new DataRecord();
            int fieldord = 1;
            foreach (var fld in Fields)
            {
                if (fld.m_regex == null) continue;
                var ms = fld.m_regex.Matches(data);
                int skip = 0;
                Int32.TryParse(fld.Skip, out skip);
                if (skip >= ms.Count) continue;
                res.Fields.Add(new DataRecord.Field
                {
                    Name = fld.FieldName ?? "Field" + fieldord.ToString(),
                    Value = ms[skip].GetGroupValue(fld.Group)
                });
                fieldord++;
            }
            return res;
        }

        public override void SaveToXml(XmlElement xml)
        {
            xml.SetAttribute("type", "regex");
            foreach (var fld in Fields)
            {
                fld.SaveProperties(xml.AddChild("Field"));
            }
            this.SavePropertiesCore(xml);
        }

        public RegexFieldAnalyser() { }

        public RegexFieldAnalyser(XmlElement xml)
        {
            foreach (XmlElement x in xml.SelectNodes("Field"))
            {
                var fld = new Field();
                fld.LoadProperties(x);
                Fields.Add(fld);
            }
            this.LoadPropertiesCore(xml);
        }
    }

    public class SeparatedFieldsAnalyser : FieldAnalyser
    {
        [XmlElem]
        public string Separator { get; set; }
        [XmlElem]
        public bool IsRegex { get; set; }

        [XmlElem]
        public string ColNameTemplate { get; set; }

        public class Field
        {
            [XmlElem]
            public string Name { get; set; }
            [XmlElem]
            public int Position { get; set; }
        }
        public List<Field> Fields = new List<Field>();

        public override DataRecord AnalyseRecord(string data)
        {
            DataRecord res = new DataRecord();
            int index = 0;
            Dictionary<int, string> fieldNames = Fields.ToDictionary(fld => fld.Position, fld => fld.Name);
            foreach (string item in TextTool.SplitBy(data, Separator, IsRegex))
            {
                if (fieldNames.ContainsKey(index))
                {
                    res.Fields.Add(new DataRecord.Field { Name = fieldNames[index], Value = item });
                }
                else
                {
                    if (ColNameTemplate != null && ColNameTemplate.Contains("#COLINDEX#"))
                    {
                        res.Fields.Add(new DataRecord.Field
                        {
                            Name = ColNameTemplate.Replace("#COLINDEX#", index.ToString()),
                            Value = item
                        });
                    }
                }
                index++;
            }
            return res;
        }

        public override void SaveToXml(XmlElement xml)
        {
            xml.SetAttribute("type", "separated");
            this.SavePropertiesCore(xml);
            foreach (var fld in Fields)
            {
                fld.SaveProperties(xml.AddChild("Field"));
            }
        }

        public SeparatedFieldsAnalyser() { }

        public SeparatedFieldsAnalyser(XmlElement xml)
        {
            this.LoadPropertiesCore(xml);
            foreach (XmlElement x in xml.SelectNodes("Field"))
            {
                var fld = new Field();
                fld.LoadProperties(x);
                Fields.Add(fld);
            }
        }
    }

    public class WholeLineAnalyser : FieldAnalyser
    {
        [XmlAttrib("field")]
        public string FieldName { get; set; }

        public override DataRecord AnalyseRecord(string data)
        {
            DataRecord res = new DataRecord();
            res.Fields.Add(new DataRecord.Field { Name = String.IsNullOrEmpty(FieldName) ? "field" : FieldName, Value = data });
            return res;
        }

        public override void SaveToXml(XmlElement xml)
        {
            xml.SetAttribute("type", "line");
            this.SavePropertiesCore(xml);
        }

        public WholeLineAnalyser() { }

        public WholeLineAnalyser(XmlElement xml)
        {
            this.LoadPropertiesCore(xml);
        }
    }

    public class ScriptAnalyser : FieldAnalyser
    {
        PythonEngine m_engine = new PythonEngine();
        string m_code;

        [XmlElem]
        public string Code
        {
            get { return m_code; }
            set
            {
                m_code = value;
                try { m_compiled = m_engine.Compile(m_code); }
                catch { m_compiled = null; }
            }
        }

        CompiledCode m_compiled;

        public ScriptAnalyser()
        {
            ScriptingEnv.InitializeEngine(m_engine);
        }

        public override DataRecord AnalyseRecord(string data)
        {
            DataRecord res = new DataRecord();
            if (m_compiled != null)
            {
                m_engine.Globals["data"] = data;
                m_engine.Globals["add_field"] = (Action<string, string>)res.AddField;
                m_compiled.Execute();
            }
            return res;
        }

        public override void SaveToXml(XmlElement xml)
        {
            xml.SetAttribute("type", "script");
            this.SavePropertiesCore(xml);
        }

        public ScriptAnalyser(XmlElement xml)
            : this()
        {
            this.LoadPropertiesCore(xml);
        }
    }
}
