using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;

namespace DatAdmin
{
    public partial class QueryDesignFrame : UserControl
    {
        List<QueryDesignTableFrame> m_tables = new List<QueryDesignTableFrame>();
        List<QueryDesignJoinControl> m_joins = new List<QueryDesignJoinControl>();
        bool m_loading;
        QueryFrame m_queryFrame;
        ControlInvoker m_invoker;
        List<NameWithSchema> m_availableTableNames;
        List<NameWithSchema> m_availableViewNames;
        bool m_changedConnection;
        ConditionDesigner m_condDesign;

        public QueryDesignFrame(QueryFrame queryFrame)
        {
            InitializeComponent();
            m_queryFrame = queryFrame;
            m_condDesign = new ConditionDesigner(tabFilter, this);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            m_invoker = new ControlInvoker(this);
            if (m_changedConnection)
            {
                RefreshTables();
            }
        }

        [XmlElem]
        public bool IsUniqueRows
        {
            get { return btnUniqueRows.Checked; }
            set { btnUniqueRows.Checked = value; }
        }

        private IPhysicalConnection Connection
        {
            get { return m_queryFrame.Connection; }
        }

        public void NotifyChangedConnection()
        {
            if (!IsHandleCreated)
            {
                m_changedConnection = true;
            }
            else
            {
                RefreshTables();
            }
            if (m_queryFrame != null)
            {
                codeEditor1.Dialect = m_queryFrame.Connection.GetAnyDialect();
            }
        }

        void PlaceTable(QueryDesignTableFrame table)
        {
            int vsplit = 30, hsplit = 30;
            int ymax;
            try
            {
                ymax = (from t in m_tables select t.Bottom).Max();
            }
            catch
            {
                ymax = 0;
            }
            int y1;
            try
            {
                y1 = (from t in m_tables where t.Bottom == ymax select t.Top).Max();
            }
            catch
            {
                y1 = vsplit;
            }
            Interval yint = new Interval(y1, y1 + table.Size.Height);
            int hspace = hsplit;
            int newleft = hspace;
            int maxwi = panel1.Width;
            foreach (var t in m_tables)
            {
                if (!Interval.Intersection(new Interval(t.Top, t.Bottom), yint).IsEmpty) newleft = Math.Max(newleft, t.Right + hspace);
            }
            if (newleft + table.Size.Width > maxwi)
            {
                table.Left = hspace;
                table.Top = ymax + vsplit;
            }
            else
            {
                table.Left = newleft;
                table.Top = y1;
            }
        }

        void AddTable(ITableStructure ts, QueryDesignTableFrame srctbl, IForeignKey join, string objtype)
        {
            try
            {
                QueryDesignTableFrame tbl = new QueryDesignTableFrame(ts, this, objtype);

                if (join != null)
                {
                    for (int i = 0; i < join.Columns.Count; i++)
                    {
                        if (srctbl.FullName == join.Table.FullName)
                        {
                            AddJoin(srctbl, join.Columns[i].ColumnName, tbl, join.PrimaryKeyColumns[i].ColumnName, false);
                        }
                        else
                        {
                            AddJoin(srctbl, join.PrimaryKeyColumns[i].ColumnName, tbl, join.Columns[i].ColumnName, false);
                        }
                    }
                }
                else
                {
                    // add implicit joins
                    foreach (var fk in ts.GetConstraints<IForeignKey>())
                    {
                        foreach (var t in m_tables)
                        {
                            if (t.FullName == fk.PrimaryKeyTable)
                            {
                                for (int index = 0; index < fk.Columns.Count; index++)
                                {
                                    AddJoin(tbl, fk.Columns[index].ColumnName, t, fk.PrimaryKeyColumns[index].ColumnName, false);
                                }
                            }
                        }
                    }
                    foreach (var t in m_tables)
                    {
                        foreach (var fk in t.Structure.GetConstraints<IForeignKey>())
                        {
                            if (tbl.FullName == fk.PrimaryKeyTable)
                            {
                                for (int index = 0; index < fk.Columns.Count; index++)
                                {
                                    AddJoin(t, fk.Columns[index].ColumnName, tbl, fk.PrimaryKeyColumns[index].ColumnName, false);
                                }
                            }
                        }
                    }
                }

                PlaceTable(tbl);
                m_tables.Add(tbl);
                panel1.Controls.Add(tbl);
            }
            catch (Exception e)
            {
                Errors.Report(e);
            }
            ReloadAll();
        }

        void AddTable(ITableSource table)
        {
            try
            {
                ITableStructure ts = table.InvokeLoadStructure(TableStructureMembers.All);
                AddTable(ts, null, null, table is GenericViewAsTableSource ? "view" : "table");
            }
            catch (Exception e)
            {
                Errors.Report(e);
            }
        }

