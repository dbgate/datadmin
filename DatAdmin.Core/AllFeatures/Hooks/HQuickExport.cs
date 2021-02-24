using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class HQuickExport
    {
        public static event Action ChangedExports;

        public static void CallChangedExports()
        {
            if (ChangedExports != null) ChangedExports();
        }
    }
}
