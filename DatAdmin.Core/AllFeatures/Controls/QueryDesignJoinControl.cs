using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;

namespace DatAdmin
{
    public class QueryDesignJoinControl : Control
    {
        QueryJoinOperator m_operator;

        [XmlElem]
        public QueryJoinOperator Operator
        {
            get { return m_operator; }
            set { m_operator = value; }
        }
        QueryJoinType m_joinType = QueryJoinType.Inner;

        [XmlElem]
        public QueryJoinType JoinType
        {
            get { return m_joinType; }
            set { m_joinType = value; }
        }
        QueryDesignTableFrame m_leftTable;

        public QueryDesignTableFrame LeftTable
        {
            get { return m_leftTable; }
            //set { m_leftTable = value; }
        }
        QueryDesignTableFrame m_rightTable;

        public QueryDesignTableFrame RightTable
        {
            get { return m_rightTable; }
            //set { m_rightTable = value; }
        }
        string m_leftCol;
        private System.ComponentModel.IContainer components;

        [XmlElem]
        public string LeftCol
        {
            get { return m_leftCol; }
            set { m_leftCol = value; }
        }
        string m_rightCol;

        [XmlElem]
        public string RightCol
        {
            get { return m_rightCol; }
            set { m_rightCol = value; }
        }

        [XmlElem]
        public int XPos
        {
            get { return Left; }
            set { Left = value; }
        }

        [XmlElem]
        public int YPos
        {
            get { return Top; }
            set { Top = value; }
        }

        [XmlElem]
        public string LeftTableSaveId
        {
            get { return m_leftTable.SaveId; }
            set
            {
                m_leftTable = m_frame.FindTableFromSaveId(value);
            }
        }

        [XmlElem]
        public string RightTableSaveId
        {
            get { return m_rightTable.SaveId; }
            set
            {
                m_rightTable = m_frame.FindTableFromSaveId(value);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap img = m_joinType.GetImage();
            string txt = m_operator.GetOpName();
            e.Graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, Width, Height));
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, Width - 1, Height-1));
            e.Graphics.DrawImage(img, new Point(Width / 2 - img.Width / 2, Height - img.Height - 2));
            SizeF tsize = e.Graphics.MeasureString(txt, Font);
            e.Graphics.DrawString(txt, Font, Brushes.Black, new RectangleF(Width / 2 - tsize.Width / 2, 1, tsize.Width, tsize.Height));
        }

        QueryDesignFrame m_frame;

        public QueryDesignJoinControl(QueryDesignFrame frame, QueryDesignTableFrame tbl1, string col1, QueryDesignTableFrame tbl2, string col2)
        {
            Initialize();
            m_leftTable = tbl1;
            m_leftCol = col1;
            m_rightTable = tbl2;
            m_rightCol = col2;
            m_frame = frame;
        }

        public QueryDesignJoinControl(XmlElement xml, QueryDesignFrame frame)
        {
            Initialize();
            m_frame = frame;
            this.LoadPropertiesCore(xml);
        }

        private void Initialize()
        {
            Width = 26;
            Height = 32;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            ContextMenuStripEx menu = new ContextMenuStripEx();
            MenuBuilder mb = new MenuBuilder();
            mb.AddObject(this);
            mb.GetMenuItems(menu.Items);
            menu.Show(PointToScreen(new Point(0, Height)));
        }

        [PopupMenu("s_remove")]
        public void RemoveJoin()
        {
            m_frame.RemoveJoin(this);
        }

        [PopupMenu("s_join_type/LEFT JOIN")]
        public void SwitchLeftJoin()
        {
            m_joinType = QueryJoinType.Left;
            Invalidate();
        }

        [PopupMenu("s_join_type/RIGHT JOIN")]
        public void SwitchRightJoin()
        {
            m_joinType = QueryJoinType.Right;
            Invalidate();
        }

        [PopupMenu("s_join_type/INNER JOIN")]
        public void SwitchInnerJoin()
        {
            m_joinType = QueryJoinType.Inner;
            Invalidate();
        }

        [PopupMenu("s_join_type/FULL OUTER JOIN")]
        public void SwitchFullJoin()
        {
            m_joinType = QueryJoinType.Full;
            Invalidate();
        }

        [PopupMenu("s_operator/=")]
        public void SwitchOpEq()
        {
            m_operator = QueryJoinOperator.eq;
            Invalidate();
        }

        [PopupMenu("s_operator/<>")]
        public void SwitchOpNe()
        {
            m_operator = QueryJoinOperator.ne;
            Invalidate();
        }

        [PopupMenu("s_operator/<")]
        public void SwitchOpLt()
        {
            m_operator = QueryJoinOperator.lt;
            Invalidate();
        }

        [PopupMenu("s_operator/>")]
        public void SwitchOpGt()
        {
            m_operator = QueryJoinOperator.gt;
            Invalidate();
        }

        [PopupMenu("s_operator/<=")]
        public void SwitchOpLe()
        {
            m_operator = QueryJoinOperator.le;
            Invalidate();
        }

        [PopupMenu("s_operator/>=")]
        public void SwitchOpGe()
        {
            m_operator = QueryJoinOperator.ge;
            Invalidate();
        }

        public void Save(XmlElement xml)
        {
            this.SavePropertiesCore(xml);
        }
    }

    public enum QueryJoinOperator
    {
        eq, ne, lt, gt, le, ge
    }
    public enum QueryJoinType
    {
        Left, Right, Inner, Full
    }
    public static class QueryJoinExtension
    {
        public static Bitmap GetImage(this QueryJoinType type)
        {
            switch (type)
            {
                case QueryJoinType.Left: return CoreIcons.leftjoin;
                case QueryJoinType.Right: return CoreIcons.rightjoin;
                case QueryJoinType.Inner: return CoreIcons.innerjoin;
                case QueryJoinType.Full: return CoreIcons.fulljoin;
            }
            throw new Exception();
        }
        public static string GetSqlName(this QueryJoinType type)
        {
            switch (type)
            {
                case QueryJoinType.Left: return "LEFT JOIN";
                case QueryJoinType.Right: return "RIGHT JOIN";
                case QueryJoinType.Inner: return "INNER JOIN";
                case QueryJoinType.Full: return "OUTER JOIN";
            }
            throw new Exception();
        }
        public static string GetOpName(this QueryJoinOperator op)
        {
            switch (op)
            {
                case QueryJoinOperator.eq: return "=";
                case QueryJoinOperator.ne: return "<>";
                case QueryJoinOperator.le: return "<=";
                case QueryJoinOperator.ge: return ">=";
                case QueryJoinOperator.lt: return "<";
                case QueryJoinOperator.gt: return ">";
            }
            throw new Exception();
        }
    }
}
