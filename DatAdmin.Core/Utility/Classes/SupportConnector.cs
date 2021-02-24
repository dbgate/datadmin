using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Collections;

namespace DatAdmin
{
    public static class SupportConnector
    {
        private static void OpenSupportUrl(string path, string addpars)
        {
            string url = String.Format("http://www.datadmin.{0}?name={1}&email={2}&version={3}&osversion={4}&edition={5}",
                path, HttpUtility.UrlEncode(LicenseTool.RegisteredToUser1()), HttpUtility.UrlEncode(LicenseTool.RegEmail1()),
                HttpUtility.UrlEncode(VersionInfo.VERSION), HttpUtility.UrlEncode(FeedbackTool.OSVersion()), HttpUtility.UrlEncode(LicenseTool.EditionText()));
            if (addpars != null) url += addpars;
            System.Diagnostics.Process.Start(url);
        }

        public static void SupportRequest()
        {
            if (Texts.Language == "cz") OpenSupportUrl("cz/podpora-kontakt", null);
            else OpenSupportUrl("com/supportrequest", null);
        }

        public static void SendFeedback()
        {
            OpenSupportUrl("com/sendfeedback", null);
        }

        public static void OpenErrorSupport(Exception err)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Message:" + err.Message);
            sb.AppendLine("Type:" + err.GetType().FullName);
            sb.AppendLine("Stack trace:" + err.StackTrace);
            foreach (DictionaryEntry item in err.Data)
            {
                sb.AppendLine(item.Key.ToString() + "=" + item.Value.SafeToString() ?? "(null)");
            }
            string addinfo = "&addinfo=" + HttpUtility.UrlEncode(sb.ToString());
            if (Texts.Language == "cz") OpenSupportUrl("cz/podpora-kontakt", addinfo);
            else OpenSupportUrl("com/supportrequest", addinfo);
        }
    }
}
