using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.IO;

namespace DatAdmin
{
    public class DumpLoaderBase : IDumpLoader
    {
        FileStream m_file;
        DateTime m_lastinfo = DateTime.Now;
        protected ISqlDialect m_dialect;
        const int FAIL_COUNT_LIMIT = 200;
        int m_okCount = 0, m_failCount = 0;

        public DumpLoaderBase(ISqlDialect dialect)
        {
            m_dialect = dialect;
        }

        protected virtual bool UseSpecificSplitter
        {
            get { return false; }
        }

        #region IDumpLoader Members

        public DbConnection Connection { get; set; }
        public DbTransaction Transaction { get; set; }
        public IProgressInfo ProgressInfo { get; set; }
        public IDumpLoaderConfig Config { get; set; }

        public virtual void Run(TextReader reader)
        {
            if (UseSpecificSplitter)
            {
                var splitter = m_dialect.CreateQuerySplitter();
                foreach (var item in splitter.Run(reader))
                {
                    ShowProgress();
                    try
                    {
                        ExecuteDumpQuery(item.Data);
                        OkInc();
                    }
                    catch (Exception err)
                    {
                        ReportFail(item.Data, err);
                    }
                }
            }
            else
            {
                foreach (string sql in QueryTools.GoSplit(reader))
                {
                    ShowProgress();
                    try
                    {
                        ExecuteDumpQuery(sql);
                        OkInc();
                    }
                    catch (Exception err)
                    {
                        ReportFail(sql, err);
                    }
                }
            }
            ReportFinish();
        }

        protected virtual void ExecuteDumpQuery(string sql)
        {
            Connection.ExecuteNonQuery(sql, m_dialect, Transaction, null);
        }

        protected void ReportFinish()
        {
            if (ProgressInfo != null)
            {
                if (m_failCount > 0)
                {
                    ProgressInfo.LogMessage("s_import", LogLevel.Warning, "{0} OK:{1}, FAIL:{2}", Texts.Get("s_imported"), m_okCount, m_failCount);
                }
                else
                {
                    ProgressInfo.LogMessage("s_import", LogLevel.Info, "{0} OK:{1}", Texts.Get("s_imported"), m_okCount);
                }
            }
        }

        protected void ReportFail(string sql, Exception err)
        {
            m_failCount++;
            Logging.Error("Error running command from SQL dump:" + err.ToString());
            if (ProgressInfo != null && m_failCount < FAIL_COUNT_LIMIT)
            {
                ProgressInfo.Error(err.Message);
                ProgressInfo.LogMessageDetail("s_import", LogLevel.Error, Texts.Get("s_error_importing_dump_command"), sql.TrimRightNice(300));
                ProgressInfo.RaiseError("DAE-00156 " + Texts.Get("s_error_importing_dump_command"));
            }
            if (ProgressInfo != null && m_failCount == FAIL_COUNT_LIMIT)
            {
                ProgressInfo.LogMessage("s_import", LogLevel.Error, "s_report_contains_to_many_errors");
            }
        }

        protected void OkInc()
        {
            m_okCount++;
        }

        protected void ShowProgress()
        {
            if (ProgressInfo != null & m_file != null && (DateTime.Now - m_lastinfo).Seconds > 1)
            {
                ProgressInfo.SetCurWork(String.Format("{0} {1} KB/{2}", Texts.Get("s_imported"), m_file.Position / 1000, m_file.Length / 1000));
                m_lastinfo = DateTime.Now;
            }
        }

        protected void InitProgress(Stream fr)
        {
            m_file = fr as FileStream;
        }

        public virtual void Run(Stream fr)
        {
            InitProgress(fr);

            using (StreamReader sr = new StreamReader(fr))
            {
                Run(sr);
            }
        }

        #endregion
    }
}
