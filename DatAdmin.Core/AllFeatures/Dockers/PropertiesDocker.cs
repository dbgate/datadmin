using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class PropertiesDocker : DockerBase
    {
        public PropertiesDocker(IDockerFactory factory)
            : base(factory)
        {
            InitializeComponent();

            HDesigner.ShowProperties += HDesigner_ShowProperties;
            HDesigner.HideProperties += HDesigner_HideProperties;
            propertyFrame1.SelectedObject = PropertiesDockerStaticData.Data;
        }

        public override void OnClose()
        {
            base.OnClose();
            HDesigner.ShowProperties -= HDesigner_ShowProperties;
            HDesigner.HideProperties -= HDesigner_HideProperties;
        }

        void HDesigner_ShowProperties(object obj)
        {
            propertyFrame1.SelectedObject = obj;
        }

        void HDesigner_HideProperties(object obj)
        {
            if (propertyFrame1.SelectedObject == obj) propertyFrame1.SelectedObject = null;
        }
    }

    [PluginHandler]
    public class PropertiesDockerStaticData : PluginHandlerBase
    {
        public static object Data;

        public override void OnLoadedAllPlugins()
        {
            HDesigner.ShowProperties += HDesigner_ShowProperties;
            HDesigner.HideProperties += HDesigner_HideProperties;
        }

        void HDesigner_HideProperties(object obj)
        {
            if (Data == obj) Data = null;
        }

        void HDesigner_ShowProperties(object obj)
        {
            Data = obj;
        }
    }

    [DockerFactory(Title = "Properties", Name = "properties", RequiredFeature = CustomDashboardsFeature.Test)]
    public class PropertiesDockerFactory : DockerFactoryBase
    {
        public override IDocker CreateDocker()
        {
            return new PropertiesDocker(this);
        }

        public override string MenuTitle
        {
            get { return "s_properties"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.properties; }
        }

        public override DockerState InitialState
        {
            get { return DockerState.DockRight; }
        }

        public override Keys Shortcut
        {
            get { return Keys.F11; }
        }
    }
}
