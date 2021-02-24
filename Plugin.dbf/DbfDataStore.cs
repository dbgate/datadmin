using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using SocialExplorer.IO.FastDBF;
using System.IO;
using System.ComponentModel;

namespace Plugin.dbf
{
    [TabularDataStore(Name = "dbf", Title = "DBF file", Description = Description, RequiredFeature = DbfFeature.Test)]
    public class DbfDataStore : FileWithFormatDataStoreBase
	{
        const string Description = @"s_dbf_desc";

        public DbfDataStore()
        {
            DefaultStringLength = 50;
            DefaultNumericScale = 5;
        }

        private DbfFile OpenReader()
        {
            var dbf = new DbfFile();
            dbf.Open(GetWorkingFileName(), System.IO.FileMode.Open);
            return dbf;
        }

        ITableStructure GetStructure(DbfFile dbf)
        {
            var res = new TableStructure();
            //output column names
            for (int i = 0; i < dbf.Header.ColumnCount; i++)
            {
                DbTypeBase type; 
                // convert DBF type to DA type
                switch (dbf.Header[i].ColumnType)
                {
                    case DbfColumn.DbfColumnType.Binary:
                        type = new DbTypeBlob();
                        break;
                    case DbfColumn.DbfColumnType.Boolean:
                        type = new DbTypeLogical();
                        break;
                    case DbfColumn.DbfColumnType.Date:
                        type = new DbTypeDatetime { SubType = DbDatetimeSubType.Date };
                        break;
                    case DbfColumn.DbfColumnType.Character:
                        type = new DbTypeString { Length = dbf.Header[i].Length };
                        break;
                    case DbfColumn.DbfColumnType.Integer:
                        type = new DbTypeInt();
                        break;
                    case DbfColumn.DbfColumnType.Memo:
                        type = new DbTypeText();
                        break;
                    case DbfColumn.DbfColumnType.Number:
                        type = new DbTypeNumeric { Precision = dbf.Header[i].DecimalCount };
                        break;
                    default:
                        type = new DbTypeString();
                        break;
                }
                res.AddColumn(dbf.Header[i].Name, type);
            }
            return res;
        }

        /// <summary>
        /// this method is called to obtain format of input file
        /// </summary>
        /// <returns></returns>
        protected override ITableStructure DoGetRowFormat()
        {
            DbfFile dbf = null;
            try
            {
                dbf = OpenReader();
                return GetStructure(dbf);
            }
            finally
            {
                if (dbf != null) dbf.Close();
            }
        }

        /// <summary>
        /// reads content of input file into given data queue
        /// </summary>
        /// <param name="queue"></param>
        protected override void DoRead(IDataQueue queue)
        {
            DbfFile dbf = null;
            try
            {
                dbf = OpenReader();
                ITableStructure format = GetStructure(dbf);

                DbfRecord irec = new DbfRecord(dbf.Header);
                // for each record in input DBF
                while (dbf.ReadNext(irec))
                {
                    if (irec.IsDeleted) continue;

                    object[] vals = new object[format.Columns.Count];
                    for (int i = 0; i < format.Columns.Count; i++)
                    {
                        vals[i] = irec[i];
                    }
                    var orec = new ArrayDataRecord(format, vals);
                    queue.PutRecord(new ArrayDataRecord(format, vals));
                }

                queue.PutEof();
            }
            catch (Exception e)
            {
                ProgressInfo.LogError(e);
                queue.PutError(e);
            }
            finally
            {
                if (dbf != null) dbf.Close();
                queue.CloseWriting();
            }
            FinalizeBulkCopy();
        }

        /// <summary>
        /// writes content of data queue into output DBF file
        /// </summary>
        /// <param name="queue"></param>
        protected override void DoWrite(IDataQueue queue)
        {
            var dbf = new DbfFile();
            var formatter = new BedValueFormatter(FormatSettings);
            try
            {
                if (File.Exists(GetWorkingFileName())) File.Delete(GetWorkingFileName());
                dbf.Create(GetWorkingFileName());

                ITableStructure ts = queue.GetRowFormat;
                foreach (var col in ts.Columns)
                {
                    DbfColumn.DbfColumnType type;
                    int len = 0, scale = 0;
                    switch (col.DataType.Code)
                    {
                        case DbTypeCode.Array:
                        case DbTypeCode.Generic:
                        case DbTypeCode.Text:
                        case DbTypeCode.Xml:
                            type = DbfColumn.DbfColumnType.Memo;
                            break;
                        case DbTypeCode.Blob:
                            type = DbfColumn.DbfColumnType.Binary;
                            break;
                        case DbTypeCode.Datetime:
                            var dtype = (DbTypeDatetime)col.DataType;
                            if (dtype.SubType == DbDatetimeSubType.Date)
                            {
                                type = DbfColumn.DbfColumnType.Date;
                            }
                            else
                            {
                                type = DbfColumn.DbfColumnType.Character;
                                len = DateTime.UtcNow.ToString("s").Length;
                            }
                            break;
                        case DbTypeCode.Float:
                            type = DbfColumn.DbfColumnType.Number;
                            len = 18;
                            scale = DefaultNumericScale;
                            break;
                        case DbTypeCode.Int:
                            if (AllowFoxProInteger)
                            {
                                type = DbfColumn.DbfColumnType.Integer;
                            }
                            else
                            {
                                type = DbfColumn.DbfColumnType.Number;
                                len = 18;
                            }
                            break;
                        case DbTypeCode.Logical:
                            type = DbfColumn.DbfColumnType.Boolean;
                            break;
                        case DbTypeCode.Numeric:
                            type = DbfColumn.DbfColumnType.Number;
                            len = 18;
                            scale = ((DbTypeNumeric)col.DataType).Scale;
                            break;
                        case DbTypeCode.String:
                            var stype = (DbTypeString)col.DataType;
                            if (stype.IsBinary)
                            {
                                type = DbfColumn.DbfColumnType.Binary;
                            }
                            else if (stype.Length <= 254)
                            {
                                type = DbfColumn.DbfColumnType.Character;
                                len = stype.Length;
                                if (len <= 0) len = DefaultStringLength;
                            }
                            else
                            {
                                type = DbfColumn.DbfColumnType.Memo;
                            }
                            break;
                        default:
                            type = DbfColumn.DbfColumnType.Character;
                            len = DefaultStringLength;
                            break;

                    }
                    dbf.Header.AddColumn(col.ColumnName, type, len, scale);
                }

                var orec = new DbfRecord(dbf.Header);
                while (!queue.IsEof)
                {
                    var record = queue.GetRecord();
                    orec.Clear();
                    for (int i = 0; i < ts.Columns.Count; i++)
                    {
                        record.ReadValue(i);
                        formatter.ReadFrom(record);
                        orec[i] = formatter.GetText();
                    }
                    dbf.Write(orec);
                }

            }
            finally
            {
                dbf.Close();
                queue.CloseReading();
            }
            FinalizeBulkCopy();
        }

        public override string FileExtension
        {
            get { return "dbf"; }
        }

        [XmlElem]
        [DatAdmin.DisplayName("s_allow_foxpro_integer")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool AllowFoxProInteger { get; set; }

        [XmlElem]
        [DatAdmin.DisplayName("s_default_string_length")]
        public int DefaultStringLength { get; set; }

        [XmlElem]
        [DatAdmin.DisplayName("s_default_numeric_scale")]
        public int DefaultNumericScale { get; set; }
    }
}