        public ITableStructure LoadTable(FullDatabaseRelatedName tname)
        {
            if (tname.ObjectType == "table")
            {
                var dbmem = new DatabaseStructureMembers
                {
                    TableMembers = TableStructureMembers.All,
                    TableFilter = new List<NameWithSchema> { tname.ObjectName }
                };
                var dbs = Connection.InvokeLoadStructure(null, dbmem, null);
                return dbs.Tables[tname.ObjectName];
            }
            if (tname.ObjectType == "view")
            {
                var dbmem = new DatabaseStructureMembers
                {
                    ViewAsTables = true,
                    ViewAsTableFilter = new List<NameWithSchema> { tname.ObjectName }
                };
                var dbs = Connection.InvokeLoadStructure(null, dbmem, null);
                return dbs.ViewAsTables[tname.ObjectName];
            }
            throw new NotImplementedError("DAE-00370");
        }

        public void AddTable(NameWithSchema tname, QueryDesignTableFrame srctbl, IForeignKey join)
        {
            AddTable(new FullDatabaseRelatedName { ObjectName = tname, ObjectType = "table" });
        }

        public void AddTable(FullDatabaseRelatedName tname, QueryDesignTableFrame srctbl, IForeignKey join)
        {
            var ts = LoadTable(tname);
            AddTable(ts, srctbl, join, tname.ObjectType);
        }

        public void AddTable(FullDatabaseRelatedName tname)
        {
            AddTable(tname, null, null);
        }

        private void ReloadAll()
        {
            labDragAndDrop.Visible = m_tables.Count == 0;
            ReflectChanges();
            FillSortColumns();
            Redraw();
        }

        public void RemoveTable(QueryDesignTableFrame table)
        {
            m_tables.Remove(table);
            table.Dispose();
            List<QueryDesignJoinControl> joins = new List<QueryDesignJoinControl>();
            joins.AddRange(from j in m_joins where j.LeftTable == table || j.RightTable == table select j);
            m_joins.RemoveAll(j => joins.Contains(j));
            var todel = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dataGridColumns.Rows) if (row.Tag == table) todel.Add(row);
            foreach (var row in todel) dataGridColumns.Rows.Remove(row);
            foreach (var j in joins) j.Dispose();
            ReloadAll();
        }

        public void AddJoin(QueryDesignTableFrame tbl1, string col1, QueryDesignTableFrame tbl2, string col2, bool refresh)
        {
            QueryDesignJoinControl join = new QueryDesignJoinControl(this, tbl1, col1, tbl2, col2);
            m_joins.Add(join);
            panel1.Controls.Add(join);
            if (refresh)
            {
                Redraw();
                ReflectChanges();
            }
        }

        public void Redraw()
        {
            panel1.Invalidate();
        }

        private void PlaceJoin(QueryDesignJoinControl join, Point a, Point b)
        {
            int x = (a.X + b.X) / 2 - join.Width / 2;
            int y = (a.Y + b.Y) / 2 - join.Height / 2;
            if (x != join.Left || y != join.Top)
            {
                join.Left = x;
                join.Top = y;
            }
        }

        private void DrawJoin(Graphics g, QueryDesignJoinControl join)
        {
            Pen pen = Pens.Gray;
            int extwi = 40;
            if (join.LeftTable.Left < join.RightTable.Left)
            {
                Point src = new Point(join.LeftTable.Right, join.LeftTable.Top + join.LeftTable.GetColY(join.LeftCol));
                Point dst = new Point(join.RightTable.Left, join.RightTable.Top + join.RightTable.GetColY(join.RightCol));
                g.DrawLine(pen, src.X, src.Y, src.X + extwi, src.Y);
                g.DrawLine(pen, src.X + extwi, src.Y, dst.X - extwi, dst.Y);
                g.DrawLine(pen, dst.X - extwi, dst.Y, dst.X, dst.Y);

                PlaceJoin(join, src, dst);
            }
            else
            {
                Point src = new Point(join.LeftTable.Left, join.LeftTable.Top + join.LeftTable.GetColY(join.LeftCol));
                Point dst = new Point(join.RightTable.Right, join.RightTable.Top + join.RightTable.GetColY(join.RightCol));
                g.DrawLine(pen, src.X, src.Y, src.X - extwi, src.Y);
                g.DrawLine(pen, src.X - extwi, src.Y, dst.X + extwi, dst.Y);
                g.DrawLine(pen, dst.X + extwi, dst.Y, dst.X, dst.Y);

                PlaceJoin(join, src, dst);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var j in m_joins) DrawJoin(e.Graphics, j);
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            DragObjectContainer obj = (DragObjectContainer)e.Data.GetData(typeof(DragObjectContainer));
            if (obj != null)
            {
                var appobjs = obj.Data as AppObject[];
                if (appobjs != null)
                {
                    foreach (var appobj in appobjs)
                    {
                        var tsrc = appobj.TableSource;
                        if (tsrc != null) AddTable(tsrc);
                        //var tbl = appobj as TableAppObject;
                        //if (tbl != null)
                        //{
                        //    AddTable(tbl.DbObjectName);
                        //}
                    }
                }
            }
            var name = (FullDatabaseRelatedName)e.Data.GetData(typeof(FullDatabaseRelatedName));
            if (name != null)
            {
                AddTable(name);
            }
        }

