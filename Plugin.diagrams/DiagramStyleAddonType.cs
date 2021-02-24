using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DatAdmin;

namespace Plugin.diagrams
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DiagramStyleAttribute : RegisterAttribute
    {
    }

    //public interface IDiagramStyle
    //{
    //    void DrawEntity(DiagramPainter dp);
    //}

    [AddonType]
    public class DiagramStyleAddonType : AddonType
    {
        public override string Name
        {
            get { return "diagramstyle"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(DiagramStyle); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(DiagramStyleAttribute); }
        }
        public static readonly DiagramStyleAddonType Instance = new DiagramStyleAddonType();
    }

    //[AttributeUsage(AttributeTargets.Class)]
    //public class DiagramStyleAttribute : RegisterAttribute
    //{
    //}

    //[AddonType]
    //public class DiagramStyleAddonType : AddonType
    //{
    //    public override string Name
    //    {
    //        get { return "diagramstyle"; }
    //    }

    //    public override Type InterfaceType
    //    {
    //        get { return typeof(DiagramStyle); }
    //    }

    //    public override Type RegisterAttributeType
    //    {
    //        get { return typeof(DiagramStyleAttribute); }
    //    }

        //public override void GetPredefinedAddons(List<AddonHolder> res)
        //{
        //    res.Add(new PredefinedAddonHolder(PredefinedSpace, "default", "DatAdmin", CreateDatAdmin));
        //    res.Add(new PredefinedAddonHolder(PredefinedSpace, "vision", "Visio", CreateVisio));
        //    res.Add(new PredefinedAddonHolder(PredefinedSpace, "canonic", "Canonic", () => new DiagramStyle()));
        //}

        //public static DiagramStyle CreateDatAdmin()
        //{
        //    DiagramStyle res = new DiagramStyle();
        //    var v0 = res.DefaulEntity;
        //    var v1 = v0.RowStyleSet;
        //    var v2 = v1.PrimaryKey;
        //    var v3 = v2.Cells[0];
        //    v3.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.None;
        //    var v4 = v3.Image;
        //    v4.CoreIcon = "primary_key";
        //    var v5 = v2.Cells[1];
        //    v5.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.ColumnName;
        //    var v6 = v5.Font;
        //    v6.FontSize = 9.75;
        //    v6.Bold = true;
        //    var v7 = v2.Cells[2];
        //    v7.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.TypeName;
        //    var v8 = v1.Null;
        //    var v9 = v8.Cells[0];
        //    v9.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.None;
        //    var v10 = v8.Cells[1];
        //    v10.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.ColumnName;
        //    var v11 = v8.Cells[2];
        //    v11.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.TypeName;
        //    var v12 = v1.NotNull;
        //    var v13 = v12.Cells[0];
        //    v13.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.None;
        //    var v14 = v12.Cells[1];
        //    v14.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.ColumnName;
        //    var v15 = v14.Font;
        //    v15.FontSize = 9.75;
        //    v15.Bold = true;
        //    var v16 = v12.Cells[2];
        //    v16.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.TypeName;
        //    var v17 = v1.FkNull;
        //    var v18 = v17.Cells[0];
        //    v18.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.None;
        //    var v19 = v18.Image;
        //    v19.CoreIcon = "foreign_key";
        //    var v20 = v17.Cells[1];
        //    v20.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.ColumnName;
        //    var v21 = v17.Cells[2];
        //    v21.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.TypeName;
        //    var v22 = v1.FkNotNull;
        //    var v23 = v22.Cells[0];
        //    v23.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.None;
        //    var v24 = v23.Image;
        //    v24.CoreIcon = "foreign_key";
        //    var v25 = v22.Cells[1];
        //    v25.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.ColumnName;
        //    var v26 = v25.Font;
        //    v26.FontSize = 9.75;
        //    v26.Bold = true;
        //    var v27 = v22.Cells[2];
        //    v27.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.TypeName;
        //    var v28 = v0.Header;
        //    var v29 = v28.Cells[0];
        //    v29.Colspan = 3;
        //    var v30 = v29.Font;
        //    v30.FontSize = 12;
        //    v30.Bold = true;
        //    v29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        //    v28.BgColor = Color.FromName("PaleTurquoise");
        //    var v31 = v28.BottomBorder;
        //    v31.Color = Color.FromName("Silver");
        //    v31.Width = 2;
        //    return res;
        //}

        //public static DiagramStyle CreateVisio()
        //{
        //    DiagramStyle res = new DiagramStyle();
        //    var v0 = res.DefaulEntity;
        //    var v1 = v0.RowStyleSet;
        //    var v2 = v1.PrimaryKey;
        //    var v3 = v2.Cells[0];
        //    v3.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.None;
        //    v3.DataPostfix = "PK";
        //    var v4 = v3.Font;
        //    v4.FontSize = 9.75;
        //    v4.Bold = true;
        //    var v5 = v2.Cells[1];
        //    v5.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.ColumnName;
        //    var v6 = v5.LeftBorder;
        //    v6.Width = 1;
        //    var v7 = v5.Font;
        //    v7.FontSize = 9.75;
        //    v7.Bold = true;
        //    v7.Underline = true;
        //    v2.PaddingTop = 3;
        //    v2.PaddingBottom = 3;
        //    var v8 = v1.Null;
        //    var v9 = v8.Cells[0];
        //    v9.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.None;
        //    var v10 = v8.Cells[1];
        //    v10.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.ColumnName;
        //    var v11 = v10.LeftBorder;
        //    v11.Width = 1;
        //    var v12 = v1.NotNull;
        //    var v13 = v12.Cells[0];
        //    v13.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.None;
        //    var v14 = v12.Cells[1];
        //    v14.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.ColumnName;
        //    var v15 = v14.LeftBorder;
        //    v15.Width = 1;
        //    var v16 = v14.Font;
        //    v16.FontSize = 9.75;
        //    v16.Bold = true;
        //    var v17 = v1.FkNull;
        //    var v18 = v17.Cells[0];
        //    v18.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.FkIndex;
        //    v18.DataPrefix = "FK";
        //    var v19 = v17.Cells[1];
        //    v19.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.ColumnName;
        //    var v20 = v19.LeftBorder;
        //    v20.Width = 1;
        //    var v21 = v1.FkNotNull;
        //    var v22 = v21.Cells[0];
        //    v22.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.FkIndex;
        //    v22.DataPrefix = "FK";
        //    var v23 = v21.Cells[1];
        //    v23.Data = Plugin.diagrams.DiagramStyle.EntityStyle.ColumnData.ColumnName;
        //    var v24 = v23.LeftBorder;
        //    v24.Width = 1;
        //    var v25 = v23.Font;
        //    v25.FontSize = 9.75;
        //    v25.Bold = true;
        //    var v26 = v0.Header;
        //    var v27 = v26.Cells[0];
        //    v27.Colspan = 2;
        //    v27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        //    v26.BgColor = Color.FromName("Gainsboro");
        //    var v28 = v26.BottomBorder;
        //    v28.Width = 1;
        //    v26.PaddingTop = 3;
        //    v26.PaddingBottom = 3;
        //    var v29 = v0.PkColsFrame;
        //    var v30 = v29.BottomBorder;
        //    v30.Width = 1;
        //    var v31 = res.DefaultReference;
        //    v31.LineWay = Plugin.diagrams.DiagramStyle.ReferenceStyle.LineWayType.Rectangular;
        //    var v32 = v31.Arrow;
        //    v32.Width = 18;
        //    v32.Length = 15;
        //    v32.Delta = 0;
        //    v32.DistanceFromEnd = 0;
        //    return res;
        //}

    //    public static readonly DiagramStyleAddonType Instance = new DiagramStyleAddonType();
    //}
}
