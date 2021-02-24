using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.ComponentModel;

namespace Plugin.mysql
{
    public class MySqlDatabaseEditor : IDialectSpecificEditor
    {
        IDatabaseStructure m_db;
        IDatabaseSource m_conn;

        string[] m_collations;
        string[] m_charsets;

        public MySqlDatabaseEditor(IDatabaseStructure db, IDatabaseSource conn)
        {
            m_db = db;
            m_conn = conn;

            Collation = m_db.SpecificData.Get("mysql.collation");
            CharacterSet = m_db.SpecificData.Get("mysql.character_set");
        }

        [TypeConverter(typeof(ListTypeConverter))]
        [ListMethod("GetCharacterSets")]
        public string CharacterSet { get; set; }

        [TypeConverter(typeof(ListTypeConverter))]
        [ListMethod("GetCollations")]
        public string Collation { get; set; }

        public string[] GetCollations()
        {
            if (m_collations == null) m_collations = new List<string>(m_conn.InvokeLoadCollations()).ToArray();
            return m_collations;
        }

        public string[] GetCharacterSets()
        {
            if (m_charsets == null) m_charsets = new List<string>(m_conn.InvokeLoadCharacterSets()).ToArray();
            return m_charsets;
        }

        #region IDialectSpecificEditor Members

        public void SaveToStructure(AbstractObjectStructure obj)
        {
            if (!String.IsNullOrEmpty(CharacterSet)) obj.SpecificData["mysql.character_set"] = CharacterSet;
            if (!String.IsNullOrEmpty(Collation)) obj.SpecificData["mysql.collation"] = Collation;
        }

        #endregion
    }
}
