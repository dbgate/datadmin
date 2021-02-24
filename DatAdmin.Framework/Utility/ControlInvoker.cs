using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public class ControlInvoker : IInvoker
    {
        Control m_ctrl;
        public ControlInvoker(Control ctrl)
        {
            m_ctrl = ctrl;
        }

        #region IInvoker Members

        public bool IsInInvokerThread
        {
            get { return AsyncTool.IsMainThread; }
        }

        public IAsyncResult BeginInvoke(PriorityLevel priority, bool behaveAsStack, IInvokableAction action, AsyncCallback callback)
        {
            DateTime started = DateTime.Now;
            while (!m_ctrl.Created)
            {
                if (AsyncTool.IsMainThread) Application.DoEvents();
                else System.Threading.Thread.Sleep(100);
                if (DateTime.Now - started > TimeSpan.FromSeconds(3)) throw new Exception("DAE-00313 Begin-envoke timeout, control doesn't exist");
            }
            return m_ctrl.BeginInvoke((Func<object>)action.RunProc);
        }

        public object EndInvoke(IAsyncResult async)
        {
            return m_ctrl.EndInvoke(async);
        }

        public IDisposable AddOnCancel(Action cancelAction) { return null; }

        #endregion
    }
}
