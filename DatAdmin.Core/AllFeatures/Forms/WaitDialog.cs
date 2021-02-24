using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DatAdmin
{
    public partial class WaitDialog : FormEx
    {
        bool m_canceled;

        public WaitDialog()
        {
            InitializeComponent();
            this.Text = VersionInfo.ProgramTitle;
        }

        public bool Canceled
        {
            get { return m_canceled; }
        }

        public bool Cancelable
        {
            get { return button1.Visible; }
            set { button1.Visible = value; }
        }

        public string WaitText
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_canceled = true;
        }
    }
}
