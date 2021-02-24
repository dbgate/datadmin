using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.charts
{
    [AppObjectExtender(Name = "chartext", RequiredFeature = ChartsFeature.Test)]
    public class ChartsAppObjExtenders : AppObjectExtenderBase
    {
        public override void GetAppObjectExtendObjects(AppObject appobj, List<object> objs)
        {
            if (appobj is TableAppObject)
            {
                objs.Add(new TableEx(appobj));
            }
        }

        public class TableEx : AppObjectExtenderInternalBase
        {
            public TableEx(AppObject appobj) : base(appobj) { }

            [PopupMenu("s_chart", ImageName = CoreIcons.chartName, GroupName = "impexp")]
            public void BackupBrowser()
            {
                var data = m_appobj.GetTabularData(new ConnectionPack("chartframe"));
                MainWindow.Instance.OpenContent(new ChartFrame(data));
            }
        }
    }
}
