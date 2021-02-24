using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using System.Drawing;

namespace DatAdmin
{
    public class CodeCompletionProvider : ICompletionDataProvider
    {
        Form m_parentForm;
        IDatabaseSource m_conn;
        SqlEditorAnalyser m_anal;
        ISqlDialect m_dialect;
        ImageCache m_imgCache;
        char m_firstChar;
        string m_presel;
        CodeCompletionSettings m_settings;

        public CodeCompletionProvider(Form parentForm, IDatabaseSource conn, SqlEditorAnalyser anal, ImageCache imgCache, char firstChar, TextAreaControl textArea, CodeCompletionSettings settings)
        {
            m_parentForm = parentForm;
            m_conn = conn;
            m_anal = anal;
            m_dialect = m_conn.Dialect;
            m_imgCache = imgCache;
            m_firstChar = firstChar;
            m_settings = settings;

            if (m_firstChar == '\0')
            {
                int col = textArea.Caret.Column;
                string line = textArea.Document.GetText(textArea.Document.GetLineSegment(textArea.Caret.Line));
                var sb = new StringBuilder();
                while (col >= 1 && (Char.IsLetterOrDigit(line[col - 1]) || line[col - 1] == '_'))
                {
                    sb.Insert(0, line[col - 1]);
                    col--;
                }
                if (sb.Length > 0) m_presel = sb.ToString();
            }
        }

        public ImageList ImageList
        {
            get
            {
                return m_imgCache.Images;
            }
        }

        public string PreSelection
        {
            get { return m_presel; }
        }

        public int DefaultIndex
        {
            get
            {
                return -1;
            }
        }

        public CompletionDataProviderKeyResult ProcessKey(char key)
        {
            if (char.IsLetterOrDigit(key) || key == '_')
            {
                return CompletionDataProviderKeyResult.NormalKey;
            }
            else
            {
                // key triggers insertion of selected items
                return CompletionDataProviderKeyResult.InsertionKey;
            }
        }

        /// <summary>
        /// Called when entry should be inserted. Forward to the insertion action of the completion data.
        /// </summary>
        public bool InsertAction(ICompletionData data, TextArea textArea, int insertionOffset, char key)
        {
            textArea.Caret.Position = textArea.Document.OffsetToPosition(insertionOffset);
            var res = data.InsertAction(textArea, key);
            return res;
        }

        //private void DoLoadTables()
        //{
        //    m_tables = new List<NameWithSchema>(m_conn.LoadFullTableNames(true));
        //}

        //private void LoadedTables(IAsyncResult async)
        //{
        //}

        private string GetShortName(string name)
        {
            if (m_settings.QuoteIdentifiers) return m_dialect.QuoteIdentifier(name);
            return name;
        }

        private string GetFullName(NameWithSchema name)
        {
            if (m_settings.AddSchema && !name.Schema.IsEmpty())
            {
                return GetShortName(name.Schema) + "." + GetShortName(name.Name);
            }
            return GetShortName(name.Name);
        }

