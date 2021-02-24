using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class StringSliceProvider : IStringSliceProvider
    {
        string m_data;
        public StringSliceProvider(string data)
        {
            m_data = data;
        }
        #region IStringSliceProvider Members

        public string GetStringSlice(int start, int stop)
        {
            if (stop > m_data.Length) stop = m_data.Length;
            if (start < 0) start = 0;
            if (stop <= start) return "";
            return m_data.Substring(start, stop - start);
        }

        #endregion
    }
}
