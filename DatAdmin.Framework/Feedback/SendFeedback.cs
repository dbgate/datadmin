using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

namespace DatAdmin
{
    public static class SendFeedback
    {
        public static bool Send(string subject, string body, bool sendanswer, string email)
        {
            var desc = ApiDescriptor.GetInstance();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(desc.SendFeedback);
            req.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            req.Method = "POST";

            Dictionary<string, string> pars = new Dictionary<string, string>();
            //if (CheckAutoUpdate.UpdateID != null) pars["PROGRAMUPDATE"] = CheckAutoUpdate.UpdateID.ToString();
            FeedbackTool.FillStdParams(pars, false);
            pars["SUBJECT"] = subject;
            pars["BODY"] = body;
            pars["SENDANSWER"] = sendanswer ? "1" : "0";
            pars["EMAIL"] = email;

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
                        var doc = new XmlDocument();
                        doc.Load(reader);
                        string state = doc.DocumentElement.GetAttribute("state");
                        return state == "ok";
                    }
                }
            }
        }
    }
}
