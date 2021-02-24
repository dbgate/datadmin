using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DatAdmin
{
    public class WaitObject
    {
        static WaitObject Instance = null;

        int m_contextDepth = 0;
        WaitDialog m_dialog = null;
        Cursor m_origCursor;
        DateTime m_started;

        WaitObject()
        {
            m_started = DateTime.UtcNow;
            m_origCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
        }

        public static void ActiveWait(IAsyncResult async)
        {
            try
            {
                EnterContext();
                Instance.PerformWait(async);
            }
            finally
            {
                LeaveContext();
            }
        }

        void PerformWait(IAsyncResult async)
        {
            while (!async.IsCompleted)
            {
                TimeSpan delta = DateTime.UtcNow - m_started;
                if (delta.TotalSeconds > 1 && m_dialog == null)
                {
                    m_dialog = new WaitDialog();
                    m_dialog.Show();
                }
                if (m_dialog != null && m_dialog.Canceled)
                {
                    throw new WaitAbortError();
                }
                try
                {
                    Application.DoEvents();
                }
                catch (Exception)
                {
                    // DoEvents cannot be called
                    Thread.Sleep(100);
                }
            }
        }

        void Close()
        {
            if (m_dialog != null)
            {
                m_dialog.Close();
                m_dialog.Dispose();
            }
            Cursor.Current = m_origCursor;
        }

        public static void EnterContext()
        {
            if (Instance == null)
            {
                Instance = new WaitObject();
            }
            Instance.m_contextDepth++;
        }

        public static void LeaveContext()
        {
            Instance.m_contextDepth--;
            if (Instance.m_contextDepth == 0)
            {
                Instance.Close();
                Instance = null;
            }
        }
    }


    public class WaitContext : IDisposable
    {
        public WaitContext()
        {
            if (!AsyncTool.IsMainThread) return;
            WaitObject.EnterContext();
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (!AsyncTool.IsMainThread) return;
            WaitObject.LeaveContext();
        }

        #endregion

    }
}
