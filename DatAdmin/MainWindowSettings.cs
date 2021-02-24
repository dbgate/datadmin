using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DatAdmin
{
    public enum ShowFavoriteMode
    {
        [Description("s_yes")]
        Yes,
        [Description("s_no")]
        No,
        [Description("s_if_no_empty")]
        IfNoEmpty,
    }

    [SettingsPage(Name = "main_window", Title = "s_main_window", Targets = SettingsTargets.Global, ImageName = CoreIcons.alone_windowName)]
    public class MainWindowSettings : SettingsPageBase
    {
        private ShowFavoriteMode m_showFavoritesMenu = ShowFavoriteMode.Yes;
        [DisplayName("s_show_favorites_menu")]
        [Category("s_favorites")]
        [TypeConverter(typeof(EnumDescConverter))]
        [SettingsKey("gui.main_window.show_favorites_menu")]
        public ShowFavoriteMode ShowFavoritesMenu
        {
            get { return m_showFavoritesMenu; }
            set { m_showFavoritesMenu = value; }
        }

        private ShowFavoriteMode m_showFavoritesToolBar = ShowFavoriteMode.IfNoEmpty;
        [DisplayName("s_show_favorites_toolbar")]
        [Category("s_favorites")]
        [TypeConverter(typeof(EnumDescConverter))]
        [SettingsKey("gui.main_window.show_favorites_toolbar")]
        public ShowFavoriteMode ShowFavoritesToolBar
        {
            get { return m_showFavoritesToolBar; }
            set { m_showFavoritesToolBar = value; }
        }

        private bool m_showToolBar = true;
        [DisplayName("s_show_toolbar")]
        [Category("s_other")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [SettingsKey("gui.main_window.show_toolbar")]
        public bool ShowToolBar
        {
            get { return m_showToolBar; }
            set { m_showToolBar = value; }
        }

        public static MainWindowSettings Page
        {
            get { return (MainWindowSettings)GlobalSettings.Pages.PageByName("main_window"); }
        }
    }

    public static class ShowFavoriteModeExtension
    {
        public static bool GetVisibility(this ShowFavoriteMode mode, FavoriteGroup grp)
        {
            switch (mode)
            {
                case ShowFavoriteMode.IfNoEmpty:
                    return grp.GetItems().Count > 0;
                case ShowFavoriteMode.Yes:
                    return true;
                case ShowFavoriteMode.No:
                    return false;
            }
            return true;
        }
    }
}
