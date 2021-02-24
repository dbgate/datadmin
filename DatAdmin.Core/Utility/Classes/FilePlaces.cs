using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Drawing.Design;
using System.Net.Mail;

namespace DatAdmin
{
    [FilePlace(Name = "filesystem", Title = "s_file_on_disk")]
    public class FilePlaceFileSystem : FilePlaceBase, ICustomPropertyPage
    {
        public string FilePath { get; set; }

        public override int Priority
        {
            get { return -10; }
        }

        public override bool LoadVirtualFile(string virtualFile, IExtendedFileNameHolderInfo holder)
        {
            FilePath = virtualFile;
            return true;
        }

        public override string GetVirtualFile()
        {
            return FilePath;
        }

        public override bool SupportsLoad(IExtendedFileNameHolderInfo info) { return true; } 
        public override bool SupportsSave(IExtendedFileNameHolderInfo info) { return true; }

        public override string GetWorkingFileName()
        {
            return FilePath;
        }

        public override void CheckInput()
        {
            base.CheckInput();
            if (FilePath.IsEmpty()) throw new CheckConfigError("DAE-00275 " + Texts.Get("s_input_file_is_not_defined"));
            if (!File.Exists(FilePath))
            {
                throw new CheckConfigError("DAE-00276 " + Texts.Get("s_input_file_does_not_exist$file", "file", FilePath));
            }
        }

        public override void CheckOutput()
        {
            base.CheckOutput();
            string fn = FilePath;
            IOTool2.CheckOutputFileName(ref fn);
            FilePath = fn;
        }

        public override void DeleteFile()
        {
            File.Delete(FilePath);
        }

        #region ICustomPropertyPage Members

        public Control CreatePropertyPage()
        {
            return new FilePlaceFileSystemFrame(this);
        }

        #endregion
    }

    [FilePlace(Name = "clipboard", Title = "s_clipboard")]
    public class FilePlaceClipboard : TempFilePlaceBase, ICustomPropertyPage
    {
        public override bool LoadVirtualFile(string virtualFile, IExtendedFileNameHolderInfo holder)
        {
            return virtualFile == "@clipboard";
        }

        public override string GetVirtualFile()
        {
            return "@clipboard";
        }

        public override bool SupportsLoad(IExtendedFileNameHolderInfo info) { return true; } 
        public override bool SupportsSave(IExtendedFileNameHolderInfo info) { return true; } 

        public Control CreatePropertyPage()
        {
            return null;
        }

        protected override void PrepareReadFileContent(string file)
        {
            string data = (string)MainWindow.Instance.Invoker.InvokeR<string>(Clipboard.GetText);
            using (StreamWriter sw = new StreamWriter(file))
            {
                sw.Write(data);
            }
        }

        protected override void AfterWriteAction(string file)
        {
            string data;
            using (var sr = new StreamReader(file))
            {
                data = sr.ReadToEnd();
            }
            MainWindow.Instance.Invoker.Invoke1<string>(Clipboard.SetText, data);
        }
    }

    [FilePlace(Name = "web", Title = "s_web", RequiredFeature = ExtendedFilePlacesFeature.Test)]
    public class FilePlaceWeb : TempFilePlaceBase, ICustomPropertyPage
    {
        public string Url { get; set; }

        public override bool LoadVirtualFile(string virtualFile, IExtendedFileNameHolderInfo holder)
        {
            if (virtualFile.StartsWith("@web:"))
            {
                Url = virtualFile.Substring(5);
                return true;
            }
            return false;
        }

        public override string GetVirtualFile()
        {
            return "@web:" + Url;
        }

        public override bool SupportsLoad(IExtendedFileNameHolderInfo info) { return true; } 
        public override bool SupportsSave(IExtendedFileNameHolderInfo info) { return true; } 

        public Control CreatePropertyPage()
        {
            return new FilePlaceWebFrame(this);
        }

