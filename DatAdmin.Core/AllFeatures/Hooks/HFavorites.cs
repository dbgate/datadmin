﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class HFavorites
    {
        public static event Action Changed;

        public static void CallChanged()
        {
            if (Changed != null) Changed();
        }
    }
}
