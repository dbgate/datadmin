using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using DatAdmin;
using System.Xml;
using System.IO;
using System.Linq;
//using ColumnStyleType = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnStyleType;

namespace Plugin.diagrams
{
    public class Diagram
    {
        public List<DiagramTableItem> Tables = new List<DiagramTableItem>();
        DiagramStyle m_style = new DatAdminDiagramStyle();
        bool m_styleChanged = false;
        ISqlDialect m_dialect;

        public ISqlDialect Dialect
        {
            get { return m_dialect ?? GenericDialect.Instance; }
            set { m_dialect = value; }
        }

        [XmlSubElem]
        public EntityStyle EntityOverride { get; set; }

        public EntityStyle[] GetEntityOverride() { return new EntityStyle[] { EntityOverride }; }
        public void SetEntityOverride(EntityStyle value) { EntityOverride = XmlTool.CloneUsingXml(value); }

        public DiagramStyle Style
        {
            get { return m_style; }
            set
            {
                m_styleChanged = true;
                m_style = value;
            }
        }

        public void Draw(Graphics g)
        {
            bool again = false, wasdraw = false; ;
            do
            {
                again = false;
                using (DiagramPainter dp = new DiagramPainter(this, g))
                {
                    try
                    {
                        if (m_styleChanged) throw new DiagramEntityDrawNeededException();
                        ReferencePainter.DrawReferences(this, g, dp, Style.Reference);
                    }
                    catch (DiagramEntityDrawNeededException)
                    {
                        if (wasdraw) throw;
                        again = true;
                    }

                    Size = new Size(0, 0);
                    int minx = Int32.MaxValue, miny = Int32.MaxValue;
                    foreach (DiagramTableItem item in Tables)
                    {
                        item.Draw(dp);
                        if (item.X + item.Size.Width > Size.Width) Size.Width = item.X + item.Size.Width;
                        if (item.Y + item.Size.Height > Size.Height) Size.Height = item.Y + item.Size.Height;
                        if (item.X < minx) minx = item.X;
                        if (item.Y < miny) miny = item.Y;
                    }
                    if (minx < Int32.MaxValue && miny < Int32.MaxValue && minx > 3 && miny > 3)
                    {
                        Size.Width += minx;
                        Size.Height += miny;
                    }
                    else
                    {
                        Size.Width += 3;
                        Size.Height += 3;
                    }
                }
                m_styleChanged = false;
                wasdraw = true;
            } while (again);
        }

        [XmlIgnore]
        public Size Size;

        public DiagramTableItem FindTable(NameWithSchema name)
        {
            foreach (DiagramTableItem item in Tables)
            {
                if (item.Table.FullName == name) return item;
            }
            return null;
        }

        public DiagramTableItem GetTableAt(int x, int y)
        {
            foreach (DiagramTableItem item in ((IEnumerable<DiagramTableItem>)Tables).Reverse())
            {
                if (item.Contains(x, y)) return item;
            }
            return null;
        }

        public void Save(IVirtualFile file)
        {
            XmlDocument doc = XmlTool.CreateDocument("Diagram");
            foreach (DiagramTableItem item in Tables)
            {
                item.Save(XmlTool.AddChild(doc.DocumentElement, "Table"));
            }
            //XmlElement stx = XmlTool.AddChild(doc.DocumentElement, "Style");
            //ObjectDiff.SaveDiff(Style, new DiagramStyle(), stx);
            Style.SaveToXml(doc.DocumentElement.AddChild("Style"));
            using (StringWriter sw = new StringWriter())
            {
                doc.Save(sw);
                file.SaveText(sw.ToString());
            }
        }

