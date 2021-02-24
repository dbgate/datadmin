using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using IronPython.Hosting;
using System.Text.RegularExpressions;
using System.Data.Common;
using System.Data;

namespace DatAdmin
{
    public class Template
    {
        PythonEngine m_engine;
        CompiledTemplate m_template;

        public Template(string code, PythonEngine engine)
        {
            m_engine = engine;
            Parser par = new Parser(code, m_engine);
            m_template = par.Parse();
        }

        public void Run(TemplateEnviroment env)
        {
            m_template.Run(env);
        }
    }

    internal delegate object PythonExpressionDelegate();

    internal abstract class CompiledTemplate
    {
        internal abstract void Run(TemplateEnviroment env);
        internal virtual void PushItem(CompiledTemplate tpl) { }
        internal virtual void Command(string type, string pars) {}
    }

    internal class ExpressionTemplate : CompiledTemplate
    {
        PythonExpressionDelegate m_func;
        internal ExpressionTemplate(string expr, PythonEngine engine)
        {
            m_func = engine.CreateLambda<PythonExpressionDelegate>(expr);
        }
        internal override void Run(TemplateEnviroment env)
        {
            object obj = m_func();
            env.Writer.Write(obj);
            env.Writer.Flush();
        }
    }

    internal class SqlExpressionTemplate : CompiledTemplate
    {
        string m_query;
        PythonEngine m_engine;

        internal SqlExpressionTemplate(string expr, PythonEngine engine)
        {
            m_query = expr;
            m_engine = engine;
        }
        internal override void Run(TemplateEnviroment env)
        {
            string sql = SelectTemplate.MakeSqlSubs(m_query, m_engine);
            LanguageQueryFunc func = (LanguageQueryFunc)m_engine.DefaultModule.Globals["query"];
            using (IDataReader reader = func(sql))
            {
                if (reader.Read())
                {
                    env.Writer.Write(reader[0].SafeToString());
                }
            }
        }
    }

    internal class StringDataTemplate : CompiledTemplate
    {
        string m_data;
        internal StringDataTemplate(string data)
        {
            m_data = data;
        }
        internal override void Run(TemplateEnviroment env)
        {
            env.Writer.Write(m_data);
            env.Writer.Flush();
        }
    }

    internal class CompoudTemplate : CompiledTemplate
    {
        List<CompiledTemplate> m_items = new List<CompiledTemplate>();
        internal override void Run(TemplateEnviroment env)
        {
            foreach (CompiledTemplate item in m_items)
            {
                item.Run(env);
            }
        }
        internal override void PushItem(CompiledTemplate tpl)
        {
            m_items.Add(tpl);
        }
    }

    internal delegate CompiledTemplate ItemParserDelegate(string pars, PythonEngine engine);

    internal class ForTemplate : CompiledTemplate
    {
        CompoudTemplate m_body;
        CompoudTemplate m_separ = null;
        string m_varname;
        PythonExpressionDelegate m_container;
        static Regex regex = new Regex(@"([^\s]+)\s+in\s+(.+)$");
        PythonEngine m_engine;

        internal static CompiledTemplate Create(string pars, PythonEngine engine)
        {
            return new ForTemplate(pars, engine);
        }
        internal ForTemplate(string pars, PythonEngine engine)
        {
            m_engine = engine;
            m_body = new CompoudTemplate();
            Match m = regex.Match(pars);
            m_varname = m.Groups[1].Value;
            m_container = engine.CreateLambda<PythonExpressionDelegate>(m.Groups[2].Value);
        }
        internal override void PushItem(CompiledTemplate tpl)
        {
            if (m_separ != null)
            {
                m_separ.PushItem(tpl);
            }
            else
            {
                m_body.PushItem(tpl);
            }
        }
        internal override void Run(TemplateEnviroment env)
        {
            bool was = false;
            foreach (object obj in (System.Collections.IEnumerable)m_container())
            {
                if (was && m_separ != null) m_separ.Run(env);
                m_engine.DefaultModule.Globals[m_varname] = obj;
                m_body.Run(env);
                was = true;
            }
        }
        internal override void Command(string type, string pars)
        {
            if (type == "sep") m_separ = new CompoudTemplate();
        }
    }

    public delegate IDataReader LanguageQueryFunc(string sql);

    internal class SelectTemplate : CompiledTemplate
    {
        CompoudTemplate m_body;
        CompoudTemplate m_separ = null;
        PythonEngine m_engine;
        string m_select;
        static Regex m_exprs = new Regex(@"\{([^\}]+)\}", RegexOptions.Compiled);

        internal static CompiledTemplate Create(string pars, PythonEngine engine)
        {
            return new SelectTemplate(pars, engine);
        }
        internal SelectTemplate(string pars, PythonEngine engine)
        {
            m_engine = engine;
            m_body = new CompoudTemplate();
            m_select = "select " + pars;
        }
        internal override void PushItem(CompiledTemplate tpl)
        {
            if (m_separ != null)
            {
                m_separ.PushItem(tpl);
            }
            else
            {
                m_body.PushItem(tpl);
            }
        }
        internal static string MakeSqlSubs(string query, PythonEngine engine)
        {
            return m_exprs.Replace(query, m => engine.Evaluate(m.Groups[1].Value).SafeToString());
        }

