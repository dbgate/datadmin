using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using IronPython.Hosting;

namespace DatAdmin
{
    public static class ScriptingEnv
    {
        delegate string TranslateProc(string text, params string[] args);

        public static void GetStdEnv(IDictionary<string, object> names)
        {
            names["core"] = DatAdmin.Scripting.Core.Instance;
            names["mainwin"] = MainWindow.Instance;
            names["_t"] = (TranslateProc)Texts.Get;
        }

        public static void InitializeEngine(PythonEngine engine)
        {
            GetStdEnv(engine.DefaultModule.Globals);
            engine.Sys.DefaultEncoding = Encoding.UTF8;
            CompiledCode code = engine.CompileFile(Path.Combine(Core.LibDirectory, "initialize.py"));
            code.Execute();
        }
    }
}

namespace DatAdmin.Scripting
{
    public class Core
    {
        public static Core Instance = new Core();

        public string lib_directory { get { return DatAdmin.Core.LibDirectory; } }
        public string version { get { return DatAdmin.VersionInfo.VERSION; } }
        public string svn_revision { get { return DatAdmin.VersionInfo.SVN_REVISION; } }
        public string built_at { get { return DatAdmin.VersionInfo.BUILT_AT; } }
        public string edition { get { return LicenseTool.EditionText(); } }
        public string edition_valid_to {
            get
            {
                //if (DatAdmin.Registration.EditionValidTo != null) return DatAdmin.Registration.EditionValidTo.ToString();
                return "";
            }

        }
    }

    public class Record
    {
        IBedRecord m_record;
        int m_curValue = -1;

        public Record(IBedRecord record)
        {
            m_record = record;
        }

        private void WantValue(int index)
        {
            if (m_curValue != index)
            {
                m_curValue = index;
                m_record.ReadValue(m_curValue);
            }
        }

        public object this[int index]
        {
            get
            {
                WantValue(index);
                return m_record.GetValue();
            }
        }

        public object this[string name]
        {
            get
            {
                WantValue(m_record.GetOrdinal(name));
                return m_record.GetValue();
            }
        }

        public int FieldCount
        {
            get
            {
                return m_record.FieldCount;
            }
        }
    }
}
