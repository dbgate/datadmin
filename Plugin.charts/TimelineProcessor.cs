using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Linq;
using System.Globalization;
using System.Xml;

namespace Plugin.charts
{
    public class TimeLineItem
    {
        [XmlElem]
        public TimeLineExpression Expression { get; set; }
        [XmlElem]
        public TimeGroupSelector TimeSelector { get; set; }
        [XmlElem]
        public string Column { get; set; }
    }

    [ChartDataProcessor(Name = "timeline")]
    public class TimeLineChartDataProcessor : ChartDataProcessorBase
    {
        [XmlElem]
        public string DateColumn { get; set; }
        [XmlElem]
        public TimeLineStep Step { get; set; }
        [XmlElem]
        public TimeLineStep SmallStep { get; set; }
        [XmlElem]
        public bool UseStructuredTime { get; set; }
        [XmlCollection(typeof(TimeLineItem))]
        public List<TimeLineItem> Items { get; set; }

        public TimeLineChartDataProcessor()
        {
            Items = new List<TimeLineItem>();
        }

        private static string ExtractKey(string date, TimeLineStep step)
        {
            DateTime dt;
            if (!DateTime.TryParse(date, out dt)) return null;
            switch (step)
            {
                case TimeLineStep.Day: return dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                case TimeLineStep.Week: return dt.ToString("yyyy-") + CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday).ToString("00");
                case TimeLineStep.Month: return dt.ToString("yyyy-MM", CultureInfo.InvariantCulture);
                case TimeLineStep.Quarter: return dt.ToString("yyyy-", CultureInfo.InvariantCulture) + ("Q" + (dt.Month + 2) / 3).ToString();
                case TimeLineStep.Year: return dt.ToString("yyyy", CultureInfo.InvariantCulture);
                case TimeLineStep.DayOfWeek: return dt.ToString("ddd", CultureInfo.InvariantCulture);
            }
            return null;
        }

        private void AdaptValue(ref double curval, double myval, TimeLineExpression expr)
        {
            switch (expr)
            {
                case TimeLineExpression.Avg:
                case TimeLineExpression.Sum:
                    if (Double.IsNaN(curval)) curval = myval;
                    else curval += myval;
                    break;
                case TimeLineExpression.Min:
                    if (Double.IsNaN(curval)) curval = myval;
                    else if (myval < curval) curval = myval;
                    break;
                case TimeLineExpression.Max:
                    if (Double.IsNaN(curval)) curval = myval;
                    else if (myval > curval) curval = myval;
                    break;
            }
        }

