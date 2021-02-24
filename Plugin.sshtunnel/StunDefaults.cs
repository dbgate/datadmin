using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.sshtunnel
{
    public class StunDefaults : TunnellingDefaults
    {
        public static StunDefaults Instance = new StunDefaults();

        public override string[] Engines
        {
            get { return new string[] { "mysql", "postgre" }; }
        }
    }
}
