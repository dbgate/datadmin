using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace DatAdmin
{
    public partial class GenerateSqlForm : FormEx
    {
        AppObject[] m_objs;
        TableDataFrame m_dataFrame;
        bool m_initialized;
        IDatabaseSource m_db;
        ISqlDialect m_dialect;
        ISqlGeneratorCommon m_sqlgen;
        DataFrameRowsExtractor m_rowsExtractor;
        string m_preview;
        SqlFormatProperties m_formatProps;
        int m_previewRows;
        Action m_curRun;

        public GenerateSqlForm()
        {
            InitializeComponent();
            Disposed += new EventHandler(GenerateSqlForm_Disposed);
            ConnPack.Cache = new CachePack
            {
                ReuseAnalyserCache = true
            };
        }

        void GenerateSqlForm_Disposed(object sender, EventArgs e)
        {
            var last = propertyFrame1.SelectedObject as ISqlGeneratorCommon;
            if (last != null) last.ChangedProperties -= generator_ChangedProperties;
            ConnPack = null;
        }

        
        private void Initialize(AppObject[] objs)
        {
            m_objs = objs;
            foreach (var gen in m_objs[0].GetSqlGenerators())
            {
                lbxGenerator.Items.Add(gen);
            }
            InitiDialects();
            addonSelectFrame1.Reload(false);
            if (lbxGenerator.Items.Count > 0) lbxGenerator.SelectedIndex = 0;
            m_db = m_objs[0].FindDatabaseConnection(ConnPack);
            if (m_db != null) Async.SafeOpen(m_db.Connection);

            m_initialized = true;
            tbxRows.Enabled = btnOkPreviewRows.Enabled = false;
            RefreshPreview();
        }

        private void InitiDialects()
        {
            cbxDialect.Items.Add("(" + Texts.Get("s_default") + ")");
            foreach (var dialect in DialectAddonType.GetAllDialects(false)) cbxDialect.Items.Add(dialect);
            cbxDialect.SelectedIndex = 0;
        }

        private void Initialize(TableDataFrame dataFrame)
        {
            m_dataFrame = dataFrame;
            int curhldindex = 0;
            int updindex = -1;
            foreach (var holder in DataSqlGeneratorAddonType.Instance.CommonSpace.GetAllAddons())
            {
                var gen = (IDataSqlGenerator)holder.CreateInstance();
                var tablegen = gen as TableDataSqlGeneratorBase;
                if (tablegen != null)
                {
                    if (dataFrame.TabularData.TableSource != null)
                    {
                        tablegen.FullTableName = dataFrame.TabularData.TableSource.FullName;
                    }
                    else
                    {
                        var ts = dataFrame.TabularData.GetStructure(null);
                        tablegen.FullTableName = ts.FullName;
                    }
                }
                if (gen is UpdateSqlGenerator) updindex = curhldindex;
                lbxGenerator.Items.Add(gen);
                curhldindex++;
            }
            InitiDialects();

            addonSelectFrame1.Reload(false);

            foreach (var gen in dataFrame.GetSqlGens())
            {
                lbxGenerator.Items.Add(gen);
            }

            if (lbxGenerator.Items.Count > 0)
            {
                lbxGenerator.SelectedIndex = updindex >= 0 ? updindex : 0;
                dataSqlGeneratorFrame1.Generator = (IDataSqlGenerator)lbxGenerator.SelectedItem;
            }
            //m_db = m_objs[0].FindDatabaseConnection(ConnPack);
            //Async.SafeOpen(m_db.Connection);

            lbxRows.Items.Add(new AllRowsExtractor(dataFrame));
            lbxRows.Items.Add(new FilteredRowsExtractor(dataFrame));
            lbxRows.Items.Add(new LoadedRowsExtractor(dataFrame));
            lbxRows.Items.Add(new SelectedRowsExtractor(dataFrame));
            lbxRows.SelectedIndex = 3;

            m_initialized = true;
            if (dataFrame.m_table != null) dataSqlGeneratorFrame1.SetHintStructure(dataFrame.m_table.Structure, m_dataFrame.GetSelectedColumns());
            btnSaveAsJob.Visible = false;
            RefreshPreview();
        }

        public static void Run(AppObject[] objs)
        {
            var win = new GenerateSqlForm();
            win.Initialize(objs);
            win.ShowDialogEx();
        }

        private void ShowDataGenerator(bool visible)
        {
            dataSqlGeneratorFrame1.Visible = visible;
            labRows.Visible = visible;
            lbxRows.Visible = visible;
            propertyFrame1.Visible = !visible;
        }

        private void lbxGenerator_SelectedIndexChanged(object sender, EventArgs e)
        {
            var last = propertyFrame1.SelectedObject as ISqlGeneratorCommon;
            if (last != null) last.ChangedProperties -= generator_ChangedProperties;

            if (lbxGenerator.SelectedItem is IDataSqlGenerator)
            {
                dataSqlGeneratorFrame1.Generator = (IDataSqlGenerator)lbxGenerator.SelectedItem;
                ShowDataGenerator(true);
                labRows.Visible = lbxRows.Visible = dataSqlGeneratorFrame1.Generator.IsRowEnumerator;
            }
            else
            {
                ShowDataGenerator(false);
                propertyFrame1.SelectedObject = lbxGenerator.SelectedItem;
            }

            var cur = propertyFrame1.SelectedObject as ISqlGeneratorCommon;
            if (cur != null) cur.ChangedProperties += generator_ChangedProperties;
            RefreshPreview();
        }

        void generator_ChangedProperties(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void RefreshPreview()
        {
            if (!m_initialized) return;
            var dial = cbxDialect.SelectedItem as ISqlDialect;
            if (dial == null && m_db != null) dial = m_db.Dialect;
            if (dial == null && m_dataFrame != null) dial = m_dataFrame.GetDialect();
            if (dial == null && m_objs != null) dial = m_objs[0].Dialect;
            if (dial == null) dial = GenericDialect.Instance;
            m_previewRows = tbxRows.Text.SafeIntParse(10);
            m_dialect = dial;
            m_sqlgen = lbxGenerator.SelectedItem as ISqlGeneratorCommon;
            m_rowsExtractor = (DataFrameRowsExtractor)lbxRows.SelectedItem;
            m_formatProps = sqlFormatPropsFrame1.Value;

            if (m_curRun != null) return;

            if (m_dialect != null && m_sqlgen != null)
            {
                pictureBox1.BringToFront();
                if (m_objs != null)
                {
                    if (m_db != null)
                    {
                        m_db.Connection.BeginInvoke((Action)DoRefreshPreview, m_invoker.CreateInvokeCallback(RefreshedPreview));
                    }
                    else
                    {
                        m_curRun = DoRefreshPreview;
                        m_curRun.BeginInvoke(m_invoker.CreateInvokeCallback(RefreshedPreview), null);
                        //DoRefreshPreview();
                        //RefreshedPreview(null);
                    }
                }
                if (m_dataFrame != null)
                {
                    if (m_dataFrame.TabularData.Connection != null)
                    {
                        m_dataFrame.TabularData.Connection.BeginInvoke((Action)DoRefreshPreview, m_invoker.CreateInvokeCallback(RefreshedPreview));
                    }
                    else
                    {
                        ThreadPool.BeginInvoke(DoRefreshPreviewFunc, m_invoker.CreateInvokeCallback(RefreshedPreview));
                    }
                }
            }
        }

        private object DoRefreshPreviewFunc()
        {
            DoRefreshPreview();
            return null;
        }

        private void DoRefreshPreview()
        {
            var sw = new StringWriter();
            var dmp = m_dialect.CreateDumper(sw, m_formatProps);
            if (m_sqlgen is IAppObjectSqlGenerator)
            {
                foreach (var obj in m_objs)
                {
                    if (m_db != null)
                    {
                        ((IAppObjectSqlGenerator)m_sqlgen).GenerateSql(m_db, obj.GetFullDatabaseRelatedName(), dmp, m_dialect);
                    }
                    else
                    {
                        ((IAppObjectSqlGenerator)m_sqlgen).GenerateSql(obj, dmp, m_dialect);
                    }
                }
            }
            if (m_sqlgen is IDataSqlGenerator)
            {
                if (((IDataSqlGenerator)m_sqlgen).IsRowEnumerator)
                {
                    var preview = m_rowsExtractor.GetPreviewRows(m_previewRows);
                    int rowcount = m_rowsExtractor.GetRowCount();

                    dmp.Put("-- All row count: %s, preview row count: %s\n\n", rowcount, preview.Rows.Count);
                    foreach (var row in preview.Rows)
                    {
                        ((IDataSqlGenerator)m_sqlgen).GenerateSqlRow(row, dmp, m_dataFrame.GetSelectedColumns());
                    }
                }
                else
                {
                    ((IDataSqlGenerator)m_sqlgen).GenerateSql(dmp);
                }
           }
            m_preview = sw.ToString();
        }

        private void RefreshedPreview(IAsyncResult async)
        {
            try
            {
                pictureBox1.SendToBack();
                if (m_db != null)
                {
                    m_db.Connection.EndInvoke(async);
                }
                if (m_dataFrame != null)
                {
                    if (m_dataFrame.TabularData.Connection != null)
                    {
                        m_dataFrame.TabularData.Connection.EndInvoke(async);
                    }
                    else
                    {
                        ((IStandaloneAsyncResult)async).EndInvoke();
                    }
                }
                if (m_curRun != null)
                {
                    var cr = m_curRun;
                    m_curRun = null;
                    cr.EndInvoke(async);
                }
            }
            catch (Exception err)
            {
                Errors.Report(err);
            }
            codeEditor1.SetCodeText(m_preview, false);
            codeEditor1.Dialect = m_dialect;
        }

        private void cbxDialect_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private ExtendedFileNameHolderInfo GetFileHolderInfo()
        {
            return new ExtendedFileNameHolderInfo
            {
                DirectionIsSave = true,
                FileExtension = "sql",
                Filter = "*.sql|{s_sql_script} (*.sql)",
            };
        }

        private void addonSelectFrame1_FilterAddon(object sender, FilterAddonEventArgs e)
        {
            var place = (IFilePlace)e.InstanceModel;
            var holder = GetFileHolderInfo();
            if (m_objs != null)
            {
                holder.RelatedConnection = m_objs[0].GetConnection();
                holder.RelatedDatabase = m_objs[0].FindDatabaseName();
            }
            if (m_dataFrame != null)
            {
                if (m_dataFrame.TabularData.Connection != null)
                {
                    holder.RelatedConnection = m_dataFrame.TabularData.Connection.PhysicalFactory;
                }
                holder.RelatedDatabase = m_dataFrame.TabularData.DatabaseSource != null ? m_dataFrame.TabularData.DatabaseSource.DatabaseName : null;
            }
            if (!place.SupportsSave(holder)) e.Skip = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private Job CreateJob()
        {
            var gen = lbxGenerator.SelectedItem as ISqlGeneratorCommon;
            if (gen is IAppObjectSqlGenerator)
            {
                return GenerateSqlJob.CreateJob(
                    (IAppObjectSqlGenerator)gen,
                    (IFilePlace)addonSelectFrame1.SelectedObject,
                    cbxDialect.SelectedItem as ISqlDialect,
                    m_objs,
                    ConnPack, sqlFormatPropsFrame1.Value, new JobProperties());
            }
            if (gen is IDataSqlGenerator)
            {
                return GenerateSqlJob.CreateDataJob(
                    (IDataSqlGenerator)gen,
                    (IFilePlace)addonSelectFrame1.SelectedObject,
                    cbxDialect.SelectedItem as ISqlDialect,
                    m_dataFrame,
                    (DataFrameRowsExtractor)lbxRows.SelectedItem,
                    ConnPack, sqlFormatPropsFrame1.Value, new JobProperties());
            }
            return null;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            CreateJob().StartProcess();
            Close();
        }

        private void splitContainerPanel_Paint(object sender, PaintEventArgs e)
        {
            var panel = (SplitterPanel)sender;
            e.Graphics.DrawRectangle(Pens.Gray, 0, 0, panel.Width - 1, panel.Height - 1);
        }

        private void btnSaveAsJob_Click(object sender, EventArgs e)
        {
            Job.AskAndExportToFile(CreateJob);
        }

        public static void Run(TableDataFrame dataFrame)
        {
            var win = new GenerateSqlForm();
            win.Initialize(dataFrame);
            win.ShowDialogEx();
        }

        private void lbxRows_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void dataSqlGeneratorFrame1_SettingsChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void sqlFormatPropsFrame1_UserChangedProperties(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void addonSelectFrame1_ChangedSelectedObject(object sender, EventArgs e)
        {
            var place = addonSelectFrame1.SelectedObject as IFilePlace;
            if (place != null) place.SetFileHolderInfo(GetFileHolderInfo());
        }

        private void btnOkPreviewRows_Click(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void btnRefreshPreview_Click(object sender, EventArgs e)
        {
            RefreshPreview();
        }
    }
}
