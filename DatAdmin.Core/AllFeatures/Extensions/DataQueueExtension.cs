using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DatAdmin
{
    public static class DataQueueExtension
    {
        public static IEnumerable<IBedRecord> EnumRows(this IDataQueue queue)
        {
            try
            {
                while (!queue.IsEof) yield return queue.GetRecord();
            }
            finally
            {
                queue.CloseReading();
            }
        }
    }
}
