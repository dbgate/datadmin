using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace DatAdmin
{
    public enum OpenTableAction
    {
        [Description("s_open_data")]
        OPEN_DATA,
        [Description("s_design_table")]
        DESIGN_TABLE,
        [Description("{s_generate_sql}: SELECT ALL")]
        GENERATE_SELECT_ALL,
        [Description("{s_generate_sql}: CREATE TABLE")]
        GENERATE_CREATE,
        [Description("{s_generate_sql}: RECREATE TABLE")]
        GENERATE_RECREATE,
        [Description("s_export")]
        EXPORT,
        [Description("s_import")]
        IMPORT
    }

    public enum OpenDuplicateMode
    {
        [Description("s_reuse_if_possible")]
        ReuseIfPossible,
        [Description("s_reuse_after_confirm")]
        ReuseAfterConfirm,
        [Description("s_allways_new_window")]
        AllwaysNewWindow
    }

    [SettingsPage(Name = "tree", Title = "s_tree", Targets = SettingsTargets.All)]
    public class TreeSettings : SettingsPageBase
    {
        bool m_autoExpandTables = true;
        [DisplayName("s_auto_expand_tables")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [SettingsKey("gui.tree.auto_expand_tables")]
        public bool AutoExpandTables
        {
            get { return m_autoExpandTables; }
            set { m_autoExpandTables = value; }
        }

        bool m_autoExpandDatabases = true;
        [DisplayName("s_auto_expand_databases")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [SettingsKey("gui.tree.auto_expand_databases")]
        public bool AutoExpandDatabases
        {
            get { return m_autoExpandDatabases; }
            set { m_autoExpandDatabases = value; }
        }


        OpenTableAction m_primaryTableAction = OpenTableAction.OPEN_DATA;
        [DisplayName("s_primary_action")]
        [SettingsKey("gui.tree.primary_table_action")]
        [TypeConverter(typeof(EnumDescConverter))]
        [Category("s_table")]
        public OpenTableAction PrimaryTableAction
        {
            get { return m_primaryTableAction; }
            set { m_primaryTableAction = value; }
        }

        OpenTableAction m_secondaryTableAction = OpenTableAction.DESIGN_TABLE;
        [DisplayName("s_secondary_action")]
        [SettingsKey("gui.tree.secondary_table_action")]
        [TypeConverter(typeof(EnumDescConverter))]
        [Category("s_table")]
        public OpenTableAction SecondaryTableAction
        {
            get { return m_secondaryTableAction; }
            set { m_secondaryTableAction = value; }
        }

        OpenDuplicateMode m_openTableDuplicateMode = OpenDuplicateMode.ReuseAfterConfirm;
        [DisplayName("s_open_table")]
        [SettingsKey("gui.tree.dupmode.open_table")]
        [TypeConverter(typeof(EnumDescConverter))]
        [Category("s_duplicate_windows")]
        public OpenDuplicateMode OpenTableDuplicateMode
        {
            get { return m_openTableDuplicateMode; }
            set { m_openTableDuplicateMode = value; }
        }

        OpenDuplicateMode m_designTableDuplicateMode = OpenDuplicateMode.ReuseIfPossible;
        [DisplayName("s_design_table")]
        [SettingsKey("gui.tree.dupmode.design_table")]
        [TypeConverter(typeof(EnumDescConverter))]
        [Category("s_duplicate_windows")]
        public OpenDuplicateMode DesignTableDuplicateMode
        {
            get { return m_designTableDuplicateMode; }
            set { m_designTableDuplicateMode = value; }
        }

        bool m_hideDataSamplesFolder = false;
        [DisplayName("s_hide_data_samples_folder")]
        [SettingsKey("gui.tree.hide_data_samples_folder")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool HideDataSamplesFolder
        {
            get { return m_hideDataSamplesFolder; }
            set { m_hideDataSamplesFolder = value; }
        }
    }

    public static class SettingsPageCollection_Tree
    {
        public static TreeSettings Tree(this SettingsPageCollection col)
        {
            return (TreeSettings)col.PageByName("tree");
        }
    }

    public static class OpenDuplicateModeExtension
    {
        public static bool ShouldReuse(this OpenDuplicateMode mode)
        {
            switch (mode)
            {
                case OpenDuplicateMode.AllwaysNewWindow:
                    return false;
                case OpenDuplicateMode.ReuseAfterConfirm:
                    return MessageBox.Show(Texts.Get("s_window_allready_open_reuse_q"), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes;
                case OpenDuplicateMode.ReuseIfPossible:
                    return true;
            }
            return false;
        }
    }
}