        protected override void PrepareReadFileContent(string file)
        {
            var req = WebRequest.Create(Url);
            using (var resp = req.GetResponse())
            {
                using (var fr = resp.GetResponseStream())
                {
                    using (var fw = new FileStream(file, FileMode.Create))
                    {
                        IOTool.CopyStream(fr, fw);
                    }
                }
            }
        }

        protected override void AfterWriteAction(string file)
        {
            var req = WebRequest.Create(Url);
            req.Method = WebRequestMethods.Ftp.UploadFile;
            using (var fw = req.GetRequestStream())
            {
                using (var fr = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    IOTool.CopyStream(fr, fw);
                }
            }
        }
    }

    [FilePlace(Name = "cmd", Title = "s_command_line", RequiredFeature = ExtendedFilePlacesFeature.Test)]
    public class FilePlaceCommandLine : TempFilePlaceBase, ICustomPropertyPage
    {
        public string Application { get; set; }
        public string Arguments { get; set; }

        public override bool LoadVirtualFile(string virtualFile, IExtendedFileNameHolderInfo holder)
        {
            if (virtualFile.StartsWith("@cmd:"))
            {
                string[] ar = virtualFile.Substring(5).Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                if (ar.Length >= 1) Application = ar[0];
                if (ar.Length >= 2) Arguments = ar[1];
                return true;
            }
            return false;
        }

        public override string GetVirtualFile()
        {
            return "cmd:" + Application + "||" + Arguments;
        }

        public override bool SupportsSave(IExtendedFileNameHolderInfo info) { return true; } 

        public Control CreatePropertyPage()
        {
            return new FilePlaceCommandLineFrame(this);
        }

        protected override void PrepareReadFileContent(string file)
        {
            using (StreamWriter sw = new StreamWriter(file))
            {
                sw.Write(Clipboard.GetText());
            }
        }

        protected override void AfterWriteAction(string file)
        {
            using (var sw = new StreamReader(file))
            {
                Clipboard.SetText(sw.ReadToEnd());
            }
        }

        public override void CheckOutput()
        {
            base.CheckOutput();
            if (!File.Exists(Application))
            {
                throw new CheckConfigError("DAE-00277 " + Texts.Get("s_file_does_not_exist$file", "file", Application));
            }
        }
    }

    [FilePlace(Name = "externalapp", Title = "s_open_using_external_application")]
    public class FilePlaceExternalApp : TempFilePlaceBase, ICustomPropertyPage
    {
        public override bool LoadVirtualFile(string virtualFile, IExtendedFileNameHolderInfo holder)
        {
            return virtualFile == "@externalapp";
        }

        public override string GetVirtualFile()
        {
            return "@externalapp";
        }

        public override bool SupportsSave(IExtendedFileNameHolderInfo info) { return true; } 

        public Control CreatePropertyPage()
        {
            return null;
        }

        protected override void AfterWriteAction(string file)
        {
            System.Diagnostics.Process.Start(file);
        }
    }

    [FilePlace(Name = "text", Title = "s_text")]
    public class FilePlaceText : TempFilePlaceBase, ICustomPropertyPage
    {
        public string Text { get; set; }

        public override bool LoadVirtualFile(string virtualFile, IExtendedFileNameHolderInfo holder)
        {
            Errors.CheckNotNull("DAE-00125", virtualFile);
            if (virtualFile.StartsWith("@text:"))
            {
                Text = virtualFile.Substring(6);
                return true;
            }
            return false;
        }

        public override string GetVirtualFile()
        {
            return "@text:" + Text;
        }

        public override bool SupportsLoad(IExtendedFileNameHolderInfo info) { return true; }

        public Control CreatePropertyPage()
        {
            return new FilePlaceTextFrame(this);
        }

        protected override void PrepareReadFileContent(string file)
        {
            using (var sw = new StreamWriter(file))
            {
                sw.Write(Text);
            }
        }
    }


    [FilePlace(Name = "ftp", Title = "FTP", RequiredFeature = ExtendedFilePlacesFeature.Test)]
    public class FilePlaceFtpUpload : TempFilePlaceBase, IPropertyPageWithSerialization
    {
        [DisplayName("s_server")]
        public string Server { get; set; }

