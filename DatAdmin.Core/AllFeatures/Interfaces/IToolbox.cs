using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DatAdmin
{
    public class ToolboxItemCollection : List<IToolboxItem>
    {
    }

    public interface IToolboxGenerator
    {
        ToolboxItemCollection GetToolbox();
    }

    public interface IToolboxItem
    {
        string Category { get; }
        string DisplayName { get; }
        string Description { get; }
        Bitmap Image { get; }
        Type ItemType { get; }
    }
}
