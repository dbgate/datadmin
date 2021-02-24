using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DatAdmin32
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            DatAdmin.Program.PublicMain(args);
        }
    }
}
