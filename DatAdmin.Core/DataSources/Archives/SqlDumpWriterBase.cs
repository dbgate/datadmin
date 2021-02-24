using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace DatAdmin
{
    public class SqlDumpWriterBase : ISqlDumpWriter
    {
        protected StreamWriter m_fw;
        protected ISqlDumper m_dmp;
        ISqlDialect m_dialect;
        private SqlFormatProperties m_formatProps = new SqlFormatProperties();

        public SqlDumpWriterBase(ISqlDialect dialect)
        {
            m_dialect = dialect;
        }

        protected virtual void WriteHeader() { }
        protected virtual void WriteFooter() { }
        protected virtual void GetFormatProps(SqlFormatProperties formatProps) { }

        #region ISqlDumpWriter Members

        public string FileName { get; set; }
        public SqlDumpWriterConfig Config { get; set; }

        public virtual void OpenFile()
        {
            if (m_fw != null) return;
            m_fw = new StreamWriter(FileName);
            m_formatProps.DumpWriterConfig = Config;
            GetFormatProps(m_formatProps);
            //if (m_formatProps.DumpFileBegin != null) m_fw.Write(m_formatProps.DumpFileBegin);
            m_dmp = m_dialect.CreateDumper(m_fw, m_formatProps);
            WriteHeader();
        }
        public virtual void CloseFile()
        {
            if (m_fw != null)
            {
                WriteFooter();
                //if (m_formatProps.DumpFileEnd != null) m_fw.Write(m_formatProps.DumpFileEnd);
                m_fw.Close();
                m_fw = null;
            }
        }

        public virtual void WriteStructureBeforeData(IDatabaseStructure db)
        {
            // create tables without foreign keys
            DatabaseStructure dbcopy = new DatabaseStructure(db);
            foreach (TableStructure tbl in dbcopy.Tables) tbl.RemoveConstraints<IForeignKey>();
            m_dmp.CreateDatabaseObjects(dbcopy, new CreateDatabaseObjectsProps
            {
                CreateSpecificObjects = false
            });
        }

        public virtual void WriteStructureAfterData(IDatabaseStructure db)
        {
            // create other database objects than tables and create foreign keys
            foreach (var table in db.Tables)
            {
                m_dmp.CreateConstraints(table.GetConstraints<IForeignKey, IConstraint>());
            }

            m_dmp.CreateDatabaseObjects(db, new CreateDatabaseObjectsProps
            {
                CreateSchemata = false,
                CreateTables = false,
                CreateFixedData = false,
                CreateDomains = false,
            });
        }

        public virtual void BeforeFillData()
        {
        }

        public virtual void FillTable(ITableStructure table, IDataQueue queue, TableCopyOptions opts)
        {
            var colnames = from c in queue.GetRowFormat.Columns select c.ColumnName;
            bool autoinc = queue.GetRowFormat.FindAutoIncrementColumn() != null;
            if (autoinc) m_dmp.AllowIdentityInsert(table.FullName, true);
            try
            {
                while (!queue.IsEof)
                {
                    IBedRecord row = queue.GetRecord();
                    m_dmp.PutCmd("^insert ^into %f (%,i) ^values (%,v)", table, colnames, row);
                }
            }
            finally
            {
                queue.CloseReading();
            }
            if (autoinc) m_dmp.AllowIdentityInsert(table.FullName, false);
        }

        public virtual void AfterFillData()
        {
        }

        #endregion
    }
}
