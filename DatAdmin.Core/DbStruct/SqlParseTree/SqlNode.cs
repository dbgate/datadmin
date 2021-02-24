using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections.ObjectModel;

namespace DatAdmin
{
    public interface ISymbolEnumerator
    {
        void EnumSymbol(SymbolPosition symbol, SqlNode owningNode);
    }

    public class SqlEqualityTestProps
    {
    }

    public abstract class SqlNode
    {
        public List<SqlNode> m_children = new List<SqlNode>();
        public ReadOnlyCollection<SqlNode> Children { get; private set; }
        SqlNode m_parent;

        public SqlNode()
        {
            Children = new ReadOnlyCollection<SqlNode>(m_children);
        }
        public SqlNode Parent
        {
            get { return m_parent; }
            set
            {
                if (m_parent != null) m_parent.m_children.Remove(this);
                m_parent = value;
                if (m_parent != null) m_parent.m_children.Add(this);
            }
        }
        public virtual void SaveToXml(XmlElement xml) { }
        public abstract void GenerateSql(ISqlDumper dmp);
        public abstract void EnumSymbols(ISymbolEnumerator en);
    }

    public class SqlLeafNode : SqlNode
    {
        public SymbolPosition Original;

        public override void GenerateSql(ISqlDumper dmp)
        {
            if (Original != null) dmp.WriteRaw(Original.GetOriginalToken());
            else throw new NotImplementedError("DAE-00117");
        }

        public SqlLeafNode(SymbolPosition original)
        {
            this.Original = original;
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            en.EnumSymbol(Original, this);
        }
        public SqlLeafNode() { }

    }
}
