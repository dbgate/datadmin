using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DatAdmin
{
    public class DragDropTarget : Panel
    {
        DragObjectContainer[] m_memory = new DragObjectContainer[2];

        public DragDropTarget()
        {
            AllowDrop = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int itwi = Width / TargetCount;
            for (int i = 0; i < TargetCount; i++)
            {
                GraphicsState oldstate = e.Graphics.Save();
                DragObjectContainer obj = m_memory[i];
                int x0 = itwi * i;
                e.Graphics.Clip = new Region(new Rectangle(x0, 0, itwi, Height));
                e.Graphics.DrawRectangle(Pens.Black, x0 + 2, 2, itwi - 4, Height - 4);
                if (obj != null)
                {
                    int imwi = 0;
                    if (obj.Image != null)
                    {
                        e.Graphics.DrawImage(obj.Image, x0 + 3, 3);
                        imwi = obj.Image.Width;
                    }
                    if (obj.Title != null) e.Graphics.DrawString(obj.Title, Font, Brushes.Black, x0 + imwi + 4, 4);
                }
                else
                {
                    e.Graphics.DrawString(Texts.Get("s_drag_and_drop_here"), Font, Brushes.Black, x0 + 4, 4);
                }
                e.Graphics.Restore(oldstate);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            int itwi = Width / TargetCount;
            int index = e.X / itwi;
            if (index >= 0 && index < TargetCount && m_memory[index] != null) DoDragDrop(m_memory[index], DragDropEffects.Copy);
        }

        int GetMemoryIndex(DragEventArgs drgevent)
        {
            int x = PointToClient(new Point(drgevent.X, drgevent.Y)).X;
            int itwi = Width / TargetCount;
            return x / itwi;
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);

            DragObjectContainer obj = (DragObjectContainer)drgevent.Data.GetData(typeof(DragObjectContainer));
            if (obj != null)
            {
                int index = GetMemoryIndex(drgevent);
                if (index >= 0 && index < TargetCount)
                {
                    drgevent.Effect = DragDropEffects.Copy;
                }
            }
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);
            DragObjectContainer obj = (DragObjectContainer)drgevent.Data.GetData(typeof(DragObjectContainer));
            if (obj != null)
            {
                int index = GetMemoryIndex(drgevent);
                if (index >= 0 && index < TargetCount) m_memory[index] = obj;
                Refresh();
            }
        }

        public int TargetCount
        {
            get { return m_memory.Length; }
            set
            {
                m_memory = new DragObjectContainer[value];
                Refresh();
            }
        }

        public DragObjectContainer[] Memory { get { return m_memory; } }
    }
}
