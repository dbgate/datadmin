using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public static class TablePerspectiveManager
    {
        static List<TablePerspective> m_cache;

        public static void ClearCache()
        {
            m_cache = null;
        }

        public static void WantCache()
        {
            if (m_cache == null)
            {
                m_cache = new List<TablePerspective>();
                foreach (string fn in Directory.GetFiles(Core.TablePerspectivesDirectory))
                {
                    try
                    {
                        var per = TablePerspective.FromFile(fn);
                        m_cache.Add(per);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        public static TablePerspective[] GetPerspectives(IPhysicalConnection conn, string dbname, NameWithSchema table, string[] columns)
        {
            WantCache();
            var res = new List<TablePerspective>();
            foreach (var p in m_cache) if (p.Conditions.Accept(conn, dbname, table, columns)) res.Add(p);
            return res.ToArray();
        }

        public static void RunManageDialog()
        {
            var editor = new TablePerspectiveListEditor();
            editor.ShowDialog("s_table_perspectives");
            TablePerspectiveManager.ClearCache();
        }
    }

    public class TablePerspectiveListEditor : DirectoryAddonListEditor
    {
        public TablePerspectiveListEditor()
            : base(TablePerspectiveAddonType.Instance)
        {
        }

        public override object GetEditObject(object item)
        {
            string fn = Path.Combine(TablePerspectiveAddonType.Instance.GetDirectory(), item.ToString() + ".per");
            var per = new TablePerspective();
            per.LoadFromFile(fn);
            return per.Conditions;
        }

        public override void SaveItem(object item, object editem)
        {
            string fn = Path.Combine(TablePerspectiveAddonType.Instance.GetDirectory(), item.ToString() + ".per");
            var per = new TablePerspective();
            per.LoadFromFile(fn);
            per.Conditions = (TablePerspectiveConditions)editem;
            per.SaveToFile(fn);
        }

        protected override void FillDefaultCaps(ListEditorCaps caps)
        {
            base.FillDefaultCaps(caps);
            caps.Edit = true;
        }

        public override bool Delete(object item)
        {
            bool res = base.Delete(item);
            TablePerspectiveManager.ClearCache();
            return res;
        }
    }
}
