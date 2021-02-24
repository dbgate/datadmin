using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    [CommandLineCommand(Name = "runsql", Description = "Run SQL on given connection")]
    public class RunSqlCommand : CommandLineCommandBase
    {
        public override ICommandLineCommandInstance CreateInstance()
        {
            return new Instance();
        }

        class Instance : InFileCommandInstanceBase
        {
            ConnectionParameter m_connection = new ConnectionParameter();
            [CommandLineParameterCollection]
            public ConnectionParameter Connection
            {
                get { return m_connection; }
                set { m_connection = value; }
            }

            private void RunScript(ISqlDumper dmp)
            {
                using (var fr = GetInputStream())
                {
                    foreach (string sql in QueryTools.GoSplit(fr))
                    {
                        dmp.WriteRaw(sql);
                        dmp.EndCommand();
                    }
                }
            }

            public override void RunCommand()
            {
                IDatabaseSource db = m_connection.GetConnection();
                Async.SafeOpen(db.Connection);
                db.RunScript(RunScript);
                Async.SafeClose(db.Connection);
            }
        }
    }
}
