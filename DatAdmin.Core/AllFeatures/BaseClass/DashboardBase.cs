using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace DatAdmin
{
    public class DashboardInstanceParams
    {
        public string LayoutName;
    }

    public abstract class DashboardBase : AddonBase, IDashboard
    {
        //protected AppObject m_selectedObject;

        public override AddonType AddonType
        {
            get { return DashboardAddonType.Instance; }
        }

        #region IDashboard Members

        [Browsable(false)]
        public virtual int Priority { get { return 0; } set { } }

        public abstract bool SuitableFor(AppObject appobj);

        //public AppObject SelectedObject
        //{
        //    get { return m_selectedObject; }
        //    set { SetSelectedObject(value); }
        //}

        //protected abstract void SetSelectedObject(AppObject obj);

        public abstract Control CreateControl(DashboardInstanceParams pars);

        #endregion

        public virtual DashboardCaps Caps
        {
            get
            {
                return new DashboardCaps
                {
                    ShowNodeToolbar = true
                };
            }
        }

        public virtual void RefreshDashboard() { }
    }
}
