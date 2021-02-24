using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Linq;
using System.Globalization;
using AlpineSoft;
using System.Security.Cryptography;

namespace DatAdmin
{
    public class License
    {
        public string Name;
        public string Text;
        public string LongText;
        public string UserEmail;
        public string UserName;
        public string Identifier;
        public List<string> Hides;
        public DateTime ActiveTo;
        public DateTime UpdatesTo;
        public DateTime SupportTo;
        public HashSetEx<string> Features = new HashSetEx<string>();
        public string Filename { get; private set; }
        public bool HidePurchaseLinks;
        public XmlElement SourceXml;

        public License(XmlElement xml, string filename)
        {
            ActiveTo = ReadValidity(xml, "ActiveTo");
            SupportTo = ReadValidity(xml, "SupportTo");
            UpdatesTo = ReadValidity(xml, "UpdatesTo");
            HidePurchaseLinks = xml.GetAttribute("hidepurchaselinks") == "1";
            Name = xml.GetAttribute("name");
            Text = xml.GetAttribute("text");
            LongText = xml.GetAttribute("longtext");
            UserName = xml.GetTextContent("Name");
            UserEmail = xml.GetTextContent("Email");
            Identifier = xml.GetAttribute("licid");
            Hides = new List<string>(xml.GetAttribute("hides").Split(','));
            foreach (XmlElement x in xml.SelectNodes("Feature"))
            {
                Features.Add(x.GetAttribute("name"));
            }
            Filename = filename;
            SourceXml = xml;
        }

        private static DateTime ReadValidity(XmlElement parent, string tag)
        {
            var node = parent.SelectSingleNode(tag);
            if (node == null) return DateTime.UtcNow;
            return DateTime.ParseExact(node.InnerText, "s", CultureInfo.InvariantCulture);
        }

        public bool IsValid()
        {
            if (DateTime.UtcNow > ActiveTo) return false;
            if (VersionInfo.BuildAt > UpdatesTo) return false;
            return true;
        }

        public bool FeatureAllowed(string features)
        {
            return LicenseTool._FeatureAllowed(features, Features);
        }

        //public void SaveToFeedbackXml(XmlElement xml)
        //{
        //    xml.SetAttribute("licid", Identifier);
        //    xml.AddChild("Name").InnerText = Name;
        //    xml.AddChild("Text").InnerText = Text;
        //    xml.AddChild("ActiveTo").InnerText = ActiveTo.ToString("s");
        //    xml.AddChild("UpdatesTo").InnerText = UpdatesTo.ToString("s");
        //    xml.AddChild("SupportTo").InnerText = SupportTo.ToString("s");
        //    xml.AddChild("UserName").InnerText = UserName;
        //    xml.AddChild("UserEmail").InnerText = UserEmail;
        //    xml.AddChild("Features").InnerText = Features.CreateDelimitedText(",");
        // }
    }

    public static class LicenseTool
    {
        private static byte[] XOR_VALS = new byte[] { 156, 39, 221, 180, 17 };
        public static List<License> ValidLicenses = new List<License>();
        public static List<License> InvalidLicenses = new List<License>();
        public static HashSetEx<string> Features = new HashSetEx<string>();
        static HashSetEx<string> HiddenLicenses = new HashSetEx<string>();

        private static XmlDocument LoadLicenseXmlFromString(string encoded)
        {
            try
            {
                byte[] ar = Convert.FromBase64String(encoded);

                unchecked
                {
                    for (int i = 0; i < ar.Length; i++)
                    {
                        ar[i] = (byte)(ar[i] ^ XOR_VALS[i % XOR_VALS.Length]);
                    }
                }

                var mscmp = new MemoryStream(ar);
                using (var br = new BinaryReader(mscmp))
                {
                    int siglen = br.ReadInt32();
                    if (siglen <= 0 || siglen > 100000) return null;
                    byte[] signature = br.ReadBytes(siglen);
                    int datalen = br.ReadInt32();
                    if (datalen <= 0 || datalen > 100000) return null;
                    byte[] data = br.ReadBytes(datalen);

                    // check signature
                    var key = new EZRSA(1024);
                    key.FromXmlString(Framework.Instance.GetPublicKey());
                    if (!key.VerifyData(data, new SHA1CryptoServiceProvider(), signature)) return null;

                    var ms = new MemoryStream(data);
                    var doc = new XmlDocument();
                    doc.Load(ms);
                    return doc;
                }
            }
            catch
            {
                return null;
            }
        }

        static Dictionary<string, IFeature> m_features = new Dictionary<string, IFeature>();
        private static IFeature GetFeature(string name)
        {
            lock (m_features)
            {
                if (m_features.ContainsKey(name)) return m_features[name];
                var holder = FeatureAddonType.Instance.FindHolder(name);
                if (holder != null)
                {
                    m_features[name] = (IFeature)holder.InstanceModel;
                    return (IFeature)holder.InstanceModel;
                }
                return null;
            }
        }

        private static XmlDocument LoadLicenseXml(string file)
        {
            try
            {
                using (var fr = new StreamReader(file))
                {
                    string encoded = fr.ReadToEnd();
                    return LoadLicenseXmlFromString(encoded);
                }
            }
            catch
            {
                return null;
            }
        }

