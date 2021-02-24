using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using DatAdmin;

namespace Plugin.diagrams
{
    public class ReferencePainter
    {
        private static void DrawPointReferences(Diagram diagram, Graphics g, DiagramPainter dp, ReferenceStyle style)
        {
            RectReferencePainter painter = new RectReferencePainter();
            painter.Run(diagram, g, dp, style);
        }

        public static void DrawReferences(Diagram diagram, Graphics g, DiagramPainter dp, ReferenceStyle style)
        {
            var oldmode = g.SmoothingMode;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (style.LineWay == LineWayType.Rectangular || style.LineWay == LineWayType.Direct)
            {
                DrawPointReferences(diagram, g, dp, style);
            }
            else if (style.LineWay == LineWayType.Polygonal)
            {
                foreach (DiagramTableItem fromTable in diagram.Tables)
                {
                    foreach (ForeignKey fk in fromTable.Table.GetConstraints<ForeignKey>())
                    {
                        DiagramTableItem toTable = diagram.FindTable(fk.PrimaryKeyTable);
                        if (toTable != null)
                        {
                            int srccolindex = fromTable.Table.Columns.GetIndex(fk.Columns[0].ColumnName);
                            int dstcolindex = toTable.Table.Columns.GetIndex(fk.PrimaryKeyColumns[0].ColumnName);
                            if (srccolindex < 0 || dstcolindex < 0) continue; // broken foreign key

                            DrawPolygonalFk(g, dp, fromTable, toTable, srccolindex, dstcolindex, style);
                        }
                    }
                }
            }
            g.SmoothingMode = oldmode;
        }

        class _Tmp
        {
            internal int xsrc, dirsrc, xdst, dirdst;
        }

        private static void DrawPolygonalFk(Graphics g, DiagramPainter dp, DiagramTableItem fromTable, DiagramTableItem toTable, int srccolindex, int dstcolindex, ReferenceStyle style)
        {
            Pen pen = dp.GetPen(style.LineColor, style.LineWidth);
            int extwi = style.PolygonalHorDistance;

            var possibilities = new List<_Tmp>();

            possibilities.Add(new _Tmp { xsrc = fromTable.Left, dirsrc = -1, xdst = toTable.Left, dirdst = -1 });
            possibilities.Add(new _Tmp { xsrc = fromTable.Left, dirsrc = -1, xdst = toTable.Right, dirdst = 1 });
            possibilities.Add(new _Tmp { xsrc = fromTable.Right, dirsrc = 1, xdst = toTable.Left, dirdst = -1 });
            possibilities.Add(new _Tmp { xsrc = fromTable.Right, dirsrc = 1, xdst = toTable.Right, dirdst = 1 });

            _Tmp minpos = possibilities.MinKey(p => Math.Abs(p.xsrc - p.xdst));

            Point src = new Point(minpos.xsrc, fromTable.Y + fromTable.GetColumnJoinY(srccolindex));
            Point dst = new Point(minpos.xdst, toTable.Y + toTable.GetColumnJoinY(dstcolindex));
            g.DrawLine(pen, src.X, src.Y, src.X + extwi * minpos.dirsrc, src.Y);
            g.DrawLine(pen, src.X + extwi * minpos.dirsrc, src.Y, dst.X + extwi * minpos.dirdst, dst.Y);
            g.DrawLine(pen, dst.X + extwi * minpos.dirdst, dst.Y, dst.X, dst.Y);

            DrawArrow(dp, g, dst.X + extwi * minpos.dirdst, dst.Y, dst.X, dst.Y, style.Arrow);
        }

        public static void DrawArrow(DiagramPainter dp, Graphics g, int x1, int y1, int x2, int y2, ReferenceStyle.ArrowStyle style)
        {
            Point2D a = new Point2D(x1, y1);
            Point2D b = new Point2D(x2, y2);
            float len = (a - b).Size;
            Point2D e = b + (a - b) * (style.DistanceFromEnd / len); // vrchol sipky
            Point2D f = b + (a - b) * ((style.DistanceFromEnd + style.Length - style.Delta) / len); // konec sipky
            Vector2D n = (b - a).NormalVector.Normalized;
            Point2D p = b + (a - b) * ((style.DistanceFromEnd + style.Length) / len); // pata sipky
            Point2D s = p + n * (style.Width / 2);//jedno kridlo sipky
            Point2D t = p - n * (style.Width / 2);//druhe kridlo sipky
            PointF[] triangle = new PointF[3];
            g.FillPolygon(dp.GetSolidBrush(style.Color), new PointF[] { e, f, s });
            g.FillPolygon(dp.GetSolidBrush(style.Color), new PointF[] { e, f, t });
        }

