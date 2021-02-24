using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class TunnelConfigFrame : UserControl
    {
        public TunnelConfigFrame()
        {
            InitializeComponent();
        }

        private void rbtUseTunnel_CheckedChanged(object sender, EventArgs e)
        {
            UpdateEnabling();
            if (SelectedDriverChanged != null) SelectedDriverChanged(this, e);
        }

        private void UpdateEnabling()
        {
            addonSelectFrame1.Enabled = rbtUseTunnel.Checked;
        }

        private void addonSelectFrame1_ChangedSelectedObject(object sender, EventArgs e)
        {
            if (SelectedDriverChanged != null) SelectedDriverChanged(this, e);
        }

        public event EventHandler SelectedDriverChanged;

        public ITunnelDriver SelectedDriver
        {
            get
            {
                if (rbtUseTunnel.Checked) return addonSelectFrame1.SelectedObject as ITunnelDriver;
                return null;
            }
            set
            {
                if (value == null)
                {
                    rbtDirectConnection.Checked = true;
                }
                else
                {
                    rbtUseTunnel.Checked = true;
                    addonSelectFrame1.SelectObject(value);
                }
                UpdateEnabling();
            }
        }

        public bool AllowDirectConnection
        {
            get { return rbtDirectConnection.Enabled; }
            set { rbtDirectConnection.Enabled = value; }
        }
    }
}
