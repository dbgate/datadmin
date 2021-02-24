using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DatAdmin
{
    public partial class TableDataFrame
    {
        //Dictionary<NameWithSchema, DataTable> m_listCache = new Dictionary<NameWithSchema, DataTable>();
        Dictionary<int, ColumnListDrawer> m_listDrawers = new Dictionary<int, ColumnListDrawer>();
        Dictionary<int, DataLookupInfo> m_lookupInfo = new Dictionary<int, DataLookupInfo>();

        //DataTable GetListTable(NameWithSchema 
        bool m_wantRepaint;

        void LoadLists()
        {
            // sem se to dostane az kdyz je m_dispModel cely vyplnen !!!

            if (!m_settings.ShowLookupHint && !m_settings.ShowLookupSelection)
            {
                TabularData.Connection.Cache.EndRefresh();
                return;
            }

            var dialect = TabularData.Dialect ?? GenericDialect.Instance;

            m_listDrawers.Clear();
            m_lookupInfo.Clear();

            for (int colindex = 0; colindex < m_dispModel.Count; colindex++)
            {
                var fk = m_dispModel[colindex].Reference;
                if (fk == null) continue;
                ITableSource dsttable = CurrentData.TableSource.Database.GetTable(fk.PrimaryKeyTable);

                DataTable listData;
                try
                {
                    listData = dsttable.CGetListData(m_settings.ListRowLimit, LoadLookupData);
                    if (listData == null) continue; // pokud jeste data nejsou nactena, nic nevime
                }
                catch
                {
                    // data jsou moc velka
                    listData = null;
                    //if (listData.Rows.Count > TableDataSettings.Instance.ListRowLimit) listData = null;
                }

                string pkcolname = fk.PrimaryKeyColumns[0].ColumnName;

                if (m_table != null && m_settings.ShowLookupHint && !Core.IsMono && dialect.DialectCaps.OptimizedComplexConditions)
                {
                    if (listData == null)
                    {
                        var pks = new Dictionary<string, bool>();
                        foreach (BedRow row in m_table.Rows)
                        {
                            string pkval = row[colindex].SafeToString();
                            if (pkval != null) pks[pkval] = true;
                        }
                        if (!m_listDrawers.ContainsKey(colindex) || m_listDrawers[colindex] != null)
                        {
                            try
                            {
                                LookupInfo lookup = dsttable.CGetLookupInfo(pkcolname, pks.Keys, LoadLookupData);
                                if (lookup != null)
                                {
                                    ColumnListDrawer cld = new ColumnListDrawer(lookup);
                                    m_listDrawers[colindex] = cld;
                                }
                            }
                            catch (Exception)
                            {
                                m_listDrawers[colindex] = null;
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            LookupInfo lookup = new LookupInfo(pkcolname, listData);
                            m_listDrawers[colindex] = new ColumnListDrawer(lookup);
                        }
                        catch (Exception)
                        {
                            m_listDrawers[colindex] = null;
                        }

                    }
                }
                if (m_settings.ShowLookupSelection && listData != null)
                {
                    m_lookupInfo[colindex] = new DataLookupInfo
                    {
                        PkColIndex = listData.Columns.GetOrdinal(fk.PrimaryKeyColumns[0].ColumnName),
                        Table = listData
                    };
                }
            }

            m_wantRepaint = true;
            TabularData.Connection.Cache.EndRefresh();
        }

        private DataLookupInfo GetLookupInfoForCol(int colindex)
        {
            if (m_lookupInfo.ContainsKey(colindex)) return m_lookupInfo[colindex];
            return null;
        }
    }


    public class ColumnListDrawer
    {
        LookupInfo m_lookup;

        internal ColumnListDrawer(LookupInfo lookup)
        {
            m_lookup = lookup;
        }
        internal string GetValue(object value)
        {
            return m_lookup[value.SafeToString()];
        }
    }



    //public class ColumnListDrawer
    //{
    //    Dictionary<string, string> m_fkCaptions = new Dictionary<string, string>();
    //    int m_pkColIndex;
    //    DataTable m_table;

    //    internal static ColumnListDrawer Create(IForeignKey fk, DataTable tbl)
    //    {
    //        ColumnListDrawer res = new ColumnListDrawer();
    //        res.m_table = tbl;
    //        res.m_pkColIndex = tbl.Columns.GetOrdinal(fk.PrimaryKeyColumns[0]);
    //        int valindex = -1;
    //        for (int i = 0; i < tbl.Columns.Count; i++)
    //        {
    //            if (i == res.m_pkColIndex) continue;
    //            if (tbl.Columns[i].DataType != typeof(string)) continue;
    //            valindex = i;
    //            break;
    //        }
    //        if (valindex < 0) return null;
    //        foreach (DataRow row in tbl.Rows)
    //        {
    //            res.m_fkCaptions[row[res.m_pkColIndex].ToString()] = StringTool.SafeToString(row[valindex]) ?? "";
    //        }
    //        return res;
    //    }

    //    internal string GetValue(object value)
    //    {
    //        if (value == null) return null;
    //        string res;
    //        if (m_fkCaptions.TryGetValue(value.ToString(), out res)) return res;
    //        return null;
    //    }

    //    internal DataLookupInfo GetLookupInfo()
    //    {
    //        return new DataLookupInfo
    //        {
    //            PkColIndex = m_pkColIndex,
    //            Table = m_table
    //        };
    //    }
    //}
}
