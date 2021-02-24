using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    [CommandLineCommand(Name = "gensql", Description = "Generates SQL from given object")]
    public class GenerateSqlCommand : CommandLineCommandBase
    {
        public override ICommandLineCommandInstance CreateInstance()
        {
            return new Instance();
        }

        class Instance : OutFileCommandInstanceBase
        {
            ConnectionParameter m_connection = new ConnectionParameter();
            [CommandLineParameterCollection]
            public ConnectionParameter Connection
            {
                get { return m_connection; }
                set { m_connection = value; }
            }

            string m_dialect;
            [CommandLineParameter(Name = "dialect", Description = "Dialect of output SQL scripts, list can be obtained using \"daci list dialect\"")]
            public string Dialect
            {
                get { return m_dialect; }
                set { m_dialect = value; }
            }

            string m_template;
            [CommandLineParameter(Name = "template", Description = "SQL template which to use; Use \"daci list sqlgenerator\" to view possible values")]
            public string Template
            {
                get { return m_template; }
                set { m_template = value; }
            }

            string m_objname;
            [CommandLineParameter(Name = "objname", Description = "Dumped object name without schema (eg. table name, when dumping table)")]
            public string Objname
            {
                get { return m_objname; }
                set { m_objname = value; }
            }

            string m_objschema;
            [CommandLineParameter(Name = "objschema", Description = "Schema of dumped object")]
            public string Objschema
            {
                get { return m_objschema; }
                set { m_objschema = value; }
            }

            string m_objtype;
            [CommandLineParameter(Name = "objtype", Description = "Type of dumped object, eg. table or view")]
            public string Objtype
            {
                get { return m_objtype; }
                set { m_objtype = value; }
            }

            string m_subname;
            [CommandLineParameter(Name = "subtype", Description = "Name of subobject, eg. column, constraint or index name")]
            public string Subname
            {
                get { return m_subname; }
                set { m_subname = value; }
            }


            public override void RunCommand()
            {
                if (Template == null) throw new CommandLineError("DAE-00153 Missing template argument");
                IDatabaseSource db = m_connection.GetConnection();
                Async.SafeOpen(db.Connection);
                IAppObjectSqlGenerator sqlgen = (IAppObjectSqlGenerator)AppObjectSqlGeneratorAddonType.Instance.FindHolder(Template).CreateInstance();
                ISqlDialect dial;
                if (Dialect != null) dial = (ISqlDialect)DialectAddonType.Instance.FindHolder(Dialect).CreateInstance();
                else dial = new GenericDialect();
                using (TextWriter fw = GetOutputStream())
                {
                    SqlOutputStream so = new SqlOutputStream(dial, fw, new SqlFormatProperties());
                    ISqlDumper dmp = dial.CreateDumper(so, new SqlFormatProperties());
                    var name = new FullDatabaseRelatedName
                    {
                        ObjectName = new NameWithSchema(Objschema, Objname),
                        ObjectType = Objtype,
                        SubName = Subname
                    };
                    sqlgen.GenerateSql(db, name, dmp, dial);
                }
                Async.SafeClose(db.Connection);
            }
        }
    }
}
