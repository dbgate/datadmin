using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class DisposeList : List<IDisposable>, IDisposable
    {
        public void DisposeAndClear()
        {
            foreach (IDisposable item in this) item.Dispose();
            Clear();
        }


        #region IDisposable Members

        public void Dispose()
        {
            DisposeAndClear();
        }

        #endregion
    }
}
