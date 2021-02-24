using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class DmlfExpressionFrame : UserControl
    {
        public event EventHandler ValueChanged;
        bool m_supportsVars;
        DmlfSource[] m_availableSources;
        IDmlfHandler m_handler;

        public DmlfExpressionFrame()
        {
            InitializeComponent();
        }

        class ColumnHandler
        {
            internal DmlfColumnRef Column;
            internal TextBox Edit;
            internal void Click()
            {
                Edit.Text = Column.ToString();
            }
        }

        public IDmlfHandler Handler
        {
            get { return m_handler; }
            set { m_handler = value; }
        }

        public DmlfSource[] AvailableSources
        {
            get { return m_availableSources; }
            set { m_availableSources = value; }
        }

        public bool SupportsVars
        {
            get { return m_supportsVars; }
            set { m_supportsVars = value; }
        }

        private void tbxValue_TextChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null) ValueChanged(this, e);
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Items.Clear();
            var mb = new MenuBuilder();
            if (AvailableSources != null && Handler != null)
            {
                foreach (var src in AvailableSources)
                {
                    int weight = 0;
                    var tbl = Handler.GetStructure(src != null ? src.TableOrView : null);
                    foreach (var col in tbl.Columns)
                    {
                        var cref = new DmlfColumnRef { Source = src, ColumnName = col.ColumnName };
                        mb.AddItem(
                            src != null ? src.AliasOrName + "/" + col.ColumnName : "(SOURCE)/" + col.ColumnName,
                            new ColumnHandler { Column = cref, Edit = tbxValue }.Click,
                            weight
                            );
                        weight++;
                    }
                }
            }
            if (SupportsVars) mb.AddItem("s_variable", InsertVariable, CoreIcons.variable);
            mb.GetMenuItems(contextMenuStrip1.Items);
            contextMenuStrip1.Show(btnChange, new Point(0, btnChange.Height));
        }

        private void InsertVariable()
        {
            string var = InsertVariableForm.RunQb();
            if (var != null) tbxValue.Text = var;
        }

        public DmlfExpression Expression
        {
            get
            {
                var col = AvailableSources.FindColumn(tbxValue.Text, Handler);
                if (col != null) return new DmlfColumnRefExpression { Column = col };
                return new DmlfStringExpression { Value = tbxValue.Text };
            }
            set
            {
                tbxValue.Text = value.SafeToString() ?? "";
            }
        }
    }
}
