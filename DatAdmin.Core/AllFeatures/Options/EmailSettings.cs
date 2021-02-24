using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Net.Mail;
using System.Net;

namespace DatAdmin
{
    [SettingsPage(Name = "email", Title = "s_email", Targets = SettingsTargets.Global, ImageName = CoreIcons.emailName)]
    public class EmailSettings : SettingsPageBase
    {
        public EmailSettings()
        {
            SmtpPort = 25;
            FromAddr = "noreply@datadmin.com";
        }

        [Category("s_email_from")]
        [DisplayName("s_from_address")]
        [SettingsKey("internet.email.from_address")]
        public string FromAddr { get; set; }

        [Category("s_email_from")]
        [DisplayName("s_from_name")]
        [SettingsKey("internet.email.from_name")]
        public string FromName { get; set; }

        [Category("s_smtp_server")]
        [DisplayName("s_server")]
        [SettingsKey("internet.email.smtp_server")]
        public string SmtpServer { get; set; }

        [Category("s_smtp_server")]
        [DisplayName("s_port")]
        [SettingsKey("internet.email.smtp_port")]
        public int SmtpPort { get; set; }

        [Category("s_smtp_server")]
        [DisplayName("s_login")]
        [SettingsKey("internet.email.smtp_login")]
        public string SmtpLogin { get; set; }

        [Category("s_smtp_server")]
        [DisplayName("s_password")]
        [PasswordPropertyText(true)]
        public string SmtpPassword { get; set; }

        [Browsable(false)]
        [SettingsKey("internet.email.smtp_password")]
        public string XmlSmtpPassword
        {
            get { return XmlTool.SafeEncodeString(SmtpPassword); }
            set { SmtpPassword = XmlTool.SafeDecodeString(value); }
        }

        public void CheckSettings()
        {
            try
            {
                var smtp = GetClient();
            }
            catch
            {
                throw new BadSettingsError("DAE-00190 " + Texts.Get("s_incorrect_smtp_configuration"), "s_email");
            }
        }

        public SmtpClient GetClient()
        {
            if (SmtpServer.IsEmpty()) throw new BadSettingsError("DAE-00191 " + Texts.Get("s_incorrect_smtp_configuration"), "s_email");
            var smtp = new SmtpClient(SmtpServer, SmtpPort);
            if (!SmtpLogin.IsEmpty()) smtp.Credentials = new NetworkCredential(SmtpLogin, SmtpPassword);
            return smtp;
        }

        public MailAddress GetFromAddress()
        {
            if (!FromName.IsEmpty()) return new MailAddress(FromAddr, FromName);
            return new MailAddress(FromAddr);
        }
    }

    public static class SettingsPageCollection_EMail
    {
        public static EmailSettings Email(this SettingsPageCollection col)
        {
            return (EmailSettings)col.PageByName("email");
        }
    }
}
