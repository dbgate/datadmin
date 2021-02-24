using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Xml;

namespace DatAdmin
{
    public interface ILanguageWriter
    {
        void Struct(string name);
        void Class(string name);
        void Variable(string visibility, string type, string name);
        void End();
        void Begin(string line, params object[] args);
        void WriteLine(string s, params object[] args);
        void Method(string type, string name, params string[] args);
        void MethodEx(string visibility, string type, string name, params string[] args);
    }

    public class CSharpWriter : ILanguageWriter
    {
        TextWriter m_fw;
        int m_indent = 0;
        public CSharpWriter(TextWriter fw)
        {
            m_fw = fw;
            m_fw.WriteLine("using System;");
            m_fw.WriteLine("using System.Collections.Generic;");
            m_fw.WriteLine("using System.Text;");
            m_fw.WriteLine("using System.Data;");
        }
        private void _WriteLine(string s)
        {
            for (int i = 0; i < m_indent; i++) m_fw.Write(' ');
            m_fw.Write(s);
            m_fw.Write("\r\n");
        }
        public void Inc()
        {
            m_indent += 4;
        }
        public void Dec()
        {
            m_indent -= 4;
        }
        public void WriteLine(string s, params object[] args)
        {
            if (args.Length > 0)
            {
                _WriteLine(String.Format(s, args));
            }
            else
            {
                _WriteLine(s);
            }
        }
        private void Object(string type, string name)
        {
            WriteLine("");
            WriteLine("{0} {1}", type, name);
            WriteLine("{");
            Inc();
        }
        public void Begin(string line, params object[] args)
        {
            if (line != "") WriteLine(line, args);
            WriteLine("{");
            Inc();
        }
        public void MethodEx(string visibility, string type, string name, params string[] args)
        {
            WriteLine("{0} {1} {2} ({3})", visibility, type, name, String.Join(",", args));
            WriteLine("{");
            Inc();
        }
        public void Method(string type, string name, params string[] args)
        {
            MethodEx("public", type, name, args);
        }
        public void Struct(string name)
        {
            Object("public struct", name);
        }
        public void Class(string name)
        {
            Object("public class", name);
        }
        public void Variable(string visibility, string type, string name)
        {
            WriteLine("{0} {1} {2};", visibility, type, name);
        }
        public void End()
        {
            Dec();
            WriteLine("}");
        }

        public static string StringLiteral(string x)
        {
            return "\"" + x
                .Replace("\\", "\\\\")
                .Replace("\n", "\\n")
                .Replace("\r", "\\r")
                .Replace("\t", "\\t") 
                .Replace("\"", "\\\"")
                .Replace("\'", "\\\'")
                + "\"";
        }
    }
}
