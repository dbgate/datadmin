using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.textio
{
    public partial class TextImportFrame : UserControl
    {
        TextImportDataStore m_store;
        bool m_created = false;

        public TextImportFrame(TextImportDataStore store)
        {
            InitializeComponent();

            foreach (var i in EncodingTypeConverter.EncodingItems)
            {
                cbxEncoding.Items.Add(i);
            }

            m_store = store;

            cbxFieldType.Items.Add(new FieldAnalyseComboItem { Frame = new WholeLineFieldFrame(this) });
            cbxFieldType.Items.Add(new FieldAnalyseComboItem { Frame = new SeparatedFieldsFrame(this) });
            cbxFieldType.Items.Add(new FieldAnalyseComboItem { Frame = new RegexFieldsFrame(this) });
            cbxFieldType.Items.Add(new FieldAnalyseComboItem { Frame = new ScriptFieldsFrame(this) });

            switch (m_store.SourceType)
            {
                case TextSourceType.File:
                    tbxFile.Text = m_store.Source;
                    rbtFile.Checked = true;
                    break;
                case TextSourceType.Url:
                    tbxUrl.Text = m_store.Source;
                    rbtUrl.Checked = true;
                    break;
                case TextSourceType.Text:
                    tbxTextInput.Text = m_store.Source;
                    rbtTextInput.Checked = true;
                    break;
            }

            tbxLineLimitBegin.Text = m_store.LineLimitBegin ?? "";
            tbxLineLimitEnd.Text = m_store.LineLimitEnd ?? "";
            tbxLineRegex.Text = m_store.LineRegex ?? "";
            tbxLineRegexGroup.Text = m_store.LineRegexGroup ?? "";
            tbxLineSeparator.Text = m_store.LineSeparator ?? "";
            chbLineSepRegex.Checked = m_store.LineSeparatowIsRegex;

            switch (m_store.RowDelimiter)
            {
                case RowDelimiterType.Limitation:
                    rbtLineLimitation.Checked = true;
                    break;
                case RowDelimiterType.Lines:
                    rbtLines.Checked = true;
                    break;
                case RowDelimiterType.Regex:
                    rbtLineRegex.Checked = true;
                    break;
                case RowDelimiterType.Separator:
                    rbtLineSeparator.Checked = true;
                    break;
            }

            UpdateRowsEnabling();
            UpdateSourceEnabling();

            cbxEncoding.SelectedIndex = EncodingTypeConverter.GetEncodingIndex(m_store.SourceEncoding);

            Translating.TranslateControl(this);

            if (m_store.FieldAnalyser is WholeLineAnalyser) cbxFieldType.SelectedIndex = 0;
            else if (m_store.FieldAnalyser is SeparatedFieldsAnalyser) cbxFieldType.SelectedIndex = 1;
            else if (m_store.FieldAnalyser is RegexFieldAnalyser) cbxFieldType.SelectedIndex = 2;
            else if (m_store.FieldAnalyser is ScriptAnalyser) cbxFieldType.SelectedIndex = 3;

            FieldFrame.LoadFromAnalyser(m_store.FieldAnalyser);

            // add field frame
            panel1.Controls.Add(FieldFrame);
            FieldFrame.Dock = DockStyle.Fill;

            m_created = true;
        }

        public FieldAnalyseFrame FieldFrame
        {
            get
            {
                var item = (FieldAnalyseComboItem)cbxFieldType.Items[cbxFieldType.SelectedIndex];
                return item.Frame;
            }
        }

        private void cbxFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_created) return;
            if (!panel1.Controls.Contains(FieldFrame))
            {
                panel1.Controls.Add(FieldFrame);
                FieldFrame.Dock = DockStyle.Fill;
            }
            foreach (Control c in panel1.Controls) c.Hide();
            FieldFrame.Show();
        }

        private void rbtOnSource_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_created) return;
            UpdateSourceEnabling();
            SourceChanged(sender, e);
        }

        public void Save()
        {
            if (!m_created) return;
            if (rbtFile.Checked)
            {
                m_store.SourceType = TextSourceType.File;
                m_store.Source = tbxFile.Text;
            }
            else if (rbtUrl.Checked)
            {
                m_store.SourceType = TextSourceType.Url;
                m_store.Source = tbxUrl.Text;
            }
            else if (rbtTextInput.Checked)
            {
                m_store.SourceType = TextSourceType.Text;
                m_store.Source = tbxTextInput.Text;
            }

            if (rbtLines.Checked) m_store.RowDelimiter = RowDelimiterType.Lines;
            else if (rbtLineLimitation.Checked) m_store.RowDelimiter = RowDelimiterType.Limitation;
            else if (rbtLineRegex.Checked) m_store.RowDelimiter = RowDelimiterType.Regex;
            else if (rbtLineSeparator.Checked) m_store.RowDelimiter = RowDelimiterType.Separator;

            m_store.LineLimitBegin = tbxLineLimitBegin.Text;
            m_store.LineLimitEnd = tbxLineLimitEnd.Text;
            m_store.LineRegex = tbxLineRegex.Text;
            m_store.LineRegexGroup = tbxLineRegexGroup.Text;
            m_store.LineSeparator = tbxLineSeparator.Text;
            m_store.LineSeparatowIsRegex = chbLineSepRegex.Checked;

            if (cbxEncoding.SelectedIndex >= 0)
            {
                m_store.SourceEncoding = Encoding.GetEncoding(((EncodingItem)cbxEncoding.Items[cbxEncoding.SelectedIndex]).WebName);
            }

            m_store.FieldAnalyser = FieldFrame.CreateAnalyser();
        }

        private void UpdateSourceEnabling()
        {
            btnBrowse.Enabled = tbxFile.Enabled = rbtFile.Checked;
            tbxUrl.Enabled = rbtUrl.Checked;
            tbxTextInput.Enabled = rbtTextInput.Checked;
        }

        private void rbtOnRows_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_created) return;
            UpdateRowsEnabling();
            Save();
        }

        private void UpdateRowsEnabling()
        {
            chbLineSepRegex.Enabled = tbxLineSeparator.Enabled = rbtLineSeparator.Checked;
            tbxLineLimitBegin.Enabled = tbxLineLimitEnd.Enabled = rbtLineLimitation.Checked;
            tbxLineRegex.Enabled = tbxLineRegexGroup.Enabled = rbtLineRegex.Checked;
        }

        private void AnyTextChanged(object sender, EventArgs e)
        {
            Save();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (!m_created) return;
            Save();
            dataGridViewRows.Rows.Clear();
            foreach (string row in m_store.EnumRows())
            {
                dataGridViewRows.Rows.Add(row);
            }
        }

        private void SourceChanged(object sender, EventArgs e)
        {
            if (!m_created) return;
            Save();
            m_store.ClearLoadCaches();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (!m_created) return;
            SourceChanged(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!m_created) return;
            Save();
            dataGridViewPreview.DataSource = new BedTable(m_store.GetTable());
        }
    }
}
