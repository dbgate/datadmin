using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using DatAdmin;
using System.Xml;
using System.Linq;

namespace Plugin.diagrams
{
    public partial class DiagramEditFrame : ContentFrame
    {
        IDatabaseSource m_conn;
        IVirtualFile m_file;
        Diagram m_diagram;
        bool m_modified = false;
        List<DiagramTableItem> m_selectedTables = new List<DiagramTableItem>();
        Point? m_movingOrigin = null;
        Point? m_selectionPoint = null;
        //PropertiesToolForm m_propsForm = null;
        DiagramTablesForm m_tablesForm = null;
        bool m_denyDraw = false;
        bool m_created = false;

        public DiagramEditFrame(IVirtualFile file, IDatabaseSource conn)
        {
            InitializeComponent();
            OnlineHelpManager.RegisterHelpButton(btnOnlineHelp, "diagrams");

            if (!LicenseTool.FeatureAllowed(DiagramsFeature.Test)) throw new MissingFeatureError(DiagramsFeature._Name);
            btnAddToFavorites.Enabled = file is DiskFile;
            m_file = file;
            //cbxStyle.Items.Add(Texts.Get("s_custom"));
            m_diagram = Diagram.Load(m_file);

            int index = 0;
            foreach (var style in DiagramStyleAddonType.Instance.CommonSpace.GetFilteredAddons(RegisterItemUsage.DirectUse))
            {
                if (style.Name == XmlTool.GetRegisterType(m_diagram.Style)) index = cbxStyle.Items.Count;
                cbxStyle.Items.Add(style);
            }
            cbxStyle.SelectedIndex = index;

            m_conn = conn;
            m_diagram.Dialect = m_conn.Dialect;
            if (m_conn != null)
            {
                m_conn.Connection.Owner = this;
                m_conn.Connection.BeginOpen(Async.CreateInvokeCallback(m_invoker, OpenedConnection));
            }

            //btnDev.Visible = VersionInfo.IsDevVersion;

            //if (m_diagram.Tables.Count == 0)
            //{
            //    btnEditTables.Checked = true;
            //    btnEditTables_Click(this, EventArgs.Empty);
            //}
            labDragAndDrop.Visible = m_diagram.Tables.Count == 0;
            cbxZoom.Text = "100 %";
            propertyFrame1.SelectedObject = m_diagram.Style;
            //cbxZoom.SelectedIndex = cbxZoom.Items.IndexOf("100 %");
            ShowCurrentEntityStyle();
            m_diagram.Style.Changed += Style_Changed;
            m_created = true;
        }

        float ZoomFactor
        {
            get
            {
                try
                {
                    float res = float.Parse(cbxZoom.Text.Split(' ')[0]) / 100.0f;
                    if (res < 0.01) return 1;
                    return res;
                }
                catch
                {
                    return 1;
                }
            }
        }

        void SetStyle(DiagramStyle style)
        {
            if (style != null)
            {
                if (m_diagram != null)
                {
                    m_diagram.Style.Changed -= Style_Changed;
                    m_diagram.Style = style;
                    m_diagram.Style.Changed += Style_Changed;
                }
                //if (m_propsForm != null) m_propsForm.SelectedObject = style;
            }
            drawPanel.Invalidate();
        }

        void Style_Changed(object sender, EventArgs e)
        {
            drawPanel.Invalidate();
        }

        DiagramStyle CreateSelectedStyle()
        {
            AddonHolder ri = (AddonHolder)cbxStyle.SelectedItem;
            object res = ri.CreateInstance();
            return (DiagramStyle)res;
        }


        public override string PageTitle
        {
            get
            {
                if (m_file != null) return m_file.Name;
                return Texts.Get("s_diagram");
            }
        }

        private void OpenedConnection(IAsyncResult async)
        {
            try
            {
                m_conn.Connection.EndOpen(async);
            }
            catch (Exception e)
            {
                Errors.Report(e);
            }
        }

        public override void OnClose()
        {
            base.OnClose();
            if (m_conn != null)
            {
                IAsyncResult res = m_conn.Connection.BeginClose(null);
                Async.WaitFor(res);
                m_conn.Connection.EndClose(res);
            }
        }

