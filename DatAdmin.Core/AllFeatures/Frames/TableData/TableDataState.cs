using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DatAdmin
{
    internal class TableDataState
    {
        internal string Filter;
        internal string SearchText;
        internal string[] SearchColumns;
        internal bool SearchExactMatch;
        //internal string SortOrder;
        //internal int SelectedDetail;
        internal int FirstRecord = 0;
        internal int RecordCount = 200;
        internal int CurrentRow = -1;
        internal int CurrentCol = -1;

        internal string SavedPkCol;
        internal object SavedPkValue;
        internal bool SavedCurrentCell;
        internal DmlfSortOrderCollection SortOrder;
        internal PerspectiveIndependendState DataStatePer;

        internal TablePerspective Perspective;
        internal ITabularDataView TableData { get { return DataStatePer.TableData; } }
        internal DmlfConditionBase AdditionalCondition { get { return DataStatePer.AdditionalCondition; } }

        //internal List<ReferencesTableDataFrame> RefGrids = new List<ReferencesTableDataFrame>();
        internal DockPanelContentFrame DockPanelFrame;
        private ColumnDisplay m_lastColumnDisplay;

        public override string ToString()
        {
            try
            {
                string tbl = TableData.TableSource.FullName.ToString();
                if (AdditionalCondition != null) tbl += "[" + AdditionalCondition.ToString() + "]";
                return tbl;
            }
            catch (Exception)
            {
                return "???";
            }
        }

        private string CreateSearchCondition(ColumnDisplay disp, ISqlDialect dialect, IDmlfHandler handler)
        {
            if (disp == null) disp = m_lastColumnDisplay;
            if (disp == null) return "1=1";
            bool was = false;
            StringBuilder res = new StringBuilder();
            //ITableStructure table = TableData.GetStructure(Perspective);
            var dda = dialect.CreateDataAdapter();
            var spars = new FulltextSearchParams { ExactMatch = SearchExactMatch };
            foreach (var col in disp)
            {
                var cs = (IColumnStructure)col.ValueTag;
                if (cs == null) continue;
                if (cs.DataType is DbTypeXml || cs.DataType is DbTypeBlob) continue;
                if (SearchColumns != null && Array.IndexOf(SearchColumns, col.ValueRef.ToString()) < 0) continue;
                if (was) res.Append(" OR ");
                res.Append(dda.GetFulltextSearchExpr(col.ValueRef.Expr.ToSql(dialect, handler), SearchText, spars));
                //res.AppendFormat("{0} LIKE {1}", dialect.QuoteIdentifier(col.ColumnName), dialect.GetSqlLiteral("%" + SearchText + "%"));
                was = true;
            }
            if (!was) res.Append("1=1");
            return "(" + res.ToString() + ")";
        }

        internal string CreateSqlCondition(ColumnDisplay disp, ISqlDialect dialect, IDmlfHandler handler, bool includeUserInput)
        {
            if (disp != null) m_lastColumnDisplay = disp;
            if (TableData == null) return "";
            List<string> conds = new List<string>();
            if (includeUserInput && !Filter.IsEmpty()) conds.Add("(" + Filter + ")");
            if (AdditionalCondition != null) conds.Add("(" + AdditionalCondition.ToSql(dialect, handler) + ")");
            if (TableData.TabDataCaps.Filtering && !SearchText.IsEmpty()) conds.Add(CreateSearchCondition(disp, dialect, handler));
            if (conds.Count == 0) return "";
            return String.Join(" AND ", conds.ToArray());
        }
    }

    public class ReferenceViewDefinition : IExplicitXmlPersistent
    {
        public NameWithSchema TableName;
        public List<string> SourceColumns = new List<string>();
        public List<string> ReferenceColumns = new List<string>();
        public ISqlDialect Dialect;

        public DmlfConditionBase GetCondition(BedRow masterRow)
        {
            var res = new DmlfAndCondition();
            if (masterRow == null) return res;
            for (int i = 0; i < SourceColumns.Count; i++)
            {
                int srcindex = masterRow.GetOrdinal(SourceColumns[i]);
                if (masterRow.Table.ResultFields != null) srcindex = masterRow.Table.ResultFields.GetColumnIndex(DmlfColumnRef.GetBaseColumn(SourceColumns[i]));
                res.Conditions.Add(
                    new DmlfEqualCondition
                    {
                        LeftExpr = new DmlfColumnRefExpression
                        {
                            Column = new DmlfColumnRef
                            {
                                ColumnName = ReferenceColumns[i]
                            }
                        },
                        RightExpr = new DmlfLiteralExpression { Value = masterRow[srcindex] }
                    });
            }
            return res;
        }

        public void SaveToXml(XmlElement xml)
        {
            TableName.SaveToXml(xml.AddChild("Table"));
            foreach (string col in SourceColumns) xml.AddChild("SourceColumn").InnerText = col;
            foreach (string col in ReferenceColumns) xml.AddChild("ReferenceColumn").InnerText = col;
        }

        public void LoadFromXml(XmlElement xml)
        {
            TableName = NameWithSchema.LoadFromXml(xml.FindElement("Table"));
            foreach (XmlElement col in xml.SelectNodes("SourceColumn")) SourceColumns.Add(col.InnerText);
            foreach (XmlElement col in xml.SelectNodes("ReferenceColumn")) ReferenceColumns.Add(col.InnerText);
        }
    }

    internal class PerspectiveIndependendState
    {
        internal ITabularDataView TableData;
        internal DmlfConditionBase AdditionalCondition;
        internal TableDataState DefaultState;
        internal TablePerspective Perspective;
        Dictionary<TablePerspective, List<Action<TableDataState>>> m_missingActions = new Dictionary<TablePerspective, List<Action<TableDataState>>>();
        // states by perspective
        Dictionary<TablePerspective, TableDataState> States = new Dictionary<TablePerspective, TableDataState>();
        internal List<TablePerspective> InMemoryPerspectives = new List<TablePerspective>();
        internal bool IsFixedPerspective;

        internal PerspectiveIndependendState()
        {
            DefaultState = new TableDataState
            {
                DataStatePer = this
            };
        }

        private TableDataState GetState(TablePerspective per)
        {
            if (per == null) return DefaultState;
            return States.Get(per, null);
        }

        internal TableDataState DataState
        {
            get
            {
                return GetState(Perspective);
            }
        }

        internal void SelectPerspective(TablePerspective per)
        {
            if (per != null && !States.ContainsKey(per))
            {
                var state = new TableDataState();
                state.DataStatePer = this;
                state.Perspective = per;
                States[per] = state;
            }
            Perspective = per;
            if (per != null && m_missingActions.ContainsKey(per))
            {
                foreach (var act in m_missingActions[per]) act(States[per]);
                m_missingActions[per].Clear();
            }
        }

        internal void ModifyDataStatePer(TablePerspective per, Action<TableDataState> action)
        {
            var state = GetState(per);
            if (state != null)
            {
                action(state);
            }
            else
            {
                if (!m_missingActions.ContainsKey(per)) m_missingActions[per] = new List<Action<TableDataState>>();
                m_missingActions[per].Add(action);
            }
        }

        internal void UseFixedPerspective(string perfile)
        {
            var per = new TablePerspective();
            per.LoadFromFile(perfile);
            per.FileName = perfile;
            SelectPerspective(per);
            IsFixedPerspective = true;
        }
    }

    internal class TableRelatedState
    {
        List<PerspectiveIndependendState> m_historyChain = new List<PerspectiveIndependendState>();
        int m_historyPosition = 0;

        internal TableRelatedState()
        {
            m_historyChain.Add(new PerspectiveIndependendState());
        }

        internal PerspectiveIndependendState DataStatePer
        {
            get { return m_historyChain[m_historyPosition]; }
        }

        internal bool GoBackEnabled
        {
            get { return m_historyPosition > 0; }
        }

        internal void GoBack()
        {
            if (GoBackEnabled) m_historyPosition--;
        }

        internal bool GoForwardEnabled
        {
            get { return m_historyPosition < m_historyChain.Count - 1; }
        }

        internal void GoForward()
        {
            if (GoForwardEnabled) m_historyPosition++;
        }

        internal void GoToReference(ReferenceViewDefinition refdef, BedRow masterRow)
        {
            ITabularDataView newdata = DataStatePer.TableData.TableSource.Database.GetTable(refdef.TableName).GetTabularData();
            int delcnt = m_historyChain.Count - m_historyPosition - 1;
            if (delcnt > 0) m_historyChain.RemoveRange(m_historyPosition + 1, delcnt);
            var state = new PerspectiveIndependendState();
            state.TableData = newdata;
            state.AdditionalCondition = refdef.GetCondition(masterRow);
            m_historyChain.Add(state);
            m_historyPosition++;
        }

        internal IEnumerable<PerspectiveIndependendState> HistoryItems
        {
            get { return m_historyChain; }
        }

        internal int HistoryPosition
        {
            get { return m_historyPosition; }
            set { m_historyPosition = value; }
        }
    }
}
