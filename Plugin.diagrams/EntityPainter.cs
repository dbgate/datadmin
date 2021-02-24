using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DatAdmin;

namespace Plugin.diagrams
{
    //internal class EntityPainter : IDisposable
    //{
    //    Graphics g;
    //    List<int> colwidths;
    //    int acty;
    //    int dleft;
    //    int dright;
    //    int contentWidth;
    //    int height;
    //    DiagramStyle.EntityStyle style;
    //    internal enum Phase { Measure, MeasureColSpan, Draw };
    //    Phase phase;
    //    Dictionary<string, int> fkColsIndexes = new Dictionary<string, int>();
    //    DiagramPainter dp;

    //    DiagramTableItem item;
    //    TableStructure table;
    //    ISqlDialect m_dialect;

    //    Dictionary<int, int> ColumnJoinY;

    //    internal Size Size
    //    {
    //        get { return new Size(dleft + contentWidth + dright, height); }
    //    }

    //    public void Dispose() { }

    //    internal EntityPainter(DiagramTableItem item, Graphics g, DiagramPainter dp, EntityStyle style, Dictionary<int, int> ColumnJoinY, ISqlDialect dialect)
    //    {
    //        colwidths = new List<int>(from s in style.Columns select s.MinimalWidth);
    //        this.style = style;
    //        this.item = item;
    //        table = item.Table;
    //        this.g = g;
    //        this.dp = dp;
    //        this.ColumnJoinY = ColumnJoinY;
    //        m_dialect = dialect;

    //        int fkindex = 0;
    //        foreach (IConstraint cnt in table.Constraints)
    //        {
    //            if (cnt is IForeignKey)
    //            {
    //                foreach (var col in ((IForeignKey)cnt).Columns)
    //                {
    //                    fkColsIndexes[col.ColumnName] = fkindex;
    //                }
    //                fkindex++;
    //            }
    //        }
    //    }

    //    internal string GetColumnData(DiagramStyle.EntityStyle.ColumnData data, int fldindex)
    //    {
    //        switch (data)
    //        {
    //            case DiagramStyle.EntityStyle.ColumnData.None: return "";
    //            case DiagramStyle.EntityStyle.ColumnData.TableName: return table.Name;
    //            case DiagramStyle.EntityStyle.ColumnData.ColumnName: return table.Columns[fldindex].ColumnName;
    //            case DiagramStyle.EntityStyle.ColumnData.TypeName: return m_dialect.GenericTypeToSpecific(table.Columns[fldindex].DataType).ToString();
    //            case DiagramStyle.EntityStyle.ColumnData.TypeSize: return table.Columns[fldindex].DataType.GetSize().ToString();
    //            case DiagramStyle.EntityStyle.ColumnData.FullTypeName: return m_dialect.GenericTypeToSpecific(table.Columns[fldindex].DataType).ToString();
    //            case DiagramStyle.EntityStyle.ColumnData.FkIndex: return (fkColsIndexes[table.Columns[fldindex].ColumnName] + 1).ToString();
    //        }
    //        throw new Exception("internal error");
    //    }

    //    internal string GetColumnString(DiagramStyle.EntityStyle.CellStyle cell, int fldindex)
    //    {
    //        string data;
    //        try
    //        {
    //            data = GetColumnData(cell.Data, fldindex);
    //        }
    //        catch (Exception)
    //        {
    //            data = "";
    //        }
    //        return cell.DataPrefix + data + cell.DataPostfix;
    //    }

    //    private void RunPhase(Phase phase)
    //    {
    //        this.phase = phase;

    //        if (phase == Phase.Draw)
    //        {
    //            g.FillRectangle(dp.GetSolidBrush(style.EntityFrame.BgColor), 0, 0, contentWidth + dleft + dright, height);
    //            RenderVertBorder(0, 0, height, style.EntityFrame.LeftBorder);
    //            RenderVertBorder(dleft + contentWidth, 0, height, style.EntityFrame.LeftBorder);
    //        }

    //        ProcessTopBorder(style.EntityFrame);
    //        ProcessRow(-1, style.Header);

    //        if (style.ColumnsFrame.Visible)
    //        {
    //            ProcessTopBorder(style.ColumnsFrame);
    //            IPrimaryKey key = table.FindConstraint<IPrimaryKey>();
    //            int fldindex = 0;

