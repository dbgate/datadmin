using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DatAdmin
{
    public interface IFrameworkMainWindow
    {
        void UpdateTitle();

        void RunInMainWindow(Action callback);
        void SendVersionInfo(string text, string newVersion);

        bool ProcessRefreshMessage();

        IInvoker Invoker { get; }
        void CloseMainWindow();
        Bitmap TakeScreenshot();
    }
}
