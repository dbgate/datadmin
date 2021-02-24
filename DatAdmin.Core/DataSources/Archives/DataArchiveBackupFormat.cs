using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DatAdmin
{
    [BackupFormat(Name = "dbk", Title = "s_data_archive", Description = "s_dbk_desc")]
    public class DataArchiveBackupFormat : BackupFormatBase
    {
        public override IDatabaseWriter GetWriter(string file, IDatabaseSource src)
        {
            var res = new DataArchiveWriter();
            res.FilePlace = FilePlaceAddonType.PlaceFromVirtualFile(file);
            return res;
        }

        public override IDatabaseLoader GetLoader(string file, IDatabaseSource dst)
        {
            return new DataArchiveLoader(file);
        }

        [Browsable(false)]
        public override string Extension
        {
            get { return ".dbk"; }
        }

        public override ITreeNode[] GetChildTreeNodes(ITreeNode parent, string filename)
        {
            return new ITreeNode[] { new DataArchiveReaderTreeNode(parent, new DbkFileHandler { FileObject = new DiskFile(filename) }) };
        }

        public override string ToString()
        {
            return Texts.Get("s_data_archive");
        }

        public override bool BackupSuitableFor(IDatabaseSource dst)
        {
            return true;
        }
    }

    [DatabaseLoader(Name = "dbkloader")]
    public class DataArchiveLoader : DatabaseLoaderBase
    {
        public DataArchiveLoader() { }
         
        public DataArchiveLoader(string filename)
        {
            Filename = filename;
        }

        public override void LoadDatabase(IDatabaseSource dst)
        {
            var writer = new DatabaseSourceWriter(dst);
            var reader = new DataArchiveReader(Filename);
            var dbmem = new DatabaseStructureMembers
            {
                TableMembers = TableStructureMembers.AllNoRefs,
                SpecificObjectDetails = true,
            };
            var copyOpts = new DatabaseCopyOptions
            {
                CopyMembers = dbmem,
                DataMode = DbCopyDataMode.All,
                CopyStructure = true,
                SchemaMode = DbCopySchemaMode.Original,
            };
            CopyDbJob.CopyDatabase(reader, writer, ProgressInfo, copyOpts);
        }

        public override string Filter
        {
            get { return String.Format("{0}|*.dbk", Texts.Get("s_data_archives")); }
        }

        public override string GetTitle()
        {
            return "s_data_archive";
        }
    }
}
