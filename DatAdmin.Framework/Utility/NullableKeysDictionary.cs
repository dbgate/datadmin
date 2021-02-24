using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class NullableKeysDictionary<Key, Value> : IEnumerable<KeyValuePair<Key, Value>>
        where Key : class
    {
        bool m_isNullKey = false;
        Value m_nullValue = default(Value);
        Dictionary<Key, Value> m_dict = new Dictionary<Key, Value>();
        public Value this[Key key]
        {
            get
            {
                if (key == null)
                {
                    if (m_isNullKey) return m_nullValue;
                    throw new Exception("DAE-00314 Null key not in dictionary");
                }
                else
                {
                    return m_dict[key];
                }
            }
            set
            {
                if (key == null)
                {
                    m_isNullKey = true;
                    m_nullValue = value;
                }
                else
                {
                    m_dict[key] = value;
                }
            }
        }

        public bool ContainsKey(Key key)
        {
            if (key == null) return m_isNullKey;
            else return m_dict.ContainsKey(key);
        }

        public IEnumerable<Key> Keys
        {
            get
            {
                if (m_isNullKey) yield return null;
                foreach (Key key in m_dict.Keys)
                {
                    yield return key;
                }
            }
        }

        public void Clear()
        {
            m_dict.Clear();
        }

        #region IEnumerable<KeyValuePair<Key,Value>> Members

        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
        {
            if (m_isNullKey) yield return new KeyValuePair<Key, Value>(null, m_nullValue);
            foreach (var item in m_dict)
            {
                yield return item;
            }
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            if (m_isNullKey) yield return new KeyValuePair<Key, Value>(null, m_nullValue);
            foreach (var item in m_dict)
            {
                yield return item;
            }
        }

        #endregion
    }
}
