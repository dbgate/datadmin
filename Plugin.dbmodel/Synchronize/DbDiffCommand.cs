using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DatAdmin;

namespace Plugin.dbmodel
{
    [CommandLineCommand(Name = "dbdiff",  Description = "Scans differences between databases and dumps them to SQL script")]
    public class DbDiffCommand : CommandLineCommandBase
    {
        public override ICommandLineCommandInstance CreateInstance()
        {
            return new Instance();
        }

        class Instance : OutFileCommandInstanceBase
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

            string m_dialect;
            [CommandLineParameter(Name = "dialect", Description = "Dialect of output SQL scripts, list can be obtained using \"daci list dialect\"")]
            public string Dialect
            {
                get { return m_dialect; }
                set { m_dialect = value; }
            }

            public override void RunCommand()
            {
                IDatabaseSource srcDb = m_source.GetConnection();
                IDatabaseSource dstDb = m_target.GetConnection();
                Async.SafeOpen(srcDb.Connection); Async.SafeOpen(dstDb.Connection);
                var src = srcDb.InvokeLoadStructure(DatabaseStructureMembers.FullStructure, null);
                var dst = dstDb.InvokeLoadStructure(DatabaseStructureMembers.FullStructure, null);
                var opts = new DbDiffOptions();
                opts.IgnoreColumnOrder = true;
                //var diff = new DatabaseDiff(src, dst, opts);
                ISqlDialect dial;
                if (Dialect != null) dial = (ISqlDialect)DialectAddonType.Instance.FindHolder(Dialect).CreateInstance();
                else dial = new GenericDialect();
                using (TextWriter fw = GetOutputStream())
                {
                    SqlOutputStream so = new SqlOutputStream(dial, fw, new SqlFormatProperties());
                    ISqlDumper dmp = dial.CreateDumper(so, new SqlFormatProperties());
                    dmp.AlterDatabase(src, dst, opts, new DbDefSource(src, DbDefSource.ReadOnly.Flag));
                    //dmp.TargetDb = new DbDefSource(dst, DbDefSource.ReadOnly.Flag);
                    //diff.Actions.GenerateScript(dmp);
                }
                Async.SafeClose(srcDb.Connection); Async.SafeClose(dstDb.Connection);
            }
        }
    }
}
