using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.phptunnel
{
    public class PtunDefaults : TunnellingDefaults
    {
        public static PtunDefaults Instance = new PtunDefaults();

        public override string[] Engines
        {
            get { return new string[] { "mysql", "postgre" }; }
        }
    }
}
