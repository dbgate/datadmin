using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Threading;

namespace DatAdmin
{
    public static class Singleton
    {
        public static string AppDataDirectory
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), VersionInfo.ProgramFolderName); }
        }
        public static string TempDirectory
        {
            get { return Path.Combine(AppDataDirectory, "temp"); }
        }
        public static string IpcDirectory
        {
            get { return Path.Combine(TempDirectory, "ipc"); }
        }

        /// <summary>
        /// Imports 
        /// </summary>

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int IsIconic(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);

        /// <summary>
        /// GetCurrentInstanceWindowHandle
        /// </summary>
        /// <returns></returns>
        private static IntPtr GetCurrentInstanceWindowHandle()
        {
            IntPtr hWnd = IntPtr.Zero;
            System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess();
            string procname = process.ProcessName;
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(procname);
            foreach (System.Diagnostics.Process _process in processes)
            {
                // Get the first instance that is not this instance, has the
                // same process name and was started from the same file name
                // and location. Also check that the process has a valid
                // window handle in this session to filter out other user's
                // processes.
                if (_process.Id != process.Id &&
                    _process.MainWindowHandle != IntPtr.Zero)
                {
                    hWnd = _process.MainWindowHandle;
                    break;
                }
            }
            return hWnd;
        }

        /// <summary>
        /// SwitchToCurrentInstance
        /// </summary>
        public static bool SwitchToCurrentInstance(string[] args)
        {
            IntPtr hWnd = GetCurrentInstanceWindowHandle();
            if (hWnd != IntPtr.Zero)
            {
                // Restore window if minimised. Do not restore if already in
                // normal or maximised window state, since we don't want to
                // change the current state of the window.
                if (IsIconic(hWnd) != 0)
                {
                    ShowWindow(hWnd, SW_RESTORE);
                }

                // Set foreground window.
                SetForegroundWindow(hWnd);

                string fn = Path.Combine(IpcDirectory, "args_" + Guid.NewGuid().ToString());
                using (var sw = new StreamWriter(fn))
                {
                    foreach (string arg in args)
                    {
                        sw.WriteLine(arg);
                    }
                }

                bool ok = PostMessage(hWnd, WM_USER + 1, 0, 0);
                if (!ok)
                {
                    try { File.Delete(fn); }
                    catch { }
                }
                return ok;
            }
            return false;
        }

        const int SW_RESTORE = 9;
        const int WM_USER = 0x0400;
    }
}
