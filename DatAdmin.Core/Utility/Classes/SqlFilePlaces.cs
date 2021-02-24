using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public abstract class SqlFilePlaceBase : TempFilePlaceBase, ICustomPropertyPage
    {
        public override bool SupportsSave(IExtendedFileNameHolderInfo info)
        {
            return info != null && info.FileExtension == "sql";
        }

        #region ICustomPropertyPage Members

        public System.Windows.Forms.Control CreatePropertyPage()
        {
            return null;
        }

        #endregion
    }

    [FilePlace(Name = "openinnewwindow", Title = "s_open_in_new_window")]
    public class OpenInNewWindowFilePlace : SqlFilePlaceBase
    {
        QueryFrame m_frame;
        string m_sqltext;
        bool m_shownGenereratedSql;

        protected override void PreparePlace()
        {
            MainWindow.Instance.RunInMainWindow(DoOpenWindow);
        }

        private void DoOpenWindow()
        {
            OpenQueryParameters pars = new OpenQueryParameters();
            pars.GeneratingSql = true;
            var appobj = ContainerInfo.RelatedObject;
            IPhysicalConnection newconn = null;
            if (ContainerInfo.RelatedConnection != null)
            {
                newconn = ContainerInfo.RelatedConnection.CreateConnection();
            }
            if (newconn != null && ContainerInfo.RelatedDatabase != null) newconn.SetOnOpenDatabase(ContainerInfo.RelatedDatabase);
            pars.GeneratingSql = true;
            m_frame = new QueryFrame(newconn, pars);
            MainWindow.Instance.OpenContent(m_frame);
            MainWindow.Instance.Window.BringToFront();
            if (!m_shownGenereratedSql)
            {
                m_frame.GenerateSqlFinished(m_sqltext);
                m_shownGenereratedSql = true;
            }
        }

        protected override void AfterWriteAction(string file)
        {
            using (var sr = new StreamReader(file))
            {
                m_sqltext = sr.ReadToEnd();
            }
            MainWindow.Instance.RunInMainWindow(DoShowGeneratedSql);
            ProgressInfo.SetCloseOnFinish(1, true);
        }

        private void DoShowGeneratedSql()
        {
            if (m_frame != null)
            {
                m_frame.GenerateSqlFinished(m_sqltext);
                m_shownGenereratedSql = true;
            }
        }

        public override bool LoadVirtualFile(string virtualFile, IExtendedFileNameHolderInfo holder)
        {
            return virtualFile == "@opennewwindow";
        }

        public override string GetVirtualFile()
        {
            return "@opennewwindow";
        }
    }

    [FilePlace(Name = "pasteintocurrentwindow", Title = "s_paste_into_current_window")]
    public class PasteIntoCurrentFilePlace : SqlFilePlaceBase
    {
        QueryFrame m_frame;
        string m_sqltext;

        protected override void PreparePlace()
        {
            MainWindow.Instance.RunInMainWindow(DoFindWindow);
        }

        private void DoFindWindow()
        {
            m_frame = MainWindow.Instance.GetCurrentContent() as QueryFrame;
            if (m_frame == null)
            {
                throw new InternalError("DAE-00053 No current query window found");
            }
        }

        protected override void AfterWriteAction(string file)
        {
            using (var sr = new StreamReader(file))
            {
                m_sqltext = sr.ReadToEnd();
            }
            MainWindow.Instance.RunInMainWindow(DoShowGeneratedSql);
            ProgressInfo.SetCloseOnFinish(1, true);
        }

        private void DoShowGeneratedSql()
        {
            if (m_frame != null && m_sqltext != null)
            {
                m_frame.InsertSqlFragment(m_sqltext);
            }
        }

        public override bool LoadVirtualFile(string virtualFile, IExtendedFileNameHolderInfo holder)
        {
            return virtualFile == "@pasteintocurrentwindow";
        }

        public override string GetVirtualFile()
        {
            return "@pasteintocurrentwindow";
        }

        public override bool SupportsSave(IExtendedFileNameHolderInfo info)
        {
            return base.SupportsSave(info) && MainWindow.Instance.GetCurrentContent() is QueryFrame;
        }
    }

    [FilePlace(Name = "executesql", Title = "s_execute_sql")]
    public class ExecuteSqlFilePlace : SqlFilePlaceBase
    {
        protected override void AfterWriteAction(string file)
        {
            var connf = ContainerInfo.RelatedConnection;
            var conn = connf.CreateConnection();
            IDatabaseSource db = connf.CreateDatabaseSource(conn, ContainerInfo.RelatedDatabase);
            db.Connection.SetOnOpenDatabase(ContainerInfo.RelatedDatabase);
            Async.SafeOpen(db.Connection);
            try
            {
                using (var sr = new StreamReader(file))
                {
                    var loader = db.Dialect.CreateDumpLoader();
                    loader.Connection = db.Connection.SystemConnection;
                    loader.ProgressInfo = ProgressInfo;
                    loader.Run(sr);
                }
            }
            finally
            {
                Async.SafeClose(db.Connection);
            }
        }

        public override bool LoadVirtualFile(string virtualFile, IExtendedFileNameHolderInfo holder)
        {
            return virtualFile == "@executesql";
        }

        public override string GetVirtualFile()
        {
            return "@executesql";
        }
    }
}