        public static void DrawArrow(DiagramPainter dp, Graphics g, Point a, Point b, ReferenceStyle.ArrowStyle style)
        {
            DrawArrow(dp, g, a.X, a.Y, b.X, b.Y, style);
        }
    }

    public class RectReferencePainter
    {
        public enum ItemSide { None, Top, Bottom, Left, Right };

        Dictionary<DiagramTableItem, Table> tables = new Dictionary<DiagramTableItem, Table>();
        Dictionary<Tuple<Table, Table>, Reference> refs = new Dictionary<Tuple<Table, Table>, Reference>();

        private void AddReference(DiagramTableItem fromTable, DiagramTableItem toTable, ForeignKey fk, ItemSide src, ItemSide dst)
        {
            Table from = AddTable(fromTable);
            Table to = AddTable(toTable);
            Reference newref = AddReference(fk, from, to);
            if (!newref.IsMultiple)
            {
                from.AddReference(src, newref);
                to.AddReference(dst, newref);
            }
        }

        private Reference AddReference(ForeignKey fk, Table fromTable, Table toTable)
        {
            var key = new Tuple<Table, Table>(fromTable, toTable);
            if (!refs.ContainsKey(key)) refs[key] = new Reference();
            refs[key].Fks.Add(fk);
            refs[key].FromTable = fromTable;
            refs[key].ToTable = toTable;
            return refs[key];
        }

        private Table AddTable(DiagramTableItem table)
        {
            if (!tables.ContainsKey(table)) tables[table] = new Table();
            tables[table].Item = table;
            return tables[table];
        }

        public class Reference
        {
            public Table FromTable, ToTable;
            public Table OtherTable(Table one)
            {
                if (one == FromTable) return ToTable;
                if (one == ToTable) return FromTable;
                return null;
            }
            public Point2D Start, End;
            public Vector2D StartVec, EndVec;
            public List<ForeignKey> Fks = new List<ForeignKey>();

            public bool IsMultiple { get { return Fks.Count > 1; } }

            public void Draw(Graphics g, DiagramPainter dp, ReferenceStyle style)
            {
                Pen pen = dp.GetPen(style.LineColor, style.LineWidth);
                if (style.LineWay == LineWayType.Rectangular)
                {
                    if (Vector2D.AngleBetween(StartVec, EndVec) < 0.1) // asi to bude rovnobezne
                    {
                        /*
                         Start          beglev
                         
                         m1             m2
                         
                         endlev         End
                         */
                        Point2D beglev = new Line2D(Start, StartVec.NormalVector).ComPoint(new Line2D(End, EndVec)) ?? new Point2D();
                        Point2D endlev = new Line2D(End, EndVec.NormalVector).ComPoint(new Line2D(Start, StartVec)) ?? new Point2D();
                        Point2D m1 = Start + (endlev - Start) / 2;
                        Point2D m2 = beglev + (End - beglev) / 2;
                        g.DrawLine(pen, (Point)Start, (Point)m1);
                        g.DrawLine(pen, (Point)m1, (Point)m2);
                        g.DrawLine(pen, (Point)m2, (Point)End);
                        ReferencePainter.DrawArrow(dp, g, (Point)m2, (Point)End, style.Arrow);
                    }
                    else
                    {
                        /*
                        Start


                        m         End
                        */
                        Point2D m = new Line2D(Start, StartVec).ComPoint(new Line2D(End, EndVec)) ?? new Point2D();
                        g.DrawLine(pen, (Point)Start, (Point)m);
                        g.DrawLine(pen, (Point)m, (Point)End);
                        ReferencePainter.DrawArrow(dp, g, (Point)m, (Point)End, style.Arrow);
                    }
                }
                if (style.LineWay == LineWayType.Direct)
                {
                    g.DrawLine(pen, Start, End);
                    ReferencePainter.DrawArrow(dp, g, (Point)Start, (Point)End, style.Arrow);
                }
            }

            public override string ToString()
            {
                return String.Format("{0}->{1}", FromTable, ToTable);
            }
        }

        public class Table
        {
            public List<Reference> Top = new List<Reference>();
            public List<Reference> Bottom = new List<Reference>();
            public List<Reference> Left = new List<Reference>();
            public List<Reference> Right = new List<Reference>();
            public DiagramTableItem Item;

