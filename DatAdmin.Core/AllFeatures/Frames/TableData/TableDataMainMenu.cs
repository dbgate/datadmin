using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public class TableDataMainMenu
    {
        TableDataFrame m_frame;

        public TableDataMainMenu(TableDataFrame frame)
        {
            m_frame = frame;
        }

        [PopupMenu("s_search", Shortcut = Keys.Control | Keys.F, ImageName = CoreIcons.findName)]
        public void Find()
        {
            if (m_frame.IsDisposed) return;
            m_frame.Find();
        }

        [PopupMenu("s_refresh", ImageName = CoreIcons.refreshName)]
        public void RefreshData()
        {
            if (m_frame.IsDisposed) return;
            m_frame.RefreshData();
        }

        [PopupMenu("s_export", ImageName = CoreIcons.exportName)]
        public void DoExport()
        {
            m_frame.DoExport();
        }

        [PopupMenu("s_paste", ImageName = CoreIcons.pasteName)]
        public void PasteMutliCell()
        {
            m_frame.PasteMultiCell();
        }

        [PopupMenu("s_show_referenced_data", ImageName = CoreIcons.referencesName, Shortcut = Keys.Control | Keys.E, RequiredFeature = MasterDetailViewsFeature.Test)]
        public void ShowReferencedData()
        {
            m_frame.CallShowReferences();
        }

        //[PopupMenu("s_goto_reference", ImageName = CoreIcons._gotoName, Shortcut = Keys.Control | Keys.G)]
        //public void GoToReference()
        //{
        //    m_frame.CallGoToReference();
        //}

        //[PopupMenu("s_back", ImageName = CoreIcons.left1Name, Shortcut = Keys.Control | Keys.Shift | Keys.G)]
        //public void BackReference()
        //{
        //    m_frame.CallGoBack();
        //}

        [PopupMenu("s_revert_changes", ImageName = CoreIcons.rollbackName)]
        public void RevertChanges()
        {
            m_frame.RevertChanges();
        }

        [PopupMenu("s_reset_view", ImageName = CoreIcons.resetName)]
        public void RefreshView()
        {
            if (m_frame.IsDisposed) return;
            m_frame.ResetView();
        }

        [PopupMenu("s_generate_sql", ImageName = CoreIcons.generate_sqlName, Shortcut = Keys.Control | Keys.G)]
        public void GenerateSql()
        {
            GenerateSqlForm.Run(m_frame);
        }

        //[PopupMenu("s_save", Shortcut = Keys.Control | Keys.S, ImageName = CoreIcons.saveName)]
        //public void SaveChanged()
        //{
        //    m_frame.SaveChanges();
        //}

        //[PopupMenu("s_generate_sql/s_backup_selected_cells")]
        //public void GenerateBackupSelectedCells()
        //{
        //    m_frame.GenerateSql(m_frame.DoBackupSelectedCells);
        //}

        ////[PopupMenu("s_generate_sql/s_insert_selected_cells")]
        ////public void GenerateInsert()
        ////{
        ////    m_frame.GenerateSql(m_frame.DoGenerateInsertSelectedCells);
        ////}

        //[PopupMenu("s_generate_sql/SELECT")]
        //public void GenerateAllSelect()
        //{
        //    m_frame.GenerateAllSelect();
        //}

        //[PopupMenu("s_generate_sql/DELETE")]
        //public void GenerateAllDelete()
        //{
        //    m_frame.GenerateAllDelete();
        //}
    }
}
