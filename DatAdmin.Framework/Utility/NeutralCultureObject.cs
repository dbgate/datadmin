using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Threading;

namespace DatAdmin
{
    public class NeutralCultureObject : IDisposable
    {
        CultureInfo old, oldui;
        public NeutralCultureObject()
        {
            old = Thread.CurrentThread.CurrentCulture;
            oldui = Thread.CurrentThread.CurrentUICulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
        }
        public void Close() { Dispose(); }
        public void Dispose()
        {
            Thread.CurrentThread.CurrentCulture = old;
            Thread.CurrentThread.CurrentUICulture = oldui;
        }
    }
}
