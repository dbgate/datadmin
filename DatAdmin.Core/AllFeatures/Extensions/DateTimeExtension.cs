using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class DateTimeExtension
    {
        public static int GetUnixTimestamp(this DateTime dt)
        {
            return (int)(dt - new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
