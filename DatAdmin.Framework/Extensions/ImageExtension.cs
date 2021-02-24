using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace DatAdmin
{
    public static class ImageExtension
    {
        public static Bitmap GetScaledInstance(this Image img, int dstwidth, int dstheight, int srcx, int srcy, int srcwidth, int srcheight, PixelFormat format)
        {
            Bitmap res = new Bitmap(dstwidth, dstheight, format);
            using (var g = Graphics.FromImage(res))
            {
                g.InterpolationMode = InterpolationMode.Low;
                g.DrawImage(img, new RectangleF(0, 0, dstwidth, dstheight), new RectangleF(srcx, srcy, srcwidth, srcheight), GraphicsUnit.Pixel);
            }
            return res;
        }

        public static Bitmap GetScaledInstance(this Image img, int dstwidth, int dstheight, PixelFormat format)
        {
            return GetScaledInstance(img, dstwidth, dstheight, 0, 0, img.Width, img.Height, format);
        }

        public static Bitmap FixTransparency(this Bitmap image, Color bgcolor)
        {
            if (Framework.IsMono) // make own transparency - on top-left basis
            {
                image = new Bitmap(image);
                Color topleft = image.GetPixel(0, 0);
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        if (image.GetPixel(x, y) == topleft) image.SetPixel(x, y, bgcolor);
                    }
                }
            }
            return image;
        }
    }
}
