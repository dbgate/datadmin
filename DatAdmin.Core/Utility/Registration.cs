using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml;

namespace DatAdmin
{
    //public class Registration
    //{
    //    public static Registration Instance;
    //    [XmlElem("Name")]
    //    public string Name { get; set; }
    //    [XmlElem("Email")]
    //    public string Email { get; set; }
    //    public string RegCode;
    //    public SoftwareEdition Edition;
    //    public DateTime? ValidTo;

    //    public static SoftwareEdition SoftwareEdition
    //    {
    //        get
    //        {
    //            if (Instance != null)
    //            {
    //                return Instance.Edition;
    //            }
    //            else
    //            {
    //                return SoftwareEdition.Personal;
    //            }
    //        }
    //    }

    //    public Registration(XmlElement xml)
    //    {
    //        this.LoadProperties(xml);
    //        RegCode = XmlTool.SafeDecodeString(xml.FindElement("RegCode").InnerText, RegisterTool.SafeMachineName());
    //        Check();
    //    }

    //    public Registration(string name, string email, string regcode)
    //    {
    //        Name = name;
    //        RegCode = regcode.ToUpper().Replace(" ", "").Replace("-", "");
    //        Email = email;
    //        Check();
    //    }

    //    public static string EditionTitle
    //    {
    //        get
    //        {
    //            switch (SoftwareEdition)
    //            {
    //                case SoftwareEdition.Evaluation: return "Evaluation";
    //                case SoftwareEdition.Personal: return "Personal";
    //                case SoftwareEdition.Professional: return "Professional";
    //                case SoftwareEdition.Ultimate: return "Ultimate";
    //            }
    //            throw new Exception("invalid");
    //        }
    //    }

    //    private void Check()
    //    {
    //        RegistrationResult res = RegisterTool.CheckRegistrationKey(Email, RegCode);
    //        if (res == null) throw new Exception("Invalid registration");
    //        if (res.ValidTo != null && DateTime.Now > res.ValidTo) throw new Exception("Invalid registration");
    //        ValidTo = res.ValidTo;
    //        Edition = res.Edition;
    //        //Registration newreg = RegisterTool.CreateRegistration(Name, Email, Edition);
    //        //if (newreg.Code != Code || newreg.EvalCode != EvalCode) throw new Exception("Bad registration");
    //        //if (Edition == SoftwareEdition.Evaluation && DateTime.Now > DateTime.Parse(XmlTool.SafeDecodeString(ExtInfo), CultureInfo.InvariantCulture)) throw new Exception("Invalid registration");
    //    }

    //    public static void Load()
    //    {
    //        string file = Path.Combine(Core.ConfigDirectory, "register.xml");
    //        try
    //        {
    //            XmlDocument doc = new XmlDocument();
    //            doc.Load(file);
    //            Instance = new Registration(doc.DocumentElement);
    //        }
    //        catch (Exception)
    //        {
    //            Instance = null;
    //        }
    //    }

    //    public static DateTime? EditionValidTo
    //    {
    //        get
    //        {
    //            try
    //            {
    //                if (Instance == null) return null;
    //                if (Instance.Edition != SoftwareEdition.Evaluation) return null;
    //                return Instance.ValidTo;
    //            }
    //            catch (Exception)
    //            {
    //                return null;
    //            }
    //        }
    //    }


    //    public static bool TryCheckEdition(SoftwareEdition edition, string logmsg)
    //    {
    //        if (SoftwareEdition < edition)
    //        {
    //            Logging.Warning("Edition error: " + logmsg);
    //            Errors.Report(new BadEditionError(edition));
    //            return false;
    //        }
    //        return true;
    //    }

    //    public void Save()
    //    {
    //        string file = Path.Combine(Core.ConfigDirectory, "register.xml");
    //        try
    //        {
    //            XmlDocument doc = XmlTool.CreateDocument("Registration");
    //            SaveToXml(doc.DocumentElement);
    //            doc.Save(file);
    //        }
    //        catch (Exception)
    //        {
    //            Instance = null;
    //        }
    //    }

    //    private void SaveToXml(XmlElement xml)
    //    {
    //        this.SaveProperties(xml);
    //        xml.AddChild("RegCode").InnerText = XmlTool.SafeEncodeString(RegCode, RegisterTool.SafeMachineName());
    //    }
    //}
    //public static class SoftwareEditionExtension
    //{
    //    public static bool IsPurchased(this SoftwareEdition edition)
    //    {
    //        return edition == SoftwareEdition.Professional || edition == SoftwareEdition.Ultimate;
    //    }
    //}
}
