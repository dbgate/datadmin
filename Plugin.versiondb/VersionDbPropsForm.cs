using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.versiondb
{
    public partial class VersionDbPropsForm : FormEx
    {
        VersionDb m_db;
        int m_w0, m_h0;

        public VersionDbPropsForm(VersionDb db)
        {
            InitializeComponent();

            m_w0 = Width;
            m_h0 = Height;

            m_db = db;
            if (db.Versions.Count == 0)
            {
                if (String.IsNullOrEmpty(m_db.m_props.GetVersionSql) && String.IsNullOrEmpty(m_db.m_props.SetVersionSql))
                {
                    chbCreateDefaultVariables.Checked = true;
                }
                chbCreateDefaultVariables_CheckedChanged(this, EventArgs.Empty);
            }
            else
            {
                chbCreateDefaultVariables.Visible = false;
            }

            foreach (var item in DialectAddonType.GetAllDialects(true))
            {
                cbxDialect.Items.Add(item);
            }
            cbxDialect.SelectedIndex = 0;

            foreach (object item in cbxDialect.Items)
            {
                if (item is ISqlDialect && ((ISqlDialect)item).DisplayName == db.m_props.Dialect.DisplayName)
                {
                    cbxDialect.SelectedItem = item;
                }
            }

            chbForceSingleSchema.Checked = m_db.m_props.ForceSingleSchema;
            codeEditorGet.SetCodeText(m_db.m_props.GetVersionSql, false);
            codeEditorSet.SetCodeText(m_db.m_props.SetVersionSql, false);
        }

        public static bool Run(VersionDb db)
        {
            var win = new VersionDbPropsForm(db);
            if (win.ShowDialogEx() == DialogResult.OK)
            {
                db.m_props.ForceSingleSchema = win.chbForceSingleSchema.Checked;
                db.m_props.Dialect = (ISqlDialect)win.cbxDialect.SelectedItem;

                if (win.chbCreateDefaultVariables.Checked)
                {
                    db.CreateDefaultVariableTable();
                }
                else
                {
                    db.m_props.GetVersionSql = win.codeEditorGet.CodeText;
                    db.m_props.SetVersionSql = win.codeEditorSet.CodeText;
                }
                return true;
            }
            return false;
        }

        private void chbCreateDefaultVariables_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Visible = !chbCreateDefaultVariables.Checked;
            if (chbCreateDefaultVariables.Checked)
            {
                Width = cbxDialect.Right + 30;
                Height = chbCreateDefaultVariables.Bottom + 80;
            }
            else
            {
                Width = m_w0;
                Height = m_h0;

                if (String.IsNullOrEmpty(codeEditorGet.CodeText)) codeEditorGet.SetCodeText(VersionDb.DEFAULT_GET_VERSION, false);
                if (String.IsNullOrEmpty(codeEditorSet.CodeText)) codeEditorSet.SetCodeText(VersionDb.DEFAULT_SET_VERSION, false);

            }
        }
    }
}
