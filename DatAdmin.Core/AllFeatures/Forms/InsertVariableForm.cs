using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class InsertVariableForm : FormEx
    {
        public InsertVariableForm()
        {
            InitializeComponent();
        }

        public static string RunQb()
        {
            var win = new InsertVariableForm();
            win.tbxName.Text = "VARIABLE";
            if (win.ShowDialogEx() == DialogResult.OK)
            {
                if (win.tbxDefaultValue.Text.IsEmpty())
                {
                    return "@" + win.tbxName.Text;
                }
                else
                {
                    return "@" + win.tbxName.Text + ":" + win.tbxDefaultValue.Text;
                }
            }
            return null;
        }
    }
}
