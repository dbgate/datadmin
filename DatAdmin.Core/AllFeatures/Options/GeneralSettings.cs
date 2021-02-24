using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using Microsoft.Win32;

namespace DatAdmin
{
    //public enum CheckNewVersions { DONT_CHECK, CHECK_ONLY, CHECK_AND_DOWNLOAD }
    public class LanguageTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string))
            {
                foreach (LangInfo lang in LangManager.Languages)
                {
                    if (lang.Identifier == value.ToString() || lang.LocalName == value.ToString() || lang.Name == value.ToString()) return lang.Identifier;
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                foreach (LangInfo lang in LangManager.Languages)
                {
                    if (lang.Identifier == value.ToString() || lang.LocalName == value.ToString() || lang.Name == value.ToString()) return lang.LocalName;
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<string> langs = new List<string>();
            foreach (LangInfo lang in LangManager.Languages)
            {
                langs.Add(lang.Identifier);
            }
            System.ComponentModel.TypeConverter.StandardValuesCollection svc = new System.ComponentModel.TypeConverter.StandardValuesCollection(langs);
            return svc;
        }
    }

    [SettingsPage(Name = "general", Title = "s_general", Targets = SettingsTargets.Global)]
    public class GeneralSettings : SettingsPageBase
    {
        string m_language = "en";

        [DisplayName("s_language")]
        [TypeConverter(typeof(LanguageTypeConverter))]
        [SettingsKey("general.language")]
        public string Language
        {
            get { return m_language; }
            set { m_language = value; }
        }

        CheckNewVersions m_checkNewVersion = CheckNewVersions.CheckAndDownload;

        [DisplayName("s_check_new_versions")]
        [TypeConverter(typeof(EnumDescConverter))]
        [SettingsKey("general.check_new_versions")]
        public CheckNewVersions CheckNewVersion
        {
            get { return m_checkNewVersion; }
            set { m_checkNewVersion = value; }
        }

        bool m_askWhenUploadUsageStats = true;

        //[DisplayName("s_ask_when_upload_usage_stats")]
        //[TypeConverter(typeof(YesNoTypeConverter))]
        [Browsable(false)]
        [SettingsKey("general.userexp.ask_when_upload_stats")]
        public bool AskWhenUploadUsageStats
        {
            get { return m_askWhenUploadUsageStats; }
            set { m_askWhenUploadUsageStats = value; }
        }

        bool m_allowUploadUsageStats = true;

        [DisplayName("s_allow_upload_usage_stats")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [SettingsKey("general.userexp.allow_upload_stats")]
        public bool AllowUploadUsageStats
        {
            get { return m_allowUploadUsageStats; }
            set { m_allowUploadUsageStats = value; }
        }

        bool m_allowSendErrorReports = true;
        [DisplayName("s_allow_send_error_reports")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [SettingsKey("general.userexp.allow_send_errors")]
        public bool AllowSendErrorReports
        {
            get { return m_allowSendErrorReports; }
            set { m_allowSendErrorReports = value; }
        }

        public override void SetDefaults()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\DatAdmin"))
                {
                    string lang = key.GetValue("Installer Language").ToString();
                    if (lang == "1029") m_language = "cz";
                    if (lang == "1033") m_language = "en";
                    if (lang == "1040") m_language = "it";
                    if (lang == "1036") m_language = "fr";
                    if (lang == "2052") m_language = "sc";
                }
            }
            catch (Exception)
            {
                m_language = "en";
            }
        }
    }

    public static class SettingsPageCollection_General
    {
        public static GeneralSettings General(this SettingsPageCollection col)
        {
            return (GeneralSettings)col.PageByName("general");
        }
    }
}