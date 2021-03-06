using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.Common;
using System.Collections;

namespace DatAdmin
{
    public class SqlOutputStream : ISqlOutputStream
    {
        ISqlDialect m_dialect;
        TextWriter m_tw;
        SqlFormatProperties m_props;
        bool m_wasend = false;
        string m_delimiterOverride = null;

        public SqlOutputStream(ISqlDialect dialect, TextWriter tw, SqlFormatProperties props)
        {
            m_dialect = dialect;
            m_props = props;
            m_tw = tw;
        }

        #region ISqlOutputStream Members

        public void Write(string text)
        {
            SeparatorIfNeeded();
            m_tw.Write(text);
        }

        public void EndCommand()
        {
            m_wasend = true;
        }

        public void OverrideCommandDelimiter(string delimiter)
        {
            SeparatorIfNeeded();
            m_delimiterOverride = delimiter;
        }

        #endregion

        private void SeparatorIfNeeded()
        {
            if (m_wasend)
            {
                if (m_delimiterOverride != null)
                {
                    m_tw.Write(m_delimiterOverride);
                }
                else if (!String.IsNullOrEmpty(m_props.CommandSeparator))
                {
                    m_tw.Write(m_props.CommandSeparator.ReplaceCEscapes());
                }
                else
                {
                    if (m_dialect.DialectCaps.MultiCommand)
                    {
                        m_tw.Write(";\n");
                    }
                    else
                    {
                        m_tw.Write("\nGO\n");
                    }
                }
                m_wasend = false;
            }
        }
    }

    public class PolyStringSqOutputStream : ISqlOutputStream
    {
        StringBuilder m_curline = new StringBuilder();
        List<string> m_lines = new List<string>();

        public List<string> Lines { get { return m_lines; } }

        #region ISqlOutputStream Members

        public void Write(string text)
        {
            m_curline.Append(text);
        }

        public void EndCommand()
        {
            m_lines.Add(m_curline.ToString());
            m_curline = new StringBuilder();
        }

        public void OverrideCommandDelimiter(string delimiter)
        {
        }

        #endregion
    }

    public class ConnectionSqlOutputStream : ISqlOutputStream
    {
        DbConnection m_conn;
        ISqlDialect m_dialect;
        DbTransaction m_trans;
        StringBuilder m_curCommand = new StringBuilder();

        public ConnectionSqlOutputStream(DbConnection conn, DbTransaction trans, ISqlDialect dialect)
        {
            m_conn = conn;
            m_dialect = dialect;
            m_trans = trans;
        }

        #region ISqlOutputStream Members

        public void OverrideCommandDelimiter(string delimiter) { }

        public void Write(string text)
        {
            m_curCommand.Append(text);
        }

        public void EndCommand()
        {
            string sql = m_curCommand.ToString().Trim();
            m_curCommand = new StringBuilder();
            m_conn.ExecuteNonQuery(sql, m_dialect, m_trans, null);
        }

        #endregion
    }
}
