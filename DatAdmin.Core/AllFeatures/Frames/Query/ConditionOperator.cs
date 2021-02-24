using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;

namespace DatAdmin
{
    public abstract class ConditionOperatorFactory
    {
        public string Identifier;
        public string Text;
        public string Group;
        public abstract ConditionOperatorDef CreateInstance(ConditionNodeBase node);
    }

    public abstract class ConditionOperatorDef : IDisposable
    {
        //static List<Tuple<string, Type>> m_registeredTypes = new List<Tuple<string, Type>>();
        static List<ConditionOperatorFactory> m_registeredTypes = new List<ConditionOperatorFactory>();
        public ConditionNodeBase Node;
        protected DisposeList m_disposeList = new DisposeList();
        public abstract void UpdatePlacements(ref int actx, int acty, ref int linehi);
        public abstract void Hide();
        public ConditionOperatorFactory Factory;

        public ConditionOperatorDef(ConditionNodeBase node, ConditionOperatorFactory fact)
        {
            Node = node;
            Factory = fact;
        }

        static ConditionOperatorDef()
        {
            m_registeredTypes.Add(new SingleValueConditionOperatorFactory { Identifier = "eq", Sql = "=", Text = "=", Group = "s_comparison" });
            m_registeredTypes.Add(new SingleValueConditionOperatorFactory { Identifier = "ne", Sql = "<>", Text = "<>", Group = "s_comparison" });
            m_registeredTypes.Add(new SingleValueConditionOperatorFactory { Identifier = "ne2", Sql = "!=", Text = "!=", Group = "s_comparison" });
            m_registeredTypes.Add(new SingleValueConditionOperatorFactory { Identifier = "gt", Sql = ">", Text = ">", Group = "s_comparison" });
            m_registeredTypes.Add(new SingleValueConditionOperatorFactory { Identifier = "lt", Sql = "<", Text = "<", Group = "s_comparison" });
            m_registeredTypes.Add(new SingleValueConditionOperatorFactory { Identifier = "ge", Sql = ">=", Text = ">=", Group = "s_comparison" });
            m_registeredTypes.Add(new SingleValueConditionOperatorFactory { Identifier = "le", Sql = "<=", Text = "<=", Group = "s_comparison" });

            m_registeredTypes.Add(new SingleValueConditionOperatorFactory { Identifier = "like", Sql = "LIKE", Text = "LIKE", Group = "s_sql_native" });
            m_registeredTypes.Add(new SingleValueConditionOperatorFactory { Identifier = "not_like", Sql = "NOT LIKE", Text = "NOT LIKE", Group = "s_sql_native" });
            m_registeredTypes.Add(new SingleSimpleValueConditionOperatorFactory_In { Identifier = "in", Sql = "IN", Text = "IN", Group = "s_sql_native" });
            m_registeredTypes.Add(new SingleSimpleValueConditionOperatorFactory_In { Identifier = "not_in", Sql = "NOT IN", Text = "NOT IN", Group = "s_sql_native" });
            m_registeredTypes.Add(new NoValueExprDefFactory { Identifier = "is_null", Sql = "IS NULL", Text = "IS NULL", Group = "s_sql_native" });
            m_registeredTypes.Add(new NoValueExprDefFactory { Identifier = "is_not_null", Sql = "IS NOT NULL", Text = "IS NOT NULL", Group = "s_sql_native" });
            m_registeredTypes.Add(new DoubleValueConditionOperatorFactory { Identifier = "between", Sql = "BETWEEN", Text = "BETWEEN", Group = "s_sql_native" });
            m_registeredTypes.Add(new DoubleValueConditionOperatorFactory { Identifier = "not_between", Sql = "NOT BETWEEN", Text = "NOT BETWEEN", Group = "s_sql_native" });

            m_registeredTypes.Add(new SingleValueConditionOperatorFactory_Contains { Identifier = "contains", Text = "s_contains_text", Group = "s_text_operations" });
            m_registeredTypes.Add(new SingleValueConditionOperatorFactory_NotContains { Identifier = "not_contains", Text = "s_not_contains_text", Group = "s_text_operations" });

            m_registeredTypes.Add(new IsOneOfExprDefFactory { Identifier = "is_one_of", Text = "s_is_one_of", Group = "s_list_operations" });
            m_registeredTypes.Add(new IsNoneOfExprDefFactory { Identifier = "is_none_of", Text = "s_is_none_of", Group = "s_list_operations" });
            m_registeredTypes.Add(new ContainsOneOfExprDefFactory { Identifier = "contains_one_of", Text = "s_contains_one_of", Group = "s_list_operations" });
            m_registeredTypes.Add(new ContainsNoneOfExprDefFactory { Identifier = "contains_none_of", Text = "s_contains_none_of", Group = "s_list_operations" });
        }

