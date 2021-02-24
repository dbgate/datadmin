using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    //[CommandLineCommand]
    //public class ExportAddonsCommand : ICommandLineCommand
    //{
    //    #region ICommandLineCommand Members

    //    public string Name
    //    {
    //        get { return "exportaddons"; }
    //    }

    //    public string Description
    //    {
    //        get { return "Exports all addons into single adp (addons package) file"; }
    //    }

    //    public ICommandLineCommandInstance CreateInstance()
    //    {
    //        return new Instance();
    //    }

    //    public bool AllowExtParams { get { return true; } }

    //    #endregion

    //    class Instance : CommandLineCommandInstanceBase
    //    {
    //        string m_outfile;

    //        [CommandLineParameter(Name = "file", Positional = true, Description = "Name of output file")]
    //        public string Outfile
    //        {
    //            get { return m_outfile; }
    //            set { m_outfile = value; }
    //        }

    //        string m_addonsdir;

    //        [CommandLineParameter(Name = "dir", Positional = false, Description = "Folder with addons, if not specified, default location is used", DefaultValue=null)]
    //        public string Addonsdir
    //        {
    //            get { return m_addonsdir; }
    //            set { m_addonsdir = value; }
    //        }

    //        public override void RunCommand()
    //        {
    //            AddonDbTool.ExportLog log = AddonDbTool.ExportAddons(Outfile, Addonsdir);
    //            Console.WriteLine("Export count:" + log.Exported.Count.ToString());
    //        }
    //    }
    //}

    //[CommandLineCommand]
    //public class ImportAddonsCommand : ICommandLineCommand
    //{
    //    #region ICommandLineCommand Members

    //    public string Name
    //    {
    //        get { return "importaddons"; }
    //    }

    //    public string Description
    //    {
    //        get { return "Imports all addons into single adp (addons package) file"; }
    //    }

    //    public ICommandLineCommandInstance CreateInstance()
    //    {
    //        return new Instance();
    //    }

    //    public bool AllowExtParams { get { return true; } }

    //    #endregion

    //    class Instance : CommandLineCommandInstanceBase
    //    {
    //        string m_infile;

    //        [CommandLineParameter(Name = "file", Positional = true, Description = "Name of input file")]
    //        public string Infile
    //        {
    //            get { return m_infile; }
    //            set { m_infile = value; }
    //        }

    //        public override void RunCommand()
    //        {
    //            AddonDbTool.ImportLog log = AddonDbTool.ImportAddons(Infile);
    //            Console.WriteLine("Import count:" + log.Imported.Count.ToString());
    //            Console.WriteLine("Skip count:" + log.Skipped.Count.ToString());
    //        }
    //    }
    //}
}
