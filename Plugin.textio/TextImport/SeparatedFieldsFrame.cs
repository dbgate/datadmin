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
    public partial class SeparatedFieldsFrame : FieldAnalyseFrame
    {
        public SeparatedFieldsFrame(TextImportFrame frame)
            : base(frame)
        {
            InitializeComponent();
        }

        public override string ComboTitle()
        {
            return "s_separated_fields";
        }

        public override FieldAnalyser CreateAnalyser()
        {
            SeparatedFieldsAnalyser res = new SeparatedFieldsAnalyser();
            res.ColNameTemplate = tbxColName.Text;
            res.Separator = tbxSeparator.Text;
            res.IsRegex = chbIsRegex.Checked;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                try
                {
                    res.Fields.Add(new SeparatedFieldsAnalyser.Field
                    {
                        Name = row.Cells[0].ToString(),
                        Position = Int32.Parse(row.Cells[1].ToString())
                    });
                }
                catch
                {
                    // ignore field
                }
            }
            return res;
        }

        public override void LoadFromAnalyser(FieldAnalyser analyser)
        {
            var a = analyser as SeparatedFieldsAnalyser;
            tbxColName.Text = a.ColNameTemplate;
            tbxSeparator.Text = a.Separator;
            chbIsRegex.Checked = a.IsRegex;
            dataGridView1.Rows.Clear();
            foreach (var fld in a.Fields)
            {
                dataGridView1.Rows.Add(fld.Name, fld.Position.ToString());
            }
        }

        private void tbxSeparator_TextChanged(object sender, EventArgs e)
        {
            m_frame.Save();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            m_frame.Save();
        }
    }
}
