using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class HCellData
    {
        public static event Action<object, IDataHolder> ShowData;
        public static event Action<object> InvalidateData;

        public static void CallShowData(object sender, IDataHolder data)
        {
            if (ShowData != null) ShowData(sender, data);
        }

        public static void CallInvalidateData(object sender)
        {
            if (InvalidateData != null) InvalidateData(sender);
        }
    }
}
