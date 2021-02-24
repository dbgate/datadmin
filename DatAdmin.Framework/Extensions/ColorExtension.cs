using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DatAdmin
{
    public static class ColorExtension
    {
        public static string ToWebName(this Color color)
        {
            return String.Format("#{0}{1}{2}", StringTool.EncodeHex(color.R), StringTool.EncodeHex(color.G), StringTool.EncodeHex(color.B));
        }
        public static Color HalfMix(this Color one, Color two)
        {
            return Color.FromArgb(
                (one.A + two.A) >> 1,
                (one.R + two.R) >> 1,
                (one.G + two.G) >> 1,
                (one.B + two.B) >> 1);
        }
    }
}
