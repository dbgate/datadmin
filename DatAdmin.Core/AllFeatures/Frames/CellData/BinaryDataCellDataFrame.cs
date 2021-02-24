using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class BinaryDataCellDataFrame : CellDataFrameBase
    {
        public BinaryDataCellDataFrame()
        {
            InitializeComponent();
        }

        public override void ShowCurrentData()
        {
            byte[] d = null;
            switch (m_holder.GetFieldType())
            {
                case TypeStorage.String:
                    d = Encoding.UTF8.GetBytes(m_holder.GetString());
                    break;
                case TypeStorage.ByteArray:
                    d = m_holder.GetByteArray();
                    break;
            }
            if (d != null)
            {
                textBox1.Text = StringTool.EncodeHexNice(d, 32).Replace("\n", "\r\n");
            }
            else
            {
                Controls.ShowError(true);
            }
        }
    }

    [CellDataEditor(Name = "binary", Title = "Binary")]
    public class BinaryCellDataEditor : CellDataEditorBase
    {
        public override string MenuTitle
        {
            get { return "s_binary_data"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.hex; }
        }

        protected override CellDataFrameBase CreateControl()
        {
            return new BinaryDataCellDataFrame();
        }

        public override int SupportLevel(IDataHolder data, IBedValueReader holder)
        {
            switch (holder.GetFieldType())
            {
                case TypeStorage.String:
                    return 1;
                case TypeStorage.ByteArray:
                    return 5;
            }
            return 0;
        }
    }
}