        [DisplayName("s_login")]
        public string Login { get; set; }

        [DisplayName("s_password")]
        [PasswordPropertyText(true)]
        public string Password { get; set; }

        [DisplayName("s_path")]
        [Editor(typeof(NameTemplateEditor), typeof(UITypeEditor))]
        public string Path { get; set; }

        [DisplayName("s_passive_mode")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool PassiveMode { get; set; }

        public override bool LoadVirtualFile(string virtualFile, IExtendedFileNameHolderInfo holder)
        {
            if (virtualFile.StartsWith("@ftp:"))
            {
                Server = LoadProp(virtualFile, "host");
                Login = LoadProp(virtualFile, "login");
                Path = LoadProp(virtualFile, "path");
                Password = XmlTool.SafeDecodeString(LoadProp(virtualFile, "pwd"));
                PassiveMode = virtualFile.Contains("[pass/]");
                return true;
            }
            return false;
        }

        public override string GetVirtualFile()
        {
            string res = String.Format("@ftp:[host]{0}[/host][login]{1}[/login][pwd]{2}[/pwd][path]{3}[/path]",
                Server, Login, XmlTool.SafeEncodeString(Password), Path);
            if (PassiveMode) res += "[pass/]";
            return res;
        }

        public override bool SupportsLoad(IExtendedFileNameHolderInfo info)  { return true; } 
        public override bool SupportsSave(IExtendedFileNameHolderInfo info)  { return true; } 

        private FtpWebRequest CreateRequest()
        {
            var req = (FtpWebRequest)FtpWebRequest.Create("ftp://" + Server + "/" + NameTemplateEngine.Eval(Path));
            req.Credentials = new NetworkCredential(Login, Password);
            req.KeepAlive = false;
            req.UsePassive = PassiveMode;
            return req;
        }

        protected override void PrepareReadFileContent(string file)
        {
            var req = CreateRequest();
            req.Method = WebRequestMethods.Ftp.DownloadFile;

            using (var resp = req.GetResponse())
            {
                using (var fr = resp.GetResponseStream())
                {
                    using (var fw = new FileStream(file, FileMode.Create))
                    {
                        IOTool.CopyStream(fr, fw);
                    }
                }
            }
        }

        protected override void AfterWriteAction(string file)
        {
            var req = CreateRequest();
            req.Method = WebRequestMethods.Ftp.UploadFile;
            using (var fw = req.GetRequestStream())
            {
                using (var fr = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    IOTool.CopyStream(fr, fw);
                }
            }
        }

        #region IPropertyPageWithSerialization Members

        public string GetSerializationKey()
        {
            return "fileplace_ftp";
        }

        public string GetSerializationValue()
        {
            return GetVirtualFile();
        }

        public void SetSerializedValue(string value)
        {
            var cntinfo = ContainerInfo;
            LoadVirtualFile(value, null);
            SetFileHolderInfo(cntinfo);
        }

        #endregion
    }

    [FilePlace(Name = "filesystemtemplate", Title = "s_file_on_disk_template", RequiredFeature = ExtendedFilePlacesFeature.Test)]
    public class FilePlaceFileSystemTemplate : FilePlaceBase, ICustomPropertyPage
    {
        private string m_lastWorkingFile;

        public string Folder { get; set; }
        public string FileTemplate { get; set; }

        public override bool LoadVirtualFile(string virtualFile, IExtendedFileNameHolderInfo holder)
        {
            if (virtualFile.StartsWith("@filetpl:"))
            {
                string full = virtualFile.Substring(9);
                string[] ar = full.Split('|');
                if (ar.Length < 2) return false;
                Folder = ar[0];
                FileTemplate = ar[1];
                return true;
            }
            return false;
        }

        public override string GetVirtualFile()
        {
            return String.Format("@filetpl:{0}|{1}", Folder, FileTemplate);
        }

