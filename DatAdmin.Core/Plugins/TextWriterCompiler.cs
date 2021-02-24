using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using IronPython.Hosting;
using System.Reflection;

namespace DatAdmin
{
    public delegate void RunTextWriterDelegate(Stream fw, IDictionary<string, object> extnames, IDictionary<string, object> outnames);

    public static class TextWriterCompiler
    {
        public static RunTextWriterDelegate CompileScript(string code, TextGeneratorLanguage language)
        {
            switch (language)
            {
                case TextGeneratorLanguage.Python:
                    return delegate(Stream fw, IDictionary<string, object> extnames, IDictionary<string, object> outnames)
                    {
                        PythonEngine engine = new PythonEngine();
                        ScriptingEnv.InitializeEngine(engine);
                        engine.SetStandardOutput(fw);
                        CompiledCode compiled = engine.Compile(code);
                        //if (node != null) node.GetScriptingNS(engine.DefaultModule.Globals);
                        if (extnames != null)
                        {
                            foreach (string key in extnames.Keys) engine.DefaultModule.Globals[key] = extnames[key];
                        }
                        compiled.Execute();
                        if (outnames != null)
                        {
                            foreach (string key in engine.DefaultModule.Globals.Keys)
                            {
                                outnames[key] = engine.DefaultModule.Globals[key];
                            }
                        }
                    };
                case TextGeneratorLanguage.Template:
                    return delegate(Stream fw, IDictionary<string, object> extnames, IDictionary<string, object> outnames)
                    {
                        PythonEngine engine = new PythonEngine();
                        engine.SetStandardOutput(fw);
                        ScriptingEnv.InitializeEngine(engine);
                        //if (node != null) node.GetScriptingNS(engine.DefaultModule.Globals);
                        if (extnames != null)
                        {
                            foreach (string key in extnames.Keys) engine.DefaultModule.Globals[key] = extnames[key];
                        }
                        Template tpl = new Template(code, engine);
                        StreamWriter sw = new StreamWriter(fw);
                        TemplateEnviroment env = new TemplateEnviroment(sw);
                        tpl.Run(env);
                    };
            }
            throw new Exception("DAE-00258 internal error");
        }
    }
}
