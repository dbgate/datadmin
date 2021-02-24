using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DatAdmin
{
    public static class HSplash
    {
        public static event Action<Bitmap, string> AddModuleInfo;

        public static void CallAddModuleInfo(Bitmap image, string info)
        {
            if (AddModuleInfo != null) AddModuleInfo(image, info);
        }
    }
}
