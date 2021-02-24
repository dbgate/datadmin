using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Data.Common;
using System.Reflection;

namespace DatAdmin
{
    public class DbDriverManager
    {
        public static readonly DbDriverManager Instance = new DbDriverManager();
        DbDriverSet m_systemDrivers;
        DbDriverSet m_customDrivers;

        public List<DbDriverDefinition> Drivers
        {
            get
            {
                List<DbDriverDefinition> res = new List<DbDriverDefinition>();
                res.AddRange(m_systemDrivers.Drivers);
                res.AddRange(m_customDrivers.Drivers);
                return res;
            }
        }

        public DbDriverDefinition AddDriver()
        {
            DbDriverDefinition res = new DbDriverDefinition();
            m_customDrivers.Drivers.Add(res);
            res.Name = "[new driver]";
            res.InvariantName = "System.Data.MyNewDriver";
            res.IsSystem = false;
            return res;
        }

        public void SaveCustom()
        {
            XmlTool.SerializeObject(Path.Combine(Core.ConfigDirectory, "drivers.xml"), m_customDrivers);
        }

        public void ReloadCustom()
        {
            try
            {
                m_customDrivers = (DbDriverSet)XmlTool.DeserializeObject(Path.Combine(Core.ConfigDirectory, "drivers.xml"));
            }
            catch (Exception)
            {
                m_customDrivers = new DbDriverSet();
            }
        }

        public DbDriverManager()
        {
            try
            {
                m_customDrivers = (DbDriverSet)XmlTool.DeserializeObject(Path.Combine(Core.ConfigDirectory, "drivers.xml"));
            }
            catch (Exception)
            {
                m_customDrivers = new DbDriverSet();
            }
            try
            {
                m_systemDrivers = (DbDriverSet)XmlTool.DeserializeObject(Path.Combine(Core.LibDirectory, "drivers.xml"));
            }
            catch (Exception)
            {
                m_systemDrivers = new DbDriverSet();
            }
            foreach (DbDriverDefinition drv in m_systemDrivers.Drivers)
            {
                drv.IsSystem = true;
            }
        }

        public void RemoveDriver(DbDriverDefinition driver)
        {
            m_customDrivers.Drivers.Remove(driver);
        }

        public DbProviderFactory CreateFactory(string driver)
        {
            foreach (DbDriverDefinition drv in Drivers)
            {
                if (drv.InvariantName == driver)
                {
                    return drv.CreateFactory();
                }
            }
            throw new Exception("DAE-00227 Driver not found:" + driver);
        }

    }

    public class DbDriverSet
    {
        List<DbDriverDefinition> m_drivers = new List<DbDriverDefinition>();

        public List<DbDriverDefinition> Drivers
        {
            get { return m_drivers; }
            set { m_drivers = value; }
        }
    }

    public class DbDriverDefinition
    {
        string m_invariantName;

        public string InvariantName
        {
            get { return m_invariantName; }
            set { m_invariantName = value; }
        }
        string m_name;

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }
        string m_assemblyPath;

        public string AssemblyPath
        {
            get { return m_assemblyPath; }
            set { m_assemblyPath = value; }
        }
        string m_factoryType;

        public string FactoryType
        {
            get { return m_factoryType; }
            set { m_factoryType = value; }
        }

        bool m_isSystem;

        [Browsable(false)]
        public bool IsSystem
        {
            get { return m_isSystem; }
            set { m_isSystem = value; }
        }

        public override string ToString()
        {
            return m_name;
        }

        public DbProviderFactory CreateFactory()
        {
            Assembly a = Assembly.LoadFile(Path.Combine(Core.ProgramDirectory, m_assemblyPath));
            Type tp = a.GetType(m_factoryType);
            object instance = tp.GetStaticPropertyOrField("Instance");
            return (DbProviderFactory)instance;
        }
    }
}
