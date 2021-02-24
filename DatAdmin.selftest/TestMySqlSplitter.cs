using System;
using System.Collections.Generic;
using System.Text;
using Plugin.mysql;
using System.IO;

namespace DatAdmin.selftest
{
    public static class TestMySqlSplitter
    {
        public static void Run(string file, string outfile)
        {
            var splitter = new MySqlQuerySplitter(new MySqlDialect());
            using (var fr = new StreamReader(file))
            {
                using (var fw = new StreamWriter(outfile))
                {
                    foreach (var item in splitter.Run(fr))
                    {
                        fw.WriteLine(item.Data);
                        fw.WriteLine("GO");
                    }
                }
            }
        }
    }
}
