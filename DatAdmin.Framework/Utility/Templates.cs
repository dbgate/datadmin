using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public struct Tuple<T1, T2> : IComparable, IComparable<Tuple<T1, T2>>
    {
        public readonly T1 V1;
        public readonly T2 V2;

        public Tuple(T1 v1, T2 v2)
        {
            V1 = v1;
            V2 = v2;
        }

        public override string ToString()
        {
            return String.Format("<{0}; {1}>", V1, V2);
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            var tpl = (Tuple<T1, T2>)obj;
            return CompareTo(tpl);
        }

        #endregion

        #region IComparable<Tuple<T1,T2>> Members

        public int CompareTo(Tuple<T1, T2> other)
        {
            int res;
            res = ((IComparable)V1).CompareTo(other.V1);
            if (res != 0) return res;
            res = ((IComparable)V2).CompareTo(other.V2);
            return res;
        }

        #endregion
    }

    public struct Triple<T1, T2, T3>
    {
        public readonly T1 V1;
        public readonly T2 V2;
        public readonly T3 V3;

        public Triple(T1 v1, T2 v2, T3 v3)
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
        }

        public override string ToString()
        {
            return String.Format("<{0}; {1}; {2}>", V1, V2, V2);
        }
    }
}
