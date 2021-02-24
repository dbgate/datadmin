using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;

namespace DatAdmin
{
    public class TunnelParameter : DbParameter
    {
        string m_name;
        int m_size;
        bool m_nullMapping;
        object m_value;
        DataRowVersion m_rowVersion = DataRowVersion.Current;
        string m_sourceColumn;

        public override DbType DbType
        {
            get { return DbType.String; }
            set { }
        }

        public override ParameterDirection Direction
        {
            get { return ParameterDirection.Input; }
            set { }
        }

        public override bool IsNullable
        {
            get { return true; }
            set { }
        }

        public override string ParameterName
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public override void ResetDbType()
        {
        }

        public override int Size
        {
            get { return m_size; }
            set { m_size = value; }
        }

        public override string SourceColumn
        {
            get { return m_sourceColumn; }
            set { m_sourceColumn = value; }
        }

        public override bool SourceColumnNullMapping
        {
            get { return m_nullMapping; }
            set { m_nullMapping = value; }
        }

        public override DataRowVersion SourceVersion
        {
            get { return m_rowVersion; }
            set { m_rowVersion = value; }
        }

        public override object Value
        {
            get { return m_value; }
            set { m_value = value; }
        }
    }
}
