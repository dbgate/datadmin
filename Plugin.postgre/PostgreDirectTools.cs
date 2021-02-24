using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using DatAdmin;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Threading;

namespace Plugin.postgre
{
    [SettingsPage(Name = "postgre_client", Title = "Postgre SQL/s_client_settings", Targets = SettingsTargets.Global)]
    public class PostgreSettings : SettingsPageBase
    {
        string m_postgrePath;

        [Category("PostgreSQL")]
        [DatAdmin.DisplayName("s_client_path")]
        [SettingsKey("postgre.client_path")]
        [Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string PostgrePath
        {
            get { return m_postgrePath; }
            set { m_postgrePath = value; }
        }

        public PostgreSettings()
        {
            if (Core.IsMono) m_postgrePath = "/usr/bin";
        }

        internal string GetToolPath(string tool)
        {
            if (PostgrePath == null) throw new BadSettingsError("DAE-00347 " + Texts.Get("s_postgre_path_not_configured"), "Postgre SQL/s_client_settings");
            string path = Path.Combine(PostgrePath, tool + Core.ExeSuffix);
            if (!File.Exists(path)) throw new BadSettingsError("DAE-00348 " + Texts.Get("s_postgre_tool_missing$path", "path", path), "Postgre SQL/s_client_settings");
            return path;
        }

        public static PostgreSqlStoredConnection CheckPostgreSource(IDatabaseSource source)
        {
            PostgreSqlStoredConnection sconn = source.Connection.StoredConnection as PostgreSqlStoredConnection;
            if (sconn == null) throw new InvalidInputError("DAE-00349 " + Texts.Get("s_connection_required$engine", "engine", "PostgreSQL"));
            return sconn;
        }

        public static PostgreSqlStoredConnection CheckPostgreSourceAndTool(IDatabaseSource source, string tool)
        {
            PostgreSettings cfg = GlobalSettings.Pages.PageByName("postgre_client") as PostgreSettings;
            cfg.GetToolPath(tool);
            return CheckPostgreSource(source);
        }
    }

    [DatabaseWriter(Name = "pgdump", Title = "Postgre Native SQL Dump (pg_dump)", Description = "s_pgdump_desc")]
    public class PostgreNativeDumpWriter : FileDatabaseWriter
    {
        bool m_includeDropStatement = true;

        public override bool DirectCopy(IDatabaseSource source)
        {
            return true;
        }

        [Category("SQL")]
        [DatAdmin.DisplayName("s_include_drop_statement")]
        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool IncludeDropStatement
        {
            get { return m_includeDropStatement; }
            set { m_includeDropStatement = value; }
        }

        //bool m_useClientTools = true;

        //[XmlElem]
        //[Category("SQL")]
        //[DatAdmin.DisplayName("s_use_client_tools")]
        //[TypeConverter(typeof(YesNoTypeConverter))]
        //public bool UseClientTools
        //{
        //    get { return m_useClientTools; }
        //    set
        //    {
        //        if (!value && Registration.SoftwareEdition < SoftwareEdition.Professional)
        //        {
        //            Logging.Warning("Edition error: Postgre set use client tools");
        //            throw new BadEditionError(SoftwareEdition.Professional);
        //        }
        //        m_useClientTools = value;
        //    }
        //}

        public override void RunDirectCopy(IDatabaseSource source, DatabaseCopyOptions copyOpts)
        {
            var sconn = PostgreSettings.CheckPostgreSource(source);
            PostgreSettings cfg = GlobalSettings.Pages.PageByName("postgre_client") as PostgreSettings;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = cfg.GetToolPath("pg_dump");
            StringBuilder pars = new StringBuilder();
            pars.AppendFormat("-U {0} ", sconn.Login);
            pars.AppendFormat("--host={0} ", sconn.DataSource);
            p.StartInfo.EnvironmentVariables["PGPASSWORD"] = sconn.Password;
            if (sconn.Port > 0) pars.AppendFormat("--port={0} ", sconn.Port);
            if (IncludeDropStatement) pars.Append("--clean ");
            pars.Append("--verbose ");
            pars.AppendFormat("\"--file={0}\" ", GetWorkingFileName());

            pars.Append(source.DatabaseName ?? sconn.ExplicitDatabaseName);

            p.StartInfo.Arguments = pars.ToString();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            Logging.Debug("Running pg_dump " + pars.ToString());
            p.Start();
            Thread thr = new Thread(() => ReadFromErrStream(p));
            thr.Start();
            p.WaitForExit();
            thr.Join();
            FinalizeFileName();
        }

