using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data.EffiProz;

namespace Plugin.effiproz
{
    public class EfzBulkInserter : BulkInserterBase
    {
        protected override void BeforeRun()
        {
            base.BeforeRun();
            ((EfzConnection)Connection.SystemConnection).AutoCommit = false;
        }
    }
}