        public void RefreshTables()
        {
            Connection.BeginInvoke((Action)DoRefreshTables, m_invoker.CreateInvokeCallback(RefreshedTables));
        }

        private void DoRefreshTables()
        {
            var dbmem = new DatabaseStructureMembers { TableList = true };
            dbmem.SpecificObjectOverride["view"] = new SpecificObjectMembers();
            dbmem.SpecificObjectOverride["view"].ObjectList = true;
            var dbs = Connection.Dialect.AnalyseDatabase(Connection, null, dbmem, null);

            m_availableTableNames = new List<NameWithSchema>();
            foreach (var ts in dbs.Tables) m_availableTableNames.Add(ts.FullName);
            m_availableTableNames.Sort();

            m_availableViewNames = new List<NameWithSchema>();
            if (dbs.SpecificObjects.ContainsKey("view"))
            {
                foreach (var ts in dbs.SpecificObjects["view"]) m_availableViewNames.Add(ts.ObjectName);
            }
            m_availableViewNames.Sort();
        }

        private void RefreshedTables(IAsyncResult async)
        {
            try
            {
                Connection.EndInvoke(async);
            }
            catch (Exception err)
            {
                Errors.Report(err);
            }

            lbxAvailableTables.Items.Clear();
            if (m_availableTableNames != null)
            {
                foreach (var tbl in m_availableTableNames)
                {
                    lbxAvailableTables.Items.Add(new FullDatabaseRelatedName { ObjectName = tbl, ObjectType = "table" });
                }
            }

            lbxAvailableViews.Items.Clear();
            if (m_availableViewNames != null)
            {
                foreach (var tbl in m_availableViewNames)
                {
                    lbxAvailableViews.Items.Add(new FullDatabaseRelatedName { ObjectName = tbl, ObjectType = "view" });
                }
            }
        }

        private void panel1_DragOver(object sender, DragEventArgs e)
        {
            DragObjectContainer obj = (DragObjectContainer)e.Data.GetData(typeof(DragObjectContainer));
            if (obj != null)
            {
                var appobjs = obj.Data as AppObject[];
                foreach (var appobj in appobjs)
                {
                    if (appobj.TableSource != null)
                    {
                        e.Effect = DragDropEffects.Copy;
                        break;
                    }
                }
            }
            if (e.Data.GetData(typeof(FullDatabaseRelatedName)) != null) e.Effect = DragDropEffects.Copy;
        }


        public void RemoveJoin(QueryDesignJoinControl join)
        {
            m_joins.Remove(join);
            join.Dispose();
            ReloadAll();
        }

        public void AddColumn(QueryDesignTableFrame table, string col)
        {
            dataGridColumns.Rows.Add(col, "", table.GetNameTitle(), true, "-", "", "", "");
            dataGridColumns.Rows[dataGridColumns.Rows.Count - 1].Tag = table;
            FillSortColumns();
            ReloadAll();
        }

        //private DataGridViewRow FindColumn(NameWithSchema table, string col)
        //{
        //    foreach (DataGridViewRow row in dataGridColumns.Rows)
        //    {
        //        if (row.Cells[0].ToString() == col && row.Cells[2].ToString() == table.ToString())
        //        {
        //            return row;
        //        }
        //    }
        //    return null;
        //}

        public void RemoveColumn(QueryDesignTableFrame table, string col)
        {
            DataGridViewRow row = FindColumn(table, col);
            if (row != null) dataGridColumns.Rows.Remove(row);
            ReloadAll();
        }

        private void FillSortColumns()
        {
            lbxSortAvailable.Items.Clear();
            foreach (DesignedColumn col in Columns)
            {
                lbxSortAvailable.Items.Add(col);
            }
            RemoveInvalidSortColumns();

            //foreach (var t in Tables)
            //{
            //    foreach (var c in t.Columns)
            //    {
            //        lbxSortAvailable.Items.Add(c);
            //    }
            //}
        }

        private void RemoveInvalidSortColumns()
        {
            var remove = new List<DesignedOrder>();
            foreach (DesignedOrder o in lbxSortedColumns.Items)
            {
                var crow = FindColumn(o.m_column.Table.m_table, o.m_column.ColumnName);
                if (crow == null)
                {
                    remove.Add(o);
                    continue;
                }
                var col = new DesignedColumn(this, crow);
                if (!col.Output)
                {
                    remove.Add(o);
                    continue;
                }
            }
            foreach (var o in remove)
            {
                lbxSortedColumns.Items.Remove(o);
            }
        }

