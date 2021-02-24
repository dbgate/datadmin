using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DatAdmin
{
    public class BackupFormatAttribute : RegisterAttribute { }

    public interface IBackupFormat : IAddonInstance
    {
        void CheckBackupConfiguration(IDatabaseSource src);
        void CheckRestoreConfiguration(IDatabaseSource dst);
        bool BackupSuitableFor(IDatabaseSource src);
        IDatabaseWriter GetWriter(string file, IDatabaseSource src);
        IDatabaseLoader GetLoader(string file, IDatabaseSource dst);
        // extension with DOT
        string Extension { get; }
        string FileNameFormat { get; set; }
        string BackupFolder { get; set; }
        ITreeNode[] GetChildTreeNodes(ITreeNode parent, string filename);
    }

    [AddonType]
    public class BackupFormatAddonType : AddonType
    {
        public override Type InterfaceType
        {
            get { return typeof(IBackupFormat); }
        }
        public override string Name
        {
            get { return "bakformat"; }
        }
        public override Type RegisterAttributeType
        {
            get { return typeof(BackupFormatAttribute); }
        }
        public static readonly BackupFormatAddonType Instance = new BackupFormatAddonType();
    }
}
