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
    public partial class SplashForm : Form
    {
        internal static SplashForm Instance;
        static Thread m_thread;
        int m_acty = 0, m_actx = 0;

        //class LoadedItem
        //{
        //    internal string Text;
        //    internal Bitmap Image;
        //}
        //List<LoadedItem> m_items = new List<LoadedItem>();

        public SplashForm()
        {
            InitializeComponent();
            Instance = this;
        }

        public void SetPosition(int value)
        {
            while (!IsHandleCreated) Thread.Sleep(100);
            Invoke((Action)(() =>
            {
                progressBar1.Value = value;
                //Application.DoEvents();
            }));
        }

        public void SetCurWork(string value)
        {
            while (!IsHandleCreated) Thread.Sleep(100);
            Invoke((Action)(() =>
            {
                labCurWork.Text = value;
                //Application.DoEvents();
            }));
        }

        public static void LoadPluginCallback(string plugin, int index, int count)
        {
            if (Instance == null) return;
            while (!Instance.IsHandleCreated) Thread.Sleep(100);
            Instance.Invoke((Action)(() =>
            {
                Instance.labCurWork.Text = Texts.Get("s_loading$plugin", "plugin", plugin);
                Instance.progressBar1.Value = (int)(20 + (index * 60.0 / count));
                //Application.DoEvents();
            }));
        }

        static void RunSplash()
        {
            // show splshscreen at first
            SplashForm splash = new SplashForm();
            Application.Run(splash);
        }

        public static SplashForm Start()
        {
            m_thread = new Thread(RunSplash);
            m_thread.IsBackground = true;
            m_thread.TrySetApartmentState(ApartmentState.STA);
            m_thread.Start();
            while (Instance == null)
            {
                Thread.Sleep(50);
            }
            return Instance;
        }

        public static void EnsureNoSplash()
        {
            if (SplashForm.Instance != null)
            {
                while (!Instance.IsHandleCreated) Thread.Sleep(100);
                SplashForm.Instance.Invoke((Action)SplashForm.Instance.Close);
                SplashForm.Instance = null;
            }
        }

        public static void SetProgress(string msg, int progress)
        {
            if (Instance != null)
            {
                Instance.SetCurWork(msg);
                Instance.SetPosition(progress);
            }
        }

        private void DoAddModuleInfo(Bitmap image, string info)
        {
            const int IMGW = 16, IMGH = 16, LABX = 21, LINEHI = 21, COLWI = 130;
            var pic = new PictureBox { Image = image, Top = m_acty, Left = m_actx, BackColor = Color.White, Width = IMGW, Height = IMGH };
            var lab = new Label { Text = info, Top = m_acty, Left = m_actx + LABX, ForeColor = Color.FromArgb(224, 224, 244) };
            panel1.Controls.Add(pic);
            panel1.Controls.Add(lab);
            m_acty += LINEHI;
            if (m_acty + LINEHI > panel1.Height)
            {
                m_acty = 0;
                m_actx += COLWI;
            }
        }

        public static void AddModuleInfo(Bitmap image, string info)
        {
            if (Instance != null)
            {
                Instance.Invoke((Action<Bitmap, string>)Instance.DoAddModuleInfo, image, info);
            }
        }
    }
}
