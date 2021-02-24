using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class PhysicalConnectionFactoryAttribute : RegisterAttribute
    {
    }

    [AddonType]
    public class PhysicalConnectionFactoryAddonType : AddonType
    {
        public override string Name
        {
            get { return "physconnfact"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IPhysicalConnectionFactory); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(PhysicalConnectionFactoryAttribute); }
        }

        public static readonly PhysicalConnectionFactoryAddonType Instance = new PhysicalConnectionFactoryAddonType();
    }

    public interface IPhysicalConnectionFactory : IAddonInstance
    {
        string GetConnectionKey();
        string GetDataSource();
        IPhysicalConnection CreateConnection();
        IServerSource CreateServerSource(IPhysicalConnection conn);
        IDatabaseSource CreateDatabaseSource(IPhysicalConnection conn, string dbname);
        string GetDatabasePrivateFolder(string dbname);
        string GetDatabasePrivateSubFolder(string dbname, string folderName);
        string GetFileName();
        string GetDataTreeName();
    }

    public abstract class PhysicalConnectionFactoryBase : AddonBase, IPhysicalConnectionFactory
    {
        #region IPhysicalConnectionFactory Members

        public abstract IPhysicalConnection CreateConnection();
        public virtual IServerSource CreateServerSource(IPhysicalConnection conn) { return null; }
        public abstract IDatabaseSource CreateDatabaseSource(IPhysicalConnection conn, string dbname);
        public abstract string GetFileName();
        public virtual string GetDataTreeName()
        {
            return TreeNodeExtension.FileNameToDataTreeName(GetFileName());
        }
        public abstract string GetConnectionKey();
        public abstract string GetDataSource();
        public virtual string GetDatabasePrivateFolder(string dbname) { return null; }
        public virtual string GetDatabasePrivateSubFolder(string dbname, string folderName) { return null; }

        #endregion

        public override AddonType AddonType
        {
            get { return PhysicalConnectionFactoryAddonType.Instance; }
        }
    }
}
