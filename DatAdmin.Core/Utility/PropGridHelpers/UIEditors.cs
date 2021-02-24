using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace DatAdmin
{
    public enum FileDialogType { Open, Save }

    public abstract class FileNameEditorBase : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                if (!context.PropertyDescriptor.IsReadOnly)
                {
                    return UITypeEditorEditStyle.Modal;
                }
            }
            return UITypeEditorEditStyle.None;
        }

        [RefreshProperties(RefreshProperties.All)]
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            if (context == null || provider == null || context.Instance == null)
            {
                return base.EditValue(provider, value);
            }
            FileDialogType dtype;
            string filter;
            GetDialogProps(context, out dtype, out filter);
            FileDialog dlg = null;
            if (dtype == FileDialogType.Open)
            {
                dlg = new OpenFileDialog();
            }
            if (dtype == FileDialogType.Save)
            {
                dlg = new SaveFileDialog();
            }
            dlg.FileName = (string)value;
            dlg.Filter = filter;
            if (dlg.ShowDialogEx() == DialogResult.OK)
            {
                return dlg.FileName;
            }
            else
            {
                return value;
            }
        }
        protected abstract void GetDialogProps(ITypeDescriptorContext context, out FileDialogType dialogType, out string filter);
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class FileNameEditorPropsAttribute : Attribute
    {
        public readonly string Filter;
        public readonly FileDialogType DialogType;

        public FileNameEditorPropsAttribute(FileDialogType dtype, string extension)
        {
            DialogType = dtype;
            Filter = String.Format("{1} {0}|*.{2}", Texts.Get("s_files"), extension.ToUpper(), extension.ToLower());
        }
    }

    public class FileNameEditor : FileNameEditorBase
    {
        protected override void GetDialogProps(ITypeDescriptorContext context, out FileDialogType dialogType, out string filter)
        {
            dialogType = FileDialogType.Open;
            filter = String.Format("*.*|*.*");
            foreach (Attribute attr in context.PropertyDescriptor.Attributes)
            {
                if (attr is FileNameEditorPropsAttribute)
                {
                    var a = (FileNameEditorPropsAttribute)attr;
                    dialogType = a.DialogType;
                    filter = a.Filter;
                }
            }
        }
    }

    public class FolderNameEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                if (!context.PropertyDescriptor.IsReadOnly)
                {
                    return UITypeEditorEditStyle.Modal;
                }
            }
            return UITypeEditorEditStyle.None;
        }

        [RefreshProperties(RefreshProperties.All)]
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            if (context == null || provider == null || context.Instance == null)
            {
                return base.EditValue(provider, value);
            }
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = (string)value;
            if (dlg.ShowDialogEx() == DialogResult.OK)
            {
                return dlg.SelectedPath;
            }
            else
            {
                return value;
            }
        }
    }
}
