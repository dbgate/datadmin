using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public interface ICancelable
    {
        void Cancel();
        bool CanCancel { get; }
    }

    public interface IStandaloneAsyncResult : IAsyncResult
    {
        object EndInvoke();
    }

    public interface IInvokableAction
    {
        object RunProc();
        bool CanCancel { get; }
        void Cancel();
    }

    public interface IInvoker
    {
        IAsyncResult BeginInvoke(PriorityLevel priority, bool behaveAsStack, IInvokableAction action, AsyncCallback callback);
        object EndInvoke(IAsyncResult async);
        bool IsInInvokerThread { get;}
        IDisposable AddOnCancel(Action cancelAction);
    }
}
