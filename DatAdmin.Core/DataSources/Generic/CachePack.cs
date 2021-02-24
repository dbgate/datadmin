using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class CachePack : CacheBase
    {
        public static CachePack TreeCache = new CachePack
        {
            ReuseAnalyserCache = true,
        };

        public bool ReuseAnalyserCache = false;

        public ConnectionCache Connection(string connkey)
        {
            return GetChildCache<ConnectionCache>(connkey);
        }
    }

    public class ConnectionCache : CacheBase
    {
        public CachePack Parent { get { return (CachePack)m_parent; } }

        public DatabaseCache Database(string dbname)
        {
            return GetChildCache<DatabaseCache>(dbname);
        }

        public DatabaseCache GetAnalyserCache(string dbname)
        {
            if (Parent != null && Parent.ReuseAnalyserCache) return Database(dbname);
            return new DatabaseCache();
        }
    }

    public class DatabaseCache : CacheBase
    {
        public ConnectionCache Parent { get { return (ConnectionCache)m_parent; } }

        public TableCache Table(NameWithSchema tablename)
        {
            return GetChildCache<TableCache>(tablename.ToString());
        }
    }

    public class TableCache : CacheBase
    {
        public DatabaseCache Parent { get { return (DatabaseCache)m_parent; } }
    }
}
