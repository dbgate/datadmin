using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using IP.Components;
using System.Drawing.Design;

namespace DatAdmin
{
    public partial class ToolboxDocker : DockerBase
    {
        public ToolboxDocker(IDockerFactory factory)
            : base(factory)
        {
            InitializeComponent();

            HDesigner.ShowInToolbox += HToolbox_ShowInToolbox;
            LoadFromGenerator(ToolboxStaticData.Data);

            hostToolbox1.TabMenu = null;
            hostToolbox1.ItemMenu = null;
            hostToolbox1.AllowDrop = false;
        }

        private void LoadFromGenerator(IToolboxGenerator gen)
        {
            hostToolbox1.Categories.Clear();
            hostToolbox1.Items.Clear();
            if (gen == null) return;
            var col = gen.GetToolbox();
            foreach (var item in col)
            {
                AddItem(item);
            }
        }

        private void AddItem(IToolboxItem src)
        {
            string tabName = Texts.Get(src.Category);
            var item = new ToolboxItem(src.ItemType);
            item.Bitmap = src.Image;
            item.DisplayName = Texts.Get(src.DisplayName);
            item.Description = Texts.Get(src.Description);
            Toolbox.Tab tab = hostToolbox1.Categories[tabName];
            if (tab == null)
            {
                tab = new Toolbox.Tab(tabName);
                tab.Opened = true;
                hostToolbox1.Categories.Add(tab);
            }
            var hostitem = new HostToolbox.HostItem(item);
            hostitem.Tag = src;
            tab.Items.Add(hostitem);
        }

        public override void OnClose()
        {
            base.OnClose();
            HDesigner.ShowInToolbox -= HToolbox_ShowInToolbox;
        }

        void HToolbox_ShowInToolbox(IToolboxGenerator obj)
        {
            LoadFromGenerator(obj);
        }

        private void hostToolbox1_DoubleClick(object sender, EventArgs e)
        {
            if (hostToolbox1.SelectedItem != null)
            {
                var item = (IToolboxItem)hostToolbox1.SelectedItem.Tag;
                HDesigner.CallUseToolBoxItem(item);
            }
        }
    }

    [PluginHandler]
    public class ToolboxStaticData : PluginHandlerBase
    {
        public static IToolboxGenerator Data;

        public override void OnLoadedAllPlugins()
        {
            HDesigner.ShowInToolbox += HToolbox_ShowInToolbox;
        }

        void HToolbox_ShowInToolbox(IToolboxGenerator obj)
        {
            Data = obj;
        }
    }

    [DockerFactory(Title = "Toolbox", Name = "toolbox", RequiredFeature = CustomDashboardsFeature.Test)]
    public class ToolboxDockerFactory : DockerFactoryBase
    {
        public override IDocker CreateDocker()
        {
            return new ToolboxDocker(this);
        }

        public override string MenuTitle
        {
            get { return "s_toolbox"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.toolbox; }
        }

        public override DockerState InitialState
        {
            get { return DockerState.DockRight; }
        }

        public override Keys Shortcut
        {
            get { return Keys.Control | Keys.Alt | Keys.T; }
        }
    }
}
