using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Windows.Forms;

namespace DatAdmin
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AddonTypeAttribute : Attribute
    {
    }

    public abstract class AddonSpace
    {
        public readonly AddonSpace ParentSpace;
        public readonly AddonType AddonType;

        public AddonSpace(AddonType addontype, AddonSpace parent)
        {
            ParentSpace = parent;
            AddonType = addontype;
        }

        public abstract void GetThisAddons(List<AddonHolder>  res);

        public List<AddonHolder> GetAllAddons()
        {
            var res = new List<AddonHolder>();
            AddonSpace sp = this;
            List<AddonSpace> spaces = new List<AddonSpace>();
            while (sp != null)
            {
                spaces.Add(sp);
                sp = sp.ParentSpace;
            }
            spaces.Reverse();
            foreach (var sp2 in spaces)
            {
                sp2.GetThisAddons(res);
            }
            return res;
        }

        // unique identifier of space, is used for caching
        public abstract string SpaceId { get; }

        public virtual void ClearCache() { }

        public IEnumerable<AddonHolder> GetFilteredAddons(RegisterItemUsage usage)
        {
            foreach (var item in GetAllAddons())
            {
                if (item.AllowUsage(usage)) yield return item;
            }
        }
    }

    // can hold defined or xml addon
    public abstract class AddonHolder
    {
        public readonly AddonSpace AddonSpace;

        public AddonHolder(AddonSpace adspace)
        {
            AddonSpace = adspace;
        }

        public abstract IAddonInstance InstanceModel { get; }
        public abstract Type InstanceType { get; }
        public abstract RegisterAttribute Attrib { get; }
        public abstract IAddonInstance CreateInstance();
        public virtual string Name { get { return null; } }
        public virtual string Title { get { return null; } }
        public AddonType AddonType { get { return AddonSpace.AddonType; } }

        public virtual bool SupportsCreateTemplate { get { return true; } }
        public virtual bool SupportsDirectUse { get { return true; } }
        public virtual bool SupportsDeserialize { get { return true; } }

        public bool AllowUsage(RegisterItemUsage usage)
        {
            switch (usage)
            {
                case RegisterItemUsage.CreateTemplate: return SupportsCreateTemplate;
                case RegisterItemUsage.Deserialize: return SupportsDeserialize;
                case RegisterItemUsage.DirectUse: return SupportsDirectUse;
            }
            return false;
        }

        public override string ToString()
        {
            return Texts.Get(Title);
        }

        public abstract string GetDefiner();
    }

    public class StaticAddonHolder : AddonHolder
    {
        IAddonInstance m_instanceModel;
        Type m_instanceType;
        RegisterAttribute m_attrib;

        public StaticAddonHolder(AddonSpace adspace, RegisterAttribute attr, Type insttype)
            : base(adspace)
        {
            m_instanceType = insttype;
            m_attrib = attr;
            m_instanceModel = (IAddonInstance)m_instanceType.CreateNewInstance();
        }

        public override RegisterAttribute Attrib
        {
            get { return m_attrib; }
        }

        public override string Name
        {
            get { return Attrib.Name; }
        }

        public override string Title
        {
            get { return Attrib.Title; }
        }

        public override IAddonInstance InstanceModel
        {
            get { return m_instanceModel; }
        }

        public override Type InstanceType
        {
            get { return m_instanceType; }
        }

        public override IAddonInstance CreateInstance()
        {
            return (IAddonInstance)m_instanceType.CreateNewInstance();
        }

        public override bool SupportsCreateTemplate
        {
            get { return Attrib.SupportsCreateTemplate; }
        }
        public override bool SupportsDeserialize
        {
            get { return Attrib.SupportsDeserialize; }
        }
        public override bool SupportsDirectUse
        {
            get { return Attrib.SupportsDirectUse; }
        }
        public override string GetDefiner()
        {
            try
            {
                return Path.GetFileNameWithoutExtension(m_instanceType.Assembly.GetName().CodeBase);
            }
            catch
            {
                return m_instanceType.Assembly.FullName;
            }
        }
    }

    public class PredefinedAddonHolder : AddonHolder
    {
        Func<IAddonInstance> m_createInstance;
        IAddonInstance m_instanceModel;
        string m_title;
        string m_name;

        public bool CanCreateTemplate = false;
        public bool CanDeserialize = false;
        public bool CanDirectUse = true;

        public PredefinedAddonHolder(AddonSpace adspace, string name, string title, Func<IAddonInstance> createInstance)
            : base(adspace)
        {
            m_createInstance = createInstance;
            m_instanceModel = createInstance();
            m_name = name;
            m_title = title;
        }

        public override string Title
        {
            get { return Texts.Get(m_title); }
        }
        public override string Name
        {
            get { return m_name; }
        }

        public override IAddonInstance InstanceModel
        {
            get { return m_instanceModel; }
        }

        public override Type InstanceType
        {
            get { return m_instanceModel.GetType(); }
        }

        public override RegisterAttribute Attrib
        {
            get { return null; }
        }

        public override IAddonInstance CreateInstance()
        {
            return m_createInstance();
        }

        public override bool SupportsCreateTemplate { get { return CanCreateTemplate; } }
        public override bool SupportsDeserialize { get { return CanDeserialize; } }
        public override bool SupportsDirectUse { get { return CanDirectUse; } }

        public override string GetDefiner()
        {
            return "DatAdmin Core";
        }
    }

    public class TemplateAddonHolder : AddonHolder
    {
        XmlElement m_xml;
        StaticAddonHolder m_model;
        string m_file;
        IAddonInstance m_instanceModel;

        public TemplateAddonHolder(AddonSpace adspace, string file)
            : base(adspace)
        {
            m_file = file;
        }

        public override RegisterAttribute Attrib
        {
            get { WantLoaded(); return m_model.Attrib; }
        }

        public override IAddonInstance InstanceModel
        {
            get { WantLoaded(); return m_instanceModel; }
        }

        public override Type InstanceType
        {
            get { WantLoaded(); return m_model.InstanceType; }
        }

        public override string Title
        {
            get { return Path.GetFileName(m_file); }
        }
        public override string Name
        {
            get { return Path.GetFileName(m_file); }
        }

        private void WantLoaded()
        {
            if (m_xml == null)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(m_file);
                m_xml = doc.DocumentElement;
                m_model = AddonType.StaticSpace.FindHolder(m_xml.GetAttribute("type"));
                m_instanceModel = m_model.CreateInstance();
                m_instanceModel.LoadFromXml(m_xml);
            }
        }

        public override IAddonInstance CreateInstance()
        {
            WantLoaded();
            IAddonInstance res = m_model.CreateInstance();
            res.LoadFromXml(m_xml);
            return res;
        }

        public override bool SupportsCreateTemplate
        {
            get { return false; }
        }
        public override bool SupportsDeserialize
        {
            get { return false; }
        }
        public override bool SupportsDirectUse
        {
            get { return true; }
        }
        public override string GetDefiner()
        {
            return "XML";
        }
    }

    public class StaticAddonSpace : AddonSpace
    {
        Dictionary<string, StaticAddonHolder> m_holders = new Dictionary<string, StaticAddonHolder>();

        public StaticAddonSpace(AddonType addontype)
            : base(addontype, null)
        {
        }

        public override string SpaceId
        {
            get { return AddonType.Name + ":static"; }
        }

        public void AddAssembly(Assembly assembly)
        {
            if (AddonType.RegisterAttributeType == null) return;
            foreach (Type tp in assembly.GetTypes())
            {
                foreach (RegisterAttribute attr in tp.GetCustomAttributes(AddonType.RegisterAttributeType, false))
                {
                    var hld = new StaticAddonHolder(this, attr, tp);
                    if (hld.Attrib.Name == null) throw new RegistryError("DAE-00312 Type " + tp.FullName + " has empty Name in attribute");
                    m_holders[hld.Attrib.Name] = hld;
                }
            }
        }

        public override void GetThisAddons(List<AddonHolder> res)
        {
            foreach (var h in m_holders.Values)
            {
                // test whether addon is for this edition
                if (!LicenseTool.FeatureAllowed(h.Attrib.RequiredFeature)) continue;

                res.Add(h);
            }
        }

        public StaticAddonHolder FindHolder(string name)
        {
            //StringBuilder sb = new StringBuilder();
            //var stackTrace = new System.Diagnostics.StackTrace();
            //foreach (var frame in stackTrace.GetFrames())
            //{
            //    sb.AppendLine(frame.GetMethod().Name);   // write method name
            //}
            //MessageBox.Show(sb.ToString());
            StaticAddonHolder res;
            if (m_holders.TryGetValue(name, out res))
            {
                if (LicenseTool.FeatureAllowed(res.Attrib.RequiredFeature)) return res;
                throw new InternalError(String.Format("DAE-00072 License for addon {0}({1}) not found", name, AddonType.Name));
            }
            throw new InternalError("DAE-00073 Addon " + AddonType.Name + " has not type " + name);
        }
    }

    public class FolderAddonSpace : AddonSpace
    {
        public readonly string Folder;
        List<TemplateAddonHolder> m_holders;

        public FolderAddonSpace(AddonType addontype, string folder, AddonSpace parent)
            : base(addontype, parent)
        {
            Folder = folder;
        }

        public override string SpaceId
        {
            get { return GetFolderSpaceId(AddonType.Name, Folder); }
        }

        public override void GetThisAddons(List<AddonHolder> res)
        {
            if (Framework.IsDesigning) return;
            if (m_holders == null)
            {
                m_holders = new List<TemplateAddonHolder>();
                if (Directory.Exists(Folder))
                {
                    foreach (string fn in Directory.GetFiles(Folder))
                    {
                        if (!fn.ToLower().EndsWith(AddonType.FileExtension)) continue;
                        m_holders.Add(new TemplateAddonHolder(this, fn));
                    }
                }
            }
            foreach (var h in m_holders) res.Add(h);
        }

        public override void ClearCache()
        {
            m_holders = null;
        }

        public static string GetFolderSpaceId(string adtype, string folder)
        {
            return adtype + ":folder:" + folder.Replace("\\", "/").ToLower();
        }
    }

    public class CommonAddonSpace : FolderAddonSpace
    {
        public CommonAddonSpace(AddonType type, AddonSpace parent)
            : base(type, Path.Combine(Framework.AddonsDirectory, type.Name), parent)
        {
        }

        public override string SpaceId
        {
            get { return AddonType.Name + ":common"; }
        }
    }

    public class PredefinedAddonSpace : AddonSpace
    {
        public PredefinedAddonSpace(AddonType type, AddonSpace parent)
            : base(type, parent)
        {
        }

        public override string SpaceId
        {
            get { return AddonType.Name + ":predefined"; }
        }

        public override void GetThisAddons(List<AddonHolder> res)
        {
            AddonType.GetPredefinedAddons(res);
        }
    }

    public abstract class AddonType
    {
        public StaticAddonSpace StaticSpace;
        public PredefinedAddonSpace PredefinedSpace;
        public CommonAddonSpace CommonSpace;

        public abstract string Name { get; }
        public abstract Type InterfaceType { get; }
        public abstract Type RegisterAttributeType { get; }
        public virtual void GetPredefinedAddons(List<AddonHolder> res) { }
        public virtual string FileExtension { get { return ".adx"; } }

        public virtual IAddonInstance LoadAddon(XmlElement xml)
        {
            var hld = StaticSpace.FindHolder(xml.GetAttribute("type"));
            var res = hld.CreateInstance();
            res.LoadFromXml(xml);
            return res;
        }

        public AddonHolder FindHolder(string name)
        {
            foreach (var item in CommonSpace.GetAllAddons())
            {
                if (item.Name == name) return item;
            }
            return null;
        }

        public void WantCommonFolder()
        {
            try
            {
                Directory.CreateDirectory(Path.Combine(Framework.AddonsDirectory, Name));
            }
            catch { }
        }

        public string GetFullFileName(string addonName)
        {
            if (!addonName.ToLower().EndsWith(FileExtension)) addonName += FileExtension;
            return Path.Combine(Path.Combine(Framework.AddonsDirectory, Name), addonName);
        }

        public string SaveCommonTemplate(IAddonInstance addon, string defname)
        {
            string name = InputBox.Run(Texts.Get("s_select_template_name"), defname ?? "tpl1");
            if (name != null)
            {
                WantCommonFolder();
                string fn = Path.Combine(CommonSpace.Folder, name + FileExtension);
                if (File.Exists(fn))
                {
                    if (!StdDialog.ReallyOverwriteFile(fn)) return null;
                }
                addon.SaveToFile(fn);
                CommonSpace.ClearCache();
                return name + ".";
            }
            return null;
        }

        public virtual string GetDirectory()
        {
            return Path.Combine(Framework.AddonsDirectory, Name);
        }

        public string GetLibDirectory()
        {
            return Path.Combine(Framework.AddonLibsDirectory, Name);
        }

        public IEnumerable<string> GetFiles()
        {
            var files = new HashSetEx<string>();
            string libdir = GetLibDirectory();
            if (Directory.Exists(libdir))
            {
                foreach (string fn in Directory.GetFiles(libdir, "*" + FileExtension))
                {
                    string ofn = Path.Combine(GetDirectory(), Path.GetFileName(fn));
                    if (File.Exists(ofn))
                    {
                        files.Add(IOTool.NormalizePath(ofn));
                        yield return ofn;
                    }
                    else
                    {
                        yield return fn;
                    }
                }
            }
            foreach (string fn in Directory.GetFiles(GetDirectory(), "*" + FileExtension))
            {
                if (files.Contains(IOTool.NormalizePath(fn))) continue;
                yield return fn;
            }
        }
    }

    // register of all addon spaces
    public class AddonRegister
    {
        public static readonly AddonRegister Instance = new AddonRegister();
        public static List<Assembly> CoreAssemblies = new List<Assembly>();

        Dictionary<string, AddonType> m_addonTypes = new Dictionary<string, AddonType>();
        Dictionary<string, AddonSpace> m_loadedSpaces = new Dictionary<string, AddonSpace>();
        Dictionary<Type, AddonType> m_addonTypesByInterfaceType = new Dictionary<Type, AddonType>();
        List<Assembly> m_addedAssemblies = new List<Assembly>();
        bool m_loadedCore = false;

        private void RegisterType(AddonType adtype)
        {
            m_addonTypes[adtype.Name] = adtype;
            if (adtype.InterfaceType != null) m_addonTypesByInterfaceType[adtype.InterfaceType] = adtype;
            adtype.StaticSpace = (StaticAddonSpace)GetStaticSpace(adtype.Name);
            adtype.PredefinedSpace = (PredefinedAddonSpace)GetPredefinedSpace(adtype.Name);
            adtype.CommonSpace = (CommonAddonSpace)GetCommonSpace(adtype.Name);
        }

        private void ProcessAssembly(Assembly assembly, AddonType adtype)
        {
            adtype.StaticSpace.AddAssembly(assembly);
        }

        private void WantCore()
        {
            if (m_loadedCore) return;
            m_loadedCore = true;
            AddAssembly(Assembly.GetAssembly(typeof(Framework)));
            foreach (var asm in CoreAssemblies) AddAssembly(asm);
        }

        public void AddAssembly(Assembly assembly)
        {
            WantCore();
            List<AddonType> addedTypes = new List<AddonType>();
            // detect new types
            foreach (Type tp in assembly.GetTypes())
            {
                foreach (AddonTypeAttribute attr in tp.GetCustomAttributes(typeof(AddonTypeAttribute), false))
                {
                    var type = (AddonType)tp.GetStaticPropertyOrField("Instance");// .CreateNewInstance() as AddonType;
                    RegisterType(type);
                    addedTypes.Add(type);
                }
            }

            // old assemblies * new types
            foreach (Assembly asm in m_addedAssemblies)
            {
                foreach (var addedType in addedTypes) ProcessAssembly(asm, addedType);
            }

            // new assembly * all types
            foreach (AddonType tp in m_addonTypes.Values)
            {
                ProcessAssembly(assembly, tp);
            }

            m_addedAssemblies.Add(assembly);
        }

        public AddonSpace GetSpace(string spaceid)
        {
            WantCore();
            if (m_loadedSpaces.ContainsKey(spaceid)) return m_loadedSpaces[spaceid];
            string[] itms = spaceid.Split(':');
            AddonSpace res = null;
            AddonType type = FindAddonType(itms[0]);
            switch (itms[1])
            {
                case "folder":
                    res = new FolderAddonSpace(type, itms[2], GetCommonSpace(itms[0]));
                    break;
                case "common":
                    res = new CommonAddonSpace(type, GetPredefinedSpace(itms[0]));
                    break;
                case "predefined":
                    res = new PredefinedAddonSpace(type, GetStaticSpace(itms[0]));
                    break;
                case "static":
                    res = new StaticAddonSpace(type);
                    break;
                default:
                    throw new InternalError("DAE-00074 Addon Space ID cannot be parsed:" + spaceid);
            }
            m_loadedSpaces[res.SpaceId] = res;
            return res;
        }

        private AddonSpace GetStaticSpace(string adtype)
        {
            return GetSpace(adtype + ":static");
        }

        private AddonSpace GetCommonSpace(string adtype)
        {
            return GetSpace(adtype + ":common");
        }

        private AddonSpace GetPredefinedSpace(string adtype)
        {
            return GetSpace(adtype + ":predefined");
        }

        public AddonType FindAddonType(string type)
        {
            WantCore();
            return m_addonTypes[type];
        }

        public AddonType FindAddonType(Type interfaceType)
        {
            WantCore();
            var res = Framework.Instance.FindAddonType(interfaceType);
            if (res != null) return res;
            return m_addonTypesByInterfaceType.Get(interfaceType);
        }

        public IEnumerable<AddonType> GetAddonTypes()
        {
            WantCore();
            return m_addonTypes.Values;
        }
    }
}
