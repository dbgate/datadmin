using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace DatAdmin
{
    public class TableDataGrid : BinaryGridView
    {
        StringFormat m_gridTextFormat = new StringFormat(StringFormat.GenericDefault);
        StringFormat m_rowNumberTextFormat = new StringFormat(StringFormat.GenericDefault);
        StringFormat m_headerFormat = new StringFormat(StringFormat.GenericDefault);
        DmlfSortOrderCollection m_sqlSortOrder;
        // map column index => sort order (>0: ASC, <0: DESC)
        Dictionary<int, int> m_sortOrders = new Dictionary<int, int>();
        GetLookupEventArgs m_lookupEventArgs = new GetLookupEventArgs();
        SolidBrush m_lookupBrush;
        TableDataSettings m_settings;
        DataFormatSettings m_fmtSettings;
        List<Font> m_fonts = new List<Font>();
        List<Font> m_boldFonts = new List<Font>();
        List<Font> m_boldItalicFonts = new List<Font>();
        List<Font> m_italicFonts = new List<Font>();
        const int FONTSIZES = 4;
        const float FONTKOEF = 1.2f;
        int m_fontPosition;
        int m_showDataErrors = 0;
        TD_DisplayModel m_headerModel;
        //Brush HIGHLIGHTBRUSH = Brushes.DarkOrange;

        public List<string> ZoomNames = new List<string>();

        public TableDataGrid()
        {
            try
            {
                SetSettings(GlobalSettings.Pages.TableData(), GlobalSettings.Pages.DataFormat());
            }
            catch
            {
                SetSettings(new TableDataSettings(), new DataFormatSettings());
            }

            m_gridTextFormat.Alignment = StringAlignment.Near;
            m_gridTextFormat.LineAlignment = StringAlignment.Center;
            m_gridTextFormat.Trimming = StringTrimming.EllipsisCharacter;
            m_gridTextFormat.FormatFlags = StringFormatFlags.NoWrap;

            m_rowNumberTextFormat.Alignment = StringAlignment.Center;
            m_rowNumberTextFormat.LineAlignment = StringAlignment.Center;
            m_rowNumberTextFormat.Trimming = StringTrimming.EllipsisCharacter;
            m_rowNumberTextFormat.FormatFlags = StringFormatFlags.NoWrap;

            m_headerFormat.Alignment = StringAlignment.Near;
            m_headerFormat.LineAlignment = StringAlignment.Center;
            m_headerFormat.Trimming = StringTrimming.EllipsisCharacter;
            m_headerFormat.FormatFlags = StringFormatFlags.NoWrap;

            ZoomNames.Add("100%");
            m_fonts.Add(Font);
            m_boldFonts.Add(new Font(Font.FontFamily, Font.Size, FontStyle.Bold));
            m_italicFonts.Add(new Font(Font.FontFamily, Font.Size, FontStyle.Italic));
            m_boldItalicFonts.Add(new Font(Font.FontFamily, Font.Size, FontStyle.Bold | FontStyle.Italic));
            float size = Font.Size; float percent = 100;
            for (int i = 0; i < FONTSIZES; i++)
            {
                size *= FONTKOEF; percent *= FONTKOEF;
                m_fonts.Add(new Font(Font.FontFamily, size, Font.Style));
                m_boldFonts.Add(new Font(Font.FontFamily, size, FontStyle.Bold));
                m_italicFonts.Add(new Font(Font.FontFamily, size, FontStyle.Italic));
                m_boldItalicFonts.Add(new Font(Font.FontFamily, size, FontStyle.Bold | FontStyle.Italic));
                ZoomNames.Add(String.Format("{0}%", ((int)(percent + 0.5))));
            }
            size = Font.Size; percent = 100;
            for (int i = 0; i < FONTSIZES; i++)
            {
                size /= FONTKOEF; percent /= FONTKOEF;
                m_fonts.Insert(0, new Font(Font.FontFamily, size, Font.Style));
                m_boldFonts.Insert(0, new Font(Font.FontFamily, size, FontStyle.Bold));
                m_italicFonts.Insert(0, new Font(Font.FontFamily, size, FontStyle.Italic));
                m_boldItalicFonts.Insert(0, new Font(Font.FontFamily, size, FontStyle.Bold | FontStyle.Italic));
                ZoomNames.Insert(0, String.Format("{0}%", ((int)(percent + 0.5))));
            }
            m_fontPosition = FONTSIZES;

            try
            {
                m_lookupBrush = new SolidBrush(m_settings._Style.LookupHintColor);
            }
            catch
            {
                m_lookupBrush = new SolidBrush(Color.Gray);
            }

            Disposed += new EventHandler(TableDataGrid_Disposed);
            MouseWheel += new MouseEventHandler(TableDataGrid_MouseWheel);

            GlobalSettings.OnChange += new Action(GlobalSettings_OnChange);
        }

        Font SortOrderMarkFont { get { return m_fonts[m_fontPosition]; } }
        Font RowNumberFont { get { return m_fonts[m_fontPosition]; } }
        Font RowSelectedNumberFont { get { return m_boldFonts[m_fontPosition]; } }
        Font HeaderFont { get { return m_fonts[m_fontPosition]; } }
        Font HeaderNotNullFont { get { return m_boldFonts[m_fontPosition]; } }
        Font HeaderLinkedFont { get { return m_italicFonts[m_fontPosition]; } }
        Font HeaderLinkedNotNullFont { get { return m_boldItalicFonts[m_fontPosition]; } }

        void GlobalSettings_OnChange()
        {
            SetSettings(m_settings, m_fmtSettings);
        }

        public event EventHandler ZoomChanged;

        public TableDataSettings Settings { get { return m_settings; } }
        public bool PersistentColWidths { get { return m_settings.ColumnsAutoSizeMode == DataGridViewAutoSizeColumnsMode.None; } }
        public int FirstRowOffset { get; set; }

        void TableDataGrid_MouseWheel(object sender, MouseEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control && e.Delta != 0)
            {
                if (e.Delta > 0) m_fontPosition--;
                else m_fontPosition++;
                if (m_fontPosition < 0) m_fontPosition = 0;
                if (m_fontPosition >= m_fonts.Count) m_fontPosition = m_fonts.Count - 1;
                SetFont(m_fontPosition);
                UpdateRowHeight();
                if (ZoomChanged != null) ZoomChanged(this, EventArgs.Empty);
            }
        }

        public string ZoomName { get { return ZoomNames[m_fontPosition]; } }

        public void SetZoomName(string name)
        {
            SetFont(ZoomNames.IndexOf(name));
            UpdateRowHeight();
            if (ZoomChanged != null) ZoomChanged(this, EventArgs.Empty);
        }

        private void SetFont(int pos)
        {
            m_fontPosition = pos;
            Font = m_fonts[pos];
        }

        void TableDataGrid_Disposed(object sender, EventArgs e)
        {
            // makes probles when quit application
            //for (int i = 0; i < m_fonts.Count; i++)
            //{
            //    if (i != m_fontPosition) m_fonts[i].Dispose();
            //}
            GlobalSettings.OnChange -= new Action(GlobalSettings_OnChange);
        }

        private void MyPaintCell(DataGridViewCellPaintingEventArgs e)
        {
            var style = m_settings._Style;
            if (m_lookupBrush.Color != style.LookupHintColor)
            {
                m_lookupBrush.Dispose();
                m_lookupBrush = new SolidBrush(style.LookupHintColor);
            }
            BedRow row = GetRow(e.RowIndex);
            row.ReadValue(e.ColumnIndex);
            var type = row.GetFieldType();
            //object value = row[e.ColumnIndex];
            //DataRow row = ((DataRowView)Rows[e.RowIndex].DataBoundItem).Row;
            //object value = row[e.ColumnIndex, row.RowState == DataRowState.Deleted ? DataRowVersion.Original : DataRowVersion.Current];
            //DataColumn col = row.Table.Columns[e.ColumnIndex];

            string sval = null;
            string lookupVal = null;
            Bitmap icon = null;

            //Brush bg = Brushes.White;
            Brush fg = Brushes.Black;
            Brush lfg = m_lookupBrush;

            //if (e.RowIndex > 0 && e.RowIndex % 6 == 2) bg = NiceColorTable.OddRowBrush;
            //if (e.RowIndex > 0 && e.RowIndex % 6 == 5) bg = NiceColorTable.EvenRowBrush;

            switch (type)
            {
                case TypeStorage.Null:
                    sval = "(NULL)";
                    fg = Brushes.Gray;
                    //bg = Brushes.LightGray;
                    break;
                case TypeStorage.ByteArray:
                    {
                        byte[] data = row.GetByteArray();
                        //sval = String.Format("BIN, {0}", Texts.Get("s_bytes$count", "count", data.Length));
                        sval = Texts.Get("s_bytes$count", "count", data.Length);
                        icon = CoreIcons.picture;
                    }
                    break;
                default:
                    {
                        var fmt = DataSource.BedConvertor.Formatter;
                        fmt.ReadFrom(row);
                        sval = fmt.GetText();
                    }
                    break;
            }

            if (GetLookupInfo != null && (type.IsNumber() || type == TypeStorage.String))
            {
                m_lookupEventArgs.ColumnIndex = e.ColumnIndex;
                m_lookupEventArgs.RowIndex = e.RowIndex;
                m_lookupEventArgs.Value = sval;
                m_lookupEventArgs.LookupValue = null;
                GetLookupInfo(this, m_lookupEventArgs);
                if (m_lookupEventArgs.LookupValue != null)
                {
                    lookupVal = m_lookupEventArgs.LookupValue;
                }
            }

            //if (value == null || value == DBNull.Value)
            //{
            //    sval = "(NULL)";
            //    bg = Brushes.LightGray;
            //}
            //else if (value is byte[])
            //{
            //    byte[] data = (byte[])value;
            //    //sval = String.Format("BIN, {0}", Texts.Get("s_bytes$count", "count", data.Length));
            //    sval = Texts.Get("s_bytes$count", "count", data.Length);
            //    icon = CoreIcons.picture;
            //}
            //else if (!(value is bool))
            //{
            //    sval = value.ToString();

            //    if (GetLookupInfo != null)
            //    {
            //        m_lookupEventArgs.ColumnIndex = e.ColumnIndex;
            //        m_lookupEventArgs.Value = value;
            //        m_lookupEventArgs.LookupValue = null;
            //        GetLookupInfo(this, m_lookupEventArgs);
            //        if (m_lookupEventArgs.LookupValue != null)
            //        {
            //            lookupVal = m_lookupEventArgs.LookupValue;
            //        }
            //    }
            //}

            string firstLine = null;
            if (sval != null)
            {
                if (sval.Contains("\n"))
                {
                    string sval0 = sval;
                    int lines = 1;
                    foreach (char c in sval) if (c == '\n') lines += 1;
                    //sval = String.Format("TEXT, {0}", Texts.Get("s_lines$count", "count", lines));
                    sval = Texts.Get("s_lines$count", "count", lines);
                    fg = Brushes.Gray;
                    icon = CoreIcons.text2;
                    int pos = sval0.IndexOf('\n');
                    firstLine = sval0.Substring(0, pos).TrimEnd();
                }
            }

            var ctype = DataGridCellType.Regular;

            if (e.ColumnIndex >= 0 && !ReadOnly)
            {
                switch (row.RowState)
                {
                    case BedRowState.Added:
                        ctype = DataGridCellType.AddedRow;
                        break;
                    case BedRowState.Deleted:
                    case BedRowState.Detached:
                        ctype = DataGridCellType.RemovedRow;
                        break;
                    case BedRowState.Modified:
                        if (row.IsChanged(e.ColumnIndex)) ctype = DataGridCellType.ModifiedCell;
                        else ctype = DataGridCellType.ModifiedRow;
                        break;
                }
            }

            if (HightlightVisible && HighlightRow >= 0 && e.RowIndex == HighlightRow) ctype = DataGridCellType.Highlight;
            if (HightlightVisible && HighlightColumn >= 0 && e.ColumnIndex == HighlightColumn) ctype = DataGridCellType.Highlight;

            //if (CurrentCell.ColumnIndex == e.ColumnIndex && CurrentCell.RowIndex == e.RowIndex)
            if (Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
            {
                ctype = DataGridCellType.Selected;
                //bg = Brushes.Navy;
                fg = Brushes.White;
                lfg = m_lookupBrush;
            }

            style.PaintCellBackground(e, ctype);
            //e.Graphics.FillRectangle(bg, e.CellBounds);

            int dleft = 0;
            if (icon != null)
            {
                int dtop = (e.CellBounds.Height - icon.Height) / 2;
                e.Graphics.DrawImage(icon, new Rectangle(e.CellBounds.Left + 1, e.CellBounds.Top + dtop, icon.Width, icon.Height));
                dleft = icon.Width + 1;
            }
            if (sval != null)
            {
                Rectangle r = e.CellBounds;
                r.X += e.CellStyle.Padding.Left + dleft;
                r.Width -= (e.CellStyle.Padding.Right + e.CellStyle.Padding.Left + dleft);
                r.Y += e.CellStyle.Padding.Top;
                r.Height -= (e.CellStyle.Padding.Bottom + e.CellStyle.Padding.Top);
                e.Graphics.DrawString(sval, Font, fg, r, m_gridTextFormat);
                if (lookupVal != null || firstLine != null)
                {
                    SizeF s = e.Graphics.MeasureString(sval, Font);
                    Rectangle rl = r;
                    rl.X = r.Left + (int)s.Width + 3;
                    rl.Width = r.Right - rl.Left;
                    if (lookupVal != null) e.Graphics.DrawString(lookupVal, Font, lfg, rl, m_gridTextFormat);
                    if (firstLine != null) e.Graphics.DrawString(firstLine, Font, Brushes.Black, rl, m_gridTextFormat);
                }
            }
            else
            {
                e.PaintContent(e.CellBounds);
            }
            if (row.RowState == BedRowState.Deleted)
            {
                int strikey = (e.CellBounds.Top + e.CellBounds.Bottom) / 2;
                var pen = Pens.Black;
                if ((e.State & DataGridViewElementStates.Selected) != 0) pen = Pens.Yellow;
                e.Graphics.DrawLine(pen, e.CellBounds.Left, strikey, e.CellBounds.Right, strikey);
            }
            e.Paint(e.CellBounds, DataGridViewPaintParts.Border);
            e.Handled = true;
        }

        //private Color BlandColor(Color c)
        //{
        //    return Color.FromArgb((c.R + 255) / 2, (c.G + 255) / 2, (c.B + 255) / 2);
        //}

        //protected override void OnCellEndEdit(DataGridViewCellEventArgs e)
        //{
        //    base.OnCellEndEdit(e);
        //    var row = GetRow(e.RowIndex);
        //    //row.EndEdit();
        //    InvalidateDataRow(row);
        //}

        int m_lastCurrentColumn = -1;
        protected override void OnSelectionChanged(EventArgs e)
        {
            base.OnSelectionChanged(e);
            if (m_lastCurrentColumn >= 0 && m_lastCurrentColumn < ColumnCount)
            {
                InvalidateCell(m_lastCurrentColumn, -1);
            }
            if (CurrentCell != null)
            {
                InvalidateCell(CurrentCell.ColumnIndex, -1);
            }
        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 0 && e.RowIndex >= 0)
                {
                    MyPaintRowNumber(e);
                }
                else if (e.RowIndex < 0)
                {
                    MyPaintHeader(e);
                }
                else
                {
                    MyPaintCell(e);
                }
            }
            catch (Exception)
            {
                // draw normaly
            }
            base.OnCellPainting(e);
        }

        private void MyPaintRowNumber(DataGridViewCellPaintingEventArgs e)
        {
            Rectangle r = e.CellBounds;
            Font fnt = RowNumberFont;
            if (CurrentCell != null && CurrentCell.RowIndex == e.RowIndex)
            {
                fnt = RowSelectedNumberFont;
            }
            var ctype = DataGridHeaderCellType.Regular;
            if (CurrentCell != null && CurrentCell.RowIndex == e.RowIndex) ctype = DataGridHeaderCellType.Current;
            if (HightlightVisible && HighlightRow == e.RowIndex && HighlightRow >= 0) ctype = DataGridHeaderCellType.Highlight;
            m_settings._Style.PaintRowNumberBackground(e, ctype);

            //e.PaintContent(e.CellBounds);
            e.Graphics.DrawString((e.RowIndex + 1 + FirstRowOffset).ToString(), fnt, Brushes.Black, r, m_rowNumberTextFormat);
            e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);
            e.Handled = true;
        }

        private void MyPaintHeader(DataGridViewCellPaintingEventArgs e)
        {
            var ctype = DataGridHeaderCellType.Regular;
            if (CurrentCell != null && CurrentCell.ColumnIndex == e.ColumnIndex)
            {
                ctype = DataGridHeaderCellType.Current;
                m_lastCurrentColumn = CurrentCell.ColumnIndex;
            }
            if (HightlightVisible && HighlightColumn == e.ColumnIndex && HighlightColumn >= 0) ctype = DataGridHeaderCellType.Highlight;
            m_settings._Style.PaintColumnHeaderBackground(e, ctype);

            Font hdrFont = HeaderFont;
            Rectangle headerRect = e.CellBounds;
            headerRect.Width -= 14;
            headerRect.X += 14;

            if (m_headerModel != null && e.ColumnIndex >= 0 && e.ColumnIndex < m_headerModel.Count)
            {
                var col = m_headerModel[e.ColumnIndex];
                bool ispk = col.IsPrimaryKey;
                bool isfk = col.IsForeignKey;
                Point imgpt = e.CellBounds.Location;
                imgpt.X += 2;
                imgpt.Y += (e.CellBounds.Height - 10) / 2;
                if (ispk) e.Graphics.DrawImage(StdIcons.primarykey_10, imgpt);
                else if (isfk) e.Graphics.DrawImage(StdIcons.foreignkey_10, imgpt);
                if (!col.IsNullable)
                {
                    if (col.IsLinked) hdrFont = HeaderLinkedNotNullFont;
                    else hdrFont = HeaderNotNullFont;
                }
                else
                {
                    if (col.IsLinked) hdrFont = HeaderLinkedFont;
                    else hdrFont = HeaderFont;
                }
            }

            if (e.ColumnIndex >= 0)
            {
                var titleRect = headerRect;
                if (m_sortOrders.ContainsKey(e.ColumnIndex)) titleRect.Width -= 15;
                e.Graphics.DrawString(Columns[e.ColumnIndex].HeaderText, hdrFont, Brushes.Black, titleRect, m_headerFormat);

                if (m_sortOrders.ContainsKey(e.ColumnIndex))
                {
                    Rectangle r = e.CellBounds;
                    int sorder = Math.Abs(m_sortOrders[e.ColumnIndex]);
                    r.X = r.Right - 8;
                    Point imgpt = new Point(e.CellBounds.Right - 15, e.CellBounds.Top + (e.CellBounds.Height - 10) / 2);
                    e.Graphics.DrawImage(m_sortOrders[e.ColumnIndex] > 0 ? StdIcons.sort_ascending : StdIcons.sort_descending, imgpt);
                    e.Graphics.DrawString(sorder.ToString(), SortOrderMarkFont, Brushes.Black, r, m_gridTextFormat);
                }
            }

            e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);
            e.Handled = true;
        }

        public TD_DisplayModel HeaderModel
        {
            get { return m_headerModel; }
            set
            {
                if (m_headerModel != value)
                {
                    m_headerModel = value;
                    foreach (DataGridViewColumn col in Columns) InvalidateCell(col.HeaderCell);
                }
            }
        }

        public DmlfSortOrderCollection SqlSortOrder
        {
            get { return m_sqlSortOrder; }
            set
            {
                m_sqlSortOrder = value;
                m_sortOrders.Clear();
                int colindex = 0;
                if (m_sqlSortOrder == null) return;
                foreach (DataGridViewColumn col in Columns)
                {
                    var colres = (DmlfResultField)col.Tag;
                    int sortindex = m_sqlSortOrder.GetExpressionIndex(colres.Expr);
                    if (sortindex >= 0)
                    {
                        var sort = m_sqlSortOrder[sortindex];
                        if (sort.OrderType == DmlfSortOrderType.Ascending)
                        {
                            col.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                            m_sortOrders[colindex] = sortindex + 1;
                        }
                        if (sort.OrderType == DmlfSortOrderType.Descendning)
                        {
                            col.HeaderCell.SortGlyphDirection = SortOrder.Descending;
                            m_sortOrders[colindex] = -sortindex - 1;
                        }
                    }
                    colindex++;
                }
            }
        }
        public event GetLookupInfoDelegate GetLookupInfo;

        void UpdateRowHeight()
        {
            int newheight = (int)(Font.Height * (1.0 + m_settings.RowSpacing / 100.0));
            if (RowTemplate.Height != newheight)
            {
                var src = DataSource;
                DataSource = null;
                RowTemplate.Height = newheight;
                //foreach (DataGridViewRow row in Rows) row.Height = RowTemplate.Height;
                DataSource = src;
                ResizeColumns();
            }
        }

        private void ResizeColumns()
        {
            AutoResizeColumns(m_settings.ColumnsAutoSizeMode);
            if ((m_settings.ColumnsAutoSizeMode & 
                (DataGridViewAutoSizeColumnsMode.AllCells 
                | DataGridViewAutoSizeColumnsMode.ColumnHeader)) 
                != 0)
            {
                using (var g = Graphics.FromHwnd(Handle))
                {
                    foreach (DataGridViewColumn col in Columns)
                    {
                        var size = g.MeasureString(col.HeaderText, HeaderNotNullFont);
                        if (col.Width < size.Width + 14) col.Width = (int)size.Width + 15;
                    }
                }
            }
            if (m_settings.ColumnsAutoSizeMode != DataGridViewAutoSizeColumnsMode.None)
            {
                int wisum = 0;
                foreach (DataGridViewColumn col in Columns)
                {
                    if (col.Width > m_settings.MaxColumnWidth) col.Width = m_settings.MaxColumnWidth;
                    wisum += col.Width;
                }
                if (wisum > 0 && wisum < Width - 80)
                {
                    float koef = (float)(Width - 80) / wisum;
                    foreach (DataGridViewColumn col in Columns)
                    {
                        col.Width = (int)(col.Width * koef);
                    }

                }
            }
        }

        public void SetSettings(TableDataSettings value, DataFormatSettings fvalue)
        {
            m_settings = value;
            if (m_settings.Pages != null) m_settings.Pages.WantLoaded();
            m_fmtSettings = fvalue;
            if (m_fmtSettings.Pages != null) m_fmtSettings.Pages.WantLoaded();
            if (DataSource != null) DataSource.BedConvertor = new BedValueConvertor(m_fmtSettings);
            GridColor = m_settings._Style.GridColor;
            UpdateRowHeight();
            Invalidate();
        }

        public void SetDataSource(BedTable table, int rowOffset)
        {
            FirstRowOffset = rowOffset;
            if (table != null) table.BedConvertor = new BedValueConvertor(m_fmtSettings);
            DataSource = table;
            ResizeColumns();
        }

        public void DeletePopupRow()
        {
            BedRow dbrow = GetPopupRow();
            if (dbrow == null) return;
            bool inserted = dbrow.RowState == BedRowState.Added;
            DataSource.Rows.Remove(dbrow);
            if (!inserted) this.MoveCurrentCell(0, 1);
        }

        public List<int> GetSelectedOrHighlightedRows()
        {
            var lst = new List<int>();
            if (HighlightRow >= 0)
            {
                lst.Add(HighlightRow);
            }
            else
            {
                foreach (DataGridViewCell cell in SelectedCells)
                {
                    if (!lst.Contains(cell.RowIndex)) lst.Add(cell.RowIndex);
                }
            }
            lst.Sort();
            lst.Reverse();
            return lst;
        }

        public List<int> GetSelectedOrHighlightedCols()
        {
            var lst = new List<int>();
            if (HighlightColumn >= 0)
            {
                lst.Add(HighlightColumn);
            }
            else
            {
                foreach (DataGridViewCell cell in SelectedCells)
                {
                    if (!lst.Contains(cell.ColumnIndex)) lst.Add(cell.ColumnIndex);
                }
            }
            lst.Sort();
            return lst;
        }

        public void DeleteSelectedRows()
        {
            var lst = GetSelectedOrHighlightedRows();
            foreach (int index in lst)
            {
                DataSource.Rows.Remove(GetRow(index));
            }
            //if (SelectedRows.Count > 0)
            //{
            //    foreach (DataGridViewRow row in SelectedRows) DataSource.Rows.Remove(GetRow(row.Index));
            //}
            //else
            //{
            //    DeleteCurrentRow();
            //}
        }

        public void DuplicatePopupRow()
        {
            BedRow dbrow = GetPopupRow();
            if (dbrow == null) return;
            var newrow = DataSource.NewRow();
            var autoinc = dbrow.Structure.FindAutoIncrementColumn();
            int autoord = -1;
            if (autoinc != null) autoord = autoinc.ColumnOrder;

            for (int i = 0; i < newrow.FieldCount; i++)
            {
                if (i == autoord) continue;
                newrow[i] = dbrow[i];
            }
            DataSource.Rows.Insert(PopupRow + 1, newrow);
        }

        public void RevertPopupRowChanges()
        {
            var lst = GetSelectedOrHighlightedRows();
            foreach (int row in lst)
            {
                BedRow dbrow = GetRow(row);
                if (dbrow == null) continue;
                dbrow.RevertChanges();
                InvalidateDataRow(dbrow);
            }
        }

        public void RevertAllChanges()
        {
            DataSource.RevertAllChanges();
            Invalidate();
        }

        public void FillCellSelection(object value)
        {
            foreach (DataGridViewCell cell in SelectedCells)
            {
                SetCellValue(cell, value);
                InvalidateCell(cell);
            }
        }

        private void SetCellValue(DataGridViewCell cell, object value)
        {
            try
            {
                m_showDataErrors++;
                GetRow(cell.RowIndex)[cell.ColumnIndex] = value;
                //if (cell == null) return;
                //if (value == null)
                //{
                //    cell.Value = DBNull.Value;
                //    if (cell.Value is string) cell.Value = null;
                //}
                //else
                //{
                //    // better to call 2x for sure
                //    cell.Value = value;
                //    //cell.Value = value;
                //    //dataGridView1.NotifyCurrentCellDirty(true);
                //}
                ////((DataRowView)cell.OwningRow.DataBoundItem).Row.EndEdit();
            }
            finally
            {
                m_showDataErrors--;
            }
        }

        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            base.OnDataError(false, e);
            Logging.Warning("Data error, row={0}, col={1}, error={2}", e.RowIndex, e.ColumnIndex, e.Exception.Message);
            if (m_showDataErrors > 0) StdDialog.ShowError(e.Exception.Message);
        }

        public void UnhighlightHeaders()
        {
            HightlightVisible = false;
            //if (col >= 0) InvalidateCell(col, -1);
            //if (row >= 0) InvalidateCell(-1, row);
            if (HighlightColumn >= 0) InvalidateColumn(HighlightColumn);
            if (HighlightRow >= 0) InvalidateRow(HighlightRow);
        }
    }

    public class GetLookupEventArgs : EventArgs
    {
        public int ColumnIndex;
        public int RowIndex;
        public string LookupValue;
        public object Value;
    }

    public delegate void GetLookupInfoDelegate(object sender, GetLookupEventArgs e);
}