        public static IEnumerable<ConditionOperatorDef> CreateConditions(ConditionNodeBase node)
        {
            foreach (var tpl in m_registeredTypes)
            {
                yield return tpl.CreateInstance(node);
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            m_disposeList.Dispose();
        }

        #endregion

        public abstract void RenderSql(ConditionExpressionDesigner leftExpr, ISqlDumper dmp);
        public virtual void SaveToXml(XmlElement xml) { }
        public virtual void LoadFromXml(XmlElement xml) { }

        public override string ToString()
        {
            return "  " + Texts.Get(Factory.Text);
        }

        public virtual void ReflectChanges()
        {
        }

        public abstract void AssignFrom(ConditionOperatorDef src);

        public virtual bool RenderLeftSql { get { return true; } }
    }

    public class NoValueExprDefFactory : ConditionOperatorFactory
    {
        public string Sql;
        public override ConditionOperatorDef CreateInstance(ConditionNodeBase node)
        {
            return new NoValueExprDef(node, this, Sql);
        }
    }

    public class NoValueExprDef : ConditionOperatorDef
    {
        public string Sql;

        public NoValueExprDef(ConditionNodeBase node, ConditionOperatorFactory fact, string sql)
            : base(node, fact)
        {
            Sql = sql;
        }

        public override void Hide() { }
        public override void UpdatePlacements(ref int actx, int acty, ref int linehi) { }

        public override void RenderSql(ConditionExpressionDesigner leftExpr, ISqlDumper dmp)
        {
            dmp.Put(" %k", Sql);
        }

        public override void AssignFrom(ConditionOperatorDef src)
        {
        }
    }

    public class SingleSimpleValueExprDef : ConditionOperatorDef
    {
        public TextBox Edit;
        public string Sql;

        public SingleSimpleValueExprDef(ConditionNodeBase node, ConditionOperatorFactory fact, string sql, string initText)
            : base(node, fact)
        {
            Sql = sql;
            Edit = new TextBox();
            m_disposeList.Add(Edit);
            Node.Designer.Parent.Controls.Add(Edit);
            Edit.Width = 200;
            Edit.Text = initText;
            Edit.TextChanged += new EventHandler(Values_TextChanged);
        }

        void Values_TextChanged(object sender, EventArgs e)
        {
            Node.Designer.Frame.ReflectChanges();
        }

        public override void UpdatePlacements(ref int actx, int acty, ref int linehi)
        {
            Edit.Visible = true;
            Edit.Left = actx;
            Edit.Top = acty;
            actx += Edit.Width;
            if (Edit.Height > linehi) linehi = Edit.Height;
        }

        public override void Hide()
        {
            if (Edit != null) Edit.Hide();
        }

        public override void RenderSql(ConditionExpressionDesigner leftExpr, ISqlDumper dmp)
        {
            dmp.Put(" %k ", Sql);
            dmp.Put(Edit.Text);
        }

        public override void SaveToXml(XmlElement xml)
        {
            xml.AddChild("RightExpr").InnerText = Edit.Text;
        }

        public override void LoadFromXml(XmlElement xml)
        {
            var el = xml.FindElement("RightExpr");
            if (el != null) Edit.Text = el.InnerText;
        }

