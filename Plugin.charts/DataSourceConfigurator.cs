using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DatAdmin;
using System.Xml;

namespace Plugin.charts
{
    public abstract class DataSourceConfigurator
    {
        public ITableStructure Table;
        public event EventHandler Changed;

        public abstract Control GetEditor();
        public abstract IChartDataProcessor GetProcessor();

        public DataSourceConfigurator(ITableStructure table)
        {
            Table = table;
        }

        public void CallChanged()
        {
            if (Changed != null) Changed(this, EventArgs.Empty);
        }

        public abstract void LoadFromProcessor(IChartDataProcessor proc);
        public abstract Type GetProcessorType();
    }
}
