using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;

namespace DatAdmin
{
    public abstract class ConditionNodeBase : IDisposable
    {
        public const int DX = 30;
        public ConditionDesigner Designer;
        public Button CtrlButton;
        public Button RemoveButton;
        public CheckBox EnabledCheck;
        public ContextMenuStripEx Popup;
        public CompoudConditionNode Parent;
        protected DisposeList m_disposeList = new DisposeList();

        protected ConditionNodeBase(ConditionDesigner designer, CompoudConditionNode parent)
        {
            Designer = designer;
            Parent = parent;

            CtrlButton = new Button();
            CtrlButton.Click += new EventHandler(CtrlButton_Click);
            m_disposeList.Add(CtrlButton);
            Designer.Parent.Controls.Add(CtrlButton);

            RemoveButton = new Button();
            RemoveButton.Width = RemoveButton.Height;
            RemoveButton.Image = CoreIcons.close;
            m_disposeList.Add(RemoveButton);
            Designer.Parent.Controls.Add(RemoveButton);
            RemoveButton.Click += new EventHandler(RemoveButton_Click);
            RemoveButton.Enabled = RemoveConditionEnabled();

            EnabledCheck = new CheckBox();
            m_disposeList.Add(EnabledCheck);
            EnabledCheck.AutoSize = true;
            Designer.Parent.Controls.Add(EnabledCheck);
            IsEnabled = true;
            EnabledCheck.CheckedChanged += new EventHandler(EnabledCheck_CheckedChanged);
        }

        void EnabledCheck_CheckedChanged(object sender, EventArgs e)
        {
            Designer.Frame.ReflectChanges();
        }

        void RemoveButton_Click(object sender, EventArgs e)
        {
            RemoveCondition();
        }

        public static ConditionNodeBase Load(ConditionDesigner designer, XmlElement xml, CompoudConditionNode parent)
        {
            string type = xml.GetAttribute("type");
            ConditionNodeBase res = null;
            switch (type)
            {
                case "expr":
                    res = new ExpressionConditionNode(designer, parent);
                    break;
                case "compoud":
                    res = new CompoudConditionNode(designer, parent);
                    break;
            }
            res.LoadFromXml(xml);
            return res;
        }

        void CtrlButton_Click(object sender, EventArgs e)
        {
            if (Popup == null)
            {
                Popup = new ContextMenuStripEx();
                m_disposeList.Add(Popup);
            }
            Popup.Items.Clear();
            var mb = new MenuBuilder();
            mb.AddObject(this);
            mb.GetMenuItems(Popup.Items);
            Popup.Show(CtrlButton, new Point(0, CtrlButton.Height));
        }

        [PopupMenuEnabled("s_convert_to_compoud")]
        public bool ConvertToCompoudEnabled()
        {
            return !(this is CompoudConditionNode);
        }

        [PopupMenu("s_convert_to_compoud")]
        public void ConvertToCompoud()
        {
            var comp = new CompoudConditionNode(Designer, Parent);
            this.Parent.Children.Remove(this);
            this.Parent.Children.Add(comp);
            this.Parent = comp;
            comp.Children.Add(this);
            Designer.UpdatePlacements();
        }

        [PopupMenuEnabled("s_remove_condition")]
        public bool RemoveConditionEnabled()
        {
            return Parent != null;
        }

        [PopupMenu("s_remove_condition")]
        public void RemoveCondition()
        {
            Parent.Children.Remove(this);
            Dispose();
            Designer.UpdatePlacements();
        }

        protected virtual void UpdatePlacements_Specific(ref int actx, int acty, ref int linehi, string namePrefix) { }
        protected virtual void UpdatePlacements_Childs(int left, ref int acty, string namePrefix) { }
        public abstract void RenderSql(ISqlDumper dmp);
        protected abstract string XmlType { get; }

        public virtual void SaveToXml(XmlElement xml)
        {
            this.SavePropertiesCore(xml);
            xml.SetAttribute("type", XmlType);
        }
        public virtual void LoadFromXml(XmlElement xml)
        {
            this.LoadPropertiesCore(xml);
        }

