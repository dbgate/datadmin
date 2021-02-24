using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace DatAdmin
{
    public partial class ImageCellDataFrame : CellDataFrameBase
    {
        public ImageCellDataFrame()
        {
            InitializeComponent();
        }

        public static Image LoadImage(byte[] data)
        {
            if (data == null) return null;
            //if (!(data is byte[])) return null;
            MemoryStream fr = new MemoryStream((byte[])data);
            try
            {
                fr.Position = 0;
                Image img = Image.FromStream(fr);
                return img;
            }
            catch (Exception)
            {
                return null;
                //BinaryFormatter b = new BinaryFormatter();
                //fr.Position = 0;
                //object o = b.Deserialize(fr);
            }
        }

        private void SetImage(byte[] data)
        {
            Image img = LoadImage(data);
            pictureBox1.Image = img;
            if (img == null)
            {
                propertyGridImage.SelectedObject = null;
                Controls.ShowError(true);
            }
            else
            {
                propertyGridImage.SelectedObject = new ImageInfoProperties(img, data);
            }
        }

        public override void ShowCurrentData()
        {
            if (m_holder.GetFieldType() == TypeStorage.ByteArray)
            {
                SetImage(m_holder.GetByteArray());
            }
            else
            {
                propertyGridImage.SelectedObject = null;
                Controls.ShowError(true);
            }
        }
    }

    [CellDataEditor(Name = "image", Title = "Image")]
    public class ImageCellDataEditor : CellDataEditorBase
    {
        public override string MenuTitle
        {
            get { return "s_image"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.picture; }
        }

        protected override CellDataFrameBase CreateControl()
        {
            return new ImageCellDataFrame();
        }

        public override int SupportLevel(IDataHolder data, IBedValueReader holder)
        {
            if (holder.GetFieldType() == TypeStorage.ByteArray)
            {
                var img = ImageCellDataFrame.LoadImage(holder.GetByteArray());
                if (img == null) return 0;
                img.Dispose();
                return 10;
            }
            return 0;
        }
    }

    public class ImageInfoProperties : PropertyPageBase
    {
        [DisplayName("s_width")]
        public int Width { get; private set; }
        [DisplayName("s_height")]
        public int Height { get; private set; }
        [DisplayName("s_format")]
        public ImageFormat Format { get; private set; }
        [DisplayName("s_bytes")]
        public int Bytes { get; private set; }

        public ImageInfoProperties(Image img, byte[] data)
        {
            Width = img.Width;
            Height = img.Height;
            Format = img.RawFormat;
            Bytes = data.Length;
        }
    }
}
