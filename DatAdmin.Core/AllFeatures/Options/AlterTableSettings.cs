using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DatAdmin
{
    [SettingsPage(Name = "altertable", Title = "ALTER TABLE", Targets = SettingsTargets.All, ImageName = CoreIcons.table_editName)]
    public class AlterTableSettings : SettingsPageBase
    {
        bool m_allowRecreateTable = false;
        [DisplayName("s_allow_recreate_table")]
        [SettingsKey("gui.alter_table.allow_recreate_table")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool AllowRecreateTable
        {
            get { return m_allowRecreateTable; }
            set { m_allowRecreateTable = value; }
        }

        bool m_allowRecreateConstraint = true;
        [DisplayName("s_allow_recreate_constraint")]
        [SettingsKey("gui.alter_table.allow_recreate_constraint")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool AllowRecreateConstraint
        {
            get { return m_allowRecreateConstraint; }
            set { m_allowRecreateConstraint = value; }
        }

        bool m_showSqlConfirm = true;
        public const string ShowSqlConfirmKey = "gui.alter_table.show_sql_confirm";
        [DisplayName("s_show_sql_confirm")]
        [Category("s_other")]
        [SettingsKey(ShowSqlConfirmKey)]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool ShowSqlConfirm
        {
            get { return m_showSqlConfirm; }
            set { m_showSqlConfirm = value; }
        }

        bool m_showAlterResult = true;
        public const string ShowAlterResultKey = "gui.alter_table.show_alter_result";
        [DisplayName("s_show_alter_result")]
        [Category("s_other")]
        [SettingsKey(ShowAlterResultKey)]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool ShowAlterResult
        {
            get { return m_showAlterResult; }
            set { m_showAlterResult = value; }
        }
    }

    public static class SettingsPageCollection_AlterTable
    {
        public static AlterTableSettings AlterTable(this SettingsPageCollection col)
        {
            return (AlterTableSettings)col.PageByName("altertable");
        }
    }
}
