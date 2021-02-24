using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.mysql
{
    public class MySqlQuerySplitter : QuerySplitterBase
    {
        bool m_isString;
        bool m_isComment;
        char m_stringQuote;
        string m_delimiter = ";";

        public MySqlQuerySplitter(ISqlDialect dialect)
            : base(dialect)
        {
        }

        protected override string CurrentDelimiter { get { return m_delimiter; } }

        protected override void ProcessLine(string line)
        {
            if (AllowGoSeparator && !m_isString && !m_isComment && StringBeginsWithWord(line, "GO"))
            {
                YieldCurrentQuery();
                return;
            }
            else if (!m_isString && !m_isComment && StringBeginsWithWord(line, "DELIMITER"))
            {
                m_delimiter = line.Substring("DELIMITER".Length).Trim();
                YieldItem(new SplitQueryItem
                {
                    Data = line,
                    StartLine = m_startLine,
                    Delimiter = null // send NULL delimiter - this query should not be delimiter
                });
            }
            else
            {
                int qstart = 0;
                for (int i = 0; i < line.Length; i++)
                {
                    if (!m_isComment && !m_isString && i + 2 <= line.Length && line[i] == '-' && line[i + 1] == '-')
                    {
                        // line comment
                        if (StripComments)
                        {
                            AddToCurrentQuery(line.Substring(qstart, i - qstart));
                            AddToCurrentQuery(" ");
                        }
                        else
                        {
                            AddToCurrentQuery(line.Substring(qstart));
                            AddToCurrentQuery("\n");
                        }
                        return;
                    }
                    if (!m_isComment && !m_isString && i + 2 <= line.Length && line[i] == '/' && line[i + 1] == '*')
                    {
                        m_isComment = true;
                    }
                    if (m_isComment && i + 2 <= line.Length && line[i] == '*' && line[i + 1] == '/')
                    {
                        m_isComment = false;
                    }
                    if (m_isString)
                    {
                        if (line[i] == '\\') // next char will not change state
                        {
                            i++;
                            continue;
                        }
                        if (line[i] == m_stringQuote)
                        {
                            m_isString = false;
                            m_stringQuote = '\0';
                        }
                    }
                    else
                    {
                        if (!m_isComment && (line[i] == '\'' || line[i] == '"' || line[i] == '`'))
                        {
                            m_stringQuote = line[i];
                            m_isString = true;
                        }
                    }
                    if (!m_isString && !m_isComment && IndexStartsWith(line, i, m_delimiter))
                    {
                        AddToCurrentQuery(line.Substring(qstart, i - qstart));
                        YieldCurrentQuery();
                        qstart = i + m_delimiter.Length;
                    }
                }
                AddToCurrentQuery(line.Substring(qstart));
                AddToCurrentQuery("\n");
            }
        }
    }
}
