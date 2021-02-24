namespace DatAdmin
{
    partial class ConfigSelectionFrame
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
            DatAdmin.TreeBehaviour treeBehaviour4 = new DatAdmin.TreeBehaviour();
            this.daTreeView1 = new DatAdmin.DATreeView();
            this.btnCheckAll = new System.Windows.Forms.Button();
            this.btnUncheckAll = new System.Windows.Forms.Button();
            this.btnCollapseAll = new System.Windows.Forms.Button();
            this.btnExpandAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // daTreeView1
            // 
            this.daTreeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.daTreeView1.Location = new System.Drawing.Point(0, 0);
            this.daTreeView1.Name = "daTreeView1";
            this.daTreeView1.Root = null;
            this.daTreeView1.RootPath = null;
            this.daTreeView1.Size = new System.Drawing.Size(566, 363);
            this.daTreeView1.TabIndex = 0;
            this.daTreeView1.TreeBehaviour = treeBehaviour4;
            this.daTreeView1.TreeStyle = DatAdmin.TreeStyle.SelectMany;
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCheckAll.Location = new System.Drawing.Point(0, 369);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(85, 23);
            this.btnCheckAll.TabIndex = 1;
            this.btnCheckAll.Text = "s_check_all";
            this.btnCheckAll.UseVisualStyleBackColor = true;
            this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
            // 
            // btnUncheckAll
            // 
            this.btnUncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUncheckAll.Location = new System.Drawing.Point(91, 369);
            this.btnUncheckAll.Name = "btnUncheckAll";
            this.btnUncheckAll.Size = new System.Drawing.Size(85, 23);
            this.btnUncheckAll.TabIndex = 2;
            this.btnUncheckAll.Text = "s_uncheck_all";
            this.btnUncheckAll.UseVisualStyleBackColor = true;
            this.btnUncheckAll.Click += new System.EventHandler(this.btnUncheckAll_Click);
            // 
            // btnCollapseAll
            // 
            this.btnCollapseAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCollapseAll.Location = new System.Drawing.Point(273, 369);
            this.btnCollapseAll.Name = "btnCollapseAll";
            this.btnCollapseAll.Size = new System.Drawing.Size(85, 23);
            this.btnCollapseAll.TabIndex = 3;
            this.btnCollapseAll.Text = "s_collapse_all";
            this.btnCollapseAll.UseVisualStyleBackColor = true;
            this.btnCollapseAll.Click += new System.EventHandler(this.btnCollapseAll_Click);
            // 
            // btnExpandAll
            // 
            this.btnExpandAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExpandAll.Location = new System.Drawing.Point(182, 369);
            this.btnExpandAll.Name = "btnExpandAll";
            this.btnExpandAll.Size = new System.Drawing.Size(85, 23);
            this.btnExpandAll.TabIndex = 4;
            this.btnExpandAll.Text = "s_expand_all";
            this.btnExpandAll.UseVisualStyleBackColor = true;
            this.btnExpandAll.Click += new System.EventHandler(this.btnExpandAll_Click);
            // 
            // ConfigSelectionFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnExpandAll);
            this.Controls.Add(this.btnCollapseAll);
            this.Controls.Add(this.btnUncheckAll);
            this.Controls.Add(this.btnCheckAll);
            this.Controls.Add(this.daTreeView1);
            this.Name = "ConfigSelectionFrame";
            this.Size = new System.Drawing.Size(566, 392);
            this.ResumeLayout(false);

        }

        #endregion

        private DATreeView daTreeView1;
        private System.Windows.Forms.Button btnCheckAll;
        private System.Windows.Forms.Button btnUncheckAll;
        private System.Windows.Forms.Button btnCollapseAll;
        private System.Windows.Forms.Button btnExpandAll;

    }
}
