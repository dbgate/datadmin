namespace Plugin.versiondb
{
    partial class FilterByNameTransformFrame
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbxObjectType = new System.Windows.Forms.ComboBox();
            this.chbRemoveSelected = new System.Windows.Forms.CheckBox();
            this.objectFilterFrame1 = new DatAdmin.ObjectFilterFrame();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "s_type";
            // 
            // cbxObjectType
            // 
            this.cbxObjectType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxObjectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxObjectType.FormattingEnabled = true;
            this.cbxObjectType.Location = new System.Drawing.Point(76, 24);
            this.cbxObjectType.Name = "cbxObjectType";
            this.cbxObjectType.Size = new System.Drawing.Size(239, 21);
            this.cbxObjectType.TabIndex = 1;
            this.cbxObjectType.SelectedIndexChanged += new System.EventHandler(this.cbxObjectType_SelectedIndexChanged);
            // 
            // chbRemoveSelected
            // 
            this.chbRemoveSelected.AutoSize = true;
            this.chbRemoveSelected.Location = new System.Drawing.Point(3, 3);
            this.chbRemoveSelected.Name = "chbRemoveSelected";
            this.chbRemoveSelected.Size = new System.Drawing.Size(72, 17);
            this.chbRemoveSelected.TabIndex = 3;
            this.chbRemoveSelected.Text = "s_remove";
            this.chbRemoveSelected.UseVisualStyleBackColor = true;
            // 
            // objectFilterFrame1
            // 
            this.objectFilterFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.objectFilterFrame1.Filter = null;
            this.objectFilterFrame1.Location = new System.Drawing.Point(3, 51);
            this.objectFilterFrame1.Name = "objectFilterFrame1";
            this.objectFilterFrame1.Size = new System.Drawing.Size(312, 147);
            this.objectFilterFrame1.TabIndex = 4;
            // 
            // FilterByNameTransformFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.objectFilterFrame1);
            this.Controls.Add(this.chbRemoveSelected);
            this.Controls.Add(this.cbxObjectType);
            this.Controls.Add(this.label1);
            this.Name = "FilterByNameTransformFrame";
            this.Size = new System.Drawing.Size(318, 201);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxObjectType;
        private System.Windows.Forms.CheckBox chbRemoveSelected;
        private DatAdmin.ObjectFilterFrame objectFilterFrame1;
    }
}
