using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.IO;
using System.Xml;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace DatAdmin
{
    [TabularDataStore(Name = "generic_db_source", SupportsDirectUse = false, SupportsCreateTemplate = false)]
    public class GenericTabularDataStore : GenericConnectionUsage, ITabularDataStore, IAddonInstance, ICustomPropertyPage
    {
        string m_dbname;
        //string m_catalog;
        string m_schema;
        string m_tblname;
        bool m_create_table;

        string m_identityColumn;

        TabularDataStoreMode m_mode;
        //DataTable m_rowFormat;
        ITableStructure m_rowFormat;
        DbCommandBuilder m_builder;

        public GenericTabularDataStore(IPhysicalConnection conn, string dbname, string schema, string tblname)
            : base(conn)
        {
            m_dbname = dbname;
            m_tblname = tblname;
            //m_catalog = catalog;
            m_schema = schema;
            m_builder = m_conn.DbFactory.CreateCommandBuilder();
        }

        #region Serialize support
        [XmlAttrib]
        [DisplayName("s_database")]
        public string dbname
        {
            get { return m_dbname; }
            set { m_dbname = value; }
        }
        [XmlAttrib]
        [DisplayName("s_table")]
        public string tblname
        {
            get { return m_tblname; }
            set { m_tblname = value; }
        }
        [XmlAttrib]
        [DisplayName("s_schema")]
        public string schema
        {
            get { return m_schema; }
            set { m_schema = value; }
        }
        [XmlAttrib]
        public bool create_table
        {
            get { return m_create_table; }
            set { m_create_table = value; }
        }

        public GenericTabularDataStore() { }
        #endregion

        [DisplayName("s_connection")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IStoredConnection GuiStored
        {
            get { return m_conn.StoredConnection; }
        }

        #region ITabularDataStore Members

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public TableCopyOptions CopyOptions { get; set; }

        public void CloseAllResources() { }

        public void LoadFromXml(XmlElement xml)
        {
            this.LoadPropertiesCore(xml);
            IStoredConnection sconn = (IStoredConnection)StoredConnectionAddonType.Instance.LoadAddon(xml.FindElement("Connection"));
            m_conn = sconn.CreatePhysicalConnection();
        }
        public void SaveToXml(XmlElement xml)
        {
            if (m_conn.StoredConnection == null) throw new SerializeNotSupportedException("DAE-00237");
            m_conn.StoredConnection.SaveToXml(xml.AddChild("Connection"));
            this.SavePropertiesCore(xml, true);
        }

        public void ClearCaches() { }

        public IAsyncResult BeginRead(AsyncCallback callback, IDataQueue queue)
        {
            return m_conn.BeginInvoke((Action<IDataQueue>)DoRead, callback, queue);
        }

        public void EndRead(IAsyncResult async)
        {
            m_conn.EndInvoke(async);
        }

        public IAsyncResult BeginWrite(AsyncCallback callback, IDataQueue queue)
        {
            return m_conn.BeginInvoke((Action<IDataQueue>)DoWrite, callback, queue);
        }

        public void EndWrite(IAsyncResult async)
        {
            m_conn.EndInvoke(async);
        }

        [Browsable(false)]
        public TabularDataStoreMode Mode
        {
            get { return m_mode; }
            set { m_mode = value; }
        }

        public IAsyncResult BeginGetRowFormat(AsyncCallback callback)
        {
            return m_conn.BeginInvoke((Func<ITableStructure>)DoGetRowFormat, callback);
        }

        public ITableStructure EndGetRowFormat(IAsyncResult async)
        {
            return (ITableStructure)m_conn.EndInvoke(async);
        }

        public void SetRowFormat(ITableStructure rowFormat)
        {
            m_rowFormat = rowFormat;
        }

        [Browsable(false)]
        public bool ConfigurationNeeded { get { return m_conn == null || create_table; } }
        [Browsable(false)]
        public bool AvailableRowFormat
        {
            get
            {
                if (m_create_table) return false;
                return true;
            }
        }

        public void CheckConfiguration()
        {
            if (create_table)
            {
                if (tblname.IsEmpty())
                {
                    throw new CheckConfigError("DAE-00369 " + Texts.Get("s_table_name_is_not_defined"));
                }
            }
        }

        [Browsable(false)]
        public IProgressInfo ProgressInfo { get; set; }

        [Browsable(false)]
        public ITabularDataOuputStream StreamApi
        {
            get { return null; }
        }

        #endregion

        ITableStructure DoGetRowFormat()
        {
            if (m_rowFormat == null)
            {
                DatabaseStructureMembers dbmem = new DatabaseStructureMembers
                {
                    TableFilter = new List<NameWithSchema> { new NameWithSchema(m_schema, m_tblname) },
                    TableMembers = TableStructureMembers.Columns,
                };
                m_rowFormat = m_conn.Dialect.AnalyseDatabase(m_conn, m_dbname, dbmem, null).Tables[new NameWithSchema(m_schema, m_tblname)];
            }
            return m_rowFormat;
        }

        void DoWrite(IDataQueue queue)
        {
            TableStructure ts;
            if (m_create_table)
            {
                ts = new TableStructure(queue.GetRowFormat);
                ts.FullName = new NameWithSchema(m_schema, m_tblname);
                m_conn.SystemConnection.SafeChangeDatabase(m_dbname);
                m_conn.RunScript(dmp =>
                {
                    dmp.CreateTable(ts);
                });
            }
            else
            {
                ts = new TableStructure(DoGetRowFormat());
            }

            IBulkInserter inserter = m_conn.Dialect.CreateBulkInserter();
            inserter.Connection = m_conn;
            inserter.CopyOptions = CopyOptions.Clone();
            inserter.DatabaseName = m_dbname;
            inserter.DestinationTable = ts;
            inserter.ProgressInfo = ProgressInfo;
            inserter.Run(queue);
        }

        void DoRead(IDataQueue queue)
        {
            try
            {
                m_conn.SystemConnection.SafeChangeDatabase(m_dbname);
                using (DbCommand cmd = m_conn.DbFactory.CreateCommand())
                {
                    var fname = new NameWithSchema(m_schema, m_tblname);
                    string qfullname = m_conn.Dialect.QuoteFullName(fname);
                    cmd.CommandText = "SELECT * FROM " + qfullname;
                    cmd.Connection = m_conn.SystemConnection;
                    try
                    {
                        using (IBedReader reader = m_conn.GetAnyDDA().AdaptReader(cmd.ExecuteReader()))
                        {
                            while (reader.Read())
                            {
                                queue.PutRecord(reader);
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        if (!m_conn.Dialect.DialectCaps.RangeSelect) throw;
                        if (err is ThreadAbortException) throw;

                        // try to load in more packets
                        int tblsize = Int32.Parse(m_conn.SystemConnection.ExecuteScalar("SELECT COUNT(*) FROM " + qfullname).ToString());
                        int ofs = 0;
                        int maxsize = 200;
                        while (ofs < tblsize)
                        {
                            if (ProgressInfo != null)
                            {
                                ProgressInfo.SetCurWork(String.Format("{0}: {1}/{2}", fname, ofs, tblsize));
                            }
                            int pacsize = tblsize - ofs;
                            if (pacsize > maxsize) pacsize = maxsize;

                            using (DbCommand cmd2 = m_conn.DbFactory.CreateCommand())
                            {
                                cmd.CommandText = m_conn.Dialect.GetRangeSelect("SELECT * FROM " + qfullname, ofs, pacsize);
                                cmd.Connection = m_conn.SystemConnection;
                                using (IBedReader reader = m_conn.GetAnyDDA().AdaptReader(cmd.ExecuteReader()))
                                {
                                    while (reader.Read())
                                    {
                                        queue.PutRecord(reader);
                                    }
                                }
                            }

                            ofs += pacsize;
                        }
                    }
                }
            }
            finally
            {
                queue.PutEof();
                if (ProgressInfo != null) ProgressInfo.SetCurWork("");
            }
        }

        public bool SupportsMode(TabularDataStoreMode mode) { return true; }

        public override string ToString()
        {
            return String.Format("{0}:{1}", m_tblname, m_conn);
        }

        #region IAddonInstance Members


        [Browsable(false)]
        public AddonType AddonType
        {
            get { return TabularDataStoreAddonType.Instance; }
        }

        #endregion

        #region ICustomPropertyPage Members

        public Control CreatePropertyPage()
        {
            if (create_table)
            {
                return new TabularDataNewTableFrame(this);
            }
            else
            {
                return new PropertyGrid { SelectedObject = this };
            }
        }

        #endregion
    }
}