        private void ProcessRecord(IBedRecord record, TemplateEnviroment env, int index)
        {
            if (index > 0 && m_separ != null) m_separ.Run(env);
            m_engine.Globals["_row_"] = record;
            for (int fi = 0; fi < record.FieldCount; fi++)
            {
                m_engine.Globals["_" + record.GetName(fi).ToLower() + "_"] = record.GetValue(fi).SafeToString();
            }
            m_body.Run(env);
        }

        internal override void Run(TemplateEnviroment env)
        {
            LanguageQueryFunc func = (LanguageQueryFunc)m_engine.DefaultModule.Globals["query"];
            IDatabaseSource dbcontext = (IDatabaseSource)m_engine.DefaultModule.Globals["dbcontext"];
            string sql = MakeSqlSubs(m_select, m_engine);
            IBedReader reader = dbcontext.GetAnyDDA().AdaptReader(func(sql));
            reader.RunForEachRecordAndDispose(dbcontext.Dialect.DialectCaps.MARS, (rec, index) => ProcessRecord(rec, env, index));
        }
        internal override void Command(string type, string pars)
        {
            if (type == "sep") m_separ = new CompoudTemplate();
        }
    }


    internal class Parser
    {
        string m_code;
        int m_pos = 0;
        StringBuilder m_buffer = new StringBuilder();
        PythonEngine m_engine;
        Stack<CompiledTemplate> m_stack = new Stack<CompiledTemplate>();
        Stack<string> m_stackCommands = new Stack<string>();
        static Dictionary<string, ItemParserDelegate> m_itemTypes = new Dictionary<string, ItemParserDelegate>();

        static Parser()
        {
            m_itemTypes["for"] = ForTemplate.Create;
            m_itemTypes["select"] = SelectTemplate.Create;
        }

        internal Parser(string code, PythonEngine engine)
        {
            m_code = code;
            m_engine = engine;
        }

        private void ParseInterior(string end)
        {
            string cmd = ReadInterior(end).Trim();
            int idx = cmd.IndexOf(' ');
            string type, pars = "";
            if (idx >= 0)
            {
                type = cmd.Substring(0, idx).Trim();
                pars = cmd.Substring(idx + 1).Trim();
            }
            else
            {
                type = cmd.Trim();
            }
            if (type == "end")
            {
                if (m_stackCommands.Pop() != pars) ParseError("end mitchmatch");
                m_stack.Pop();
            }
            else
            {
                if (m_itemTypes.ContainsKey(type))
                {
                    CompiledTemplate tpl = m_itemTypes[type](pars, m_engine);
                    m_stack.Peek().PushItem(tpl);
                    m_stack.Push(tpl);
                    m_stackCommands.Push(type);
                }
                else
                {
                    m_stack.Peek().Command(type, pars);
                }
            }
        }

        internal CompiledTemplate Parse()
        {
            m_stack.Push(new CompoudTemplate());
            while (m_pos < m_code.Length)
            {
                if (Starts("<%="))
                {
                    string expr = ReadInterior("%>");
                    m_stack.Peek().PushItem(new ExpressionTemplate(expr, m_engine));
                }
                else if (Starts("<@="))
                {
                    string expr = ReadInterior("@>");
                    m_stack.Peek().PushItem(new SqlExpressionTemplate(expr, m_engine));
                }
                else if (Starts("<%"))
                {
                    ParseInterior("%>");
                }
                else if (Starts("<@"))
                {
                    ParseInterior("@>");
                }
                else
                {
                    m_buffer.Append(m_code[m_pos]);
                    m_pos++;
                }
            }
            FlushBuffer();
            return m_stack.Pop();
        }

        private void ParseError(string msg)
        {
            throw new Exception(msg);
        }

        private void FlushBuffer()
        {
            if (m_buffer.Length > 0)
            {
                m_stack.Peek().PushItem(new StringDataTemplate(m_buffer.ToString()));
                m_buffer.Length = 0;
            }
        }

        private bool Starts(string p)
        {
            if (m_pos + p.Length > m_code.Length) return false;
            if (m_code.Substring(m_pos, p.Length) == p)
            {
                m_pos += p.Length;
                FlushBuffer();
                return true;
            }
            return false;
        }

        private string ReadInterior(string endText)
        {
            int start = m_pos;
            int end = m_code.IndexOf(endText, m_pos);
            if (end < 0)
            {
                m_pos = m_code.Length;
                return m_code.Substring(start);
            }
            else
            {
                m_pos = end + endText.Length;
                return m_code.Substring(start, end - start);
            }
            //int level = 0;
            //StringBuilder res = new StringBuilder();
            //while (m_pos < m_code.Length)
            //{
            //    char ch = m_code[m_pos];
            //    if (ch == ']' && level == 0)
            //    {
            //        m_pos++;
            //        break;
            //    }
            //    if (ch == '[') level++;
            //    if (ch == ']') level--;
            //    res.Append(ch);
            //    m_pos++;
            //}
            //return res.ToString();
        }
    }

    public class TemplateEnviroment
    {
        public TextWriter Writer;

        public TemplateEnviroment(TextWriter writer)
        {
            Writer = writer;
        }
    }
}
