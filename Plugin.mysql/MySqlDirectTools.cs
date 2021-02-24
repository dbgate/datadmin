using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.ComponentModel;
using System.Drawing.Design;
using MySql.Data.MySqlClient;
using System.IO;
using System.Threading;

namespace Plugin.mysql
{
    [SettingsPage(Name = "mysql_client", Title = "MySQL/s_client_settings", Targets = SettingsTargets.Global)]
    public class MySqlSettings : SettingsPageBase
    {
        string m_mySqlPath;

        [Category("MySQL")]
        [DatAdmin.DisplayName("s_client_path")]
        [SettingsKey("mysql.client_path")]
        [Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string MySqlPath
        {
            get { return m_mySqlPath; }
            set { m_mySqlPath = value; }
        }

        public MySqlSettings()
        {
            if (Core.IsMono) m_mySqlPath = "/usr/bin";
        }

        internal string GetToolPath(string tool)
        {
            if (MySqlPath == null) throw new BadSettingsError("DAE-00336 " + Texts.Get("s_mysql_path_not_configured"), "MySQL/s_client_settings");
            string path = Path.Combine(MySqlPath, tool + Core.ExeSuffix);
            if (!File.Exists(path)) throw new BadSettingsError("DAE-00337 " + Texts.Get("s_mysql_tool_missing$path", "path", path), "MySQL/s_client_settings");
            return path;
        }

        public static MySqlStoredConnection CheckMySqlSource(IDatabaseSource source)
        {
            MySqlStoredConnection sconn = source.Connection.StoredConnection as MySqlStoredConnection;
            if (sconn == null) throw new InvalidInputError("DAE-00338 " + Texts.Get("s_connection_required$engine", "engine", "MySQL"));
            return sconn;
        }

        public static MySqlStoredConnection CheckMySqlSourceAndTool(IDatabaseSource source, string tool)
        {
            MySqlSettings cfg = GlobalSettings.Pages.PageByName("mysql_client") as MySqlSettings;
            cfg.GetToolPath(tool);
            return CheckMySqlSource(source);
        }

    }

    public enum MySqlDateTimeAffinity { DateTime, Timespamp }
    public enum MySqlBlobAffinity { TinyBlob, Blob, MediumBlob, LongBlob }
    public enum MySqlTextAffinity { TinyText, Text, MediumText, LongText}

    public interface IMySqlMigrationProfile : IMigrationProfile
    {
        MySqlDateTimeAffinity DateTimeAffinity { get; set; }
        MySqlBlobAffinity BlobAffinity { get; set; }
        MySqlTextAffinity TextAffinity { get; set; }
    }

    [SettingsPage(Name = "mysql_migration", Title = "MySQL/s_migration_profile", Targets = SettingsTargets.Global)]
    public class MySqlMigrationProfile : MigrationProfileBase, IMySqlMigrationProfile
    {
        public MySqlMigrationProfile()
        {
            DateTimeAffinity = MySqlDateTimeAffinity.DateTime;
            BlobAffinity = MySqlBlobAffinity.Blob;
            TextAffinity = MySqlTextAffinity.Text;
        }

        [Category("Types")]
        [SettingsKey("mysql.migration.types.date_time_affinity")]
        [XmlElem]
        [DatAdmin.DisplayName("s_preferred_datetime_type")]
        public MySqlDateTimeAffinity DateTimeAffinity { get; set; }

        [Category("Types")]
        [SettingsKey("mysql.migration.types.blob_affinity")]
        [XmlElem]
        [DatAdmin.DisplayName("s_preferred_blob_type")]
        public MySqlBlobAffinity BlobAffinity { get; set; }

        [Category("Types")]
        [SettingsKey("mysql.migration.types.text_affinity")]
        [XmlElem]
        [DatAdmin.DisplayName("s_preferred_text_type")]
        public MySqlTextAffinity TextAffinity { get; set; }
    }

    [DatabaseWriter(Name = "mysqldump", Title = "MySQL Native Client Dump (mysqldump)", Description = "s_mysqldump_desc")]
    public class MySqlNativeDumpWriter : FileDatabaseWriter
    {
        bool m_includeDropStatement = true;

