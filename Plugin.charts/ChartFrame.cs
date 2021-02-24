using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;
using ZedGraph;
using System.Xml;
using System.IO;

namespace Plugin.charts
{
    public partial class ChartFrame : ContentFrame
    {
        ITabularDataView m_data;
        ITableStructure m_structure;
        ChartData m_curdata;
        Control m_cfgFrame;
        IChartDataProcessor m_proc;
        IVirtualFile m_file;
        bool m_modified;
        IChartDataProcessor m_loadProcessor;


        public ChartFrame(IVirtualFile file)
        {
            InitializeComponent();

            InitBegin();
            LoadFromFile(file);
            InitEnd();
        }

        public ChartFrame(ITabularDataView data)
        {
            InitializeComponent();
            m_data = data;
            btnSave.Enabled = SupportsSave;

            InitBegin();
            InitEnd();
        }

        private ListViewItem AddStyle(ChartStyle style, string imgkey)
        {
            var item = lbxStyle.Items.Add(style.ToString());
            item.ImageKey = imgkey;
            item.Tag = style;
            return item;
        }

        private void InitBegin()
        {
            var line = AddStyle(new LineChartStyle(), "line");
            AddStyle(new AreaChartStyle(), "area");
            AddStyle(new PieChartStyle(), "pie");
            AddStyle(new BarChartStyle(), "bar");
            lbxStyle.SelectOneItem(line, true);
        }

        private void InitEnd()
        {
            if (m_data.Connection != null)
            {
                Controls.ShowProgress(true, null, null);
                m_data.Connection.BeginOpen(m_invoker.CreateInvokeCallback(Opened));
            }
            else
            {
                DoLoadChartData();
                ShowCurrentData();
            }
        }

        private void Report(Exception err)
        {
        }

        private void Opened(IAsyncResult async)
        {
            try
            {
                m_data.Connection.EndOpen(async);
                RefreshChartData();
            }
            catch (Exception e)
            {
                Report(e);
            }
        }

        private DataSourceConfigurator Configurator
        {
            get { return (DataSourceConfigurator)cbxDataSourceType.SelectedItem; }
        }

        private ChartStyle Style
        {
            get
            {
                if (lbxStyle.FocusedItem != null) return (ChartStyle)lbxStyle.FocusedItem.Tag;
                if (lbxStyle.SelectedItems.Count > 0) return (ChartStyle)lbxStyle.SelectedItems[0].Tag;
                return null;
            }
        }

        public override string PageTitle
        {
            get { return m_data.ToString(); }
        }

        public override Bitmap Image
        {
            get { return CoreIcons.chart; }
        }

        private void DoLoadChartData()
        {
            m_curdata = null;
            if (m_proc != null && m_data != null)
            {
                m_curdata = m_proc.LoadChartData(m_data);
            }
            if (m_structure == null)
            {
                m_structure = m_data.GetStructure(null);
            }
        }

        private void LoadedChartData(IAsyncResult async)
        {
            try
            {
                IsLoadingIcon = false;
            }
            catch (Exception e)
            {
                Report(e);
                m_curdata = null;
            }
            Controls.ShowProgress(false, null, null);
            ShowCurrentData();
        }

        private InMemoryTable ChartDataToTable(ChartData data)
        {
            var ts = new TableStructure();
            ts.AddColumn(Texts.Get("s_label"), new DbTypeString());
            foreach (var item in data.ValueDefs)
            {
                ts.AddColumn(item.Label, new DbTypeFloat { Bytes = 8 });
            }
            var res = new InMemoryTable(ts);
            foreach (var item in data.Items)
            {
                var rec = new ArrayDataRecord(ts);
                rec.SeekValue(0);
                rec.SetString(item.Label);
                for (int i = 0; i < data.ValueDefs.Length; i++)
                {
                    rec.SeekValue(i + 1);
                    rec.SetDouble(item.Values[i]);
                }
                res.Rows.Add(rec);
            }
            return res;
        }

        private void ShowCurrentData()
        {
            if (m_structure == null) return;

            var parent = zedGraphControl1.Parent;
            parent.Controls.Remove(zedGraphControl1);
            zedGraphControl1.Dispose();
            zedGraphControl1 = new ZedGraphControl();
            zedGraphControl1.Dock = DockStyle.Fill;
            zedGraphControl1.IsShowPointValues = true;
            parent.Controls.Add(zedGraphControl1);

            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane.Title.Text = Texts.Get("s_chart");
            //myPane.CurveList.Clear();
            if (m_curdata != null && Style != null) Style.FillData(myPane, m_curdata);

            zedGraphControl1.Invalidate();
            zedGraphControl1.AxisChange();

            if (m_curdata != null)
            {
                var mem = ChartDataToTable(m_curdata);
                var tbl = new BedTable(mem);
                tableDataFrame1.TabularData = new InMemoryTabularData(tbl);
            }
            else
            {
                tableDataFrame1.TabularData = null;
            }

            if (cbxDataSourceType.Items.Count == 0)
            {
                CreateConfigurator(new HistogramDataSourceConfigurator(m_structure));
                CreateConfigurator(new ValuesDataSourceConfigurator(m_structure));
                CreateConfigurator(new TimeLineDataSourceConfigurator(m_structure));
                if (m_loadProcessor != null)
                {
                    for (int i = 0; i < cbxDataSourceType.Items.Count; i++)
                    {
                        var cfg = (DataSourceConfigurator)cbxDataSourceType.Items[i];
                        if (cfg.GetProcessorType() == m_loadProcessor.GetType())
                        {
                            cfg.LoadFromProcessor(m_loadProcessor);
                            cbxDataSourceType.SelectedIndex = i;
                            m_loadProcessor = null;
                            break;
                        }
                    }
                }
                else
                {
                    cbxDataSourceType.SelectedIndex = 0;
                }
                RefreshChartData();
            }
        }

