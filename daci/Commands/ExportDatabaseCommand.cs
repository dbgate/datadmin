using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    [CommandLineCommand(Name = "exportdb", Description = "Downloads database structure/data and saves it to file")]
    public class ExportDatabaseCommand : CommandLineCommandBase
    {
        public override ICommandLineCommandInstance CreateInstance()
        {
            return new Instance();
        }

        class Instance : CommandLineCommandInstanceBase
        {
            ConnectionParameter m_connection = new ConnectionParameter();
            [CommandLineParameterCollection]
            public ConnectionParameter Connection
            {
                get { return m_connection; }
                set { m_connection = value; }
            }

            string m_format;
            [CommandLineParameter(Name = "format", Description = "output format, ddf or dbk")]
            public string Format
            {
                get { return m_format; }
                set { m_format = value; }
            }

            string m_outfile;
            [CommandLineParameter(Name = "outfile", Mandatory = true, Description = "name of output file")]
            public string Outfile
            {
                get { return m_outfile; }
                set { m_outfile = value; }
            }

            public override void RunCommand()
            {
                if (Format == null)
                {
                    if (Outfile.EndsWith(".dbk")) Format = "dbk";
                    else if (Outfile.EndsWith(".ddf")) Format = "ddf";
                    else throw new CommandLineError("DAE-00151 Unknown output format, cannot be deduced from file extension");
                }
                IDatabaseSource db = m_connection.GetConnection();
                Async.SafeOpen(db.Connection);
                try
                {
                    switch (Format)
                    {
                        case "ddf":
                            {
                                var s = new DatabaseStructure(db.InvokeLoadStructure(DatabaseStructureMembers.FullStructure, null));
                                s.Save(Outfile);
                            }
                            break;
                        case "dbk":
                            {
                                DataArchiveWriter dw = new DataArchiveWriter();
                                dw.FilePlace = FilePlaceAddonType.PlaceFromVirtualFile(Outfile);
                                CopyDbJob.CopyDatabase(db, dw, null, new DatabaseCopyOptions { CopyMembers = DatabaseStructureMembers.FullStructure });
                                Async.SafeClose(db.Connection);
                            }
                            break;
                        default:
                            throw new CommandLineError("DAE-00152 Unknown format:" + Format);
                    }
                }
                finally
                {
                    Async.SafeClose(db.Connection);
                }
            }
        }
    }
}
