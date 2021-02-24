using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace DatAdmin
{
    [JobCommand(Name = "generatesql")]
    public class GenerateSqlJobCommand : JobCommand
    {
        [XmlSubElem]
        public IAppObjectSqlGenerator Generator { get; set; }

        [XmlSubElem]
        public IFilePlace Place { get; set; }

        [XmlElem]
        public ISqlDialect Dialect { get; set; }

        [XmlCollection(typeof(AppObject))]
        public List<AppObject> Args { get; set; }

        [XmlSubElem]
        public SqlFormatProperties FormatProps { get; set; }

        public GenerateSqlJobCommand()
        {
            Args = new List<AppObject>();
            FormatProps = new SqlFormatProperties();
        }

        public GenerateSqlJobCommand(IAppObjectSqlGenerator generator, IFilePlace place, ISqlDialect dialect, AppObject[] objs, ConnectionPack connpack, SqlFormatProperties formatProps)
            : this()
        {
            Generator = generator;
            Place = place;
            Dialect = dialect;
            Args.AddRange(objs);
            ConnPack = connpack;
            FormatProps = formatProps;
        }

        public override string TypeTitle
        {
            get { return "s_generate_sql"; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.generate_sql; }
        }

        protected override void DoRun(IJobRunEnv env)
        {
            var dbconn = Args[0].FindDatabaseConnection(ConnPack);
            if (dbconn != null) Async.SafeOpen(dbconn.Connection);

            Place.ProgressInfo = ProgressInfo;
            Generator.ProgressInfo = ProgressInfo;

            var holder = new ExtendedFileNameHolderInfo
            {
                DirectionIsSave = true,
                FileExtension = "sql",
                Filter = "*.sql|{s_sql_script} (*.sql)",
                RelatedConnection = Args[0].GetConnection(),
                RelatedDatabase = Args[0].FindDatabaseName()
            };
            Place.SetFileHolderInfo(holder);

            string fn = Place.GetWorkingFileName();
            using (var tw = new StreamWriter(fn))
            {
                var dmp = (Dialect ?? (dbconn != null ? dbconn.Dialect : null) ?? (Args[0].Dialect != null ? Args[0].Dialect : null) ?? GenericDialect.Instance).CreateDumper(tw, FormatProps);
                foreach (var obj in Args)
                {
                    if (dbconn != null)
                    {
                        Generator.GenerateSql(dbconn, obj.GetFullDatabaseRelatedName(), dmp, dmp.Dialect);
                    }
                    else
                    {
                        Generator.GenerateSql(obj, dmp, dmp.Dialect);
                    }
                }
            }
            Place.FinalizeFileName();
        }

        public override string ToString()
        {
            return String.Format("GENSQL({0}):{1}[{2}]", Generator, Args[0], Args.Count);
        }
    }

    public class GenerateDataSqlJobCommand : JobCommand
    {
        [XmlSubElem]
        public IDataSqlGenerator Generator { get; set; }

        [XmlSubElem]
        public IFilePlace Place { get; set; }

        [XmlElem]
        public ISqlDialect Dialect { get; set; }

        [XmlSubElem]
        public SqlFormatProperties FormatProps { get; set; }

        DataFrameRowsExtractor m_rows; // cannot be serialized now
        TableDataFrame m_dataFrame;
        ISqlDumper m_dmp;

        public GenerateDataSqlJobCommand(IDataSqlGenerator generator, IFilePlace place, ISqlDialect dialect, TableDataFrame dataFrame, DataFrameRowsExtractor rows, ConnectionPack connpack, SqlFormatProperties formatProps)
        {
            Generator = generator;
            Place = place;
            Dialect = dialect;
            ConnPack = connpack;
            m_rows = rows;
            m_dataFrame = dataFrame;
            FormatProps = formatProps;
        }

        public override void SaveToXml(XmlElement xml)
        {
            throw new NotImplementedError("DAE-00103");
        }

        protected override void DoRun(IJobRunEnv env)
        {
            IPhysicalConnection dbconn = null;
            if (m_dataFrame.TabularData.Connection != null)
            {
                ConnPack.GetConnection(m_dataFrame.TabularData.Connection.PhysicalFactory, false);
                Async.SafeOpen(dbconn);
            }

            Place.ProgressInfo = ProgressInfo;
            Generator.ProgressInfo = ProgressInfo;

            var holder = new ExtendedFileNameHolderInfo
            {
                DirectionIsSave = true,
                FileExtension = "sql",
                Filter = "*.sql|{s_sql_script} (*.sql)",
                RelatedConnection = m_dataFrame.TabularData.Connection != null ? m_dataFrame.TabularData.Connection.PhysicalFactory : null,
                RelatedDatabase = m_dataFrame.TabularData.DatabaseSource != null ? m_dataFrame.TabularData.DatabaseSource.DatabaseName : null,
            };
            Place.SetFileHolderInfo(holder);

            string fn = Place.GetWorkingFileName();
            using (var tw = new StreamWriter(fn))
            {
                m_dmp = (Dialect ?? (dbconn != null ? dbconn.Dialect : null) ?? m_dataFrame.TabularData.Dialect ?? GenericDialect.Instance).CreateDumper(tw, FormatProps);
                if (Generator.IsRowEnumerator)
                {
                    m_rows.LoadAllRows(ForEachRow);
                }
                else
                {
                    Generator.GenerateSql(m_dmp);
                }
            }
            Place.FinalizeFileName();
        }

        private void ForEachRow(IBedRecord row)
        {
            Generator.GenerateSqlRow(row, m_dmp, m_dataFrame.GetSelectedColumns());
        }

        public override string TypeTitle
        {
            get { return "s_generate_sql"; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.generate_sql; }
        }

        public override string ToString()
        {
            return String.Format("GENSQL({0})", Generator);
        }
    }

    public static class GenerateSqlJob
    {
        public static Job CreateJob(IAppObjectSqlGenerator generator, IFilePlace place, ISqlDialect dialect, AppObject[] objs, ConnectionPack connpack, SqlFormatProperties formatProps, JobProperties jobProps)
        {
            return Job.FromCommand(new GenerateSqlJobCommand(generator, place, dialect, objs, connpack, formatProps), jobProps);
        }

        public static Job CreateDataJob(IDataSqlGenerator generator, IFilePlace place, ISqlDialect dialect, TableDataFrame dataFrame, DataFrameRowsExtractor rows, ConnectionPack connpack, SqlFormatProperties formatProps, JobProperties jobProps)
        {
            return Job.FromCommand(new GenerateDataSqlJobCommand(generator, place, dialect, dataFrame, rows, connpack, formatProps), jobProps);
        }
    }
}
