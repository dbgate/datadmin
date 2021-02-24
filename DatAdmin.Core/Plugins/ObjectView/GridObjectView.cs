using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.Common;
using System.IO;
using System.ComponentModel;

namespace DatAdmin
{
    public interface IGridWidget : IWidget
    {
        void GetData(AppObject appobj, ConnectionPack connpack, out DataTable data, out DatAdmin.Scripting.ObjectGrid grid);
        int MaximumRecords { get;}
    }

    public abstract class GridWidgetBase : WidgetBase, IGridWidget
    {
        int m_rowLimit;

        [Browsable(false)]
        public virtual int MaximumRecords
        {
            get
            {
                if (RowLimit > 0) return RowLimit;
                return GlobalSettings.Pages.TableData().ObjectViewRowLimit;
            }
        }

        [XmlElem]
        public int RowLimit
        {
            get { return m_rowLimit; }
            set
            {
                if (value == m_rowLimit) return;
                m_rowLimit = value;
                HDesigner.CallChangedWidget(this, HDesigner.WidgetPart.Data);
            }
        }

        protected override WidgetBaseFrame CreateControl()
        {
            return new GridWidgetFrame(this);
        }

        public override Type GetControlType()
        {
            return typeof(GridWidgetFrame);
        }

        public abstract void GetData(AppObject appobj, ConnectionPack connpack, out DataTable data, out DatAdmin.Scripting.ObjectGrid grid);

    }

}
