using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.IO;

namespace DatAdmin
{
    public partial class ConfigSendForm : FormEx
    {
        IVirtualFileSystem m_fs;

        public ConfigSendForm(IVirtualFileSystem fs)
        {
            InitializeComponent();

            m_fs = fs;
            configSelectionFrame1.FileSystem = m_fs;
        }

        public static bool Run(IVirtualFileSystem fs)
        {
            var win = new ConfigSendForm(fs);
            return win.ShowDialogEx() == DialogResult.OK;
        }

        private void Check()
        {
            var cfg = GlobalSettings.Pages.Email();
            cfg.CheckSettings();
            foreach (string email in emailToFrame1.EmailTo.Split(';'))
            {
                if (!IncorrectEmailError.IsValid(email))
                {
                    throw new IncorrectEmailError("DAE-00161", email);
                }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                Check();
            }
            catch (Exception err)
            {
                Errors.Report(err);
                return;
            }

            var cfg = GlobalSettings.Pages.Email();
            MailMessage mail = new MailMessage();
            mail.From = cfg.GetFromAddress();
            foreach (string addr in emailToFrame1.EmailTo.Split(';')) mail.To.Add(addr);
            mail.Subject = tbxSubject.Text;
            mail.Body = tbxBody.Text;
            mail.IsBodyHtml = false;

            string fn = Core.GetTempFile(".dca");
            using (var zipfs = new ZipFileSystem(fn))
            {
                configSelectionFrame1.Root.CopyCheckedTo(zipfs, true, null);
                zipfs.Flush();
            }

            using (var dcafr = new FileInfo(fn).OpenRead())
            {
                var att = new Attachment(dcafr, "configuration.dca");
                mail.Attachments.Add(att);

                if (chbAddHowTo.Checked)
                {
                    var ms = new MemoryStream();
                    var sw = new StreamWriter(ms);
                    sw.Write(CoreRes.sendconfig_howto);
                    sw.Flush();
                    ms.Position = 0;
                    var atthowto = new Attachment(ms, "howto.html");
                    mail.Attachments.Add(atthowto);
                }

                SmtpClient smtp = cfg.GetClient();
                Logging.Info("Sending mail to " + emailToFrame1.EmailTo);
                smtp.Send(mail);
            }

            File.Delete(fn);
            Close();
            StdDialog.ShowInfo("s_configuration_sent");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string tfile = Core.GetTempFile(".html");
            using (var sw = new StreamWriter(tfile))
            {
                sw.Write(CoreRes.sendconfig_howto);
            }
            System.Diagnostics.Process.Start(tfile);
        }
    }
}
