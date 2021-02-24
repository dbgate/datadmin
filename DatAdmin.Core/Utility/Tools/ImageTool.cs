using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace DatAdmin
{
    public static class ImageTool
    {
        public const string COMBI_PREFIX = "@combi:";
        public const string COMBI_SEPARATOR = "|";

        static Dictionary<Tuple<Bitmap, Bitmap>, Bitmap> m_combineCache = new Dictionary<Tuple<Bitmap, Bitmap>, Bitmap>();
        static Dictionary<string, Bitmap> m_nameCache = new Dictionary<string, Bitmap>();

        public static Bitmap CombineImages(Bitmap img1, Bitmap img2)
        {
            var key = new Tuple<Bitmap, Bitmap>(img1, img2);
            if (m_combineCache.ContainsKey(key)) return m_combineCache[key];
            var res = new Bitmap(img1.Width, img1.Height, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(res))
            {
                g.DrawImage(img1, 0, 0);
                g.DrawImage(img2, 0, 0);
            }
            m_combineCache[key] = res;
            return res;
        }

        public static string CombineImageNames(params string[] names)
        {
            return COMBI_PREFIX + names.CreateDelimitedText(COMBI_SEPARATOR);
        }

        public static Bitmap ImageFromName(string name, Bitmap defvalue)
        {
            if (name == null) return defvalue;
            if (m_nameCache.ContainsKey(name)) return m_nameCache[name];
            if (name.StartsWith(COMBI_PREFIX))
            {
                string[] names = name.Substring(COMBI_PREFIX.Length).Split(new string[] { COMBI_SEPARATOR }, StringSplitOptions.None);
                if (names.Length == 0) return defvalue;
                Bitmap res = CoreIcons.IconTable.Get(names[0], defvalue);
                for (int i = 1; i < names.Length; i++)
                {
                    Bitmap overlay = CoreIcons.IconTable.Get(names[i], null);
                    if (overlay != null) res = CombineImages(res, overlay);
                }
                m_nameCache[name] = res;
                return res;
            }
            else
            {
                return CoreIcons.IconTable.Get(name, defvalue);
            }
        }
    }
}
