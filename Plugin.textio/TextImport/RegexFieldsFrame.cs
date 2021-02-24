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
    public partial class RegexFieldsFrame : FieldAnalyseFrame
    {
        public RegexFieldsFrame(TextImportFrame frame)
            : base(frame)
        {
            InitializeComponent();
        }

        public override FieldAnalyser CreateAnalyser()
        {
            RegexFieldAnalyser res = new RegexFieldAnalyser();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    res.Fields.Add(new RegexFieldAnalyser.Field
                    {
                        FieldName = row.Cells[0].Value.SafeToString(),
                        Pattern = row.Cells[1].Value.SafeToString(),
                        Group = row.Cells[2].Value.SafeToString(),
                        Skip = row.Cells[3].Value.SafeToString()
                    });
                }
            }
            return res;
        }

        public override string ComboTitle()
        {
            return "s_regular_expression";
        }

        public override void LoadFromAnalyser(FieldAnalyser analyser)
        {
            var a = analyser as RegexFieldAnalyser;
            dataGridView1.Rows.Clear();
            foreach (var fld in a.Fields)
            {
                dataGridView1.Rows.Add(fld.FieldName, fld.Pattern, fld.Group, fld.Skip);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            m_frame.Save();
        }
    }
}
