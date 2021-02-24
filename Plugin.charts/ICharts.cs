using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Xml;

namespace Plugin.charts
{
    public enum TimeLineStep { Day, Week, Month, Quarter, Year, DayOfWeek }
    public enum TimeLineExpression { Count, Sum, Min, Max, Avg }
    public enum TimeGroupSelector { First, Last, Middle, Sum, Min, Max, Avg }

    //public class AggregateDataColumn
    //{
    //    public string Column;
    //    public AggregateExpression Expression;
    //}

    //public interface IChartDataSource : IChartDataHolder
    //{
    //    ITableStructure GetStructure();
    //    IEnumerable<IBedRecord> GetRows();
    //    IEnumerable<IBedRecord> GetAggregatedRows(string agrcol, AggregateSelector agrsel, AggregateDataColumn[] data);
    //    /// <summary>
    //    /// connection associated with data source
    //    /// if not null, all methods must be invoked into this connection
    //    /// </summary>
    //    IPhysicalConnection Connection { get; } 
    //}

    public class ChartingEngine : IChartingEngine
    {
        #region IChartingEngine Members

        public ContentFrame CreateFrame(ITabularDataView data)
        {
            return new ChartFrame(data);
        }

        #endregion
    }

    [PluginHandler]
    public class ChartPluginHandler : PluginHandlerBase
    {
        public override void OnLoadAssembly()
        {
            base.OnLoadAssembly();
            HChartingEngine.CreateChartingEngine += new Action<GetChartingEngineEventArgs>(HChartingEngine_CreateChartingEngine);
        }

        void HChartingEngine_CreateChartingEngine(GetChartingEngineEventArgs obj)
        {
            if (ChartsFeature.Allowed)
            {
                obj.Engine = new ChartingEngine();
            }
        }
    }
}
