using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin.selftest
{
    public static class TestTool
    {
        public static void RunMacro(string name, params string[] extargs)
        {
            MainForm win = (MainForm)MainWindow.Instance;
            string file = Path.Combine(Core.SelfTestDirectory, name + ".mdx");
            Macro macro = Macro.LoadFromFile(file);
            Dictionary<string, object> defvars = macro.GetDefaultVars();
            for (int i = 0; i + 1 < extargs.Length; i += 2)
            {
                defvars[extargs[i]] = extargs[i + 1];
            }
            macro.Run(win, defvars);
        }

        public static void CreateAndConnectDb(string confile)
        {
            RunMacro("createtestingdb", "var2", confile);
        }

        public static void CreateDbs()
        {
            bool first = true;
            foreach (var con in Config.Instance.Connections)
            {
                CreateAndConnectDb(con.ConFileName);
                if (first)
                {
                    RunMacro("expandroot");
                    first = false;
                }
            }
        }
    }
}
