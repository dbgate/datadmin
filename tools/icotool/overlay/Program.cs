using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace overlay
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: overlay infile directory_with_overlays");
                return;
            }
            string infile = args[0];
            string odir = args[1];
            var ibmp = new Bitmap(infile);
            foreach (string fn in Directory.GetFiles(odir))
            {
                var obmp = new Bitmap(fn);
                var res = new Bitmap(ibmp.Width, ibmp.Height, PixelFormat.Format32bppArgb);
                for (int y = 0; y < res.Height; y++)
                {
                    for (int x = 0; x < res.Width; x++)
                    {
                        var color = ibmp.GetPixel(x, y);
                        int srcx = x - ibmp.Width + obmp.Width;
                        int srcy = y - ibmp.Height + obmp.Height;
                        if (srcx >= 0 && srcy >= 0 && srcx < obmp.Width && srcy < obmp.Height)
                        {
                            var color2 = obmp.GetPixel(srcx, srcy);
                            color = Mix(color, color2, color2.A / 255.0f);
                        }
                        res.SetPixel(x, y, color);
                    }
                }
                //using (var g = Graphics.FromImage(res))
                //{
                //    g.DrawImage(ibmp, 0, 0);
                //    g.DrawImage(obmp, 0, 0);
                //}
                string ext = Path.GetExtension(infile);
                string ofn = infile.Substring(0, infile.Length - ext.Length) + "_" + Path.GetFileNameWithoutExtension(fn) + ext;
                Console.Out.WriteLine("Creating file " + ofn);
                res.Save(ofn);
            }
        }

        public static Color Mix(Color from, Color to, float percent)
        {
            float amountFrom = 1.0f - percent;

            return Color.FromArgb(
            (byte)(from.A * amountFrom + to.A * percent),
            (byte)(from.R * amountFrom + to.R * percent),
            (byte)(from.G * amountFrom + to.G * percent),
            (byte)(from.B * amountFrom + to.B * percent));
        }
    }
}
