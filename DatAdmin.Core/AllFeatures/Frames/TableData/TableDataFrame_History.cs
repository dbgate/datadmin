using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Xml;

namespace DatAdmin
{
    partial class TableDataFrame
    {
        internal TableRelatedState State
        {
            get { return m_state; }
            set { m_state = value; }
        }

        internal TableDataState DataState
        {
            get
            {
                return DataStatePer.DataState;
            }
        }

        internal PerspectiveIndependendState DataStatePer
        {
            get
            {
                return m_state.DataStatePer;
            }
        }

        internal ITabularDataView CurrentData
        {
            get { return DataState.TableData; }
        }

        private void UpdateHistoryEnabling()
        {
            btnGoBack.Enabled = State.GoBackEnabled;
            btnGoForward.Enabled = State.GoForwardEnabled;
            cbxHistory.Items.Clear();
            foreach (var hi in State.HistoryItems)
            {
                cbxHistory.Items.Add(hi);
            }
            cbxHistory.SelectedIndex = State.HistoryPosition;
        }

        ITabularDataView m_bindedTabularData;
        bool m_programaticallySelectCell = false;

        internal void RefreshCurrentView()
        {
            RefreshCurrentView(MasterFrame == null);
        }

        protected void RefreshCurrentViewNoRedock()
        {
            RefreshCurrentView(false);
        }

        internal void RefreshCurrentView(bool allowChangeDocking)
        {
            if (allowChangeDocking && ParentFrame != null)
            {
                var frm = ParentFrame;
                var dock = frm.m_dockWrapper;
                frm.ReleaseFrame(this);
                dock.ReplaceContent(this);
            }

            try
            {
                m_programaticallySelectCell = true;

                nbstart.Text = DataState.FirstRecord.ToString();
                nbcount.Text = DataState.RecordCount.ToString();

                if (m_bindedTabularData != TabularData)
                {
                    if (m_bindedTabularData != null) m_bindedTabularData.LoadedNextData -= OnLoadedNextData;
                    m_bindedTabularData = TabularData;
                    if (m_bindedTabularData != null) m_bindedTabularData.LoadedNextData += OnLoadedNextData;
                }

                m_table = null;

                btnReferences.Enabled = TabularData != null && TabularData.TableSource != null;
                btnGenerateSql.Enabled = TabularData != null && TabularData.TabDataCaps.Scriptable;

                dataGridView1.DataSource = null;
                tbxFilter.Text = DataState.Filter ?? "";
                tbxSearch.Text = DataState.SearchText ?? "";

                if (TabularData != null)
                {
                    SetSettings(TabularData.Settings.TableData(), TabularData.Settings.DataFormat());

                    if (TabularData.Connection != null && TabularData.Connection.Owner == null)
                    {
                        TabularData.Connection.Owner = this;
                    }
                    if (TabularData.Connection != null && !TabularData.Connection.IsOpened)
                    {
                        OpenAndLoadData();
                    }
                    else
                    {
                        LoadDataPage();
                    }
                    cbfilter.Enabled = TabularData.TabDataCaps.Filtering;
                    if (!cbfilter.Enabled) cbfilter.Checked = false;
                    cbpaging.Enabled = TabularData.TabDataCaps.Paging;
                    if (!cbpaging.Enabled) cbpaging.Checked = false;
                }
                else
                {
                    cbfilter.Enabled = false;
                    cbfilter.Checked = false;
                    cbpaging.Enabled = false;
                }
                if (TabularData != null)
                {
                    lblCurrentTable.Text = TabularData.ToString();
                }
                else
                {
                    lblCurrentTable.Text = "";
                }

            }
            finally
            {
                m_programaticallySelectCell = false;
            }


            if (allowChangeDocking)
            {
                if (DataState.DockPanelFrame != null)
                {
                    m_dockWrapper.ReplaceContent(DataState.DockPanelFrame);
                    DataState.DockPanelFrame.ReuseReleasedFrame(this);
                    MainWindow.Instance.UpdateFrameEnabling(DataState.DockPanelFrame);
                }
                else if (DataState.Perspective != null && DataState.Perspective.DockPanelDesign != null)
                {
                    if (m_dockWrapper != null)
                    {
                        LoadPerspectiveLayout();
                    }
                    else
                    {
                        MainWindow.Instance.RunInMainWindow(LoadPerspectiveLayout);
                    }
                }
            }

            UpdateHistoryEnabling();
        }

