using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DatAdmin
{
    public class ImageCache
    {
        ImageList m_list;
        Dictionary<Bitmap, int> m_images = new Dictionary<Bitmap, int>();
        Color m_bgcolor;

        public ImageCache(ImageList list, Color bgcolor)
        {
            m_list = list;
            m_bgcolor = bgcolor;
        }

        public int GetImageIndex(Bitmap image)
        {
            if (image == null) image = CoreIcons.treenode;
            if (m_images.ContainsKey(image)) return m_images[image];

            image = image.FixTransparency(m_bgcolor);

            int res = m_list.Images.Count;
            m_list.Images.Add(image);
            m_images[image] = res;
            return res;
        }

        public ImageList Images { get { return m_list; } }
    }
}
