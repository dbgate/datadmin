using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.diagrams
{
    public partial class BackgroundColorFrame : UserControl
    {
        public BackgroundColorFrame()
        {
            InitializeComponent();
        }

        private void labStart_Click(object sender, EventArgs e)
        {
            var lab = (Label)sender;
            colorDialog1.Color = lab.BackColor;
            if (colorDialog1.ShowDialogEx() == DialogResult.OK)
            {
                lab.BackColor = colorDialog1.Color;
                CallChanged();
            }
        }

        public GradientDef Gradient
        {
            get
            {
                var res = new GradientDef();
                res.BgColor = labStart.BackColor;
                res.GradientColor = labEnd.BackColor;
                res.IsGradient = chbGradient.Checked;
                res.Angle = trackBarAngle.Value * 10;
                return res;
            }
            set
            {
                labStart.BackColor = value.BgColor;
                labEnd.BackColor = value.GradientColor;
                chbGradient.Checked = value.IsGradient;
                trackBarAngle.Value = (int)Math.Round(value.Angle / 10);
            }
        }

        private void chbGradient_CheckedChanged(object sender, EventArgs e)
        {
            trackBarAngle.Enabled = chbGradient.Checked;
            CallChanged();
        }

        private void CallChanged()
        {
            if (Changed != null) Changed(this, EventArgs.Empty);
        }

        public event EventHandler Changed;

        private void trackBarAngle_ValueChanged(object sender, EventArgs e)
        {
            CallChanged();
        }

        private void btnExchange_Click(object sender, EventArgs e)
        {
            var tmp = labStart.BackColor;
            labStart.BackColor = labEnd.BackColor;
            labEnd.BackColor = tmp;
            CallChanged();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            labEnd.BackColor = labStart.BackColor;
            CallChanged();
        }
    }
}