        public void UpdatePlacements(int actx, ref int acty, string namePrefix)
        {
            int linehi = CtrlButton.Height;
            int left = actx;
            CtrlButton.Left = actx;
            actx += CtrlButton.Width;
            CtrlButton.Top = acty;
            CtrlButton.Text = namePrefix;
            UpdatePlacements_Specific(ref actx, acty, ref linehi, namePrefix);

            RemoveButton.Left = actx;
            RemoveButton.Top = acty + linehi / 2 - RemoveButton.Height / 2;
            actx += RemoveButton.Width;

            EnabledCheck.Left = actx;
            EnabledCheck.Top = acty + linehi / 2 - EnabledCheck.Height / 2;
            actx += EnabledCheck.Width;

            acty += linehi;
            UpdatePlacements_Childs(left, ref acty, namePrefix);
        }

        [XmlElem]
        public bool IsEnabled
        {
            get { return EnabledCheck.Checked; }
            set { EnabledCheck.Checked = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            m_disposeList.Dispose();
        }

        #endregion

        public virtual void ReflectChanges()
        {
        }
    }

    public class CompoudConditionNode : ConditionNodeBase
    {
        public ComboBox CompoudType;
        public List<ConditionNodeBase> Children = new List<ConditionNodeBase>();

        public CompoudConditionNode(ConditionDesigner designer, CompoudConditionNode parent)
            : base(designer, parent)
        {
            CompoudType = new ComboBox();
            CompoudType.Width = 200;
            CompoudType.Items.Add(Texts.Get("s_all_conditions_must_match"));
            CompoudType.Items.Add(Texts.Get("s_any_condition_must_match"));
            CompoudType.Items.Add(Texts.Get("s_none_condition_must_match"));
            CompoudType.Items.Add(Texts.Get("s_any_condition_mus_not_match"));
            CompoudType.DropDownStyle = ComboBoxStyle.DropDownList;
            CompoudType.SelectedIndex = 0;
            m_disposeList.Add(CompoudType);
            Designer.Parent.Controls.Add(CompoudType);
        }

        [PopupMenuEnabled("s_pack_compoud_condition")]
        public bool PackCompoudConditionEnabled()
        {
            return Children.Count == 1;
        }

        [PopupMenu("s_pack_compoud_condition")]
        public void PackCompoudCondition()
        {
            this.Parent.Children.Remove(this);
            this.Parent.Children.Add(Children[0]);
            Children[0].Parent = Parent;
            m_disposeList.Remove(Children[0]);
            Children.Clear();
            Dispose();
            Designer.UpdatePlacements();
        }

        [PopupMenu("s_add_condition")]
        public void AddCondition()
        {
            var child = new ExpressionConditionNode(Designer, this);
            Children.Add(child);
            m_disposeList.Add(child);
            Designer.UpdatePlacements();
        }

        protected override void UpdatePlacements_Specific(ref int actx, int acty, ref int linehi, string namePrefix)
        {
            CompoudType.Left = actx;
            CompoudType.Top = acty;
            actx += CompoudType.Width;
        }

        protected override void UpdatePlacements_Childs(int left, ref int acty, string namePrefix)
        {
            int childx = left + DX;

            int index = 1;
            foreach (var child in Children)
            {
                child.UpdatePlacements(childx, ref acty, namePrefix + "." + index.ToString());
                index++;
            }
        }

        public override void RenderSql(ISqlDumper dmp)
        {
            string prefix = null, joiner = null;
            switch (CompoudType.SelectedIndex)
            {
                case 0:
                    joiner = "AND";
                    break;
                case 1:
                    joiner = "OR";
                    break;
                case 2:
                    prefix = "NOT";
                    joiner = "OR";
                    break;
                case 3:
                    prefix = "NOT";
                    joiner = "AND";
                    break;
            }
            if (prefix != null) dmp.Put(" %k ", prefix);
            dmp.Put("(");
            bool was = false;
            foreach (var child in Children)
            {
                if (!child.IsEnabled) continue;

                if (was) dmp.Put(" %k ", joiner);
                child.RenderSql(dmp);
                was = true;
            }
            if (!was) dmp.Put("1=1");
            dmp.Put(")");
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);

            foreach (var child in Children)
            {
                child.SaveToXml(xml.AddChild("Child"));
            }
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);

            foreach (XmlElement cx in xml.SelectNodes("Child"))
            {
                var child = ConditionNodeBase.Load(Designer, cx, this);
                Children.Add(child);
            }
        }

