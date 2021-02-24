using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;

namespace DatAdmin
{
    public static class ErrorSendThread
    {
        private static void DoSendError(Exception error, List<LogMessageRecord> logHistory, Bitmap screenshot)
        {
            try
            {
                var desc = ApiDescriptor.GetInstance();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(desc.SendError);
                req.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
                req.Method = "POST";

                XmlDocument doc = XmlTool.CreateDocument("Error");
                XmlElement xml = doc.DocumentElement;

                xml.SetAttribute("version", VersionInfo.VERSION);
                xml.AddChild("Message").InnerText = error.Message;
                xml.AddChild("Type").InnerText = error.GetType().FullName;
                xml.AddChild("StackTrace").InnerText = error.StackTrace;
                //xml.AddChild("DataTree").InnerText = GetDataTree();
                Exception se = error;
                while (se != null)
                {
                    XmlTool.SaveParameters(xml, se.Data);
                    se = se.InnerException;
                }

                xml.AddChild("Text").InnerText = error.ToString();

                //if (logHistory != null && CheckAutoUpdate.SendErrorLogs) Logging.SaveLogs(logHistory, xml.AddChild("LogHistory"));

                Dictionary<string, string> pars = new Dictionary<string, string>();
                StringWriter sw = new StringWriter();
                doc.Save(sw);
                pars["DATA"] = sw.ToString();
                //if (screenshot != null && CheckAutoUpdate.SendErrorScreenshots) pars["SCREENSHOT"] = SerializeScreenshot(screenshot);
                //if (CheckAutoUpdate.UpdateID != null) pars["PROGRAMUPDATE"] = CheckAutoUpdate.UpdateID.ToString();
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
                Errors.ReportSilent(err, false);
            }
        }

        //private static string SerializeScreenshot(Bitmap screenshot)
        //{
        //    using (var ms = new MemoryStream())
        //    {
        //        var img = screenshot.GetScaledInstance(CheckAutoUpdate.ScreenshotWidth, (int)(CheckAutoUpdate.ScreenshotWidth * screenshot.Height / (float)screenshot.Width), PixelFormat.Format16bppRgb565);
        //        img.Save(ms, ImageFormat.Png);
        //        return Convert.ToBase64String(ms.ToArray());
        //    }
        //}

        //private static string GetDataTree()
        //{
        //    var res = new List<string>();
        //    foreach (string fn in Directory.GetFiles(Framework.AppDataDirectory, "*.*", SearchOption.AllDirectories))
        //    {
        //        if (fn.ToLower().StartsWith(Framework.DataSamplesDirectory.ToLower())) continue;
        //        string add = "";
        //        if (fn.ToLower().EndsWith(".con"))
        //        {
        //            try
        //            {
        //                var doc = new XmlDocument();
        //                doc.Load(fn);
        //                add = " : " + doc.DocumentElement.GetAttribute("type");
        //                var modex = doc.SelectSingleNode("//DatabaseMode");
        //                if (modex != null) add += " " + modex.InnerText;
        //            }
        //            catch
        //            {
        //                add = "";
        //            }
        //        }
        //        res.Add(IOTool.RelativePathTo(Framework.AppDataDirectory, fn) + add);
        //    }
        //    return res.CreateDelimitedText("\n");
        //}

        public static void SendError(Exception error, List<LogMessageRecord> logHistory, Bitmap screenshot)
        {
            // send asyncrhonous so that user is not forced to wait
            new Thread(() => DoSendError(error, logHistory, screenshot)).Start();
        }
    }
}
