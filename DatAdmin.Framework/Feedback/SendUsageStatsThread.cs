using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Net;
using System.Xml;

namespace DatAdmin
{
    public static class SendUsageStatsThread
    {
        //public static void DoSendUsage()
        //{
        //    foreach (string file in Directory.GetFiles(Core.UsageDirectory))
        //    {
        //        if (GlobalSettings.Pages.General.AllowUploadUsageStats)
        //        {
        //            UploadUsageFile(file);
        //        }
        //        File.Delete(file);
        //    }
        //}

        //public static void UploadUsageFile(string file)
        //{
        //    using (StreamReader sr = new StreamReader(file))
        //    {
        //        UploadUsageData(sr.ReadToEnd());
        //    }
        //}

        //public static void UploadUsageData(string usageData)
        //{
        //    try
        //    {
        //        var desc = ApiDescriptor.GetInstance();
        //        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(desc.SendUsage);
        //        req.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
        //        req.Method = "POST";

        //        Dictionary<string, string> pars = new Dictionary<string, string>();
        //        pars["DATA"] = usageData;
        //        FeedbackTool.FillStdParams(pars);

        //        string pars_enc = StringTool.UrlEncode(pars, Encoding.UTF8);
        //        byte[] data = Encoding.UTF8.GetBytes(pars_enc);
        //        req.ContentLength = data.Length;

        //        using (Stream fw = req.GetRequestStream())
        //        {
        //            fw.Write(data, 0, data.Length);
        //        }
        //        using (var resp = req.GetResponse())
        //        {
        //            using (Stream fr = resp.GetResponseStream())
        //            {
        //                using (StreamReader reader = new StreamReader(fr))
        //                {
        //                    string line = reader.ReadToEnd();
        //                    Logging.Debug("Read result from datadmin feedback:" + line);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        Errors.ReportSilent(err);
        //    }
        //}

        private static void DoSendUsage()
        {
            try
            {
                var desc = ApiDescriptor.GetInstance();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(desc.SendUsage);
                req.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
                req.Method = "POST";

                Dictionary<string, string> pars = new Dictionary<string, string>();
                FeedbackTool.FillStdParams(pars, true);

                string pars_enc = StringTool.UrlEncode(pars, Encoding.UTF8);
                byte[] data = Encoding.UTF8.GetBytes(pars_enc);
                req.ContentLength = data.Length;

                using (Stream fw = req.GetRequestStream())
                {
                    fw.Write(data, 0, data.Length);
                }
                using (var resp = req.GetResponse())
                {
                    using (Stream fr = resp.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(fr))
                        {
                            string line = reader.ReadToEnd();
                            Logging.Debug("Read result from datadmin feedback:" + line);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Errors.ReportSilent(err);
            }
        }

        public static void SendUsage()
        {
            if (!Framework.Instance.AllowSendUsageStats()) return;
            new Thread(DoSendUsage).Start();
        }
    }
}
