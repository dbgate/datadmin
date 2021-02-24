using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Linq;

namespace DatAdmin
{
    public partial class TableEditFrame : ContentFrame
    {
        TableStructure m_originalTable;
        ISqlDialect m_dialect;
        //IDialectTypeSystem m_typeSystem;
        //ITableSaver m_saver;
        //ITableSource m_tconn;
        IDatabaseSource m_conn;
        bool m_initialized;
        bool noRectOnpktablename_SelectedIndexChange;
        //List<string> m_tableNames;
        List<NameWithSchema> m_tableNames;
        bool m_modified = false;
        Dictionary<NameWithSchema, List<string>> m_columnNames = new Dictionary<NameWithSchema, List<string>>();
        Dictionary<NameWithSchema, ITableStructure> m_tables = new Dictionary<NameWithSchema, ITableStructure>();
        EditorForeignColumnStructure m_selectedForeignKeyColumn;
        string[] m_charSets = null;
        string[] m_collations = null;
        private string m_sqlPreview;
        private CachingLogger m_messagesPreview;
        Dictionary<NameWithSchema, IDomainStructure> m_domains = null;
        IDialectSpecificEditor m_specificEditor;
        public bool ShowAlteredInfoDialog = true;

        List<EditorColumnStructure> m_columns = new List<EditorColumnStructure>();
        //List<int> m_originalColumnIndexes = new List<int>();

        List<EditorIndexOrKeyStructure> m_indexOrKeys = new List<EditorIndexOrKeyStructure>();
        List<EditorForeignKeyStructure> m_foreignKeys = new List<EditorForeignKeyStructure>();
        List<EditorForeignKeyStructure> m_refKeys = new List<EditorForeignKeyStructure>();
        List<EditorCheckStructure> m_checks = new List<EditorCheckStructure>();

        int PAGE_COLUMNS;
        int PAGE_INDEXESKEYS;
        int PAGE_RELATIONS;
        int PAGE_CHECKS;

        const int COL_NAME = 0;
        const int COL_TYPE = 1;
        const int COL_NULLABLE = 2;
        const int COL_ISPK = 3;
        const int COL_LENGTH = 4;
        const int COL_AUTOINC = 5;
        const int COL_FK = 6;

        AlterTableSettings m_defSettings, m_settings;
        ObjectEditorPars m_pars;

        public TableEditFrame()
        {
            InitializeComponent();
            OnlineHelpManager.RegisterHelpButton(btnOnlineHelp, "altertable");
        }

        public TableEditFrame(IDatabaseSource conn, ITableStructure table, AlterTableEditorPars pars)
        {
            InitializeComponent();
            Init(conn, table, pars);
            HConnection.RemoveByKey += RemoveConnectionByKey;
            Disposed += new EventHandler(TableEditFrame_Disposed);
            OnlineHelpManager.RegisterHelpButton(btnOnlineHelp, "altertable");
        }

        void TableEditFrame_Disposed(object sender, EventArgs e)
        {
            HConnection.RemoveByKey -= RemoveConnectionByKey;
        }

        private void SetSettings(AlterTableSettings value)
        {
            if (value != null) m_settings = value;
            else m_settings = m_defSettings;
            btnAllowRecreate.Checked = m_settings.AllowRecreateTable;
            EnableButtons();
        }

        public void Init(IDatabaseSource conn, ITableStructure table, AlterTableEditorPars pars)
        {
            if (conn != null && conn.Connection != null)
            {
                conn.Connection.Owner = this;
                Async.SafeOpen(conn.Connection);
            }
            m_dialect = conn.Dialect;
            m_pars = pars;
            if (m_dialect != null)
            {
                if (!m_dialect.DialectCaps.Checks) tabControl1.TabPages.Remove(tabCheckConstraints);
                if (!m_dialect.DialectCaps.ForeignKeys) tabControl1.TabPages.Remove(tabRelations);
            }
            PAGE_COLUMNS = tabControl1.TabPages.IndexOf(tabColumns);
            PAGE_INDEXESKEYS = tabControl1.TabPages.IndexOf(tabIndexesKeys);
            PAGE_RELATIONS = tabControl1.TabPages.IndexOf(tabRelations);
            PAGE_CHECKS = tabControl1.TabPages.IndexOf(tabCheckConstraints);
            m_conn = conn;
            if (table != null && table.Columns.Count == 0)
            {
                table = m_conn.InvokeLoadTableStructure(table.FullName, TableStructureMembers.All).CloneWithDb();
            }
            m_originalTable = table != null ? table.CloneWithDb() : null;
            //m_typeSystem = m_dialect == null ? null : m_dialect.TypeSystem;
            FillFromStructure();
            EnableButtons();
            m_initialized = true;
            if (!DatabaseConnection.DatabaseCaps.ExecuteSql) tabSql.Dispose();
            btnAllowRecreate.Enabled = DatabaseConnection.AlterCaps.RecreateTable;

            m_defSettings = GlobalSettings.Pages.AlterTable();
            if (m_defSettings == null) m_defSettings = new AlterTableSettings();
            SetSettings(m_conn.Settings.AlterTable());

            if (pars != null)
            {
                switch (pars.InitialTab)
                {
                    case AlterTableEditorPars.Tab.Columns:
                        tabControl1.SelectedIndex = PAGE_COLUMNS;
                        break;
                    case AlterTableEditorPars.Tab.IndexesKeys:
                        tabControl1.SelectedIndex = PAGE_INDEXESKEYS;
                        break;
                    case AlterTableEditorPars.Tab.ForeignKeys:
                        tabControl1.SelectedIndex = PAGE_RELATIONS;
                        break;
                    case AlterTableEditorPars.Tab.Checks:
                        tabControl1.SelectedIndex = PAGE_CHECKS;
                        break;
                }
            }

            if (!conn.GetAnyDialect().DialectCaps.AutoIncrement)
            {
                columnsGrid.Columns[COL_AUTOINC].Visible = false;
            }
        }

        public override void OnClose()
        {
            base.OnClose();
            if (m_conn != null) Async.SafeClose(m_conn.Connection);
        }

        public bool Modified { get { return m_modified; } }

        IPhysicalConnection PhysicalConnection
        {
            get
            {
                return m_conn.Connection;
                //if (m_dconn != null) return m_dconn.Connection;
                //return m_tconn.Connection;
            }
        }

        //public TableSourceCaps Behaviour
        //{
        //    get
        //    {
        //        if (m_dconn != null) return m_dconn.TableCaps;
        //        return m_tconn.TableCaps;
        //    }
        //}

        internal IDatabaseSource DatabaseConnection
        {
            get
            {
                return m_conn;
                //if (m_dconn != null) return m_dconn;
                //return m_tconn.Database;
            }
        }

        List<NameWithSchema> TableNames
        {
            get
            {
                if (m_tableNames == null)
                {
                    m_tableNames = new List<NameWithSchema>(DatabaseConnection.InvokeLoadFullTableNames(true));
                }
                return m_tableNames;
            }
        }

        List<string> ColumnNames(NameWithSchema table)
        {
            if (!m_columnNames.ContainsKey(table))
            {
                try
                {
                    m_columnNames[table] = new List<string>(from IColumnStructure c in DatabaseConnection.GetTable(table).InvokeLoadStructure(TableStructureMembers.ColumnNames).Columns select c.ColumnName);
                }
                catch
                {
                    m_columnNames[table] = new List<string>();
                }
            }
            return m_columnNames[table];
        }

        ITableStructure TableStructure(NameWithSchema table)
        {
            if (!m_tables.ContainsKey(table)) m_tables[table] = m_conn.InvokeLoadTableStructure(table, TableStructureMembers.All);
            return m_tables[table];
        }

