using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DatAdmin
{
    public class DataGridStyleAttribute : RegisterAttribute { }

    public enum DataGridCellType { Regular, AddedRow, RemovedRow, ModifiedCell, ModifiedRow, Highlight, Selected }
    public enum DataGridHeaderCellType { Regular, Current, Highlight }

    public interface IDataGridStyle : IAddonInstance
    {
        Color LookupHintColor { get; }
        Color GridColor { get; }
        void PaintRowNumberBackground(DataGridViewCellPaintingEventArgs e, DataGridHeaderCellType ctype);
        void PaintColumnHeaderBackground(DataGridViewCellPaintingEventArgs e, DataGridHeaderCellType ctype);
        void PaintCellBackground(DataGridViewCellPaintingEventArgs e, DataGridCellType ctype);
    }

    [AddonType]
    public class DataGridStyleAddonType : AddonType
    {
        public override string Name
        {
            get { return "datagridstyle"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IDataGridStyle); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(DataGridStyleAttribute); }
        }

        public static readonly DataGridStyleAddonType Instance = new DataGridStyleAddonType();
    }
}
