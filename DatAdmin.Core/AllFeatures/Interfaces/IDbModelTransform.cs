using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public interface IDbModelTransform
    {
        void RunTransform(DatabaseStructure model);
    }

    public class DbModelTransformAttribute : RegisterAttribute { }


    [AddonType]
    public class DbModelTransformAddonType : AddonType
    {
        public override string Name
        {
            get { return "dbmodeltransform"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IDbModelTransform); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(DbModelTransformAttribute); }
        }

        public static readonly DbModelTransformAddonType Instance = new DbModelTransformAddonType();
    }


    public abstract class DbModelTransformBase : AddonBase, IDbModelTransform
    {
        public override AddonType AddonType
        {
            get { return DbModelTransformAddonType.Instance; }
        }

        #region IDbModelTransform Members

        public abstract void RunTransform(DatabaseStructure model);

        #endregion
    }
}
