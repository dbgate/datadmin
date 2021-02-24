using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DatAdmin
{
    public abstract class TabularDataViewLoaderBase : AddonBase, ITabularDataViewLoader
    {
        public abstract ITabularDataView CreateTabularDataView();

        public override AddonType AddonType
        {
            get { return TabularDataViewLoaderAddonType.Instance; }
        }
    }
}
