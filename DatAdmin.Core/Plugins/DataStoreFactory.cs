using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    [CreateFactory(Name="datastore")]
    public class DataStoreCreateFactory : AddonCreateFactory
    {
        public DataStoreCreateFactory()
            : base(TabularDataStoreAddonType.Instance, "s_datastores", "datastores", CoreIcons.datastore32)
        {
        }
    }
}