        public override bool SupportsLoad(IExtendedFileNameHolderInfo info) { return false; } 
        public override bool SupportsSave(IExtendedFileNameHolderInfo info) { return true; } 

        public override string GetWorkingFileName()
        {
            m_lastWorkingFile = Path.Combine(Folder, NameTemplateEngine.Eval(FileTemplate));
            return m_lastWorkingFile;
        }

        public override void CheckOutput()
        {
            base.CheckOutput();
            string fn = GetWorkingFileName();
            IOTool2.CheckOutputFileName(ref fn);
        }

        public override void DeleteFile()
        {
            if (m_lastWorkingFile != null)
            {
                File.Delete(m_lastWorkingFile);
            }
        }

        #region ICustomPropertyPage Members

        public Control CreatePropertyPage()
        {
            return new FilePlaceFileSystemTemplateFrame(this);
        }

        #endregion
    }

    [FilePlace(Name = "sendemail", Title = "s_as_email_attachment")]
    public class FilePlaceSendEmail : TempFilePlaceBase, ICustomPropertyPage
    {
        public FilePlaceSendEmail()
        {
            Subject = "";
            To = "";
            AttachmentName = "attachment";
            Body = "";
        }
        public string Subject { get; set; }
        public string To { get; set; }
        public string AttachmentName { get; set; }
        public string Body { get; set; }

        public override bool LoadVirtualFile(string virtualFile, IExtendedFileNameHolderInfo holder)
        {
            if (virtualFile.StartsWith("@email:"))
            {
                Subject = LoadProp(virtualFile, "subj");
                To = LoadProp(virtualFile, "to");
                AttachmentName = LoadProp(virtualFile, "att");
                Body = LoadProp(virtualFile, "body").Replace("#NL#", "\r\n");
                return true;
            }
            return false;
        }

        public override string GetVirtualFile()
        {
            return String.Format("@email:[to]{0}[/to][subj]{1}[/subj][att]{2}[/att][body]{3}[/body]",
                To, Subject, AttachmentName, Body.Replace("\r", "").Replace("\n", "#NL#"));
        }

        public override bool SupportsLoad(IExtendedFileNameHolderInfo info) { return false; } 
        public override bool SupportsSave(IExtendedFileNameHolderInfo info) { return true; } 

        public override void CheckOutput()
        {
            base.CheckOutput();

            if (To.IsEmpty()) throw new ValueNotSpecifiedError("DAE-00278", "s_email_to");
            if (Subject.IsEmpty()) throw new ValueNotSpecifiedError("DAE-00279", "s_subject");
            if (Body.IsEmpty()) throw new ValueNotSpecifiedError("DAE-00280", "s_body");

            var cfg = GlobalSettings.Pages.Email();
            cfg.CheckSettings();
            foreach (string email in To.Split(';'))
            {
                if (!IncorrectEmailError.IsValid(email))
                {
                    throw new IncorrectEmailError("DAE-00281", email);
                }
            }
        }

        protected override void AfterWriteAction(string file)
        {
            base.AfterWriteAction(file);

            var cfg = GlobalSettings.Pages.Email();
            MailMessage mail = new MailMessage();
            mail.From = cfg.GetFromAddress();
            foreach (string addr in To.Split(';')) mail.To.Add(addr);
            mail.Subject = NameTemplateEngine.Eval(Subject);
            mail.Body = Body;
            mail.IsBodyHtml = false;
            using (var fr = new FileInfo(file).OpenRead())
            {
                var att = new Attachment(fr, Path.ChangeExtension(AttachmentName, Path.GetExtension(file)));
                mail.Attachments.Add(att);

                SmtpClient smtp = cfg.GetClient();
                ProgressInfo.Info("Sending mail to " + To);
                smtp.Send(mail);
            }
        }

        #region ICustomPropertyPage Members

        public Control CreatePropertyPage()
        {
            return new FilePlaceSendEmailFrame(this);
        }

        #endregion
    }
}
