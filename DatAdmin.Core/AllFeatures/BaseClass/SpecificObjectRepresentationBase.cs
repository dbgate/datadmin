using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace DatAdmin
{
    public abstract class SpecificObjectRepresentationBase : AddonBase, ISpecificRepresentation
    {
        #region ISpecificRepresentation Members

        public virtual Bitmap Icon
        {
            get { return null; }
        }

        public abstract string TitlePlural
        {
            get;
        }

        public abstract string TitleSingular
        {
            get;
        }

        public abstract string ObjectType
        {
            get;
        }

        public virtual string XmlElementName
        {
            get { return null; }
        }

        public virtual bool ShowInTree
        {
            get { return true; }
        }

        public virtual bool UseInSynchronization
        {
            get { return true; }
        }

        #endregion

        [Browsable(false)]
        public override AddonType AddonType
        {
            get { return SpecificRepresentationAddonType.Instance; }
        }
    }
}
