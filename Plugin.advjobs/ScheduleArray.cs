using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.advjobs
{
    public class ScheduleArray
    {
        List<int> m_items = new List<int>();
        int m_min, m_max;

        public ScheduleArray(int min, int max)
        {
            m_min = min;
            m_max = max;
        }

        public static ScheduleArray Parse(string s, int min, int max)
        {
            var res = new ScheduleArray(min, max);
            if (s == "*")
            {
                for (int i = min; i <= max; i++) res.m_items.Add(i);
                return res;
            }
            if (s == "?" || String.IsNullOrEmpty(s))
            {
                return res;
            }
            foreach (string item in s.Split(','))
            {
                string it2 = item.Trim();
                if (it2.Contains("-"))
                {
                    string[] bounds = it2.Split('-');
                    int low = Int32.Parse(bounds[0].Trim());
                    int high = Int32.Parse(bounds[1].Trim());
                    for (int i = low; i <= high; i++) res.m_items.Add(i);
                }
                else
                {
                    res.m_items.Add(Int32.Parse(it2));
                }
            }
            return res;
        }

        public bool HasAll()
        {
            for (int i = m_min; i <= m_max; i++) if (!m_items.Contains(i)) return false;
            return true;
        }

        public override string ToString()
        {
            if (HasAll()) return "*";
            if (m_items.Count == 0) return "?";
            var sb = new StringBuilder();
            m_items.Sort();
            for (int i = 0; i < m_items.Count; )
            {
                if (i > 0) sb.Append(",");
                int j = i + 1;
                while (j < m_items.Count && m_items[j] == m_items[j - 1] + 1) j++;
                if (j - i > 1)
                {
                    sb.AppendFormat("{0}-{1}", m_items[i], m_items[j - 1]);
                }
                else
                {
                    sb.AppendFormat("{0}", m_items[i]);
                }
                i = j;
            }
            return sb.ToString();
        }

        public List<int> Items { get { return m_items; } }

        public bool this[int item]
        {
            get { return Items.Contains(item); }
        }
    }
}
