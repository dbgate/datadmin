using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Globalization;
using DatAdmin;

namespace Plugin.dbmodel
{
    public class ObjectGridView : DataGridViewEx
    {
        IList m_dataSource;
        ObjectGridOverview m_overview;

        public ObjectGridView()
        {
            base.VirtualMode = true;
            base.AllowUserToAddRows = false;
        }

        public new bool VirtualMode { get { return true; } set { } }
        public new bool AllowUserToAddRows { get { return false; } set { } }

        public bool Modified { get; set; }

        [Browsable(false)]
        public new IList DataSource
        {
            get
            {
                return m_dataSource;
            }
            set
            {
                m_dataSource = value;
                Modified = false;
                DoLoadData();
            }
        }

        private void DoLoadData()
        {
            RowCount = 0;
            Columns.Clear();
            Rows.Clear();
            m_overview = null;
            if (m_dataSource == null) return;
            m_overview = new ObjectGridOverview(m_dataSource);
            foreach (var col in m_overview.Columns)
            {
                DataGridViewColumn dcol = new DataGridViewTextBoxColumn();
                dcol.HeaderText = Texts.Get(col);
                dcol.Name = col;
                dcol.SortMode = DataGridViewColumnSortMode.Programmatic;
                Columns.Add(dcol);
            }
            RowCount = m_overview.RowCount;
        }

        protected override void OnCellValueNeeded(DataGridViewCellValueEventArgs e)
        {
            base.OnCellValueNeeded(e);
            if (m_overview != null && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                e.Value = m_overview[e.RowIndex, e.ColumnIndex];
            }
        }

        protected override void OnCellValuePushed(DataGridViewCellValueEventArgs e)
        {
            base.OnCellValuePushed(e);
            if (m_overview != null && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                m_overview[e.RowIndex, e.ColumnIndex] = e.Value.SafeToString();
                Modified = true;
            }
        }

        protected override void OnColumnHeaderMouseClick(DataGridViewCellMouseEventArgs e)
        {
            base.OnColumnHeaderMouseClick(e);
            var dcol = Columns[e.ColumnIndex];
            SortOrder order = dcol.HeaderCell.SortGlyphDirection;
            foreach (DataGridViewColumn d in Columns) d.HeaderCell.SortGlyphDirection = SortOrder.None;
            if (order == SortOrder.Ascending) order = SortOrder.Descending;
            else order = SortOrder.Ascending;
            dcol.HeaderCell.SortGlyphDirection = order;
            object oldData = null;
            if (CurrentCell != null && CurrentCell.RowIndex >= 0) oldData = m_overview[CurrentCell.RowIndex];
            m_overview.Sort(e.ColumnIndex, order);
            if (oldData != null) CurrentCell = Rows[m_overview.FindRow(oldData)].Cells[CurrentCell.ColumnIndex];
            Refresh();
        }

        protected override void OnCellBeginEdit(DataGridViewCellCancelEventArgs e)
        {
            base.OnCellBeginEdit(e);
            if (e.RowIndex >= 0)
            {
                if (m_overview.GetPropertyDef(m_overview[e.RowIndex], e.ColumnIndex).Property.GetSetMethod() == null)
                {
                    // no set method available
                    e.Cancel = true;
                }
            }
        }

        public void FillCellSelection(object value)
        {
            foreach (DataGridViewCell cell in SelectedCells)
            {
                SetCellValue(cell, value);
            }
        }

        private void SetCellValue(DataGridViewCell cell, object value)
        {
            try
            {
                m_overview[cell.RowIndex, cell.ColumnIndex] = value.SafeToString();
            }
            catch
            { }
        }

        public object SelectedObject
        {
            get
            {
                if (CurrentCell != null && CurrentCell.RowIndex >= 0) return m_overview[CurrentCell.RowIndex];
                return null;
            }
        }
    }

    public class ObjectGridPropertyDef
    {
        //public object Obj;
        public Type BaseType;
        public string DisplayName;
        public PropertyInfo Property;
    }

