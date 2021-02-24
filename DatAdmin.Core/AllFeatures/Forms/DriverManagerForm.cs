using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class DriverManagerForm : FormEx
    {
        public DriverManagerForm()
        {
            InitializeComponent();
        }

        public static void Run()
        {
            DriverManagerForm win = new DriverManagerForm();
            if (win.ShowDialogEx() == DialogResult.OK)
            {
                DbDriverManager.Instance.SaveCustom();
            }
            else
            {
                DbDriverManager.Instance.ReloadCustom();
            }
        }

        //public override string UsageEventName
        //{
        //    get { return "driver_manager"; }
        //}
    }
}