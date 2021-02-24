using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.datasyn
{
    public class DataSynError : ExpectedError
    {
        public DataSynError(string message)
            : base(message, null)
        {
        }
    }
}
