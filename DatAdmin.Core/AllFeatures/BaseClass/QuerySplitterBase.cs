using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public class QuerySplitterBase : IQuerySplitter
    {
        protected readonly ISqlDialect m_dialect;
        protected int m_curLine;
        protected int m_startLine;
        protected StringBuilder m_curQuery;
        protected List<SplitQueryItem> m_yieldItems = new List<SplitQueryItem>();
        
        public QuerySplitterBase(ISqlDialect dialect)
        {
            m_dialect = dialect;
        }

        protected virtual string CurrentDelimiter { get { return null; } }

        protected static bool StringBeginsWithWord(string value, string with)
        {
            if (!value.StartsWith(with, StringComparison.InvariantCultureIgnoreCase)) return false;
            return value.Length == with.Length || Char.IsWhiteSpace(value[with.Length]) || Char.IsSymbol(value[with.Length]);
        }

        protected static bool IndexStartsWith(string line, int start, string delimiter)
        {
            for (int i = 0; i < delimiter.Length; i++)
            {
                if (start + i >= line.Length) return false;
                if (line[start + i] != delimiter[i]) return false;
            }
            return true;
        }

        protected void YieldItem(SplitQueryItem item)
        {
            m_yieldItems.Add(item);
        }

        protected virtual void ProcessLine(string line)
        {
            if (StringBeginsWithWord(line, "GO"))
            {
                YieldCurrentQuery();
                return;
            }
            else
            {
                AddToCurrentQuery(line + "\n");
            }
        }

        protected void YieldCurrentQuery()
        {
            string res = m_curQuery.ToString();
            if (res.Trim() != "") YieldItem(new SplitQueryItem { Data = res, StartLine = m_startLine, Delimiter = CurrentDelimiter });
            m_curQuery = new StringBuilder();
            m_startLine = m_curLine + 1;
        }

        protected void AddToCurrentQuery(string data)
        {
            m_curQuery.Append(data);
        }

        #region IQuerySplitter Members

        public bool AllowGoSeparator { get; set; }
        public bool StripComments { get; set; }

        public virtual IEnumerable<SplitQueryItem> Run(TextReader reader)
        {
            m_curQuery = new StringBuilder();
            m_curLine = -1;
            m_startLine = 0;

            for (; ; )
            {
                string line = reader.ReadLine();
                m_curLine++;
                if (line == null) break;
                ProcessLine(line);
                foreach (var item in m_yieldItems) yield return item;
                m_yieldItems.Clear();
            }
            string res2 = m_curQuery.ToString();
            if (res2.Trim() != "") yield return new SplitQueryItem { Data = res2, StartLine = m_startLine };
        }

        #endregion
    }
}
