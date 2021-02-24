using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DatAdmin
{
    //[JobCommand(Name = "dbdefexport")]
    //public class DbExportJobCommand : JobCommand
    //{
    //    IDatabaseSource m_db;
    //    string m_file;

    //    public DbExportJobCommand(IDatabaseSource db, string file)
    //    {
    //        m_db = db;
    //        m_file = file;
    //    }

    //    public DbExportJobCommand() { }

    //    protected override void DoRun(IJobRunEnv env)
    //    {
    //        IDatabaseStructure dbs = m_db.LoadDatabaseStructure(DatabaseStructureMembers.FullStructure, ProgressInfo);
    //        DatabaseStructure s = new DatabaseStructure(dbs);
    //        //CatalogOverview cat = m_db.LoadCatalogOverview();
    //        //foreach (TableOverview table in cat.Tables)
    //        //{
    //        //    Logging.Debug("Saving table {0}", table.FullName);
    //        //    ITableSource tsrc = m_db.GetTable(table.CatalogName, table.SchemaName, table.TableName);
    //        //    ITableStructure ts = TableSourceExtension.InvokeLoadStructure(tsrc);
    //        //    s.Tables.Add(new TableStructure(ts));
    //        //}
    //        s.Save(m_file);
    //    }

    //    [XmlElem]
    //    public string Filename
    //    {
    //        get { return m_file; }
    //        set { m_file = value; }
    //    }

    //    public override string ToString()
    //    {
    //        return String.Format("{0}->{1}", m_db, m_file);
    //    }

    //    public override void SaveToXml(XmlElement xml)
    //    {
    //        this.SaveProperties(xml, true);
    //        m_db.SaveToXml(xml.AddChild("Source"));
    //    }

    //    public override void LoadFromXml(XmlElement xml)
    //    {
    //        this.LoadProperties(xml);
    //    }
    //}

    //public static class DbDefExportJob
    //{
    //    public static Job CreateJob(IDatabaseSource db, string file, JobProperties jobProps)
    //    {
    //        return Job.FromCommand(new DbExportJobCommand(db, file), jobProps);
    //    }
    //}
}
