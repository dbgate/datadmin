using System;
using System.Collections.Generic;
using System.Text;
using IronPython.Hosting;
using DatAdmin;

namespace Plugin.apps
{
    public class AppEnv
    {
        PythonEngine m_engine;

        public AppEnv()
        {
            m_engine = new PythonEngine();
            ScriptingEnv.InitializeEngine(m_engine);
        }

        public PythonEngine Engine { get { return m_engine; } }
    }
}