        public ICompletionData[] GenerateCompletionData(string fileName, TextArea textArea, char charTyped)
        {
            var res = new List<ICompletionData>();
            int tblindex = m_imgCache.GetImageIndex(StdIcons.table);
            int vindex = m_imgCache.GetImageIndex(StdIcons.view);

            var context = m_anal.GetContext(textArea.Caret.Line, textArea.Caret.Column);
            switch (context)
            {
                case SqlEditorAnalyser.CodeContext.Table:
                    {
                        var prefixName = FindPrefix(textArea);
                        if (prefixName == null) // do not handle table prefixes
                        {
                            foreach (var tbl in m_anal.TableNames)
                            {
                                res.Add(new SqlCompletionData(tbl.Name, tbl.ToString(), tblindex, GetFullName(tbl)));
                            }
                            foreach (var tbl in m_anal.ViewNames)
                            {
                                res.Add(new SqlCompletionData(tbl.Name, tbl.ToString(), vindex, GetFullName(tbl)));
                            }
                        }
                    }
                    break;
                case SqlEditorAnalyser.CodeContext.Column:
                case SqlEditorAnalyser.CodeContext.ColumnWithoutQualifier:
                    {
                        var prefixName = FindPrefix(textArea);
                        int colindex = m_imgCache.GetImageIndex(StdIcons.column);
                        int starindex = m_imgCache.GetImageIndex(StdIcons.star_blue);
                        bool qual = context == SqlEditorAnalyser.CodeContext.Column;
                        
                        if (prefixName != null)
                        {
                            var used = m_anal.FindUsedMatch(prefixName);
                            if (used != null)
                            {
                                string alltext = String.Format("all ({0})", used.FullName);
                                string allins = "";
                                foreach (var col in used.Structure.Columns)
                                {
                                    if (allins.Length > 0)
                                    {
                                        allins += ", ";
                                        allins += (qual ? prefixName.ToString() + "." : "") + GetShortName(col.ColumnName);
                                    }
                                    else
                                    {
                                        allins += GetShortName(col.ColumnName);
                                    }
                                }
                                res.Add(new SqlCompletionData(alltext, allins, starindex, allins));

                                foreach (var col in used.Structure.Columns)
                                {
                                    string text = String.Format("{0} ({1})", col.ColumnName, used.FullName);
                                    string ins = GetShortName(col.ColumnName);
                                    res.Add(new SqlCompletionData(text, text, colindex, ins));
                                }
                            }
                        }
                        else
                        {
                            foreach (var tbl in m_anal.UsedTables)
                            {
                                string alltext = String.Format("all ({0})", (tbl.Alias ?? tbl.FullName.ToString()));
                                string allins = "";
                                foreach (var col in tbl.Structure.Columns)
                                {
                                    if (allins.Length > 0) allins += ", ";
                                    string ins = (qual ? (tbl.Alias ?? GetFullName(tbl.FullName)) + "." : "") + GetShortName(col.ColumnName);
                                    allins += ins;
                                }
                                res.Add(new SqlCompletionData(alltext, allins, starindex, allins));
                                if (qual)
                                {
                                    int imgindex = m_anal.ViewNames.IndexOf(tbl.FullName) >= 0 ? vindex : tblindex;
                                    res.Add(new SqlCompletionData(tbl.Alias ?? tbl.FullName.Name, tbl.Alias ?? tbl.FullName.ToString(), imgindex, tbl.Alias ?? GetFullName(tbl.FullName)));
                                }

                                foreach (var col in tbl.Structure.Columns)
                                {
                                    string text = String.Format("{0} ({1}{2})", col.ColumnName, tbl.FullName, tbl.Alias != null ? ":" + tbl.Alias : "");
                                    string ins = (qual ? (tbl.Alias ?? GetFullName(tbl.FullName)) + "." : "") + GetShortName(col.ColumnName);
                                    res.Add(new SqlCompletionData(text, text, colindex, ins));
                                }
                            }
                        }
                    }
                    break;
            }
            return res.ToArray();
        }

        private DepsName FindPrefix(TextArea textArea)
        {
            string line = textArea.Document.GetText(textArea.Document.GetLineSegment(textArea.Caret.Line));
            if (m_firstChar != '\0') line = line.Insert(textArea.Caret.Column, new String(m_firstChar, 1));
            var par = new ReverseLineParser
            {
                m_dialect = m_dialect,
                m_line = line,
                m_pos = textArea.Caret.Column,
            };
            return par.ParseName();
        }
    }

    internal class ReverseLineParser
    {
        internal int m_pos;
        internal string m_line;
        internal ISqlDialect m_dialect;

        internal DepsName ParseName()
        {
            JumpWhite();
            if (Current != '.') return null;
            ConsumeChar();
            string id = TryReadIdent();
            if (id == null) return null;
            var res = new DepsName();
            res.Components.Add(id);
            JumpWhite();
            while (Current == '.')
            {
                ConsumeChar();
                string id2 = TryReadIdent();
                if (id2 == null) break;
                res.Components.Insert(0, id2);
                JumpWhite();
            }
            return res;
        }

        private string TryReadIdent()
        {
            JumpWhite();
            if (Current == m_dialect.QuoteIdentEnd)
            {
                ConsumeChar();
                var sb = new StringBuilder();
                while (m_pos >= 0 && Current != m_dialect.QuoteIdentBegin)
                {
                    sb.Insert(0, Current);
                    ConsumeChar();
                }
                if (Current == m_dialect.QuoteIdentBegin) ConsumeChar();
                return sb.ToString();
            }
            if (Char.IsLetterOrDigit(Current) || Current == '_')
            {
                var sb = new StringBuilder();
                while (Char.IsLetterOrDigit(Current) || Current == '_')
                {
                    sb.Insert(0, Current);
                    ConsumeChar();
                }
                return sb.ToString();
            }
            return null;
        }

        private void ConsumeChar() { if (m_pos >= 0) m_pos--; }

        private char Current
        {
            get
            {
                if (m_pos < 0 || m_pos >= m_line.Length) return '\0';
                return m_line[m_pos];
            }
        }

        private void JumpWhite()
        {
            while (m_pos > 0)
            {
                if (m_pos >= m_line.Length || Char.IsWhiteSpace(m_line, m_pos)) m_pos--;
                else break;
            }
        }
    }

    public class SqlCompletionData : DefaultCompletionData
    {
        public string InsertString;

        public SqlCompletionData(string text, string description, int imageIndex, string insText)
            : base(text, description, imageIndex)
        {
            InsertString = insText;
        }

        public override bool InsertAction(TextArea textArea, char ch)
        {
            textArea.InsertString(InsertString);
            return false;
        }
    }
}
