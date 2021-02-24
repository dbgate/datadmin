using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public abstract class SqlDumpLoaderBase : DatabaseLoaderBase, IDumpLoaderConfig
    {
        public override void LoadDatabase(IDatabaseSource dst)
        {
            using (FileStream fr = new FileInfo(Filename).OpenRead())
            {
                var sysconn = dst.Connection.SystemConnection;
                sysconn.SafeChangeDatabase(dst.DatabaseName);
                var loader = (dst.Dialect ?? GenericDialect.Instance).CreateDumpLoader();
                loader.Config = this;
                loader.Connection = sysconn;
                loader.ProgressInfo = ProgressInfo;
                loader.Run(fr);
            }
        }
    }

    [DatabaseLoader(Name = "generic_dumploader", SupportsDirectUse = false)]
    public class GenericDatabaseLoader : SqlDumpLoaderBase
    {
        public override string GetTitle()
        {
            return "Generic Dump Loader (not recommended)";
        }
    }
}