        public override ChartData LoadChartData(ITabularDataView data)
        {
            var fmt = new BedValueFormatter(new DataFormatSettings());
            var res = new ChartData();

            res.ValueDefs = new ChartData.ValueDef[Items.Count];
            for (int i = 0; i < Items.Count; i++)
            {
                res.ValueDefs[i] = new ChartData.ValueDef();
                res.ValueDefs[i].Column = Items[i].Column;
                res.ValueDefs[i].Label = Items[i].Expression + (Items[i].Expression != TimeLineExpression.Count ? " " + Items[i].Column : "");
            }

            var tbl = data.GetStructure(null);
            var colnames = tbl.Columns.GetNames();

            int[] valindexes = new int[Items.Count];
            for (int i = 0; i < Items.Count; i++)
            {
                valindexes[i] = colnames.IndexOfEx(Items[i].Column);
            }
            int dtcolindex = colnames.IndexOfEx(DateColumn);
            if (dtcolindex < 0) return res;

            var dct = new Dictionary<string, TimeLineGroup>();

            var smallstep = SmallStep;
            if (!UseStructuredTime) smallstep = Step;

            // go through all rows
            data.LoadAllRows(new TableDataSetProperties(), (row) =>
            {
                row.ReadValue(dtcolindex);
                fmt.ReadFrom(row);
                string curdt = fmt.GetText();
                string key = ExtractKey(curdt, Step);
                if (key != null)
                {
                    TimeLineGroup item;
                    if (!dct.ContainsKey(key))
                    {
                        item = new TimeLineGroup { Key = key };
                        dct[key] = item;
                    }
                    else
                    {
                        item = dct[key];
                    }

                    var inkey = ExtractKey(curdt, smallstep);
                    if (inkey != null)
                    {
                        double[] vals;

                        if (!item.Items.ContainsKey(inkey))
                        {
                            vals = new double[Items.Count + 1];
                            for (int i = 0; i < Items.Count; i++) vals[i] = Double.NaN;
                            item.Items[inkey] = new ChartData.DataItem { Label = inkey, Values = vals };
                        }
                        else
                        {
                            vals = item.Items[inkey].Values;
                        }
                        vals[Items.Count] += 1; // count
                        for (int i = 0; i < Items.Count; i++)
                        {
                            if (valindexes[i] < 0)
                            {
                                if (Double.IsNaN(vals[i])) vals[i] = 0;
                                vals[i] += 1; // count
                            }
                            else
                            {
                                row.ReadValue(valindexes[i]);
                                fmt.ReadFrom(row);
                                double myval;
                                if (Double.TryParse(fmt.GetText(), NumberStyles.Number, CultureInfo.InvariantCulture, out myval))
                                {
                                    AdaptValue(ref vals[i], myval, Items[i].Expression);
                                }
                            }
                        }
                    }
                }
            });

            // summarize results
            var pairs = new List<KeyValuePair<string, TimeLineGroup>>(dct);
            pairs.SortByKey(p => p.Key);
            foreach (var pair in pairs)
            {
                var item = pair.Value;

                foreach (var itm in item.Items.Values)
                {
                    for (int i = 0; i < Items.Count; i++)
                    {
                        if (Double.IsNaN(itm.Values[i])) itm.Values[i] = 0;
                        if (Items[i].Expression == TimeLineExpression.Avg && itm.Values[Items.Count] > 0)
                        {
                            itm.Values[i] /= itm.Values[Items.Count];
                        }
                    }
                }

                if (UseStructuredTime)
                {
                    double[] vals = new double[Items.Count];
                    for (int i = 0; i < Items.Count; i++)
                    {
                        switch (Items[i].TimeSelector)
                        {
                            case TimeGroupSelector.Sum:
                                vals[i] = 0;
                                foreach (var val in item.Items.Values)
                                {
                                    vals[i] += val.Values[i];
                                }
                                break;
                            case TimeGroupSelector.Avg:
                                vals[i] = 0;
                                foreach (var val in item.Items.Values)
                                {
                                    vals[i] += val.Values[i];
                                }
                                if (item.Items.Count > 0) vals[i] /= item.Items.Count;
                                break;
                            case TimeGroupSelector.Min:
                                vals[i] = Double.NaN;
                                foreach (var val in item.Items.Values)
                                {
                                    if (Double.IsNaN(vals[i]) || val.Values[i] < vals[i])
                                    {
                                        vals[i] = val.Values[i];
                                    }
                                }
                                if (Double.IsNaN(vals[i])) vals[i] = 0;
                                break;
                            case TimeGroupSelector.Max:
                                vals[i] = Double.NaN;
                                foreach (var val in item.Items.Values)
                                {
                                    if (Double.IsNaN(vals[i]) || val.Values[i] > vals[i])
                                    {
                                        vals[i] = val.Values[i];
                                    }
                                }
                                if (Double.IsNaN(vals[i])) vals[i] = 0;
                                break;
                            case TimeGroupSelector.First:
                                {
                                    string key = item.Items.Keys.Min();
                                    if (key != null) vals[i] = item.Items[key].Values[i];
                                }
                                break;
                            case TimeGroupSelector.Last:
                                {
                                    string key = item.Items.Keys.Min();
                                    if (key != null) vals[i] = item.Items[key].Values[i];
                                }
                                break;
                            case TimeGroupSelector.Middle:
                                {
                                    var keys = new List<string>(item.Items.Keys);
                                    keys.Sort();
                                    if (keys.Count > 0)
                                    {
                                        if (keys.Count % 2 == 1)
                                        {
                                            vals[i] = item.Items[keys[keys.Count / 2]].Values[i];
                                        }
                                        else
                                        {
                                            vals[i] = (
                                                item.Items[keys[keys.Count / 2 - 1]].Values[i]
                                                + item.Items[keys[keys.Count / 2]].Values[i]
                                                ) / 2;
                                        }
                                    }
                                }
                                break;

                        }
                    }
                    res.Items.Add(new ChartData.DataItem { Label = pair.Key, Values = vals });
                }
                else
                {
                    var itm = item.Items.First().Value;
                    res.Items.Add(itm);
                }
            }
            return res;
        }

        class TimeLineGroup
        {
            internal string Key;
            internal Dictionary<string, ChartData.DataItem> Items = new Dictionary<string, ChartData.DataItem>();
        }
    }
}