        private Type EnumType { get { return m_dialect != null ? m_dialect.SpecificTypeEnum : typeof(DbTypeCode); } }

        List<string> GetTypeCodes()
        {
            List<string> res = new List<string>();
            if (m_dialect == null)
            {
                res.AddRange(Enum.GetNames(typeof(DbTypeCode)));
            }
            else
            {
                foreach (object typecode in Enum.GetValues(m_dialect.SpecificTypeEnum))
                {
                    if (m_dialect.SupportsTypeCode(typecode))
                    {
                        res.Add(typecode.ToString());
                    }
                }
            }
            res.Sort();
            return res;
        }

        private void FillFromStructure()
        {
            m_tableNames = null;

            DataGridViewComboBoxColumn dtcol = (DataGridViewComboBoxColumn)columnsGrid.Columns[1];
            dtcol.Items.Clear();
            foreach (string code in GetTypeCodes())
            {
                dtcol.Items.Add(code);
            }
            foreach (var dom in Domains.Values.Sorted())
            {
                dtcol.Items.Add(dom.FullName.ToString());
            }

            DataGridViewComboBoxColumn itcol = (DataGridViewComboBoxColumn)indexGrid.Columns[1];
            itcol.Items.Clear();
            foreach (string code in Enum.GetNames(typeof(EditorIndexOrKeyType)))
            {
                itcol.Items.Add(code);
            }

            DataGridViewComboBoxColumn fkcol = (DataGridViewComboBoxColumn)columnsGrid.Columns[COL_FK];
            fkcol.Items.Clear();
            fkcol.Items.Add("-");
            fkcol.Items.Add("???");
            foreach (NameWithSchema tablename in TableNames.Sorted())
            {
                fkcol.Items.Add(tablename);
            }

            //m_originalColumnIndexes.Clear();
            m_columns.Clear();
            m_indexOrKeys.Clear();
            m_foreignKeys.Clear();
            m_checks.Clear();
            m_refKeys.Clear();

            if (m_dialect != null) m_specificEditor = m_dialect.GetSpecificEditor(m_originalTable, DatabaseConnection);
            propertyFrame2.SelectedObject = m_specificEditor;

            if (m_originalTable == null) return;
            foreach (ColumnStructure col in m_originalTable.Columns)
            {
                //m_originalColumnIndexes.Add(col.ColumnOrder);
                EditorColumnStructure edcol = new EditorColumnStructure(this, col, m_dialect);
                m_columns.Add(edcol);
            }
            foreach (IConstraint cnt in m_originalTable.Constraints)
            {
                if (cnt is IIndex || cnt is IPrimaryKey || cnt is IUnique)
                {
                    m_indexOrKeys.Add(new EditorIndexOrKeyStructure(this, (ColumnsConstraint)cnt));
                }
                if (cnt is IForeignKey)
                {
                    m_foreignKeys.Add(new EditorForeignKeyStructure(this, (ForeignKey)cnt));
                }
                if (cnt is ICheck)
                {
                    m_checks.Add(new EditorCheckStructure(this, (CheckConstraint)cnt));
                }
            }
            foreach (ForeignKey fk in m_originalTable.GetReferencedFrom())
            {
                m_refKeys.Add(new EditorForeignKeyStructure(this, fk));
            }
            DisplayColumns();
            DisplayIndexes();
            DisplayForeignKeys();
            DisplayChecks();
            m_modified = false;
            EnableButtons();
            ShowAlterSql();
        }

        private void DisplayChecks()
        {
            checkGrid.Rows.Clear();
            foreach (EditorCheckStructure edch in m_checks)
            {
                int newrow = checkGrid.Rows.Add(edch.Name, edch.Expression);
                checkGrid.Rows[newrow].Tag = edch;
            }
        }

        private void DisplayForeignKeys()
        {
            fkGrid.Rows.Clear();
            System.Collections.IEnumerable en = null;
            if (rbowned.Checked) en = m_foreignKeys;
            if (rbreferenced.Checked) en = m_refKeys;
            foreach (EditorForeignKeyStructure edfk in en)
            {
                NameWithSchema src = edfk.Table, dst = edfk.PrimaryKeyTable;
                if (HideSchema)
                {
                    if (src != null) src = src.GetNameWithHiddenSchema();
                    if (dst != null) dst = dst.GetNameWithHiddenSchema();
                }
                int newrow = fkGrid.Rows.Add(edfk.Name, src != null ? src.ToString() : "", dst != null ? dst.ToString() : "", edfk.GetColumns());
                fkGrid.Rows[newrow].Tag = edfk;
            }
        }

        private void DisplayIndexes()
        {
            indexGrid.Rows.Clear();
            foreach (EditorIndexOrKeyStructure edidx in m_indexOrKeys)
            {
                int newrow = indexGrid.Rows.Add(edidx.Name, edidx.Type.ToString(), edidx.GetColumns());
                indexGrid.Rows[newrow].Tag = edidx;
            }
        }

        int m_displayingColumns = 0;
        private void DisplayColumns()
        {
            if (m_displayingColumns > 0) return;
            m_displayingColumns++;

            try
            {
                columnsGrid.Rows.Clear();
                EditorIndexOrKeyStructure pk = FindPrimaryKey();
                foreach (EditorColumnStructure edcol in m_columns)
                {
                    string typecode;
                    if (edcol.Domain != null) typecode = edcol.Domain.FullName.ToString();
                    else if (edcol.DataType is ISpecificType) typecode = ((ISpecificType)edcol.DataType).Code.ToString();
                    else typecode = ((DbTypeBase)edcol.DataType).Code.ToString();

                    NameWithSchema fktable = null;
                    foreach (EditorForeignKeyStructure edfk in m_foreignKeys)
                    {
                        if (edfk.Columns.Count == 1 && edfk.Columns[0].SrcName.ColumnName == edcol.ColumnName) fktable = edfk.PrimaryKeyTable;
                        if (edfk.Columns.Count > 1 && edfk.FindColumn(edcol.ColumnName) != null) fktable = new NameWithSchema("???");
                    }
                    bool ispk = false;
                    if (pk != null && pk.Columns.IndexOfIf(cr => cr.ColumnName == edcol.ColumnName) >= 0) ispk = true;
                    DbTypeBase tp = null;
                    if (edcol.DataType is DbTypeBase) tp = (DbTypeBase)edcol.DataType;
                    if (edcol.DataType is ISpecificType) tp = ((ISpecificType)edcol.DataType).ToGenericType();
                    bool isautoinc = tp.IsAutoIncrement();
                    if (HideSchema && fktable != null) fktable = fktable.GetNameWithHiddenSchema();
                    string len = GetLengthText(tp);
                    int newrow = columnsGrid.Rows.Add(edcol.ColumnName, typecode, edcol.IsNullable, ispk, len, isautoinc, (object)fktable ?? (object)"-");
                    columnsGrid.Rows[newrow].Tag = edcol;
                }
                DisplayColumnsCombos();
            }
            finally
            {
                m_displayingColumns--;
            }
        }

        private void DisplayColumnsCombos()
        {
            lbixaddcols.Items.Clear();
            foreach (EditorColumnStructure edcol in m_columns)
            {
                lbixaddcols.Items.Add(edcol.ColumnName);
            }
        }

        private void columnsGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Logging.Warning("Data error, row={0}, col={1}, error={2}", e.RowIndex, e.ColumnIndex, e.Exception.Message);
        }

        private void SetModified() { m_modified = true; }

