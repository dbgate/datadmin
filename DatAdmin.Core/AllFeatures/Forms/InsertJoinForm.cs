using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class InsertJoinForm : FormEx
    {
        SqlEditorAnalyser m_anal;
        SqlEditorAnalyser.TableItem m_exTable;
        CodeCompletionSettings m_settings;

        public class ForeignKeyStruct
        {
            public string ExistingColumn;
            public NameWithSchema NewTable;
            public string NewColumn;
            public string ForeignKey;
            public IForeignKey Fk;
            public bool BackRef;
            public string TableName { get { return NewTable.ToString(); } }
        }

        public InsertJoinForm(SqlEditorAnalyser anal, CodeCompletionSettings settings)
        {
            InitializeComponent();
            codeEditor1.Dialect = anal.Dialect;
            m_anal = anal;
            m_settings = settings;
            foreach (var tbl in anal.UsedTables)
            {
                if (tbl.RealTable == null) continue;
                lbxExistingTable.Items.Add(tbl);
            }
            if (lbxExistingTable.Items.Count > 0) lbxExistingTable.SelectedIndex = 0;
        }

        public static void Run(SqlEditorAnalyser anal, CodeEditor editor, CodeCompletionSettings settings)
        {
            var win = new InsertJoinForm(anal, settings);
            if (win.ShowDialogEx() == DialogResult.OK)
            {
                string text = win.GetSqlText();
                editor.InsertTextOnCursor(text);
                editor.ActiveTextAreaControl.Caret.Position = editor.Document.OffsetToPosition(editor.ActiveTextAreaControl.Caret.Offset + text.Length);
            }
        }

        private void lbxExistingTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_exTable = (SqlEditorAnalyser.TableItem)lbxExistingTable.SelectedItem;
            olvNewTable.ClearObjects();
            if (m_exTable == null) return;

            foreach (var fk in m_exTable.Structure.GetConstraints<IForeignKey>())
            {
                olvNewTable.AddObject(new ForeignKeyStruct
                {
                    ExistingColumn = fk.Columns.GetNames().CreateDelimitedText(", "),
                    ForeignKey = fk.Name,
                    NewColumn = fk.PrimaryKeyColumns.GetNames().CreateDelimitedText(", "),
                    NewTable = fk.PrimaryKeyTable,
                    Fk = fk,
                    BackRef = false,
                });
            }

            foreach (var fk in m_exTable.Structure.GetReferencedFrom())
            {
                olvNewTable.AddObject(new ForeignKeyStruct
                {
                    ExistingColumn = fk.PrimaryKeyColumns.GetNames().CreateDelimitedText(", "),
                    ForeignKey = fk.Name,
                    NewColumn = fk.Columns.GetNames().CreateDelimitedText(", "),
                    NewTable = fk.Table.FullName,
                    Fk = fk,
                    BackRef = true,
                });
            }

            if (olvNewTable.Items.Count > 0) olvNewTable.SelectedIndex = 0;
        }

        private void GenerateSql(ISqlDumper dmp)
        {
            var obj = olvNewTable.SelectedObject as ForeignKeyStruct;
            if (obj == null) return;
            if (m_exTable == null) return;
            if (rbtInner.Checked) dmp.Put("^inner ^join");
            if (rbtLeft.Checked) dmp.Put("^left ^join");
            if (rbtRight.Checked) dmp.Put("^right ^join");
            dmp.Put(" %f ", obj.NewTable);
            if (!tbxAlias.Text.IsEmpty()) dmp.Put("%s ", tbxAlias.Text);
            dmp.Put("^on ");
            for (int col = 0; col < obj.Fk.Columns.Count; col++)
            {
                if (col > 0) dmp.Put(" ^and ");
                if (m_exTable.Alias != null) dmp.WriteRaw(m_exTable.Alias);
                else dmp.Put("%f", m_exTable.FullName);
                dmp.Put(".");
                if (obj.BackRef) dmp.Put("%i", obj.Fk.PrimaryKeyColumns[col].ColumnName);
                else dmp.Put("%i", obj.Fk.Columns[col].ColumnName);
                dmp.Put(" = ");
                if (!tbxAlias.Text.IsEmpty()) dmp.Put("%s", tbxAlias.Text);
                else dmp.Put("%f", obj.NewTable);
                if (obj.BackRef) dmp.Put(".%i", obj.Fk.Columns[col].ColumnName);
                else dmp.Put(".%i", obj.Fk.PrimaryKeyColumns[col].ColumnName);
            }
        }

        private void RefreshPreview()
        {
            codeEditor1.SetCodeText(GetSqlText(), false);
        }

        private string GetSqlText()
        {
            var props = new SqlFormatProperties();
            props.IdentifierQuoteMode = m_settings.QuoteIdentifiers ? SqlIdentifierQuoteMode.Quoted : SqlIdentifierQuoteMode.Plain;
            props.SqlCommandCase = m_settings.KeywordCase == CharacterCase2.Upper ? CharacterCase.Upper : CharacterCase.Lower;
            props.QualifierMode = m_settings.AddSchema ? SqlQualifierMode.Original : SqlQualifierMode.OmitAll;
            return m_anal.Dialect.GenerateScript(GenerateSql, props);
        }

        private void olvNewTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void InsertJoinForm_Shown(object sender, EventArgs e)
        {
            lbxExistingTable.Focus();
        }

        private void InsertJoinForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }

        private void InsertJoinForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (lbxExistingTable.Focused)
                {
                    olvNewTable.Focus();
                }
                else if (olvNewTable.Focused)
                {
                    tbxAlias.Focus();
                }
                else if (tbxAlias.Focused)
                {
                    btnOk.Focus();
                }
            }
        }

        private void tbxAlias_TextChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void rbtInner_CheckedChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }
    }
}
