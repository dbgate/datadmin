using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace DatAdmin
{
    public partial class TablePerspectiveEditorFrame : UserControl, IDmlfHandler
    {
        TablePerspective m_per;
        ITableStructure m_table;
        IDatabaseSource m_db;
        Dictionary<NameWithSchema, ITableStructure> m_tables = new Dictionary<NameWithSchema, ITableStructure>();
        List<NameWithSchema> m_allTables;
        Dictionary<string, DmlfColumnRef> m_colSources = new Dictionary<string, DmlfColumnRef>();

        DmlfRelation m_loadedRelation;
        List<TextBox> m_relPkCols = new List<TextBox>();
        List<DmlfExpressionFrame> m_relPkVals = new List<DmlfExpressionFrame>();

        TablePerspectiveConditions m_filter;

        public TablePerspectiveEditorFrame(TablePerspective per, ITableStructure table, IDatabaseSource db)
        {
            InitializeComponent();
            Init(per, table, db);
        }

        public void Init(TablePerspective per, ITableStructure table, IDatabaseSource db)
        {
            per.Select.Columns.NormalizeBaseTables();

            cbxReferencedTable.Items.Clear();
            m_per = per;
            m_db = db;
            m_table = table;
            foreach (var tbl in AllTables)
            {
                cbxReferencedTable.Items.Add(tbl);
            }
            m_filter = per.Conditions.CloneUsingXml();
            objectFilterFrame1.Filter = m_filter;
            //tbxDatabase.Text = m_per.Conditions.Database;
            //chbDatabase.Checked = m_per.Conditions.DatabaseChecked;
            //chbDatabaseRegex.Checked = m_per.Conditions.SchemaRegex;

            //tbxServer.Text = m_per.Conditions.Server;
            //chbServer.Checked = m_per.Conditions.ServerChecked;
            //chbServerRegex.Checked = m_per.Conditions.ServerRegex;

            //tbxSchema.Text = m_per.Conditions.Schema;
            //chbSchema.Checked = m_per.Conditions.SchemaChecked;
            //chbSchemaRegex.Checked = m_per.Conditions.SchemaRegex;

            //tbxTable.Text = m_per.Conditions.Table;
            //chbTable.Checked = m_per.Conditions.TableChecked;
            //chbTableRegex.Checked = m_per.Conditions.TableRegex;

            //chbColumns.Checked = m_per.Conditions.ColumnsChecked;
            //tbxColumns.Text = "";
            //if (m_per.Conditions.Columns != null)
            //{
            //    tbxColumns.Text = m_per.Conditions.Columns.CreateDelimitedText("\r\n");
            //}

            foreach (var rel in m_per.Select.From.Relations)
            {
                AddRelation(rel);
            }

            FillAvailableColSources();

            var cd = m_per.Select.GetColumnDisplay();
            foreach (var col in cd)
            {
                dataGridViewColumns.Rows.Add(col.ValueRef.Alias ?? "", col.ValueRef.SafeToString(), col.LookupRef.SafeToString());
            }
            LoadCurrentRelation();
        }

        private string GetRelationSourceText(DmlfRelation rel)
        {
            string src = "(SOURCE)";
            var rs = rel.Conditions.FindAnySource(false, true);
            if (rs != null) src = rs.ToString();
            return src;
        }

        private ListViewItem AddRelation(DmlfRelation rel)
        {
            var item = lsvRelations.Items.Add(GetRelationSourceText(rel));
            item.Tag = rel;
            item.SubItems.Add(rel.Reference != null && rel.Reference.TableOrView != null ? rel.Reference.TableOrView.ToString() : "");
            item.SubItems.Add(rel.Reference != null && rel.Reference.Alias != null ? rel.Reference.Alias : "");
            return item;
        }

        private void RefreshLoadedRelation()
        {
            foreach (ListViewItem item in lsvRelations.Items)
            {
                if (item.Tag != (object)m_loadedRelation) continue;
                var rel = (DmlfRelation)item.Tag;

                item.Text = GetRelationSourceText(rel);
                item.SubItems[1].Text = rel.Reference != null && rel.Reference.TableOrView != null ? rel.Reference.TableOrView.ToString() : "";
                item.SubItems[2].Text = rel.Reference != null && rel.Reference.Alias != null ? rel.Reference.Alias : "";
            }
        }


        public TablePerspectiveEditorFrame()
        {
            InitializeComponent();
        }

        private List<NameWithSchema> AllTables
        {
            get
            {
                if (m_allTables == null) m_allTables = new List<NameWithSchema>(m_db.LoadFullTableNames(true)).Sorted();
                return m_allTables;
            }
        }

        //private void FillBindingTables()
        //{
        //    //colBindingTable.Items.Clear();
        //    //colBindingTable.Items.Add("(SOURCE)");
        //    //for (int i = 0; i < dataGridViewRelations.RowCount; i++)
        //    //{
        //    //    if (dataGridViewRelations.Rows[i].Cells[2].Value == null) continue;
        //    //    string alias = dataGridViewRelations.Rows[i].Cells[4].Value.SafeToString();
        //    //    if (String.IsNullOrEmpty(alias)) colBindingTable.Items.Add(dataGridViewRelations.Rows[i].Cells[2].Value);
        //    //    else colBindingTable.Items.Add(alias);
        //    //}
        //}

        private ITableStructure GetTableStruct(NameWithSchema name)
        {
            if (!m_tables.ContainsKey(name))
            {
                m_tables[name] = m_db.InvokeLoadTableStructure(name, TableStructureMembers.ColumnNames | TableStructureMembers.PrimaryKey);
            }
            return m_tables[name];
        }

        private ITableStructure GetTableStruct(DmlfRelation rel)
        {
            if (rel == null) return m_table;
            return GetTableStruct(rel.Reference.TableOrView);
        }

        private bool HideSchema
        {
            get
            {
                return !m_db.DatabaseCaps.MultipleSchema;
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Errors.LogError(e.Exception);
        }

        private void btnAddRelation_Click(object sender, EventArgs e)
        {
            var item = AddRelation(new DmlfRelation { JoinType = DmlfJoinType.Left });
            item.Focused = item.Selected = true;
        }

        private void btnRemoveRelation_Click(object sender, EventArgs e)
        {
            if (lsvRelations.SelectedIndices.Count > 0)
            {
                lsvRelations.Items.RemoveAt(lsvRelations.SelectedIndices[0]);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                FillAvailableColSources();
            }
        }

        private void FillAvailableColSources()
        {
            m_colSources.Clear();
            FillAvailableColSources(colDataColumn);
            FillAvailableColSources(colLookupColumn);

            btnAddTableColumns.DropDownItems.Clear();
            AddTableColumnsDropDown(null);
            foreach (var rel in GetRelations())
            {
                AddTableColumnsDropDown(rel);
            }
        }

        private void AddTableColumnsDropDown(DmlfRelation rel)
        {
            var item = btnAddTableColumns.DropDownItems.Add(rel.SafeToString() ?? "(SOURCE)");
            item.Tag = rel;
            item.Click += new EventHandler(addTableItem_Click);
        }

        void addTableItem_Click(object sender, EventArgs e)
        {
            var item = (ToolStripItem)sender;
            var rel = (DmlfRelation)item.Tag;
            var ts = GetTableStruct(rel);
            foreach (var col in ts.Columns)
            {
                var tpc = new DmlfColumnRef { Source = rel != null ? rel.Reference : DmlfSource.BaseTable, ColumnName = col.ColumnName };
                dataGridViewColumns.Rows.Add(col.ColumnName, tpc.ToString(), null);
            }
        }

        private void FillAvailableColSources(DataGridViewComboBoxColumn col)
        {
            col.Items.Clear();
            col.Items.Add("-");

            FillAvailableColSources(col, null, m_table);
            foreach (var rel in GetRelations())
            {
                if (rel.Reference == null) continue;
                FillAvailableColSources(col, rel, GetTableStruct(rel.Reference.TableOrView));
            }

            foreach (DataGridViewRow row in dataGridViewColumns.Rows)
            {
                if (row.Cells[col.Index].Value == null) continue;
                if (col.Items.IndexOf(row.Cells[col.Index].Value) < 0) row.Cells[col.Index].Value = null;
            }
        }

        private void FillAvailableColSources(DataGridViewComboBoxColumn col, DmlfRelation rel, ITableStructure ts)
        {
            foreach (var cs in ts.Columns)
            {
                var tpc = new DmlfColumnRef
                {
                    Source = rel != null ? rel.Reference : DmlfSource.BaseTable,
                    ColumnName = cs.ColumnName,
                };
                m_colSources[tpc.ToString()] = tpc;
                col.Items.Add(tpc.ToString());
            }
        }

        private void btnAddColumn_Click(object sender, EventArgs e)
        {
            dataGridViewColumns.Rows.Add(1);
        }

        private void btnRemoveColumn_Click(object sender, EventArgs e)
        {
            if (dataGridViewColumns.CurrentCell.RowIndex >= 0) dataGridViewColumns.Rows.RemoveAt(dataGridViewColumns.CurrentCell.RowIndex);
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            dataGridViewColumns.MoveRowUp();
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            dataGridViewColumns.MoveRowDown();
        }

        //internal static string RowToTableName(DataGridViewRow row)
        //{
        //    if (row == null) return "(SOURCE)";
        //    if (String.IsNullOrEmpty(row.Cells[4].Value.SafeToString())) return row.Cells[2].Value.ToString();
        //    return row.Cells[4].Value.ToString();
        //}

        public void SavePerspective()
        {
            //m_per.Conditions.Server = tbxServer.Text;
            //m_per.Conditions.ServerChecked = chbServer.Checked;
            //m_per.Conditions.ServerRegex = chbServerRegex.Checked;

            //m_per.Conditions.Database = tbxDatabase.Text;
            //m_per.Conditions.DatabaseChecked = chbDatabase.Checked;
            //m_per.Conditions.DatabaseRegex = chbDatabaseRegex.Checked;

            //m_per.Conditions.Schema = tbxSchema.Text;
            //m_per.Conditions.SchemaChecked = chbSchema.Checked;
            //m_per.Conditions.SchemaRegex = chbSchemaRegex.Checked;

            //m_per.Conditions.Table = tbxTable.Text;
            //m_per.Conditions.TableChecked = chbTable.Checked;
            //m_per.Conditions.TableRegex = chbTableRegex.Checked;

            //m_per.Conditions.Columns = (from s in tbxColumns.Text.Split('\n') where !s.IsEmpty() select s.Trim()).ToArray();
            //m_per.Conditions.ColumnsChecked = chbColumns.Checked;

            m_per.Conditions = m_filter.CloneUsingXml();

            m_per.Select.From.Relations.Clear();
            m_per.Select.Columns.Clear();
            foreach (var rel in GetRelations())
            {
                m_per.Select.From.Relations.Add(rel.Clone());
            }
            // data columns
            foreach (DataGridViewRow row in dataGridViewColumns.Rows)
            {
                var datacol = GetColumnSource(row.Cells[1].Value);
                if (datacol == null) continue;
                var di = new ColumnDisplayInfo { Style = ColumnDisplayInfo.UsageStyle.Value };
                var rdata = new DmlfResultField { Expr = new DmlfColumnRefExpression { Column = datacol }, Alias = row.Cells[0].Value.SafeToString(), DisplayInfo = di };
                m_per.Select.Columns.Add(rdata);
            }
            // lookup columns
            int colindex = 0;
            foreach (DataGridViewRow row in dataGridViewColumns.Rows)
            {
                var datacol = GetColumnSource(row.Cells[1].Value);
                var lookup = GetColumnSource(row.Cells[2].Value);
                if (datacol == null) continue;
                if (lookup != null)
                {
                    var di = new ColumnDisplayInfo { Style = ColumnDisplayInfo.UsageStyle.Lookup, VisibleColumnIndex = colindex };
                    var rloo = new DmlfResultField { Expr = new DmlfColumnRefExpression { Column = lookup }, DisplayInfo = di };
                    m_per.Select.Columns.Add(rloo);
                }
                colindex++;
            }

            m_per.Select.Columns.NormalizeBaseTables();
            m_per.Select.CompleteUpdatingInfo(this);
        }

        private IEnumerable<DmlfRelation> GetRelations()
        {
            foreach (ListViewItem item in lsvRelations.Items)
            {
                yield return (DmlfRelation)item.Tag;
            }
        }


        private IEnumerable<DmlfRelation> GetRelationsBeforeCurrent()
        {
            for (int i = 0; i < lsvRelations.SelectedIndices[0]; i++)
            {
                var rel = (DmlfRelation)lsvRelations.Items[i].Tag;
                yield return rel;
            }
        }

        private DmlfColumnRef GetColumnSource(object value)
        {
            if (value == null || "-" == value.SafeToString()) return null;
            return m_colSources[value.ToString()];
        }

        private void lsvRelations_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCurrentRelation();
        }

        private bool m_loadingRelation;
        private void LoadCurrentRelation()
        {
            m_loadingRelation = true;
            try
            {
                ClearRelationCtrls();
                m_loadedRelation = null;
                if (lsvRelations.SelectedIndices.Count > 0) m_loadedRelation = (DmlfRelation)lsvRelations.Items[lsvRelations.SelectedIndices[0]].Tag;
                if (m_loadedRelation != null)
                {
                    rbtLeftJoin.Checked = true;
                    if (m_loadedRelation.JoinType == DmlfJoinType.Inner) rbtInnerJoin.Checked = true;
                    tbxAlias.Text = m_loadedRelation.Reference != null && m_loadedRelation.Reference.Alias != null ? m_loadedRelation.Reference.Alias : "";
                    cbxReferencedTable.SelectedIndex = m_loadedRelation.Reference != null ? cbxReferencedTable.Items.IndexOf(m_loadedRelation.Reference.TableOrView) : -1;

                    //cbxBindingTable.Items.Clear();
                    //cbxBindingTable.Items.Add("(SOURCE)");

                    //foreach(var rel in GetRelationsBeforeCurrent())
                    //{
                    //    if (rel.Reference != null) cbxBindingTable.Items.Add(rel.Reference);
                    //}

                    //var s0 = m_loadedRelation.GetRelationSource();
                    //if (s0 == null) cbxBindingTable.SelectedIndex = 0;
                    //else cbxBindingTable.SelectedIndex = cbxBindingTable.Items.IndexOf(s0);

                    CreateRelationCtrls();
                }
            }
            finally
            {
                m_loadingRelation = false;
            }
            rbtInnerJoin.Enabled = rbtLeftJoin.Enabled = cbxReferencedTable.Enabled = tbxAlias.Enabled = m_loadedRelation != null;
        }

        private DmlfSource[] GetAvailableSources()
        {
            var res = new List<DmlfSource>();
            res.Add(null);
            foreach (var rel in GetRelationsBeforeCurrent())
            {
                res.Add(rel.Reference);
            }
            return res.ToArray();
        }

        private void CreateRelationCtrls()
        {
            //ITableStructure ts = m_table;
            //var bind = m_loadedRelation.GetRelationSource();
            //if (bind != null && bind.TableOrView != null)
            //{
            //    ts = GetTableStruct(bind.TableOrView);
            //}

            int index = 1;
            foreach (var cond in m_loadedRelation.Conditions)
            {
                string colname = "";
                if (cond.LeftExpr is DmlfColumnRefExpression) colname = ((DmlfColumnRefExpression)cond.LeftExpr).Column.ColumnName;
                var tbx = new TextBox { ReadOnly = true, Text = colname ?? "" };
                var ed = new DmlfExpressionFrame();
                tabPage1.Controls.Add(tbx);
                tabPage1.Controls.Add(ed);
                int acty = cbxReferencedTable.Top + (cbxReferencedTable.Height + 10) * index;
                tbx.Left = cbxReferencedTable.Left;
                tbx.Width = cbxReferencedTable.Width;
                tbx.Top = acty;
                ed.Left = labBindingExpression.Left;
                ed.Width = cbxReferencedTable.Width;
                ed.Top = acty;
                ed.Handler = this;
                ed.AvailableSources = GetAvailableSources();
                ed.Tag = cond;
                ed.Expression = cond.RightExpr;
                ed.ValueChanged += new EventHandler(cbx_TextChanged);
                m_relPkCols.Add(tbx);
                m_relPkVals.Add(ed);
                index++;
            }
        }

        void cbx_TextChanged(object sender, EventArgs e)
        {
            var ed = (DmlfExpressionFrame)sender;
            var cond = (DmlfEqualCondition)ed.Tag;
            cond.RightExpr = ed.Expression;
            RefreshLoadedRelation();
            //string val = cbx.Text;
            //if (cbx.Items.IndexOf(val) >= 0)
            //{
            //    cond.RightExpr = new DmlfColumnRefExpression
            //    {
            //        Column = new DmlfColumnRef
            //        {
            //            ColumnName = val,
            //            Source = m_loadedRelation.GetRelationSource()
            //        }
            //    };
            //}
            //else
            //{
            //    cond.RightExpr = new DmlfStringExpression { Value = val };
            //}
        }

        private void ClearRelationCtrls()
        {
            foreach (var tbx in m_relPkCols) tbx.Dispose();
            m_relPkCols.Clear();
            foreach (var cbx in m_relPkVals) cbx.Dispose();
            m_relPkVals.Clear();
        }

        //private void cbxBindingTable_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (m_loadingRelation) return;

        //    foreach (var cond in m_loadedRelation.Conditions)
        //    {
        //        cond.RightExpr = new DmlfStringExpression { Value = "" };
        //    }
        //    ClearRelationCtrls();
        //    CreateRelationCtrls();
        //    RefreshLoadedRelation();
        //}

        private void cbxReferencedTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_loadingRelation) return;
            if (m_loadedRelation == null) return;

            m_loadedRelation.Conditions.Clear();

            m_loadedRelation.Reference = new DmlfSource
            {
                TableOrView = (NameWithSchema)cbxReferencedTable.SelectedItem,
                Alias = tbxAlias.Text
            };

            ITableStructure refts = null;
            var reft = m_loadedRelation.Reference;
            if (reft != null) refts = GetTableStruct(reft.TableOrView);
            //var bind = m_loadedRelation.GetRelationSource();

            if (refts != null)
            {
                foreach (string pkcol in refts.FindConstraint<IPrimaryKey>().Columns.GetNames())
                {
                    m_loadedRelation.Conditions.Add(new DmlfEqualCondition
                    {
                        LeftExpr = new DmlfColumnRefExpression
                        {
                            Column = new DmlfColumnRef
                            {
                                Source = reft,
                                ColumnName = pkcol
                            }
                        },
                        RightExpr = new DmlfStringExpression { Value = "" }
                    });
                }
            }

            ClearRelationCtrls();
            CreateRelationCtrls();
            RefreshLoadedRelation();
        }

        private void tbxAlias_TextChanged(object sender, EventArgs e)
        {
            if (m_loadedRelation != null && m_loadedRelation.Reference != null)
            {
                m_loadedRelation.Reference.Alias = tbxAlias.Text;
                RefreshLoadedRelation();
            }
        }

        #region IDmlfHandler Members

        ITableStructure IDmlfHandler.GetStructure(NameWithSchema name)
        {
            if (name == null) return m_table;
            return GetTableStruct(name);
        }

        DmlfSource IDmlfHandler.BaseTable
        {
            get { return new DmlfSource { Alias = "basetbl", TableOrView = m_table.FullName }; }
        }

        #endregion

        private void dataGridViewColumns_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Logging.Warning("Table perspective columns error, row={0}, col={1}, error={2}", e.RowIndex, e.ColumnIndex, e.Exception.Message);
        }
    }

    //internal class TPColSource
    //{
    //    internal TablePerspectiveRelation Relation;
    //    internal string ColumnName;

    //    public override string ToString()
    //    {
    //        if (Relation == null) return ColumnName;
    //        string table;
    //        if (!String.IsNullOrEmpty(Relation.ReferencedTableAlias)) table = Relation.ReferencedTableAlias;
    //        else table = Relation.ReferencedTable.ToString();
    //        return table + "." + ColumnName;
    //    }
    //}
}