        public void ReflectChanges()
        {
            RemoveInvalidSortColumns();
            codeEditor1.Text = GenerateSql();
            Redraw();
            if (m_condDesign != null) m_condDesign.ReflectChanges();
        }

        private void RenderFrom(ISqlDumper dmp)
        {
            dmp.Put("&n^from&>&n");

            var restTables = new List<DesignedTable>();
            restTables.AddRange(Tables);

            bool wascomp = false;
            while (restTables.Count > 0)
            {
                var tbl = restTables[0];
                restTables.RemoveAt(0);
                // komponenta souvislosti, ve ktere je tabulka tbl
                var comp = new List<DesignedTable>();
                comp.Add(tbl);
                for (; ; )
                {
                    DesignedTable added = null;
                    foreach (var t in comp)
                    {
                        foreach (var n in t.Neightboors)
                        {
                            if (!comp.Contains(n))
                            {
                                added = n;
                            }
                        }
                    }
                    if (added != null)
                    {
                        comp.Add(added);
                        restTables.Remove(added);
                    }
                    else
                    {
                        break;
                    }
                }

                var rendered = new List<DesignedTable>();

                var first = comp[0];
                comp.RemoveAt(0);
                if (wascomp) dmp.Put(",&n");
                else dmp.Put("&n");
                first.WriteDefine(dmp);
                rendered.Add(first);

                while (comp.Count > 0)
                {
                    DesignedTable curt = null;
                    // vybereme tabulku, ktera je spojena hranou s nejakou uz vyrenderovanou tabulkou
                    foreach (var t in comp)
                    {
                        foreach (var n in t.Neightboors)
                        {
                            if (rendered.Contains(n))
                            {
                                curt = t;
                                break;
                            }
                        }
                        if (curt != null) break;
                    }
                    if (curt == null) throw new InternalError("DAE-00187 public error");
                    comp.Remove(curt);
                    dmp.Put("&n");
                    List<DesignedJoin> joins = GetJoins(rendered, new List<DesignedTable> { curt });

                    rendered.Add(curt);

                    dmp.Put(" ");
                    dmp.Put("%k", joins[0].JoinType.GetSqlName());
                    dmp.Put(" ");
                    curt.WriteDefine(dmp);
                    dmp.Put(" ^on ");
                    bool wasjoin = false;
                    foreach (var j in joins)
                    {
                        if (wasjoin) dmp.Put(" ^and ");
                        j.LeftCol.WriteExpression(dmp);
                        dmp.Put("%k", j.Operator.GetOpName());
                        j.RightCol.WriteExpression(dmp);
                        wasjoin = true;
                    }
                }
                wascomp = true;
            }
            dmp.Put("&<");
        }

        public string GenerateSql()
        {
            if (m_queryFrame == null) return "";
            var dialect = m_queryFrame.Connection.GetAnyDialect();
            var sw = new StringWriter();
            var dmp = dialect.CreateDumper(sw);

            dmp.Put("^select");
            if (IsUniqueRows) dmp.Put(" ^distinct");

            //StringBuilder sb = new StringBuilder();
            //sb.Append("SELECT");
            //if (IsUniqueRows) sb.Append(" DISTINCT");
            bool wascol = false;
            var groupBy = new List<DesignedColumn>();
            dmp.Put("&>");
            foreach (DesignedColumn col in Columns)
            {
                if (col.GroupBy) groupBy.Add(col);
                if (!col.Output) continue;
                if (wascol) dmp.Put(",&n");
                else dmp.Put("&n");
                if (col.Aggregate != null) dmp.Put(col.Aggregate + "(");
                col.WriteExpression(dmp);
                if (col.Aggregate != null) dmp.Put(")");
                if (col.Alias != "") dmp.Put(" ^as %s", col.Alias);
                wascol = true;
            }
            dmp.Put("&<");
            RenderFrom(dmp);

            if (m_condDesign != null) m_condDesign.RenderSql(dmp);

            if (groupBy.Count > 0)
            {
                bool wasg = false;
                dmp.Put("&n^group ^by");
                foreach (DesignedColumn col in groupBy)
                {
                    if (wasg) dmp.Put(", ");
                    col.WriteExpression(dmp);
                    wasg = true;
                }
            }

            if (lbxSortedColumns.Items.Count > 0)
            {
                dmp.Put("&n^order ^by ");
                bool waso = false;
                foreach (DesignedOrder o in lbxSortedColumns.Items)
                {
                    if (waso) dmp.Put(", ");
                    var col = new DesignedColumn(this, FindColumn(o.m_column.Table.m_table, o.m_column.ColumnName));
                    if (col.Aggregate != null) dmp.Put(col.Aggregate + "(");
                    o.m_column.WriteExpression(dmp);
                    if (col.Aggregate != null) dmp.Put(")");
                    if (o.Desc) dmp.Put(" ^desc");
                    waso = true;
                }
            }

            return sw.ToString();
        }