        private void drawPanel_DragOver(object sender, DragEventArgs e)
        {
            DragObjectContainer obj = (DragObjectContainer)e.Data.GetData(typeof(DragObjectContainer));
            if (obj != null)
            {
                var appobjs = obj.Data as AppObject[];
                foreach (var appobj in appobjs)
                {
                    if (appobj is TableAppObject) e.Effect = DragDropEffects.Copy;
                }
            }
        }

        private void drawPanel_DragDrop(object sender, DragEventArgs e)
        {
            DragObjectContainer obj = (DragObjectContainer)e.Data.GetData(typeof(DragObjectContainer));
            if (obj != null)
            {
                var appobjs = obj.Data as AppObject[];
                if (appobjs != null)
                {
                    foreach (var appobj in appobjs)
                    {
                        var tbl = appobj as TableAppObject;
                        if (tbl != null)
                        {
                            NameWithSchema name = new NameWithSchema(tbl.DbObjectName.Name);
                            AddTable(name, PointToClient(new Point(e.X, e.Y)));
                        }
                    }
                }
            }
        }

        public override bool Save()
        {
            m_diagram.Save(m_file);
            m_modified = false;
            return true;
        }

        public override bool SupportsSave { get { return true; } }

        public override bool AllowClose()
        {
            if (m_modified)
            {
                DialogResult dr = MessageBox.Show(Texts.Get("s_file_modified_save$file", "file", m_file), VersionInfo.ProgramTitle, MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Yes)
                {
                    return Save();
                }
                if (dr == DialogResult.No) return true;
                return false;
            }
            return true;
        }

        private bool AntiAlias { get { return true; } }

        private void drawPanel_Paint(object sender, PaintEventArgs e)
        {
            if (m_denyDraw) return;

            //if (btnAntiAliasing.Checked)
            //{
            //    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //    e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            //}

            //e.Graphics.ScaleTransform(ZoomFactor, ZoomFactor);
            //m_diagram.Draw(e.Graphics);

            if (Math.Abs(ZoomFactor - 1.0) > 0.01)
            {
                if (AntiAlias)
                {
                    using (Bitmap bmp = new Bitmap(m_diagram.Size.Width, m_diagram.Size.Height))
                    {
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            m_diagram.Draw(g);
                        }
                        //e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        e.Graphics.DrawImage(bmp, new RectangleF(0, 0, m_diagram.Size.Width * ZoomFactor, m_diagram.Size.Height * ZoomFactor));
                    }
                }
                else
                {
                    e.Graphics.ScaleTransform(ZoomFactor, ZoomFactor);
                    m_diagram.Draw(e.Graphics);
                }
            }
            else
            {
                m_diagram.Draw(e.Graphics);
            }

            e.Graphics.ResetTransform();
            e.Graphics.ScaleTransform(ZoomFactor, ZoomFactor);

            Size newsize = m_diagram.Size;
            newsize.Width = (int)(newsize.Width * ZoomFactor);
            newsize.Height = (int)(newsize.Height * ZoomFactor);
            if (newsize.Height < 1) newsize.Height = 1;
            if (newsize.Width < 1) newsize.Width = 1;
            if (newsize != drawPanel.Size)
            {
                drawPanel.Size = newsize;
            }
            foreach (var seltbl in m_selectedTables)
            {
                using (Pen pen = new Pen(drawPanel.Focused ? Brushes.Blue : Brushes.Gray, 3))
                {
                    e.Graphics.DrawRectangle(pen, new Rectangle(seltbl.Location, seltbl.Size));
                }
            }

            if (m_movingOrigin != null && m_selectionPoint != null)
            {
                using (var pen = new Pen(Color.Black, 1))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    var r = DrawingExtension.GetBoundingRectangle(m_movingOrigin.Value, m_selectionPoint.Value);
                    e.Graphics.DrawRectangle(pen, r);
                }
            }

