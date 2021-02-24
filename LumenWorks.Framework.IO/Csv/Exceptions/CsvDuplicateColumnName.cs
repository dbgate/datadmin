using System;
using System.Collections.Generic;
using System.Text;

namespace LumenWorks.Framework.IO.Csv
{
    public class CsvDuplicateColumnName : Exception
    {
        public string Column;
        public CsvDuplicateColumnName(string column)
        {
            Column = column;
        }
    }
}
