using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DatAdmin
{
    public class DragObjectContainer
    {
        public readonly object Data;
        public readonly Image Image;
        public readonly string Title;
        public DragObjectContainer(object data, string title, Image image)
        {
            Data = data;
            Title = title;
            Image = image;
        }

        public static DragObjectContainer Create(AppObject[] objs)
        {
            DragObjectContainer res = new DragObjectContainer(objs, objs[0].ToString(), objs[0].Image);
            return res;
        }
    }
}
