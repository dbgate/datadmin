using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class InfoBoxFrame : UserControl
    {
        public InfoBoxFrame()
        {
            InitializeComponent();
        }

        public string InfoText
        {
            get { return tbxInfo.Text; }
            set { tbxInfo.Text = Texts.Get(value); }
        }
    }
}
