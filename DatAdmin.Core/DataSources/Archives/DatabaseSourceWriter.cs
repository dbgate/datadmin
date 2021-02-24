using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;

namespace DatAdmin
{
    [DatabaseWriter(Name = "dbsource", SupportsDirectUse = false)]
    public class DatabaseSourceWriter : DatabaseWriterBase
    {
        IDatabaseSource m_db;

        public DatabaseSourceWriter(IDatabaseSource db)
        {
            m_db = db;
        }

        public DatabaseSourceWriter() { }

        public override void OpenConnection()
        {
            Async.SafeOpen(m_db.Connection);
        }

        //private void WriteLog(string message)
        //{
        //    CallLogMessage(new LogEventArgs { Message = message });
        //}

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            m_db.SaveToXml(xml.AddChild("Database"));
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            m_db = (IDatabaseSource)DatabaseSourceAddonType.Instance.LoadAddon(xml.FindElement("Database"));
        }

        public override void WriteStructureBeforeData(IDatabaseStructure db)
        {
            if (m_db.DatabaseCaps.ExecuteSql)
            {
                DatabaseStructure dbcopy = new DatabaseStructure(db);
                if (!Dialect.DialectCaps.UncheckedReferences)
                {
                    foreach (TableStructure tbl in dbcopy.Tables) tbl.RemoveConstraints<IForeignKey>();
                }
                m_db.InvokeScript(dmp => { dmp.CreateDatabaseObjects(dbcopy); }, ProgressInfo);
                m_db.InvokeScript(dmp => SqlTemplates.GenerateInsertFixedData(dmp, dbcopy), ProgressInfo);
            }
            else
            {
                var d0 = new DatabaseStructure();
                d0.GroupId = db.GroupId;
                m_db.AlterDatabase(d0, db);
            }
            //foreach (var tbl in db.Tables)
            //{
            //    Logging.Debug("Creating table {0}", tbl.FullName);
            //    SetCurWork(String.Format("{0} {1}", Texts.Get("s_creating_table"), tbl.FullName));
            //    TableStructure tsNoFk = new TableStructure(tbl);
            //    tsNoFk.RemoveConstraints<IForeignKey>();
            //    m_db.CreateTable(tsNoFk);
            //}
        }

        void SetCurWork(string title)
        {
            if (ProgressInfo != null) ProgressInfo.SetCurWork(title);
        }

        public override void WriteStructureAfterData(IDatabaseStructure db)
        {
            if (ProgressInfo != null)
            {
                ProgressInfo.SetCurWork("s_creating_references");
            }
            if (m_db.DatabaseCaps.ExecuteSql && !m_db.Dialect.DialectCaps.UncheckedReferences)
            {
                m_db.InvokeScript(dmp =>
                {
                    foreach (var table in db.Tables)
                    {
                        foreach (var cnt in table.GetConstraints<IForeignKey, IConstraint>())
                        {
                            try
                            {
                                dmp.CreateConstraint(cnt);
                            }
                            catch (Exception err)
                            {
                                ProgressInfo.LogMessageDetail("REFERENCE", LogLevel.Error, Texts.Get("s_error_creating_reference$fk", "fk", cnt.Name), err.ToString());
                            }
                        }
                    }
                }, ProgressInfo);
            }
            //foreach (var table in m_dbStruct.Tables)
            //{
            //    var tdst = m_db.GetTable(table.FullName);
            //    tdst.CreateConstraints(table.GetConstraints<IForeignKey, IConstraint>());
            //}
        }

        public override void FillTable(ITableStructure table, IDataQueue queue, TableCopyOptions opts)
        {
            var tdst = m_db.GetTable(table.FullName);
            ITabularDataStore dstds = tdst.GetDataStoreAndReuse();
            dstds.CopyOptions = opts;
            dstds.ProgressInfo = ProgressInfo;
            IAsyncResult async = dstds.BeginWrite(null, queue);
            dstds.EndWrite(async);
        }

        [Browsable(false)]
        public override ISqlDialect Dialect
        {
            get { return m_db.Connection.Dialect; }
        }

        public override IDatabaseStructure InvokeLoadStructure(DatabaseStructureMembers members, IProgressInfo progress)
        {
            return m_db.InvokeLoadStructure(members, progress);
        }

        [Browsable(false)]
        public override DatabaseWriterCaps WriterCaps
        {
            get
            {
                return new DatabaseWriterCaps
                {
                    AllFlags = false,
                    AcceptData = m_db.TableCaps.DataStoreForWriting,
                    AcceptStructure = true,
                    HasStructure = true,
                    PreferStructure= m_db.DatabaseCaps.PreferStructure,
                    MultipleSchema = m_db.DatabaseCaps.MultipleSchema,
                    ExecuteSql = m_db.DatabaseCaps.ExecuteSql,
                    AllowDataOnly = true,
                };
            }
        }

        public override string ToString()
        {
            return m_db.ToString();
        }

        [DisplayName("s_database")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IDatabaseSource Database
        {
            get { return m_db; }
        }
    }
}
