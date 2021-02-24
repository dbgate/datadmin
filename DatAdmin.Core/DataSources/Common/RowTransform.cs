using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.Linq;
using System.ComponentModel;

namespace DatAdmin
{
    public abstract class RowTransformBase : AddonBase, IRowTransform
    {
        #region IRowTransform Members

        public abstract IBedRecord Transform(IBedRecord record);
        public abstract ITableStructure InputFormat { get; }
        public abstract ITableStructure OutputFormat { get; }
        public abstract void LoadFromXml(XmlElement xml, ITableStructure source, ITableStructure target);

        #endregion

        [Browsable(false)]
        public override AddonType AddonType
        {
            get { return RowTransformAddonType.Instance; }
        }
    }

    [RowTransform(Name = "permute", SupportsDirectUse=false)]
    public class PermuteTransform : RowTransformBase
    {
        ITableStructure m_source;
        ITableStructure m_target;
        List<int> m_dstIndexes = new List<int>();

        public PermuteTransform() { }

        //public PermuteTransform(ITableStructure source, ITableStructure target)
        //{
        //    m_source = source;
        //    m_target = target;
        //    foreach (IColumnStructure col in m_target.Columns)
        //    {
        //        m_dstIndexes.Add(m_source.Columns.GetIndex(col.ColumnName));
        //    }
        //}

        public PermuteTransform(ITableStructure source, ITableStructure target, IEnumerable<int> dstIndexes)
        {
            m_source = source;
            m_target = target;
            m_dstIndexes.AddRange(dstIndexes);
        }

        public override IBedRecord Transform(IBedRecord record)
        {
            object[] values = new object[m_target.Columns.Count];
            for (int i = 0; i < m_dstIndexes.Count; i++)
            {
                values[i] = record.GetValue(m_dstIndexes[i]);
            }
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
            if (m_target == null)
            {
                var t = new TableStructure();
                foreach (XmlElement e in xml.SelectNodes("Column"))
                {
                    int colindex = m_source.Columns.GetIndex(e.GetAttribute("src"));
                    m_dstIndexes.Add(colindex);
                    var newcol = new ColumnStructure(m_source.Columns[colindex]);
                    newcol.ColumnName = e.GetAttribute("dst");
                    t.AddColumn(newcol, true);
                }
                m_target = t;
            }
            else
            {
                var t = new TableStructure();
                foreach (XmlElement e in xml.SelectNodes("Column"))
                {
                    string colname = e.GetAttribute("src");
                    string dstcolname = e.GetAttribute("dst");
                    int pos = m_source.Columns.GetIndex(colname);
                    if (pos < 0) throw new InternalError(String.Format("DAE-00025 Error transforming column {0}, column not found in source table", colname));
                    IColumnStructure dstcol = m_target.Columns.FirstOrDefault(col => col.ColumnName == dstcolname);
                    if (dstcol == null) throw new InternalError(String.Format("DAE-00026 Error transforming column {0}, column not found in destination table", dstcolname));
                    m_dstIndexes.Add(pos);
                    t.AddColumn(dstcol, true);
                }
                m_target = t;
            }
        }

        public override void SaveToXml(XmlElement xml)
        {
            if (IsIdentity)
            {
                xml.SetAttribute("type", "identity");
            }
            else
            {
                xml.SetAttribute("type", "permute");
                for (int i = 0; i < m_dstIndexes.Count; i++)
                {
                    XmlElement e = xml.AddChild("Column");
                    e.SetAttribute("src", m_source.Columns[m_dstIndexes[i]].ColumnName);
                    if (m_target != null) e.SetAttribute("dst", m_target.Columns[i].ColumnName);
                }
            }
        }

        public bool IsIdentity
        {
            get
            {
                if (m_source.Columns.Count != m_dstIndexes.Count) return false;
                for (int i = 0; i < m_dstIndexes.Count; i++) if (m_dstIndexes[i] != i) return false;
                return true;
            }
        }

        [TypeConverter(typeof(ShortIntListTypeConverter))]
        public List<int> DestIndexes { get { return m_dstIndexes; } }

        public override string ToString()
        {
            if (IsIdentity) return "(identity)";
            return String.Join(", ", (from i in m_dstIndexes select i.ToString()).ToArray());
        }
    }

    [RowTransform(Name = "identity", SupportsDirectUse = false)]
    public class IdentityTransform : RowTransformBase
    {
        TableStructure m_inputFormat;
        TableStructure m_outputFormat;

        public IdentityTransform(ITableStructure rowFormat)
        {
            m_outputFormat = m_inputFormat = new TableStructure(rowFormat);
        }

        public IdentityTransform() { }

        public override IBedRecord Transform(IBedRecord record)
        {
            return record;
        }

        public override ITableStructure InputFormat
        {
            get { return m_inputFormat; }
        }

        public override ITableStructure OutputFormat
        {
            get { return m_outputFormat; }
        }

        public override void LoadFromXml(XmlElement xml, ITableStructure source, ITableStructure target)
        {
            if (source == null) throw new ArgumentNullException("source", "DAE-00226 source is null");
            m_inputFormat = new TableStructure(source);
            m_outputFormat = target != null ? new TableStructure(target) : m_inputFormat;
            if (target != null)
            {
                int colcount = Math.Min(m_inputFormat.Columns.Count, m_outputFormat.Columns.Count);
                while (m_inputFormat.Columns.Count > colcount) m_inputFormat._Columns.RemoveAt(m_inputFormat.Columns.Count - 1);
                while (m_outputFormat.Columns.Count > colcount) m_outputFormat._Columns.RemoveAt(m_outputFormat.Columns.Count - 1);
            }
        }
        public override void SaveToXml(XmlElement xml)
        {
            xml.SetAttribute("type", "identity");
        }

        public override string ToString()
        {
            return "(identity)";
        }
    }
}