            public void SetReferencePositions()
            {
                Bottom.SortByKey<Reference, int>(r => r.OtherTable(this).Item.X);
                Top.SortByKey<Reference, int>(r => r.OtherTable(this).Item.X);
                Left.SortByKey<Reference, int>(r => r.OtherTable(this).Item.Y);
                Right.SortByKey<Reference, int>(r => r.OtherTable(this).Item.Y);

                ProcessReferences(Top, Item.TopLeft, Item.TopRight, new Vector2D(0, -1));
                ProcessReferences(Left, Item.TopLeft, Item.BottomLeft, new Vector2D(-1, 0));
                ProcessReferences(Bottom, Item.BottomLeft, Item.BottomRight, new Vector2D(0, 1));
                ProcessReferences(Right, Item.TopRight, Item.BottomRight, new Vector2D(1, 0));
            }

            private void ProcessReferences(List<Reference> refs, Point2D intbeg, Point2D intend, Vector2D linedir)
            {
                if (refs.Count == 0) return;
                float dt = 1.0f / (refs.Count + 1), t = dt;
                foreach (Reference r in refs)
                {
                    Point2D pt = intbeg + (intend - intbeg) * t;
                    if (r.FromTable == this)
                    {
                        r.Start = pt;
                        r.StartVec = linedir;
                    }
                    if (r.ToTable == this)
                    {
                        r.End = pt;
                        r.EndVec = linedir;
                    }
                    t += dt;
                }
            }

            public void AddReference(ItemSide src, Reference newref)
            {
                switch (src)
                {
                    case ItemSide.Bottom:
                        Bottom.Add(newref);
                        break;
                    case ItemSide.Left:
                        Left.Add(newref);
                        break;
                    case ItemSide.Right:
                        Right.Add(newref);
                        break;
                    case ItemSide.Top:
                        Top.Add(newref);
                        break;
                }
            }

            public override string ToString()
            {
                return Item.ToString();
            }
        }

        public void Run(Diagram diagram, Graphics g, DiagramPainter dp, ReferenceStyle style)
        {
            FillFromDiagram(diagram);
            foreach (Table tbl in tables.Values)
            {
                tbl.SetReferencePositions();
            }
            foreach (Reference r in refs.Values)
            {
                r.Draw(g, dp, style);
            }
        }

        void FillFromDiagram(Diagram diagram)
        {
            // nejdrive vybereme zdrojovou a cilovou stranu
            foreach (DiagramTableItem fromTable in diagram.Tables)
            {
                foreach (ForeignKey fk in TableStructureExtension.GetConstraints<ForeignKey, ForeignKey>(fromTable.Table))
                {
                    DiagramTableItem toTable = diagram.FindTable(fk.PrimaryKeyTable);
                    if (toTable != null)
                    {
                        Interval fromx = fromTable.HorInterv, tox = toTable.HorInterv;
                        Interval fromy = fromTable.VerInterv, toy = toTable.VerInterv;
                        ItemSide src = ItemSide.None, dst = ItemSide.None;
                        if (fromx.Intersection(tox).Size > 0)
                        {
                            if (fromy <= toy)
                            {
                                src = ItemSide.Bottom;
                                dst = ItemSide.Top;
                            }
                            if (toy <= fromy)
                            {
                                src = ItemSide.Top;
                                dst = ItemSide.Bottom;
                            }
                            // prekryv pres oba rozmery - caru nebudeme kreslit
                        }
                        else if (fromy.Intersection(toy).Size > 0)
                        {
                            if (fromx <= tox)
                            {
                                src = ItemSide.Right;
                                dst = ItemSide.Left;
                            }
                            if (tox <= fromx)
                            {
                                src = ItemSide.Left;
                                dst = ItemSide.Right;
                            }
                        }
                        else //neni prekryv ani v jedne souradnici
                        {
                            if (fromx <= tox && fromy <= toy)
                            {
                                src = ItemSide.Right;
                                dst = ItemSide.Top;
                            }
                            if (fromx >= tox && fromy <= toy)
                            {
                                src = ItemSide.Left;
                                dst = ItemSide.Top;
                            }
                            if (fromx <= tox && fromy >= toy)
                            {
                                src = ItemSide.Right;
                                dst = ItemSide.Bottom;
                            }
                            if (fromx >= tox && fromy >= toy)
                            {
                                src = ItemSide.Left;
                                dst = ItemSide.Bottom;
                            }
                        }
                        AddReference(fromTable, toTable, fk, src, dst);
                    }
                }
            }
        }
    }
}
