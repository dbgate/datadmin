namespace DatAdmin
{
    partial class NewTableChooserFrame
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DatAdmin.TreeBehaviour treeBehaviour1 = new DatAdmin.TreeBehaviour();
            this.splitContainerEx1 = new DatAdmin.SplitContainerEx();
            this.daTreeView1 = new DatAdmin.DATreeView();
            this.tableFromTabularDataFrame1 = new DatAdmin.TableFromTabularDataFrame();
            this.splitContainerEx1.Panel1.SuspendLayout();
            this.splitContainerEx1.Panel2.SuspendLayout();
            this.splitContainerEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerEx1
            // 
            this.splitContainerEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEx1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerEx1.MoreFixedPanel = DatAdmin.SplitContainerEx.PanelType.First;
            this.splitContainerEx1.Name = "splitContainerEx1";
            // 
            // splitContainerEx1.Panel1
            // 
            this.splitContainerEx1.Panel1.Controls.Add(this.daTreeView1);
            this.splitContainerEx1.Panel1DynamicCollapsed = false;
            this.splitContainerEx1.Panel1MinDynamicSize = 0;
            // 
            // splitContainerEx1.Panel2
            // 
            this.splitContainerEx1.Panel2.Controls.Add(this.tableFromTabularDataFrame1);
            this.splitContainerEx1.Panel2DynamicCollapsed = false;
            this.splitContainerEx1.Panel2MinDynamicSize = 0;
            this.splitContainerEx1.Size = new System.Drawing.Size(560, 358);
            this.splitContainerEx1.SplitterDistance = 280;
            this.splitContainerEx1.SplitterWidth = 6;
            this.splitContainerEx1.TabIndex = 2;
            // 
            // daTreeView1
            // 
            this.daTreeView1.DialogLabel = "s_select_database";
            this.daTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.daTreeView1.Location = new System.Drawing.Point(0, 0);
            this.daTreeView1.Name = "daTreeView1";
            this.daTreeView1.Root = null;
            this.daTreeView1.RootPath = null;
            this.daTreeView1.Size = new System.Drawing.Size(280, 358);
            this.daTreeView1.TabIndex = 0;
            this.daTreeView1.TreeBehaviour = treeBehaviour1;
            this.daTreeView1.TreeStyle = DatAdmin.TreeStyle.SelectOne;
            this.daTreeView1.ActiveNodeChange += new System.EventHandler(this.daTreeView1_ActiveNodeChange);
            // 
            // tableFromTabularDataFrame1
            // 
            this.tableFromTabularDataFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableFromTabularDataFrame1.Location = new System.Drawing.Point(0, 0);
            this.tableFromTabularDataFrame1.Name = "tableFromTabularDataFrame1";
            this.tableFromTabularDataFrame1.Size = new System.Drawing.Size(274, 358);
            this.tableFromTabularDataFrame1.TabIndex = 1;
            this.tableFromTabularDataFrame1.ChangedProperties += new System.EventHandler(this.tableFromTabularDataFrame1_ChangedProperties);
            // 
            // NewTableChooserFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerEx1);
            this.Name = "NewTableChooserFrame";
            this.Size = new System.Drawing.Size(560, 358);
            this.splitContainerEx1.Panel1.ResumeLayout(false);
            this.splitContainerEx1.Panel2.ResumeLayout(false);
            this.splitContainerEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DATreeView daTreeView1;
        private TableFromTabularDataFrame tableFromTabularDataFrame1;
        private SplitContainerEx splitContainerEx1;
    }
}
