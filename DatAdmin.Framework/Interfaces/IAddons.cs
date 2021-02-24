using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DatAdmin
{
    public enum RegisterItemUsage { CreateTemplate, Deserialize, DirectUse };


    //public enum SoftwareEdition
    //{
    //    Personal,
    //    Professional,
    //    Ultimate,
    //    Evaluation,
    //}

    public class RegisterAttribute : Attribute
    {
        public string Name;
        public string Title;
        public string Description;
        public bool SupportsCreateTemplate = true;
        public bool SupportsDirectUse = true;
        public bool SupportsDeserialize = true;
        public string RequiredFeature = null;
        //public SoftwareEdition MinimalVersion = SoftwareEdition.Personal;
    }

    public interface IAddonInstance
    {
        void LoadFromXml(XmlElement xml);
        void SaveToXml(XmlElement xml);
        AddonType AddonType { get; }
    }

    public interface IFileBasedAddonInstance : IAddonInstance
    {
        string AddonFileName { get; set; }
    }

    public interface INamedAddonInstance : IAddonInstance
    {
        string AddonName { get; set; }
    }
}
