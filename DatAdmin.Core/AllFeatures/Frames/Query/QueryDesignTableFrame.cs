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
    public partial class QueryDesignTableFrame : UserControl
    {
        internal ITableStructure m_table;
        internal QueryDesignFrame m_frame;
        Point? m_moveSrc;
        Point? m_resizeSrc;
        //bool m_isChecking;
        int m_lineHeight;
        int m_checkWidth;
        int m_highlightedCheckbox = -1;
        int m_hightlightedFk = -1;
        int m_hightlightedColName = -1;
        internal HashSetEx<string> m_checkedColumns = new HashSetEx<string>();
        internal HashSetEx<string> m_pkCols = new HashSetEx<string>();
        internal HashSetEx<string> m_fkCols = new HashSetEx<string>();
        List<Interval> m_fkButs;
        List<int> m_textLefts;

        [XmlElem]
        public int XPos
        {
            get { return Left; }
            set { Left = value; }
        }

        [XmlElem]
        public int YPos
        {
            get { return Top; }
            set { Top = value; }
        }

        [XmlElem]
        public int XSize
        {
            get { return Width; }
            set { Width = value; }
        }

        [XmlElem]
        public int YSize
        {
            get { return Height; }
            set { Height = value; }
        }

        [XmlElem]
        public string SaveId { get; set; }

        [XmlElem]
        public string ObjectType { get; set; }

        public QueryDesignTableFrame(ITableStructure table, QueryDesignFrame frame, string objtype)
        {
            InitializeComponent();
            SaveId = Guid.NewGuid().ToString();
            Translating.TranslateControl(this);
            m_table = table;
            m_frame = frame;
            ObjectType = objtype;
            FillData();
        }

        public QueryDesignTableFrame(XmlElement xml, QueryDesignFrame frame)
        {
            InitializeComponent();
            Translating.TranslateControl(this);
            m_frame = frame;
            var tbl = new TableStructure(xml.FindElement("Structure"));
            tbl.Parent = new DatabaseStructure();
            foreach (XmlElement rx in xml.SelectNodes("Reference"))
            {
                var fk = new ForeignKey(rx.FindElement("ForeignKey"));
                fk.SetDummyTable(NameWithSchema.LoadFromXml(rx));
                tbl.AddReference(fk);
            }
            m_table = tbl;
            this.LoadPropertiesCore(xml);
            FillData();
        }

        private void FillData()
        {
            var pk = m_table.FindConstraint<IPrimaryKey>();
            if (pk != null) m_pkCols.AddRange(pk.Columns.GetNames());
            foreach (var fk in m_table.GetConstraints<IForeignKey>())
            {
                m_fkCols.AddRange(fk.Columns.GetNames());
            }
            using (var g = Graphics.FromHwnd(IntPtr.Zero))
            {
                m_lineHeight = (int)(g.MeasureString("M", Font).Height + 0.99);
                if (m_lineHeight < 16) m_lineHeight = 16;
                m_checkWidth = CheckBoxRenderer.GetGlyphSize(g, System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal).Width;
            }
            panCols.Height = m_table.Columns.Count * m_lineHeight;
            labTableName.Text = m_table.FullName.ToString();
        }

        private void btnRemoveTable_Click(object sender, EventArgs e)
        {
            m_frame.RemoveTable(this);
        }

        private void windowTitle_MouseDown(object sender, MouseEventArgs e)
        {
            m_moveSrc = new Point(e.X, e.Y);
            ((Control)sender).Capture = true;
        }

        private void windowTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_moveSrc != null)
            {
                int l = Left + e.X - m_moveSrc.Value.X;
                int t = Top + e.Y - m_moveSrc.Value.Y;
                if (l < 0) l = 0;
                if (t < 0) t = 0;
                Left = l;
                Top = t;
                m_frame.Redraw();
            }
        }

        private void windowTitle_MouseUp(object sender, MouseEventArgs e)
        {
            ((Control)sender).Capture = false;
            m_moveSrc = null;
        }

        private void lbxColumns_DragOver(object sender, DragEventArgs e)
        {
            //QueryDesignDrapDropColumn obj = (QueryDesignDrapDropColumn)e.Data.GetData(typeof(QueryDesignDrapDropColumn));
            //if (obj != null)
            //{
            //    if (obj.Frame == this)
            //    {
            //        e.Effect = DragDropEffects.None;
            //        return;
            //    }
            //    Point pt = lbxColumns.PointToClient(new Point(e.X, e.Y));
            //    int idx = lbxColumns.IndexFromPoint(pt);
            //    if (idx >= 0 && idx < lbxColumns.Items.Count)
            //    {
            //        lbxColumns.SelectedIndex = idx;
            //        e.Effect = DragDropEffects.Copy;
            //    }
            //}
        }

        private void lbxColumns_DragDrop(object sender, DragEventArgs e)
        {
            //QueryDesignDrapDropColumn obj = (QueryDesignDrapDropColumn)e.Data.GetData(typeof(QueryDesignDrapDropColumn));
            //if (obj != null && lbxColumns.SelectedIndex >= 0)
            //{
            //    m_frame.AddJoin(obj.Frame, obj.Column, this, lbxColumns.SelectedItem.ToString(), true);
            //}
        }

        public int GetColY(string col)
        {
            int index = m_table.Columns.GetIndex(col);
            if (index >= 0)
            {
                int res = panColParent.Top + m_lineHeight * index + m_lineHeight / 2 - panColParent.VerticalScroll.Value;
                if (res < 0) return 0;
                if (res > Height) return Height;
                return res;
            }
            return 0;
        }

        //private void lbxColumns_Scroll(object Sender, ListBoxExScrollArgs e)
        //{
        //    m_frame.Redraw();
        //}

        private void lbxColumns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //if (m_isChecking) return;
            //if (e.NewValue == CheckState.Checked)
            //{
            //    m_frame.AddColumn(this, lbxColumns.Items[e.Index].ToString());
            //}
            //if (e.NewValue == CheckState.Unchecked)
            //{
            //    m_frame.RemoveColumn(this, lbxColumns.Items[e.Index].ToString());
            //}
        }

        public void CheckColumn(string colname)
        {
            m_checkedColumns.Add(colname);
            panCols.Invalidate();
        }

        public void UncheckColumn(string colname)
        {
            m_checkedColumns.Remove(colname);
            panCols.Invalidate();
        }

        public NameWithSchema FullName { get { return m_table.FullName; } }
        [XmlElem]
        public string Alias
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
        public string AliasOrName
        {
            get
            {
                if (!String.IsNullOrEmpty(Alias)) return Alias;
                return FullName.ToString();
            }
        }
        public ITableStructure Structure { get { return m_table; } }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            m_frame.ReflectChanges();
            m_frame.FillTableNames();
        }

        private void mnuAddAllColumns_Click(object sender, EventArgs e)
        {
            foreach (var col in m_table.Columns)
            {
                if (!m_checkedColumns.Contains(col.ColumnName))
                {
                    m_checkedColumns.Add(col.ColumnName);
                    m_frame.AddColumn(this, col.ColumnName);
                }
            }
            panCols.Invalidate();
        }

        private void mnuRemoveAllColumns_Click(object sender, EventArgs e)
        {
            foreach (var col in m_table.Columns)
            {
                if (m_checkedColumns.Contains(col.ColumnName))
                {
                    m_checkedColumns.Remove(col.ColumnName);
                    m_frame.RemoveColumn(this, col.ColumnName);
                }
            }
            panCols.Invalidate();
        }

        internal QueryDesignFrame.DesignedTable Designed
        {
            get { return new QueryDesignFrame.DesignedTable(m_frame, this); }
        }

        public void Save(XmlElement xml)
        {
            this.SavePropertiesCore(xml);
            new TableStructure(m_table).Save(xml.AddChild("Structure"));
            foreach (var fk in m_table.GetReferencedFrom())
            {
                var x = xml.AddChild("Reference");
                fk.Table.FullName.SaveToXml(x);
                new ForeignKey(fk).Save(x);
            }
        }

        public string GetNameTitle()
        {
            if (!textBox1.Text.IsEmpty()) return textBox1.Text;
            return FullName.ToString();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            m_resizeSrc = new Point(e.X, e.Y);
            ((Control)sender).Capture = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_resizeSrc != null)
            {
                int w = Width + e.X - m_resizeSrc.Value.X;
                int h = Height + e.Y - m_resizeSrc.Value.Y;
                if (w < 80) w = 80;
                if (h < 80) h = 80;
                Width = w;
                Height = h;
                m_frame.Redraw();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            ((Control)sender).Capture = false;
            m_resizeSrc = null;
        }

        private void panCols_Paint(object sender, PaintEventArgs e)
        {
            m_fkButs = new List<Interval>();
            m_textLefts = new List<int>();
            for (int i = 0; i < m_table.Columns.Count; i++)
            {
                System.Windows.Forms.VisualStyles.CheckBoxState state;
                string colname = m_table.Columns[i].ColumnName;
                int y = i * m_lineHeight;
                //e.Graphics.FillRectangle(SystemBrushes.ButtonFace, new Rectangle(0, y, m_checkWidth, m_lineHeight));
                if (m_checkedColumns.Contains(colname))
                {
                    if (i == m_highlightedCheckbox) state = System.Windows.Forms.VisualStyles.CheckBoxState.CheckedHot;
                    else state = System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal;
                }
                else
                {
                    if (i == m_highlightedCheckbox) state = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedHot;
                    else state = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
                }
                CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(0, y), state);
                int x = m_checkWidth + 2;
                if (m_pkCols.Contains(colname))
                {
                    e.Graphics.DrawImage(CoreIcons.primary_key, new Point(x, y));
                    x += 18;
                }
                if (m_fkCols.Contains(colname))
                {
                    m_fkButs.Add(new Interval(x, x + 16));
                    var r = new Rectangle(x, y, 16, 15);
                    if (i == m_hightlightedFk) e.Graphics.FillRectangle(Brushes.Aqua, r);
                    else e.Graphics.FillRectangle(Brushes.White, r);
                    e.Graphics.DrawRectangle(Pens.Black, r);
                    e.Graphics.DrawImage(CoreIcons.foreign_key, new Point(x, y));
                    x += 18;
                }
                else
                {
                    m_fkButs.Add(new Interval());
                }
                m_textLefts.Add(x);
                var r2 = new Rectangle(x, y, panCols.Width - x, m_lineHeight);
                if (m_hightlightedColName == i) e.Graphics.FillRectangle(Brushes.Yellow, r2);
                e.Graphics.DrawString(colname, Font, Brushes.Black, x, y);
            }
        }

        private void panCols_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (m_highlightedCheckbox >= 0)
                {
                    string colname = m_table.Columns[m_highlightedCheckbox].ColumnName;
                    if (m_checkedColumns.Contains(colname))
                    {
                        m_checkedColumns.Remove(colname);
                        m_frame.RemoveColumn(this, colname);
                    }
                    else
                    {
                        m_checkedColumns.Add(colname);
                        m_frame.AddColumn(this, colname);
                    }
                    panCols.Invalidate();
                }
                else if (m_hightlightedColName >= 0)
                {
                    DoDragDrop(new QueryDesignDrapDropColumn { Frame = this, Column = m_table.Columns[m_hightlightedColName].ColumnName }, DragDropEffects.Copy);
                }
                else if (m_hightlightedFk >= 0)
                {
                    AddReferencedFromCol(m_hightlightedFk);
                }
            }
        }

        private void panCols_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && m_hightlightedColName >= 0)
            {
                var menu = new ContextMenuStripEx();
                var mb = new MenuBuilder();
                mb.AddObject(new QueryDesignColumnPopupMenu(this, m_hightlightedColName));
                mb.GetMenuItems(menu.Items);
                menu.Show(this, e.Location);
            }
        }

        internal void AddReferencedFromCol(int colindex)
        {
            string col = m_table.Columns[colindex].ColumnName;
            var fk = m_table.GetKeyWithColumn<IForeignKey>(m_table.Columns[colindex]);
            m_frame.AddTable(fk.PrimaryKeyTable, this, fk);
        }

        internal void AddFilter(int colindex)
        {
            m_frame.AddFilter(this, m_table.Columns[colindex].ColumnName);
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            contextMenuStripTable.Show(btnMenu, new Point(0, btnMenu.Height));
        }

        private void panCols_MouseLeave(object sender, EventArgs e)
        {
            m_highlightedCheckbox = -1;
            m_hightlightedColName = -1;
            m_hightlightedFk = -1;
            panCols.Invalidate();
        }

        private void panCols_MouseMove(object sender, MouseEventArgs e)
        {
            int newcheck = e.Y / m_lineHeight;
            if (e.X > m_checkWidth || newcheck >= m_table.Columns.Count || newcheck < 0) newcheck = -1;
            if (newcheck != m_highlightedCheckbox)
            {
                m_highlightedCheckbox = newcheck;
                panCols.Invalidate();
            }

            int newFk = e.Y / m_lineHeight;
            if (m_fkButs == null || newFk < 0 || newFk >= m_fkButs.Count || !m_fkButs[newFk].Contains(e.X)) newFk = -1;
            if (newFk != m_hightlightedFk)
            {
                m_hightlightedFk = newFk;
                panCols.Invalidate();
            }

            int newCol = e.Y / m_lineHeight;
            if (m_textLefts == null || newCol < 0 || newCol >= m_textLefts.Count || e.X < m_textLefts[newCol]) newCol = -1;
            if (newCol != m_hightlightedColName)
            {
                m_hightlightedColName = newCol;
                panCols.Invalidate();
            }
        }

        private void panCols_DragOver(object sender, DragEventArgs e)
        {
            QueryDesignDrapDropColumn obj = (QueryDesignDrapDropColumn)e.Data.GetData(typeof(QueryDesignDrapDropColumn));
            if (obj != null)
            {
                if (obj.Frame == this)
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }
                Point pt = panCols.PointToClient(new Point(e.X, e.Y));
                int idx = pt.Y / m_lineHeight;
                if (idx >= 0 && idx < m_table.Columns.Count)
                {
                    m_hightlightedColName = idx;
                    e.Effect = DragDropEffects.Copy;
                    panCols.Invalidate();
                }
            }
        }

        private void panCols_DragDrop(object sender, DragEventArgs e)
        {
            QueryDesignDrapDropColumn obj = (QueryDesignDrapDropColumn)e.Data.GetData(typeof(QueryDesignDrapDropColumn));
            if (obj != null && m_hightlightedColName >= 0)
            {
                m_frame.AddJoin(obj.Frame, obj.Column, this, m_table.Columns[m_hightlightedColName].ColumnName, true);
            }
        }

        private void panColParent_Scroll(object sender, ScrollEventArgs e)
        {
            m_frame.Redraw();
        }

        private void mnuAddReferencedTable_Click(object sender, EventArgs e)
        {
            var fk = SelectReferenceForm.RunFk(m_table, null, null);
            if (fk != null)
            {
                m_frame.AddTable(fk.PrimaryKeyTable == m_table.FullName ? fk.Table.FullName : fk.PrimaryKeyTable, this, fk);
            }
        }

        private void mnuRefresh_Click(object sender, EventArgs e)
        {
            m_table = m_frame.LoadTable(new FullDatabaseRelatedName { ObjectName = m_table.FullName, ObjectType = ObjectType ?? "table" });
            FillData();
            panCols.Invalidate();
        }

        private void panColParent_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStripTable.Show(panColParent, e.Location);
            }
        }
    }

    public class QueryDesignDrapDropColumn
    {
        public QueryDesignTableFrame Frame;
        public string Column;
    }

    public class QueryDesignColumnPopupMenu
    {
        QueryDesignTableFrame m_frame;
        int m_colindex;

        public QueryDesignColumnPopupMenu(QueryDesignTableFrame frame, int colindex)
        {
            m_frame = frame;
            m_colindex = colindex;
        }

        [PopupMenuEnabled("s_add_referenced_table")]
        public bool AddReferenceEnabled()
        {
            return m_frame.m_fkCols.Contains(m_frame.m_table.Columns[m_colindex].ColumnName);
        }

        [PopupMenu("s_add_referenced_table")]
        public void AddReference()
        {
            m_frame.AddReferencedFromCol(m_colindex);
        }

        [PopupMenu("s_add_filter")]
        public void AddFilter()
        {
            m_frame.AddFilter(m_colindex);
        }
    }
}
