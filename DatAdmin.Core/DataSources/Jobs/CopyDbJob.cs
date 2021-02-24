using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;

namespace DatAdmin
{
    [JobCommand(Name = "copydb")]
    public class CopyDbJobCommand : JobCommand
    {
        IDatabaseSource m_src;
        IDatabaseWriter m_dst;
        DatabaseCopyOptions m_copyOpts;

        public CopyDbJobCommand(IDatabaseSource src, IDatabaseWriter dst, DatabaseCopyOptions copyOpts)
        {
            m_src = src;
            m_dst = dst;
            m_copyOpts = copyOpts;
        }

        public CopyDbJobCommand() { }

        protected override void DoRun(IJobRunEnv env)
        {
            CopyDbJob.CopyDatabase(m_src, m_dst, ProgressInfo, m_copyOpts);
        }

        public override string ToString()
        {
            return String.Format("{0}->{1}", m_src, m_dst);
        }


        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            m_src = (IDatabaseSource)DatabaseSourceAddonType.Instance.LoadAddon(xml.FindElement("Source"));
            m_dst = (IDatabaseWriter)DatabaseWriterAddonType.Instance.LoadAddon(xml.FindElement("Target"));
            m_copyOpts = new DatabaseCopyOptions();
            m_copyOpts.LoadFromXml(xml.FindElement("Options"));
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            xml.SetAttribute("type", "copydb");
            m_src.SaveToXml(xml.AddChild("Source"));
            m_dst.SaveToXml(xml.AddChild("Target"));
            m_copyOpts.SaveToXml(xml.AddChild("Options"));
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("s_source")]
        public IDatabaseSource Source { get { return m_src; } }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("s_target")]
        public IDatabaseWriter Target { get { return m_dst; } }

        public override string TypeTitle
        {
            get { return "s_copy"; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.copy; }
        }

        public override void GetUsageParams(UsageBuilder ub)
        {
            ub["src"] = m_src.SafeToString();
            ub["dst"] = m_dst.SafeToString();
        }
    }

    public static class CopyDbJob
    {
        public static Job CreateJob(IDatabaseSource src, IDatabaseWriter dst, DatabaseCopyOptions copyOpts, JobProperties jobProps)
        {
            return Job.FromCommand(new CopyDbJobCommand(src, dst, copyOpts), jobProps);
        }

        public static void CopyDatabase(IDatabaseSource src, IDatabaseWriter dst, IProgressInfo progress, DatabaseCopyOptions copyOpts)
        {
            IDatabaseWriter dst2 = null;
            for (; ; )
            {
                dst2 = dst.GetRedirectedWriter();
                if (dst2 == null) break;
                dst = dst2;
            }
            dst.SetSourceInfo(new DatabaseWriterSourceInfo
            {
                Dialect = src.Dialect,
                //CopyMode = copyOpts.Mode,
                SchemaMode = copyOpts.SchemaMode,
            });
            try
            {
                dst.ProgressInfo = progress;

                Async.SafeOpen(src.Connection);
                dst.OpenConnection();

                if (dst.DirectCopy(src))
                {
                    dst.RunDirectCopy(src, copyOpts);
                }
                else
                {
                    copyOpts.CopyMembers.IgnoreSystemObjects = true;
                    IDatabaseStructure tmpDb = src.InvokeLoadStructure(copyOpts.CopyMembers, progress);
                    DatabaseStructure sourceDb = new DatabaseStructure(tmpDb);
                    //sourceDb.AutoFillRefs();
                    DatabaseStructure targetDb = sourceDb.GetMappedDatabase(copyOpts, dst.Dialect);
                    if (dst.Dialect != null) dst.Dialect.MigrateDatabase(targetDb, copyOpts.MigrationProfile, progress);

                    if (copyOpts.CopyStructure) dst.WriteStructureBeforeData(targetDb);

                    bool copydata = copyOpts.DataMode != DbCopyDataMode.None && src.TableCaps.DataStoreForReading && dst.WriterCaps.AcceptData;
                    if (copydata)
                    {
                        dst.BeforeFillData();

                        foreach (var tbl in sourceDb.Tables.SortedByKey<ITableStructure, int>(tbl => copyOpts.DataCopyTables.IndexOf(tbl.FullName)))
                        {
                            if (!copyOpts.CopyTableData(tbl.FullName)) continue;
                            Logging.Debug("Copying table {0}", tbl);
                            if (progress != null) progress.SetCurWork(String.Format("{0} {1}", Texts.Get("s_copying_table"), tbl));
                            GenericDataQueue queue = new GenericDataQueue(tbl, tbl, new IdentityTransform(tbl));
                            queue.ProgressInfo = progress;
                            if (dst.WriterCaps.ExecuteSql)
                            {
                                var ada = new RecordToDbAdapter(tbl, tbl, dst.Dialect, new DataFormatSettings());
                                ada.ProgressInfo = progress;
                                queue.AddOutputAdapter(ada);
                            }
                            ITableSource tsrc = src.GetTable(tbl.FullName);
                            ITabularDataStore srcds = tsrc.GetDataStoreAndReuse();
                            IAsyncResult async_src = srcds.BeginRead(null, queue);
                            ITableStructure newTableStruct = (ITableStructure)targetDb.FindByGroupId(tbl.GroupId);
                            dst.FillTable(newTableStruct, queue, copyOpts.TableOptions);
                            srcds.EndRead(async_src);
                        }
                        dst.AfterFillData();
                    }
                    if (copyOpts.CopyStructure) dst.WriteStructureAfterData(targetDb);
                }
            }
            catch (Exception)
            {
                dst.ProcessFailed();
                throw;
            }
            finally
            {
                Async.SafeClose(src.Connection);
                dst.CloseConnection();
            }
        }
    }
}

