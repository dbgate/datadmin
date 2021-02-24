using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class DatabasePropertiesForm : FormEx
    {
        IDialectSpecificEditor m_editor;
        DatabaseProperties m_props;

        public DatabasePropertiesForm(DatabaseProperties props, IDatabaseSource conn, bool readOnlyName)
        {
            InitializeComponent();
            m_props = props;
            DatabaseStructure dbs = new DatabaseStructure();
            dbs.SpecificData.AddAll(props.SpecificData);
            if (conn.Dialect != null) m_editor = conn.Dialect.GetSpecificEditor(dbs, conn);
            propertyFrame1.SelectedObject = m_editor;
            tbxName.ReadOnly = readOnlyName;
            tbxName.Text = props.Name;
        }

        public static bool Run(DatabaseProperties props, IDatabaseSource conn, bool readOnlyName)
        {
            var win = new DatabasePropertiesForm(props, conn, readOnlyName);
            return win.ShowDialogEx() == DialogResult.OK;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            var db = new DatabaseStructure();
            if (m_editor != null) m_editor.SaveToStructure(db);
            m_props.SpecificData = new Dictionary<string, string>();
            m_props.SpecificData.AddAll(db.SpecificData);
            m_props.Name = tbxName.Text;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    public class DatabaseProperties
    {
        public string Name;
        public Dictionary<string, string> SpecificData;
    }
}
