using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class DbSourceTarget
    {
        IDatabaseStructure m_src;
        IDatabaseStructure m_dst;

        public IDatabaseStructure Source { get { return m_src; } }
        public IDatabaseStructure Target { get { return m_dst; } }

        public DbSourceTarget(IDatabaseStructure src, IDatabaseStructure dst)
        {
            m_src = src;
            m_dst = dst;
        }
    }

    public class DbObjectPairing : DbSourceTarget
    {
        Dictionary<string, IAbstractObjectStructure> srcGroupIds = new Dictionary<string, IAbstractObjectStructure>();
        Dictionary<string, IAbstractObjectStructure> dstGroupIds = new Dictionary<string, IAbstractObjectStructure>();

        public DbObjectPairing(IDatabaseStructure src, IDatabaseStructure dst)
            : base(src,dst)
        {
            srcGroupIds.Clear();
            dstGroupIds.Clear();
            foreach (var obj in Source.GetAllObjects())
            {
                srcGroupIds[obj.GroupId] = obj;
            }
            foreach (var obj in Target.GetAllObjects())
            {
                dstGroupIds[obj.GroupId] = obj;
            }
        }

        public bool IsPaired(IAbstractObjectStructure obj)
        {
            return srcGroupIds.ContainsKey(obj.GroupId) && dstGroupIds.ContainsKey(obj.GroupId);
        }

        public T FindPair<T>(T obj)
            where T : class, IAbstractObjectStructure
        {
            var src = srcGroupIds.Get(obj.GroupId, null);
            var dst = dstGroupIds.Get(obj.GroupId, null);
            if (src == (IAbstractObjectStructure)obj) return (T)dst;
            if (dst == (IAbstractObjectStructure)obj) return (T)src;
            return null;
        }

    }
}
