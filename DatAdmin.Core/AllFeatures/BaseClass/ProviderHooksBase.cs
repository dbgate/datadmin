using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class ProviderHooksBase : IProviderHooks
    {
        #region IProviderHooks Members

        public virtual List<string> GetDatabaseNames(IPhysicalConnection conn)
        {
            return new List<string> { null };
        }

        #endregion
    }
}
