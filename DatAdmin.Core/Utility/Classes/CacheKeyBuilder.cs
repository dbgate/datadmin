using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace DatAdmin
{
    public class CacheKeyBuilder
    {
        List<object> m_items = new List<object>();

        public void Add(object value)
        {
            m_items.Add(value);
        }

        public string CacheKey
        {
            get
            {
                MD5 md5 = MD5.Create();
                StringBuilder sb = new StringBuilder();
                foreach (object o in m_items)
                {
                    sb.Append("#sep#");
                    if (o == null) sb.Append("(NULL)");
                    else sb.Append(o.ToString());
                }
                byte[] bkey = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));
                return Convert.ToBase64String(bkey);
            }
        }
    }
}
