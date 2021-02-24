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
    public partial class ProgressFrame : UserControl
    {
        public ProgressFrame()
        {
            InitializeComponent();
        }
    }

    public class ControlsProgressWrapper
    {
        public Control.ControlCollection Collection;
        public bool ProgressVisible;
        Timer m_timer;
        Func<Control, bool?> m_forceVisibility;

        public void ShowProgress(bool isLoading, string message, Func<Control, bool?> forceVisibility)
        {
            m_forceVisibility = forceVisibility;
            lock (this)
            {
                if (isLoading)
                {
                    if (!ProgressVisible)
                    {
                        // wait 300 milliseconds before real show progress
                        m_timer = new Timer();
                        m_timer.Interval = 300;
                        m_timer.Enabled = true;
                        m_timer.Tick += new EventHandler(tim_Tick);
                    }
                }
                else
                {
                    if (m_timer != null)
                    {
                        m_timer.Dispose();
                        m_timer = null;
                    }

                    if (ProgressVisible)
                    {
                        DoHideProgress();
                    }
                }
            }
        }

        private void DoHideProgress()
        {
            ProgressFrame ef = FindFrame();

            if (ef != null)
            {
                Collection.Remove(ef);
                ef.Dispose();
            }

            foreach (Control ctrl in Collection)
            {
                if (ctrl is ToolStrip) continue;
                bool? vis = null;
                if (m_forceVisibility != null) vis = m_forceVisibility(ctrl);
                ctrl.Visible = vis ?? true;
            }

            ProgressVisible = false;
        }

        void tim_Tick(object sender, EventArgs e)
        {
            lock (this)
            {
                if (m_timer == null) return; // timer was destroyed - do not show wait
                if (m_timer != null)
                {
                    m_timer.Dispose();
                    m_timer = null;
                }
                if (!ProgressVisible) DoShowProgress();
            }
        }

        private ProgressFrame FindFrame()
        {
            foreach (Control ctrl in Collection)
            {
                if (ctrl is ProgressFrame) return (ProgressFrame)ctrl;
            }
            return null;
        }

        private void DoShowProgress()
        {
            ProgressFrame ef = FindFrame();

            foreach (Control ctrl in Collection)
            {
                if (ctrl is ToolStrip) continue;
                bool? vis = null;
                if (m_forceVisibility != null) vis = m_forceVisibility(ctrl);
                ctrl.Visible = vis ?? false;
            }

            if (ef == null)
            {
                ProgressFrame frm = new ProgressFrame();
                Collection.Add(frm);
                try
                {
                    frm.BringToFront();
                }
                catch (Exception err)
                {
                    Logging.Warning("DAE-00076 " + err.Message);
                    //throw new InternalError("[E :00076] " + err.Message, err);
                }
                frm.Dock = DockStyle.Fill;
                //frm.labErrorDetail.Text = Texts.Get(message ?? "");
            }
            else
            {
                ef.Visible = true;
                //ef.labErrorDetail.Text = Texts.Get(message ?? "");
            }

            ProgressVisible = true;
        }
    }

    public static partial class ControlCollectionExtension
    {
        static Dictionary<Control.ControlCollection, ControlsProgressWrapper> m_wraps = new Dictionary<Control.ControlCollection, ControlsProgressWrapper>();

        private static ControlsProgressWrapper GetWrapper(Control.ControlCollection ctrls)
        {
            lock (m_wraps)
            {
                var res = m_wraps.Get(ctrls, null);
                if (res == null)
                {
                    res = new ControlsProgressWrapper { Collection = ctrls };
                    m_wraps[ctrls] = res;
                }
                return res;
            }
        }

        public static void ShowProgress(this Control.ControlCollection ctrls, bool isloading)
        {
            ctrls.ShowProgress(isloading, null, null);
        }

        public static void ShowProgress(this Control.ControlCollection ctrls, bool isloading, string message, Func<Control, bool?> forceVisibility)
        {
            if (Core.IsMono) return; // unsafe under linux

            var wrap = GetWrapper(ctrls);
            wrap.ShowProgress(isloading, message, forceVisibility);
        }
    }
}
