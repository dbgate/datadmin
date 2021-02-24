using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class ArrayTool
    {
        public static bool Contains<T>(IEnumerable<T> list, T value)
        {
            foreach (T x in list)
            {
                if (x == null)
                {
                    if (value == null) return true;
                }
                else
                {
                    if (x.Equals(value)) return true;
                }
            }
            return false;
        }

        public static bool EqualsArrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++) if (a[i] != b[i]) return false;
            return true;
        }

        public static string[] Difference(IEnumerable<string> a, IEnumerable<string> b)
        {
            var sb = new HashSetEx<string>(b);
            var res = new List<string>();
            foreach (string s in a)
            {
                if (!sb.Contains(s)) res.Add(s);
            }
            return res.ToArray();
        }
    }
}
