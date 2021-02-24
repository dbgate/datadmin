using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class ProgressInfoExtension
    {
        public static void RaiseErrorEx(this IProgressInfo progress, Exception err, string message, string category)
        {
            string longmsg = String.Format("{0} ({1})", message, err.Message);
            if (category != null)
            {
                progress.LogMessage(category, LogLevel.Error, longmsg);
            }
            Logging.Error("Error: {0}; {1}", message, err);
            if (progress != null)
            {
                progress.RaiseError(longmsg);
            }
            else
            {
                throw new Exception(message, err);
            }
        }
    }
}
