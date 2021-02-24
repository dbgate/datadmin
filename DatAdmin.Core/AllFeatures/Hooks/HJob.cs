using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class HJob
    {
        public static event Action EveryMinute;

        public static void CallEveryMinute()
        {
            if (EveryMinute != null) EveryMinute();
        }
    }
}
