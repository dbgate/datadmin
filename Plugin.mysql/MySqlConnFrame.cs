using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.mysql
{
    public partial class MySqlConnFrame : ConnectionEditFrame
    {
        public MySqlConnFrame(MySqlStoredConnection conn)
            : base(conn)
        {
            InitializeComponent();
            tbxDataSource.Text = conn.DataSource;
            tbxLogin.Text = conn.Login;
            tbxPassword.Text = conn.Password;
            tbxPort.Text = conn.Port.ToString();
            cbxCharacterSet.Items.Add(new EncodingItem { Title = Texts.Get("s_default"), WebName = null });
            foreach (var i in EncodingTypeConverter.EncodingItems)
            {
                cbxCharacterSet.Items.Add(i);
            }
            cbxCharacterSet.SelectedIndex = 0;
        }

        public override void SaveConnection()
        {
            MySqlStoredConnection conn = (MySqlStoredConnection)m_conn;
            conn.DataSource = tbxDataSource.Text;
            conn.Login = tbxLogin.Text;
            conn.Password = tbxPassword.Text;
            EncodingItem enc = (EncodingItem)cbxCharacterSet.SelectedItem;
            if (enc.WebName == null) conn.CharacterSet = null;
            else conn.CharacterSet = Encoding.GetEncoding(enc.WebName);
            try { conn.Port = Int32.Parse(tbxPort.Text); }
            catch { conn.Port = 3306; }
        }

        private void datasource_TextChanged(object sender, EventArgs e)
        {
            CallConnectionChanged();
        }

    }
}
