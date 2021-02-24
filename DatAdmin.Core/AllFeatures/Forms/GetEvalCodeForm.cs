using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class GetEvalCodeForm : FormEx
    {
        public GetEvalCodeForm()
        {
            InitializeComponent();
            tbxName.Text = LicenseTool.RegisteredToUser1() ?? "";
            tbxEmail.Text = LicenseTool.RegEmail1() ?? "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (tbxName.Text.IsEmpty())
            {
                StdDialog.ShowError("s_please_fill_name");
                tbxName.Focus();
                return;
            }
            if (!IncorrectEmailError.IsValid(tbxEmail.Text))
            {
                StdDialog.ShowError(Texts.Get("s_incorrect$email", "email", tbxEmail.Text));
                tbxEmail.Focus();
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        public class Result
        {
            public string Name;
            public string Email;
        }
        public static Result Run()
        {
            var win = new GetEvalCodeForm();
            if (win.ShowDialogEx() == DialogResult.OK)
            {
                return new Result
                {
                    Name = win.tbxName.Text,
                    Email = win.tbxEmail.Text,
                };
            }
            return null;
        }
    }
}