            bool wantRefresh = false;
            foreach (var t in m_diagram.Tables)
            {
                if (t.MustBePlaced)
                {
                    PlaceTable(t);
                    wantRefresh = true;
                    t.MustBePlaced = false;
                }
            }
            if (wantRefresh) drawPanel.Invalidate();
        }

        private void PlaceTable(DiagramTableItem table)
        {
            int ymax;
            try
            {
                ymax = (from t in m_diagram.Tables where !t.MustBePlaced select t.Bottom).Max();
            }
            catch
            {
                ymax = 0;
            }
            int y1;
            try
            {
                y1 = (from t in m_diagram.Tables where t.Bottom == ymax && !t.MustBePlaced select t.Top).Max();
            }
            catch
            {
                y1 = m_diagram.Style.Placement.MinVerticalDistance;
            }
            Interval yint = new Interval(y1, y1 + table.Size.Height);
            int hspace = m_diagram.Style.Placement.MinHorizontalDistance;
            int newleft = hspace;
            int maxwi = m_diagram.Style.Placement.MaxDiagramWidth;
            if (maxwi < 0) maxwi = drawPanelContainer.Width;
            foreach (var t in m_diagram.Tables)
            {
                if (t.MustBePlaced) continue;
                if (!Interval.Intersection(t.VerInterv, yint).IsEmpty) newleft = Math.Max(newleft, t.Right + hspace);
            }
            if (newleft + table.Size.Width > maxwi)
            {
                table.X = hspace;
                table.Y = ymax + m_diagram.Style.Placement.MinVerticalDistance;
            }
            else
            {
                table.X = newleft;
                table.Y = y1;
            }
        }

        private void drawPanel_MouseDown(object sender, MouseEventArgs e)
        {
            int x = (int)(e.X / ZoomFactor);
            int y = (int)(e.Y / ZoomFactor);
            var seltbl = m_diagram.GetTableAt(x, y);
            m_movingOrigin = new Point(x, y);
            Point pt = drawPanelContainer.AutoScrollPosition;
            drawPanel.Focus();
            pt.X *= -1;
            pt.Y *= -1;
            drawPanelContainer.AutoScrollPosition = pt;
            drawPanel.Invalidate();
            UpdateEnabling();

            if ((Control.ModifierKeys & Keys.Control) != 0)
            {
                if (seltbl != null)
                {
                    if (m_selectedTables.Contains(seltbl)) m_selectedTables.Remove(seltbl);
                    else m_selectedTables.Add(seltbl);
                }
            }
            else
            {
                if (seltbl != null && m_selectedTables.Contains(seltbl))
                {
                    // do nothing
                }
                else
                {
                    m_selectedTables.Clear();
                    if (seltbl != null) m_selectedTables.Add(seltbl);
                }
                //if (seltbl == null || seltbl != null && !m_selectedTables.Contains(seltbl)) m_selectedTables.Clear();
                //if (seltbl != null) m_selectedTables.Add(seltbl);
            }

            ShowCurrentEntityStyle();
        }

        private EntityStyle[] EnumNamedStyles(string style)
        {
            var res = new List<EntityStyle>();
            foreach (var tbl in m_diagram.Tables)
            {
                if (tbl.EntityOverride != null)
                {
                    if (style == null || tbl.EntityOverride.StyleName == style) res.Add(tbl.EntityOverride);
                }
            }
            return res.ToArray();
        }

        private void ShowCurrentEntityStyle()
        {
            if (m_selectedTables.Count >= 1)
            {
                var names = new List<string>();
                foreach (var tbl in m_selectedTables) names.Add(tbl.Table.Name);
                tbxStyleTableName.Text = names.CreateDelimitedText(", ");
                entityStyleFrame1.ShowEntityStyle(m_diagram.GetCurrentEntityStyle(), EnumNamedStyles, GetSelectedTablesEntityOveride, SetSelectedTablesEntityOveride);
            }
            if (m_selectedTables.Count == 0)
            {
                tbxStyleTableName.Text = "(" + Texts.Get("s_diagram") + ")";
                entityStyleFrame1.ShowEntityStyle(m_diagram.Style.Entity, null, m_diagram.GetEntityOverride, m_diagram.SetEntityOverride);
            }
        }

        private EntityStyle[] GetSelectedTablesEntityOveride()
        {
            return (from tbl in m_selectedTables where tbl.EntityOverride != null select tbl.EntityOverride).ToArray();
        }

        private void SetSelectedTablesEntityOveride(EntityStyle value)
        {
            foreach (var tbl in m_selectedTables)
            {
                tbl.SetEntityOverride(value);
            }
        }

        private void drawPanel_MouseMove(object sender, MouseEventArgs e)
        {
            int x = (int)(e.X / ZoomFactor);
            int y = (int)(e.Y / ZoomFactor);
            if (m_movingOrigin != null)
            {
                if (m_selectedTables.Count > 0)
                {
                    foreach (var tbl in m_selectedTables)
                    {
                        tbl.X += x - m_movingOrigin.Value.X;
                        tbl.Y += y - m_movingOrigin.Value.Y;
                    }
                    m_movingOrigin = new Point(x, y);
                    drawPanel.Invalidate();
                }
                else
                {
                    m_selectionPoint = new Point(x, y);
                    drawPanel.Invalidate();
                }
            }
        }

        private void drawPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_movingOrigin != null && m_selectionPoint != null)
            {
                var r = DrawingExtension.GetBoundingRectangle(m_movingOrigin.Value, m_selectionPoint.Value);
                foreach (var table in m_diagram.Tables)
                {
                    if (!table.HorInterv.Intersection(r.GetHorInterv()).IsEmpty &&
                        !table.VerInterv.Intersection(r.GetVerInterv()).IsEmpty)
                    {
                        if (!m_selectedTables.Contains(table)) m_selectedTables.Add(table);
                    }
                }
                ShowCurrentEntityStyle();
            }
            m_movingOrigin = null;
            m_selectionPoint = null;
            drawPanel.Invalidate();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DeleteSelectedTable();
        }

        public void DeleteSelectedTable()
        {
            var copy = new List<DiagramTableItem>(m_selectedTables);
            foreach (var tbl in copy)
            {
                DeleteTable(tbl);
            }
        }

        private void DeleteTable(DiagramTableItem table)
        {
            if (table != null)
            {
                m_diagram.Tables.Remove(table);
                m_selectedTables.Remove(table);
                drawPanel.Invalidate();
            }
            labDragAndDrop.Visible = m_diagram.Tables.Count == 0;
            UpdateEnabling();
        }

        [PopupMenuEnabled("s_reload")]
        public bool ReloadDataEnabled()
        {
            return m_conn != null;
        }

        [PopupMenu("s_reload", ImageName = CoreIcons.refreshName)]
        public void ReloadData()
        {
            if (m_conn == null) return;
            if (MainWindow.Instance.ProcessRefreshMessage()) return;
            m_conn.ClearCaches();
            foreach (DiagramTableItem item in m_diagram.Tables)
            {
                try
                {
                    var ts = m_conn.GetTable(item.Table.FullName).InvokeLoadStructure(TableStructureMembers.AllNoRefs);
                    if (ts.Columns.Count > 0)
                    {
                        item.Table = new TableStructure(ts);
                    }
                    else
                    {
                        Logging.Info("DAE-00377 table {0} not found", item.Table.FullName);
                    }
                }
                catch
                {
                    Logging.Info("DAE-00378 table {0} not found", item.Table.FullName);
                }
            }
            drawPanel.Invalidate();
        }

        public override string MenuBarTitle
        {
            get { return "s_diagram"; }
        }

        private void drawPanelContainer_MouseDown(object sender, MouseEventArgs e)
        {
            m_selectedTables.Clear();
            drawPanel.Invalidate();
            UpdateEnabling();
            ShowCurrentEntityStyle();
        }

        private void UpdateEnabling()
        {
            mnuDelete.Enabled = btnDelete.Enabled = m_selectedTables.Count > 0;
            btnAlterTable.Enabled = mnuDesign.Enabled = mnuRename.Enabled = m_selectedTables.Count == 1;

        }

        [PopupMenu("s_export_as_image")]
        public void ExportAsImage()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "*.PNG|*.png|*.JPG|*.jpg|*.BMP|*.bmp";
            dlg.FilterIndex = 0;
            if (dlg.ShowDialogEx() == DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(m_diagram.Size.Width, m_diagram.Size.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
                    m_diagram.Draw(g);
                }
                ImageFormat[] fmts = new ImageFormat[] { ImageFormat.Png, ImageFormat.Jpeg, ImageFormat.Bmp };
                bmp.Save(dlg.FileName, fmts[dlg.FilterIndex - 1]);
                Usage.AddSub("save_as_image", Path.GetExtension(dlg.FileName), m_diagram.Tables.Count.ToString());
            }
        }

        private void drawPanel_Leave(object sender, EventArgs e)
        {
            drawPanel.Invalidate();
        }

        private void sdeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedTable();
        }

        //public override string UsageEventName
        //{
        //    get { return "editdiagram"; }
        //}

        //private void toolStripButton1_Click_1(object sender, EventArgs e)
        //{
        //    if (btnStyleProps.Checked)
        //    {
        //        if (m_propsForm == null)
        //        {
        //            m_propsForm = new PropertiesToolForm();
        //            m_propsForm.SelectedObject = m_diagram.Style;
        //            m_propsForm.FormClosing += new FormClosingEventHandler(m_propsForm_FormClosing);
        //            m_propsForm.OnChanged += new EventHandler(m_propsForm_OnChanged);
        //        }
        //        m_propsForm.Show();
        //    }
        //    else
        //    {
        //        if (m_propsForm != null) m_propsForm.Hide();
        //    }
        //}

        //void m_propsForm_OnChanged(object sender, EventArgs e)
        //{
        //    drawPanel.Invalidate();
        //}

        //void m_propsForm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    m_propsForm.Hide();
        //    e.Cancel = true;
        //    //btnStyleProps.Checked = false;
        //}

        private void cbxStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_created) return;
            SetStyle(CreateSelectedStyle());
            Usage.AddSub("change_diagram_style", XmlTool.GetRegisterType(m_diagram.Style));
        }

        private void toolStripButton1_Click_2(object sender, EventArgs e)
        {
            string name = InputBox.Run(Texts.Get("s_select_template_name"), "tpl1");
            if (name != null)
            {
                string fn = Path.Combine(DiagramStyleAddonType.Instance.CommonSpace.Folder, name + ".adx");
                if (File.Exists(fn))
                {
                    if (!StdDialog.ReallyOverwriteFile(fn)) return;
                }
                //m_diagram.Style.WriteXml
                XmlTool.SerializeObject(fn, m_diagram.Style);
                DiagramStyleAddonType.Instance.CommonSpace.ClearCache();
            }
        }

        //private void generateC30CodeToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    XmlDocument doc = XmlTool.CreateDocument("Diff");
        //    ObjectDiff.SaveDiff(m_diagram.Style, new DiagramStyle(), doc.DocumentElement);
        //    string text = ObjectDiff.DiffToCsharp3(typeof(DiagramStyle), doc.DocumentElement);
        //    Clipboard.SetText(text);
        //    StdDialog.ShowInfo("C# code was copied into clipboard");
        //}

        public IEnumerable<NameWithSchema> TableNames
        {
            get
            {
                foreach (var t in m_diagram.Tables)
                {
                    yield return t.Table.FullName;
                }
            }
        }

        public IDatabaseSource Database { get { return m_conn; } }

        public void RemoveTable(NameWithSchema table)
        {
            DeleteTable(m_diagram.FindTable(table));
        }

        public void AddTable(NameWithSchema table)
        {
            AddTable(table, null);
        }

        public void AddTable(NameWithSchema table, Point? pt)
        {
            if (m_diagram.FindTable(table) != null) return;
            DiagramTableItem item = new DiagramTableItem(m_diagram);
            if (pt == null)
            {
                item.MustBePlaced = true;
            }
            else
            {
                item.X = pt.Value.X;
                item.Y = pt.Value.Y;
            }
            try
            {
                m_denyDraw = true;
                m_conn.ClearCaches();
                item.Table = (TableStructure)m_conn.GetTable(table).InvokeLoadStructure(TableStructureMembers.AllNoRefs);
            }
            finally
            {
                m_denyDraw = false;
            }
            m_diagram.Tables.Add(item);
            m_modified = true;
            drawPanel.Invalidate();
            labDragAndDrop.Visible = m_diagram.Tables.Count == 0;
            Usage.AddSub("add_table", table.ToString());
        }

        private void btnEditTables_Click(object sender, EventArgs e)
        {
            if (btnEditTables.Checked)
            {
                if (m_tablesForm == null)
                {
                    m_tablesForm = new DiagramTablesForm(this);
                    m_tablesForm.FormClosing += new FormClosingEventHandler(m_tablesForm_FormClosing);
                }
                m_tablesForm.Show();
            }
            else
            {
                if (m_tablesForm != null) m_tablesForm.Hide();
            }
        }

        void m_tablesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_tablesForm.Hide();
            e.Cancel = true;
            btnEditTables.Checked = false;
        }

        private void AlterCurrentTable()
        {
            if (m_selectedTables.Count == 1 && m_conn != null && m_conn.Connection != null)
            {
                m_movingOrigin = null;
                if (TableEditForm.Run(m_conn.CloneSource(), m_selectedTables[0].Table, new AlterTableEditorPars()))
                {
                    ReloadData();
                    m_modified = true;
                }
                //MainWindow.Instance.OpenContent(new TableEditFrame(m_selectedTable.Table, m_conn.Connection.Dialect, m_conn.GetTable(m_selectedTable.Table.FullName)));
            }
        }

        private void btnAlterTable_Click(object sender, EventArgs e)
        {
            AlterCurrentTable();
        }

        private void cbxZoom_TextChanged(object sender, EventArgs e)
        {
            drawPanel.Invalidate();
            //btnAntiAliasing.Enabled = Math.Abs(ZoomFactor - 1.0) > 0.01;
        }

        public override Bitmap Image
        {
            get { return CoreIcons.diagram; }
        }

        private void srenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenameSelectedTable();
        }

        private void RenameSelectedTable()
        {
            if (m_selectedTables.Count != 1) return;
            RenameTable(m_selectedTables[0]);
        }

        private void RenameTable(DiagramTableItem table)
        {
            if (table != null)
            {
                string newname = InputBox.Run("s_new_table_name", table.Table.FullName.ToString());
                if (newname != null)
                {
                    var tbl = m_conn.GetTable(table.Table.FullName);
                    tbl.RenameTable(newname);
                    DbObjectNameTool.RenameTable(table.Table, from t in m_diagram.Tables select t.Table, new NameWithSchema(newname));
                    drawPanel.Invalidate();
                }
            }
        }

        private void sdesignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlterCurrentTable();
        }

        private void btnAddToFavorites_Click(object sender, EventArgs e)
        {
            var fav = new DiagramFavorite
            {
                File = m_file.DiskPath,
                Database = m_conn
            };
            AddToFavoriteForm.Run(fav, Path.GetFileNameWithoutExtension(m_file.DiskPath));
        }

        private void btnExportAsImage_Click(object sender, EventArgs e)
        {
            ExportAsImage();
        }

        private void DiagramEditFrame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                ReloadData();
                e.Handled = true;
            }
        }

        private void entityStyleFrame1_Changed(object sender, EventArgs e)
        {
            drawPanel.Invalidate();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            ReloadData();
        }
    }

    [Favorite(Name = "diagram", Title = "Diagram", RequiredFeature = DiagramsFeature.Test)]
    public class DiagramFavorite : FavoriteBase
    {
        [XmlElem]
        public string File { get; set; }

        public IDatabaseSource Database;

        public override Bitmap Image
        {
            get { return CoreIcons.diagram; }
        }

        public override void Open()
        {
            MainWindow.Instance.OpenContent(new DiagramEditFrame(new DiskFile(File), Database.CloneSource()));
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            var dbo = (DatabaseAppObject)AppObjectAddonType.Instance.LoadAddon(xml.FindElement("Database"));
            Database = dbo.FindDatabaseConnection(new ConnectionPack("dummy")).CloneSource();
            //Database = (IDatabaseSource)DatabaseSourceAddonType.Instance.LoadAddon(xml.FindElement("Database"));
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            var dbo = new DatabaseAppObject();
            dbo.FillFromDatabase(Database);
            dbo.SaveToXml(xml.AddChild("Database"));
            //Database.SaveToXml(xml.AddChild("Database"));
        }

        public override string Description
        {
            get { return "s_diagram"; }
        }

        public override void DisplayProps(Action<string, string> display)
        {
            base.DisplayProps(display);
            display("s_file", File);
        }
    }
}
