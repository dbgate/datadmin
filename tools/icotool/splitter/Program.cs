using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace splitter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: splitter infile");
                Console.WriteLine("OR: splitter infile size");
                Console.WriteLine("OR: splitter infile size space");
                Console.WriteLine("OR: splitter infile width height horspace verspace");

                return;
            }
            var bmp = new Bitmap(args[0]);
            int w = 16, h = 16;
            int spaceh = 0, spacev = 0;
            if (args.Length == 2)
            {
                h = w = Int32.Parse(args[1]);
            }
            if (args.Length == 3)
            {
                h = w = Int32.Parse(args[1]);
                spaceh = spacev = Int32.Parse(args[2]);
            }
            if (args.Length == 5)
            {
                w = Int32.Parse(args[1]);
                h = Int32.Parse(args[2]);
                spaceh = Int32.Parse(args[3]);
                spacev = Int32.Parse(args[4]);
            }
            string dirname = args[0] + ".split";
            if (Directory.Exists(dirname)) Directory.Delete(dirname, true);
            Directory.CreateDirectory(dirname);
            int y = 0, yi = 0;
            while (y + h <= bmp.Height)
            {
                int x = 0, xi = 0;
                while (x + w <= bmp.Width)
                {
                    string itemfn = Path.Combine(dirname, String.Format("{0}_{1}.png", yi + 1, xi + 1));
                    Console.WriteLine("Creating file " + itemfn);
                    var item = new Bitmap(w, h, PixelFormat.Format32bppArgb);
                    using (var g = Graphics.FromImage(item))
                    {
                        g.DrawImage(bmp,
                            new Rectangle(0, 0, w, h),
                            new Rectangle(x, y, w, h),
                            GraphicsUnit.Pixel);
                    }
                    item.Save(itemfn);
                    x += w + spaceh; xi++;
                }
                y += h + spacev; yi++;
            }
        }
    }
}
