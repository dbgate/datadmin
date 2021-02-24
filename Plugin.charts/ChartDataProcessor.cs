using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Linq;
using System.Globalization;
using System.Xml;

namespace Plugin.charts
{
    [ChartDataProcessor(Name = "values")]
    public class ValuesChartDataProcessor : ChartDataProcessorBase
    {
        [XmlElem]
        public string LabelColumn { get; set; }
        [XmlCollection(typeof(ChartData.ValueDef))]
        public List<ChartData.ValueDef> ValueDefs { get; set; }


        public ValuesChartDataProcessor()
        {
            ValueDefs = new List<ChartData.ValueDef>();
        }

        public override ChartData LoadChartData(ITabularDataView data)
        {
            var fmt = new BedValueFormatter(new DataFormatSettings());
            var res = new ChartData();
            res.ValueDefs = ValueDefs.ToArray();
            var tbl = data.GetStructure(null);
            var colnames = tbl.Columns.GetNames();
            int labindex = colnames.IndexOfEx(LabelColumn);
            int[] valindexes = new int[ValueDefs.Count];
            for (int i = 0; i < ValueDefs.Count; i++)
            {
                valindexes[i] = colnames.IndexOfEx(ValueDefs[i].Column);
            }

            var pg = new TablePageProperties { Count = 128 };
            var table = data.LoadTableData(pg);
            foreach (var row in table.Rows)
            {
                var item = new ChartData.DataItem();
                row.ReadValue(labindex);
                fmt.ReadFrom(row);
                item.Label = fmt.GetText();
                item.Values = new double[ValueDefs.Count];
                for (int i = 0; i < ValueDefs.Count; i++)
                {
                    row.ReadValue(valindexes[i]);
                    fmt.ReadFrom(row);
                    Double.TryParse(fmt.GetText(), NumberStyles.Number, CultureInfo.InvariantCulture, out item.Values[i]);
                }
                res.Items.Add(item);
            }
            return res;
        }
    }

    [ChartDataProcessor(Name = "histogram")]
    public class HistogramChartDataProcessor : ChartDataProcessorBase
    {
        [XmlElem]
        public string Column { get; set; }

        public override ChartData LoadChartData(ITabularDataView data)
        {
            var fmt = new BedValueFormatter(new DataFormatSettings());
            var res = new ChartData();

            res.ValueDefs = new ChartData.ValueDef[1];
            res.ValueDefs[0] = new ChartData.ValueDef();
            res.ValueDefs[0].Column = Column;
            res.ValueDefs[0].Label = Texts.Get("s_count");

            var dct = new Dictionary<string, int>();
            var tbl = data.GetStructure(null);

            int colindex = tbl.Columns.GetNames().IndexOfEx(Column);

            data.LoadAllRows(new TableDataSetProperties(), (row) =>
            {
                row.ReadValue(colindex);
                fmt.ReadFrom(row);
                string curval = fmt.GetText();
                if (!dct.ContainsKey(curval)) dct[curval] = 0;
                dct[curval] += 1;
            });

            var pairs = new List<KeyValuePair<string, int>>(dct);
            pairs.SortByKey(p => p.Value);
            pairs.Reverse();
            foreach (var pair in pairs)
            {
                var item = new ChartData.DataItem();
                item.Label = pair.Key;
                item.Values = new double[] { pair.Value };
                res.Items.Add(item);
            }

            return res;
        }
    }
}
