using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace DatAdmin
{
    public static class IntegerExtension
    {
        public static int InRange(this int x, int lo, int hi)
        {
            Debug.Assert(lo <= hi);
            return x < lo ? lo : (x > hi ? hi : x);
        }
        public static bool IsInRange(this int x, int lo, int hi)
        {
            return x >= lo && x <= hi;
        }
    }
}
