using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public interface ICommandLineCommand : IAddonInstance
    {
        string Name { get;}
        string Description { get;}
        ICommandLineCommandInstance CreateInstance();
        bool AllowExtParams { get; }
    }

    public interface ICommandLineCommandInstance
    {
        void RunCommand();
        Dictionary<string, string> ExtParams { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CommandLineParameterAttribute : Attribute
    {
        public string Name;
        public string ShortName;
        public string Description;
        public bool Positional = false;
        public bool Mandatory = false;
        public string DefaultValue;
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CommandLineParameterCollectionAttribute : Attribute
    {
        public string Prefix;
        public string Description;
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class CommandLineCommandAttribute : RegisterAttribute
    {
    }

    [AddonType]
    public class CommandLineCommandAddonType : AddonType
    {
        public override Type InterfaceType
        {
            get { return typeof(ICommandLineCommand); }
        }

        public override string Name
        {
            get { return "cmdline"; }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(CommandLineCommandAttribute); }
        }

        public static readonly CommandLineCommandAddonType Instance = new CommandLineCommandAddonType();
    }
}
