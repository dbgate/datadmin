using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class TD_DisplayModel : List<TD_DisplayColumn>
    {
    }

    public class TD_DisplayColumn
    {
        public bool IsPrimaryKey;
        public bool IsLinked;
        public bool IsNullable;
        public IForeignKey Reference;
        public bool IsForeignKey { get { return Reference != null; } }
    }
}
