using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class EmailToFrame : UserControl
    {
        public EmailToFrame()
        {
            InitializeComponent();
        }

        public string EmailTo
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public event EventHandler EmailToChanged;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (EmailToChanged != null) EmailToChanged(this, e);
        }
    }
}
