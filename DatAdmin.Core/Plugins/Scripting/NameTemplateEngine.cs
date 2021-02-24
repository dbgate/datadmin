using System;
using System.Collections.Generic;
using System.Text;
using IronPython.Hosting;
using System.Text.RegularExpressions;
using System.Globalization;

namespace DatAdmin
{
    public class NameTemplateEngine
    {
        static NameTemplateEngine m_instance;
        static Regex m_re = new Regex(@"\{([^\}]+)\}");

        PythonEngine m_engine;

        static NameTemplateEngine Instance
        {
            get
            {
                if (m_instance == null) m_instance = new NameTemplateEngine();
                return m_instance;
            }
        }

        private NameTemplateEngine()
        {
            m_engine = new PythonEngine();
            ScriptingEnv.InitializeEngine(m_engine);
            m_engine.Globals["guid"] = (Func<string>)GuidFunc;
            m_engine.Globals["date"] = (Func<string, string>)DateFunc;
            m_engine.Globals["utcdate"] = (Func<string, string>)UtcDateFunc;
        }

        private string GuidFunc()
        {
            return Guid.NewGuid().ToString();
        }

        private string DateFunc(string format)
        {
            return DateTime.Now.ToString(format, CultureInfo.InvariantCulture);
        }

        private string UtcDateFunc(string format)
        {
            return DateTime.UtcNow.ToString(format, CultureInfo.InvariantCulture);
        }

        private string EvalExpr(Match m)
        {
            lock (m_engine)
            {
                try
                {
                    return m_engine.Evaluate(m.Groups[1].Value).SafeToString();
                }
                catch
                {
                    return "ERR";
                }
            }
        }

        private static string StaticEvalExpr(Match m)
        {
            return Instance.EvalExpr(m);
        }

        public static string Eval(string template)
        {
            return m_re.Replace(template, StaticEvalExpr);
        }
    }
}
