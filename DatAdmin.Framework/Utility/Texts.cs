using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DatAdmin
{
    public static class Texts
    {
        private static Regex m_replaceRegex = new Regex(@"\{([^\}]+)\}", RegexOptions.Compiled);
        static List<ITextProvider> m_textProviders = new List<ITextProvider>();
        static string m_lang = "en";

        public static void RegisterTextProvider(ITextProvider provider)
        {
            m_textProviders.Add(provider);
        }

        public static string LangGet(string name, string lang)
        {
            foreach (ITextProvider provider in m_textProviders)
            {
                string res = provider.GetText(name, lang ?? m_lang);
                if (res != null) return res;
            }
            return name;
        }

        public static string Get(string name)
        {
            return LangGet(name, m_lang);
        }

        public static string LangReplace(string text, string lang)
        {
            text = m_replaceRegex.Replace(text, m => LangGet(m.Groups[1].Value, lang));
            if (text != null && text.StartsWith("s_")) text = LangGet(text, lang);
            return text;
        }

        public static string Replace(string text)
        {
            return LangReplace(text, m_lang);
        }

        public static string Get(string name, string name1, string value1)
        {
            string res = Get(name);
            res = res.Replace("$" + name1, value1);
            return res;
        }

        public static string Get(string name, params object[] parval)
        {
            string res = Get(name);
            for (int i = 0; i < parval.Length; i += 2)
            {
                res = res.Replace("$" + parval[i].ToString(), parval[i + 1].ToString());
            }
            return res;
        }

        public static string Language
        {
            get { return m_lang; }
            set { m_lang = value; }
        }

        public static string GetTextIdWithoutPrefix(string s)
        {
            if (s.StartsWith("s_")) return s.Substring(2);
            return s;
        }
    }
}
