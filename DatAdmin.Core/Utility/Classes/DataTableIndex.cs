using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DatAdmin
{
    public class DataTableIndex
    {
        class Directory
        {
            internal NullableKeysDictionary<string, Directory> m_dirs = new NullableKeysDictionary<string, Directory>();
            internal List<DataRow> m_rows = new List<DataRow>();
        }

        Directory m_root = new Directory();
        DataTable m_table;

        public DataTableIndex(DataTable table, params int[] indexCols)
        {
            m_table = table;
            foreach (DataRow row in table.Rows)
            {
                var dir = m_root;
                foreach (int col in indexCols)
                {
                    string val = row.SafeString(col);
                    if (!dir.m_dirs.ContainsKey(val)) dir.m_dirs[val] = new Directory();
                    dir = dir.m_dirs[val];
                }
                dir.m_rows.Add(row);
            }
        }

        public DataTable Query(params Func<string, bool>[] keys)
        {
            var res = m_table.Clone();
            var cur = new List<Directory>();
            var next = new List<Directory>();
            cur.Add(m_root);
            foreach (var func in keys)
            {
                foreach (var curitem in cur)
                {
                    foreach (var item in curitem.m_dirs)
                    {
                        if (!func(item.Key)) continue;
                        next.Add(item.Value);
                        foreach (var row in item.Value.m_rows) res.ImportRow(row);
                    }
                }
                cur = next;
                next = new List<Directory>();
            }
            return res;
        }
    }
}
