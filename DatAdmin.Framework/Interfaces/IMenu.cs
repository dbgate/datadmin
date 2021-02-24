using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class PopupMenuEnabledAttribute : System.Attribute
    {
        public PopupMenuEnabledAttribute(string path)
        {
            Path = path;
        }
        public readonly string Path;
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class PopupMenuVisibleAttribute : System.Attribute
    {
        public PopupMenuVisibleAttribute(string path)
        {
            Path = path;
        }
        public readonly string Path;
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PopupMenuAttribute : System.Attribute
    {
        public PopupMenuAttribute(string path)
        {
            Path = path;
        }
        public readonly string Path;
        public Keys Shortcut = Keys.None;
        public string ShortcutDisplayString;
        public string ImageName = null;
        public int Weight = 0;
        public string RequiredFeature = null;
        public MultipleMode MultiMode = MultipleMode.SingleOnly;
        public string GroupName = "misc";
        public bool HideIfNoChildren;
    }

    public enum MultipleMode { NativeMulti, Sequencable, SingleOnly }

    [AttributeUsage(AttributeTargets.Method)]
    public class DragDropOperationAttribute : Attribute
    {
        public string Name;
        public string Title;
        public string RequiredFeature = null;
        public MultipleMode MultiMode = MultipleMode.SingleOnly;
        //public SoftwareEdition MinimalVersion = SoftwareEdition.Personal;
    }
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class DragDropOperationVisibleAttribute : Attribute
    {
        public string Name;
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class DragDropOperationFilterMultiAttribute : Attribute
    {
        public string Name;
    }
}