        private void LoadPerspectiveLayout()
        {
            if (DataState.Perspective == null) return;
            if (DataState.Perspective.DockPanelDesign == null) return;
            if (m_dockWrapper == null) return;

            var design = DataState.Perspective.DockPanelDesign;

            // load dockpanel from XML
            var master = new DockPanelContentFrame();
            master.HeaderRedirectFrame = this;
            master.PrimaryContent = this;
            m_dockWrapper.ReplaceContent(master);

            master.LoadFromXml(design.LayoutXml, design.CreateFrames(this));
            DataState.DockPanelFrame = master;
            MainWindow.Instance.UpdateFrameEnabling(master);
            DispatchDetailRow(dataGridView1.GetCurrentRow());
        }

        private ReferencesDockPanelDesign SaveDockPanel()
        {
            if (DataState.DockPanelFrame == null) return null;
            var res = new ReferencesDockPanelDesign();
            var wins = new List<ContentFrame>();
            GetThisAndDetails(wins);
            wins.Remove(this);
            foreach (var win in wins)
            {
                var r = win as ReferencesTableDataFrame;
                if (r == null) continue;
                res._AddFrameDef(r);
            }
            res.LoadLayoutFromPanel(DataState.DockPanelFrame);
            return res;
        }

        private void SaveCurrentCell()
        {
            if (m_table == null || dataGridView1.CurrentCell == null || m_programaticallySelectCell) return;

            try
            {
                DataState.CurrentCol = dataGridView1.CurrentCell.ColumnIndex;

                ITableStructure ts = CGetStructure(null);
                if (ts == null || ts.PrimaryKeyColumnName() == null || dataGridView1.CurrentCell.Value == null)
                {
                    DataState.CurrentRow = dataGridView1.CurrentCell.RowIndex;
                    DataState.SavedPkCol = null;
                }
                else
                {
                    DataState.CurrentRow = -1;
                    DataState.SavedPkCol = ts.PrimaryKeyColumnName();
                    DataState.SavedPkValue = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[DataState.SavedPkCol].Value;
                }
                DataState.SavedCurrentCell = true;
            }
            catch { }
        }

        private void RestoreCurrentCell()
        {
            if (!DataState.SavedCurrentCell) return;
            if (m_table == null) return;
            if (DataState.CurrentCol < 0 || DataState.CurrentCol >= m_table.Structure.Columns.Count) return;
            if (DataState.SavedPkCol == null || DataState.SavedPkValue == null)
            {
                if (DataState.CurrentRow >= 0 && DataState.CurrentRow < dataGridView1.Rows.Count)
                {
                    dataGridView1.IgnoreSelectionChanged = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[DataState.CurrentRow].Cells[DataState.CurrentCol];
                    dataGridView1.IgnoreSelectionChanged = false;
                    NotifySelectionChanged();
                    return;
                }
            }
            else
            {
                int i = 0, colindex = -1;
                foreach (var col in m_table.Structure.Columns)
                {
                    if (col.ColumnName == DataState.SavedPkCol)
                    {
                        colindex = i;
                        break;
                    }
                    i++;
                }
                if (colindex < 0) return;
                int rowindex = 0;
                int curcol = Math.Min(DataState.CurrentCol, dataGridView1.ColumnCount - 1);
                foreach (var row in m_table.Rows)
                {
                    if (row[colindex] != null && row[colindex].ToString() == DataState.SavedPkValue.ToString())
                    {
                        dataGridView1.IgnoreSelectionChanged = true;
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[curcol];
                        dataGridView1.IgnoreSelectionChanged = false;
                        NotifySelectionChanged();
                        return;
                    }
                    rowindex++;
                }
                if (dataGridView1.CurrentRow != null)
                {
                    dataGridView1.CurrentCell = dataGridView1.CurrentRow.Cells[curcol];
                }
            }
        }
    }
}
