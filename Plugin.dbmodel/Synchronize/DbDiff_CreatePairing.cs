using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DatAdmin;

namespace Plugin.dbmodel
{
    partial class DatabaseDiff
    {
        public bool IsPaired(IAbstractObjectStructure obj)
        {
            return srcGroupIds.ContainsKey(obj.GroupId) && dstGroupIds.ContainsKey(obj.GroupId);
        }

        public T FindPair<T>(T obj)
            where T : AbstractObjectStructure
        {
            var src = srcGroupIds.Get(obj.GroupId, null);
            var dst = dstGroupIds.Get(obj.GroupId, null);
            if (src == (AbstractObjectStructure)obj) return (T)dst;
            if (dst == (AbstractObjectStructure)obj) return (T)src;
            return null;
        }

        public IAbstractObjectStructure FindSource(string groupid)
        {
            if (groupid == null) return null;
            return srcGroupIds.Get(groupid, null);
        }

        public IAbstractObjectStructure FindTarget(string groupid)
        {
            if (groupid == null) return null;
            return dstGroupIds.Get(groupid, null);
        }

        // sparovani objektu
        private void CreatePairing()
        {
            PairDomains();
            PairTables();
            PairSpecificObjects();
        }

        private void PairDomains()
        {
            foreach (DomainStructure dsrc in m_src.Domains)
            {
                if (IsPaired(dsrc)) continue;
                foreach (DomainStructure ddst in m_dst.Domains)
                {
                    if (DbDiffTool.EqualFullNames(dsrc.FullName, ddst.FullName, m_options))
                    {
                        if (!IsPaired(ddst)) PairObjects(dsrc, ddst);
                    }
                }
            }
        }

        private void PairSpecificObjects()
        {
            foreach (SpecificObjectStructure osrc in m_src.GetAllSpecificObjects())
            {
                if (IsPaired(osrc)) continue;
                foreach (SpecificObjectStructure odst in m_dst.GetAllSpecificObjects())
                {
                    if (odst.ObjectType == osrc.ObjectType && DbDiffTool.EqualFullNames(osrc.ObjectName, odst.ObjectName, m_options))
                    {
                        if (!IsPaired(odst)) PairObjects(osrc, odst);
                    }
                }
            }
        }

        private void PairTables()
        {
            foreach (TableStructure tsrc in m_src.Tables)
            {
                if (IsPaired(tsrc)) continue;
                foreach (TableStructure tdst in m_dst.Tables)
                {
                    if (DbDiffTool.EqualFullNames(tsrc.FullName, tdst.FullName, m_options) && !IsPaired(tdst))
                    {
                        PairObjects(tsrc, tdst);
                        break;
                    }
                }
                //TableStructure tdst = m_dst.FindTable(tsrc.FullName);
                //if (tdst != null) PairObjects(tsrc, tdst);
            }

            if (m_options.AllowPairRenamedTables)
            {
                // snazime se tabulky sparovat na zaklade stejnych jmen VSECH sloupcu
                foreach (TableStructure tsrc in m_src.Tables)
                {
                    if (IsPaired(tsrc)) continue;
                    foreach (TableStructure tdst in m_dst.Tables)
                    {
                        if (DbDiffTool.EqualColumnNames(tsrc, tdst) && !IsPaired(tdst))
                        {
                            PairObjects(tsrc, tdst);
                            break;
                        }
                    }
                }
            }

            foreach (TableStructure tsrc in m_src.Tables)
            {
                TableStructure tdst = FindPair(tsrc);
                if (tdst != null)
                {
                    PairTableContent(tsrc, tdst);
                }
            }
        }

        private void PairTableContent(TableStructure tsrc, TableStructure tdst)
        {
            // parovani sloupcu podle jmen
            foreach (ColumnStructure csrc in tsrc.Columns)
            {
                ColumnStructure cdst = (ColumnStructure)tdst.Columns.FirstOrDefault(c => c.ColumnName == csrc.ColumnName);
                if (cdst != null)
                {
                    PairObjects(csrc, cdst);
                }
            }

            // parovani sloupcu dle indexu (jen ty nesparovane podle jmen)
            foreach (ColumnStructure csrc in tsrc.Columns)
            {
                if (IsPaired(csrc)) continue;
                int cindex = tsrc.Columns.GetIndex(csrc.ColumnName);
                if (cindex < tdst.Columns.Count && !IsPaired((AbstractObjectStructure)tdst.Columns[cindex]))
                {
                    PairObjects(csrc, (AbstractObjectStructure)tdst.Columns[cindex]);
                }
            }

            // sparovani primarnich klicu
            PrimaryKey psrc = tsrc.FindConstraint<PrimaryKey>();
            PrimaryKey pdst = tdst.FindConstraint<PrimaryKey>();
            if (psrc != null && pdst != null) PairObjects(psrc, pdst);

            // sparovani na zaklade jmen constraintu
            foreach (Constraint csrc in tsrc.Constraints)
            {
                if (IsPaired(csrc)) continue;
                Constraint cdst = tdst.Constraints.FirstOrDefault(c => c.Name != null && c.Name == csrc.Name) as Constraint;

                if (cdst != null && !IsPaired(cdst) && csrc.GetType() == cdst.GetType())
                {
                    PairObjects(csrc, cdst);
                }
            }

            // sparovani na zaklade typu a obsahu constraintu (jmen sloupcu/vyrazu CHECK)
            foreach (Constraint csrc in tsrc.Constraints)
            {
                if (IsPaired(csrc)) continue;

                foreach (Constraint cdst in tdst.Constraints)
                {
                    if (IsPaired(cdst)) continue;
                    if (csrc.GetType() != cdst.GetType()) continue;

                    if (csrc is ColumnsConstraint)
                    {
                        if (((ColumnsConstraint)csrc).Columns.EqualSequence(((ColumnsConstraint)cdst).Columns))
                        {
                            PairObjects(csrc, cdst);
                            break;
                        }
                    }

                    if (csrc is CheckConstraint)
                    {
                        if (((CheckConstraint)csrc).Expression == ((CheckConstraint)cdst).Expression)
                        {
                            PairObjects(csrc, cdst);
                            break;
                        }
                    }
                }
            }
        }

        private void PairObjects(AbstractObjectStructure src, AbstractObjectStructure dst)
        {
            dstGroupIds.Remove(dst.GroupId);
            dst.GroupId = src.GroupId;
            dstGroupIds[dst.GroupId] = dst;
        }
    }
}
