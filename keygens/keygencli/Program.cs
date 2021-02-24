using System;
using System.Collections.Generic;
using System.Text;

namespace keygencli
{
    class Program
    {
        static void Main(string[] args)
        {
            string product = args[0];
            string email = args[1];
            string name = args[2];
            string license = keygenlib.LicenseTool.CreateLicense(name, email, "name", product, null, null);
            Console.Out.Write(license);
        }
    }
}
