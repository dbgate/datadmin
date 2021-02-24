using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Drawing;

namespace DatAdmin
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SpecificRepresentationAttribute : RegisterAttribute
    {
    }

    public interface ISpecificRepresentation
    {
        Bitmap Icon { get; }
        string TitlePlural { get; }
        string TitleSingular { get; }
        string ObjectType { get; }
        string XmlElementName { get; }
        bool ShowInTree { get; }
        bool UseInSynchronization { get; }
    }


    public enum DbObjectParent { Database, Table, Constraint };
    public enum DbObjectScript { Create, Drop, Alter };

    /// <summary>
    /// GUI support for specific objects
    /// analysing and generating SQL for specific objects should be defined
    /// in Analuser/Dumper specializations
    /// </summary>
    public interface ISpecificObjectType
    {
        bool SupportsLoadOverview { get; }
        DataTable LoadOverview(DbConnection conn, ObjectPath parpath);
        string ObjectType { get; }
        bool SupportedCreateNew { get; }
        string GenerateCreateNew(DbConnection conn, ObjectPath parpath);
        List<IWidget> GetWidgets();
        bool HasSystemVariant { get; }
        DbObjectParent ParentType { get; }
        bool HasTabularData { get; }
        ITabularDataView MergeTabularData(IPhysicalConnection conn, ObjectPath fullName);
        void GetPopupMenu(MenuBuilder menu, IPhysicalConnection conn, ObjectPath fullName);
    }

    [AddonType]
    public class SpecificRepresentationAddonType : AddonType
    {
        private Dictionary<string, ISpecificRepresentation> m_reprCache = new Dictionary<string, ISpecificRepresentation>();
        private Dictionary<string, ISpecificRepresentation> m_reprElemCache = new Dictionary<string, ISpecificRepresentation>();

        public override string Name
        {
            get { return "specificrepr"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(ISpecificRepresentation); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(SpecificRepresentationAttribute); }
        }

        public static readonly SpecificRepresentationAddonType Instance = new SpecificRepresentationAddonType();

        public ISpecificRepresentation FindByElement(string elemName)
        {
            lock (m_reprElemCache)
            {
                if (m_reprElemCache.ContainsKey(elemName)) return m_reprElemCache[elemName];
            }
            foreach (var item in CommonSpace.GetAllAddons())
            {
                ISpecificRepresentation repr = (ISpecificRepresentation)item.CreateInstance();
                if (repr.XmlElementName == elemName)
                {
                    lock (m_reprCache)
                    {
                        m_reprElemCache[elemName] = repr;
                    }
                    return repr;
                }
            }
            return null;
        }

        public ISpecificRepresentation TryFindRepresentation(string objtype)
        {
            lock (m_reprCache)
            {
                if (m_reprCache.ContainsKey(objtype)) return m_reprCache[objtype];
            }
            foreach (var item in CommonSpace.GetAllAddons())
            {
                ISpecificRepresentation repr = (ISpecificRepresentation)item.CreateInstance();
                if (repr.ObjectType == objtype)
                {
                    lock (m_reprCache)
                    {
                        m_reprCache[objtype] = repr;
                    }
                    return repr;
                }
            }
            return null;
        }

        public ISpecificRepresentation FindRepresentation(string objtype)
        {
            var res = TryFindRepresentation(objtype);
            if (res != null) return res;
            throw new KeyNotFoundException(String.Format("DAE-00189 Repesentation for object type {0} not found", objtype));
        }
    }
}
