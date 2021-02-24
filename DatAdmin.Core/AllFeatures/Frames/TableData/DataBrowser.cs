using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class DataBrowser : TableDataFrame
    {
        ITreeNode m_selectedObject;

        //Dictionary<string, IPhysicalConnection> m_connCache = new Dictionary<string, IPhysicalConnection>();
        Dictionary<string, TableRelatedState> m_tablesCache = new Dictionary<string, TableRelatedState>();
        string m_currentTableKey = null;

        public DataBrowser()
        {
            InitializeComponent();
            UpdateHint();
            ConnPack.Cache = CachePack.TreeCache;
        }

        private void UpdateHint()
        {
            if (TabularData == null)
            {
                labNoDataMessage.Text = Texts.Get("s_select_table_on_connection_tree");
                labNoDataMessage.Visible = true;
            }
            else
            {
                labNoDataMessage.Visible = false;
            }
        }

        public ITreeNode SelectedObject
        {
            get
            {
                return m_selectedObject;
            }
            set
            {
                var newvalue = (ITreeNode)value;
                while (newvalue != null && newvalue.FindTabularDataObject() == null && !newvalue.HasTabularData) newvalue = newvalue.Parent;

                ChangeDependencySource(newvalue);
            }
        }

        protected override void LoadFromDependencySource(object value)
        {
            var tvalue = (ITreeNode)value;
            if (m_selectedObject == tvalue)
            {
                UpdateState();
                return;
            }

            RefreshRowCount();
            m_selectedObject = tvalue;
            LoadDataFromNode();
            ReloadPerspectives();
            UpdateHint();
            MainWindow.Instance.UpdateFrameEnabling(this);
        }

        //public override string UsageEventName
        //{
        //    get { return "data_browser"; }
        //}

        //public override bool AllowClose()
        //{
        //    if (Detached) return false;
        //    return base.AllowClose();
        //}

        //protected override void DisallowedClose()
        //{
        //    Detached = true;
        //}

        private void LoadDataFromNode()
        {
            bool loaded = false;
            var tabdataobj = m_selectedObject.FindTabularDataObject();
            ITabularDataView data = null;
            if (tabdataobj != null) data = tabdataobj.GetTabularData(ConnPack);
            if (data == null && m_selectedObject != null && m_selectedObject.HasTabularData)
            {
                data = m_selectedObject.GetTabularData();
                if (data != null) data.ChangeConnection(ConnPack);
            }
            if (data != null)
            {
                string key = data.GetConnectionKey();
                try
                {
                    m_currentTableKey =  key + "#" + data.TableSource.Database.DatabaseName + "#" + data.TableSource.FullName.ToString() + "#" + data.FixedPerspective;
                }
                catch
                {
                    try
                    {
                        m_currentTableKey = key + "#" + data.TableSource.FullName.ToString()+ "#" + data.FixedPerspective;
                    }
                    catch
                    {
                        m_currentTableKey = null;
                    }
                }
                if (m_currentTableKey == null)
                {
                    State = new TableRelatedState();
                }
                else
                {
                    if (m_tablesCache.ContainsKey(m_currentTableKey))
                    {
                        State = m_tablesCache[m_currentTableKey];
                        State.HistoryPosition = 0;
                    }
                    else
                    {
                        State = new TableRelatedState();
                        m_tablesCache[m_currentTableKey] = State;
                    }
                }
                DataStatePer.TableData = data;
                if (data != null && data.FixedPerspective != null)
                {
                    DataStatePer.UseFixedPerspective(data.FixedPerspective);
                }
                RefreshCurrentView();
                loaded = true;
            }

            if (!loaded) TabularData = null;
            UpdateHint();
            UpdateCellDataView();
            UpdateState();
        }

        internal override void ResetView()
        {
            base.ResetView();
            if (m_currentTableKey != null)
            {
                m_tablesCache[m_currentTableKey] = State;
            }
        }

        //public override void OnShowContent()
        //{
        //    LoadDataFromNode(true);
        //}

        public override string PageTitle
        {
            get
            {
                if (ParentFrame != null) return base.PageTitle;
                return "s_data_browser";
            }
        }

        public override string PageTitleForParent
        {
            get { return "s_data_browser"; }
        }

        //public override void RemoveConnectionByGroup(RemoveConntectionByGroupArgs e)
        //{
        //    if (m_connCache.ContainsKey(e.GroupId))
        //    {
        //        if (!AllowClose())
        //        {
        //            e.Canceled = true;
        //            return;
        //        }
        //        Async.SafeClose(m_connCache[e.GroupId]);
        //        m_connCache.Remove(e.GroupId);
        //    }
        //}

        public override bool AllowCloseConnection(string connkey)
        {
            if (TabularData != null && TabularData.GetConnectionKey() == connkey)
            {
                return AllowClose();
            }
            return true;
        }

        public override string MenuBarTitle
        {
            get
            {
                if (TabularData != null) return "s_data_browser";
                return null;
            }
        }
    }
}
