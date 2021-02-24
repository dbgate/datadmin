using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.dbmodel
{
    public partial class DependencyBrowserFrame : ContentFrame
    {
        IDatabaseSource m_conn;
        DatabaseStructure m_originalDb;
        DatabaseStructure m_db;
        List<DependencyItem> m_history = new List<DependencyItem>();
        int m_historyPosition = -1;
        bool m_loading;

        public DependencyBrowserFrame(IDatabaseSource conn)
        {
            InitializeComponent();
            m_conn = conn;
            m_conn.Connection.Owner = this;
            cbxDepTypes.Items.Add(Texts.Get("s_detected_dependencies"));
            cbxDepTypes.Items.Add(Texts.Get("s_native_dependencies"));
            cbxDepTypes.SelectedIndex = 0;
            Async.SafeOpen(m_conn.Connection);
            UpdateHistoryEnabling();
            ReloadStructure();
        }

        private void ReloadStructure()
        {
            if (m_loading) return;
            btnRefresh.Enabled = false;
            m_loading = true;
            m_conn.Connection.BeginInvoke((Action)DoReloadStructure, Async.CreateInvokeCallback(m_invoker, ReloadedStructure));
            panel1.Visible = false;
            pictureBox1.Visible = true;
        }

        private void DoReloadStructure()
        {
            m_originalDb = new DatabaseStructure(m_conn.LoadDatabaseStructure(DatabaseStructureMembers.FullStructure, null));
            m_db = new DatabaseStructure(m_originalDb);
            m_db.DetectDependencies(false);
        }

        private DatabaseStructure CurDb
        {
            get
            {
                if (cbxDepTypes.SelectedIndex == 1) return m_originalDb;
                return m_db;
            }
        }

        private void LoadCurrentDependency()
        {
            depSelect.Items = new List<DependencyItem>(CurDb.GetAllDepItems()).ToArray();
        }

        private void ReloadedStructure(IAsyncResult async)
        {
            try
            {
                async.StandaloneEndInvoke();
            }
            catch (Exception err)
            {
                Errors.Report(err);
            }
            btnRefresh.Enabled = true;
            panel1.Visible = true;
            pictureBox1.Visible = false;
            m_loading = false;
            LoadCurrentDependency();
        }

        public override void OnClose()
        {
            base.OnClose();
            Async.SafeClose(m_conn.Connection);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ReloadStructure();
        }

        private void depSelect_ChangeSelectedItem(object sender, EventArgs e)
        {
            depSelectedDependsOn.Items = CurDb.GetSelectedDependsOn(depSelect.SelectedItem, true);
            depDependsOnSelected.Items = CurDb.GetDependsOnSelected(depSelect.SelectedItem, true);
        }

        public override string PageTitle
        {
            get
            {
                return String.Format("{0:S}", m_conn);
            }
        }

        public override string PageToolTip
        {
            get
            {
                return String.Format("{0:M}", m_conn);
            }
        }

        public override string PageTypeTitle
        {
            get
            {
                return "s_dependency_browser";
            }
        }

        public override Bitmap Image
        {
            get
            {
                return CoreIcons.dependency;
            }
        }

        private void SelectPivotItemCore(DependencyItem item)
        {
            depSelect.SelectedItem = item;
            depSelect.Focus();
        }

        private void SelectPivotItem(DependencyItem item)
        {
            if (m_history.Count == 0 && depSelect.SelectedItem != null) m_history.Add(depSelect.SelectedItem);
            SelectPivotItemCore(item);
            if (m_historyPosition >= 0 && m_historyPosition < m_history.Count)
            {
                m_history.RemoveRange(m_historyPosition + 1, m_history.Count - m_historyPosition - 1);
            }
            m_history.Add(item);
            m_historyPosition = m_history.Count - 1;
            UpdateHistoryEnabling();
        }

        private void depDependsOnSelected_CallSelect(object sender, EventArgs e)
        {
            SelectPivotItem(depDependsOnSelected.SelectedItem);
        }

        private void depSelectedDependsOn_CallSelect(object sender, EventArgs e)
        {
            SelectPivotItem(depSelectedDependsOn.SelectedItem);
        }

        private void UpdateHistoryEnabling()
        {
            btnBack.Enabled = m_historyPosition > 0;
            btnForward.Enabled = m_historyPosition < m_history.Count - 1;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (m_historyPosition > 0)
            {
                m_historyPosition--;
                SelectPivotItemCore(m_history[m_historyPosition]);
                UpdateHistoryEnabling();
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (m_historyPosition < m_history.Count - 1)
            {
                m_historyPosition++;
                SelectPivotItemCore(m_history[m_historyPosition]);
                UpdateHistoryEnabling();
            }
        }
    }
}

