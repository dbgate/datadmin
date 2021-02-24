using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class GetChartingEngineEventArgs
    {
        public IChartingEngine Engine;
    }

    public static class HChartingEngine
    {
        public static event Action<GetChartingEngineEventArgs> CreateChartingEngine;
        public static IChartingEngine CallCreateChartingEngine()
        {
            var args = new GetChartingEngineEventArgs();
            if (CreateChartingEngine != null) CreateChartingEngine(args);
            return args.Engine;
        }
    }
}