        private EditorIndexOrKeyStructure FindPrimaryKey()
        {
            foreach (EditorIndexOrKeyStructure eik in m_indexOrKeys)
            {
                if (eik.Type == EditorIndexOrKeyType.PrimaryKey)
                {
                    return eik;
                }
            }
            return null;
        }

        private void SetColumnType(string value, EditorColumnStructure col, DataGridViewRow row)
        {
            bool isdomain = false;
            bool oldautoinc = GetGenericType(col.DataType).IsAutoIncrement();
            foreach (var dom in Domains.Values)
            {
                if (String.Compare(dom.FullName.ToString(), value, true) == 0)
                {
                    isdomain = true;
                    col.DataType = dom.DataType;
                    col.IsNullable = dom.IsNullable;
                    col.Domain = dom;
                    row.Cells[COL_LENGTH].Value = GetLengthText(GetGenericType(col.DataType));
                    row.Cells[COL_AUTOINC].Value = GetGenericType(col.DataType).IsAutoIncrement();
                    break;
                }
            }
            if (!isdomain)
            {
                object depcode = Enum.Parse(EnumType, value.ToString(), true);
                DbTypeBase gen = null;
                if (m_dialect != null) gen = m_dialect.CreateSpecificTypeInstance(depcode).ToGenericType();
                else gen = DbTypeBase.CreateType((DbTypeCode)depcode);
                gen.SetAutoincrement(oldautoinc);
                col.DataType = GetSpecificType(gen);
                row.Cells[COL_LENGTH].Value = GetLengthText(gen);
                row.Cells[COL_AUTOINC].Value = gen.IsAutoIncrement();
            }
        }

