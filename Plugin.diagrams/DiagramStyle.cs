using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Linq;
using System.Xml.Serialization;
using System.Xml;
using DatAdmin;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Plugin.diagrams
{
    public class EntityStyle
    {
        [XmlSubElem]
        public GradientDef BodyBg { get; set; }
        [XmlElem]
        public bool IsDefinedHeader { get; set; }
        [XmlSubElem]
        public GradientDef HeaderBg { get; set; }
        [XmlElem]
        public string StyleName { get; set; }

        public EntityStyle Clone()
        {
            return XmlTool.CloneUsingXml(this);
        }
    }

    public class PlacementStyle
    {
        int m_minHorizontalDistance = 20;

        public int MinHorizontalDistance
        {
            get { return m_minHorizontalDistance; }
            set { m_minHorizontalDistance = value; }
        }
        int m_minVerticalDistance = 20;

        public int MinVerticalDistance
        {
            get { return m_minVerticalDistance; }
            set { m_minVerticalDistance = value; }
        }

        int m_maxDiagramWidth = -1;

        public int MaxDiagramWidth
        {
            get { return m_maxDiagramWidth; }
            set { m_maxDiagramWidth = value; }
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ReferenceStyle
    {
        int m_lineWidth = 2;

        public int LineWidth
        {
            get { return m_lineWidth; }
            set { m_lineWidth = value; }
        }
        Color m_lineColor = Color.Black;

        public Color LineColor
        {
            get { return m_lineColor; }
            set { m_lineColor = value; }
        }

        int polygonalHorDistance = 30;

        public int PolygonalHorDistance
        {
            get { return polygonalHorDistance; }
            set { polygonalHorDistance = value; }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class ArrowStyle
        {
            int m_width = 15;

            public int Width
            {
                get { return m_width; }
                set { m_width = value; }
            }
            Color m_color = Color.Black;

            public Color Color
            {
                get { return m_color; }
                set { m_color = value; }
            }
            int m_length = 20;

            public int Length
            {
                get { return m_length; }
                set { m_length = value; }
            }
            int m_delta = 3;

            public int Delta
            {
                get { return m_delta; }
                set { m_delta = value; }
            }

            int m_distanceFromEnd = 3;

            public int DistanceFromEnd
            {
                get { return m_distanceFromEnd; }
                set { m_distanceFromEnd = value; }
            }
        }

        LineWayType m_lineWay = LineWayType.Polygonal;

        public LineWayType LineWay
        {
            get { return m_lineWay; }
            set { m_lineWay = value; }
        }

        ArrowStyle m_arrow = new ArrowStyle();

        public ArrowStyle Arrow
        {
            get { return m_arrow; }
            set { m_arrow = value; }
        }

        public override string ToString()
        {
            return m_lineWidth.ToString() + ";" + m_lineColor.ToString();
        }
    }

    public enum LineWayType { Direct, Rectangular, Polygonal };

    public abstract class DiagramStyle : AddonBase
    {
        public PlacementStyle Placement = new PlacementStyle();
        public ReferenceStyle Reference = new ReferenceStyle();
        public EntityStyle Entity = new EntityStyle();

        public event EventHandler Changed;

        public virtual Box GetEntityBox(DiagramTableItem item, Dictionary<int, Box> colNameBoxes)
        {
            var panel = new PanelBox { Orientation = Orientation.Vertical };
            panel.BorderAllBrush = Brushes.Black;
            var header = new StringBox { Text = item.Table.Name, Font = GetHeaderFont(), Brush = Brushes.Black };
            panel.Boxes.Add(header);
            var table = new TableBox();
            table.HAlign = BoxModelAlignement.Fill;
            FillColumnsTable(item, colNameBoxes, table);
            panel.Boxes.Add(table);
            header.BorderBottom = 1;
            header.PadLeft = header.PadRight = 3;
            FillHeader(header);
            panel.BorderAll = 1;
            panel.BorderAllBrush = Brushes.Black;
            panel.BackgroundBrush = Brushes.White;
            var es = item.GetCurrentEntityStyle();
            if (es.IsDefinedHeader)
            {
                header.Gradient = es.HeaderBg;
                table.Gradient = es.BodyBg;
            }
            else
            {
                panel.Gradient = es.BodyBg;
            }

            return panel;
        }

        protected virtual Font GetHeaderFont() { return SystemFonts.DefaultFont; }
        protected virtual void FillColumnsTable(DiagramTableItem item, Dictionary<int, Box> colNameBoxes, TableBox table) { }
        protected virtual void FillHeader(StringBox header) { }

        protected void CallChanged()
        {
            if (Changed != null) Changed(this, EventArgs.Empty);
        }

        protected Dictionary<string, int> GetFkColIndexes(ITableStructure table)
        {
            var res = new Dictionary<string, int>();
            int fkindex = 0;
            foreach (IConstraint cnt in table.Constraints)
            {
                if (cnt is IForeignKey)
                {
                    foreach (var col in ((IForeignKey)cnt).Columns)
                    {
                        res[col.ColumnName] = fkindex;
                    }
                    fkindex++;
                }
            }
            return res;
        }

        [Browsable(false)]
        public override AddonType AddonType
        {
            get { return DiagramStyleAddonType.Instance; }
        }

        [XmlElem]
        public LineWayType LineWay
        {
            get { return Reference.LineWay; }
            set
            {
                Reference.LineWay = value;
                CallChanged();
            }
        }
    }

    [DiagramStyle(Name = "datadmin", Title = "DatAdmin")]
    public class DatAdminDiagramStyle : DiagramStyle
    {
        public Font ColumnBold = SystemFonts.DefaultFont;
        public Font ColumnRegular = SystemFonts.DefaultFont;
        public Font HeaderFont = SystemFonts.DefaultFont;

        public DatAdminDiagramStyle()
        {
            ColumnRegular = new Font(FontFamily.GenericSansSerif, 9);
            ColumnBold = new Font(ColumnRegular, FontStyle.Bold);
            HeaderFont = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
            Entity.IsDefinedHeader = true;
            Entity.HeaderBg = GradientDef.Gradient(Color.PaleTurquoise, Color.White);
            Entity.BodyBg = GradientDef.Solid(Color.White);
        }

        protected override void FillHeader(StringBox header)
        {
            header.BorderBottom = 2;
            header.HAlign = BoxModelAlignement.Center;
            header.BorderBottomBrush = Brushes.Gray;
        }

        protected override Font GetHeaderFont()
        {
            return HeaderFont;
        }

        protected override void FillColumnsTable(DiagramTableItem item, Dictionary<int, Box> colNameBoxes, TableBox table)
        {
            var fkColsIndexes = GetFkColIndexes(item.Table);
            int fldindex = 0;
            foreach (var col in item.Table.Columns)
            {
                var row = table.AddRow();
                if (item.Table.GetKeyWithColumn<IPrimaryKey>(col) != null)
                {
                    row.AddCell(CoreIcons.primary_key);
                }
                else if (fkColsIndexes.ContainsKey(col.ColumnName))
                {
                    row.AddCell(CoreIcons.foreign_key);
                }
                else
                {
                    row.AddCell(new FixedBox { FixedSize = new Size(16, 16) });
                }
                var cname = row.AddCell(col.ColumnName, col.IsNullable ? ColumnRegular : ColumnBold, Brushes.Black);
                if (colNameBoxes != null) colNameBoxes[fldindex] = cname;
                row.AddCell(item.GetFullTypeName(col), ColumnRegular, Brushes.Black);
                fldindex++;
            }
        }
    }

    [DiagramStyle(Name = "visio", Title = "Visio")]
    public class VisioDiagramStyle : DiagramStyle
    {
        public Font ColumnBold = SystemFonts.DefaultFont;
        //public Font ColumnBoldUnderline = SystemFonts.DefaultFont;
        public Font ColumnRegular = SystemFonts.DefaultFont;
        public Font HeaderFont = SystemFonts.DefaultFont;

        public VisioDiagramStyle()
        {
            ColumnRegular = new Font(FontFamily.GenericSansSerif, 9);
            ColumnBold = new Font(ColumnRegular, FontStyle.Bold);
            //ColumnBoldUnderline = new Font(ColumnRegular, FontStyle.Bold | FontStyle.Underline);
            HeaderFont = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
            Entity.IsDefinedHeader = true;
            Entity.HeaderBg = GradientDef.Solid(Color.LightGray);
            Entity.BodyBg = GradientDef.Solid(Color.White);

            var rs = Reference;
            rs.LineWay = LineWayType.Rectangular;
            var ar = rs.Arrow;
            ar.Width = 18;
            ar.Length = 15;
            ar.Delta = 0;
            ar.DistanceFromEnd = 0;
        }

        protected override void FillColumnsTable(DiagramTableItem item, Dictionary<int, Box> colNameBoxes, TableBox table)
        {
            var fkColsIndexes = GetFkColIndexes(item.Table);

            IPrimaryKey pk = item.Table.FindConstraint<IPrimaryKey>();
            int fldindex = 0;

            table.ColumnDelimiter = new FixedBox { BackgroundBrush = Brushes.Black, FixedSize = new Size(1, 1) };

            if (pk != null)
            {
                foreach (IColumnStructure col in item.Table.Columns)
                {
                    if (pk.Columns.IndexOfIf(cr => cr.ColumnName == col.ColumnName) >= 0)
                    {
                        var row = table.AddRow();
                        row.AddCell("PK", ColumnBold, Brushes.Black);
                        var cname = row.AddCell(col.ColumnName, col.IsNullable ? ColumnRegular : ColumnBold, Brushes.Black);
                        if (colNameBoxes != null) colNameBoxes[fldindex] = cname;
                    }
                    fldindex++;
                }
                table.RowDelimiterOverrides[pk.Columns.Count] = new FixedBox { BackgroundBrush = Brushes.Black, FixedSize = new Size(1, 1), MarginTop = 2, MarginBottom = 2 };
            }

            foreach (IColumnStructure col in item.Table.Columns)
            {
                if (pk == null || pk.Columns.IndexOfIf(cr => cr.ColumnName == col.ColumnName) < 0)
                {
                    var row = table.AddRow();
                    if (fkColsIndexes.ContainsKey(col.ColumnName))
                    {
                        row.AddCell("FK" + (fkColsIndexes[col.ColumnName] + 1).ToString(), ColumnRegular, Brushes.Black);
                    }
                    else
                    {
                        row.AddCell(new FixedBox { FixedSize = new Size(16, 16) });
                    }
                    var cname = row.AddCell(col.ColumnName, col.IsNullable ? ColumnRegular : ColumnBold, Brushes.Black);
                    if (colNameBoxes != null) colNameBoxes[fldindex] = cname;
                }
                fldindex++;
            }
        }

        protected override void FillHeader(StringBox header)
        {
            header.PadTop = header.PadBottom = 3;
        }
    }


    //public class DiagramStyle : AddonBase, IXmlSerializable
    //{
    //    EntityStyle m_defaulEntity = new EntityStyle();
    //    public EntityStyle DefaulEntity
    //    {
    //        get { return m_defaulEntity; }
    //        set { m_defaulEntity = value; }
    //    }

    //    ReferenceStyle m_defaultReference = new ReferenceStyle();
    //    public ReferenceStyle DefaultReference
    //    {
    //        get { return m_defaultReference; }
    //        set { m_defaultReference = value; }
    //    }

    //    public override string ToString()
    //    {
    //        return "Diagram";
    //    }

    //    [TypeConverter(typeof(ExpandableObjectConverter))]
    //    public class EntityStyle
    //    {
    //        public enum ColumnData
    //        {
    //            None, TableName, ColumnName, TypeName, TypeSize, FullTypeName, FkIndex
    //        }

    //        [TypeConverter(typeof(ExpandableObjectConverter))]
    //        public class ColumnStyle
    //        {
    //            int m_minimalWidth = 0;

    //            public int MinimalWidth
    //            {
    //                get { return m_minimalWidth; }
    //                set { m_minimalWidth = value; }
    //            }

    //            public override string ToString()
    //            {
    //                return m_minimalWidth.ToString();
    //            }
    //        }

    //        [TypeConverter(typeof(ExpandableObjectConverter))]
    //        public class CellStyle
    //        {
    //            ColumnData m_data = ColumnData.None;

    //            public ColumnData Data
    //            {
    //                get { return m_data; }
    //                set { m_data = value; }
    //            }
    //            string m_dataPrefix = "";

    //            public string DataPrefix
    //            {
    //                get { return m_dataPrefix; }
    //                set { m_dataPrefix = value; }
    //            }
    //            string m_dataPostfix = "";

    //            public string DataPostfix
    //            {
    //                get { return m_dataPostfix; }
    //                set { m_dataPostfix = value; }
    //            }
    //            PersistentImage m_image = new PersistentImage();

    //            public PersistentImage Image
    //            {
    //                get { return m_image; }
    //                set { m_image = value; }
    //            }
    //            int m_colspan = 1;

    //            public int Colspan
    //            {
    //                get { return m_colspan; }
    //                set { m_colspan = value; }
    //            }

    //            BorderStyle m_leftBorder = new BorderStyle();

    //            public BorderStyle LeftBorder
    //            {
    //                get { return m_leftBorder; }
    //                set { m_leftBorder = value; }
    //            }
    //            BorderStyle m_rightBorder = new BorderStyle();

    //            public BorderStyle RightBorder
    //            {
    //                get { return m_rightBorder; }
    //                set { m_rightBorder = value; }
    //            }

    //            int m_leftPadding = 0;

    //            public int LeftPadding
    //            {
    //                get { return m_leftPadding; }
    //                set { m_leftPadding = value; }
    //            }
    //            int m_rightPadding = 0;

    //            public int RightPadding
    //            {
    //                get { return m_rightPadding; }
    //                set { m_rightPadding = value; }
    //            }

    //            int m_topPadding = 0;

    //            public int TopPadding
    //            {
    //                get { return m_topPadding; }
    //                set { m_topPadding = value; }
    //            }
    //            int m_bottomPadding = 0;

    //            public int BottomPadding
    //            {
    //                get { return m_bottomPadding; }
    //                set { m_bottomPadding = value; }
    //            }

    //            public override string ToString()
    //            {
    //                return Data.ToString();
    //            }

    //            PersistentFont m_font = new PersistentFont();

    //            public PersistentFont Font
    //            {
    //                get { return m_font; }
    //                set { m_font = value; }
    //            }

    //            ContentAlignment m_textAlign;

    //            public ContentAlignment TextAlign
    //            {
    //                get { return m_textAlign; }
    //                set { m_textAlign = value; }
    //            }

    //            public int GetHorizontalPadding()
    //            {
    //                int res = 0;
    //                res += LeftBorder.Width + RightBorder.Width;
    //                res += LeftPadding + RightPadding;
    //                return res;
    //            }
    //            public int GetVerticalPadding()
    //            {
    //                int res = 0;
    //                res += TopPadding + BottomPadding;
    //                return res;
    //            }
    //        }

    //        [TypeConverter(typeof(ExpandableObjectConverter))]
    //        public class BorderStyle
    //        {
    //            Color m_color = Color.Black;

    //            public Color Color
    //            {
    //                get { return m_color; }
    //                set { m_color = value; }
    //            }
    //            int m_width = 0;

    //            public int Width
    //            {
    //                get { return m_width; }
    //                set { m_width = value; }
    //            }

    //            public override string ToString()
    //            {
    //                return Width.ToString() + ";" + Color.ToString();
    //            }

    //            public BorderStyle Clone()
    //            {
    //                return (BorderStyle)MemberwiseClone();
    //            }
    //        }

    //        [TypeConverter(typeof(ExpandableObjectConverter))]
    //        public class RowFrameStyle
    //        {
    //            Color m_bgColor = Color.White;

    //            public Color BgColor
    //            {
    //                get { return m_bgColor; }
    //                set { m_bgColor = value; }
    //            }
    //            bool m_visible = true;

    //            public bool Visible
    //            {
    //                get { return m_visible; }
    //                set { m_visible = value; }
    //            }
    //            BorderStyle m_topBorder = new BorderStyle();

    //            public BorderStyle TopBorder
    //            {
    //                get { return m_topBorder; }
    //                set { m_topBorder = value; }
    //            }
    //            BorderStyle m_bottomBorder = new BorderStyle();

    //            public BorderStyle BottomBorder
    //            {
    //                get { return m_bottomBorder; }
    //                set { m_bottomBorder = value; }
    //            }

    //            int m_paddingTop = 0;

    //            public int PaddingTop
    //            {
    //                get { return m_paddingTop; }
    //                set { m_paddingTop = value; }
    //            }

    //            int m_paddingBottom = 0;

    //            public int PaddingBottom
    //            {
    //                get { return m_paddingBottom; }
    //                set { m_paddingBottom = value; }
    //            }
    //        }

    //        [TypeConverter(typeof(ExpandableObjectConverter))]
    //        public class FrameStyle : RowFrameStyle
    //        {
    //            BorderStyle m_leftBorder = new BorderStyle();

    //            public BorderStyle LeftBorder
    //            {
    //                get { return m_leftBorder; }
    //                set { m_leftBorder = value; }
    //            }
    //            BorderStyle m_rightBorder = new BorderStyle();

    //            public BorderStyle RightBorder
    //            {
    //                get { return m_rightBorder; }
    //                set { m_rightBorder = value; }
    //            }

    //            public override string ToString()
    //            {
    //                return "Container";
    //            }

    //            public BorderStyle AllBorders
    //            {
    //                set
    //                {
    //                    LeftBorder = value.Clone();
    //                    RightBorder = value.Clone();
    //                    TopBorder = value.Clone();
    //                    BottomBorder = value.Clone();
    //                }
    //            }
    //        }

    //        [TypeConverter(typeof(ExpandableObjectConverter))]
    //        public class RowStyle : RowFrameStyle
    //        {
    //            CellStyle[] m_cells = new CellStyle[] {
    //                new CellStyle(),
    //                new CellStyle(),
    //                new CellStyle(),
    //                new CellStyle(),
    //                new CellStyle(),
    //            };

    //            public RowStyle() { }
    //            public static RowStyle SingleColumn(ColumnData data)
    //            {
    //                var res = new RowStyle();
    //                res.Cells[0].Data = data;
    //                return res;
    //            }

    //            public CellStyle[] Cells
    //            {
    //                get { return m_cells; }
    //                set { m_cells = value; }
    //            }

    //            public override string ToString()
    //            {
    //                return String.Format("Row: {0} cells", (from c in Cells where c.Data != ColumnData.None select c).Count());
    //            }
    //        }

    //        public enum ColumnStyleType { PrimaryKey, Null, NotNull, FkNull, FkNotNull };

    //        [TypeConverter(typeof(ExpandableObjectConverter))]
    //        public class RowStyleSetStyle
    //        {
    //            RowStyle m_pkRow = RowStyle.SingleColumn(ColumnData.ColumnName);

    //            public RowStyle PrimaryKey
    //            {
    //                get { return m_pkRow; }
    //                set { m_pkRow = value; }
    //            }

    //            RowStyle m_nullableStyle = RowStyle.SingleColumn(ColumnData.ColumnName);

    //            public RowStyle Null
    //            {
    //                get { return m_nullableStyle; }
    //                set { m_nullableStyle = value; }
    //            }
    //            RowStyle m_notNullStyle = RowStyle.SingleColumn(ColumnData.ColumnName);

    //            public RowStyle NotNull
    //            {
    //                get { return m_notNullStyle; }
    //                set { m_notNullStyle = value; }
    //            }

    //            RowStyle m_fkNull = RowStyle.SingleColumn(ColumnData.ColumnName);

    //            public RowStyle FkNull
    //            {
    //                get { return m_fkNull; }
    //                set { m_fkNull = value; }
    //            }
    //            RowStyle m_fkNotNull = RowStyle.SingleColumn(ColumnData.ColumnName);

    //            public RowStyle FkNotNull
    //            {
    //                get { return m_fkNotNull; }
    //                set { m_fkNotNull = value; }
    //            }

    //            [TypeConverter(typeof(ExpandableObjectConverter))]
    //            public class StyleMapping
    //            {
    //                ColumnStyleType m_primaryKey = ColumnStyleType.PrimaryKey;

    //                public ColumnStyleType PrimaryKey
    //                {
    //                    get { return m_primaryKey; }
    //                    set { m_primaryKey = value; }
    //                }
    //                ColumnStyleType m_null = ColumnStyleType.Null;

    //                public ColumnStyleType Null
    //                {
    //                    get { return m_null; }
    //                    set { m_null = value; }
    //                }
    //                ColumnStyleType m_notNull = ColumnStyleType.NotNull;

    //                public ColumnStyleType NotNull
    //                {
    //                    get { return m_notNull; }
    //                    set { m_notNull = value; }
    //                }
    //                ColumnStyleType m_fkNull = ColumnStyleType.FkNull;

    //                public ColumnStyleType FkNull
    //                {
    //                    get { return m_fkNull; }
    //                    set { m_fkNull = value; }
    //                }
    //                ColumnStyleType m_fkNotNull = ColumnStyleType.FkNotNull;

    //                public ColumnStyleType FkNotNull
    //                {
    //                    get { return m_fkNotNull; }
    //                    set { m_fkNotNull = value; }
    //                }

    //                internal ColumnStyleType MapType(ColumnStyleType type)
    //                {
    //                    switch (type)
    //                    {
    //                        case ColumnStyleType.FkNotNull: return FkNotNull;
    //                        case ColumnStyleType.FkNull: return FkNull;
    //                        case ColumnStyleType.NotNull: return NotNull;
    //                        case ColumnStyleType.Null: return Null;
    //                        case ColumnStyleType.PrimaryKey: return PrimaryKey;
    //                    }
    //                    throw new Exception("internal error");
    //                }

    //                public override string ToString()
    //                {
    //                    return "Mapping";
    //                }
    //            }
    //            private void ForEachRowStyle(Action<RowStyle> action)
    //            {
    //                action(this.FkNotNull);
    //                action(this.FkNull);
    //                action(this.NotNull);
    //                action(this.Null);
    //                action(this.PrimaryKey);
    //            }

    //            [XmlIgnore]
    //            public int BatchSet_Col1BorderWidth
    //            {
    //                get { return PrimaryKey.Cells[1].LeftBorder.Width; }
    //                set { ForEachRowStyle(row => { row.Cells[1].LeftBorder.Width = value; }); }
    //            }
    //            [XmlIgnore]
    //            public int BatchSet_Col2BorderWidth
    //            {
    //                get { return PrimaryKey.Cells[2].LeftBorder.Width; }
    //                set { ForEachRowStyle(row => { row.Cells[2].LeftBorder.Width = value; }); }
    //            }
    //            [XmlIgnore]
    //            public ColumnData BatchSet_Col0Data
    //            {
    //                get { return PrimaryKey.Cells[0].Data; }
    //                set { ForEachRowStyle(row => { row.Cells[0].Data = value; }); }
    //            }
    //            [XmlIgnore]
    //            public ColumnData BatchSet_Col1Data
    //            {
    //                get { return PrimaryKey.Cells[1].Data; }
    //                set { ForEachRowStyle(row => { row.Cells[1].Data = value; }); }
    //            }
    //            [XmlIgnore]
    //            public ColumnData BatchSet_Col2Data
    //            {
    //                get { return PrimaryKey.Cells[2].Data; }
    //                set { ForEachRowStyle(row => { row.Cells[2].Data = value; }); }
    //            }

    //            StyleMapping m_mapping = new StyleMapping();

    //            public StyleMapping Mapping
    //            {
    //                get { return m_mapping; }
    //                set { m_mapping = value; }
    //            }

    //            public RowStyle this[ColumnStyleType type]
    //            {
    //                get
    //                {
    //                    switch (Mapping.MapType(type))
    //                    {
    //                        case ColumnStyleType.FkNotNull: return FkNotNull;
    //                        case ColumnStyleType.FkNull: return FkNull;
    //                        case ColumnStyleType.NotNull: return NotNull;
    //                        case ColumnStyleType.Null: return Null;
    //                        case ColumnStyleType.PrimaryKey: return PrimaryKey;
    //                    }
    //                    throw new Exception("internal error");
    //                }
    //            }
    //        }

    //        RowStyleSetStyle m_rowStyleSet = new RowStyleSetStyle();

    //        public RowStyleSetStyle RowStyleSet
    //        {
    //            get { return m_rowStyleSet; }
    //            set { m_rowStyleSet = value; }
    //        }

    //        RowStyle m_headerStyle = RowStyle.SingleColumn(ColumnData.TableName);

    //        public RowStyle Header
    //        {
    //            get { return m_headerStyle; }
    //            set { m_headerStyle = value; }
    //        }
    //        RowFrameStyle m_pkColsFrame = new RowFrameStyle();

    //        public RowFrameStyle PkColsFrame
    //        {
    //            get { return m_pkColsFrame; }
    //            set { m_pkColsFrame = value; }
    //        }

    //        RowFrameStyle m_noPkColsFrame = new RowFrameStyle();

    //        public RowFrameStyle NoPkColsFrame
    //        {
    //            get { return m_noPkColsFrame; }
    //            set { m_noPkColsFrame = value; }
    //        }

    //        RowFrameStyle m_columnsStyle = new RowFrameStyle();

    //        public RowFrameStyle ColumnsFrame
    //        {
    //            get { return m_columnsStyle; }
    //            set { m_columnsStyle = value; }
    //        }
    //        FrameStyle m_entityFrame = new FrameStyle
    //        {
    //            AllBorders = new BorderStyle { Width = 1 }
    //        };

    //        public FrameStyle EntityFrame
    //        {
    //            get { return m_entityFrame; }
    //            set { m_entityFrame = value; }
    //        }

    //        ColumnStyle[] m_columns = new ColumnStyle[]
    //        {
    //            new ColumnStyle(),
    //            new ColumnStyle(),
    //            new ColumnStyle(),
    //            new ColumnStyle(),
    //            new ColumnStyle(),
    //        };
    //        [Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
    //        public ColumnStyle[] Columns
    //        {
    //            get { return m_columns; }
    //            set { m_columns = value; }
    //        }

    //        public override string ToString()
    //        {
    //            return "Entity";
    //        }
    //    }

    //    [TypeConverter(typeof(ExpandableObjectConverter))]
    //    public class ReferenceStyle
    //    {
    //        int m_lineWidth = 2;

    //        public int LineWidth
    //        {
    //            get { return m_lineWidth; }
    //            set { m_lineWidth = value; }
    //        }
    //        Color m_lineColor = Color.Black;

    //        public Color LineColor
    //        {
    //            get { return m_lineColor; }
    //            set { m_lineColor = value; }
    //        }

    //        int polygonalHorDistance = 30;

    //        public int PolygonalHorDistance
    //        {
    //            get { return polygonalHorDistance; }
    //            set { polygonalHorDistance = value; }
    //        }

    //        public enum LineWayType { Direct, Rectangular, Polygonal };

    //        [TypeConverter(typeof(ExpandableObjectConverter))]
    //        public class ArrowStyle
    //        {
    //            int m_width = 15;

    //            public int Width
    //            {
    //                get { return m_width; }
    //                set { m_width = value; }
    //            }
    //            Color m_color = Color.Black;

    //            public Color Color
    //            {
    //                get { return m_color; }
    //                set { m_color = value; }
    //            }
    //            int m_length = 20;

    //            public int Length
    //            {
    //                get { return m_length; }
    //                set { m_length = value; }
    //            }
    //            int m_delta = 3;

    //            public int Delta
    //            {
    //                get { return m_delta; }
    //                set { m_delta = value; }
    //            }

    //            int m_distanceFromEnd = 3;

    //            public int DistanceFromEnd
    //            {
    //                get { return m_distanceFromEnd; }
    //                set { m_distanceFromEnd = value; }
    //            }
    //        }

    //        LineWayType m_lineWay = LineWayType.Polygonal;

    //        public LineWayType LineWay
    //        {
    //            get { return m_lineWay; }
    //            set { m_lineWay = value; }
    //        }

    //        ArrowStyle m_arrow = new ArrowStyle();

    //        public ArrowStyle Arrow
    //        {
    //            get { return m_arrow; }
    //            set { m_arrow = value; }
    //        }

    //        public override string ToString()
    //        {
    //            return m_lineWidth.ToString() + ";" + m_lineColor.ToString();
    //        }
    //    }

    //    [TypeConverter(typeof(ExpandableObjectConverter))]
    //    public class PlacementStyle
    //    {
    //        int m_minHorizontalDistance = 20;

    //        public int MinHorizontalDistance
    //        {
    //            get { return m_minHorizontalDistance; }
    //            set { m_minHorizontalDistance = value; }
    //        }
    //        int m_minVerticalDistance = 20;

    //        public int MinVerticalDistance
    //        {
    //            get { return m_minVerticalDistance; }
    //            set { m_minVerticalDistance = value; }
    //        }

    //        int m_maxDiagramWidth = -1;

    //        public int MaxDiagramWidth
    //        {
    //            get { return m_maxDiagramWidth; }
    //            set { m_maxDiagramWidth = value; }
    //        }
    //    }

    //    PlacementStyle m_placement = new PlacementStyle();

    //    public PlacementStyle Placement
    //    {
    //        get { return m_placement; }
    //        set { m_placement = value; }
    //    }

    //    public void Save(IVirtualFile file)
    //    {
    //        XmlTool.SerializeDiff(file, this);
    //    }

    //    public static DiagramStyle Load(IVirtualFile file)
    //    {
    //        return XmlTool.DeserializeDiff<DiagramStyle>(file);
    //    }

    //    [Browsable(false)]
    //    public override AddonType AddonType
    //    {
    //        get { return DiagramStyleAddonType.Instance; }
    //    }

    //    #region IXmlSerializable Members

    //    public System.Xml.Schema.XmlSchema GetSchema()
    //    {
    //        return null;
    //    }

    //    public void ReadXml(System.Xml.XmlReader reader)
    //    {
    //        XmlDocument doc = new XmlDocument();
    //        doc.Load(reader);
    //        ObjectDiff.LoadDiff(this, doc.DocumentElement);
    //    }

    //    public void WriteXml(System.Xml.XmlWriter writer)
    //    {
    //        XmlDocument doc = XmlTool.CreateDocument("DiagramStyle");
    //        ObjectDiff.SaveDiff(this, new DiagramStyle(), doc.DocumentElement);
    //        doc.DocumentElement.WriteContentTo(writer);
    //    }

    //    #endregion
    //}
}
