using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;

namespace DatAdmin
{
    //public interface ITunnellingDriver
    //{
    //    string[] SupportedEngines { get; }
    //    TunnelConnFrameBase CreateEditorFrame(TunnelStoredConnection conn);
    //    IStoredConnection CreateStoredConnection();
    //    DbProviderFactory ProviderFactory { get; }
    //}

    public interface ITunnelResultSet : IDisposable
    {
        object[] FetchRow();
        ITableStructure GetStructure();
        void Close();
        int RecordsAffected { get; }
    }

    public interface ITunnelDriver : IAddonInstance
    {
        DbProviderFactory ProviderFactory { get; }
        void SaveConnectionParams(TunnelConnectionStringBuilder pars);
        void LoadConnectionParams(TunnelConnectionStringBuilder pars);
        HashSetEx<string> GetSupportedKeys();
        void GetConnectionKey(CacheKeyBuilder ckb);
    }

    public class TunnelDriverAttribute : RegisterAttribute { }

    [AddonType]
    public class TunnelDriverAddonType : AddonType
    {
        public override string Name
        {
            get { return "tunneldriver"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(ITunnelDriver); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(TunnelDriverAttribute); }
        }

        public static readonly TunnelDriverAddonType Instance = new TunnelDriverAddonType();
    }

    public abstract class TunnelDriverBase : AddonBase, ITunnelDriver
    {
        public override AddonType AddonType
        {
            get { return TunnelDriverAddonType.Instance; }
        }

        #region ITunnelDriver Members

        public abstract DbProviderFactory ProviderFactory { get; }
        /// <summary>
        /// returns connection params defined in tunnelling driver
        /// with params from connection can be created connection string for tunnel system connection
        /// </summary>
        /// <param name="pars"></param>
        public abstract void SaveConnectionParams(TunnelConnectionStringBuilder pars);
        public abstract void LoadConnectionParams(TunnelConnectionStringBuilder pars);
        public abstract HashSetEx<string> GetSupportedKeys();
        public abstract void GetConnectionKey(CacheKeyBuilder ckb);

        #endregion
    }

    public interface ITunellableStoredConnection
    {
        ITunnelDriver TunnelDriver { get; set; }
        bool AllowDirectConnection { get; }
    }
}
