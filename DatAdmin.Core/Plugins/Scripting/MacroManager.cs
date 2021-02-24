using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public delegate void FormDelegate(Form window);
    public class MacroManager
    {
        static IMacroListener m_listener;

        public static IMacroListener Listener
        {
            get { return m_listener; }
            set
            {
                if (value != null && Listener != null && value != Listener)
                {
                    throw new Exception("DAE-00259 Duplicate macro listener");
                }
                m_listener = value;
            }
        }

        public static event FormDelegate OpenedWindow;
        static Form m_lastOpenedWindow;

        public static Form LastOpenedWindow
        {
            get { return MacroManager.m_lastOpenedWindow; }
        }

        public static void RegisterNonModalWindow(Form window)
        {
            if (OpenedWindow != null) OpenedWindow(window);
            m_lastOpenedWindow = window;
        }

        public static DialogResult ShowDialog(Form window)
        {
            if (Listener != null) return Listener.ShowDialog(window);
            else return window.ShowDialog();
        }
        // should be called from Form.Shown of DialogWindow
        public static void RunDialogMacro(Form window)
        {
            if (Listener != null) Listener.RunDialogMacro(window);
        }

        public static DialogResult ShowCommonDialog(CommonDialog dialog)
        {
            return dialog.ShowDialog();
        }

        public static void SetPopupMenuObject(object obj)
        {
            if (Listener != null) Listener.SetPopupMenuObject(obj);
        }

        public static void RunPopupMenuCommand(string path)
        {
            if (Listener != null) Listener.RunPopupMenuCommand(path);
        }

        public static void ExpandNode(ITreeNode node)
        {
            if (Listener != null) Listener.ExpandNode(node);
        }

        public static void DeleteNode(ITreeNode node)
        {
            if (Listener != null) Listener.DeleteNode(node);
        }

        public static void DoubleClickNode(ITreeNode node)
        {
            if (Listener != null) Listener.DoubleClickNode(node);
        }

        public static void DragDropNode(ITreeNode targetNode, ITreeNode draggedNode)
        {
            if (Listener != null) Listener.DragDropNode(targetNode, draggedNode);
        }

        public static void DropFileIntoTree(TreeView tree, string file)
        {
            if (Listener != null) Listener.DropFileIntoTree(tree, file);
        }

        public static void RenameNode(ITreeNode node, string newname)
        {
            if (Listener != null) Listener.RenameNode(node, newname);
        }
    }

    public interface IMacroListener
    {
        DialogResult ShowDialog(Form window);
        void RunDialogMacro(Form window);
        void SetPopupMenuObject(object obj);
        void RunPopupMenuCommand(string path);
        void ExpandNode(ITreeNode node);
        void DeleteNode(ITreeNode node);
        void DoubleClickNode(ITreeNode node);
        void DragDropNode(ITreeNode targetNode, ITreeNode draggedNode);
        void DropFileIntoTree(TreeView tree, string file);
        void RenameNode(ITreeNode node, string newname);
    }
}
