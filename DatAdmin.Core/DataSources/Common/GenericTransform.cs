using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using IronPython.Hosting;
using System.Collections;
using System.Xml;
using System.ComponentModel;
using System.Drawing.Design;
using System.ComponentModel.Design;

namespace DatAdmin
{
    [RowTransform(Name = "generic", SupportsDirectUse = false)]
    public class GenericTransform : RowTransformBase
    {
        ITableStructure m_source;
        ITableStructure m_target;
        List<ColExpr> m_dstcols = new List<ColExpr>();
        PythonEngine m_engine;
        string m_script;
        int m_processedRows;

        bool m_hasPython;

        public GenericTransform(ITableStructure source, ITableStructure target, IEnumerable<ColExpr> dstcols)
        {
            m_source = source;
            m_target = target;
            m_dstcols.AddRange(dstcols);
            UpdateFlags();
        }

        private void UpdateFlags()
        {
            m_hasPython = false;
            foreach (var col in m_dstcols) if (col.Type is PythonColExprType) m_hasPython = true;
        }

        private void WantEngine()
        {
            if (m_engine != null) return;
            m_engine = new PythonEngine();
            ScriptingEnv.InitializeEngine(m_engine);
        }

        public GenericTransform() { }

        public static IEnumerable<ColExprType> GetColExprTypes()
        {
            yield return new ColumnColExprType();
            yield return new NullColExprType();
            yield return new ConstColExprType();
            yield return new PythonColExprType();
            yield return new RowNumberColExprType();
        }

        public static void GetColExprTypes(IList list)
        {
            foreach (var type in GetColExprTypes()) list.Add(type);
        }

        public abstract class ColExprType
        {
            public abstract object EvalExpr(IBedRecord record, string expr, PythonEngine engine, int rownum);
            public abstract string Name { get; }

            public override bool Equals(object obj)
            {
                return obj.GetType() == GetType();
            }

            public override int GetHashCode()
            {
                return GetType().GetHashCode();
            }
        }
        public class ColumnColExprType : ColExprType
        {
            public override string ToString()
            {
                return Texts.Get("s_column_value");
            }
            public override object EvalExpr(IBedRecord record, string expr, PythonEngine engine, int rownum)
            {
                return record.GetValue(expr);
            }
            public override string Name
            {
                get { return "column"; }
            }
        }
        public class ConstColExprType : ColExprType
        {
            public override string ToString()
            {
                return Texts.Get("s_fixed_text");
            }
            public override object EvalExpr(IBedRecord record, string expr, PythonEngine engine, int rownum)
            {
                return expr;
            }
            public override string Name
            {
                get { return "const"; }
            }
        }
        public class NullColExprType : ColExprType
        {
            public override string ToString()
            {
                return "NULL";
            }
            public override object EvalExpr(IBedRecord record, string expr, PythonEngine engine, int rownum)
            {
                return null;
            }
            public override string Name
            {
                get { return "null"; }
            }
        }
        public class PythonColExprType : ColExprType
        {
            public override string ToString()
            {
                return "Python expression";
            }
            public override object EvalExpr(IBedRecord record, string expr, PythonEngine engine, int rownum)
            {
                return engine.Evaluate(expr);
            }
            public override string Name
            {
                get { return "python"; }
            }
        }
        public class RowNumberColExprType : ColExprType
        {
            public override string ToString()
            {
                return Texts.Get("s_row_number");
            }
            public override object EvalExpr(IBedRecord record, string expr, PythonEngine engine, int rownum)
            {
                return rownum + 1;
            }
            public override string Name
            {
                get { return "rownum"; }
            }
        }
        public class ColExpr
        {
            public ColExprType Type { get; set; }
            public string Expression { get; set; }
            public string Name { get; set; }

            public static ColExpr Load(XmlElement xml)
            {
                ColExpr res = new ColExpr();
                res.Expression = xml.GetAttribute("expr");
                res.Name = xml.GetAttribute("name");
                switch (xml.GetAttribute("type"))
                {
                    case "column":
                        res.Type = new ColumnColExprType();
                        break;
                    case "python":
                        res.Type = new PythonColExprType();
                        break;
                    case "null":
                        res.Type = new NullColExprType();
                        break;
                    case "const":
                        res.Type = new ConstColExprType();
                        break;
                    case "rownum":
                        res.Type = new RowNumberColExprType();
                        break;
                    default:
                        throw new InternalError("DAE-00024 Unknown col expr type:" + xml.GetAttribute("type"));
                }
                return res;
            }

            public void Save(XmlElement xml)
            {
                xml.SetAttribute("name", Name);
                xml.SetAttribute("expr", Expression);
                xml.SetAttribute("type", Type.Name);
            }

            public DbTypeBase GetColType(ITableStructure source)
            {
                if (Type is ColumnColExprType)
                {
                    try
                    {
                        return source.Columns[Expression].DataType;
                    }
                    catch
                    {
                        return new DbTypeString();
                    }
                }
                return new DbTypeString();
            }
        }

        public override IBedRecord Transform(IBedRecord record)
        {
            if (m_hasPython)
            {
                WantEngine();
                m_engine.Globals["row"] = new DatAdmin.Scripting.Record(record);
                if (!m_script.IsEmpty()) m_engine.Execute(m_script);
            }

            object[] values = new object[m_dstcols.Count];
            for (int i = 0; i < m_dstcols.Count; i++)
            {
                values[i] = m_dstcols[i].Type.EvalExpr(record, m_dstcols[i].Expression, m_engine, m_processedRows);
            }
            m_processedRows++;
            return new ArrayDataRecord(m_target, values);
        }

        public override ITableStructure InputFormat
        {
            get { return m_source; }
        }

        public override ITableStructure OutputFormat
        {
            get { return m_target; }
        }

        public override void LoadFromXml(XmlElement xml, ITableStructure source, ITableStructure target)
        {
            m_source = source;
            m_target = target;

            if (xml.SelectSingleNode("Script") != null) m_script = xml.SelectSingleNode("Script").InnerText;

            TableStructure t = new TableStructure();
            foreach (XmlElement e in xml.SelectNodes("Column"))
            {
                ColExpr ce = ColExpr.Load(e);
                m_dstcols.Add(ce);
                t.AddColumn(new ColumnStructure
                {
                    ColumnName = ce.Name,
                    DataType = ce.GetColType(source)
                }, true);
            }
            m_target = t;
            UpdateFlags();
        }

        public override void SaveToXml(XmlElement xml)
        {
            xml.SetAttribute("type", "generic");
            foreach (var col in m_dstcols)
            {
                XmlElement e = xml.AddChild("Column");
                col.Save(e);
            }
            if (!m_script.IsEmpty()) xml.AddChild("Script").InnerText = m_script;
        }

        [Editor(typeof(SyntaxEditor), typeof(UITypeEditor))]
        [SyntaxEditorLanguage(CodeLanguage.Python)]
        public string Script
        {
            get { return m_script; }
            set { m_script = value; }
        }

        [Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
        public List<ColExpr> DestCols { get { return m_dstcols; } }
    }
}
