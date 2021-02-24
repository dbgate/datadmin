using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DatAdmin
{
    [SettingsPage(Name = "editor", Title = "s_editor", Targets = SettingsTargets.Global, ImageName = CoreIcons.sqlName)]
    public class EditorSettings : SettingsPageBase
    {
        bool m_showLineNumbers = true;
        [DisplayName("s_show_line_numbers")]
        [SettingsKey("gui.editor.show_line_numbers")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool ShowLineNumbers
        {
            get { return m_showLineNumbers; }
            set { m_showLineNumbers = value; }
        }

        bool m_showMatchingBracket = true;
        [DisplayName("s_show_matching_bracket")]
        [SettingsKey("gui.editor.show_matching_bracket")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool ShowMatchingBracket
        {
            get { return m_showMatchingBracket; }
            set { m_showMatchingBracket = value; }
        }

        bool m_useTabs = true;
        [DisplayName("s_use_tabs")]
        [SettingsKey("gui.editor.use_tabs")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool UseTabs
        {
            get { return m_useTabs; }
            set { m_useTabs = value; }
        }

        int m_tabWidth = 4;
        [DisplayName("s_tab_width")]
        [SettingsKey("gui.editor.tab_width")]
        public int TabWidth
        {
            get { return m_tabWidth; }
            set { m_tabWidth = value; }
        }

        bool m_autoIndent = true;
        [DisplayName("s_auto_indent")]
        [SettingsKey("gui.editor.auto_indent")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool AutoIndent
        {
            get { return m_autoIndent; }
            set { m_autoIndent = value; }
        }

        bool m_showTabs = false;
        [DisplayName("s_show_tabs")]
        [SettingsKey("gui.editor.show_tabs")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool ShowTabs
        {
            get { return m_showTabs; }
            set { m_showTabs = value; }
        }

        bool m_showSpaces = false;
        [DisplayName("s_show_spaces")]
        [SettingsKey("gui.editor.show_spaces")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool ShowSpaces
        {
            get { return m_showSpaces; }
            set { m_showSpaces = value; }
        }

        bool m_showEOLMarkers = false;
        [DisplayName("s_show_eol_markers")]
        [SettingsKey("gui.editor.show_eol_markers")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool ShowEOLMarkers
        {
            get { return m_showEOLMarkers; }
            set { m_showEOLMarkers = value; }
        }

        bool m_highlightCurrentLine = false;
        [DisplayName("s_hightlight_current_line")]
        [SettingsKey("gui.editor.hightlight_current_line")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool HighlightCurrentLine
        {
            get { return m_highlightCurrentLine; }
            set { m_highlightCurrentLine = value; }
        }
    }

    public static class SettingsPageCollection_Editor
    {
        public static EditorSettings Editor(this SettingsPageCollection col)
        {
            return (EditorSettings)col.PageByName("editor");
        }
    }
}
