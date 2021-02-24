using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Threading;

namespace Plugin.mysql
{
    public class MySqlCmdLineAdapter : CmdLineAdapterBase
    {
        public override void OpenDatabase(ICommandShell shell, ICmdLineConnection conn)
        {
            string verstr = "Server version:";
            string pwdstr = "Enter password:";

            //Thread.Sleep(1000);
            //shell.ConsumeOutput();
            string s = shell.ReadLineAndWait();
            while (s != null && !s.Contains("$")) s = shell.ReadLineAndWait();
            //m_shell.WriteLine("echo cau");
            shell.WriteLine("mysql -p -u " + conn.Params.Login);
            //System.Threading.Thread.Sleep(2000);
            //m_shell.WriteLine("echo cau");
            //int b = m_outstream.ReadByte();
            //string s = shell.ReadLineAndWait();
            s = shell.ReadLineAndWait();
            while (s != null && !s.Contains(verstr) && !s.StartsWith(pwdstr)) s = shell.ReadLineAndWait();
            if (s != null && s.StartsWith(pwdstr))
            {
                shell.WriteLine(conn.Params.Password);
                s = shell.ReadLineAndWait();
            }
            while (s != null && !s.Contains(verstr)) s = shell.ReadLineAndWait();
            if (s != null && s.Contains(verstr))
            {
                string version = s.Substring(verstr.Length).Trim();
                if (version.Contains(" ")) version = version.Substring(0, version.IndexOf(" ")).Trim();
                conn.SetVersion(version);
            }
        }
    }
}
