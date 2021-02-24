using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;

namespace DatAdmin
{
    //public enum ConditionType { Compoud, Expression, Custom }

    public class ConditionDesigner
    {
        public CompoudConditionNode Root;
        public Control Parent;
        public QueryDesignFrame Frame;

        public ConditionDesigner(Control parent, QueryDesignFrame frame)
        {
            Parent = parent;
            Frame = frame;
            Root = new CompoudConditionNode(this, null);
            UpdatePlacements();
        }

        public void UpdatePlacements()
        {
            int acty = 0;
            Root.UpdatePlacements(0, ref acty, "1");
            Frame.ReflectChanges();
        }

        public void RenderSql(ISqlDumper dmp)
        {
            if (Root.Children.Count == 0 || !Root.IsEnabled) return;
            dmp.Put("&n^where&>&n");
            Root.RenderSql(dmp);
            dmp.Put("&<");
        }

        public void SaveToXml(XmlElement xml)
        {
            Root.SaveToXml(xml);
        }

        public void LoadFromXml(XmlElement xml)
        {
            Root.LoadFromXml(xml);
            UpdatePlacements();
        }

        public void ReflectChanges()
        {
            Root.ReflectChanges();
        }
    }

    public class ConditionExpressionDesigner : IDisposable
    {
        public TextBox Edit;
        public Button DropDown;
        public ConditionNodeBase Node;
        public ContextMenuStripEx Popup;
        DisposeList m_disposeList = new DisposeList();
        bool m_supportsVars;

        public ConditionExpressionDesigner(ConditionNodeBase node, bool supportsVars)
        {
            Node = node;
            Edit = new TextBox();
            DropDown = new Button();
            DropDown.Width = DropDown.Height;
            DropDown.Image = CoreIcons.dropdown;
            Node.Designer.Parent.Controls.Add(Edit);
            Node.Designer.Parent.Controls.Add(DropDown);
            DropDown.Click += new EventHandler(DropDown_Click);
            Edit.TextChanged += new EventHandler(Edit_TextChanged);
            Edit.BackColor = Color.White;
            Edit.ForeColor = Color.Black;

            m_disposeList.Add(Edit);
            m_disposeList.Add(DropDown);
            m_supportsVars = supportsVars;
        }

        void Edit_TextChanged(object sender, EventArgs e)
        {
            Node.Designer.Frame.ReflectChanges();
            ReflectChanges();
        }

        void DropDown_Click(object sender, EventArgs e)
        {
            if (Popup == null)
            {
                Popup = new ContextMenuStripEx();
                m_disposeList.Add(Popup);
            }
            Popup.Items.Clear();
            var mb = new MenuBuilder();
            foreach (var tbl in Node.Designer.Frame.Tables)
            {
                int weight = 0;
                foreach (var col in tbl.Columns)
                {
                    mb.AddItem(
                        tbl.AliasOrName + "/" + col.ColumnName,
                        new ColumnHandler { Table = tbl.AliasOrName, Column = col.ColumnName, Edit = Edit }.Click,
                        weight);
                    weight++;
                }
            }
            if (m_supportsVars) mb.AddItem("s_variable", InsertVariable, CoreIcons.variable);

            mb.AddObject(this);
            mb.GetMenuItems(Popup.Items);
            Popup.Show(DropDown, new Point(0, DropDown.Height));
        }

        private void InsertVariable()
        {
            string var = InsertVariableForm.RunQb();
            if (var != null) Edit.Text = var;
        }

        class ColumnHandler
        {
            internal string Table;
            internal string Column;
            internal TextBox Edit;
            internal void Click()
            {
                Edit.Text = Table + "." + Column;
            }
        }

        public void UpdatePlacement(ref int actx, int acty)
        {
            Edit.Left = actx;
            Edit.Top = acty;
            actx += Edit.Width;
            DropDown.Left = actx;
            DropDown.Top = acty;
            Edit.Visible = true;
            DropDown.Visible = true;
            actx += DropDown.Width;
        }

        #region IDisposable Members

        public void Dispose()
        {
            m_disposeList.Dispose();
        }

        #endregion

        public void Hide()
        {
            Edit.Visible = false;
            DropDown.Visible = false;
        }

        public void RenderSql(ISqlDumper dmp)
        {
            var full = Node.Designer.Frame.GetFullColumnName(Edit.Text);
            int val;
            if (full != null)
            {
                full.WriteExpression(dmp);
            }
            else if (Int32.TryParse(Edit.Text.Trim(), out val))
            {
                dmp.WriteRaw(Edit.Text.Trim());
            }
            else if (Edit.Text.StartsWith("@") && m_supportsVars)
            {
                string name = Edit.Text.Substring(1), defval = "";
                if (name.Contains(":"))
                {
                    int pos = name.IndexOf(':');
                    defval = name.Substring(pos + 1);
                    name = name.Substring(0, pos);
                }
                dmp.WriteRaw(String.Format("'###{0}||{1}###'", name, defval));
            }
            else
            {
                dmp.Put("%v", Edit.Text);
            }
        }

        public void SaveToXml(XmlElement xml)
        {
            xml.AddChild("Value").InnerText = Edit.Text;
        }

        public void LoadFromXml(XmlElement xml)
        {
            Edit.Text = xml.FindElement("Value").InnerText;
        }

        public void ReflectChanges()
        {
            var full = Node.Designer.Frame.GetFullColumnName(Edit.Text);
            int val;
            if (full != null)
            {
                Edit.ForeColor = Color.DarkGreen;
            }
            else if (Int32.TryParse(Edit.Text.Trim(), out val))
            {
                Edit.ForeColor = Color.DarkRed;
            }
            else if (Edit.Text.StartsWith("@") && m_supportsVars)
            {
                Edit.ForeColor = Color.DarkCyan;
            }
            else
            {
                Edit.ForeColor = Color.Black;
            }
        }
    }


}