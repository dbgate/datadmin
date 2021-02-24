using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class DataDocker : DockerBase
    {
        IDataHolder m_data;
        object m_sender;
        bool m_visible;
        CellDataFrameBase m_frame;

        public DataDocker(CellDataDockerFactory factory)
            : base(factory)
        {
            InitializeComponent();
            m_frame = factory.Editor.GetControl();
            Controls.Add(m_frame);
            m_frame.Dock = DockStyle.Fill;
            HCellData.ShowData += HCellData_ShowData;
            HCellData.InvalidateData += HCellData_InvalidateData;

            HCellData_ShowData(DataDockerStaticData.Sender, DataDockerStaticData.Data);
        }

        void HCellData_InvalidateData(object sender)
        {
            if (m_sender == sender)
            {
                m_frame.Data = null;
                m_sender = null;
            }
        }

        public override void DockerVisibleChanged(bool visible)
        {
            base.DockerVisibleChanged(visible);
            m_visible = visible;
            if (visible) m_frame.Data = m_data;
        }

        void HCellData_ShowData(object sender, IDataHolder data)
        {
            m_data = data;
            m_sender = sender;
            if (m_visible) m_frame.Data = data;
        }

        public override void OnClose()
        {
            base.OnClose();
            HCellData.ShowData -= HCellData_ShowData;
        }
    }

    [PluginHandler]
    public class DataDockerStaticData : PluginHandlerBase
    {
        public static object Sender;
        public static IDataHolder Data;

        public override void OnLoadedAllPlugins()
        {
            HCellData.ShowData += new Action<object, IDataHolder>(HCellData_ShowData);
            HCellData.InvalidateData += new Action<object>(HCellData_InvalidateData);
        }

        void HCellData_InvalidateData(object sender)
        {
            if (Sender == sender)
            {
                Data = null;
                Sender = null;
            }
        }

        void HCellData_ShowData(object sender, IDataHolder data)
        {
            Sender = sender;
            Data = data;
        }
    }

    public class CellDataDockerFactory : DockerFactoryBase
    {
        ICellDataEditor m_editor;

        public CellDataDockerFactory(ICellDataEditor editor)
        {
            m_editor = editor;
        }

        public override Bitmap Icon
        {
            get { return m_editor.Icon; }
        }

        public override DockerState InitialState
        {
            get { return m_editor.InitialState; }
        }

        public override string MenuTitle
        {
            get { return m_editor.MenuTitle; }
        }

        public override Keys Shortcut
        {
            get { return m_editor.Shortcut; }
        }

        public override IDocker CreateDocker()
        {
            return new DataDocker(this);
        }

        public override string GetPersistString()
        {
            return "data_editor-" + m_editor.GetPersistString();
        }

        public ICellDataEditor Editor { get { return m_editor; } }
    }

    //[DockerFactory(Title = "Data window", Name = "data_window")]
    //public class DataDockerFactory : DockerFactoryBase
    //{
    //    public override IDocker CreateDocker()
    //    {
    //        return new DataDocker(this);
    //    }

    //    public override string MenuTitle
    //    {
    //        get { return "s_cell_data"; }
    //    }

    //    public override Image Icon
    //    {
    //        get { return CoreIcons.data; }
    //    }

    //    public override DockerState InitialState
    //    {
    //        get { return DockerState.DockBottom; }
    //    }

    //    public override Keys Shortcut
    //    {
    //        get { return Keys.Control | Keys.Alt | Keys.C; }
    //    }
    //}
}
