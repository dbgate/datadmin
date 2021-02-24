namespace DatAdmin
{
    partial class ColumnMapFrame_FixedTarget
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
            this.labTarget = new System.Windows.Forms.Label();
            this.labValue = new System.Windows.Forms.Label();
            this.labType = new System.Windows.Forms.Label();
            this.labSkip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labTarget
            // 
            this.labTarget.AutoSize = true;
            this.labTarget.Location = new System.Drawing.Point(22, 16);
            this.labTarget.Name = "labTarget";
            this.labTarget.Size = new System.Drawing.Size(45, 13);
            this.labTarget.TabIndex = 0;
            this.labTarget.Text = "s_target";
            // 
            // labValue
            // 
            this.labValue.AutoSize = true;
            this.labValue.Location = new System.Drawing.Point(326, 16);
            this.labValue.Name = "labValue";
            this.labValue.Size = new System.Drawing.Size(44, 13);
            this.labValue.TabIndex = 1;
            this.labValue.Text = "s_value";
            // 
            // labType
            // 
            this.labType.AutoSize = true;
            this.labType.Location = new System.Drawing.Point(182, 16);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(38, 13);
            this.labType.TabIndex = 2;
            this.labType.Text = "s_type";
            // 
            // labSkip
            // 
            this.labSkip.AutoSize = true;
            this.labSkip.Location = new System.Drawing.Point(129, 16);
            this.labSkip.Name = "labSkip";
            this.labSkip.Size = new System.Drawing.Size(37, 13);
            this.labSkip.TabIndex = 3;
            this.labSkip.Text = "s_skip";
            // 
            // ColumnMapFrame_FixedTarget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.labSkip);
            this.Controls.Add(this.labType);
            this.Controls.Add(this.labValue);
            this.Controls.Add(this.labTarget);
            this.Name = "ColumnMapFrame_FixedTarget";
            this.Size = new System.Drawing.Size(514, 369);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labTarget;
        private System.Windows.Forms.Label labValue;
        private System.Windows.Forms.Label labType;
        private System.Windows.Forms.Label labSkip;

    }
}
