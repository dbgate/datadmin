using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Antlr.Runtime;

namespace DatAdmin
{
    public class SqlEditorAnalyser
    {
        public class TableItem
        {
            public DepsName Name;
            public string Alias;

            // mapping to real FULL NAMED object
            public NameWithSchema RealTable;
            public NameWithSchema RealView;

            public ITableStructure Structure;

            public NameWithSchema FullName { get { return RealTable ?? RealView; } }

            public bool Match(DepsName name)
            {
                if (Alias != null) return name.Components.Count == 1 && String.Compare(name.Components[0], Alias, true) == 0;
                return name.SimilarTo(FullName);
            }

            public override string ToString()
            {
                string res = "";
                if (Alias != null) res = Alias + ":";
                return res + FullName.ToString();
            }
        }
        public enum CodeContext { None, Table, Column, ColumnWithoutQualifier, String }

        public class CodeRange
        {
            public CodeContext Context = CodeContext.None;
            public int StartOffset, EndOffset;
        }

        public class Line
        {
            public List<CodeRange> Ranges = new List<CodeRange>();

            public CodeContext GetContext(int ofs)
            {
                foreach (var range in Ranges)
                {
                    if (ofs >= range.StartOffset && ofs < range.EndOffset) return range.Context;
                }
                return Ranges.Last().Context;
            }
        }

        public List<TableItem> UsedTables = new List<TableItem>();
        public Dictionary<int, Line> Lines = new Dictionary<int, Line>();

        public List<NameWithSchema> TableNames = new List<NameWithSchema>();
        public List<NameWithSchema> ViewNames = new List<NameWithSchema>();

        ITokenStream m_input;
        AntlrTokens m_tokens;
        CodeContext m_context = CodeContext.None;
        ISqlDialect m_dialect;
        int m_lastLine = -1;
        int m_lastOfs = 0;
        int m_lastWrittenOfs = 0;

        public ISqlDialect Dialect { get { return m_dialect; } }

        enum QueryType { NONE, SELECT, INSERT, DELETE, UPDATE }
        QueryType m_queryType = QueryType.NONE;

        private void ProcessToken()
        {
            var tok = m_input.LT(1);
            int la1 = m_input.LA(1), la2 = m_input.LA(2);

            if (la1 == m_tokens.SELECT) m_queryType = QueryType.SELECT;
            if (la1 == m_tokens.UPDATE) m_queryType = QueryType.UPDATE;
            if (la1 == m_tokens.DELETE) m_queryType = QueryType.DELETE;
            if (la1 == m_tokens.INSERT) m_queryType = QueryType.INSERT;

            if (la1 == m_tokens.T_STRING)
            {
                var oldctx = m_context;
                SetPositionBegin(tok);
                SetContext(CodeContext.String);
                SetPositionEnd(tok);
                SetContext(oldctx);
                m_input.Consume();
                return;
            }

            if (la1 == m_tokens.SELECT
                || la1 == m_tokens.WHERE
                || la1 == m_tokens.ON
                )
            {
                SetPositionEnd(tok);
                SetContext(CodeContext.Column);
                m_input.Consume();
                return;
            }

            if ((la1 == m_tokens.LPAREN && m_queryType == QueryType.INSERT)
                || la1 == m_tokens.SET
                )
            {
                SetPositionEnd(tok);
                SetContext(CodeContext.ColumnWithoutQualifier);
                m_input.Consume();
                return;
            }

            if (la1 == m_tokens.ORDER && la2 == m_tokens.BY
                || la1 == m_tokens.GROUP && la2 == m_tokens.BY)
            {
                SetPositionEnd(m_input.LT(2));
                m_input.Consume();
                m_input.Consume();
                SetContext(CodeContext.Column);
                return;
            }
            if (la1 == m_tokens.FROM
                || la1 == m_tokens.JOIN
                || la1 == m_tokens.UPDATE
                || la1 == m_tokens.DELETE
                || la1 == m_tokens.INSERT
                )
            {
                SetPositionEnd(tok);
                SetContext(CodeContext.Table);
                m_input.Consume();
                return;
            }

            if (m_context == CodeContext.Table && m_tokens.IsIdent(la1))
            {
                var name = new DepsName();
                name.Components.Add(m_dialect.UnquoteName(tok.Text));
                m_input.Consume();
                while (m_input.LA(1) == m_tokens.DOT && m_tokens.IsIdent(m_input.LA(2)))
                {
                    name.Components.Add(m_dialect.UnquoteName(m_input.LT(2).Text));
                    m_input.Consume();
                    m_input.Consume();
                }
                var titem = new TableItem { Name = name };
                if (m_tokens.IsIdent(m_input.LA(1)))
                {
                    titem.Alias = m_dialect.UnquoteName(m_input.LT(1).Text);
                    m_input.Consume();
                }
                UsedTables.Add(titem);
                return;
            }

            // default token handling
            m_input.Consume();
            SetPositionEnd(tok);
        }

        private void SetPositionBegin(IToken token)
        {
            SetPosition(token.Line - 1, token.CharPositionInLine);
        }

        private void SetPositionEnd(IToken token)
        {
            SetPosition(token.Line - 1, token.CharPositionInLine + token.Text.Length);
        }

