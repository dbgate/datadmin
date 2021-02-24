using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace DatAdmin.selftest
{
    class Program
    {
        static void RunTest()
        {
            GlobalSettings.Pages.Log().WindowLogLevel = LogLevel.Debug;
            TestPrepare.CreateConnections();
            TestPrepare.DropDbs();
            TestTool.CreateDbs();
            //TestTool.RunMacro("quit");
        }

        [STAThread]
        static void Main(string[] args)
        {
            TestMySqlSplitter.Run(args[0], args[1]);
            //Config.Load(args[0]);
            //TestPrepare.ClearDirs();
            //Core.AppDataOverride = Config.Instance.AppDataDirectory;
            //DatAdmin.Program.RunMain(RunTest);
        }
    }
}
