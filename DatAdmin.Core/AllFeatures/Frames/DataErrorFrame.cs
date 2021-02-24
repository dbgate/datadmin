using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace DatAdmin
{
    public partial class DataErrorFrame : UserControl
    {
        public DataErrorFrame()
        {
            InitializeComponent();
            Translating.TranslateControl(this);
        }
    }

    public static partial class ControlCollectionExtension
    {
        public static void SetEnabled(this System.Windows.Forms.Control.ControlCollection ctrls, bool value)
        {
            foreach (Control ctrl in ctrls)
            {
                ctrl.Enabled = value;
            }
        }

        public static void ShowError(this System.Windows.Forms.Control.ControlCollection ctrls, bool iserror)
        {
            ctrls.ShowError(iserror, null, null);
        }

        public static void ShowError(this System.Windows.Forms.Control.ControlCollection ctrls, bool iserror, string message)
        {
            ctrls.ShowError(iserror, message, null);
        }

        public static void ShowError(this System.Windows.Forms.Control.ControlCollection ctrls, bool iserror, string message, Func<Control, bool?> forceVisibility)
        {
            if (Core.IsMono) return; // unsafe under linux
            DataErrorFrame ef = null;
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is DataErrorFrame) ef = (DataErrorFrame)ctrl;
            }
            if (iserror)
            {
                foreach (Control ctrl in ctrls)
                {
                    if (ctrl is ToolStrip) continue;
                    bool? vis = null;
                    if (forceVisibility != null) vis = forceVisibility(ctrl);
                    ctrl.Visible = vis ?? false;
                }

                if (ef == null)
                {
                    DataErrorFrame frm = new DataErrorFrame();
                    ctrls.Add(frm);
                    frm.BringToFront();
                    frm.Dock = DockStyle.Fill;
                    frm.labErrorDetail.Text = Texts.Get(message ?? "");
                }
                else
                {
                    ef.Visible = true;
                    ef.labErrorDetail.Text = Texts.Get(message ?? "");
                }
            }
            else
            {
                if (ef != null)
                {
                    ctrls.Remove(ef);
                    ef.Dispose();
                }

                foreach (Control ctrl in ctrls)
                {
                    if (ctrl is ToolStrip) continue;
                    bool? vis = null;
                    if (forceVisibility != null) vis = forceVisibility(ctrl);
                    ctrl.Visible = vis ?? true;
                }
            }
        }
    }
}
