using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using Microsoft.Win32;
using System.Data.Common;
using System.Data.OleDb;
using System.Data;

namespace Plugin.oledb
{
    [StoredConnection(Name = "oledb", Title = "OLE DB")]
    public class OleDbStoredConnection : ProviderStoredConnection
    {
        public override IEnumerable<string> GetProviders()
        {
            List<string> provs = new List<string>();
            RegistryKey key = Registry.ClassesRoot.OpenSubKey("CLSID");
            foreach (string sub in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(sub);
                string prov = subkey.GetKeyValue("OLE DB Provider");
                if (prov != null)
                {
                    string proid = subkey.GetKeyValue("VersionIndependentProgID");
                    provs.Add(proid ?? prov);
                    subkey.Close();
                }
            }
            key.Close();
            return provs;
        }

        public override string GenerateConnectionString(bool includepwd)
        {
            string res;
            res = String.Format("Provider={0};Server={1};Uid={2};Pwd={3}", Provider, DataSource, Login, includepwd ? Password : "******");
            if (DatabaseMode == ConnectionDatabaseMode.Explicit) res += ";Database=" + ExplicitDatabaseName;
            return res;
        }

        public override DbProviderFactory GetFactory()
        {
            return OleDbFactory.Instance;
        }

        public override IProviderHooks CreateHooks()
        {
            return new OleDbHooks();
        }
    }

    public class OleDbHooks : ProviderHooksBase
    {
        public override List<string> GetDatabaseNames(IPhysicalConnection conn)
        {
            OleDbConnection c = conn.SystemConnection as OleDbConnection;
            if (c != null)
            {
                DataTable dbs = c.GetOleDbSchemaTable(OleDbSchemaGuid.Catalogs, new object[] { });
                List<string> res = new List<string>();
                foreach (DataRow row in dbs.Rows)
                {
                    res.Add(row[0].SafeToString());
                }
                return res;
            }
            return base.GetDatabaseNames(conn);
        }
    }

    [CreateFactoryItem(Name="oledb")]
    public class OleDbCreateWizard : GenericConnectionCreateWizard
    {
        public OleDbCreateWizard()
            : base("oledb", "OLE DB", "s_file_desc_oledb")
        {
        }
        public override IStoredConnection CreateStoredConnection()
        {
            return new OleDbStoredConnection();
        }
        public override System.Drawing.Bitmap Bitmap
        {
            get { return StdIcons.oledb; }
        }
    }
}
