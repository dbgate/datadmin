using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using DatAdmin;

namespace Plugin.dbmodel
{
    [SettingsPage(Name = "synchronize", Title = "s_synchronize", Targets = SettingsTargets.Global, ImageName = CoreIcons.swapName, RequiredFeature = DbStructSynchronizationFeature.Test)]
    public class SynchronizeSettings : SettingsPageBase
    {
        bool m_ignoreColumnOrder = true;
        [DatAdmin.DisplayName("s_column_order")]
        [Category("s_ignore")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [SettingsKey("synchronize.ignore.column_order")]
        public bool IgnoreColumnOrder
        {
            get { return m_ignoreColumnOrder; }
            set { m_ignoreColumnOrder = value; }
        }

        bool m_ignoreConstraintNames = false;
        [DatAdmin.DisplayName("s_constraint_names")]
        [Category("s_ignore")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [SettingsKey("synchronize.ignore.constraint_names")]
        public bool IgnoreConstraintNames
        {
            get { return m_ignoreConstraintNames; }
            set { m_ignoreConstraintNames = value; }
        }

        bool m_ignoreAllTableProperties = false;
        [DatAdmin.DisplayName("s_all_table_properties")]
        [Category("s_ignore")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [SettingsKey("synchronize.ignore.all_table_properties")]
        public bool IgnoreAllTableProperties
        {
            get { return m_ignoreAllTableProperties; }
            set { m_ignoreAllTableProperties = value; }
        }

        List<string> m_ignoreTableProperties = new List<string>(new string[] { "mysql.auto_increment" });
        [DatAdmin.DisplayName("s_table_properties")]
        [Category("s_ignore")]
        [Editor(typeof(LinesEditor), typeof(UITypeEditor))]
        [SettingsKey("synchronize.ignore.table_properties")]
        public List<string> IgnoreTableProperties
        {
            get { return m_ignoreTableProperties; }
            set { m_ignoreTableProperties = value; }
        }

        List<string> m_ignoreDataTypeProperties = new List<string>(new string[] { "mysql.length" });
        [DatAdmin.DisplayName("Data type properties")]
        [Category("s_ignore")]
        [Editor(typeof(LinesEditor), typeof(UITypeEditor))]
        [SettingsKey("synchronize.ignore.datatype_properties")]
        public List<string> IgnoreDataTypeProperties
        {
            get { return m_ignoreDataTypeProperties; }
            set { m_ignoreDataTypeProperties = value; }
        }

        bool m_ignoreSchema;
        [DatAdmin.DisplayName("s_schema")]
        [Category("s_ignore")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [SettingsKey("synchronize.ignore.schema")]
        public bool IgnoreSchema
        {
            get { return m_ignoreSchema; }
            set { m_ignoreSchema = value; }
        }

        bool m_ignoreColumnCharacterSet = true;
        [Category("s_ignore")]
        [DatAdmin.DisplayName("s_column_character_set")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [SettingsKey("synchronize.ignore.column_character_set")]
        public bool IgnoreColumnCharacterSet
        {
            get { return m_ignoreColumnCharacterSet; }
            set { m_ignoreColumnCharacterSet = value; }
        }

        bool m_ignoreColumnCollation = true;
        [Category("s_ignore")]
        [DatAdmin.DisplayName("s_column_collation")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [SettingsKey("synchronize.ignore.column_collation")]
        public bool IgnoreColumnCollation
        {
            get { return m_ignoreColumnCollation; }
            set { m_ignoreColumnCollation = value; }
        }

        bool m_allowPairRenamedTables = true;
        [DatAdmin.DisplayName("s_pair_renamed_tables")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [SettingsKey("synchronize.pair_renamed_tables")]
        [Description("s_pair_renamed_tables_desc")]
        [Category("s_pairing")]
        public bool AllowPairRenamedTables
        {
            get { return m_allowPairRenamedTables; }
            set { m_allowPairRenamedTables = value; }
        }

        bool m_ignoreCase = true;
        [Category("s_ignore")]
        [DatAdmin.DisplayName("s_character_case")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [SettingsKey("synchronize.ignore.character_case")]
        public bool IgnoreCase
        {
            get { return m_ignoreCase; }
            set { m_ignoreCase = value; }
        }
    }


    //partial class SettingsPageCollection
    //{
    //    public SynchronizeSettings Synchronize
    //    {
    //        get { return (SynchronizeSettings)PageByName("synchronize"); }
    //    }
    //}
}
