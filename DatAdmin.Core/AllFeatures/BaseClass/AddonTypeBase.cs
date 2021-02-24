using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

//namespace DatAdmin
//{
//    public abstract class AddonTypeBase : IAddonType
//    {
//        protected RegisterBase m_register;

//        public AddonTypeBase(RegisterBase register)
//        {
//            m_register = register;
//        }

//        #region IAddonType Members

//        public AddonBase LoadAddon(XmlElement xml)
//        {
//            var res = (AddonBase)m_register.FindItem(xml.GetAttribute("type"), RegisterItemUsage.Deserialize).CreateInstance();
//            res.LoadFromXml(xml);
//            return res;
//        }

//        public virtual string Name
//        {
//            get { return XmlTool.GetRegisterType(this); }
//        }

//        #endregion
//    }
//}
