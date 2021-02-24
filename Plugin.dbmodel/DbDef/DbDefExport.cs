using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DatAdmin;

namespace Plugin.dbmodel
{
    [DatabaseWriter(Name = "ddf_writer", Title = "s_db_structure", Description = "s_ddf_writer_desc", SupportsCreateTemplate = false)]
    public class DatabaseStructureWriter : FileDatabaseWriter
    {
        bool m_forceSingleSchema;
        ISqlDialect m_dialect;
        DatabaseStructure m_db;

        public override DatabaseWriterCaps WriterCaps
        {
            get
            {
                return new DatabaseWriterCaps
                {
                    MultipleSchema = true,
                    AcceptStructure = true,
                    AcceptData = true,
                    PreferStructure = true,
                };
            }
        }

        public override void SetSourceInfo(DatabaseWriterSourceInfo info)
        {
            m_forceSingleSchema = info.SchemaMode == DbCopySchemaMode.Explicit;
        }

        public override void WriteStructureBeforeData(IDatabaseStructure db)
        {
            m_db = new DatabaseStructure(db);
            m_db.ForceSingleSchema = m_forceSingleSchema;
        }

        public override void WriteStructureAfterData(IDatabaseStructure db)
        {
            using (FileStream fw = new FileStream(GetWorkingFileName(), FileMode.Create))
            {
                m_db.Save(fw);
            }
        }

        public override void FillTable(ITableStructure table, IDataQueue queue, TableCopyOptions opts)
        {
            TableStructure dst = null;
            try
            {
                dst = (TableStructure)m_db.Tables[table.FullName];
            }
            catch
            {
                throw new InternalError("DAE-00064 Table not found in target structure:" + table.FullName.ToString());
            }
            InMemoryTable tbl = InMemoryTable.FromEnumerable(queue.GetRowFormat, queue.EnumRows());
            if (tbl.Rows.Count == 0) tbl = null;
            dst.FixedData = tbl;
        }

        public override void CloseConnection()
        {
            base.CloseConnection();
            FinalizeFileName();
        }

        public override string FileExtension
        {
            get { return "ddf"; }
        }
    }


    //public class DbExportStructProcess : Process
    //{
    //    IDatabaseSource m_db;
    //    string m_file;

    //    public DbExportStructProcess(IDatabaseSource db, string file)
    //    {
    //        m_db = db;
    //        m_file = file;
    //    }

    //    protected override void Run()
    //    {
    //        DatabaseStructure s = new DatabaseStructure();
    //        CatalogOverview cat = m_db.LoadCatalogOverview();
    //        foreach (TableOverview table in cat.Tables)
    //        {
    //            Logging.Debug("Saving table {0}", table.FullName);
    //            ITableSource tsrc = m_db.GetTable(table.CatalogName, table.SchemaName, table.TableName);
    //            ITableStructure ts = TableSourceExtension.InvokeLoadStructure(tsrc);
    //            s.Tables.Add(new TableStructure(ts));
    //        }
    //        s.Save(m_file);
    //    }

    //    public override string Title
    //    {
    //        get { return String.Format("{0}->{1}", m_db, m_file); }
    //    }

    //    public static void StartProcess(IDatabaseSource db, string file)
    //    {
    //        DbExportStructProcess proc = new DbExportStructProcess(db, file);
    //        proc.Start();
    //    }
    //}
}
