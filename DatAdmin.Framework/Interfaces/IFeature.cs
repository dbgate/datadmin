using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FeatureAttribute : RegisterAttribute
    {
    }

    public interface IFeature
    {
        bool AllwaysAllowed { get; }
    }

    [AddonType]
    public class FeatureAddonType : AddonType
    {
        public override string Name
        {
            get { return "feature"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IFeature); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(FeatureAttribute); }
        }

        public static readonly FeatureAddonType Instance = new FeatureAddonType();

        public static string GetTitle(string features)
        {
            var res = new List<string>();
            foreach(string item in features.Split('|'))
            {
                AddonHolder addon = Instance.FindHolder(item);
                if (addon == null) continue;
                res.Add(addon.Attrib.Title);
                var attr = (FeatureAttribute)addon.Attrib;
            }
            return res.CreateDelimitedText(",");
        }
    }

    public abstract class FeatureBase : AddonBase, IFeature
    {
        public override AddonType AddonType
        {
            get { return FeatureAddonType.Instance; }
        }

        public virtual bool AllwaysAllowed
        {
            get { return false; }
        }
    }
}