        public override void AssignFrom(ConditionOperatorDef src)
        {
            if (src is SingleSimpleValueExprDef)
            {
                Edit.Text = ((SingleSimpleValueExprDef)src).Edit.Text;
            }
        }

        protected virtual bool SupportsVars { get { return true; } }
    }

    public class SingleSimpleValueExprDef_In : SingleSimpleValueExprDef
    {
        public SingleSimpleValueExprDef_In(ConditionNodeBase node, ConditionOperatorFactory fact, string sql)
            : base(node, fact, sql, "('item1', 'item2')")
        {
        }
    }

    public class SingleValueExprDef : ConditionOperatorDef
    {
        public ConditionExpressionDesigner Expr;
        public string Sql;

        public SingleValueExprDef(ConditionNodeBase node, ConditionOperatorFactory fact, string sql)
            : base(node, fact)
        {
            Expr = new ConditionExpressionDesigner(Node, SupportsVars);
            m_disposeList.Add(Expr);
            Sql = sql;
        }

        public override void UpdatePlacements(ref int actx, int acty, ref int linehi)
        {
            Expr.UpdatePlacement(ref actx, acty);
        }

        public override void Hide()
        {
            if (Expr != null) Expr.Hide();
        }

        public override void RenderSql(ConditionExpressionDesigner leftExpr, ISqlDumper dmp)
        {
            dmp.Put(" %k ", Sql);
            Expr.RenderSql(dmp);
        }

        public override void SaveToXml(XmlElement xml)
        {
            Expr.SaveToXml(xml.AddChild("RightExpr"));
        }

        public override void LoadFromXml(XmlElement xml)
        {
            Expr.LoadFromXml(xml.FindElement("RightExpr"));
        }

        public override void ReflectChanges()
        {
            Expr.ReflectChanges();
        }

        public override void AssignFrom(ConditionOperatorDef src)
        {
            if (src is SingleValueExprDef)
            {
                Expr.Edit.Text = ((SingleValueExprDef)src).Expr.Edit.Text;
            }
        }

        protected virtual bool SupportsVars { get { return true; } }
    }

    public class SingleValueConditionOperatorFactory : ConditionOperatorFactory
    {
        public string Sql;

        public override ConditionOperatorDef CreateInstance(ConditionNodeBase node)
        {
            return new SingleValueExprDef(node, this, Sql);
        }
    }

    public class SingleSimpleValueConditionOperatorFactory_In : ConditionOperatorFactory
    {
        public string Sql;

        public override ConditionOperatorDef CreateInstance(ConditionNodeBase node)
        {
            return new SingleSimpleValueExprDef_In(node, this, Sql);
        }
    }

    public class SingleValueExprDef_Contains : SingleSimpleValueExprDef
    {
        public SingleValueExprDef_Contains(ConditionNodeBase node, ConditionOperatorFactory fact)
            : base(node, fact, null, "")
        {
        }

        public override void RenderSql(ConditionExpressionDesigner leftExpr, ISqlDumper dmp)
        {
            dmp.Put(" ^like %v", "%" + Edit.Text + "%");
        }

        protected override bool SupportsVars { get { return false; } }
    }

    public class SingleValueConditionOperatorFactory_Contains : ConditionOperatorFactory
    {
        public override ConditionOperatorDef CreateInstance(ConditionNodeBase node)
        {
            return new SingleValueExprDef_Contains(node, this);
        }
    }

    public class SingleValueExprDef_NotContains : SingleSimpleValueExprDef
    {
        public SingleValueExprDef_NotContains(ConditionNodeBase node, ConditionOperatorFactory fact)
            : base(node, fact, null, "")
        {
        }

        public override void RenderSql(ConditionExpressionDesigner leftExpr, ISqlDumper dmp)
        {
            dmp.Put(" ^not ^like %v", "%" + Edit.Text + "%");
        }

        protected override bool SupportsVars { get { return false; } }
    }

    public class SingleValueConditionOperatorFactory_NotContains : ConditionOperatorFactory
    {
        public override ConditionOperatorDef CreateInstance(ConditionNodeBase node)
        {
            return new SingleValueExprDef_NotContains(node, this);
        }
    }

