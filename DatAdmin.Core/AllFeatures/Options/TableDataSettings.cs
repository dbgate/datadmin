using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace DatAdmin
{
    [SettingsPage(Name = "tabledata", Title = "s_table_data", Targets = SettingsTargets.All, ImageName = CoreIcons.table_dataName)]
    public class TableDataSettings : SettingsPageBase
    {
        public TableDataSettings()
        {
            try
            {
                m_style = DataGridStyleAddonType.Instance.FindHolder("default");
            }
            catch
            {
                m_style = null;
            }
        }

        bool m_useUtcTime = false;
        [Category("s_datetime")]
        [DisplayName("s_use_utc_time")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [SettingsKey("gui.datagrid.use_utc_time")]
        public bool UseUtcTime
        {
            get { return m_useUtcTime; }
            set { m_useUtcTime = value; }
        }

        //string m_dateFormat = "yyyy-MM-dd";

        //[DisplayName("s_date_format")]
        //[Category("s_datetime")]
        //[SettingsKey("gui.datagrid.date_format")]
        //public string DateFormat
        //{
        //    get { return m_dateFormat; }
        //    set { m_dateFormat = value; }
        //}

        //string m_timeFormat = "HH:mm:ss";

        //[DisplayName("s_time_format")]
        //[Category("s_datetime")]
        //[SettingsKey("gui.datagrid.time_format")]
        //public string TimeFormat
        //{
        //    get { return m_timeFormat; }
        //    set { m_timeFormat = value; }
        //}

        //string m_dateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        //[DisplayName("s_datetime_format")]
        //[Category("s_datetime")]
        //[SettingsKey("gui.datagrid.datetime_format")]
        //public string DateTimeFormat
        //{
        //    get { return m_dateTimeFormat; }
        //    set { m_dateTimeFormat = value; }
        //}

        int m_defaultRowLimit = 200;
        [DisplayName("s_default_row_limit")]
        [Category("s_limits")]
        [SettingsKey("gui.datagrid.default_row_limit")]
        public int DefaultRowLimit
        {
            get { return m_defaultRowLimit; }
            set { m_defaultRowLimit = value; }
        }

        int m_referencesRowLimit = 100;
        [DisplayName("s_references_row_limit")]
        [Category("s_limits")]
        [SettingsKey("gui.datagrid.references_row_limit")]
        public int ReferencesRowLimit
        {
            get { return m_referencesRowLimit; }
            set { m_referencesRowLimit = value; }
        }

        int m_listRowLimit = 100;
        [DisplayName("s_list_row_limit")]
        [Category("s_limits")]
        [SettingsKey("gui.datagrid.list_row_limit")]
        public int ListRowLimit
        {
            get { return m_listRowLimit; }
            set { m_listRowLimit = value; }
        }

        int m_objectViewRowLimit = 200;

        [DisplayName("s_object_view_row_limit")]
        [Category("s_limits")]
        [SettingsKey("gui.datagrid.object_view_row_limit")]
        public int ObjectViewRowLimit
        {
            get { return m_objectViewRowLimit; }
            set { m_objectViewRowLimit = value; }
        }

        AddonHolder m_style;
        [DisplayName("s_style")]
        [SettingsKey("gui.datagrid.style")]
        [RegisterItemType(typeof(DataGridStyleAddonType))]
        [TypeConverter(typeof(RegisterItemTypeConverter))]
        [Category("s_other")]
        public AddonHolder Style
        {
            get { return m_style; }
            set { m_style = value; }
        }

        [Browsable(false)]
        public IDataGridStyle _Style
        {
            get
            {
                if (Style == null) return DefaultDataGridStyle.Instance;
                return (IDataGridStyle)m_style.InstanceModel;
            }
        }

        //Color m_lookupHintColor = Color.Gray;
        //[DisplayName("s_hint_color")]
        //[Category("s_lookup")]
        //[SettingsKey("gui.datagrid.lookup_hint_color")]
        //public Color LookupHintColor
        //{
        //    get { return m_lookupHintColor; }
        //    set { m_lookupHintColor = value; }
        //}

        bool m_showLookupHint = true;
        [DisplayName("s_show_lookup_hint")]
        [Category("s_lookup")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [SettingsKey("gui.datagrid.show_lookup_hint")]
        public bool ShowLookupHint
        {
            get { return m_showLookupHint; }
            set { m_showLookupHint = value; }
        }

        bool m_showLookupSelection = true;
        [DisplayName("s_show_lookup_selection")]
        [Category("s_lookup")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [SettingsKey("gui.datagrid.show_lookup_selection")]
        public bool ShowLookupSelection
        {
            get { return m_showLookupSelection; }
            set { m_showLookupSelection = value; }
        }

        DataGridViewAutoSizeColumnsMode m_columnsAutoSizeMode = DataGridViewAutoSizeColumnsMode.AllCells;
        [DisplayName("s_columns_auto_size_mode")]
        [Category("s_other")]
        [SettingsKey("gui.datagrid.columns_auto_size_mode")]
        public DataGridViewAutoSizeColumnsMode ColumnsAutoSizeMode
        {
            get { return m_columnsAutoSizeMode; }
            set { m_columnsAutoSizeMode = value; }
        }

        int m_rowSpacing = 70;
        [DisplayName("s_row_spacing")]
        [Category("s_other")]
        [SettingsKey("gui.datagrid.row_spacing")]
        [Description("s_space_between_rows_in_percent_of_font_height")]
        public int RowSpacing
        {
            get { return m_rowSpacing; }
            set { m_rowSpacing = value; }
        }

        int m_maxColumnWidth = 300;
        [DisplayName("s_max_column_width")]
        [Category("s_other")]
        [SettingsKey("gui.datagrid.max_column_width")]
        public int MaxColumnWidth
        {
            get { return m_maxColumnWidth; }
            set { m_maxColumnWidth = value; }
        }

        public const string ShowSqlConfirmKey = "gui.datagrid.show_sql_confirm";
        bool m_showSqlConfirm = true;
        [DisplayName("s_show_sql_confirm")]
        [Category("s_other")]
        [SettingsKey(ShowSqlConfirmKey)]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool ShowSqlConfirm
        {
            get { return m_showSqlConfirm; }
            set { m_showSqlConfirm = value; }
        }

        public const string ShowSavedInfoKey = "gui.datagrid.show_saved_info";
        bool m_showSavedInfo = true;
        [DisplayName("s_show_saved_info")]
        [Category("s_other")]
        [SettingsKey(ShowSavedInfoKey)]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool ShowSavedInfo
        {
            get { return m_showSavedInfo; }
            set { m_showSavedInfo = value; }
        }

        bool m_loadRowCount = true;
        [DisplayName("s_load_row_count")]
        [Category("s_other")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [SettingsKey("gui.datagrid.load_row_count")]
        public bool LoadRowCount
        {
            get { return m_loadRowCount; }
            set { m_loadRowCount = value; }
        }

        [Browsable(false)]
        public DateTime Now
        {
            get
            {
                if (m_useUtcTime) return DateTime.UtcNow;
                else return DateTime.Now;
            }
        }
    }

    public static class SettingsPageCollection_TableData
    {
        public static TableDataSettings TableData(this SettingsPageCollection col)
        {
            return (TableDataSettings)col.PageByName("tabledata");
        }
    }
}
