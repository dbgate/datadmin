using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class ColumnStructureExtension
    {
        public static string GetSpecificAttribute(this IColumnStructure col, string dialect, string name)
        {
            string key = dialect + "." + name;
            if (col.SpecificData.ContainsKey(key)) return col.SpecificData[key];
            return null;
        }
        public static string[] GetNames(this IEnumerable<IColumnStructure> cols)
        {
            return new List<string>(from c in cols select c.ColumnName).ToArray();
        }
        public static string[] GetNames(this IEnumerable<IColumnReference> refs)
        {
            return new List<string>(from c in refs select c.ColumnName).ToArray();
        }
    }
}
