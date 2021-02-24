using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DatAdmin
{
    public enum DockerState
    {
        Unknown = 0,
        Float = 1,
        DockTopAutoHide = 2,
        DockLeftAutoHide = 3,
        DockBottomAutoHide = 4,
        DockRightAutoHide = 5,
        Document = 6,
        DockTop = 7,
        DockLeft = 8,
        DockBottom = 9,
        DockRight = 10,
        Hidden = 11
    }

    public enum DocumentDockPosition
    {
        Center,
        Top,
        Left,
        Right,
        Bottom,
    }

    public interface IDocker
    {
        IDockerFactory Factory { get; }
        Control DockerControl { get; }
        void DockerVisibleChanged(bool visible);
        void OnClose();
        bool AllowClose();
    }

    public interface IDockerFactoryCommon
    {
        string MenuTitle { get; }
        Bitmap Icon { get; }
        string GetPersistString();
        DockerState InitialState { get; }
        Keys Shortcut { get; }
    }

    public interface IDockerFactory : IDockerFactoryCommon, IAddonInstance
    {
        IDocker CreateDocker();
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class DockerFactoryAttribute : RegisterAttribute
    {
    }

    [AddonType]
    public class DockerFactoryAddonType : AddonType
    {
        public override Type InterfaceType
        {
            get { return typeof(IDockerFactory); }
        }

        public override string Name
        {
            get { return "dockerfactory"; }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(DockerFactoryAttribute); }
        }

        public static readonly DockerFactoryAddonType Instance = new DockerFactoryAddonType();
    }

    public interface IDockWrapper
    {
        bool AllowClose();
        void OnCloseWindow();
        void UpdateTitle();
        void UpdateIcon();
        void ReplaceContent(ContentFrame newframe);
        //event EventHandler ClosedEvent;
    }
}


