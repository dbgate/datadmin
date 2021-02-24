using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.ComponentModel;

namespace Plugin.mysql
{
    public class MySqlTableEditor : IDialectSpecificEditor
    {
        IDatabaseSource m_db;
        string[] m_charsets;
        string[] m_collations;
        string[] m_engines;

        [TypeConverter(typeof(ListTypeConverter))]
        [ListMethod("GetEngines")]
        public string Engine { get; set; }

        [Browsable(false)]
        [TypeConverter(typeof(ListTypeConverter))]
        [ListMethod("GetCharacterSets")]
        public string CharacterSet { get; set; }

        [TypeConverter(typeof(ListTypeConverter))]
        [ListMethod("GetCollations")]
        public string Collation { get; set; }

        public int? AutoIncrement { get; set; }

        [Browsable(false)]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool? Checksum { get; set; }

        [Browsable(false)]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool? DelayKeyWrite { get; set; }

        public MySqlTableEditor(ITableStructure table, IDatabaseSource db)
        {
            m_db = db;
            if (table != null)
            {
                Engine = table.SpecificData.Get("mysql.engine");
                CharacterSet = table.SpecificData.Get("mysql.character_set");
                Collation = table.SpecificData.Get("mysql.collation");
                AutoIncrement = StringTool.ParseNullableInt(table.SpecificData.Get("mysql.auto_increment"));
                Checksum = StringTool.ParseNullableBool(table.SpecificData.Get("mysql.checksum"));
                DelayKeyWrite = StringTool.ParseNullableBool(table.SpecificData.Get("mysql.delay_key_write"));
            }
        }

        public string[] GetEngines()
        {
            if (m_engines == null)
            {
                var dbmem = new DatabaseStructureMembers();
                dbmem.SpecificObjectOverride["mysql.engine"] = new SpecificObjectMembers();
                dbmem.SpecificObjectOverride["mysql.engine"].ObjectList = true;
                IDatabaseStructure cat = m_db.InvokeLoadStructure(dbmem, null);
                List<string> res = new List<string>();
                if (cat.SpecificObjects.ContainsKey("mysql.engine"))
                {
                    foreach (var c in cat.SpecificObjects["mysql.engine"])
                    {
                        res.Add(c.ObjectName.Name);
                    }
                }
                else
                {
                    res.Add("InnoDB");
                    res.Add("MyISAM");
                }
                m_engines = res.ToArray();
            }
            return m_engines;
        }

        public string[] GetCharacterSets()
        {
            if (m_charsets == null) m_charsets = new List<string>(m_db.InvokeLoadCharacterSets()).ToArray();
            return m_charsets;
        }

        public string[] GetCollations()
        {
            if (m_collations == null) m_collations = new List<string>(m_db.InvokeLoadCollations()).ToArray();
            return m_collations;
        }

        #region IDialectSpecificEditor Members

        public void SaveToStructure(AbstractObjectStructure obj)
        {
            var table = (TableStructure)obj;
            if (!String.IsNullOrEmpty(Engine)) table.SpecificData["mysql.engine"] = Engine;
            if (!String.IsNullOrEmpty(CharacterSet)) table.SpecificData["mysql.character_set"] = CharacterSet;
            if (!String.IsNullOrEmpty(Collation)) table.SpecificData["mysql.collation"] = Collation;
            if (AutoIncrement != null) table.SpecificData["mysql.auto_increment"] = AutoIncrement.ToString();
            if (Checksum != null) table.SpecificData["mysql.checksum"] = Checksum.Value ? "1" : "0";
            if (DelayKeyWrite != null) table.SpecificData["mysql.delay_key_write"] = DelayKeyWrite.Value ? "1" : "0";
        }

        #endregion
    }
}
