using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DatAdmin
{
    public static class StructLoader
    {
        public static List<string> SchemaNames(Func<DatabaseStructureMembers, IDatabaseStructure> loadFunc)
        {
            DatabaseStructureMembers dbmem = new DatabaseStructureMembers { SchemaList = true };
            IDatabaseStructure dbs = loadFunc(dbmem);
            return new List<string>(from s in dbs.Schemata select s.SchemaName);
        }
    }
}
