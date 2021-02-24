using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.IO;

namespace Plugin.datasyn
{
    public enum LogReportMode { All, None, Errors, Succeeded }

    public class DataSynSqlReportFactory : JobReportFactoryBase
    {
        public override IJobReportConfiguration CreateConfig()
        {
            return new DataSynSqlReportConfiguration();
        }

        public override string ToString()
        {
            return String.Format("SQL ({0})", RelatedCommand);
        }
    }

    public class DataSynInfoReportFactory : JobReportFactoryBase
    {
        public override IJobReportConfiguration CreateConfig()
        {
            return new DataSynInfoReportConfiguration();
        }

        public override string ToString()
        {
            return String.Format("Info ({0})", RelatedCommand);
        }
    }

    [JobReportConfiguration(Name = "datasynsql", Title = "Data synchronization SQL report")]
    public class DataSynSqlReportConfiguration : JobReportConfigurationBase
    {
        public override string FileExtension
        {
            get { return "txt"; }
        }

        public DataSynSqlReportConfiguration()
        {
            LogUpdates = LogReportMode.All;
            LogInserts = LogReportMode.All;
            LogDeletes = LogReportMode.All;
        }

        [XmlElem]
        public LogReportMode LogUpdates { get; set; }

        [XmlElem]
        public LogReportMode LogInserts { get; set; }

        [XmlElem]
        public LogReportMode LogDeletes { get; set; }

        public string GetDelimiter()
        {
            return "--------------------------------------------------------------";
        }

        public LogReportMode GetModeForOperation(DataSynOperation operation)
        {
            switch (operation)
            {
                case DataSynOperation.Insert:
                    return LogInserts;
                case DataSynOperation.Delete:
                    return LogDeletes;
                case DataSynOperation.Update:
                    return LogUpdates;
            }
            return LogReportMode.None;
        }

        public override IJobReportProcessor CreateProcessor(JobCommand cmd)
        {
            return new DataSynSqlReportProcessor(this, cmd);
        }

        public string GetCommandStart()
        {
            return GetDelimiter();
        }

        public override string ToString()
        {
            return "SQL:" + FilePlace.ToString();
        }
    }


    [JobReportConfiguration(Name = "datasyninfo", Title = "Data synchronization information")]
    public class DataSynInfoReportConfiguration : FormattedJobReportConfigurationBase
    {
        public override IJobReportProcessor CreateProcessor(JobCommand cmd)
        {
            return new DataSynInfoReportProcessor(this, cmd);
        }

        public override string ToString()
        {
            return "Info:" + FilePlace.ToString();
        }
    }

    public interface IDataSynSqlReportProcessor
    {
        void OnSendScript(IDataSynScriptWrapper wrapper, Exception err, DateTime started, DateTime finished);
    }

    public interface IDataSynInfoReportProcessor
    {
        void OnLogResult(NameWithSchema table, int updated, int inserted, int deleted);
        void OnLogResultError(NameWithSchema table, Exception err);
    }

    public class DataSynSqlReportProcessor : JobReportProcessorBase, IDataSynSqlReportProcessor
    {
        StreamWriter fw;

        public DataSynSqlReportProcessor(DataSynSqlReportConfiguration config, JobCommand command)
            : base(config, command)
        {
        }

        public override void OnStart()
        {
            base.OnStart();
            fw = new StreamWriter(WorkingFileName);
        }

        public override void OnFinish()
        {
            fw.Close();
            base.OnFinish();
        }

        public override void Dispose()
        {
            base.Dispose();
            fw.Dispose();
        }