        private void CreateConfigurator(DataSourceConfigurator cfg)
        {
            cbxDataSourceType.Items.Add(cfg);
            cfg.Changed += new EventHandler(cfg_Changed);
        }

        void cfg_Changed(object sender, EventArgs e)
        {
            if (chbAutoRefresh.Checked)
            {
                RefreshChartData();
            }
        }

        private void RefreshChartData()
        {
            try
            {
                m_proc = Configurator.GetProcessor();
            }
            catch
            {
                m_proc = null;
            }
            if (m_data.Connection != null)
            {
                Controls.ShowProgress(true, null, null);
                m_data.Connection.BeginInvoke((Action)DoLoadChartData, Async.CreateInvokeCallback(m_invoker, LoadedChartData));
            }
            else
            {
                DoLoadChartData();
                ShowCurrentData();
            }
        }

        private void cbxDataSourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_cfgFrame != null) m_cfgFrame.Visible = false;
            m_cfgFrame = Configurator.GetEditor();
            if (m_cfgFrame.Parent == null)
            {
                panConfigurator.Controls.Add(m_cfgFrame);
                m_cfgFrame.Dock = DockStyle.Fill;
                Translating.TranslateControl(m_cfgFrame);
            }
            else
            {
                m_cfgFrame.Visible = true;
            }
            if (chbAutoRefresh.Checked)
            {
                RefreshChartData();
            }
        }

        private void lbxStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowCurrentData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshChartData();
        }

        public override void OnClose()
        {
            base.OnClose();
            m_data.CloseView();
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            zedGraphControl1.SaveAs();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        public override bool SupportsSave
        {
            get { return m_data.SupportsSerialize; }
        }

        public override bool SupportsSaveAs
        {
            get { return SupportsSave; }
        }

        public override bool SaveAs()
        {
            if (!Directory.Exists(Core.ChartsDirectory)) Directory.CreateDirectory(Core.ChartsDirectory);
            saveFileDialog1.InitialDirectory = Core.ChartsDirectory;
            saveFileDialog1.CustomPlaces.Add(Core.ChartsDirectory);
            if (saveFileDialog1.ShowDialogEx() == DialogResult.OK)
            {
                m_file = new DiskFile(saveFileDialog1.FileName);
                ChangedWinId();
                Save();
                UpdateTitle();
                m_modified = false;
                if (!IOTool.RelativePathTo(Core.ScriptsDirectory, saveFileDialog1.FileName).StartsWith(".."))
                {
                    HTree.CallRefreshRoot();
                }
                return true;
            }
            return false;
        }

        public override bool Save()
        {
            if (m_file == null)
            {
                return SaveAs();
            }
            else
            {
                DoSave();
                return true;
            }
        }

        private void DoSave()
        {
            var doc = XmlTool.CreateDocument("Chart");
            var root = doc.DocumentElement;
            m_data.SaveToXml(root.AddChild("Data"));
            Style.SaveToXml(root.AddChild("Style"));
            Configurator.GetProcessor().SaveToXml(root.AddChild("Processor"));
            m_file.SaveText(doc.OuterXml);
            m_modified = false;
        }

        private void LoadFromFile(IVirtualFile file)
        {
            m_file = file;
            ChangedWinId();
            var doc = new XmlDocument();
            doc.LoadXml(m_file.GetText());
            var root = doc.DocumentElement;
            var loader = (ITabularDataViewLoader)TabularDataViewLoaderAddonType.Instance.LoadAddon(root.FindElement("Data"));
            m_data = loader.CreateTabularDataView();
            var style = ChartStyle.LoadFromXml(root.FindElement("Style"));

            for (int i = 0; i < lbxStyle.Items.Count; i++)
            {
                if (style.GetType() == lbxStyle.Items[i].Tag.GetType())
                {
                    lbxStyle.Items[i].Tag = style;
                    lbxStyle.SelectOneItem(lbxStyle.Items[i], true);
                    break;
                }
            }

            m_loadProcessor = (IChartDataProcessor)ChartDataProcessorAddonType.Instance.LoadAddon(root.FindElement("Processor"));

            m_modified = false;
        }

        private void ChangedWinId()
        {
            WinId = null;
            if (m_file != null && m_file.DataDiskPath != null)
            {
                WinId = IOTool.NormalizePath(m_file.DataDiskPath) + "#chart";
            }
        }

        public static bool FindOpenedFile(string filename)
        {
            if (filename == null) return false;
            string winid = IOTool.NormalizePath(filename) + "#chart";
            if (MainWindow.Instance.HasContent(winid))
            {
                MainWindow.Instance.ActivateContent(winid);
                return true;
            }
            return false;
        }

        private void chbAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (chbAutoRefresh.Checked)
            {
                RefreshChartData();
            }
        }

        private void btnShowFolder_Click(object sender, EventArgs e)
        {
            MainWindow.Instance.ShowDocker(new ChartsTreeDockerFactory());
        }
    }
}