        private void FlushLastRange()
        {
            if (m_lastLine < 0) return;
            if (m_lastOfs == m_lastWrittenOfs) return;
            var line = Lines.Get(m_lastLine, null);
            if (line == null)
            {
                line = new Line();
                Lines[m_lastLine] = line;
            }
            line.Ranges.Add(new CodeRange { StartOffset = m_lastWrittenOfs, EndOffset = m_lastOfs, Context = m_context });
        }

        private void SetPosition(int line, int ofs)
        {
            if (m_lastLine != line)
            {
                FlushLastRange();
            }
            m_lastLine = line;
            m_lastOfs = ofs;
        }

        private void SetContext(CodeContext ctx)
        {
            if (ctx != m_context)
            {
                FlushLastRange();
            }
            m_context = ctx;
        }

        public SqlEditorAnalyser(ITokenStream input, ISqlDialect dialect)
        {
            m_input = input;
            m_dialect = dialect;
            m_tokens = dialect.GetAntlrTokens();
        }

        public bool ParseInput()
        {
            try
            {
                while (m_input.LA(1) != m_tokens.EOF)
                {
                    ProcessToken();
                }

                FlushLastRange();
            }
            catch
            {
                return false;
            }

            return true;
        }

        private NameWithSchema FindItem(DepsName name, List<NameWithSchema> available)
        {
            foreach (var av in available)
            {
                if (name.SimilarTo(av)) return av;
            }
            return null;
        }

        public bool LoadCatalogs(IDatabaseSource conn)
        {
            var cache = conn.GetCache();

            var tblcache = cache.Get("code_completion", "table_list");
            var vicache = cache.Get("code_completion", "view_list");

            var dbmem = new DatabaseStructureMembers();
            if (tblcache == null)
            {
                dbmem.TableList = true;
                dbmem.SpecificObjectOverride["view"] = new SpecificObjectMembers { ObjectList = true };
            }
            IDatabaseStructure dbs = null;
            if (tblcache == null)
            {
                dbs = conn.LoadDatabaseStructure(dbmem, null);
                foreach (var tbl in dbs.Tables) TableNames.Add(tbl.FullName);
                cache.Put("code_completion", "table_list", new List<NameWithSchema>(TableNames));
                if (dbs.SpecificObjects.ContainsKey("view"))
                {
                    foreach (var obj in dbs.SpecificObjects["view"])
                    {
                        ViewNames.Add(obj.ObjectName);
                    }
                }
                cache.Put("code_completion", "view_list", new List<NameWithSchema>(ViewNames));
            }
            else
            {
                TableNames.AddRange((List<NameWithSchema>)cache.Get("code_completion", "table_list"));
                ViewNames.AddRange((List<NameWithSchema>)cache.Get("code_completion", "view_list"));
            }

            foreach (var tbl in UsedTables)
            {
                tbl.RealTable = FindItem(tbl.Name, TableNames);
                if (tbl.RealTable == null) tbl.RealView = FindItem(tbl.Name, ViewNames);
            }
            UsedTables.RemoveIf(tbl => tbl.RealTable == null && tbl.RealView == null);
            var used2 = new List<TableItem>();
            foreach (var tbl in UsedTables)
            {
                if (used2.Find(t => t.RealTable == tbl.RealTable && t.RealView == tbl.RealView && t.Alias == tbl.Alias) == null)
                {
                    used2.Add(tbl);
                }
            }
            UsedTables = used2;

            dbmem = new DatabaseStructureMembers();
            dbmem.TableMembers = TableStructureMembers.ColumnNames | TableStructureMembers.ForeignKeys | TableStructureMembers.ReferencedFrom;
            dbmem.TableFilter = new List<NameWithSchema>(from u in UsedTables where u.RealTable != null select u.RealTable);
            dbmem.ViewAsTables = true;
            dbmem.ViewAsTableFilter = new List<NameWithSchema>(from u in UsedTables where u.RealView != null select u.RealView);

            dbmem.TableFilter.RemoveIf(tbl => cache.Has("code_completion_table", tbl.ToString()));
            dbmem.ViewAsTableFilter.RemoveIf(tbl => cache.Has("code_completion_view", tbl.ToString()));

            if (dbmem.TableFilter.Count > 0 || dbmem.ViewAsTableFilter.Count > 0)
            {
                dbs = conn.LoadDatabaseStructure(dbmem, null);
            }
            
            foreach (var tbl in UsedTables)
            {
                var ts = (ITableStructure)cache.Get(tbl.RealTable != null ? "code_completion_table" : "code_completion_view", tbl.ToString());
                if (ts != null)
                {
                    tbl.Structure = ts;
                }
                else
                {
                    if (tbl.RealTable != null) tbl.Structure = dbs.Tables[tbl.RealTable];
                    if (tbl.RealView != null) tbl.Structure = dbs.ViewAsTables[tbl.RealView];
                }
            }

            return true;
            //dbmem.TableMembers = TableStructureMembers.ColumnNames;
        }

        public CodeContext GetContext(int line, int ofs)
        {
            var lineobj = Lines.Get(line, null);
            if (lineobj == null)
            {
                line--;
                while (line >= 0)
                {
                    lineobj = Lines.Get(line, null);
                    if (lineobj != null) return lineobj.Ranges.Last().Context;
                    line--;
                }
                return CodeContext.None;
            }
            return lineobj.GetContext(ofs);
        }

        public TableItem FindUsedMatch(DepsName name)
        {
            return UsedTables.FirstOrDefault(tbl => tbl.Match(name));
        }
    }
}
