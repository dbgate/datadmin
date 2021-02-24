using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace DatAdmin
{
    public partial class ReferencesTableDataFrame : TableDataFrame
    {
        ITabularDataView m_baseView;
        ReferenceViewDefinition m_refdef;
        //TableDataFrame m_parentFrame;
        BedRow m_masterRow;

        public ReferencesTableDataFrame(TableDataFrame masterFrame, ReferenceViewDefinition refdef, BedRow masterRow)
        {
            InitializeComponent();
            //ParentFrame = parentFrame;
            //m_parentFrame = parentFrame;
            MasterFrame = masterFrame;
            m_baseView = masterFrame.TabularData;
            m_refdef = refdef;
            m_masterRow = masterRow;
            var src = m_baseView.DatabaseSource.GetTable(m_refdef.TableName);
            State = new TableRelatedState();
            DataStatePer.AdditionalCondition = m_refdef.GetCondition(m_masterRow);
            DataStatePer.TableData = src.GetTabularData();

            //RefreshCurrentViewNoRedock();
            //MainWindow.Instance.RunInMainWindow(RefreshCurrentViewNoRedock);
            //btnReferences.Enabled = false;
            //TabularData = src.GetTabularDataAndReuse();
            //LoadDependendData();
        }

        public override void OnClose()
        {
            ((TableDataFrame)MasterFrame).ClosedReference(this);
            base.OnClose();
        }

        //public override string UsageEventName
        //{
        //    get { return "references_table_data"; }
        //}

        //private void LoadCombos()
        //{
        //    ITableStructure tbl = CGetStructure(LoadCombos);
        //    if (tbl == null) return;

        //    cbxDetailTable.Items.Clear();
        //    List<RefDef> refdefs = new List<RefDef>();
        //    foreach (IForeignKey fk in tbl.GetReferencedFrom())
        //    {
        //        if (fk.Columns.Count == 1)
        //        {
        //            refdefs.Add(new RefDef { Fk = fk, Text = String.Format("{0} ({1})", fk.Table, fk.Columns[0]) });
        //        }
        //    }
        //    refdefs.SortByKey(rd => rd.Text);
        //    foreach (var rd in refdefs) cbxDetailTable.Items.Add(rd);
        //    if (cbxDetailTable.Items.Count > 0)
        //    {
        //        if (DataState.DetailTableIndex >= 0 && DataState.DetailTableIndex < cbxDetailTable.Items.Count)
        //        {
        //            cbxDetailTable.SelectedIndex = DataState.DetailTableIndex;
        //        }
        //        else
        //        {
        //            cbxDetailTable.SelectedIndex = 0;
        //        }
        //    }
        //}

        public override Bitmap Image
        {
            get { return CoreIcons.references; }
        }

        //private void DoLoadReferenceData(int colindex, object value, RefDef rd)
        //{
        //    if (TabularData == null || TabularData.Connection == null) return;
        //    ITableStructure ts = CGetStructure(ReloadDetail);
        //    if (ts == null) return;
        //    string colname = ts.Columns[colindex].ColumnName;
        //    ISqlDialect dialect = TabularData.Connection.Dialect;
        //    string sqlpar, formpar;
        //    dialect.CreateNamedParameter("p1", out sqlpar, out formpar);

        //    CurrentReference = null;

        //    if (rd == null)
        //    {
        //        foreach (IForeignKey fk in TableStructureExtension.GetConstraints<IForeignKey, IForeignKey>(ts))
        //        {
        //            if (fk.Columns.Count == 1 && fk.Columns[0].ColumnName == colname)
        //            {
        //                DataState.MasterTableName = fk.PrimaryKeyTable;
        //                CurrentReference = new ReferencesView();
        //                CurrentReference.Argument = value;
        //                CurrentReference.Condition = AdditionalCondition.CreateEqualCondition(dialect, fk.PrimaryKeyColumns[0].ColumnName, sqlpar, value);
        //                CurrentReference.TableName = fk.PrimaryKeyTable;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        DataState.MasterTableName = new NameWithSchema("???");
        //        IForeignKey fk = rd.Fk;
        //        CurrentReference = new ReferencesView();
        //        CurrentReference.Argument = value;
        //        CurrentReference.Condition = AdditionalCondition.CreateEqualCondition(dialect, fk.Columns[0].ColumnName, sqlpar, value);
        //        CurrentReference.TableName = fk.Table.FullName;
        //    }

        //    if (CurrentReference != null)
        //    {
        //        using (DbCommand cmd = TabularData.Connection.DbFactory.CreateCommand())
        //        {
        //            cmd.Connection = TabularData.Connection.SystemConnection;
        //            cmd.CommandText = String.Format("SELECT * FROM {0} WHERE {1}", DialectExtension.QuoteFullName(dialect, CurrentReference.TableName), CurrentReference.Condition);
        //            DbParameter par = TabularData.Connection.DbFactory.CreateParameter();
        //            par.ParameterName = formpar;
        //            par.Value = value;
        //            cmd.Parameters.Add(par);
        //            using (IBedReader reader = GetDDA().AdaptReader(cmd.ExecuteReader()))
        //            {
        //                m_detailTable = reader.ToBinaryTable(m_settings.ReferencesRowLimit);
        //            }
        //        }
        //    }
        //}


        public void SelectedRow(BedRow row)
        {
            ChangeDependencySource(row);
        }

        protected override void LoadFromDependencySource(object value)
        {
            m_masterRow = (BedRow)value;
            LoadDependendData();
        }

        private void LoadDependendData()
        {
            DataStatePer.AdditionalCondition = m_refdef.GetCondition(m_masterRow);
            RefreshRowCount();
            ResetPaging();
            LoadDataPage(false);
        }

        public void SaveReferenceXml(XmlElement xml)
        {
            xml.SetAttribute("saveid", PersistString);
            m_refdef.SaveToXml(xml);
        }

        public ReferenceViewDefinition RefDef { get { return m_refdef; } }

        //public void SaveReferenceXml(XmlElement xml)
        //{
        //    m_refdef.SaveToXml(xml);
        //    foreach (var refgrid in DataState.RefGrids)
        //    {
        //        refgrid.SaveReferenceXml(xml.AddChild("Reference"));
        //    }
        //}
    }
}