    public class ObjectGridOverview
    {
        List<object> m_data;
        Dictionary<Type, List<ObjectGridPropertyDef>> m_types = new Dictionary<Type, List<ObjectGridPropertyDef>>();
        Dictionary<Type, List<ObjectGridPropertyDef>> m_colDefs = new Dictionary<Type, List<ObjectGridPropertyDef>>();
        List<string> m_columns = new List<string>();
        Dictionary<string, int> m_colWeights = new Dictionary<string, int>();
        public ObjectGridOverview(IList data)
        {
            m_data = new List<object>(data.Count);
            foreach (var obj in data)
            {
                m_data.Add(obj);
                var type = obj.GetType();
                if (m_types.ContainsKey(type)) continue;
                m_types[type] = GetProperties(type);
            }
            var cols = new HashSetEx<string>();
            foreach (var lst in m_types.Values)
            {
                foreach (var def in lst)
                {
                    cols.Add(def.DisplayName);
                }
            }
            m_columns = new List<string>(cols);
            m_columns.Sort(CompareCols);

            foreach (var tp in m_types.Keys)
            {
                m_colDefs[tp] = new List<ObjectGridPropertyDef>();
                foreach (string col in m_columns)
                {
                    ObjectGridPropertyDef def = null;
                    foreach (var d in m_types[tp])
                    {
                        if (d.DisplayName == col)
                        {
                            def = d;
                            break;
                        }
                    }
                    m_colDefs[tp].Add(def);
                }
            }
        }

        private int CompareCols(string a, string b)
        {
            int res = m_colWeights.Get(a, 0) - m_colWeights.Get(b, 0);
            if (res != 0) return res;
            return String.Compare(Texts.Get(a), Texts.Get(b), true);
        }

        public List<string> Columns { get { return m_columns; } }
        public int RowCount { get { return m_data.Count; } }

        private List<ObjectGridPropertyDef> GetProperties(Type type)
        {
            var res = new List<ObjectGridPropertyDef>();
            foreach (var desc in type.GetProperties())
            {
                bool show = false;
                int weight = 0;
                foreach (ShowInGridAttribute attr in desc.GetCustomAttributes(typeof(ShowInGridAttribute), true))
                {
                    show = true;
                    weight = attr.Order;
                }
                if (!show) continue;
                string dname = desc.Name;
                foreach (DatAdmin.DisplayNameAttribute attr in desc.GetCustomAttributes(typeof(DatAdmin.DisplayNameAttribute), true))
                {
                    dname = attr.DisplayName;
                }
                m_colWeights[dname] = weight;
                var od = new ObjectGridPropertyDef();
                od.BaseType = type;
                od.DisplayName = dname;
                od.Property = desc;
                res.Add(od);
            }
            return res;
        }

        private string GetPropertyData(object obj, int col)
        {
            var def = GetPropertyDef(obj, col);
            if (def == null) return "N/A";
            return def.Property.CallGet(obj).SafeToString();
        }

        public ObjectGridPropertyDef GetPropertyDef(object obj, int col)
        {
            return m_colDefs[obj.GetType()][col];
        }

        public string this[int row, int col]
        {
            get
            {
                return GetPropertyData(m_data[row], col);
            }
            set
            {
                var obj = m_data[row];
                var def = GetPropertyDef(obj, col);
                if (def == null) return;
                def.Property.CallSet(obj, Convert.ChangeType(value, def.Property.PropertyType, CultureInfo.InvariantCulture));
            }
        }

        public void Sort(int colindex, SortOrder order)
        {
            m_data.Sort((a, b) => CompareRows(a, b, colindex, order));
        }

        private int CompareRows(object a, object b, int colindex, SortOrder order)
        {
            string sa = GetPropertyData(a, colindex), sb = GetPropertyData(b, colindex);
            int res = String.Compare(sa ?? "", sb ?? "", true);
            if (order == SortOrder.Descending) res *= -1;
            return res;
        }

        public int FindRow(object oldData)
        {
            return m_data.IndexOf(oldData);
        }

        public object this[int row]
        {
            get { return m_data[row]; }
        }
    }
}
