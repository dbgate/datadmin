using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public interface ICmdLineAdapter
    {
        void OpenDatabase(ICommandShell shell, ICmdLineConnection conn);
    }

    public interface ICommandShell
    {
        void WriteLine(string s);
        string ReadLine();
        string ReadLineAndWait();

        void ConsumeOutput();
    }

    public interface ICmdLineConnection
    {
        TunnelConnectionStringBuilder Params { get; }
        void SetVersion(string version);
    }

    public abstract class CmdLineAdapterBase : ICmdLineAdapter
    {
        #region ICmdLineAdapter Members

        public abstract void OpenDatabase(ICommandShell shell, ICmdLineConnection conn);

        #endregion
    }
}