    public class DoubleValueExprDef : ConditionOperatorDef
    {
        public ConditionExpressionDesigner Expr1, Expr2;
        public Label AndLabel;
        public string Sql;

        public DoubleValueExprDef(ConditionNodeBase node, ConditionOperatorFactory fact, string sql)
            : base(node, fact)
        {
            Sql = sql;
            Expr1 = new ConditionExpressionDesigner(Node, true);
            m_disposeList.Add(Expr1);

            AndLabel = new Label { Text = Texts.Get("s_and") };
            Node.Designer.Parent.Controls.Add(AndLabel);
            AndLabel.AutoSize = true;
            m_disposeList.Add(AndLabel);

            Expr2 = new ConditionExpressionDesigner(Node, true);
            m_disposeList.Add(Expr2);
        }

        public override void UpdatePlacements(ref int actx, int acty, ref int linehi)
        {
            AndLabel.Visible = true;
            Expr1.UpdatePlacement(ref actx, acty);
            AndLabel.Left = actx;
            AndLabel.Top = acty;
            actx += AndLabel.Width;
            Expr2.UpdatePlacement(ref actx, acty);
        }

        public override void Hide()
        {
            if (Expr1 != null) Expr1.Hide();
            if (Expr2 != null) Expr2.Hide();
            if (AndLabel != null) AndLabel.Hide();
        }

        public override void RenderSql(ConditionExpressionDesigner leftExpr, ISqlDumper dmp)
        {
            dmp.Put(" %k ", Sql);
            Expr1.RenderSql(dmp);
            dmp.Put(" ^and ");
            Expr1.RenderSql(dmp);
        }

        public override void SaveToXml(XmlElement xml)
        {
            Expr1.SaveToXml(xml.AddChild("Expr1"));
            Expr2.SaveToXml(xml.AddChild("Expr2"));
        }

        public override void LoadFromXml(XmlElement xml)
        {
            Expr1.LoadFromXml(xml.FindElement("Expr1"));
            Expr2.LoadFromXml(xml.FindElement("Expr2"));
        }

        public override void ReflectChanges()
        {
            base.ReflectChanges();
            Expr1.ReflectChanges();
            Expr2.ReflectChanges();
        }

        public override void AssignFrom(ConditionOperatorDef src)
        {
            if (src is DoubleValueExprDef)
            {
                Expr1.Edit.Text = ((DoubleValueExprDef)src).Expr1.Edit.Text;
                Expr2.Edit.Text = ((DoubleValueExprDef)src).Expr2.Edit.Text;
            }
        }
    }

    public class DoubleValueConditionOperatorFactory: ConditionOperatorFactory
    {
        public string Sql;
        public override ConditionOperatorDef CreateInstance(ConditionNodeBase node)
        {
            return new DoubleValueExprDef(node, this, Sql);
        }
    }

    public abstract class ValueListExprDef : ConditionOperatorDef
    {
        public TextBox Values;

        public ValueListExprDef(ConditionNodeBase node, ConditionOperatorFactory fact)
            : base(node, fact)
        {
            Values = new TextBox();
            Values.Multiline = true;
            m_disposeList.Add(Values);
            Node.Designer.Parent.Controls.Add(Values);
            Values.Width = 200;
            Values.Height = 100;
            Values.TextChanged += new EventHandler(Values_TextChanged);
        }

        void Values_TextChanged(object sender, EventArgs e)
        {
            Node.Designer.Frame.ReflectChanges();
        }

        public override void UpdatePlacements(ref int actx, int acty, ref int linehi)
        {
            Values.Visible = true;
            Values.Left = actx;
            Values.Top = acty;
            actx += Values.Width;
            if (Values.Height > linehi) linehi = Values.Height;
        }

        public override void AssignFrom(ConditionOperatorDef src)
        {
            if (src is ValueListExprDef)
            {
                Values.Text = ((ValueListExprDef)src).Values.Text;
            }
        }

        public override void Hide()
        {
            Values.Visible = false;
        }

