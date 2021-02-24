using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data;
using Npgsql;
using System.Globalization;

namespace Plugin.postgre
{
    public class PostgreDDA : DialectDataAdapterBase
    {
        public PostgreDDA(ISqlDialect dialect)
            : base(dialect)
        {
        }

        public override SqlLiteralFormatter CreateLiteralFormatter()
        {
            return new PostgreLiteralFormatter(m_dialect);
        }

        public override string GetFulltextSearchExpr(string expr, string substring, FulltextSearchParams pars)
        {
            substring = substring.Replace("@", "@@").Replace("%", "@%").Replace("_", "@_");
            return String.Format("(lower({0} || '') LIKE lower({1}) ESCAPE '@')", expr, this.GetSqlLiteral(pars.LikePrefix + substring + pars.LikePostfix));
        }

        public override IBedReader AdaptReader(IDataReader reader)
        {
            return new PostgreSqlDataReaderAdapter(reader, m_dialect);
        }
    }

    public class PostgreLiteralFormatter : SqlLiteralFormatter
    {
        public PostgreLiteralFormatter(ISqlDialect dialect)
            : base(dialect)
        {
        }

        public override void SetBoolean(bool value)
        {
            m_text = value ? "true" : "false";
        }

        public override void SetByteArray(byte[] value)
        {
            m_text = "'" + StringTool.EncodeOct(value, "\\\\") + "'::bytea";
        }
    }

    public class PostgreSqlDataReaderAdapter : DataReaderAdapter
    {
        NpgsqlDataReader m_myReader;
        public PostgreSqlDataReaderAdapter(IDataReader reader, ISqlDialect dialect)
            : base(reader, dialect)
        {
            m_myReader = reader as NpgsqlDataReader;
        }

        public override void ReadValue(int i)
        {
            if (m_myReader != null)
            {
                var type = m_myReader.GetFieldNpgsqlDbType(i);
                if ((type & NpgsqlTypes.NpgsqlDbType.Array) != 0)
                {
                    object obj = m_myReader.GetValue(i);
                    if (obj == DBNull.Value)
                    {
                        this.SetNull();
                        return;
                    }
                    var ar = (Array)m_myReader.GetValue(i);
                    var sb = new StringBuilder();
                    int[] indices = new int[ar.Rank];
                    WriteArrayDimension(sb, ar, indices, 0);
                    this.ReadFrom(sb.ToString());
                    return;
                }
            }
            base.ReadValue(i);
        }

        private void WriteArrayDimension(StringBuilder sb, Array ar, int[] indices, int dim)
        {
            int len = ar.GetLength(dim);
            sb.Append("{");
            for (int i = 0; i < len; i++)
            {
                indices[dim] = i;
                if (i > 0) sb.Append(",");
                if (dim < ar.Rank - 1)
                {
                    WriteArrayDimension(sb, ar, indices, dim + 1);
                }
                else
                {
                    object o = ar.GetValue(indices);
                    sb.Append(String.Format(CultureInfo.InvariantCulture, "{0}", o));
                    //if (o.GetType().IsNumberType())
                    //{
                    //    sb.Append(String.Format(CultureInfo.InvariantCulture, "{0}", o));
                    //}
                    //else
                    //{
                    //    sb.Append("\'" + o.ToString().Replace("\'", "\\\'") + "\'");
                    //}
                }
            }
            sb.Append("}");
        }
    }
}
