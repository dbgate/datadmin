using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace DatAdmin
{
    public static class GetEvalCode
    {
        public static bool GetLicense(string name, string email, string product)
        {
            var desc = ApiDescriptor.GetInstance();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(desc.RequestLicense);
            req.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            req.Method = "POST";

            Dictionary<string, string> pars = new Dictionary<string, string>();
            FeedbackTool.FillStdParams(pars, false);
            pars["NAME"] = name;
            pars["EMAIL"] = email;
            pars["PRODUCT"] = product;

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
                        string licdata = reader.ReadToEnd();
                        if (LicenseTool.LoadLicenseFromString(licdata) == null) return false;
                        using (var fw = new StreamWriter(Path.Combine(Framework.LicensesDirectory, Guid.NewGuid().ToString())))
                        {
                            fw.Write(licdata);
                            return true;
                        }
                    }
                }
            }
        }
    }
}