        public static Diagram Load(IVirtualFile file)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(file.GetText());
            Diagram res = new Diagram();
            foreach (XmlElement child in doc.DocumentElement.SelectNodes("Table"))
            {
                res.Tables.Add(new DiagramTableItem(res, child));
            }
            XmlElement stx = (XmlElement)doc.DocumentElement.SelectSingleNode("Style");
            if (stx != null)
            {
                res.m_style = (DiagramStyle)DiagramStyleAddonType.Instance.LoadAddon(stx);
                //ObjectDiff.LoadDiff(res.Style, stx);
            }
            return res;
        }

        public static Diagram CreateNew()
        {
            return new Diagram();
            //{
            //    Style = DiagramStyleAddonType.CreateDatAdmin()
            //};
        }

        public EntityStyle GetCurrentEntityStyle()
        {
            return EntityOverride ?? m_style.Entity;
        }
    }

    public class DiagramPainter : IDisposable
    {
        Dictionary<Color, Brush> brushCache = new Dictionary<Color, Brush>();
        Dictionary<Tuple<Color, int>, Pen> penCache = new Dictionary<Tuple<Color, int>, Pen>();
        internal Diagram m_diagram;
        internal Graphics m_g;

        public DiagramPainter(Diagram diagram, Graphics g)
        {
            m_diagram = diagram;
            m_g = g;
        }

        public Brush GetSolidBrush(Color color)
        {
            if (!brushCache.ContainsKey(color)) brushCache[color] = new SolidBrush(color);
            return brushCache[color];
        }

        public Pen GetPen(Color color, int width)
        {
            var key = new Tuple<Color, int>(color, width);
            if (!penCache.ContainsKey(key)) penCache[key] = new Pen(color, width);
            return penCache[key];
        }

        public void Dispose()
        {
            foreach (var br in brushCache.Values) br.Dispose();
            foreach (var pen in penCache.Values) pen.Dispose();
        }
    }


    public class DiagramTableItem
    {
        public TableStructure Table;
        public int X, Y;
        public bool MustBePlaced = false;
        [XmlIgnore]
        public Size Size;
        [XmlIgnore]
        public Dictionary<int, int> ColumnJoinY = new Dictionary<int, int>();

        public Point Location { get { return new Point(X, Y); } }

        public Interval HorInterv { get { return new Interval(X, X + Size.Width); } }
        public Interval VerInterv { get { return new Interval(Y, Y + Size.Height); } }

        public Point2D TopLeft { get { return new Point2D(X, Y); } }
        public Point2D TopRight { get { return new Point2D(X + Size.Width, Y); } }
        public Point2D BottomLeft { get { return new Point2D(X, Y + Size.Height); } }
        public Point2D BottomRight { get { return new Point2D(X + Size.Width, Y + Size.Height); } }

        [XmlSubElem]
        public EntityStyle EntityOverride { get; set; }

        public EntityStyle GetEntityOverride() { return EntityOverride; }
        public void SetEntityOverride(EntityStyle value) { EntityOverride = XmlTool.CloneUsingXml(value); }

        internal Diagram m_diagram;

        public void Save(XmlElement xml)
        {
            Table.Save(xml);
            xml.SetAttribute("x", X.ToString());
            xml.SetAttribute("y", Y.ToString());
            this.SavePropertiesCore(xml);
        }

        public DiagramTableItem(Diagram diagram)
        {
            m_diagram = diagram;
        }

        public DiagramTableItem(Diagram diagram, XmlElement xml)
        {
            m_diagram = diagram;
            Table = new TableStructure(xml);
            X = Int32.Parse(xml.GetAttribute("x"));
            Y = Int32.Parse(xml.GetAttribute("y"));
            this.LoadPropertiesCore(xml);
        }

        public void DoDraw(DiagramPainter dp)
        {
            ColumnJoinY.Clear();
            var colNameBoxes = new Dictionary<int, Box>();
            var box = dp.m_diagram.Style.GetEntityBox(this, colNameBoxes);
            Size = box.GetOuterSize(dp.m_g);
            box.CallDraw(dp.m_g, new Rectangle(new Point(0, 0), Size));
            foreach (var cbox in colNameBoxes)
            {
                ColumnJoinY.Add(cbox.Key, cbox.Value.DrawBounds.Value.GetMidY());
            }
            //dp.m_diagram.Style.DrawEntity(this, dp);
            //using (EntityPainter painter = new EntityPainter(this, g, dp, style, ColumnJoinY, dialect))
            //{
            //    painter.Run();
            //    Size = painter.Size;
            //}
            //var colwidths = new List<int>(from s in style.Columns select s.MinimalWidth);
            //int hi;

            //MeasureRegion(style.Header, -1, colwidths, g, out hi);



            //int fullwi = colwidths.Sum() + style.EntityFrame.LeftBorder.Width + style.EntityFrame.RightBorder.Width;

            //int acty = style.EntityFrame.TopBorder.Width;

            //RenderRow(style.EntityFrame.LeftBorder.Width, - 1, ref acty, style.Header, colwidths, g);

            //int linehi = (int)g.MeasureString(Table.Name, style.Header.Font.GdiFont).Height;
            //int hi = linehi + 8 + linehi * Table.Columns.Count;
            //int wi = (int)g.MeasureString(Table.Name, style.Header.Font.GdiFont).Width;
            //foreach (IColumnStructure col in Table.Columns)
            //{
            //    wi = Math.Max(wi, (int)g.MeasureString(col.ColumnName, font).Width);
            //}
            //wi += 6;

            //g.FillRectangle(Brushes.LightBlue, 0, 0, wi, linehi + 3);
            //g.DrawRectangle(Pens.Black, 0, 0, wi - 1, hi - 1);
            //g.DrawLine(Pens.Black, 0, linehi + 4, wi, linehi + 4);

            //g.DrawString(Table.Name, font, Brushes.Black, 3, 3);

            //int acty = linehi + 6;
            //string pkcol = TableStructureExtension.PrimaryKeyColumnName(Table);
            //foreach (IColumnStructure col in Table.Columns)
            //{
            //    Brush pencolor = Brushes.Black;
            //    if (col.ColumnName == pkcol) pencolor = Brushes.Red;
            //    g.DrawString(col.ColumnName, font, pencolor, 3, acty);
            //    ColumnJoinY.Add(acty + linehi / 2);
            //    acty += linehi;
            //}
            //Size = new Size(wi, hi);
        }

        public string GetColumnName(int fldindex)
        {
            return Table.Columns[fldindex].ColumnName;
        }
        public string GetFullTypeName(int fldindex)
        {
            return GetFullTypeName(Table.Columns[fldindex]);
        }
        public string GetFullTypeName(IColumnStructure column)
        {
            return m_diagram.Dialect.GenericTypeToSpecific(column.DataType).ToString();
        }

        public EntityStyle GetCurrentEntityStyle()
        {
            return EntityOverride ?? m_diagram.GetCurrentEntityStyle();
        }

        public void Draw(DiagramPainter dp)
        {
            GraphicsState state = dp.m_g.Save();
            try
            {
                dp.m_g.TranslateTransform(X, Y);
                DoDraw(dp);
            }
            finally
            {
                dp.m_g.Restore(state);
            }
        }

        public bool Contains(int x, int y)
        {
            return x >= X && y >= Y && x <= X + Size.Width && y <= Y + Size.Height;
        }

        public int CenterX { get { return X + Size.Width / 2; } }

        public override string ToString()
        {
            return Table.ToString();
        }

        public int Left { get { return X; } }
        public int Top { get { return Y; } }
        public int Right { get { return X + Size.Width; } }
        public int Bottom { get { return Y + Size.Height; } }

        public int GetColumnJoinY(int index)
        {
            if (index >= ColumnJoinY.Count) throw new DiagramEntityDrawNeededException();
            return ColumnJoinY[index];
        }
    }

    public class DiagramEntityDrawNeededException : Exception { }
}