        protected override string XmlType
        {
            get { return "compoud"; }
        }

        public override void ReflectChanges()
        {
            base.ReflectChanges();
            foreach (var child in Children) child.ReflectChanges();
        }
    }

    public class ExpressionConditionNode : ConditionNodeBase
    {
        public ComboBox Operator;
        public ConditionExpressionDesigner LeftExpr;
        ConditionOperatorDef m_lastExpr;

        public ExpressionConditionNode(ConditionDesigner designer, CompoudConditionNode parent)
            : base(designer, parent)
        {
            LeftExpr = new ConditionExpressionDesigner(this, true);
            m_disposeList.Add(LeftExpr);
            Operator = new ComboBox();
            m_disposeList.Add(Operator);
            Designer.Parent.Controls.Add(Operator);
            string lastgrp = null;
            foreach (var edef in ConditionOperatorDef.CreateConditions(this))
            {
                if (lastgrp == null || edef.Factory.Group != lastgrp)
                {
                    lastgrp = edef.Factory.Group;
                    Operator.Items.Add(Texts.Get(lastgrp));
                }
                Operator.Items.Add(edef);
                m_disposeList.Add(edef);
                edef.Hide();
            }
            Operator.SelectedIndex = 1;
            Operator.DropDownStyle = ComboBoxStyle.DropDownList;
            Operator.SelectedIndexChanged += new EventHandler(Operator_SelectedIndexChanged);
        }

        bool m_changingIndex;
        void Operator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_changingIndex) return;
            m_changingIndex = true;
            if (Operator.SelectedItem is string) Operator.SelectedIndex++;
            m_changingIndex = false;
            Designer.UpdatePlacements();
        }

        protected override void UpdatePlacements_Specific(ref int actx, int acty, ref int linehi, string namePrefix)
        {
            LeftExpr.UpdatePlacement(ref actx, acty);
            Operator.Width = 200;
            Operator.Top = acty;
            Operator.Left = actx;
            actx += Operator.Width;
            if (m_lastExpr != Operator.SelectedItem && m_lastExpr != null)
            {
                m_lastExpr.Hide();
            }
            var newexpr = (ConditionOperatorDef)Operator.SelectedItem;
            if (m_lastExpr != null && newexpr != null)
            {
                newexpr.AssignFrom(m_lastExpr);
            }
            m_lastExpr = newexpr;
            if (m_lastExpr != null)
            {
                m_lastExpr.UpdatePlacements(ref actx, acty, ref linehi);
            }
        }

        public override void RenderSql(ISqlDumper dmp)
        {
            if (m_lastExpr != null)
            {
            if (m_lastExpr.RenderLeftSql) LeftExpr.RenderSql(dmp);
            m_lastExpr.RenderSql(LeftExpr, dmp);
        }
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);

            if (LeftExpr != null) LeftExpr.SaveToXml(xml.AddChild("LeftExpr"));
            if (m_lastExpr != null)
            {
                xml.SetAttribute("operator", m_lastExpr.Factory.Identifier);
                m_lastExpr.SaveToXml(xml);
            }
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);

            string opname = xml.GetAttribute("operator");
            var lex = xml.FindElement("LeftExpr");
            if (lex != null)
            {
                LeftExpr.LoadFromXml(lex);
            }
            if (!String.IsNullOrEmpty(opname))
            {
                for (int i = 0; i < Operator.Items.Count; i++)
                {
                    var op = Operator.Items[i] as ConditionOperatorDef;
                    if (op == null) continue;
                    if (op.Factory.Identifier == opname)
                    {
                        Operator.SelectedIndex = i;
                    }
                }
                if (Operator.SelectedIndex < 0) throw new InternalError("DAE-00186 Unknown operator:" + opname);
                ((ConditionOperatorDef)Operator.SelectedItem).LoadFromXml(xml);
            }
        }

        protected override string XmlType
        {
            get { return "expr"; }
        }

        public string LeftExprData
        {
            get { return LeftExpr.Edit.Text; }
            set { LeftExpr.Edit.Text = value; }
        }

        public override void ReflectChanges()
        {
            base.ReflectChanges();
            LeftExpr.ReflectChanges();
            if (m_lastExpr != null) m_lastExpr.ReflectChanges();
        }
    }
}
