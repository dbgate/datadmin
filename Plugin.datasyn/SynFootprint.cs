using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DatAdmin;

namespace Plugin.datasyn
{
    public class SynFootprint : IComparable
    {
        public string[] KeyData;
        public string Hash;

        #region IComparable Members

        public int CompareTo(object obj)
        {
            var ft = obj as SynFootprint;
            if (ft != null)
            {
                return KeyData.CompareSequence(ft.KeyData);
            }
            return 0;
        }

        #endregion

        public static bool operator <(SynFootprint a, SynFootprint b) { return a.CompareTo(b) < 0; }
        public static bool operator >(SynFootprint a, SynFootprint b) { return a.CompareTo(b) > 0; }
        public static bool operator <=(SynFootprint a, SynFootprint b) { return a.CompareTo(b) <= 0; }
        public static bool operator >=(SynFootprint a, SynFootprint b) { return a.CompareTo(b) >= 0; }

        public static SynFootprint FromReader(IBedRecord record, int keylen, BedValueHolder holder, BedValueConvertor conv, IDataSynAdapter adapter)
        {
            var res = new SynFootprint();
            res.KeyData = new string[keylen];
            for (int i = 0; i < keylen; i++)
            {
                record.ReadValue(i);
                conv.ConvertValue(record, TypeStorage.String, holder);
                res.KeyData[i] = holder.GetString();
            }
            record.ReadValue(keylen);
            res.Hash = adapter.ReadHash(record, conv, holder);
            return res;
        }

        public void SaveToStream(BinaryWriter bw)
        {
            foreach (string s in KeyData) bw.Write(s);
            bw.Write(Hash);
        }

        public static SynFootprint FromStream(BinaryReader br, int keylen)
        {
            var res = new SynFootprint();
            res.KeyData = new string[keylen];
            for (int i = 0; i < keylen; i++)
            {
                res.KeyData[i] = br.ReadString();
            }
            res.Hash = br.ReadString();
            return res;
        }

        public bool EqualKey(SynFootprint other)
        {
            return KeyData.EqualSequence(other.KeyData);
        }
    }
}
