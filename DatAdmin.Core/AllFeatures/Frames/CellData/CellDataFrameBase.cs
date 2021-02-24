using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class CellDataFrameBase : UserControl
    {
        protected IDataHolder m_data;
        protected BedValueHolder m_holder = new BedValueHolder();
        protected TypeStorage m_targetType;
        //protected object m_sender;
        protected bool m_loadingData;

        public CellDataFrameBase()
        {
            InitializeComponent();
            //HCellData.ShowData += HCellData_ShowData;
            //HCellData.InvalidateData += HCellData_InvalidateData;
            //Disposed += new EventHandler(CellDataFrameBase_Disposed);

            //HCellData_ShowData(DataDockerStaticData.Sender, DataDockerStaticData.Data);
        }

        //void CellDataFrameBase_Disposed(object sender, EventArgs e)
        //{
        //    HCellData.ShowData -= HCellData_ShowData;
        //    HCellData.InvalidateData -= HCellData_InvalidateData;
        //}

        //void HCellData_InvalidateData(object sender)
        //{
        //    if (m_sender == sender)
        //    {
        //        m_data = null;
        //        m_sender = null;
        //        CallShowCurrentData();
        //    }
        //}

        //void HCellData_ShowData(object sender, IDataHolder data)
        //{
        //    m_data = data;
        //    m_sender = sender;
        //    if (m_data != null)
        //    {
        //        m_targetType = m_data.GetTargetType();
        //        m_data.GetData(m_holder);
        //    }
        //    CallShowCurrentData();
        //}

        public IDataHolder Data
        {
            get { return m_data; }
            set
            {
                m_data = value;
                CallShowCurrentData();
            }
        }

        public void CallShowCurrentData()
        {
            try
            {
                m_loadingData = true;
                if (m_data != null)
                {
                    Controls.ShowError(false);
                    m_data.GetData(m_holder);
                    m_targetType = m_data.GetTargetType();
                    ShowCurrentData();
                }
                else
                {
                    Controls.ShowError(true);
                    ClearCurrentData();
                }
            }
            finally
            {
                m_loadingData = false;
            }
        }

        protected bool IsReadOnly
        {
            get
            {
                if (m_data != null) return m_data.IsReadOnly;
                return true;
            }
        }

        public virtual void ClearCurrentData()
        {
        }

        public virtual void ShowCurrentData()
        {
        }

        protected void DispatchDataChanged()
        {
            m_data.SetData(m_holder);
        }

        protected string GetStringValue()
        {
            return GetStringValue(m_holder);
        }

        public static string GetStringValue(IBedValueReader holder)
        {
            var type = holder.GetFieldType();
            if (type == TypeStorage.String) return holder.GetString();
            if (type == TypeStorage.ByteArray)
            {
                try
                {
                    return Encoding.UTF8.GetString(holder.GetByteArray());
                }
                catch { }
            }
            return "";
        }
    }
}
