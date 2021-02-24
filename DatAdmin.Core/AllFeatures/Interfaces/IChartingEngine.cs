using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public interface IChartDataHolder
    {
    }

    public interface IChartingEngine
    {
        ContentFrame CreateFrame(ITabularDataView data);
    }

    public class ChartDataProcessorAttribute : RegisterAttribute { }

    [AddonType]
    public class ChartDataProcessorAddonType : AddonType
    {
        public override string Name
        {
            get { return "chartdataprocessor"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IChartDataProcessor); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(ChartDataProcessorAttribute); }
        }

        public static readonly ChartDataProcessorAddonType Instance = new ChartDataProcessorAddonType();
    }


    public interface IChartDataProcessor : IAddonInstance
    {
        ChartData LoadChartData(ITabularDataView source);
    }

    public class ChartData
    {
        public List<DataItem> Items = new List<DataItem>();
        public ValueDef[] ValueDefs;

        public class DataItem
        {
            public string Label;
            public double[] Values;
        }

        public class ValueDef
        {
            [XmlElem]
            public string Label { get; set; }
            [XmlElem]
            public string Column { get; set; }
        }
    }

    public abstract class ChartDataProcessorBase : AddonBase, IChartDataProcessor
    {
        public abstract ChartData LoadChartData(ITabularDataView source);

        public override AddonType AddonType
        {
            get { return ChartDataProcessorAddonType.Instance; }
        }
    }

    [Feature(Name = _Name, Title = "Charts")]
    public class ChartsFeature : FeatureBase
    {
        public static bool Allowed { get { return LicenseTool.FeatureAllowed(ChartsFeature.Test); } }
        public const string _Name = "charts";
        public const string Test = _Name + "|" + ProfessionalFeature._Name;
    }
}
