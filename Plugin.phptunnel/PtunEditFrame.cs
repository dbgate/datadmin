using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;
using System.IO;

namespace Plugin.phptunnel
{
    public partial class PtunEditFrame : TunnelConnFrameBase
    {
        public PtunEditFrame(PtunStoredConnection conn)
            : base(conn)
        {
            InitializeComponent();

            cbxEncodingStyle.Items.Add(Texts.Get("s_default"));
            cbxEncodingStyle.Items.Add(Texts.Get("s_database"));
            cbxEncodingStyle.Items.Add(Texts.Get("s_explicit"));

            foreach (var i in EncodingTypeConverter.EncodingItems)
            {
                cbxEncoding.Items.Add(i);
            }

            cbxEncoding.SelectedIndex = EncodingTypeConverter.GetEncodingIndex(Params.RealEncoding);

            tbxUrl.Text = Params.Url;
            cbxExtendedSafety.Checked = Params.ExtendedSafety;
            tbxHttpLogin.Text = Params.HttpLogin;
            tbxHttpPassword.Text = Params.HttpPassword;
            cbxEncodingStyle.SelectedIndex = (int)Params.EncodingStyle;
        }

        public PtunConnectionStringBuilder Params
        {
            get { return (PtunConnectionStringBuilder)m_conn.Params; }
        }

        public override void SaveConnection()
        {
            Params.Url = tbxUrl.Text;
            Params.ExtendedSafety = cbxExtendedSafety.Checked;
            Params.HttpLogin = tbxHttpLogin.Text;
            Params.HttpPassword = tbxHttpPassword.Text;
            Params.EncodingStyle = (EncodingStyle)cbxEncodingStyle.SelectedIndex;
            if (cbxEncoding.SelectedIndex >= 0) Params.RealEncoding = Encoding.GetEncoding(((EncodingItem)cbxEncoding.Items[cbxEncoding.SelectedIndex]).WebName);
        }

        private void btmSavePhpTunnel_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialogEx() == DialogResult.OK)
            {
                SaveConnection();
                using (FileStream fw = new FileStream(saveFileDialog1.FileName, FileMode.Create))
                {
                    byte[] buffer = Params.GetPhpTunnelFile();
                    fw.Write(buffer, 0, buffer.Length);
                    StdDialog.ShowInfo("s_ok");
                }
            }
        }
    }
}
