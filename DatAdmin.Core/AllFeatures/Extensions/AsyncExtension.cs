using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class AsyncExtension
    {
        public static object StandaloneEndInvoke(this IAsyncResult async)
        {
            IStandaloneAsyncResult r = (IStandaloneAsyncResult)async;
            return r.EndInvoke();
        }
    }
}