        private void columnsGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!m_initialized) return;
            EditorColumnStructure col = (EditorColumnStructure)columnsGrid.Rows[e.RowIndex].Tag;
            if (col == null) return;
            DataGridViewCell cell = columnsGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
            object value = cell.Value;
            switch (e.ColumnIndex)
            {
                case COL_NAME:
                    if (value == null) col.ColumnName = "";
                    else col.ColumnName = value.ToString();
                    break;
                case COL_TYPE:
                    SetColumnType(value.ToString(), col, columnsGrid.Rows[e.RowIndex]);
                    break;
                case COL_NULLABLE:
                    col.IsNullable = (bool)value;
                    break;
                case COL_ISPK:
                    if ((bool)value)
                    {
                        EditorIndexOrKeyStructure pk = FindPrimaryKey();
                        if (pk == null)
                        {
                            pk = new EditorIndexOrKeyStructure(GetTableName(), m_dialect, this);
                            pk.Type = EditorIndexOrKeyType.PrimaryKey;
                            pk.CreateName(m_dialect);
                            pk.Columns.Add(new ColumnReference(col.ColumnName));
                            m_indexOrKeys.Add(pk);
                        }
                        else
                        {
                            pk.Columns.Add(new ColumnReference(col.ColumnName));
                        }
                    }
                    else
                    {
                        EditorIndexOrKeyStructure pk = FindPrimaryKey();
                        if (pk != null)
                        {
                            if (pk.Columns.Count > 1) pk.Columns.RemoveIf(cr => cr.ColumnName == col.ColumnName);
                            else m_indexOrKeys.Remove(pk);
                        }
                    }
                    using (GridPosition gp = new GridPosition(columnsGrid))
                    {
                        DisplayColumns();
                    }
                    DisplayIndexes();
                    break;
                case COL_FK:
                    // remove  foreign key
                    if (value.ToString() != "???")
                    {
                        foreach (EditorForeignKeyStructure edfk in m_foreignKeys)
                        {
                            if (edfk.Columns.Count == 1 && edfk.Columns[0].SrcName.ColumnName == col.ColumnName)
                            {
                                m_foreignKeys.Remove(edfk);
                                break;
                            }
                        }
                    }

                    // add new foreign key
                    if (value.ToString() != "???" && value.ToString() != "-")
                    {
                        NameWithSchema tblname = new NameWithSchema(value.ToString());
                        if (HideSchema)
                        {
                            foreach (NameWithSchema t in TableNames)
                            {
                                if (t.Name == value.ToString())
                                {
                                    tblname = new NameWithSchema(t.Schema, t.Name, true);
                                }
                            }
                        }
                        string pkcolname = TableStructureExtension.PrimaryKeyColumnName(TableStructure(tblname));
                        if (pkcolname != null)
                        {
                            EditorForeignColumnStructure fkcol = new EditorForeignColumnStructure();
                            EditorForeignKeyStructure fk = new EditorForeignKeyStructure(GetTableName(), m_dialect, this);
                            fk.PrimaryKeyTable = tblname;
                            fkcol.DstName = new ColumnReference(pkcolname);
                            fkcol.SrcName = new ColumnReference(col.ColumnName);
                            fk.Columns.Add(fkcol);
                            fk.SetTable(GetTableName(), m_dialect);
                            m_foreignKeys.Add(fk);
                        }
                        else
                        {
                            StdDialog.ShowError(Texts.Get("s_target_table_has_no_primary_key"));
                        }
                    }

                    DisplayForeignKeys();
                    using (GridPosition gp = new GridPosition(columnsGrid))
                    {
                        DisplayColumns();
                    }
                    break;
                case COL_LENGTH:
                    {
                        if (col.DataType is ISpecificType)
                        {
                            DbTypeBase tp = ((ISpecificType)col.DataType).ToGenericType();
                            if (tp is DbTypeString)
                            {
                                try
                                {
                                    ((DbTypeString)tp).Length = Int32.Parse(value.ToString());
                                }
                                catch
                                {
                                    cell.Value = ((DbTypeString)tp).Length.ToString();
                                }
                                col.DataType = m_dialect != null ? m_dialect.GenericTypeToSpecific(tp) : tp;
                            }
                        }
                        else if (col.DataType is DbTypeString)
                        {
                            try
                            {
                                ((DbTypeString)col.DataType).Length = Int32.Parse(value.ToString());
                            }
                            catch
                            {
                                cell.Value = ((DbTypeString)col.DataType).Length.ToString();
                            }
                        }
                    }
                    break;
                case COL_AUTOINC:
                    {
                        var gen = GetGenericType(col.DataType);
                        bool oldval = gen.IsAutoIncrement();
                        gen.SetAutoincrement(!gen.IsAutoIncrement());
                        if (gen.IsAutoIncrement() != oldval)
                        {
                            col.DataType = GetSpecificType(gen);
                        }
                    }
                    break;
            }
            AfterColumnsCellChange();
        }

        private void AfterColumnsCellChange()
        {
            propertyFrame1.ReloadData();
            DisplayColumnsCombos();
            SetModified();
            ShowAlterSql();
        }

        private NameWithSchema GetTableName()
        {
            if (m_originalTable != null) return m_originalTable.FullName;
            return new NameWithSchema("newtable");
        }

        //public AlterTableCaps AlterTableCaps
        //{
        //    get
        //    {
        //        AlterTableCaps caps;
        //        if (Behaviour != null) return Behaviour.AlterTableCaps;
        //        if (m_dialect != null && m_dialect.AlterTableCaps != null) caps = m_dialect.AlterTableCaps;
        //        else caps = new AlterTableCaps();
        //        return caps;
        //    }
        //}

        private DbTypeBase GetGenericType(object type)
        {
            var spec = type as ISpecificType;
            if (spec != null) return spec.ToGenericType();
            return (DbTypeBase)type;
        }

        private object GetSpecificType(DbTypeBase type)
        {
            return m_dialect != null ? m_dialect.GenericTypeToSpecific(type) : type;
        }

        private string GetLengthText(DbTypeBase type)
        {
            if (type is DbTypeString) return ((DbTypeString)type).Length.ToString();
            return "";
        }

        private void columnsGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            AlterProcessorCaps caps = m_conn.AlterCaps;
            EditorColumnStructure col = (EditorColumnStructure)columnsGrid.Rows[e.RowIndex].Tag;
            if (col == null) return;
            switch (e.ColumnIndex)
            {
                case COL_NAME:
                    if (col.IsOld && !caps.RenameColumn && !AllowRecreate)
                        e.Cancel = true;
                    break;
                case COL_TYPE:
                case COL_NULLABLE:
                    if (col.IsOld && !caps.ChangeColumnType && !AllowRecreate)
                        e.Cancel = true;
                    break;
                case COL_LENGTH:
                    if (col.IsOld && !caps.ChangeColumnType && !AllowRecreate)
                        e.Cancel = true;
                    if (!(GetGenericType(col.DataType) is DbTypeString))
                        e.Cancel = true;
                    break;
                case COL_AUTOINC:
                    if (col.IsOld && !caps.ChangeAutoIncrement && !AllowRecreate)
                        e.Cancel = true;
                    break;
            }
        }

        private bool HideSchema
        {
            get
            {
                return !m_conn.DatabaseCaps.MultipleSchema;
            }
        }

        private void columnsGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            propertyFrame1.SelectedObject = columnsGrid.Rows[e.RowIndex].Tag;
        }

        private TableStructure CreateStructure(NameWithSchema name)
        {
            DatabaseStructure resdb = new DatabaseStructure();
            TableStructure res = new TableStructure();
            resdb.Tables.Add(res);
            if (m_originalTable != null) res.GroupId = m_originalTable.GroupId;
            if (m_columns.Count == 0) throw new InconsistentTableStructureError("DAE-00164 Table must have at least one column");
            List<string> excols = new List<string>();
            foreach (EditorColumnStructure col in m_columns)
            {
                if (excols.Contains(col.ColumnName.ToLower())) throw new InconsistentTableStructureError("DAE-00165 Duplicate column name:" + col.ColumnName);
                excols.Add(col.ColumnName.ToLower());
                col.CheckConsistency();
                col.SetTable(res);
                DbTypeBase type;
                if (col.DataType is ISpecificType) type = ((ISpecificType)col.DataType).ToGenericType();
                else type = (DbTypeBase)col.DataType;
                ColumnStructure newcol = res.AddColumn(col.ColumnName, type);

                if (col.Domain != null)
                {
                    newcol.DataType = col.Domain.DataType;
                    newcol.Domain = col.Domain.FullName;
                }

                newcol.IsNullable = col.IsNullable;
                newcol.DefaultValue = SqlExpression.ParseDefaultValue(col.DefaultValue, m_dialect);
                newcol.CharacterSet = col.CharacterSet;
                newcol.Collation = col.Collation;
                if (col.m_originalGroupId != null) newcol.GroupId = col.m_originalGroupId;
            }
            res.FullName = name == null ? GetTableName() : name;

            foreach (EditorIndexOrKeyStructure cnt in m_indexOrKeys)
            {
                cnt.SetTable(res.FullName, m_dialect);

                res._Constraints.Add(cnt.CreateConstraint());
            }
            foreach (EditorForeignKeyStructure fk in m_foreignKeys)
            {
                fk.SetTable(res.FullName, m_dialect);
                res._Constraints.Add(fk.CreateConstraint());
            }
            foreach (EditorForeignKeyStructure fk in m_refKeys)
            {
                fk.SetPkTable(res.FullName, m_dialect);
                resdb.FindOrCreateTable(fk.Table)._Constraints.Add(fk.CreateConstraint());
                //res._ReferencedFrom.Add((ForeignKey)fk.CreateConstraint());
            }
            foreach (EditorCheckStructure chk in m_checks)
            {
                chk.SetTable(res.FullName, m_dialect);
                res._Constraints.Add((CheckConstraint)chk.CreateConstraint());
            }

            if (m_specificEditor != null) m_specificEditor.SaveToStructure(res);
            return res;
        }

        public override bool SupportsSave { get { return true; } }

        //private string GenerateAlterSql()
        //{
        //    if (m_dialect != null)
        //    {
        //        TableStructure newStructure = CreateStructure(null);
        //    }
        //    return "";
        //}

        private AlterPlan GenerateAlterPlan(TableStructure newStructure, DbDiffOptions opts)
        {
            AlterPlan plan = DbDiffTool.PlanAlterTable(m_originalTable, newStructure, opts);
            plan.AddLogicalDependencies(m_conn.AlterCaps, opts, m_conn);
            plan.Transform(m_conn.AlterCaps, opts, m_conn);
            return plan;
        }

        private void DoGenerateAlterSql(TableStructure newStructure)
        {
            var opts = GetDbDiffOptions();
            m_messagesPreview = new CachingLogger(LogLevel.Info);
            opts.AlterLogger = m_messagesPreview;
            var plan = GenerateAlterPlan(newStructure, opts);
            m_sqlPreview = m_dialect.GenerateScript(dmp => plan.CreateRunner().Run(dmp, opts));
        }

        private void GeneratedAlterSql(IAsyncResult async)
        {
            try
            {
                m_conn.Connection.EndInvoke(async);
                tbxAlterSql.Text = m_sqlPreview;
            }
            catch (Exception e)
            {
                tbxAlterSql.Text = "";
                m_messagesPreview = new CachingLogger(LogLevel.Info);
                m_messagesPreview.Error(Errors.ExtractImportantException(e).Message);
            }
            messageLogFrame1.Source = m_messagesPreview;
        }

        private void ShowAlterSql()
        {
            if (!DatabaseConnection.DatabaseCaps.ExecuteSql) return;
            if (m_dialect == null) return;
            try
            {
                TableStructure newStructure = CreateStructure(null);
                m_conn.Connection.BeginInvoke((Action)(() => { DoGenerateAlterSql(newStructure); }), Async.CreateInvokeCallback(m_invoker, GeneratedAlterSql));
            }
            catch (InconsistentTableStructureError)
            {
                // do nothing
            }
        }

        public override bool Save()
        {
            PrepareToSave();
            if (m_originalTable == null)
            {
                return SaveAs();
            }
            else
            {
                try
                {
                    TableStructure newStructure = CreateStructure(null);
                    string alterSql = null;
                    var opts = GetDbDiffOptions();
                    var log = new CachingLogger(LogLevel.Info);
                    opts.AlterLogger = log;
                    var plan = GenerateAlterPlan(newStructure, opts);
                    alterSql = m_dialect.GenerateScript(dmp => plan.CreateRunner().Run(dmp, opts));
                    m_settings.WantLoaded();
                    if (DatabaseConnection.DatabaseCaps.ExecuteSql && (m_settings.ShowSqlConfirm || log.Count > 0) && !SqlConfirmForm.Run(alterSql, m_dialect ?? GenericDialect.Instance, log, AlterTableSettings.ShowSqlConfirmKey)) return false;
                    if (DatabaseConnection.DatabaseCaps.ExecuteSql)
                    {
                        m_conn.InvokeScript(dmp => plan.CreateRunner().Run(dmp, opts), null);
                    }
                    else
                    {
                        m_conn.AlterDatabase(plan, opts);
                        //m_conn.AlterObject(m_originalTable, newStructure, GetDbDiffOptions());
                    }
                    Usage.AddSub("alter_table", m_conn.ToString(), alterSql);
                    if (m_pars != null && m_pars.SavedCallback != null) m_pars.SavedCallback();

                    m_settings.WantLoaded();
                    if (ShowAlteredInfoDialog && m_settings.ShowAlterResult) MessageBoxWithHide.Run("s_table_altered", AlterTableSettings.ShowAlterResultKey);
                    m_originalTable = m_conn.InvokeLoadTableStructure(newStructure.FullName, TableStructureMembers.All).CloneWithDb();
                    FillFromStructure();
                    return true;
                }
                catch (Exception e)
                {
                    Errors.Report(e);
                    return false;
                }
            }
        }

        private DbDiffOptions GetDbDiffOptions()
        {
            return new DbDiffOptions { AllowRecreateTable = AllowRecreate, AllowRecreateConstraint = AllowRecreateConstraint };
        }

        [PopupMenu("s_refresh", ImageName = CoreIcons.refreshName)]
        public void RefreshData()
        {
            if (MainWindow.Instance.ProcessRefreshMessage()) return;
            if (!AllowClose()) return;
            try
            {
                m_domains = null;
                m_originalTable = m_conn.InvokeLoadTableStructure(m_originalTable.FullName, TableStructureMembers.All).CloneWithDb();
                m_tables.Clear();
                FillFromStructure();
            }
            catch (Exception e)
            {
                Errors.Report(e);
            }
        }

        private void PrepareToSave()
        {
            // this is neccessary so that changes in grids are saved
            tabControl1.Focus();
        }

        public override bool SupportsSaveAs { get { return true; } }
        public override bool SaveAs()
        {
            PrepareToSave();

            NameWithSchema name = NameWithSchemaForm.Run(
                m_conn.InvokeLoadSchemata(),
                m_conn.DatabaseCaps.MultipleSchema,
                new NameWithSchema(m_conn.DefaultSchema, "new_table"));
            //string name = InputBox.Run(Texts.Get("s_enter_table_name"), "new_table");
            if (name != null)
            {
                name = m_dialect.MigrateFullName(name);
                try
                {
                    TableStructure newStructure = CreateStructure(name);
                    string createSql = null;
                    if (DatabaseConnection.DatabaseCaps.ExecuteSql)
                    {
                        createSql = m_dialect.GenerateScript(dmp => dmp.CreateTable(newStructure));
                        if (!SqlConfirmForm.Run(createSql, m_dialect)) return false;
                    }
                    m_conn.CreateObject(newStructure);
                    Usage.AddSub("create_table", m_conn.ToString(), createSql);
                    if (m_pars != null && m_pars.SavedCallback != null) m_pars.SavedCallback();
                    m_originalTable = m_conn.InvokeLoadTableStructure(newStructure.FullName, TableStructureMembers.All).CloneWithDb();
                    FillFromStructure();
                    UpdateTitle();
                    return true;
                }
                catch (Exception err)
                {
                    Errors.Report(err);
                }
            }
            return false;
        }

        public override string MenuBarTitle
        {
            get { return "s_table"; }
        }

        private void columnsGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            m_columns.RemoveAt(e.Row.Index);
            SetModified();
        }

        private void ExchangeColumns(int i1, int i2)
        {
            if (i1 >= 0 && i1 < m_columns.Count && i2 >= 0 && i2 < m_columns.Count)
            {
                int lastcol = columnsGrid.CurrentCell.ColumnIndex;
                m_columns.Exchange(i1, i2);
                DisplayColumns();
                columnsGrid.CurrentCell = columnsGrid.Rows[i1].Cells[lastcol];
            }
            SetModified();
        }

        bool AllowPermuteColumns()
        {
            bool isnew = m_originalTable == null;
            if (isnew) return true;
            if (AllowRecreate) return true;
            if (!m_conn.AlterCaps.PermuteColumns)
            {
                StdDialog.ShowError(Texts.Get("s_cannot_permute"));
                return false;
            }
            return true;
        }

        private void MoveItem(int d)
        {
            if (tabControl1.SelectedIndex == PAGE_COLUMNS)
            {
                if (!AllowPermuteColumns() || columnsGrid.CurrentCell == null) return;
                int index = columnsGrid.CurrentCell.RowIndex;
                ExchangeColumns(index + d, index);
            }
            else if (tabControl1.SelectedIndex == PAGE_INDEXESKEYS)
            {
                EditorIndexOrKeyStructure ik = CurrentIndexOrKey;
                if (ik != null)
                {
                    int curindex = lbixcols.SelectedIndex;
                    if (curindex >= 0 && curindex < lbixcols.Items.Count && curindex + d >= 0 && curindex + d < lbixcols.Items.Count)
                    {
                        ik.Columns.Exchange(curindex, curindex + d);
                        FillIndexColumns(GetRowIndex(ik));
                    }
                }
            }
            ShowAlterSql();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MoveItem(-1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MoveItem(1);
        }

        private void indexGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Logging.Warning("Data error, row={0}, col={1}, error={2}", e.RowIndex, e.ColumnIndex, e.Exception.Message);
        }

        private void indexGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            propertyFrame1.SelectedObject = indexGrid.Rows[e.RowIndex].Tag;
            FillIndexColumns(e.RowIndex);
        }

        private void FillIndexColumns(int rowindex)
        {
            EditorIndexOrKeyStructure ixk = (EditorIndexOrKeyStructure)indexGrid.Rows[rowindex].Tag;
            bool allowCahnge = ixk != null ? ixk.AllowChange : false;
            propertyFrame1.SelectedObject = ixk;
            lbixcols.Items.Clear();
            if (ixk != null)
            {
                foreach (var colref in ixk.Columns) lbixcols.Items.Add(colref.ColumnName);
                btnAddIndexCol.Enabled = btnDeleteIndexCol.Enabled = allowCahnge;
            }
        }

        private void EnableButtons()
        {
            bool isnew = m_originalTable == null;
            var caps = m_conn.AlterCaps;
            if (tabControl1.SelectedIndex == PAGE_COLUMNS)
            {
                btnMoveDown.Enabled = isnew || caps.PermuteColumns || AllowRecreate;
                btnMoveUp.Enabled = isnew || caps.PermuteColumns || AllowRecreate;
                btnAdd.Enabled = isnew || caps.AddColumn || AllowRecreate;
                btnRemove.Enabled = isnew || caps.DropColumn || AllowRecreate;
            }
            else
            {
                bool changecnt = AllowRecreate || AllowRecreateConstraint;
                btnMoveDown.Enabled = isnew || changecnt;
                btnMoveUp.Enabled = isnew || changecnt;
                btnAdd.Enabled = isnew || changecnt;
                btnRemove.Enabled = isnew || changecnt;
            }
            rbreferenced.Enabled = rbowned.Enabled = tabControl1.SelectedIndex == PAGE_RELATIONS;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedIndex == PAGE_COLUMNS)
                {
                    propertyFrame1.SelectedObject = columnsGrid.CurrentRow.Tag;
                }
                else if (tabControl1.SelectedIndex == PAGE_INDEXESKEYS)
                {
                    propertyFrame1.SelectedObject = indexGrid.CurrentRow.Tag;
                }
                else if (tabControl1.SelectedIndex == PAGE_RELATIONS)
                {
                    propertyFrame1.SelectedObject = fkGrid.CurrentRow.Tag;
                }
                else if (tabControl1.SelectedIndex == PAGE_CHECKS)
                {
                    propertyFrame1.SelectedObject = checkGrid.CurrentRow.Tag;
                }
            }
            catch (Exception)
            {
                propertyFrame1.SelectedObject = null;
            }
            EnableButtons();
        }

        private void fkgrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (m_machineChangingFkGrid) return;
            EditorForeignKeyStructure efk = (EditorForeignKeyStructure)fkGrid.Rows[e.RowIndex].Tag;
            propertyFrame1.SelectedObject = efk;
            MainWindow.Instance.RunInMainWindow(() => { FillFkColumns(efk); });
        }

        private void FillFkColumns(EditorForeignKeyStructure fk)
        {
            fkcolsPanel.Visible = false;
            bool allowChange = fk != null ? fk.AllowChange : false;
            allowChange &= rbowned.Checked;
            btnAddFkCol.Enabled = btnRemoveFkCol.Enabled = pktablename.Enabled = allowChange;
            m_selectedForeignKeyColumn = null;
            foreach (Control ctrl in fkcolsPanel.Controls)
            {
                var combo = ctrl as ComboBox;
                if (combo != null)
                {
                    EditorForeignColumnStructure col = (EditorForeignColumnStructure)combo.Tag;
                    col.SourceCombo = null;
                    col.TargetCombo = null;
                    combo.Tag = null;
                }
            }
            fkcolsPanel.Controls.Clear();
            if (fk != null)
            {
                int acty = 0;
                foreach (EditorForeignColumnStructure col in fk.Columns)
                {
                    Label bglab = new Label();
                    bglab.AutoSize = false;
                    bglab.BackColor = SystemColors.Highlight;
                    bglab.Visible = false;
                    bglab.Tag = col;
                    fkcolsPanel.Controls.Add(bglab);

                    ComboBox src = new ComboBox();
                    ComboBox dst = new ComboBox();
                    bglab.SetBounds(0, acty, fkcolsPanel.Width, src.Height + 2);

                    src.Enabled = dst.Enabled = allowChange;
                    col.SourceCombo = src;
                    col.TargetCombo = dst;
                    fkcolsPanel.Controls.Add(src);
                    fkcolsPanel.Controls.Add(dst);
                    src.SetBounds(1, acty + 1, fkcolsPanel.Width / 2, src.Height);
                    dst.SetBounds(fkcolsPanel.Width / 2, acty + 1, fkcolsPanel.Width / 2 - 1, src.Height);
                    src.DropDownStyle = ComboBoxStyle.DropDownList;
                    dst.DropDownStyle = ComboBoxStyle.DropDownList;
                    if (rbowned.Checked)
                    {
                        foreach (EditorColumnStructure scol in m_columns)
                        {
                            src.Items.Add(scol.ColumnName);
                        }
                    }
                    else
                    {
                        src.Enabled = false;
                        foreach (string scol in ColumnNames(fk.Table))
                        {
                            src.Items.Add(scol);
                        }
                    }
                    foreach (string dcol in ColumnNames(fk.PrimaryKeyTable))
                    {
                        dst.Items.Add(dcol);
                    }
                    src.SelectedIndex = src.Items.IndexOf(col.SrcName.ColumnName);
                    dst.SelectedIndex = dst.Items.IndexOf(col.DstName.ColumnName);
                    if (dst.SelectedIndex == 0) dst.Text = col.DstName.ColumnName;
                    src.SelectedIndexChanged += new EventHandler(src_SelectedIndexChanged);
                    dst.SelectedIndexChanged += new EventHandler(dst_SelectedIndexChanged);
                    src.Tag = col;
                    dst.Tag = col;
                    acty += Math.Max(src.Height, dst.Height) + 1;
                    src.Enter += new EventHandler(srcdst_Enter);
                    dst.Enter += new EventHandler(srcdst_Enter);
                }
            }

            foreach (var ctrl in fkcolsPanel.Controls)
            {
                var label = ctrl as Label;
                if (label != null) label.SendToBack();
            }

            pktablename.Items.Clear();
            foreach (NameWithSchema tbl in TableNames.Sorted()) pktablename.Items.Add(tbl);
            if (fk != null && fk.PrimaryKeyTable != null)
            {
                noRectOnpktablename_SelectedIndexChange = true;
                pktablename.SelectedIndex = pktablename.Items.IndexOf(fk.PrimaryKeyTable);
                noRectOnpktablename_SelectedIndexChange = false;
            }

            fktablename.Text = (fk != null && fk.Table != null ? fk.Table : new NameWithSchema("")).ToString();
            fkcolsPanel.Visible = true;
        }

        void srcdst_Enter(object sender, EventArgs e)
        {
            foreach (var ctrl in fkcolsPanel.Controls)
            {
                var label = ctrl as Label;
                if (label != null) label.Visible = false;
            }
            ComboBox s = (ComboBox)sender;
            EditorForeignColumnStructure col = (EditorForeignColumnStructure)s.Tag;
            m_selectedForeignKeyColumn = col;

            foreach (var ctrl in fkcolsPanel.Controls)
            {
                var label = ctrl as Label;
                if (label != null && label.Tag == col) 
                {
                    label.Visible = true;
                    break;
                }
            }
        }

        void dst_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox s = (ComboBox)sender;
            EditorForeignColumnStructure col = (EditorForeignColumnStructure)s.Tag;
            col.DstName = new ColumnReference(s.Text);
            RefreshFk(col.Fk, false);
        }

        void src_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox s = (ComboBox)sender;
            EditorForeignColumnStructure col = (EditorForeignColumnStructure)s.Tag;
            col.SrcName = new ColumnReference(s.Text);
            RefreshFk(col.Fk, false);
        }

        bool m_machineChangingIndexGrid = false;
        void ReloadIndexNames()
        {
            try
            {
                m_machineChangingIndexGrid = true;

                foreach (DataGridViewRow row in indexGrid.Rows)
                {
                    var ik = (EditorIndexOrKeyStructure)row.Tag;
                    row.Cells[0].Value = ik.Name;
                }
            }
            finally
            {
                m_machineChangingIndexGrid = false;
            }
        }

        private void indexGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (m_machineChangingIndexGrid) return;
            if (!m_initialized) return;
            EditorIndexOrKeyStructure ik = (EditorIndexOrKeyStructure)indexGrid.Rows[e.RowIndex].Tag;
            if (ik == null) return;
            object value = indexGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            switch (e.ColumnIndex)
            {
                case 0:
                    ik.Name = value.ToString();
                    break;
                case 1:
                    object type = Enum.Parse(typeof(EditorIndexOrKeyType), value.ToString(), true);
                    ik.Type = (EditorIndexOrKeyType)type;
                    break;
            }
            propertyFrame1.ReloadData();
            SetModified();
            ShowAlterSql();
            ReloadIndexNames();
        }

        public override string PageTitle
        {
            get
            {
                return m_originalTable != null ? m_originalTable.Name : Texts.Get("s_new_table");
            }
        }

        public override string PageToolTip
        {
            get
            {
                return String.Format("{0}\n{1:M}\n{2:M}", Texts.Get("s_editing"), m_originalTable ?? (object)Texts.Get("s_new_table"), m_conn);
            }
        }

        public override string PageTypeTitle
        {
            get
            {
                return "s_edit_table";
            }
        }

        public override Bitmap Image
        {
            get { return CoreIcons.table_edit; }
        }

        private void propertyFrame1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            ShowAlterSql();
            object o = propertyFrame1.SelectedObject;
            if (tabControl1.SelectedIndex == PAGE_COLUMNS)
            {
                using (GridPosition gp = new GridPosition(columnsGrid))
                    DisplayColumns();
            }
            else if (tabControl1.SelectedIndex == PAGE_INDEXESKEYS)
            {
                using (GridPosition gp = new GridPosition(indexGrid))
                    DisplayIndexes();
            }
            else if (tabControl1.SelectedIndex == PAGE_RELATIONS)
            {
                using (GridPosition gp = new GridPosition(fkGrid))
                    DisplayForeignKeys();
            }
            else if (tabControl1.SelectedIndex == PAGE_CHECKS)
            {
                using (GridPosition gp = new GridPosition(checkGrid))
                    DisplayChecks();
            }
            propertyFrame1.SelectedObject = o;
        }

        EditorIndexOrKeyStructure CurrentIndexOrKey
        {
            get
            {
                if (indexGrid.CurrentRow == null) return null;
                int rowindex = indexGrid.CurrentRow.Index;
                EditorIndexOrKeyStructure ik = (EditorIndexOrKeyStructure)indexGrid.Rows[rowindex].Tag;
                return ik;
            }
        }

        int GetRowIndex(EditorIndexOrKeyStructure ik)
        {
            int index = 0;
            foreach (DataGridViewRow row in indexGrid.Rows)
            {
                if (row.Tag == ik) return index;
                index++;
            }
            return -1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EditorIndexOrKeyStructure ik = CurrentIndexOrKey;
            if (ik == null) return;
            if (lbixaddcols.Text != null && lbixaddcols.Text != "") ik.Columns.Add(new ColumnReference(lbixaddcols.Text));
            using (GridPosition gp = new GridPosition(indexGrid)) DisplayIndexes();
            FillIndexColumns(GetRowIndex(ik));
            ShowAlterSql();
            ReloadIndexNames();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (indexGrid.CurrentRow == null) return;
            int rowindex = indexGrid.CurrentRow.Index;
            EditorIndexOrKeyStructure ik = CurrentIndexOrKey;
            if (ik == null) return;
            if (lbixcols.SelectedIndex >= 0) ik.Columns.RemoveAt(lbixcols.SelectedIndex);
            using (GridPosition gp = new GridPosition(indexGrid)) DisplayIndexes();
            FillIndexColumns(rowindex);
            ShowAlterSql();
            ReloadIndexNames();
        }

        private void checkGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            propertyFrame1.SelectedObject = checkGrid.Rows[e.RowIndex].Tag;
        }

        private void pktablename_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (noRectOnpktablename_SelectedIndexChange) return;
            EditorForeignKeyStructure fk = (EditorForeignKeyStructure)fkGrid.CurrentRow.Tag;
            if (fk != null) fk.PrimaryKeyTable = (NameWithSchema)pktablename.SelectedItem;
            RefreshFk(fk, true);
        }

        private void RefreshFk(EditorForeignKeyStructure fk, bool fillfkcols)
        {
            try
            {
                m_machineChangingFkGrid = true;
                using (GridPosition gp = new GridPosition(fkGrid))
                {
                    DisplayForeignKeys();
                }
            }
            finally
            {
                m_machineChangingFkGrid = false;
            }
            ShowAlterSql();
            if (fillfkcols) FillFkColumns(fk);
            propertyFrame1.SelectedObject = fk;
            ReloadFkNames();
        }

        private void btnAddFkCol_Click(object sender, EventArgs e)
        {
            if (fkGrid.CurrentRow == null) return;
            EditorForeignKeyStructure fk = (EditorForeignKeyStructure)fkGrid.CurrentRow.Tag;
            fk.Columns.Add(new EditorForeignColumnStructure());
            RefreshFk(fk, true);
        }

        private void btnRemoveFkCol_Click(object sender, EventArgs e)
        {
            if (fkGrid.CurrentRow == null) return;
            EditorForeignKeyStructure fk = (EditorForeignKeyStructure)fkGrid.CurrentRow.Tag;
            if (m_selectedForeignKeyColumn != null) fk.Columns.Remove(m_selectedForeignKeyColumn);
            RefreshFk(fk, true);
        }

        void AddIndexOrKey()
        {
            EditorIndexOrKeyStructure ixk = new EditorIndexOrKeyStructure(GetTableName(), m_dialect, this);
            m_indexOrKeys.Add(ixk);
            using (GridPosition gp = new GridPosition(indexGrid)) DisplayIndexes();
        }

        void RemoveIndexOrKey(int index)
        {
            m_indexOrKeys.RemoveAt(index);
            SetModified();
            DisplayIndexes();
        }

        void RemoveIndexOrKey()
        {
            if (indexGrid.CurrentCell == null) return;
            int index = indexGrid.CurrentCell.RowIndex;
            RemoveIndexOrKey(index);
        }

        void AddRelation()
        {
            EditorForeignKeyStructure fk = new EditorForeignKeyStructure(GetTableName(), m_dialect, this);
            m_foreignKeys.Add(fk);
            using (GridPosition gp = new GridPosition(fkGrid))
            {
                DisplayForeignKeys();
                DisplayColumns();
            }
        }

        void AddCheck()
        {
            EditorCheckStructure check = new EditorCheckStructure(GetTableName(), m_dialect, this);
            m_checks.Add(check);
            using (GridPosition gp = new GridPosition(checkGrid)) DisplayChecks();
        }

        void RemoveCheck(int index)
        {
            m_checks.RemoveAt(index);
            SetModified();
            DisplayChecks();
        }

        void RemoveCheck()
        {
            if (checkGrid.CurrentCell == null) return;
            int index = checkGrid.CurrentCell.RowIndex;
            RemoveCheck(index);
        }

        void RemoveRelation(int index)
        {
            if (rbowned.Checked) m_foreignKeys.RemoveAt(index);
            if (rbreferenced.Checked) m_refKeys.RemoveAt(index);
            DisplayForeignKeys();
            DisplayColumns();
            SetModified();
        }

        void RemoveRelation()
        {
            if (fkGrid.CurrentCell == null) return;
            int index = fkGrid.CurrentCell.RowIndex;
            RemoveRelation(index);
        }

        public override bool AllowClose()
        {
            if (Modified)
            {
                DialogResult dr = MessageBox.Show(Texts.Get("s_close_table_save_q"), VersionInfo.ProgramTitle, MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Yes)
                {
                    return Save();
                }
                if (dr == DialogResult.No) return true;
                return false;
            }
            return true;
        }

        public void AddColumn()
        {

            object type;
            if (m_dialect != null) type = m_dialect.GenericTypeToSpecific(new DbTypeString());
            else type = new DbTypeString();
            EditorColumnStructure col = new EditorColumnStructure(this);
            m_columns.Add(col);
            col.DataType = type;
            using (GridPosition gp = new GridPosition(columnsGrid)) DisplayColumns();
        }

        public void RemoveColumn()
        {
            if (columnsGrid.CurrentCell == null) return;
            int index = columnsGrid.CurrentCell.RowIndex;
            m_columns.RemoveAt(index);
            DisplayColumns();
            SetModified();
        }

        [PopupMenu("s_add", Shortcut = Keys.Control | Keys.Add, Weight = 10)]
        public void AddAnyObject()
        {
            if (tabControl1.SelectedIndex == PAGE_COLUMNS)
            {
                AddColumn();
            }
            else if (tabControl1.SelectedIndex == PAGE_INDEXESKEYS)
            {
                AddIndexOrKey();
            }
            else if (tabControl1.SelectedIndex == PAGE_RELATIONS)
            {
                AddRelation();
            }
            else if (tabControl1.SelectedIndex == PAGE_CHECKS)
            {
                AddCheck();
            }
            ShowAlterSql();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddAnyObject();
        }

        [PopupMenu("s_remove", Shortcut = Keys.Control | Keys.Delete, Weight = 11)]
        public void RemoveAnyObject()
        {
            if (tabControl1.SelectedIndex == PAGE_COLUMNS)
            {
                RemoveColumn();
            }
            else if (tabControl1.SelectedIndex == PAGE_INDEXESKEYS)
            {
                RemoveIndexOrKey();
            }
            else if (tabControl1.SelectedIndex == PAGE_RELATIONS)
            {
                RemoveRelation();
            }
            else if (tabControl1.SelectedIndex == PAGE_CHECKS)
            {
                RemoveCheck();
            }
            ShowAlterSql();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            RemoveAnyObject();
        }

        private void rbowned_Click(object sender, EventArgs e)
        {
            rbowned.Checked = true;
            rbreferenced.Checked = false;
            DisplayForeignKeys();
        }

        private void rbreferenced_Click(object sender, EventArgs e)
        {
            rbowned.Checked = false;
            rbreferenced.Checked = true;
            DisplayForeignKeys();
        }

        public bool IsNameUsed(string name)
        {
            foreach (EditorColumnStructure col in m_columns)
            {
                if (col.ColumnName == name) return true;
            }
            foreach (EditorIndexOrKeyStructure ik in m_indexOrKeys)
            {
                if (ik.Name == name) return true;
            }
            foreach (EditorForeignKeyStructure fk in m_foreignKeys)
            {
                if (fk.Name == name) return true;
            }
            foreach (EditorCheckStructure chk in m_checks)
            {
                if (chk.Name == name) return true;
            }
            return false;
        }

        public string GetUniqueName(string name)
        {
            if (!IsNameUsed(name)) return name;
            int i = 1;
            while (IsNameUsed(name + i.ToString())) i++;
            return name + i.ToString();
        }

        public void RemoveConnectionByKey(RemoveConntectionByKeyArgs e)
        {
            if (PhysicalConnection != null && PhysicalConnection.GetConnKey() == e.ConnKey)
            {
                if (!AllowClose())
                {
                    e.Canceled = true;
                    return;
                }
                MainWindow.Instance.RunInMainWindow(CloseContent);
            }
        }

        bool m_machineChangingFkGrid = false;
        void ReloadFkNames()
        {
            try
            {
                m_machineChangingFkGrid = true;

                foreach (DataGridViewRow row in fkGrid.Rows)
                {
                    var fk = (EditorForeignKeyStructure)row.Tag;
                    row.Cells[0].Value = fk.Name;
                }
            }
            finally
            {
                m_machineChangingFkGrid = false;
            }
        }

        private void fkGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!m_initialized) return;
            if (m_machineChangingFkGrid) return;
            EditorForeignKeyStructure fk = (EditorForeignKeyStructure)fkGrid.Rows[e.RowIndex].Tag;
            if (fk == null) return;
            object value = fkGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            switch (e.ColumnIndex)
            {
                case 0:
                    fk.Name = value.ToString();
                    break;
            }
            propertyFrame1.ReloadData();
            SetModified();
            ShowAlterSql();
        }

        private void checkGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!m_initialized) return;
            EditorCheckStructure chk = (EditorCheckStructure)checkGrid.Rows[e.RowIndex].Tag;
            if (chk == null) return;
            object value = checkGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            switch (e.ColumnIndex)
            {
                case 0:
                    chk.Name= value.ToString();
                    break;
                case 1:
                    chk.Expression = value.ToString();
                    break;
            }
            propertyFrame1.ReloadData();
            SetModified();
            ShowAlterSql();
        }

        private void indexGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            EditorIndexOrKeyStructure ik = (EditorIndexOrKeyStructure)indexGrid.Rows[e.RowIndex].Tag;
            if (!ik.AllowChange) e.Cancel = true;
        }

        private void fkGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            EditorForeignKeyStructure fk = (EditorForeignKeyStructure)fkGrid.Rows[e.RowIndex].Tag;
            if (!fk.AllowChange) e.Cancel = true;
            if (rbreferenced.Checked) e.Cancel = true;
        }

        private void checkGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            EditorCheckStructure chk = (EditorCheckStructure)checkGrid.Rows[e.RowIndex].Tag;
            if (!chk.AllowChange) e.Cancel = true;
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            ShowAlterSql();
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            ShowAlterSql();
            OpenQueryParameters pars = new OpenQueryParameters();
            IPhysicalConnection conn = PhysicalConnection;
            if (conn != null) conn = conn.Clone();
            pars.SqlText = tbxAlterSql.Text;
            string dbname = DatabaseConnection != null ? DatabaseConnection.DatabaseName : null;
            if (dbname != null) conn.AfterOpen += ConnTools.ChangeDatabaseCallback(dbname);
            MainWindow.Instance.OpenContent(new QueryFrame(conn, pars));
        }

        public string[] CharacterSets
        {
            get
            {
                if (m_charSets == null) m_charSets = new List<string>(DatabaseConnection.InvokeLoadCharacterSets()).ToArray();
                return m_charSets;
            }
        }

        public string[] Collations
        {
            get
            {
                if (m_collations == null) m_collations = new List<string>(DatabaseConnection.InvokeLoadCollations()).ToArray();
                return m_collations;
            }
        }

        public Dictionary<NameWithSchema, IDomainStructure> Domains
        {
            get
            {
                if (m_domains == null)
                {
                    m_domains = new Dictionary<NameWithSchema, IDomainStructure>();
                    if (m_conn.DatabaseCaps.Domains)
                    {
                        var dbmem = new DatabaseStructureMembers
                        {
                            DomainDetails = true,
                            DomainList = true,
                        };
                        foreach (var dom in DatabaseConnection.InvokeLoadStructure(dbmem, null).Domains)
                        {
                            m_domains[dom.FullName] = dom;
                        }
                    }
                }
                return m_domains;
            }
        }

        private void fkGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (rbreferenced.Checked) m_refKeys.RemoveAt(e.Row.Index);
            if (rbowned.Checked) m_foreignKeys.RemoveAt(e.Row.Index);
            SetModified();
        }

        private void indexGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            m_indexOrKeys.RemoveAt(e.Row.Index);
            SetModified();
        }

        private void checkGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            m_checks.RemoveAt(e.Row.Index);
            SetModified();
        }

        private void btnAllowRecreate_Click(object sender, EventArgs e)
        {
            btnAllowRecreate.Checked = !btnAllowRecreate.Checked;
            EnableButtons();
        }

        internal bool AllowRecreate
        {
            get { return btnAllowRecreate.Checked && m_conn.AlterCaps.RecreateTable; }
        }
        internal bool AllowRenameColumn
        {
            get { return AllowRecreate || m_conn.AlterCaps.RenameColumn; }
        }
        internal bool AllowChangeColumnType
        {
            get { return AllowRecreate || m_conn.AlterCaps.ChangeColumnType; }
        }
        internal bool AllowChangeColumnDefaultValue
        {
            get { return AllowRecreate || m_conn.AlterCaps.ChangeColumnDefaultValue; }
        }
        internal bool AllowChangeConstraint
        {
            get { return AllowRecreate || m_conn.AlterCaps.ChangeConstraint || AllowRecreateConstraint; }
        }
        internal bool AllowRecreateConstraint
        {
            get { return m_settings.AllowRecreateConstraint; }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        //private void columnsGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    if (columnsGrid.CurrentCell.ColumnIndex == COL_TYPE)
        //    {
        //        var cb = e.Control as ComboBox;
        //        if (cb != null)
        //        {
        //            cb.Tag=columnsGrid.CurrentCell;
        //            cb.DropDownStyle = ComboBoxStyle.DropDown;
        //            cb.Leave += new EventHandler(cb_Leave);
        //        }
        //    }
        //}

        //void cb_Leave(object sender, EventArgs e)
        //{
        //    //columnsGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
        //    var cb = sender as ComboBox;
        //    for (int i = 0; i < cb.Items.Count; i++)
        //    {
        //        if (String.Compare(cb.Items[i].ToString(), cb.Text, true) == 0)
        //        {
        //            cb.SelectedIndex = i;
        //            columnsGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
        //            break;
        //        }
        //    }
        //}
    }
}