        public List<string> GetValues()
        {
            var res = new List<string>();
            foreach (string line in Values.Text.Split('\n'))
            {
                if (!line.IsEmpty()) res.Add(line.Trim());
            }
            return res;
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            var sb = new StringBuilder();
            foreach (XmlElement x in xml.SelectNodes("Value")) sb.Append(x.InnerText + "\r\n");
            Values.Text = sb.ToString();
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            foreach (string val in GetValues())
            {
                xml.AddChild("Value").InnerText = val;
            }
        }

        public override bool RenderLeftSql { get { return false; } }

        public override void RenderSql(ConditionExpressionDesigner leftExpr, ISqlDumper dmp)
        {
            dmp.Put("(");
            bool was = false;
            foreach (string val in GetValues())
            {
                if (was) dmp.Put(" %k ", Conjuction);
                RenderItem(leftExpr, dmp, val);
                was = true;
            }
            if (!was) dmp.Put("1=1");
            dmp.Put(")");
        }

        protected abstract void RenderItem(ConditionExpressionDesigner leftExpr, ISqlDumper dmp, string item);
        protected virtual string Conjuction { get { return "OR"; } }
    }

    public class IsOneOfExprDef : ValueListExprDef
    {
        public IsOneOfExprDef(ConditionNodeBase node, ConditionOperatorFactory fact)
            : base(node, fact)
        {
        }
        protected override void RenderItem(ConditionExpressionDesigner leftExpr, ISqlDumper dmp, string item)
        {
            leftExpr.RenderSql(dmp);
            dmp.Put("=%v", item);
        }
    }
    public class IsOneOfExprDefFactory : ConditionOperatorFactory
    {
        public override ConditionOperatorDef CreateInstance(ConditionNodeBase node)
        {
            return new IsOneOfExprDef(node, this);
        }
    }


    public class IsNoneOfExprDef : ValueListExprDef
    {
        public IsNoneOfExprDef(ConditionNodeBase node, ConditionOperatorFactory fact)
            : base(node, fact)
        {
        }
        protected override void RenderItem(ConditionExpressionDesigner leftExpr, ISqlDumper dmp, string item)
        {
            leftExpr.RenderSql(dmp);
            dmp.Put("<>%v", item);
        }
        protected override string Conjuction { get { return "AND"; } }
    }
    public class IsNoneOfExprDefFactory : ConditionOperatorFactory
    {
        public override ConditionOperatorDef CreateInstance(ConditionNodeBase node)
        {
            return new IsNoneOfExprDef(node, this);
        }
    }

    public class ContainsOneOfExprDef : ValueListExprDef
    {
        public ContainsOneOfExprDef(ConditionNodeBase node, ConditionOperatorFactory fact)
            : base(node, fact)
        {
        }
        protected override void RenderItem(ConditionExpressionDesigner leftExpr, ISqlDumper dmp, string item)
        {
            leftExpr.RenderSql(dmp);
            dmp.Put("^like %v", "%" + item + "%");
        }
    }
    public class ContainsOneOfExprDefFactory : ConditionOperatorFactory
    {
        public override ConditionOperatorDef CreateInstance(ConditionNodeBase node)
        {
            return new ContainsOneOfExprDef(node, this);
        }
    }

    public class ContainsNoneOfExprDef : ValueListExprDef
    {
        public ContainsNoneOfExprDef(ConditionNodeBase node, ConditionOperatorFactory fact)
            : base(node, fact)
        {
        }
        protected override void RenderItem(ConditionExpressionDesigner leftExpr, ISqlDumper dmp, string item)
        {
            leftExpr.RenderSql(dmp);
            dmp.Put("^not ^like %v", "%" + item + "%");
        }
        protected override string Conjuction { get { return "AND"; } }
    }
    public class ContainsNoneOfExprDefFactory : ConditionOperatorFactory
    {
        public override ConditionOperatorDef CreateInstance(ConditionNodeBase node)
        {
            return new ContainsNoneOfExprDef(node, this);
        }
    }
}