        public static License LoadLicenseFromString(string data)
        {
            var doc = LoadLicenseXmlFromString(data);
            if (doc == null) return null;
            var lic = new License(doc.DocumentElement, "datadmin.license");
            return lic;
        }

        public static License LoadLicense(string file)
        {
            var doc = LoadLicenseXml(file);
            if (doc == null) return null;
            var lic = new License(doc.DocumentElement, file);
            return lic;
        }

        public static void AddLicense(License lic)
        {
            if (lic.IsValid())
            {
                ValidLicenses.Add(lic);
                Features.AddRange(lic.Features);
                HiddenLicenses.AddRange(lic.Hides);
            }
            else
            {
                InvalidLicenses.Add(lic);
            }
        }

        public static void ReloadLicenses()
        {
            ValidLicenses.Clear();
            InvalidLicenses.Clear();
            Features.Clear();
            HiddenLicenses.Clear();
            Framework.Instance.AddFixedLicenses();
            if (!VersionInfo.DenyCustomLicenses)
            {
                foreach (string fn in Directory.GetFiles(Framework.LicensesDirectory))
                {
                    var lic = LoadLicense(fn);
                    if (lic == null) continue;
                    AddLicense(lic);
                }
            }
            if (Framework.MainWindow != null) Framework.MainWindow.UpdateTitle();
        }

        public static string RegisteredToUser1()
        {
            foreach (var lic in ValidLicenses)
            {
                if (HiddenLicenses.Contains(lic.Name)) continue;
                if (String.IsNullOrEmpty(lic.UserName)) continue;
                return lic.UserName;
            }
            return null;
        }

        public static string RegisteredToUser()
        {
            var res = new List<string>();
            foreach (var lic in ValidLicenses)
            {
                if (HiddenLicenses.Contains(lic.Name)) continue;
                if (String.IsNullOrEmpty(lic.UserName)) continue;
                if (res.Contains(lic.UserName)) continue;
                res.Add(lic.UserName);
            }
            return String.Join(",", res.ToArray());
        }

        public static string EditionText()
        {
            if (VersionInfo.DenyCustomLicenses) return "";
            var res = new List<string>();
            foreach (var lic in ValidLicenses)
            {
                if (HiddenLicenses.Contains(lic.Name)) continue;
                res.Add(lic.Text);
            }
            return String.Join("+", res.ToArray());
        }

        public static string RegEmails()
        {
            var res = new List<string>();
            foreach (var lic in ValidLicenses)
            {
                if (HiddenLicenses.Contains(lic.Name)) continue;
                if (String.IsNullOrEmpty(lic.UserEmail)) continue;
                if (res.Contains(lic.UserName)) continue;
                res.Add(lic.UserEmail);
            }
            return String.Join(";", res.ToArray());
        }

        public static string RegEmail1()
        {
            foreach (var lic in ValidLicenses)
            {
                if (HiddenLicenses.Contains(lic.Name)) continue;
                if (String.IsNullOrEmpty(lic.UserEmail)) continue;
                return lic.UserEmail;
            }
            return null;
        }

        internal static bool _FeatureAllowed(string features, HashSetEx<string> testedFeatures)
        {
            if (testedFeatures.Contains("all")) return true;
            if (String.IsNullOrEmpty(features)) return true;
            foreach (string item in features.Split('|'))
            {
                if (testedFeatures.Contains(item)) return true;
                var ifeat = GetFeature(item);
                if (ifeat != null && ifeat.AllwaysAllowed) return true;
            }
            return false;
        }

        public static bool FeatureAllowed(string features)
        {
            return _FeatureAllowed(features, Features);
        }

        public static void InstallLicense(string filename)
        {
            string dstfn = Path.Combine(Framework.LicensesDirectory, Guid.NewGuid().ToString());
            File.Copy(filename, dstfn);
            ReloadLicenses();
        }

        public static bool FeatureAllowedMsg(string feature)
        {
            if (!FeatureAllowed(feature))
            {
                Logging.Warning("Edition error: " + feature);
                Errors.Report(new MissingFeatureError(feature));
                return false;
            }
            return true;
        }

        public static string FormatLicenceDate(DateTime dt)
        {
            if (dt.Year < 1000) return Texts.Get("s_never");
            if (dt.Year > 3000) return Texts.Get("s_unlimited");
            return dt.ToString("d");
        }

        public static bool HidePurchaseLinks()
        {
            if (VersionInfo.HidePurchaseLinks) return true;
            foreach (var lic in ValidLicenses) if (lic.HidePurchaseLinks) return true;
            return false;
        }

        public static bool ContainsProduct(string name, bool testInvalid)
        {
            if (ValidLicenses.Exists(e => e.Name == name)) return true;
            if (testInvalid && InvalidLicenses.Exists(e => e.Name == name)) return true;
            return false;
        }

        private static bool m_licensesSent;
        public static string GetFeedbackLicenseInfo()
        {
            if (m_licensesSent) return null;
            var doc = XmlTool.CreateDocument("Licenses");
            foreach (var lic in ValidLicenses)
            {
                doc.DocumentElement.AppendChild(doc.ImportNode(lic.SourceXml, true));
            }
            m_licensesSent = true;
            return doc.GetPackedDocumentXml();
        }
    }
}
