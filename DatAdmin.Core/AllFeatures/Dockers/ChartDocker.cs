using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DatAdmin
{
    [DockerFactory(Title = "Charts tree", Name = "charts_tree", RequiredFeature = ChartsFeature.Test)]
    public class ChartsTreeDockerFactory : DockerFactoryBase
    {
        public override IDocker CreateDocker()
        {
            return new TreeDocker("charts:", this, false, false);
        }

        public override string MenuTitle
        {
            get { return "s_charts"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.chart; }
        }

        public override DockerState InitialState
        {
            get { return DockerState.DockLeft; }
        }

        public override Keys Shortcut
        {
            get { return Keys.Control | Keys.Alt | Keys.C; }
        }
    }
}
