using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DatAdmin
{
    public class EncodingTypeConverter : TypeConverter
    {
        static Dictionary<string, string> m_encodingNames = new Dictionary<string, string>();
        static Dictionary<string, string> m_encodingTitles = new Dictionary<string, string>();
        static List<string> m_encodings = new List<string>();
        public static List<EncodingItem> EncodingItems = new List<EncodingItem>();

        static EncodingTypeConverter()
        {
            foreach (EncodingInfo info in Encoding.GetEncodings())
            {
                Encoding enc;
                try
                {
                    enc = info.GetEncoding();
                }
                catch
                {
                    continue;
                }
                m_encodings.Add(enc.WebName);
                string title = enc.WebName + ": " + enc.EncodingName;
                m_encodingNames[enc.EncodingName] = enc.WebName;
                m_encodingNames[enc.WebName] = enc.WebName;
                m_encodingNames[title] = enc.WebName;
                m_encodingTitles[enc.WebName] = title;
                EncodingItems.Add(new EncodingItem { WebName = enc.WebName, Title = title });
            }
            m_encodings.Sort();
            EncodingItems.Sort();
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string))
            {
                return m_encodingNames[value.ToString()];
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return m_encodingTitles[value.ToString()];
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new System.ComponentModel.TypeConverter.StandardValuesCollection(m_encodings);
        }

        public static int GetEncodingIndex(Encoding encoding)
        {
            for (int i = 0; i < EncodingItems.Count; i++)
            {
                if (EncodingItems[i].WebName == encoding.WebName) return i;
            }
            return -1;
        }
    }

    public class EncodingItem : IComparable
    {
        public string Title;
        public string WebName;

        public override string ToString()
        {
            return Title;
        }

        public Encoding RealEncoding { get { return Encoding.GetEncoding(WebName); } }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            var ei = obj as EncodingItem;
            if (ei != null) return String.Compare(WebName, ei.WebName);
            throw new NotImplementedError("DAE-00142");
        }

        #endregion
    }
}
