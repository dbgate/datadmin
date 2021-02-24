using System;
using System.Collections.Generic;
using System.Web;
using keygenlib;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Xml;

public static class SendTool
{
    public static void SendLicense(string name, string email, string targetEmail, string text, string product)
    {
        LicenseData licdata = new LicenseData();
        string license = LicenseTool.CreateLicense(name, email, "name", product, null, licdata);
        MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(license));
        SmtpClient client = new SmtpClient("mail.dzavy.net");
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential("mail@datadmin.com", "kijokGawg9");

        XmlElement prod = LicenseTool.GetProductXml("name", product);
        MailMessage message = new MailMessage("sales@datadmin.com", targetEmail, String.Format("DatAdmin License - {0}", prod.GetAttribute("text")), text);
        Attachment attach = new Attachment(ms, "datadmin.license", "application/octet-stream");
        message.Attachments.Add(attach);
        client.Send(message);

        ms.Position = 0;
        string copyInfo = String.Format("Name: {0}\r\nE-mail: {1}\r\nProduct: {2}\r\nLICENSE:\r\n{3}\r\n\r\n", name, email, prod.GetAttribute("text"), licdata.LicenseXml);
        MailMessage copyMessage = new MailMessage("sales@datadmin.com", "mail@datadmin.com", String.Format("DatAdmin License COPY - {0}", prod.GetAttribute("text")), copyInfo + text);
        copyMessage.Attachments.Add(attach);
        client.Send(copyMessage);
    }
}