        DataGridViewRow FindColumn(QueryDesignTableFrame table, string col)
        {
            foreach (DataGridViewRow row in dataGridColumns.Rows)
            {
                if (row.Tag == table && row.Cells[0].Value.ToString() == col) return row;
            }
            return null;
        }

        private List<DesignedJoin> GetJoins(List<DesignedTable> group1, List<DesignedTable> group2)
        {
            var res = new List<DesignedJoin>();
            foreach (var j in m_joins)
            {
                if (group1.Contains(j.LeftTable.Designed) && group2.Contains(j.RightTable.Designed)) res.Add(new DesignedJoin(this, j));
                else if (group2.Contains(j.LeftTable.Designed) && group1.Contains(j.RightTable.Designed)) res.Add(new DesignedJoin(this, j));
            }
            return res;
        }

        public IEnumerable<DesignedColumn> Columns
        {
            get { foreach (DataGridViewRow col in dataGridColumns.Rows) yield return new DesignedColumn(this, col); }
        }
        public IEnumerable<DesignedTable> Tables
        {
            get { foreach (QueryDesignTableFrame tbl in m_tables) yield return new DesignedTable(this, tbl); }
        }

        public class ColWrapper
        {
            protected string m_colname;
            public QueryDesignFrame m_frame;
            public QueryDesignTableFrame m_table;
            public ColWrapper(QueryDesignFrame frame, QueryDesignTableFrame table, string colname)
            {
                m_frame = frame;
                m_colname = colname;
                m_table = table;
            }
            public string Name { get { return m_colname; } }
            public DesignedTable Table { get { return new DesignedTable(m_frame, m_table); } }
            //public string GetExpression()
            //{
            //    StringBuilder sb = new StringBuilder();
            //    WriteExpression(sb);
            //    return sb.ToString();
            //}
            public void WriteExpression(ISqlDumper dmp)
            {
                if (!Table.Alias.IsEmpty())
                {
                    dmp.Put("%i", Table.Alias);
                }
                else
                {
                    dmp.Put("%f", Table.FullName);
                }
                dmp.Put(".%i", Name);
            }
            public override string ToString()
            {
                return Table.m_table.GetNameTitle() + "." + Name;
            }
            public string ColumnName
            {
                get { return m_colname; }
            }
        }

        public class DesignedOrder
        {
            public ColWrapper m_column;
            private bool m_desc;

            [XmlElem]
            public bool Desc
            {
                get { return m_desc; }
                set { m_desc = value; }
            }

            public DesignedOrder(ColWrapper column, bool desc)
            {
                m_column = column;
                m_desc = desc;
            }
            public override string ToString()
            {
                return m_column.ToString() + (m_desc ? " DESC" : " ASC");
            }
            public void Save(XmlElement xml)
            {
                xml.AddChild("TableSaveId").InnerText = m_column.Table.m_table.SaveId;
                xml.AddChild("Column").InnerText = m_column.ColumnName;
                this.SavePropertiesCore(xml);
            }
            public DesignedOrder(XmlElement xml, QueryDesignFrame frame)
            {
                var table = frame.FindTableFromSaveId(xml.FindElement("TableSaveId").InnerText);
                m_column = new ColWrapper(frame, table, xml.FindElement("Column").InnerText);
                this.LoadPropertiesCore(xml);
            }
        }

        public class DesignedColumn : ColWrapper
        {
            DataGridViewRow m_row;
            public DesignedColumn(QueryDesignFrame frame, DataGridViewRow row)
                : base(frame, (QueryDesignTableFrame)row.Tag, row.Cells[0].Value.SafeToString())
            {
                m_row = row;
                m_frame = frame;
            }
            public DesignedColumn(QueryDesignFrame frame, DataGridViewRow row, XmlElement xml)
                : base(frame, 
                frame.FindTableFromSaveId(xml.SelectSingleNode("TableSaveId").InnerText),
                xml.SelectSingleNode("ColumnName").InnerText)
            {
                m_row = row;
                m_frame = frame;
                m_row.Tag = m_table;
                m_row.Cells[0].Value = m_colname;
                m_row.Cells[2].Value = m_table.GetNameTitle();
                Load(xml);
            }
            [XmlElem]
            public string Alias
            {
                get { return m_row.Cells[1].Value.SafeToString(); }
                set { m_row.Cells[1].Value = value; }
            }
            [XmlElem]
            public bool Output
            {
                get { return (bool)m_row.Cells[3].Value; }
                set { m_row.Cells[3].Value = value; }
            }

