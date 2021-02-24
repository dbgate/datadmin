using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class SqlConfirmForm : FormEx
    {
        string m_settingsKey;

        public SqlConfirmForm(string settingsKey)
        {
            InitializeComponent();
            m_settingsKey = settingsKey;
            chbDontShowAgain.Visible = pictureBox1.Visible = m_settingsKey != null;
        }

        public static bool Run(string sql)
        {
            return Run(sql, null, null, null);
        }

        public static bool Run(string sql, ISqlDialect dialect)
        {
            return Run(sql, dialect, null, null);
        }

        public static bool Run(string sql, ISqlDialect dialect, ILogMessageSource warnings)
        {
            return Run(sql, dialect, warnings, null);
        }

        public static bool Run(string sql, ISqlDialect dialect, ILogMessageSource warnings, string settingsKey)
        {
            SqlConfirmForm win = new SqlConfirmForm(settingsKey);
            win.codeEditor1.SetCodeText(sql, false);
            win.codeEditor1.Dialect = dialect;
            win.messageLogFrame1.Source = warnings;
            DialogResult res = win.ShowDialogEx();
            win.Dispose();
            return res == DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SqlConfirmForm_Shown(object sender, EventArgs e)
        {
            btnOk.Focus();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            StdDialog.ShowInfo("s_this_options_changes_global_settings");
        }

        private void chbDontShowAgain_CheckedChanged(object sender, EventArgs e)
        {
            var dct = new Dictionary<string, string>();
            dct[m_settingsKey] = chbDontShowAgain.Checked ? "0" : "1";
            GlobalSettings.Pages.DirectModify(dct);
        }
    }
}