    //            if (key != null && style.PkColsFrame.Visible)
    //            {
    //                ProcessTopBorder(style.PkColsFrame);
    //                foreach (IColumnStructure col in table.Columns)
    //                {
    //                    if (key.Columns.IndexOfIf(cr => cr.ColumnName == col.ColumnName) >= 0)
    //                    {
    //                        ProcessRow(fldindex, style.RowStyleSet[ColumnStyleType.PrimaryKey]);
    //                    }
    //                    fldindex++;
    //                }
    //                ProcessBottomBorder(style.PkColsFrame);
    //            }
    //            if (style.NoPkColsFrame.Visible)
    //            {
    //                ProcessTopBorder(style.NoPkColsFrame);
    //                fldindex = 0;
    //                foreach (IColumnStructure col in table.Columns)
    //                {
    //                    if (key == null || key.Columns.IndexOfIf(cr => cr.ColumnName == col.ColumnName) < 0)
    //                    {
    //                        ColumnStyleType st;
    //                        if (fkColsIndexes.ContainsKey(col.ColumnName))
    //                        {
    //                            if (col.IsNullable) st = ColumnStyleType.FkNull;
    //                            else st = ColumnStyleType.FkNotNull;
    //                        }
    //                        else
    //                        {
    //                            if (col.IsNullable) st = ColumnStyleType.Null;
    //                            else st = ColumnStyleType.NotNull;
    //                        }
    //                        ProcessRow(fldindex, style.RowStyleSet[st]);
    //                    }
    //                    fldindex++;
    //                }
    //                ProcessBottomBorder(style.NoPkColsFrame);
    //            }
    //            ProcessBottomBorder(style.ColumnsFrame);
    //        }

    //        ProcessBottomBorder(style.EntityFrame);
    //    }

    //    private void ProcessTopBorder(DiagramStyle.EntityStyle.RowFrameStyle frame)
    //    {
    //        ProcessHorBorder(frame.TopBorder);
    //        ProcessPadding(frame.PaddingTop, frame.BgColor);
    //    }

    //    private void ProcessBottomBorder(DiagramStyle.EntityStyle.RowFrameStyle frame)
    //    {
    //        ProcessPadding(frame.PaddingBottom, frame.BgColor);
    //        ProcessHorBorder(frame.BottomBorder);
    //    }

    //    private void ProcessPadding(int hi, Color color)
    //    {
    //        if (hi > 0 && phase == Phase.Draw)
    //        {
    //            g.FillRectangle(dp.GetSolidBrush(color), dleft, acty, contentWidth, hi);
    //        }
    //        acty += hi;
    //    }

    //    private void ProcessHorBorder(DiagramStyle.EntityStyle.BorderStyle border)
    //    {
    //        if (phase == Phase.Draw)
    //        {
    //            RenderHorzBorder(acty, dleft, contentWidth, border);
    //        }
    //        acty += border.Width;
    //    }

    //    private void ProcessRow(int fldindex, DiagramStyle.EntityStyle.RowStyle region)
    //    {
    //        ProcessTopBorder(region);
    //        int rowhi = 0;
    //        for (int i = 0; i < colwidths.Count && i < region.Cells.Length; i++)
    //        {
    //            Size size = MeasureCell(fldindex, region.Cells[i]);
    //            if (size.Height > rowhi) rowhi = (int)size.Height;
    //            if (region.Cells[i].Colspan == 1 && phase == Phase.Measure)
    //            {
    //                if (size.Width > colwidths[i]) colwidths[i] = (int)size.Width;
    //            }
    //            if (region.Cells[i].Colspan > 1 && phase == Phase.MeasureColSpan)
    //            {
    //                int wi = 0;
    //                for (int j = i; j < i + region.Cells[i].Colspan; j++) wi += colwidths[j];
    //                if (size.Width > wi)
    //                {
    //                    float k = (float)size.Width / (float)wi;
    //                    for (int j = i; j < i + region.Cells[i].Colspan; j++) colwidths[j] = (int)(colwidths[j] * k);
    //                }
    //            }
    //        }
    //        if (phase == Phase.Draw)
    //        {
    //            g.FillRectangle(dp.GetSolidBrush(region.BgColor), dleft, acty, contentWidth - 1, rowhi + region.PaddingBottom);
    //            int actx = dleft;
    //            for (int i = 0; i < colwidths.Count && i < region.Cells.Length; )
    //            {
    //                int wi = colwidths[i];
    //                for (int j = i + 1; j < colwidths.Count && j < i + region.Cells[i].Colspan; j++)
    //                {
    //                    wi += colwidths[j];
    //                }
    //                RenderCell(actx, acty, fldindex, wi, rowhi, region, region.Cells[i]);
    //                actx += colwidths[i];
    //                i += Math.Max(region.Cells[i].Colspan, 1);
    //            }
    //        }
    //        if (fldindex >= 0) ColumnJoinY[fldindex] = acty + rowhi / 2;
    //        acty += rowhi;

    //        acty += region.PaddingBottom;

    //        // musime nakreslit rucne, ProcessBottomBorder by premazalo oddelovace sloupcu
    //        if (phase == Phase.Draw)
    //        {
    //            RenderHorzBorder(acty, dleft, contentWidth, region.BottomBorder);
    //        }
    //        acty += region.BottomBorder.Width;

    //        //ProcessBottomBorder(region);
    //    }

