using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class HSettings
    {
        public static event Action ReloadSettings;
        public static event Action Loaded;

        public static void CallReloadSettings()
        {
            if (ReloadSettings != null) ReloadSettings();
        }

        public static void CallLoaded()
        {
            if (Loaded != null) Loaded();
        }
    }
}
