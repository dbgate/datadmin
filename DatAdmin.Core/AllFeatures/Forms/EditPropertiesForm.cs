using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class EditPropertiesForm : FormEx
    {
        public EditPropertiesForm()
        {
            InitializeComponent();
        }

        public static bool Run(object o, bool allowCancel)
        {
            EditPropertiesForm win = new EditPropertiesForm();
            win.btnCancel.Visible = allowCancel;
            win.ctlProperties.SelectedObject = o;
            return win.ShowDialogEx() == DialogResult.OK;
        }
   }
}