using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class TextCellDataFrame : CellDataFrameBase
    {
        public TextCellDataFrame()
        {
            InitializeComponent();
        }

        public override void ShowCurrentData()
        {
            var type = m_holder.GetFieldType();
            if (type == TypeStorage.ByteArray)
            {
                try
                {
                    textBox1.Text = Encoding.UTF8.GetString(m_holder.GetByteArray()).Replace("\r", "").Replace("\n", "\r\n");
                    textBox1.ReadOnly = IsReadOnly;
                }
                catch
                {
                    Controls.ShowError(true);
                }
            }
            else
            {
                m_data.BedConvertor.Formatter.ReadFrom(m_holder);
                textBox1.Text = m_data.BedConvertor.Formatter.GetText().Replace("\r", "").Replace("\n", "\r\n");
                textBox1.ReadOnly = IsReadOnly;
            }
        }

        public override void ClearCurrentData()
        {
            textBox1.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (m_loadingData) return;

            if (m_targetType == TypeStorage.ByteArray)
            {
                m_holder.SetByteArray(Encoding.UTF8.GetBytes(textBox1.Text));
            }
            else
            {
                m_holder.SetString(textBox1.Text);
            }
            DispatchDataChanged();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A) textBox1.SelectAll();
        }
    }

    [CellDataEditor(Name = "text", Title = "Text")]
    public class TextCellDataEditor : CellDataEditorBase
    {
        public override string MenuTitle
        {
            get { return "s_text"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.text; }
        }

        protected override CellDataFrameBase CreateControl()
        {
            return new TextCellDataFrame();
        }

        public override int SupportLevel(IDataHolder data, IBedValueReader holder)
        {
            switch (holder.GetFieldType())
            {
                case  TypeStorage.String:
                    return 3;
                case TypeStorage.ByteArray:
                    try
                    {
                        Encoding.UTF8.GetString(holder.GetByteArray());
                        return 4;
                    }
                    catch
                    {
                        return 0;
                    }
                default:
                    return 1;
            }
        }
    }
}