    //    private Size MeasureCell(int fldindex, DiagramStyle.EntityStyle.CellStyle cellStyle)
    //    {
    //        int imgwi = 0;
    //        int imghi = 0;
    //        if (cellStyle.Image.IsDefined)
    //        {
    //            imgwi = cellStyle.Image.GdiBitmap.Width;
    //            imghi = cellStyle.Image.GdiBitmap.Height;
    //        }
    //        string data = GetColumnString(cellStyle, fldindex);
    //        SizeF size = g.MeasureString(data, cellStyle.Font.GdiFont);
    //        int w = (int)size.Width, h = (int)size.Height;
    //        w += cellStyle.GetHorizontalPadding();
    //        h += cellStyle.GetVerticalPadding();
    //        return new Size(w + imgwi, Math.Max(h, imghi));
    //    }

    //    private void RenderCell(int x, int y, int fldindex, int rowwi, int rowhi, Plugin.diagrams.DiagramStyle.EntityStyle.RowStyle rowStyle, DiagramStyle.EntityStyle.CellStyle cell)
    //    {
    //        string data = GetColumnString(cell, fldindex);
    //        SizeF tsize = g.MeasureString(data, cell.Font.GdiFont);
    //        //cell.TextAlign == ContentAlignment.
    //        int tx = x + cell.LeftBorder.Width + cell.LeftPadding;
    //        int ty = y + cell.TopPadding;
    //        int imgy = y + cell.TopPadding;
    //        int talwi = rowwi - cell.GetHorizontalPadding();
    //        int talhi = rowhi - cell.GetVerticalPadding();

    //        int imgwi = 0;
    //        int imghi = 0;
    //        if (cell.Image.IsDefined)
    //        {
    //            imgwi = cell.Image.GdiBitmap.Width;
    //            imghi = cell.Image.GdiBitmap.Height;
    //        }

    //        // horizontal alignement
    //        switch (cell.TextAlign)
    //        {
    //            case ContentAlignment.BottomCenter:
    //            case ContentAlignment.MiddleCenter:
    //            case ContentAlignment.TopCenter:
    //                tx = (tx + talwi) / 2 - (int)((tsize.Width + imgwi) / 2);
    //                break;
    //            case ContentAlignment.BottomRight:
    //            case ContentAlignment.MiddleRight:
    //            case ContentAlignment.TopRight:
    //                tx = tx + talwi - (int)tsize.Width - imgwi;
    //                break;
    //        }
    //        // vertical alignement
    //        switch (cell.TextAlign)
    //        {
    //            case ContentAlignment.MiddleCenter:
    //            case ContentAlignment.MiddleLeft:
    //            case ContentAlignment.MiddleRight:
    //                ty = (ty + talhi) / 2 - (int)(tsize.Height / 2);
    //                imgy = (imgy + talhi) / 2 - (int)(imghi / 2);
    //                break;
    //            case ContentAlignment.BottomCenter:
    //            case ContentAlignment.BottomLeft:
    //            case ContentAlignment.BottomRight:
    //                ty = ty + talhi - (int)tsize.Height;
    //                imgy = imgy + talhi - (int)imghi;
    //                break;
    //        }
    //        if (cell.Image.IsDefined) g.DrawImage(cell.Image.GdiBitmap, tx, imgy);
    //        g.DrawString(data, cell.Font.GdiFont, cell.Font.GdiBrush, tx + imgwi, ty);
    //        int y0 = y - rowStyle.PaddingTop;
    //        int yhi = rowhi + rowStyle.PaddingTop + rowStyle.PaddingBottom;
    //        RenderVertBorder(x, y0, yhi, cell.LeftBorder);
    //        RenderVertBorder(x + rowwi - cell.RightBorder.Width, y0, yhi, cell.RightBorder);
    //    }

    //    private void RenderVertBorder(int x, int y, int len, DiagramStyle.EntityStyle.BorderStyle style)
    //    {
    //        if (style.Width > 0)
    //        {
    //            g.DrawLine(dp.GetPen(style.Color, style.Width), x, y, x, y + len);
    //        }
    //    }

    //    private void RenderHorzBorder(int y, int x, int len, DiagramStyle.EntityStyle.BorderStyle style)
    //    {
    //        if (style.Width > 0)
    //        {
    //            g.DrawLine(dp.GetPen(style.Color, style.Width), x, y, x + len, y);
    //        }
    //    }

    //    internal void Run()
    //    {
    //        RunPhase(Phase.Measure);
    //        height = acty;
    //        acty = 0;
    //        RunPhase(Phase.MeasureColSpan);
    //        acty = 0;
    //        contentWidth = colwidths.Sum();
    //        dleft = style.EntityFrame.LeftBorder.Width;
    //        dright = style.EntityFrame.RightBorder.Width;
    //        RunPhase(Phase.Draw);
    //    }
    //}

    //public class DiagramEntityDrawNeededException : Exception { }
}
