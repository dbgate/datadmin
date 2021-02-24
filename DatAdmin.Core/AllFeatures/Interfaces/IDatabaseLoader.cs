using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DatAdmin
{
    public class DatabaseLoaderAttribute : RegisterAttribute { }

    public interface IDatabaseLoader : IAddonInstance
    {
        string Filename { get; set;  }
        IProgressInfo ProgressInfo { get; set; }
        string Filter { get; }

        bool IsSqlDumpLoader { get; }
        void LoadDatabase(IDatabaseSource dst);
        void CheckConfiguration(IDatabaseSource dst);
        bool SuitableFor(IDatabaseSource dst);
        string GetTitle();
    }

    [AddonType]
    public class DatabaseLoaderAddonType : AddonType
    {
        public override string Name
        {
            get { return "dbloader"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IDatabaseLoader); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(DatabaseLoaderAttribute); }
        }

        public static readonly DatabaseLoaderAddonType Instance = new DatabaseLoaderAddonType();
    }
}
