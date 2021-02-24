using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    [CommandLineCommand(Name = "copydb", Description = "Copies one database to other")]
    public class CopyDatabaseCommand : CommandLineCommandBase
    {
        public override ICommandLineCommandInstance CreateInstance()
        {
            return new Instance();
        }

        class Instance : CommandLineCommandInstanceBase
        {
            ConnectionParameter m_source = new ConnectionParameter();
            [CommandLineParameterCollection(Prefix = "src.", Description = "determines source database")]
            public ConnectionParameter Source
            {
                get { return m_source; }
                set { m_source = value; }
            }

            ConnectionParameter m_target = new ConnectionParameter();
            [CommandLineParameterCollection(Prefix = "dst.", Description = "determines target database")]
            public ConnectionParameter Target
            {
                get { return m_target; }
                set { m_target = value; }
            }

            public override void RunCommand()
            {
                IDatabaseSource src = m_source.GetConnection();
                IDatabaseSource dst = m_target.GetConnection();
                CopyDbJob.CopyDatabase(src, new DatabaseSourceWriter(dst), null, new DatabaseCopyOptions { CopyMembers = DatabaseStructureMembers.FullStructure });
            }
        }
    }
}
