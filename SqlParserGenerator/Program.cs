using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.IO;

namespace SqlParserGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            //var secs = (new DateTime(2009, 1, 1) - new DateTime(1970, 1, 1)).TotalSeconds;
            string inf = args[0];
            string outf = args[1];
            string ns = args[2];
            string cls = args[3];
            using (StreamReader sr = new StreamReader(inf))
            {
                string data = sr.ReadToEnd();
                GramParser parser = new GramParser(new GramTokenizer(new StringReader(data), new StringSliceProvider(data)));
                RuleCollection rules = parser.ParseFile();
                ParserCompiler pc = new ParserCompiler(rules);
                rules.Compile(pc);
                using (StreamWriter sw = new StreamWriter(outf))
                {
                    CSharpWriter csw = new CSharpWriter(sw);
                    csw.WriteLine("using DatAdmin;");
                    rules.GenCode(csw, ns, cls);
                }
            }
        }
    }
}