            public bool GroupBy { get { return m_row.Cells[4].Value.ToString() == "GROUP BY"; } }

            public string TableTitle
            {
                get { return m_row.Cells[2].Value.SafeToString(); }
                set { m_row.Cells[2].Value = value; }
            }

            public string Aggregate
            {
                get
                {
                    string res = m_row.Cells[4].Value.ToString();
                    if (res != "-" && res != "GROUP BY") return res;
                    return null;
                }
            }
            [XmlElem("Aggregate")]
            public string XmlAggregate
            {
                get { return m_row.Cells[4].Value.SafeToString(); }
                set { m_row.Cells[4].Value = value; }
            }

            public void Save(XmlElement xml)
            {
                this.SavePropertiesCore(xml);
                xml.AddChild("TableSaveId").InnerText = m_table.SaveId;
                xml.AddChild("ColumnName").InnerText = m_colname;
            }

            public void Load(XmlElement xml)
            {
                this.LoadPropertiesCore(xml);
            }

            public override string ToString()
            {
                if (!Alias.IsEmpty()) return Alias;
                return base.ToString();
            }
        }

        public class DesignedTable
        {
            public QueryDesignTableFrame m_table;
            QueryDesignFrame m_frame;
            public DesignedTable(QueryDesignFrame frame, QueryDesignTableFrame table)
            {
                m_table = table;
                m_frame = frame;
            }
            public NameWithSchema FullName { get { return m_table.FullName; } }
            public string Alias { get { return m_table.Alias; } }
            public string AliasOrName { get { return m_table.AliasOrName; } }

            public bool IsJoined
            {
                get
                {
                    foreach (var j in m_frame.m_joins)
                    {
                        if (j.LeftTable == m_table) return true;
                        if (j.RightTable == m_table) return true;
                    }
                    return false;
                }
            }
            public IEnumerable<DesignedTable> Neightboors
            {
                get
                {
                    foreach (var j in m_frame.m_joins)
                    {
                        if (j.LeftTable == m_table) yield return new DesignedTable(m_frame, j.RightTable);
                        if (j.RightTable == m_table) yield return new DesignedTable(m_frame, j.LeftTable);
                    }
                }
            }
            public void WriteDefine(ISqlDumper dmp)
            {
                dmp.Put("%f", FullName);
                if (!Alias.IsEmpty()) dmp.Put(" %i", Alias);
            }
            public IEnumerable<ColWrapper> Columns
            {
                get
                {
                    foreach (IColumnStructure col in m_table.Structure.Columns)
                    {
                        yield return new ColWrapper(m_frame, m_table, col.ColumnName);
                    }
                }
            }

            public override int GetHashCode()
            {
                return m_table.GetHashCode();
            }
            public override bool Equals(object obj)
            {
                if (obj is DesignedTable) return m_table == ((DesignedTable)obj).m_table;
                return base.Equals(obj);
            }
            public override string ToString()
            {
                return m_table.FullName.ToString();
            }
        }

        public class DesignedJoin
        {
            public QueryDesignJoinControl m_join;
            QueryDesignFrame m_frame;
            public DesignedJoin(QueryDesignFrame frame, QueryDesignJoinControl join)
            {
                m_join = join;
                m_frame = frame;
            }

            public ColWrapper LeftCol { get { return new ColWrapper(m_frame, m_join.LeftTable, m_join.LeftCol); } }
            public ColWrapper RightCol { get { return new ColWrapper(m_frame, m_join.RightTable, m_join.RightCol); } }
            public QueryJoinType JoinType { get { return m_join.JoinType; } }
            public QueryJoinOperator Operator { get { return m_join.Operator; } }
        }

        public event EventHandler ShowSqlClick;
        public event EventHandler RunSqlClick;

        private void btnShowSql_Click(object sender, EventArgs e)
        {
            if (ShowSqlClick != null) ShowSqlClick(this, e);
        }

        private void btnRunSql_Click(object sender, EventArgs e)
        {
            if (RunSqlClick != null) RunSqlClick(this, e);
        }

        private void dataGridColumns_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (m_loading) return;

            if (e.ColumnIndex >= 5 && e.ColumnIndex <= 7 && e.RowIndex >= 0)
            {
                dataGridColumns.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = FixCondition(dataGridColumns.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.SafeToString());
            }