        [Category("SQL")]
        [DatAdmin.DisplayName("s_include_drop_statement")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [XmlElem]
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
        //            Logging.Warning("Edition error: MySQL set use client tools");
        //            throw new BadEditionError(SoftwareEdition.Professional);
        //        }
        //        m_useClientTools = value;
        //    }
        //}

        public override DatabaseWriterCaps WriterCaps
        {
            get
            {
                return new DatabaseWriterCaps
                {
                    AllFlags = false,
                    AcceptData = true,
                    MultipleSchema = false,
                    AcceptStructure = true,
                };
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
        //        res.FormatProps.SpecificData["mysql.dump_delim_mode"] = "1";
        //        res.UsedDialect = new MySqlDialect();
        //        res.FormatProps.DumpFileBegin = "-- DatAdmin Native MySQL Dump\n\n/*!40101 SET NAMES utf8 */;\n/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;\n\n";
        //        res.FormatProps.DumpFileEnd = "\n\n;/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;\n";
        //        return res;
        //    }
        //    return null;
        //}

        public override bool DirectCopy(IDatabaseSource source)
        {
            return true;
        }

        public override void RunDirectCopy(IDatabaseSource source, DatabaseCopyOptions copyOpts)
        {
            MySqlStoredConnection sconn = MySqlSettings.CheckMySqlSource(source);
            MySqlSettings cfg = GlobalSettings.Pages.PageByName("mysql_client") as MySqlSettings;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = cfg.GetToolPath("mysqldump");
            StringBuilder pars = new StringBuilder();
            pars.AppendFormat("--user={0} ", sconn.Login);
            pars.AppendFormat("--password={0} ", sconn.Password);
            pars.AppendFormat("--host={0} ", sconn.DataSource);
            pars.AppendFormat("--port={0} ", sconn.Port);
            pars.Append("--routines ");
            if (IncludeDropStatement) pars.Append("--add-drop-table ");
            else pars.Append("--skip-add-drop-table ");

            pars.Append("--verbose ");
            pars.Append(source.DatabaseName ?? sconn.ExplicitDatabaseName);
            pars.Append(" ");

            if (copyOpts.CopyMembers.TableFilter != null)
            {
                pars.Append("--tables ");
                foreach (NameWithSchema table in copyOpts.CopyMembers.TableFilter)
                {
                    pars.Append(table.Name + " ");
                }
            }

            p.StartInfo.Arguments = pars.ToString();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            Logging.Debug("Running mysqldump " + pars.ToString());
            p.Start();
            Thread thr = new Thread(() => ReadFromErrStream(p));
            thr.Start();
            //Console.Write(p.StandardOutput.ReadToEnd());
            using (FileStream fw = new FileStream(GetWorkingFileName(), FileMode.Create))
            {
                IOTool.CopyStream(p.StandardOutput.BaseStream, fw);
            }
            p.WaitForExit();
            thr.Join();
            FinalizeFileName();
        }

        private void ReadFromErrStream(System.Diagnostics.Process p)
        {
            while (!p.StandardError.EndOfStream)
            {
                string line = p.StandardError.ReadLine();
                ProgressInfo.LogMessage("mysql", LogLevel.Info, line);
            }
        }

        public override void CheckConfiguration(IDatabaseSource source)
        {
            base.CheckConfiguration(source);
            MySqlSettings.CheckMySqlSourceAndTool(source, "mysqldump");
        }

        public override string FileExtension
        {
            get { return "sql"; }
        }
    }

    [DatabaseLoader(Name = "mysqldump", Title = "MySQL Native Client")]
    public class MySqlNativeClientDumpLoader : DatabaseLoaderBase
    {
        public override void LoadDatabase(IDatabaseSource dst)
        {
            MySqlStoredConnection sconn = MySqlSettings.CheckMySqlSource(dst);
            MySqlSettings cfg = GlobalSettings.Pages.PageByName("mysql_client") as MySqlSettings;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = cfg.GetToolPath("mysql");
            StringBuilder pars = new StringBuilder();
            pars.AppendFormat("--user={0} ", sconn.Login);
            pars.AppendFormat("--password={0} ", sconn.Password);
            pars.AppendFormat("--host={0} ", sconn.DataSource);
            pars.AppendFormat("--port={0} ", sconn.Port);

            pars.Append("--verbose ");
            pars.Append(dst.DatabaseName ?? sconn.ExplicitDatabaseName);
            pars.Append(" ");

            p.StartInfo.Arguments = pars.ToString();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardError = true;
            Logging.Debug("Running mysql " + pars.ToString());
            p.Start();
            Thread thr = new Thread(() => ReadFromErrStream(p));
            thr.Start();
            using (FileStream fr = new FileInfo(Filename).OpenRead())
            {
                IOTool.CopyStream(fr, p.StandardInput.BaseStream);
            }
            p.StandardInput.Close();
            p.WaitForExit();
            thr.Join();
        }

        private void ReadFromErrStream(System.Diagnostics.Process p)
        {
            while (!p.StandardError.EndOfStream)
            {
                string line = p.StandardError.ReadLine();
                ProgressInfo.LogMessage("mysql", LogLevel.Info, line);
            }
        }

        public override void CheckConfiguration(IDatabaseSource target)
        {
            MySqlSettings.CheckMySqlSourceAndTool(target, "mysql");
        }

        public override bool IsSqlDumpLoader
        {
            get { return true; }
        }

        public override bool SuitableFor(IDatabaseSource dst)
        {
            return dst.Connection.DbFactory == MySqlClientFactory.Instance;
        }

        public override string GetTitle()
        {
            return "MySQL Native Client";
        }
    }

    [BackupFormat(Name = "mysqldump", Title = "Native MySQL Dump")]
    public class MySqlBackupFormat : BackupFormatBase
    {
        public override IDatabaseWriter GetWriter(string file, IDatabaseSource src)
        {
            return new MySqlNativeDumpWriter { FilePlace = FilePlaceAddonType.PlaceFromVirtualFile(file) };
        }

        public override IDatabaseLoader GetLoader(string file, IDatabaseSource dst)
        {
            return new MySqlNativeClientDumpLoader { Filename = file };
        }

        public override string Extension
        {
            get { return ".sql"; }
        }

        public override void CheckBackupConfiguration(IDatabaseSource src)
        {
            MySqlSettings.CheckMySqlSourceAndTool(src, "mysqldump");
        }

        public override void CheckRestoreConfiguration(IDatabaseSource dst)
        {
            MySqlSettings.CheckMySqlSourceAndTool(dst, "mysql");
        }

        public override bool BackupSuitableFor(IDatabaseSource dst)
        {
            return dst.Connection.DbFactory == MySqlClientFactory.Instance;
        }
    }
}