        private void ReadFromErrStream(System.Diagnostics.Process p)
        {
            while (!p.StandardError.EndOfStream)
            {
                string line = p.StandardError.ReadLine();
                ProgressInfo.LogMessage("postgre", DatAdmin.LogLevel.Info, line);
            }
        }

        //public override IDatabaseWriter GetRedirectedWriter()
        //{
        //    if (!UseClientTools)
        //    {
        //        SqlDatabaseWriter res = new SqlDatabaseWriter();
        //        res.Filename = Filename;
        //        res.IncludeDropStatement = IncludeDropStatement;
        //        res.FormatProps.CommandSeparator = ";\n"; // override dialect default value
        //        res.UsedDialect = new PostgreDialect();
        //        res.FormatProps.DumpFileBegin = "-- DatAdmin Native PostgreSQL Dump\n\n";
        //        return res;
        //    }
        //    return null;
        //}

        public override DatabaseWriterCaps WriterCaps
        {
            get
            {
                return new DatabaseWriterCaps
                {
                    AllFlags = false,
                    AcceptData = true,
                    MultipleSchema = true,
                    AcceptStructure = true,
                };
            }
        }

        public override void CheckConfiguration(IDatabaseSource source)
        {
            base.CheckConfiguration(source);
            PostgreSettings.CheckPostgreSourceAndTool(source, "pg_dump");
        }

        public override string FileExtension
        {
            get { return "sql"; }
        }
    }

    [DatabaseLoader(Name = "pgdump", Title = "Postgre SQL Native Client")]
    public class PostgreDumpLoader : DatabaseLoaderBase
    {
        public override void LoadDatabase(IDatabaseSource dst)
        {
            var sconn = PostgreSettings.CheckPostgreSource(dst);
            PostgreSettings cfg = GlobalSettings.Pages.PageByName("postgre_client") as PostgreSettings;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = cfg.GetToolPath("psql");
            StringBuilder pars = new StringBuilder();
            pars.AppendFormat("-U {0} ", sconn.Login);
            pars.AppendFormat("--host={0} ", sconn.DataSource);
            pars.AppendFormat("--dbname={0} ", dst.DatabaseName ?? sconn.ExplicitDatabaseName);
            p.StartInfo.EnvironmentVariables["PGPASSWORD"] = sconn.Password;
            if (sconn.Port > 0) pars.AppendFormat("--port={0} ", sconn.Port);
            pars.AppendFormat("-f {0} ", Filename);

            p.StartInfo.Arguments = pars.ToString();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            Logging.Debug("Running psql " + pars.ToString());
            p.Start();
            Thread thr = new Thread(() => ReadFromErrStream(p));
            thr.Start();
            p.WaitForExit();
            thr.Join();
        }

        private void ReadFromErrStream(System.Diagnostics.Process p)
        {
            while (!p.StandardError.EndOfStream)
            {
                string line = p.StandardError.ReadLine();
                ProgressInfo.LogMessage("postgre", DatAdmin.LogLevel.Info, line);
            }
        }

        public override void CheckConfiguration(IDatabaseSource source)
        {
            PostgreSettings.CheckPostgreSourceAndTool(source, "psql");
        }

        public override bool IsSqlDumpLoader
        {
            get { return true; }
        }

        public override bool SuitableFor(IDatabaseSource dst)
        {
            return dst.Connection.DbFactory == NpgsqlFactory.Instance;
        }

        public override string GetTitle()
        {
            return "Postgre SQL Native Client";
        }
    }

    [BackupFormat(Name = "pgdump", Title = "Postgre SQL Native SQL Dump")]
    public class PostgreBackupFormat : BackupFormatBase
    {
        public override IDatabaseWriter GetWriter(string file, IDatabaseSource src)
        {
            return new PostgreNativeDumpWriter { FilePlace = FilePlaceAddonType.PlaceFromVirtualFile(file) };
        }

        public override IDatabaseLoader GetLoader(string file, IDatabaseSource dst)
        {
            return new PostgreDumpLoader { Filename = file };
        }

        public override string Extension
        {
            get { return ".sql"; }
        }

        public override void CheckBackupConfiguration(IDatabaseSource src)
        {
            PostgreSettings.CheckPostgreSourceAndTool(src, "pg_dump");
        }

        public override void CheckRestoreConfiguration(IDatabaseSource dst)
        {
            PostgreSettings.CheckPostgreSourceAndTool(dst, "psql");
        }

        public override bool BackupSuitableFor(IDatabaseSource dst)
        {
            return dst.Connection.DbFactory == NpgsqlFactory.Instance;
        }
    }
}