            ReflectChanges();
        }

        private void btnClearQuery_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Texts.Get("s_really_clear_query"), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            foreach (var j in m_joins) j.Dispose();
            foreach (var t in m_tables) t.Dispose();
            dataGridColumns.Rows.Clear();
            m_joins.Clear();
            m_tables.Clear();
            lbxSortedColumns.Items.Clear();
            ReloadAll();
        }

        private void btnAddToSort_Click(object sender, EventArgs e)
        {
            if (lbxSortAvailable.SelectedItem != null) lbxSortedColumns.Items.Add(new DesignedOrder((ColWrapper)lbxSortAvailable.SelectedItem, false));
            ReflectChanges();
        }

        private void btnRemoveFromSort_Click(object sender, EventArgs e)
        {
            if (lbxSortedColumns.SelectedItem != null) lbxSortedColumns.Items.Remove(lbxSortedColumns.SelectedItem);
            ReflectChanges();
        }

        private void btnSortUp_Click(object sender, EventArgs e)
        {
            lbxSortedColumns.MoveSelectedUp();
            ReflectChanges();
        }

        private void btnSortDown_Click(object sender, EventArgs e)
        {
            lbxSortedColumns.MoveSelectedDown();
            ReflectChanges();
        }

        private void btnSortOrder_Click(object sender, EventArgs e)
        {
            if (lbxSortedColumns.SelectedItem != null)
            {
                ((DesignedOrder)lbxSortedColumns.SelectedItem).Desc = !((DesignedOrder)lbxSortedColumns.SelectedItem).Desc;
                lbxSortedColumns.ReloadItemNames();
            }
            ReflectChanges();
        }

        private void btnUniqueRows_Click(object sender, EventArgs e)
        {
            ReflectChanges();
        }

        private void dataGridColumns_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mnuColumns.Items.Clear();
                MenuBuilder mb = new MenuBuilder();
                mb.AddObject(this);
                var hinfo = dataGridColumns.HitTest(e.X, e.Y);
                if (hinfo.Type == DataGridViewHitTestType.Cell)
                {
                    var cell = dataGridColumns.Rows[hinfo.RowIndex].Cells[hinfo.ColumnIndex];
                    if (!cell.Selected)
                    {
                        dataGridColumns.CurrentCell = cell;
                    }
                }
                mb.GetMenuItems(mnuColumns.Items);
                mnuColumns.Show(dataGridColumns, e.Location);
            }
        }

        [PopupMenu("s_move_up", ImageName = CoreIcons.up1Name, Weight = 1)]
        public void MoveColumnUp()
        {
            dataGridColumns.MoveRowUp();
        }

        [PopupMenu("s_move_down", ImageName = CoreIcons.down1Name, Weight = 2)]
        public void MoveColumnDown()
        {
            dataGridColumns.MoveRowDown();
        }

        public void Save(string file)
        {
            var doc = XmlTool.CreateDocument("QueryDesign");
            Save(doc.DocumentElement);
            doc.Save(file);
        }

        public void Save(XmlElement xml)
        {
            this.SavePropertiesCore(xml);
            foreach (var tbl in m_tables)
            {
                tbl.Save(xml.AddChild("Table"));
            }
            foreach (var join in m_joins)
            {
                join.Save(xml.AddChild("Join"));
            }
            foreach (DesignedColumn col in Columns)
            {
                col.Save(xml.AddChild("Column"));
            }
            foreach (DesignedOrder o in lbxSortedColumns.Items)
            {
                o.Save(xml.AddChild("OrderBy"));
            }
            m_condDesign.SaveToXml(xml.AddChild("Filter"));
        }

        public new void Load(string file)
        {
            var doc = new XmlDocument();
            doc.Load(file);
            Load(doc.DocumentElement);
        }

        public new void Load(XmlElement xml)
        {
            m_loading = true;
            this.LoadPropertiesCore(xml);
            foreach (XmlElement xtbl in xml.SelectNodes("Table"))
            {
                var tbl = new QueryDesignTableFrame(xtbl, this);
                m_tables.Add(tbl);
                panel1.Controls.Add(tbl);
            }
            foreach (XmlElement xjoin in xml.SelectNodes("Join"))
            {
                var join = new QueryDesignJoinControl(xjoin, this);
                m_joins.Add(join);
                panel1.Controls.Add(join);
            }
            foreach (XmlElement xcol in xml.SelectNodes("Column"))
            {
                int newrow = dataGridColumns.Rows.Add();
                var col = new DesignedColumn(this, dataGridColumns.Rows[newrow], xcol);
                col.Table.m_table.CheckColumn(col.ColumnName);
            }
            foreach (XmlElement xord in xml.SelectNodes("OrderBy"))
            {
                var ord = new DesignedOrder(xord, this);
                lbxSortedColumns.Items.Add(ord);
            }
            var cx = xml.FindElement("Filter");
            if (cx != null) m_condDesign.LoadFromXml(cx);
            m_loading = false;
            ReloadAll();
            Redraw();
        }

        public bool IsDesign
        {
            get
            {
                return m_tables.Count > 0;
            }
        }

        public QueryDesignTableFrame FindTableFromSaveId(string value)
        {
            return m_tables.First(t => t.SaveId == value);
        }

        public TabControl GetTabs()
        {
            return tabControl1;
        }

        public int BaseTabCount { get { return 5; } }
        public MessageLogFrame MessageLog { get { return messageLogFrame1; } }

        public void ShowMessagesTab()
        {
            tabControl1.SelectedIndex = 4;
        }

        public void FillTableNames()
        {
            foreach (var col in Columns)
            {
                col.TableTitle = col.Table.m_table.GetNameTitle();
            }
        }

        public bool CheckQuery(ILogger log)
        {
            bool res = true;

            // check whether output column exists
            bool iscol = false;
            foreach (var col in Columns)
            {
                if (col.Output) iscol = true;
            }
            if (!iscol)
            {
                log.Error(Texts.Get("s_query_has_not_output_columns"));
                res = false;
            }

            bool isgrouped = false;
            // check group-by
            foreach (var col in Columns)
            {
                if (col.GroupBy) isgrouped = true;
            }
            if (isgrouped)
            {
                foreach (var col in Columns)
                {
                    if (col.Output && !col.GroupBy && col.Aggregate == null)
                    {
                        log.Error(Texts.Get("s_column_must_have_group_by_or_aggregate$column", "column", col.ToString()));
                        res = false;
                    }
                }
            }

            return res;
        }

        private void btnRefreshTables_Click(object sender, EventArgs e)
        {
            RefreshTables();
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            var tname = (FullDatabaseRelatedName)(tabControl2.SelectedIndex == 0 ? lbxAvailableTables : lbxAvailableViews).SelectedItem;
            if (tname == null) return;
            AddTable(tname);
        }

        private void lbxAvailableTables_MouseDown(object sender, MouseEventArgs e)
        {
            if (lbxAvailableTables.SelectedItem == null) return;
            lbxAvailableTables.DoDragDrop(lbxAvailableTables.SelectedItem, DragDropEffects.Copy);
            if (e.Clicks == 2) btnAddTable_Click(sender, e);
        }

        private void btnShowTables_Click(object sender, EventArgs e)
        {
            panelTables.Visible = btnShowTables.Checked;
        }

        Regex NUMRE = new Regex(@"^\d+([,\.]\d+)?$");
        private string FixCondition(string cond)
        {
            if (cond == null) return cond;
            string ctest = cond.ToLower().Trim();
            if (NUMRE.Match(ctest).Success) return "= " + ctest;
            if (ctest.Length == 0) return cond;
            if (Char.IsLetterOrDigit(ctest[0]) && !ctest.StartsWith("like ") && !ctest.StartsWith("between "))
            {
                return "= '" + cond.Trim() + "'";
            }
            return cond;
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            MoveColumnUp();
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            MoveColumnDown();
        }

        public void AddFilter(QueryDesignTableFrame table, string col)
        {
            string colname = table.AliasOrName + "." + col;
            var cond = new ExpressionConditionNode(m_condDesign, m_condDesign.Root);
            cond.LeftExprData = colname;
            m_condDesign.Root.Children.Add(cond);
            m_condDesign.UpdatePlacements();
            tabControl1.SelectedIndex = 1;
        }

        public ColWrapper GetFullColumnName(string name)
        {
            name = (name ?? "").Trim();
            foreach (var tbl in Tables)
            {
                foreach (var col in tbl.Columns)
                {
                    string fullname = tbl.AliasOrName + "." + col.ColumnName;
                    if (String.Compare(fullname, name, true) == 0) return col;
                    if (String.Compare(col.ColumnName, name, true) == 0) return col;
                }
            }
            return null;
        }

        private void DeletedRow(DataGridViewRow row)
        {
            var table = (QueryDesignTableFrame)row.Tag;
            table.UncheckColumn(row.Cells[0].Value.ToString());
            ReflectChanges();
        }

        private void dataGridColumns_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            DeletedRow(e.Row);
        }

        private void btnRemoveColumn_Click(object sender, EventArgs e)
        {
            var row = dataGridColumns.DeleteCurrentRow();
            if (row != null) DeletedRow(row);
            //if (dataGridColumns.CurrentCell == null) return;
            //var row = dataGridColumns.Rows[dataGridColumns.CurrentCell.RowIndex];
            //dataGridColumns.Rows.Remove(row);
        }

        private void lbxAvailableViews_MouseDown(object sender, MouseEventArgs e)
        {
            if (lbxAvailableViews.SelectedItem == null) return;
            lbxAvailableViews.DoDragDrop(lbxAvailableViews.SelectedItem, DragDropEffects.Copy);
            if (e.Clicks == 2) btnAddTable_Click(sender, e);
        }
    }

    //public enum QueryChangeSource { Tables, Columns, Order, Condition }

    //public class DesignedQuery
    //{
    //    public class Column
    //    {
    //        public string Name;
    //        public NameWithSchema Table;
    //        public string Alias;
    //    }
    //    List<Column> Colums = new List<Column>();
    //    public class Table
    //    {
    //    }
    //}
}
