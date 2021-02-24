using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.Drawing.Design;
using System.Data.Common;

namespace DatAdmin
{
    [JobCommand(Name = "runsql")]
    public class RunSqlJobCommand : JobCommand
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IStoredConnection Connection { get; set; }

        [XmlElem]
        [SyntaxEditorLanguage(CodeLanguage.Sql)]
        [Editor(typeof(SyntaxEditor), typeof(UITypeEditor))]
        public string Sql { get; set; }

        [XmlElem]
        public string Database { get; set; }

        public RunSqlJobCommand(IStoredConnection conn, string db, string sql)
        {
            Connection = conn;
            Sql = sql;
            Database = db;
        }

        public RunSqlJobCommand() { }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            Connection = (IStoredConnection)StoredConnectionAddonType.Instance.LoadAddon(xml.FindElement("Connection"));
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            Connection.SaveToXml(xml.AddChild("Connection"));
        }

        protected override void DoRun(IJobRunEnv env)
        {
            m_job.m_process.Info("Executing SQL:" + ToString());
            using (var conn = Connection.CreateSystemConnection())
            {
                conn.Open();
                conn.SafeChangeDatabase(Database);
                conn.ExecuteNonQueries(Sql, Connection.GetDialect(), null, 3600);
            }
        }

        public override string ToString()
        {
            if (!Sql.IsEmpty())
            {
                if (Sql.Contains("\n")) return Sql.Substring(0, Sql.IndexOf('\n'));
                return Sql;
            }
            return "(SQL)";
        }

        public override string TypeTitle
        {
            get { return "s_run_sql"; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.sql; }
        }
    }

    [JobCommand(Name = "runsql2")]
    public class RunSqlJobCommand2 : JobCommand
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IDatabaseSource Database { get; set; }

        [XmlElem]
        [SyntaxEditorLanguage(CodeLanguage.Sql)]
        [Editor(typeof(SyntaxEditor), typeof(UITypeEditor))]
        public string Sql { get; set; }

        public RunSqlJobCommand2(IDatabaseSource db, string sql)
        {
            Database =db;
            Sql = sql;
        }

        public RunSqlJobCommand2() { }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            Database = (IDatabaseSource)DatabaseSourceAddonType.Instance.LoadAddon(xml.FindElement("Database"));
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            Database.SaveToXml(xml.AddChild("Database"));
        }

        private void RunQueries()
        {
            Database.Connection.SystemConnection.SafeChangeDatabase(Database.DatabaseName);
            Database.Connection.SystemConnection.ExecuteNonQueries(Sql, Database.Dialect, null, 3600);
        }

        protected override void DoRun(IJobRunEnv env)
        {
            m_job.m_process.Info("Executing SQL:" + ToString());

            Async.SafeOpen(Database.Connection);
            Database.Connection.Invoke(RunQueries);
            Async.SafeClose(Database.Connection);
        }

        public override string ToString()
        {
            if (!Sql.IsEmpty())
            {
                if (Sql.Contains("\n")) return Sql.Substring(0, Sql.IndexOf('\n'));
                return Sql;
            }
            return "(SQL)";
        }

        public override string TypeTitle
        {
            get { return "s_run_sql"; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.sql; }
        }
    }

    public static class RunSqlJob
    {
        public static Job CreateJob(IDatabaseSource db, string sql)
        {
            return Job.FromCommand(new RunSqlJobCommand2(db, sql), new JobProperties());
        }

        public static Job CreateJob(IStoredConnection conn, string db, string sql)
        {
            return Job.FromCommand(new RunSqlJobCommand(conn, db, sql), new JobProperties());
        }
    }
}
