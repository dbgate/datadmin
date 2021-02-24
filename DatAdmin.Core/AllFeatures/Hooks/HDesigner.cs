using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class HDesigner
    {
        [Flags]
        public enum WidgetPart : int
        {
            Icon = 0x1,
            Title = 0x2,
            Data = 0x4,
            All = 0xFFFF,
        }

        public static event Action<IToolboxGenerator> ShowInToolbox;
        public static event Action<IToolboxItem> UseToolBoxItem;
        public static event Action<object> ShowProperties;
        public static event Action<IWidget, WidgetPart> ChangedWidget;
        public static event Action<object> HideProperties;

        public static void CallShowInToolbox(IToolboxGenerator gen)
        {
            if (ShowInToolbox != null) ShowInToolbox(gen);
        }

        public static void CallUseToolBoxItem(IToolboxItem item)
        {
            if (UseToolBoxItem != null) UseToolBoxItem(item);
        }

        public static void CallShowProperties(object value)
        {
            if (ShowProperties != null) ShowProperties(value);
        }

        public static void CallHideProperties(object value)
        {
            if (HideProperties != null) HideProperties(value);
        }

        public static void CallChangedWidget(IWidget widget, WidgetPart part)
        {
            if (ChangedWidget != null) ChangedWidget(widget, part);
        }
    }
}
