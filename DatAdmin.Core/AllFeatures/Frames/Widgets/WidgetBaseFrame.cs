using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class WidgetBaseFrame : ConnectionContentFrame
    {
        protected AppObject m_appobj;
        protected IWidget m_widget;
        bool m_isLoading;

        public WidgetBaseFrame(IWidget widget)
        {
            InitializeComponent();
            m_widget = widget;
        }

        public WidgetBaseFrame()
        {
            InitializeComponent();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            CallLoad(m_appobj);
        }

        protected virtual bool? ControlVisibility(Control ctrl)
        {
            return null;
        }

        protected virtual void CallLoad(AppObject appobj)
        {
            if (m_isLoading) return;
            m_appobj = appobj;
            m_conn = null;
            Controls.ShowError(false, null, ControlVisibility);
            if (m_appobj == null || m_appobj.DisableAutoConnect)
            {
                DoLoadData();
                ShowDataInGui();
                return;
            }
            CurrentConnFactory = m_appobj.GetConnection();
            Controls.ShowProgress(true, null, ControlVisibility);
            CallOpen();
        }

        protected override void OnOpenedConnection()
        {
            LoadFromObject(m_appobj);
            if (m_conn != null && m_conn.IsOpened)
            {
                ProcessRegister.AddBackgroundTask("s_loading_data");
                m_conn.BeginInvoke((Action)DoLoadData, Async.CreateInvokeCallback(m_invoker, LoadedData));
            }
            else
            {
                try
                {
                    Controls.ShowProgress(false, null, ControlVisibility);
                    DoLoadData();
                    ShowDataInGui();
                }
                catch (Exception e)
                {
                    HandleError(e);
                }
            }
        }

        protected virtual void LoadFromObject(AppObject appobj)
        {
        }

        protected virtual void ShowDataInGui()
        {
        }

        protected virtual void DoResetData()
        {
        }

        protected virtual void DoLoadData()
        {
        }

        void LoadedData(IAsyncResult async)
        {
            try
            {
                async.StandaloneEndInvoke();
                Controls.ShowError(false, null, ControlVisibility);
            }
            catch (Exception e)
            {
                HandleError(e);
            }
            Controls.ShowProgress(false, null, ControlVisibility);
            ProcessRegister.RemoveBackgroundTask("s_loading_data");
            ShowDataInGui();
            m_isLoading = false;
        }

        protected void HandleError(Exception e)
        {
            Errors.LogError(e);
            Controls.ShowError(true, Errors.ExtractMessage(e), ControlVisibility);
        }

        public void LoadWidgetData(AppObject ao)
        {
            m_appobj = ao;
            if (IsHandleCreated) CallLoad(m_appobj);
        }

        public void SetWidget(IWidget widget)
        {
            m_widget = widget;
        }

        public override Bitmap Image
        {
            get { return m_widget.Image; }
        }

        public override string PageTitle
        {
            get { return m_widget.PageTitle; }
        }

        public IWidget Widget
        {
            get { return m_widget; }
        }

        public virtual void OnBeginDesign()
        {
        }

        public virtual void OnFinishDesign()
        {
        }

        public virtual void BeforeSave()
        {
        }

        public virtual void OnChangedDesigning()
        {
        }
    }
}
