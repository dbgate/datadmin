using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Plugin.Csv
{
    public class CsvWriter : IDisposable
    {
        TextWriter m_stream;
        char m_delimiter = ',';
        char m_quote = '"';
        char m_escape = '"';
        char m_comment = '#';
        string m_lineEnds = "\r\n";
        bool m_hasHeaders = true;
        CsvQuotingMode m_qmode;

        public CsvWriter(TextWriter stream, bool hasHeaders, char delimiter, char quote, char escape, char comment, CsvQuotingMode qmode, string lineEnds)
        {
            m_stream = stream;
            m_hasHeaders = hasHeaders;
            m_lineEnds = lineEnds;
            m_delimiter = delimiter;
            m_quote = quote;
            m_escape = escape;
            m_comment = comment;
            m_qmode = qmode;
        }

        public void WriteRow(IEnumerable<string> cols)
        {
            bool wascol = false;
            foreach (string col in cols)
            {
                if (wascol) m_stream.Write(m_delimiter);
                bool quote = false;
                switch (m_qmode)
                {
                    case CsvQuotingMode.Always:
                        quote = true;
                        break;
                    case CsvQuotingMode.AlwaysExceptNumbers:
                        if (col != null)
                        {
                            foreach (char ch in col)
                            {
                                if (ch <= '0' || ch >= '9')
                                {
                                    quote = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            quote = true;
                        }
                        break;
                    case CsvQuotingMode.OnlyIfNecessary:
                        if (col != null)
                        {
                            foreach (char ch in col)
                            {
                                if (ch <= ' ' || ch == m_delimiter || ch == m_quote || ch == m_escape || ch == m_comment)
                                {
                                    quote = true;
                                    break;
                                }
                            }
                        }
                        break;
                }
                if (quote)
                {
                    m_stream.Write(m_quote);
                    foreach (char ch in col)
                    {
                        if (ch == m_quote) m_stream.Write(m_escape);
                        m_stream.Write(ch);
                    }
                    m_stream.Write(m_quote);
                }
                else
                {
                    m_stream.Write(col);
                }
                wascol = true;
            }
            m_stream.Write(m_lineEnds);
        }

        #region IDisposable Members

        public void Dispose()
        {
            m_stream.Dispose();
        }

        #endregion
    }
}
