using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms;

namespace DatAdmin
{
    public class FontEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                if (!context.PropertyDescriptor.IsReadOnly)
                {
                    return UITypeEditorEditStyle.Modal;
                }
            }
            return UITypeEditorEditStyle.None;
        }

        [RefreshProperties(RefreshProperties.All)]
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            if (context == null || provider == null || context.Instance == null)
            {
                return base.EditValue(provider, value);
            }

            FontDialog dlg = new FontDialog();
            PersistentFont src = (PersistentFont)value;
            dlg.ShowColor = true;
            dlg.Font = (Font)src.GdiFont.Clone();
            dlg.Color = src.FontColor;
            if (dlg.ShowDialogEx() == DialogResult.OK)
            {
                return PersistentFont.FromFont(dlg.Font, dlg.Color);
            }
            else
            {
                return value;
            }
        }

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            if (context == null || context.Instance == null)
            {
                return base.GetPaintValueSupported(context);
            }
            return true;
        }
        public override void PaintValue(PaintValueEventArgs e)
        {
            if (e.Value != null)
            {
                PersistentFont font = (PersistentFont)e.Value;
                using (Brush color = new SolidBrush(font.FontColor))
                {
                    GraphicsState state = e.Graphics.Save();
                    e.Graphics.FillRectangle(color, e.Bounds);
                    e.Graphics.Restore(state);
                }
            }
            else
            {
                base.PaintValue(e);
            }
        }
    }


    [Editor(typeof(FontEditor), typeof(UITypeEditor))]
    public class PersistentFont
    {
        private Font m_fontCache = null;
        private Brush m_brushCache = null;
        private void ClearCache()
        {
            if (m_fontCache != null) m_fontCache.Dispose();
            m_fontCache = null;
            if (m_brushCache != null) m_brushCache.Dispose();
            m_brushCache = null;
        }

        private String m_fontName = "Arial";
        public String FontName
        {
            get { return m_fontName; }
            set { ClearCache(); m_fontName = value; }
        }

        private double m_fontSize = 10;
        public double FontSize
        {
            get { return m_fontSize; }
            set { ClearCache(); m_fontSize = value; }
        }

        private FontStyle m_fontStyle = FontStyle.Regular;
        [XmlIgnore]
        public FontStyle FontStyle
        {
            get { return m_fontStyle; }
            set { ClearCache(); m_fontStyle = value; }
        }

        private Color m_fontColor = Color.Black;
        [System.Xml.Serialization.XmlIgnore]
        public Color FontColor
        {
            get { return m_fontColor; }
            set { ClearCache(); m_fontColor = value; }
        }

        [System.Xml.Serialization.XmlElement("FontColor")]
        [Browsable(false)]
        public string _ColorName
        {
            get { return m_fontColor.Name; }
            set { ClearCache(); m_fontColor = Color.FromName(value); }
        }

        public static PersistentFont FromFont(Font font)
        {
            return FromFont(font, Color.Black);
        }

        public static PersistentFont FromFont(Font font, Color color)
        {
            PersistentFont result = new PersistentFont();
            result.FontName = font.Name;
            result.FontSize = font.Size;
            result.FontStyle = font.Style;
            result.FontColor = color;
            return result;
        }

        [Browsable(false)]
        [XmlIgnore]
        public Font GdiFont
        {
            get
            {
                if (m_fontCache == null) m_fontCache = new Font(FontName, (float)FontSize, FontStyle);
                return m_fontCache;
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public Brush GdiBrush
        {
            get
            {
                if (m_brushCache == null) m_brushCache = new SolidBrush(m_fontColor);
                return m_brushCache;
            }
        }

        public bool Bold
        { 
            get { return (m_fontStyle & FontStyle.Bold) == FontStyle.Bold; }
            set { if (value) m_fontStyle |= FontStyle.Bold; else m_fontStyle &= ~FontStyle.Bold; }
        }
        public bool Italic { 
            get { return (m_fontStyle & FontStyle.Italic) == FontStyle.Italic; }
            set { if (value) m_fontStyle |= FontStyle.Italic;else m_fontStyle &= ~FontStyle.Italic; }
        }
        public bool Underline
        {
            get { return (m_fontStyle & FontStyle.Underline) == FontStyle.Underline; }
            set { if (value) m_fontStyle |= FontStyle.Underline; else m_fontStyle &= ~FontStyle.Underline; }
        }
        public bool Strikeout
        {
            get { return (m_fontStyle & FontStyle.Strikeout) == FontStyle.Strikeout; }
            set { if (value) m_fontStyle |= FontStyle.Strikeout; else m_fontStyle &= ~FontStyle.Strikeout; }
        }
        public string GetTitle()
        {
            string flags = "";
            if (Bold) flags += "B";
            if (Italic) flags += "I";
            if (Strikeout) flags += "S";
            if (Underline) flags += "U";
            return String.Format("{0} {1}pt {2}", FontName, FontSize, flags);
        }

        public override string ToString()
        {
            return GetTitle();
        }
    }
}
