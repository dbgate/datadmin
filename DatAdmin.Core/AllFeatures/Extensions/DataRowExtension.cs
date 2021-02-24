using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DatAdmin
{
    public static class DataRowExtension
    {
        public static string SafeString(this DataRow row, params string[] fieldVariants)
        {
            foreach (string field in fieldVariants)
            {
                int ord = row.Table.Columns.GetOrdinal(field);
                if (ord >= 0) return row[ord].SafeToString();
            }
            return null;
        }

        public static string SafeString(this DataRow row, int ord)
        {
            if (ord < 0) return null;
            return row[ord].SafeToString();
        }

        public static string SafeString(this DataRow row, string field)
        {
            int ord = row.Table.Columns.GetOrdinal(field);
            if (ord >= 0) return row[ord].SafeToString();
            return null;
        }

        public static bool SafeBool(this DataRow row, string field, bool defvalue)
        {
            int ord = row.Table.Columns.GetOrdinal(field);
            if (ord >= 0)
            {
                object val = row[ord];
                if (val is bool) return (bool)val;
                string s = val.SafeToString();
                if (s == null) return defvalue;
                if (s.ToLower() == "true" || s == "1") return true;
                if (s.ToLower() == "false" || s == "0") return false;
                return defvalue;
            }
            return defvalue;
        }

		public static bool IsAnyChange(this DataRow row) {
			bool anychange = false;
			for(int coli = 0; coli < row.Table.Columns.Count; coli++) {
				if (!Object.Equals(row[coli, DataRowVersion.Original], row[coli, DataRowVersion.Current])) {
					anychange = true;
				}
			}
			return anychange;
		}
	}
}
