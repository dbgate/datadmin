using System;
using System.Collections.Generic;
using System.Text;
using ZedGraph;
using System.Drawing;
using DatAdmin;
using System.Xml;

namespace Plugin.charts
{
    public abstract class ChartStyle
    {
        public static Color[] COLORS = new Color[] { Color.Blue, Color.Green, Color.Red, Color.Cyan, Color.Purple, Color.Brown };

        public abstract void FillData(GraphPane pane, ChartData data);

        protected Color GetColor(int index)
        {
            return COLORS[index % COLORS.Length];
        }

        public abstract void SaveToXml(XmlElement xml);

        public static ChartStyle LoadFromXml(XmlElement xml)
        {
            switch (xml.GetAttribute("type"))
            {
                case "line": return new LineChartStyle();
                case "pie": return new PieChartStyle();
                case "bar": return new BarChartStyle();
                case "area": return new AreaChartStyle();
            }
            throw new InternalError("DAE-00388 unknown chart style:" + xml.GetAttribute("type"));
        }
    }

    public abstract class LineChartStyleBase : ChartStyle
    {
        public override void FillData(GraphPane pane, ChartData data)
        {
            var labs = new List<string>();
            pane.XAxis.Type = AxisType.Text;

            var series = new PointPairList[data.ValueDefs.Length];
            for (int j = 0; j < data.ValueDefs.Length; j++) series[j] = new PointPairList();

            for (int i = 0; i < data.Items.Count; i++)
            {
                labs.Add(data.Items[i].Label);
                for (int j = 0; j < data.ValueDefs.Length; j++)
                {
                    series[j].Add(i, data.Items[i].Values[j]);
                }
            }

            pane.XAxis.Scale.TextLabels = labs.ToArray();
            pane.XAxis.Scale.FontSpec.Angle = 90;
            pane.XAxis.Scale.IsPreventLabelOverlap = false;

            for (int j = 0; j < data.ValueDefs.Length; j++)
            {
                AddCurve(pane, data.ValueDefs[j].Label, series[j], j);
            }
        }

        protected virtual Color? GetFillColor(int index)
        {
            return null;
        }
        protected virtual void AddCurve(GraphPane pane, string label, PointPairList points, int index)
        {
            LineItem myCurve = pane.AddCurve(label, points, GetColor(index), SymbolType.Circle);
            var fcolor = GetFillColor(index);
            if (fcolor != null)
            {
                myCurve.Line.Fill = new Fill(Color.White, fcolor.Value, 45F);
            }
        }
    }

    public class LineChartStyle : LineChartStyleBase
    {
        public override string ToString()
        {
            return Texts.Get("s_line_chart");
        }

        public override void SaveToXml(XmlElement xml)
        {
            xml.SetAttribute("type", "line");
        }
    }

    public class AreaChartStyle : LineChartStyleBase
    {
        protected override Color? GetFillColor(int index)
        {
            return GetColor(index);
        }

        public override string ToString()
        {
            return Texts.Get("s_area_chart");
        }

        public override void SaveToXml(XmlElement xml)
        {
            xml.SetAttribute("type", "area");
        }
    }

    public class PieChartStyle : ChartStyle
    {
        public override void FillData(GraphPane pane, ChartData data)
        {
            for (int i = 0; i < data.Items.Count; i++)
            {
                pane.AddPieSlice(data.Items[i].Values[0], GetColor(i), 0.02, data.Items[i].Label);
            }
        }

        public override string ToString()
        {
            return Texts.Get("s_pie_chart");
        }

        public override void SaveToXml(XmlElement xml)
        {
            xml.SetAttribute("type", "pie");
        }
    }

    public class BarChartStyle : LineChartStyleBase
    {
        protected override void AddCurve(GraphPane pane, string label, PointPairList points, int index)
        {
            pane.AddBar(label, points, GetColor(index));
        }

        public override string ToString()
        {
            return Texts.Get("s_bar_chart");
        }

        public override void SaveToXml(XmlElement xml)
        {
            xml.SetAttribute("type", "bar");
        }
    }
}
