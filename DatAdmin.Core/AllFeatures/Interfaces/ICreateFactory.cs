using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DatAdmin
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CreateFactoryItemAttribute : RegisterAttribute
    {
    }

    public interface ICreateFactoryItem
    {
        string Name { get;}
        string GroupName { get;}
        string Title { get;}
        string Group { get;}
        Bitmap Bitmap { get;}
        string InfoText { get; }
        bool AllowCreateFiles { get; }
        bool Create(ITreeNode parent, string name);
        bool CreateFile(string filename);
        string FileExtensions { get; }
        int Weight { get; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class CreateFactoryAttribute : RegisterAttribute
    {
    }

    public interface ICreateFactory
    {
        ICreateFactoryItem[] GetItems(ITreeNode parent);
    }

    [AddonType]
    public class CreateFactoryAddonType : AddonType
    {
        public override Type InterfaceType
        {
            get { return typeof(ICreateFactory); }
        }
        public override string Name
        {
            get { return "createfactory"; }
        }
        public override Type RegisterAttributeType
        {
            get { return typeof(CreateFactoryAttribute); }
        }
        public static CreateFactoryAddonType Instance = new CreateFactoryAddonType();

        public static IEnumerable<ICreateFactoryItem> GetItems(ITreeNode parent)
        {
            foreach (var item in Instance.CommonSpace.GetAllAddons())
            {
                foreach (var it2 in ((ICreateFactory)item.InstanceModel).GetItems(parent))
                {
                    yield return it2;
                }
            }
            foreach (var item in CreateFactoryItemAddonType.Instance.CommonSpace.GetAllAddons())
            {
                yield return (ICreateFactoryItem)item.InstanceModel;
            }
        }
    }

    [AddonType]
    public class CreateFactoryItemAddonType : AddonType
    {
        public override Type InterfaceType
        {
            get { return typeof(ICreateFactoryItem); }
        }
        public override string Name
        {
            get { return "createfactoryitem"; }
        }
        public override Type RegisterAttributeType
        {
            get { return typeof(CreateFactoryItemAttribute); }
        }
        public static CreateFactoryItemAddonType Instance = new CreateFactoryItemAddonType();
    }
}
