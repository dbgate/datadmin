using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DatAdmin
{
    public class SettingsPageBase : AddonBase
    {
        [Browsable(false)]
        public SettingsPageCollection Pages { get; internal set; }
        public virtual void SetDefaults()
        {
        }

        [Browsable(false)]
        public override AddonType AddonType
        {
            get { return SettingsPageAddonType.Instance; }
        }

        public void WantLoaded()
        {
            if (Pages != null) Pages.WantLoaded();
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SettingsPageAttribute : RegisterAttribute
    {
        public string ImageName;
        public SettingsTargets Targets = SettingsTargets.All;
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SettingsKeyAttribute : Attribute
    {
        public readonly string KeyName;
        public SettingsKeyAttribute(string keyName)
        {
            KeyName = keyName;
        }
    }

    [AddonType]
    public class SettingsPageAddonType : AddonType
    {
        public override string Name
        {
            get { return "settingspage"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(SettingsPageBase); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(SettingsPageAttribute); }
        }

        public static readonly SettingsPageAddonType Instance = new SettingsPageAddonType();
    }
}
