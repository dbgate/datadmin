using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.ComponentModel;

namespace Plugin.mssql
{
    public class MsSqlDatabaseEditor : IDialectSpecificEditor
    {
        IDatabaseStructure m_db;
        IDatabaseSource m_conn;

        string[] m_collations;

        public MsSqlDatabaseEditor(IDatabaseStructure db, IDatabaseSource conn)
        {
            m_db = db;
            m_conn = conn;

            Collation = m_db.SpecificData.Get("mssql.collation");
        }

        [TypeConverter(typeof(ListTypeConverter))]
        [ListMethod("GetCollations")]
        public string Collation { get; set; }

        public string[] GetCollations()
        {
            if (m_collations == null) m_collations = new List<string>(m_conn.InvokeLoadCollations()).ToArray();
            return m_collations;
        }

        #region IDialectSpecificEditor Members

        public void SaveToStructure(AbstractObjectStructure obj)
        {
            if (!String.IsNullOrEmpty(Collation)) obj.SpecificData["mssql.collation"] = Collation;
        }

        #endregion
    }
}
