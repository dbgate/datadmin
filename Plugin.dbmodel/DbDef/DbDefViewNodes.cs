using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.dbmodel
{
    public class DbDefViewTreeNode : TreeNodeBase
    {
        //DatabaseMenuCommands m_commands;
        DbDefSource m_conn;
        IDatabaseStructure m_db;

        public DbDefViewTreeNode(IDatabaseStructure db, ISqlDialect dialect)
            : base("dbstruct")
        {
            m_db = db;
            m_conn = new DbDefSource(m_db, DbDefSource.ReadOnly.Flag);
            var appobj = new DatabaseAppObject();
            appobj.FillFromDatabase(m_conn);
            SetAppObject(appobj);
            //m_commands = new DatabaseMenuCommands(m_conn, this);
        }

        public override string Title
        {
            get { return "dbdef"; }
        }
        public override ITreeNode[] GetChildren()
        {
            return DatabaseNodeExtension.GetChildren(m_conn, this, null);
            //return m_commands.GetChildren(this, null);
        }
        public override string TypeTitle
        {
            get { return "dbdef"; }
        }
    }
}
