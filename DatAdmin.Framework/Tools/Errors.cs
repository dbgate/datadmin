using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace DatAdmin
{
    public class ReportErrorEventArgs : EventArgs
    {
        public Exception Error;
    }

    public delegate void ReportErrorDelegate(object sender, ReportErrorEventArgs e);

    public static class Errors
    {
        public static Exception ExtractImportantException(Exception err)
        {
            while (err is TargetInvocationException && err.InnerException != null) err = err.InnerException;
            return err;
        }

        public static void ReportSilent(Exception e) { ReportSilent(e, true); }

        public static void ReportSilent(Exception e, bool allowSend)
        {
            LogError(e, LogLevel.Error);
            if (allowSend && Framework.Instance.AllowSendErrorReports())
            {
                ErrorSendThread.SendError(ExtractImportantException(e), Logging.GetFeedbackLastLogEntries(), Framework.MainWindow.TakeScreenshot());
            }
        }

        public static void ShowErrorWindow(Exception e)
        {
            e = ExtractImportantException(e);
            if (e is BadSettingsError) BadSettingsErrorForm.Run((BadSettingsError)e);
            else if (e is ExpectedError) StdDialog.ShowError(e.Message);
            else ErrorForm.Run(e, Logging.GetFeedbackLastLogEntries());
        }

        public static void Report(Exception error)
        {
            LogError(error, LogLevel.Error);

            if (AsyncTool.IsMainThread)
            {
                ShowErrorWindow(error);
            }
        }
        private static void LogError(Exception error, LogLevel level)
        {
            LogError(Logging.Root, error, level);
        }
        public static void LogError(Exception error)
        {
            LogError(error, LogLevel.Error);
        }
        public static void LogError(this ILogger logger, Exception error)
        {
            LogError(logger, error, LogLevel.Error);
        }
        public static void LogError(this ILogger logger, Exception error, LogLevel level)
        {
            bool inner = false;
            while (error != null)
            {
                string fmt = inner ? "Inner exception: {0}" : "{0}";
                LogMessageRecord rec = new LogMessageRecord(LogLevel.Error, String.Format(fmt, error.Message));
                rec.Detail = error.StackTrace;
                logger.LogMessage(rec);
                error = error.InnerException;
                inner = true;
            }
        }
        public static void Assert(bool cond)
        {
            if (!cond) throw new InternalError("DAE-00069 Assertion failed", null);
        }
        public static void Assert(bool cond, string message)
        {
            if (!cond) throw new InternalError(message, null);
        }

        public static void Internal(string msg)
        {
            throw new InternalError(msg, null);
        }
        public static void Internal()
        {
            throw new InternalError("DAE-00070 Internal error", null);
        }

        public static string ExtractMessage(Exception e)
        {
            return ExtractImportantException(e).Message;
        }

        public static void CheckNotNull(string errcode, object obj)
        {
            if (obj == null) throw new InternalError("Internal error " + errcode);
        }
    }
}
