using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.IO;

namespace DatAdmin
{
    public abstract class SpecificObjectTypeBase : ISpecificObjectType
    {
        protected ISqlDialect m_dialect;

        protected string CreateNewTemplate = null;

        protected SpecificObjectTypeBase(ISqlDialect dialect)
        {
            m_dialect = dialect;
        }

        public virtual ITabularDataView MergeTabularData(IPhysicalConnection conn, ObjectPath fullName)
        {
            throw new InternalError("DAE-00006 Node has not tabular data");
        }
        public virtual bool HasTabularData { get { return false; } }

        public virtual bool SupportsLoadOverview
        {
            get { return false; }
        }

        public virtual System.Data.DataTable LoadOverview(System.Data.Common.DbConnection conn, ObjectPath parpath)
        {
            throw new NotImplementedError("DAE-00157");
        }

        public virtual DbObjectParent ParentType { get { return DbObjectParent.Database; } }

        public virtual List<IWidget> GetWidgets()
        {
            var res = new List<IWidget>();
            res.Add(new DbObjectSqlWidget());
            return res;
        }

        public abstract string ObjectType { get; }

        public virtual bool HasSystemVariant { get { return false; } }
        public virtual bool IsSystemObject(ObjectPath objpath) { return false; }

        public virtual void GetPopupMenu(MenuBuilder mb, IPhysicalConnection conn, ObjectPath fullName)
        {
            var cmd = CreateMenu();
            cmd.Connection = conn;
            cmd.FullName = fullName;
            cmd.Parent = this;
            mb.AddObject(cmd);
        }

        public virtual SpecificObjectMenuCommandsBase CreateMenu()
        {
            return new SpecificObjectMenuCommandsBase();
        }

        public virtual bool SupportedCreateNew { get { return CreateNewTemplate != null; } }
        public virtual string GenerateCreateNew(DbConnection conn, ObjectPath parpath)
        {
            return CreateNewTemplate;
        }
    }

    public class SpecificObjectMenuCommandsBase
    {
        public IPhysicalConnection Connection;
        public ObjectPath FullName;
        public SpecificObjectTypeBase Parent;
    }
}
