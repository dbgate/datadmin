using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Xml;
using System.IO;

namespace DatAdmin
{
    public abstract class CommandLineCommandInstanceBase : ICommandLineCommandInstance
    {
        #region ICommandLineCommandInstance Members

        public abstract void RunCommand();
        public Dictionary<string, string> ExtParams { get; set; }

        #endregion
    }

    public abstract class CommandLineCommandBase : AddonBase, ICommandLineCommand
    {
        public override AddonType AddonType
        {
            get { return CommandLineCommandAddonType.Instance; }
        }

        #region ICommandLineCommand Members

        public virtual string Name
        {
            get { return AddonTool.ExtractAddonName(this); }
        }

        public virtual string Description
        {
            get { return AddonTool.ExtractDesctiption(this); }
        }

        public abstract ICommandLineCommandInstance CreateInstance();

        public virtual bool AllowExtParams
        {
            get { return false; }
        }

        #endregion
    }

    public abstract class OutFileCommandInstanceBase : CommandLineCommandInstanceBase
    {
        string m_outfile;
        [CommandLineParameter(Name = "outfile", Description = "name of output file, stdout when ommited")]
        public string Outfile
        {
            get { return m_outfile; }
            set { m_outfile = value; }
        }

        protected TextWriter GetOutputStream()
        {
            if (m_outfile == null) return Console.Out;
            return new StreamWriter(m_outfile);
        }
    }

    public abstract class InFileCommandInstanceBase : CommandLineCommandInstanceBase
    {
        string m_infile;
        [CommandLineParameter(Name = "infile", Description = "name of input file, stdin when ommited")]
        public string Infile
        {
            get { return m_infile; }
            set { m_infile = value; }
        }

        protected TextReader GetInputStream()
        {
            if (m_infile == null) return Console.In;
            return new StreamReader(m_infile);
        }
    }
}
