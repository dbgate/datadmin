using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class HLicense
    {
        public static event Action ChangedLicenses;

        public static void CallChangedLicenses()
        {
            if (ChangedLicenses != null) ChangedLicenses();
        }
    }
}
