using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.oracle
{
    public class OracleQuerySplitter : QuerySplitterBase
    {
        bool m_isString;
        bool m_isComment;
        char m_stringQuote;
        int m_blockLevel;

        public OracleQuerySplitter(ISqlDialect dialect)
            : base(dialect)
        {
        }

        protected override void ProcessLine(string line)
        {
            if (line.Trim() == "/" || line.Trim().ToUpper() == "GO")
            {
                YieldCurrentQuery();
                return;
            }
            else
            {
                //AddToCurrentQuery(line + "\n");

                int qstart = 0;
                for (int i = 0; i < line.Length; )
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
                        i++;
                        continue;
                    }
                    if (m_isComment && i + 2 <= line.Length && line[i] == '*' && line[i + 1] == '/')
                    {
                        m_isComment = false;
                        i++;
                        continue;
                    }
                    if (m_isString)
                    {
                        if (line[i] == m_stringQuote && i + 2 <= line.Length && line[i + 1] == m_stringQuote) // next char will not change state
                        {
                            i += 2;
                            continue;
                        }
                        if (line[i] == m_stringQuote)
                        {
                            i++;
                            m_isString = false;
                            m_stringQuote = '\0';
                            continue;
                        }
                    }
                    else
                    {
                        if (!m_isComment && (line[i] == '\'' || line[i] == '"'))
                        {
                            m_stringQuote = line[i];
                            m_isString = true;
                            i++;
                            continue;
                        }
                    }
                    if (!m_isComment && !m_isString && IsWordCI("BEGIN", line, i))
                    {
                        i += 5;
                        m_blockLevel++;
                        continue;
                    }
                    if (!m_isComment && !m_isString && IsWordCI("END", line, i))
                    {
                        i += 3;
                        m_blockLevel--;
                        if (m_blockLevel == 0)
                        {
                            // handle ';' after END
                            while (i < line.Length && Char.IsWhiteSpace(line, i)) i++;
                            if (i < line.Length && line[i] == ';')
                            {
                                AddToCurrentQuery(line.Substring(qstart, i - qstart));
                                AddToCurrentQuery(";");
                                YieldCurrentQuery();
                                i++;
                                qstart = i;
                            }
                        }
                        continue;
                    }
                    if (!m_isString && !m_isComment && m_blockLevel == 0 && line[i] == ';')
                    {
                        AddToCurrentQuery(line.Substring(qstart, i - qstart));
                        YieldCurrentQuery();
                        i++;
                        qstart = i;
                        continue;
                    }
                    i++;
                }
                AddToCurrentQuery(line.Substring(qstart));
                AddToCurrentQuery("\n");
            }
        }

        private bool IsWordCI(string word, string line, int start)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (i + start >= line.Length) return false;
                if (Char.ToLower(line[i + start]) != Char.ToLower(word[i])) return false;
            }
            if (start >= 1 && Char.IsLetterOrDigit(line[start - 1])) return false;
            if (start + word.Length + 1 < line.Length && Char.IsLetterOrDigit(line[start + word.Length + 1])) return false;
            return true;
        }
    }
}
