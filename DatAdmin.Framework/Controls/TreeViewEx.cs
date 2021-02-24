using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Runtime.InteropServices;

namespace DatAdmin
{
    public partial class TreeViewEx : TreeView
    {
        public TreeViewEx()
        {
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);
            DragScroll(drgevent);
        }

        private void DragScroll(DragEventArgs e)
        {
            // Set a constant to define the autoscroll region
            const Single scrollRegion = 20;

            // See where the cursor is
            Point pt = PointToClient(Cursor.Position);

            // See if we need to scroll up or down
            if ((pt.Y + scrollRegion) > Height)
            {
                // Call the API to scroll down
                SendMessage(Handle, (int)277, (int)1, 0);
            }
            else if (pt.Y < scrollRegion)
            {
                // Call thje API to scroll up
                SendMessage(Handle, (int)277, (int)0, 0);
            }
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            const int WM_LBUTTONDBLCLK = 0x203;
            switch (m.Msg)
            {
                case WM_LBUTTONDBLCLK:
                    {
                        Point pt = new Point(m.LParam.ToInt32());
                        MouseEventArgs ev = new MouseEventArgs(MouseButtons.Left, 0, pt.X, pt.Y, 0);
                        OnMouseDoubleClick(ev);
                        return;
                    }
            }
            base.WndProc(ref m);
        }


        // MULTISELECT SUPPORT - will be released in future
        //// class members

        //protected ArrayList m_coll = new ArrayList();
        //protected TreeNode m_lastNode, m_firstNode;


        //public TreeViewEx()
        //{
        //    this.DoubleBuffered = true;
        //}

        ///* API method */
        //public ArrayList SelectedNodes
        //{
        //    get
        //    {
        //        return m_coll;
        //    }
        //    set
        //    {
        //        removePaintFromNodes();
        //        m_coll.Clear();
        //        m_coll = value;
        //        paintSelectedNodes();
        //    }
        //}

        //protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        //{
        //    // e.Node is the current node exposed by the base TreeView control

        //    base.OnBeforeSelect(e);

        //    bool bControl = (ModifierKeys == Keys.Control);
        //    bool bShift = (ModifierKeys == Keys.Shift);

        //    // selecting twice the node while pressing CTRL ?

        //    if (bControl && m_coll.Contains(e.Node))
        //    {
        //        // unselect it

        //        // (let framework know we don't want selection this time)

        //        e.Cancel = true;

        //        // update nodes

        //        removePaintFromNodes();
        //        m_coll.Remove(e.Node);
        //        paintSelectedNodes();
        //        return;
        //    }

        //    m_lastNode = e.Node;
        //    if (!bShift) m_firstNode = e.Node; // store begin of shift sequence

        //}

        //protected override void OnAfterSelect(TreeViewEventArgs e)
        //{
        //    // e.Node is the current node exposed by the base TreeView control


        //    base.OnAfterSelect(e);

        //    bool bControl = (ModifierKeys == Keys.Control);
        //    bool bShift = (ModifierKeys == Keys.Shift);

        //    if (bControl)
        //    {
        //        if (!m_coll.Contains(e.Node)) // new node ?
        //        {
        //            m_coll.Add(e.Node);
        //        }
        //        else  // not new, remove it from the collection
        //        {
        //            removePaintFromNodes();
        //            m_coll.Remove(e.Node);
        //        }
        //        paintSelectedNodes();
        //    }
        //    else
        //    {
        //        if (bShift)
        //        {
        //            Queue myQueue = new Queue();

        //            TreeNode uppernode = m_firstNode;
        //            TreeNode bottomnode = e.Node;

        //            // case 1 : begin and end nodes are parent

        //            bool bParent = isParent(m_firstNode, e.Node);
        //            if (!bParent)
        //            {
        //                bParent = isParent(bottomnode, uppernode);
        //                if (bParent) // swap nodes
        //                {
        //                    TreeNode t = uppernode;
        //                    uppernode = bottomnode;
        //                    bottomnode = t;
        //                }
        //            }
        //            if (bParent)
        //            {
        //                TreeNode n = bottomnode;
        //                while (n != uppernode.Parent)
        //                {
        //                    if (!m_coll.Contains(n)) // new node ?

        //                        myQueue.Enqueue(n);

        //                    n = n.Parent;
        //                }
        //            }
        //            // case 2 : nor the begin nor the

        //            // end node are descendant one another

        //            else
        //            {
        //                // are they siblings ?                 


        //                if ((uppernode.Parent == null && bottomnode.Parent == null)
        //                      || (uppernode.Parent != null &&
        //                      uppernode.Parent.Nodes.Contains(bottomnode)))
        //                {
        //                    int nIndexUpper = uppernode.Index;
        //                    int nIndexBottom = bottomnode.Index;
        //                    if (nIndexBottom < nIndexUpper) // reversed?
        //                    {
        //                        TreeNode t = uppernode;
        //                        uppernode = bottomnode;
        //                        bottomnode = t;
        //                        nIndexUpper = uppernode.Index;
        //                        nIndexBottom = bottomnode.Index;
        //                    }

        //                    TreeNode n = uppernode;
        //                    while (nIndexUpper <= nIndexBottom)
        //                    {
        //                        if (!m_coll.Contains(n)) // new node ?

        //                            myQueue.Enqueue(n);

        //                        n = n.NextNode;

        //                        nIndexUpper++;
        //                    } // end while


        //                }
        //                else
        //                {
        //                    if (!m_coll.Contains(uppernode))
        //                        myQueue.Enqueue(uppernode);
        //                    if (!m_coll.Contains(bottomnode))
        //                        myQueue.Enqueue(bottomnode);
        //                }

        //            }

        //            m_coll.AddRange(myQueue);

        //            paintSelectedNodes();
        //            // let us chain several SHIFTs if we like it

        //            m_firstNode = e.Node;

        //        } // end if m_bShift

        //        else
        //        {
        //            // in the case of a simple click, just add this item

        //            if (m_coll != null && m_coll.Count > 0)
        //            {
        //                removePaintFromNodes();
        //                m_coll.Clear();
        //            }
        //            m_coll.Add(e.Node);
        //        }
        //    }
        //}


        //// Helpers

        ////

        ////



        //protected bool isParent(TreeNode parentNode, TreeNode childNode)
        //{
        //    if (parentNode == childNode)
        //        return true;

        //    TreeNode n = childNode;
        //    bool bFound = false;
        //    while (!bFound && n != null)
        //    {
        //        n = n.Parent;
        //        bFound = (n == parentNode);
        //    }
        //    return bFound;
        //}

        //protected void paintSelectedNodes()
        //{
        //    foreach (TreeNode n in m_coll)
        //    {
        //        n.BackColor = SystemColors.Highlight;
        //        n.ForeColor = SystemColors.HighlightText;
        //    }
        //}

        //protected void removePaintFromNodes()
        //{
        //    if (m_coll.Count == 0) return;

        //    TreeNode n0 = (TreeNode)m_coll[0];
        //    Color back = n0.TreeView.BackColor;
        //    Color fore = n0.TreeView.ForeColor;

        //    foreach (TreeNode n in m_coll)
        //    {
        //        n.BackColor = back;
        //        n.ForeColor = fore;
        //    }
        //}
    }
}