        public void OnSendScript(IDataSynScriptWrapper wrapper, Exception err, DateTime started, DateTime finished)
        {
            var cfg = (DataSynSqlReportConfiguration)Config;
            var mode = cfg.GetModeForOperation(wrapper.Operation);
            if (mode == LogReportMode.All 
                || (mode == LogReportMode.Errors && err != null) 
                || (mode == LogReportMode.Succeeded && err == null))
            {
                fw.WriteLine(cfg.GetCommandStart());
                fw.WriteLine("{0}, started: {1}, len: {2} bytes, duration: {3:0.0} ms, result: {4}",
                    wrapper.Operation, started, wrapper.Script.Length, (finished - started).TotalMilliseconds, err == null ? "OK" : err.Message);
                if (err != null) fw.WriteLine(err.ToString());
                fw.WriteLine(cfg.GetDelimiter());
                fw.WriteLine(wrapper.Script);
                fw.Flush();
            }
        }
    }

    public class DataSynInfoReportProcessor : FormattedJobReportProcessorBase, IDataSynInfoReportProcessor
    {
        List<Exception> m_errors = new List<Exception>();

        public DataSynInfoReportProcessor(DataSynInfoReportConfiguration config, JobCommand cmd)
            : base(config, cmd)
        {
        }

        public override void OnStart()
        {
            base.OnStart();
            Formatter.Heading(Command.ToString(), 2);
            Formatter
                .BeginTable(new TableStyle { Border = 1 })
                .BeginRow(true)
                .Cell("s_table")
                .Cell("s_updated")
                .Cell("s_inserted")
                .Cell("s_deleted")
                .EndRow(true);
        }

        public override void OnFinish()
        {
            Formatter.EndTable();

            if (m_errors.Count > 0)
            {
                Formatter.Heading("s_errors", 2);
                Formatter.BeginTable(new TableStyle { Border = 1 });
                for (int i = 0; i < m_errors.Count; i++)
                {
                    Formatter
                        .BeginRow(false)
                        .Cell(i + 1)
                        .Cell(m_errors[i].Message)
                        .EndRow(false);
                }
                Formatter.EndTable();
            }

            base.OnFinish();
        }

        #region IDataSynInfoReportProcessor Members

        public void OnLogResultError(NameWithSchema table, Exception err)
        {
            m_errors.Add(err);
            string cell = "(ERROR #" + m_errors.Count.ToString() + ")";
            Formatter
                .BeginRow(false)
                .Cell(cell)
                .Cell(cell)
                .Cell(cell)
                .EndRow(false);
        }

        public void OnLogResult(NameWithSchema table, int updated, int inserted, int deleted)
        {
            Formatter
                .BeginRow(false)
                .Cell(table)
                .Cell(updated)
                .Cell(inserted)
                .Cell(deleted)
                .EndRow(false);
        }

        #endregion
    }

    public class DataSynReportEnv : JobReportEnvBase, IDataSynReportEnv
    {
        public DataSynReportEnv(JobCommand cmd)
            : base(cmd)
        {
        }

        public void LogResult(NameWithSchema table, int updated, int inserted, int deleted)
        {
            foreach (var proc in m_processors)
            {
                var p1 = proc as IDataSynInfoReportProcessor;
                if (p1 != null) p1.OnLogResult(table, updated, inserted, deleted);
            }
        }

        #region IDataSynReportEnv Members

        public void SendScriptWrapper(IDataSynScriptWrapper wrapper)
        {
            Exception err = null;
            var started = DateTime.Now;
            try
            {
                wrapper.Run();
            }
            catch (Exception e)
            {
                err = e;
            }
            var finished = DateTime.Now;
            foreach (var proc in m_processors)
            {
                try
                {
                    var p1 = proc as IDataSynSqlReportProcessor;
                    if (p1 != null) p1.OnSendScript(wrapper, err, started, finished);
                }
                catch { }
            }
            if (err != null) throw err;
        }

        #endregion

        public void LogResultError(NameWithSchema table, Exception err)
        {
            foreach (var proc in m_processors)
            {
                var p1 = proc as IDataSynInfoReportProcessor;
                if (p1 != null) p1.OnLogResultError(table, err);
            }
        }
    }

    public class DummyDataSynReportEnv : IDataSynReportEnv
    {
        #region IDataSynReportEnv Members

        public void SendScriptWrapper(IDataSynScriptWrapper wrapper)
        {
            wrapper.Run();
        }

        #endregion
    }
}
