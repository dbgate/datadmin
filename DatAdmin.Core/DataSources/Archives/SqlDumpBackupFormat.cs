using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DatAdmin
{
    [BackupFormat(Name = "sql_dump", Title = "SQL Dump")]
    public class SqlDumpBackupFormat : BackupFormatBase
    {
        public override IDatabaseWriter GetWriter(string file, IDatabaseSource src)
        {
            var res = new SqlDatabaseWriter();
            res.UsedDialect = src.Dialect;
            res.FilePlace = FilePlaceAddonType.PlaceFromVirtualFile(file);
            return res;
        }

        public override IDatabaseLoader GetLoader(string file, IDatabaseSource dst)
        {
            var dialect = dst.Dialect ?? GenericDialect.Instance;
            var res = dialect.CreateSpecificDatabaseLoader();
            res.Filename = file;
            return res;
        }

        [Browsable(false)]
        public override string Extension
        {
            get { return ".sql"; }
        }

        public override string ToString()
        {
            return "SQL Dump";
        }

        public override bool BackupSuitableFor(IDatabaseSource dst)
        {
            return dst.DatabaseCaps.ExecuteSql;
        }

    }
}
