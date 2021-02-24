using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class CacheCaps
    {
        public bool PropagateRefreshToParent;
    }

    public abstract class CacheBase
    {
        protected CacheBase m_parent;
        protected Dictionary<string, CacheBase> m_children = new Dictionary<string, CacheBase>();

        protected Dictionary<string, Namespace> m_namespaces = new Dictionary<string, Namespace>();
        Namespace m_defNs = new Namespace();

        public class Namespace
        {
            internal Dictionary<string, CacheItem> m_items = new Dictionary<string, CacheItem>();

            public void Put(string key, object value)
            {
                if (key == null) key = "";
                lock (m_items)
                {
                    m_items[key] = new CacheItem
                    {
                        CreatedAt = DateTime.Now,
                        Data = value,
                    };
                }
            }

            public object Get(string key)
            {
                if (key == null) key = "";
                lock (m_items)
                {
                    var res = m_items.Get(key);
                    if (res == null || res.m_refreshFlag) return null;
                    if (res.Error != null) throw res.Error;
                    return res.Data;
                }
            }

            public object Get(string key, Func<object> createData)
            {
                if (key == null) key = "";
                bool create = false;
                CacheItem res;
                lock (m_items)
                {
                    res = m_items.Get(key);
                    if (res == null || res.m_refreshFlag)
                    {
                        if (res == null)
                        {
                            res = new CacheItem();
                            m_items[key] = res;
                        }
                        res.m_refreshFlag = false;
                        create = true;
                    }
                }
                if (create)
                {
                    try
                    {
                        res.Data = createData();
                        res.CreatedAt = DateTime.Now;
                        res.Error = null;
                    }
                    catch (Exception err)
                    {
                        res.Error = err;
                    }
                }
                if (res.Error != null) throw res.Error;
                return res.Data;
            }

            public bool Has(string key)
            {
                if (key == null) key = "";
                lock (m_items)
                {
                    var res = m_items.Get(key);
                    if (res == null || res.m_refreshFlag) return false;
                    return true;
                }
            }

            public void PutException(string key, Exception err)
            {
                if (key == null) key = "";
                lock (m_items)
                {
                    m_items[key] = new CacheItem
                    {
                        CreatedAt = DateTime.Now,
                        Error = err,
                    };
                }
            }
        }

        public class CacheItem
        {
            public DateTime CreatedAt;
            public object Data;
            public Exception Error;
            internal bool m_refreshFlag;
        }

        protected T GetChildCache<T>(string key)
            where T : CacheBase, new()
        {
            if (key == null) key = "";
            lock (m_children)
            {
                T res = (T)m_children.Get(key, null);
                if (res == null)
                {
                    res = new T();
                    res.m_parent = this;
                    m_children[key] = res;
                }
                return res;
            }
        }

        private IEnumerable<CacheItem> EnumItems()
        {
            lock (m_namespaces)
            {
                foreach (var val in m_namespaces.Values)
                {
                    lock (val.m_items)
                    {
                        foreach (var item in val.m_items.Values)
                        {
                            yield return item;
                        }
                    }
                }
            }
            lock (m_defNs.m_items)
            {
                foreach (var item in m_defNs.m_items.Values)
                {
                    yield return item;
                }
            }
        }

        public object Get(string ns, string key)
        {
            return GetNs(ns).Get(key);
        }

        public object Get(string ns, string key, Func<object> createData)
        {
            return GetNs(ns).Get(key, createData);
        }

        public void Put(string ns, string key, object value)
        {
            GetNs(ns).Put(key, value);
        }

        public object Get(string key)
        {
            return m_defNs.Get(key);
        }

        public object Get(string key, Func<object> createData)
        {
            return m_defNs.Get(key, createData);
        }

        public void Put(string key, object value)
        {
            m_defNs.Put(key, value);
        }

        public void PutException(string ns, string key, Exception err)
        {
            GetNs(ns).PutException(key, err);
        }

        private Namespace GetNs(string ns)
        {
            if (ns == null) return m_defNs;
            lock (m_namespaces)
            {
                var res = m_namespaces.Get(ns, null);
                if (res == null)
                {
                    res = new Namespace();
                    m_namespaces[ns] = res;
                }
                return res;
            }
        }

        public bool Has(string ns, string key)
        {
            return GetNs(ns).Has(key);
        }

        public void BeginRefresh()
        {
            if (m_parent != null && Caps.PropagateRefreshToParent) m_parent.BeginRefresh();
            foreach (var item in EnumItems()) item.m_refreshFlag = true;
        }

        public void EndRefresh()
        {
            if (m_parent != null && Caps.PropagateRefreshToParent) m_parent.EndRefresh();
            foreach (var item in EnumItems()) item.m_refreshFlag = false;
        }

        public void ClearNs(string ns)
        {
            lock (m_namespaces)
            {
                m_namespaces.Remove(ns);
            }
        }

        public virtual CacheCaps Caps
        {
            get
            {
                return new CacheCaps
                {
                    PropagateRefreshToParent = false
                };
            }
        }

        public object this[string ns, string key]
        {
            get { return Get(ns, key); }
            set { Put(ns, key, value); }
        }

        public object this[string key]
        {
            get { return Get(key); }
            set { Put(key, value); }
        }

        public void Clear()
        {
            lock (m_namespaces) m_namespaces.Clear();
            lock (m_children) m_children.Clear();
            m_defNs = new Namespace();
        }
    }
}
