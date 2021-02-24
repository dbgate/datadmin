using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class ConnectionPack : IDisposable
    {
        public object Owner { get; private set; }
        /// <summary>
        /// cache associated with connection pack
        /// </summary>
        public CachePack Cache;

        public ConnectionPack(object owner)
        {
            Owner = owner;
        }

        class CacheItem
        {
            internal IPhysicalConnection Connection;
            internal int RefCount;
            internal bool KeepAlive;
        }

        Dictionary<string, CacheItem> m_connCache = new Dictionary<string, CacheItem>();
        int m_refCount;

        public IPhysicalConnection Open(IPhysicalConnectionFactory fact, bool keepAlive)
        {
            return _OpenCore(fact, keepAlive, true, true);
        }

        public IPhysicalConnection GetConnection(IPhysicalConnectionFactory fact, bool keepAlive)
        {
            return _OpenCore(fact, keepAlive, false, false);
        }

        private IPhysicalConnection _OpenCore(IPhysicalConnectionFactory fact, bool keepAlive, bool incref, bool open)
        {
            if (fact == null) return null;
            string key = fact.GetConnectionKey();
            IPhysicalConnection res;
            lock (m_connCache)
            {
                if (!m_connCache.ContainsKey(key))
                {
                    var item = new CacheItem();
                    item.Connection = fact.CreateConnection();
                    if (Cache != null)
                    {
                        // use shared cache
                        item.Connection.Cache = Cache.Connection(key);
                    }
                    item.Connection.Owner = this;
                    m_connCache[key] = item;
                }
                if (incref) m_connCache[key].RefCount++;
                if (keepAlive) m_connCache[key].KeepAlive = true;
                res = m_connCache[key].Connection;
            }
            if (open) Async.SafeOpen(res);
            return res;
        }

        public void Close(IPhysicalConnectionFactory fact)
        {
            string key = fact.GetConnectionKey();
            lock (m_connCache)
            {
                if (!m_connCache.ContainsKey(key)) throw new InternalError("DAE-00029 Connection key not found");
                m_connCache[key].RefCount--;
                if (m_connCache[key].RefCount <= 0 && !m_connCache[key].KeepAlive)
                {
                    Async.SafeClose(m_connCache[key].Connection);
                }
            }
        }

        public void RemoveByKey(string connkey)
        {
            lock (m_connCache)
            {
                Async.SafeClose(m_connCache[connkey].Connection);
                m_connCache.Remove(connkey);
            }
        }

        public IAsyncResult BeginRemoveByKey(string connkey, AsyncCallback callback)
        {
            CacheItem item;
            lock (m_connCache)
            {
                if (!m_connCache.ContainsKey(connkey)) return new ValueAsyncResult(null, null);
                item = m_connCache[connkey];
                //m_connCache.Remove(connkey);
            }
            if (item.Connection.IsOpened) return item.Connection.BeginClose(callback);
            return new ValueAsyncResult(null, null);
        }

        public void CloseAll()
        {
            lock (m_connCache)
            {
                foreach (var item in m_connCache.Values)
                {
                    Async.SafeClose(item.Connection);
                }
                m_connCache.Clear();
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            CloseAll();
        }

        #endregion

        public void AddRef()
        {
            m_refCount++;
        }

        public void Release()
        {
            m_refCount--;
            if (m_refCount <= 0) CloseAll();
        }

        public bool Contains(string connkey)
        {
            lock (m_connCache) return m_connCache.ContainsKey(connkey);
        }

        public bool IsOpened(IPhysicalConnectionFactory fact)
        {
            string key = fact.GetConnectionKey();
            lock (m_connCache)
            {
                var item = m_connCache[key];
                return item.Connection != null && item.Connection.IsOpened;
            }
        }

        public override string ToString()
        {
            return Owner.ToString();
        }
    }

    public interface IConnectionPackHolder
    {
        ConnectionPack ConnPack { get; set; }
    }
}
