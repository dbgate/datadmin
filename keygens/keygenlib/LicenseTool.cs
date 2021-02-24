using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Globalization;
using AlpineSoft;
using System.Security.Cryptography;

namespace keygenlib
{
    public class LicenseData
    {
        public string LicenseXml;
    }

    public static class LicenseTool
    {
        private static byte[] XOR_VALS = new byte[] { 156, 39, 221, 180, 17 };

        public static XmlElement GetProductXml(string productattr, string productval)
        {
            var prods = LoadProducts();
            var prod = (XmlElement)prods.SelectSingleNode(String.Format("//Product[@{0}='{1}']", productattr, productval));
            if (prod == null) throw new Exception(String.Format("Product {0}={1} not defined", productattr, productval));
            if (!String.IsNullOrEmpty(prod.GetAttribute("use")))
            {
                foreach (XmlElement childelem in prods.DocumentElement.SelectNodes(prod.GetAttribute("use")))
                {
                    foreach (XmlElement feat in childelem.SelectNodes("Feature"))
                    {
                        prod.AppendChild(feat.CloneNode(true));
                    }
                }
            }
            return prod;
        }

        private static XmlDocument CreateNotSignedLicenseDoc(string name, string email, string productattr, string productval, string licid)
        {
            if (licid == null) licid = Guid.NewGuid().ToString();
            var res = new XmlDocument();
            res.AppendChild(res.CreateElement("License"));
            var prod = GetProductXml(productattr, productval);
            foreach (XmlElement feat in prod.SelectNodes("Feature"))
            {
                var f2 = res.CreateElement("Feature");
                f2.SetAttribute("name", feat.GetAttribute("name"));
                res.DocumentElement.AppendChild(f2);
            }
            AddTextElement(res.DocumentElement, "Name", name);
            AddTextElement(res.DocumentElement, "Email", email);
            AddTextElement(res.DocumentElement, "ActiveTo", LoadTimeTo(prod.SelectSingleNode("Active")));
            AddTextElement(res.DocumentElement, "UpdatesTo", LoadTimeTo(prod.SelectSingleNode("Updates")));
            AddTextElement(res.DocumentElement, "SupportTo", LoadTimeTo(prod.SelectSingleNode("Support")));
            res.DocumentElement.SetAttribute("name", prod.GetAttribute("name"));
            res.DocumentElement.SetAttribute("text", prod.GetAttribute("text"));
            res.DocumentElement.SetAttribute("licid", licid);
            if (String.IsNullOrEmpty(prod.GetAttribute("longtext")))
            {
                res.DocumentElement.SetAttribute("longtext", prod.GetAttribute("text"));
            }
            else
            {
                res.DocumentElement.SetAttribute("longtext", prod.GetAttribute("longtext"));
            }
            res.DocumentElement.SetAttribute("text", prod.GetAttribute("text"));
            res.DocumentElement.SetAttribute("hides", prod.GetAttribute("hides"));
            res.DocumentElement.SetAttribute("hidepurchaselinks", prod.GetAttribute("hidepurchaselinks"));
            return res;
        }

        private static byte[] CreateLicenseBytes(string name, string email, string productattr, string productval, string licid, LicenseData licdata)
        {
            var doc = CreateNotSignedLicenseDoc(name, email, productattr, productval, licid);
            var ms = new MemoryStream();
            using (var xw = XmlWriter.Create(ms))
            {
                doc.Save(xw);
            }
            if (licdata != null)
            {
                var sw = new StringWriter();
                doc.Save(sw);
                licdata.LicenseXml = sw.ToString();
            }
            byte[] data = ms.ToArray();

            // sign binary representation of license
            var key = new EZRSA(1024);
            key.FromXmlString(Resources.privatekey);
            byte[] signature = key.SignData(data, new SHA1CryptoServiceProvider());

            var mscmp = new MemoryStream();
            using (var bw = new BinaryWriter(mscmp))
            {
                bw.Write((int)signature.Length);
                bw.Write(signature);
                bw.Write((int)data.Length);
                bw.Write(data);
            }

            byte[] ar = mscmp.ToArray();

            unchecked
            {
                for (int i = 0; i < ar.Length; i++)
                {
                    ar[i] = (byte)(ar[i] ^ XOR_VALS[i % XOR_VALS.Length]);
                }
            }
            return ar;
        }

        public static string CreateLicense(string name, string email, string productattr, string productval, string licid, LicenseData licdata)
        {
            return Convert.ToBase64String(CreateLicenseBytes(name, email, productattr, productval, licid, licdata));
        }

        private static void AddTextElement(XmlElement parent, string elemName, DateTime value)
        {
            value = new DateTime(value.Year, value.Month, value.Day, 23, 59, 59);
            AddTextElement(parent, elemName, value.ToString("s", CultureInfo.InvariantCulture));
        }

        private static void AddTextElement(XmlElement parent, string elemName, string value)
        {
            var elem = parent.OwnerDocument.CreateElement(elemName);
            elem.InnerText = value;
            parent.AppendChild(elem);
        }

        private static DateTime LoadTimeTo(XmlNode xmlnode)
        {
            var xml = xmlnode as XmlElement;
            if (xml == null) return DateTime.MinValue;
            if (xml.GetAttribute("unit") == "day") return DateTime.UtcNow + TimeSpan.FromDays(Int32.Parse(xml.GetAttribute("count")));
            if (xml.GetAttribute("unit") == "month") return DateTime.UtcNow + TimeSpan.FromDays(31 * Int32.Parse(xml.GetAttribute("count")));
            if (xml.GetAttribute("unit") == "year") return DateTime.UtcNow + TimeSpan.FromDays(365 * Int32.Parse(xml.GetAttribute("count")));
            if (xml.GetAttribute("forever") == "1") return DateTime.MaxValue;
            return DateTime.MinValue;
        }

        public static XmlDocument LoadProducts()
        {
            var prods = new XmlDocument();
            prods.LoadXml(Resources.products);
            return prods;
        }
    }
}
