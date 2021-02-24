namespace Plugin.diagrams
{
    partial class BackgroundColorFrame
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
            this.labStart = new System.Windows.Forms.Label();
            this.chbGradient = new System.Windows.Forms.CheckBox();
            this.labEnd = new System.Windows.Forms.Label();
            this.btnExchange = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarAngle = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAngle)).BeginInit();
            this.SuspendLayout();
            // 
            // labStart
            // 
            this.labStart.BackColor = System.Drawing.Color.Black;
            this.labStart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labStart.Location = new System.Drawing.Point(0, 0);
            this.labStart.Name = "labStart";
            this.labStart.Size = new System.Drawing.Size(45, 39);
            this.labStart.TabIndex = 0;
            this.labStart.Click += new System.EventHandler(this.labStart_Click);
            // 
            // chbGradient
            // 
            this.chbGradient.AutoSize = true;
            this.chbGradient.Location = new System.Drawing.Point(51, 0);
            this.chbGradient.Name = "chbGradient";
            this.chbGradient.Size = new System.Drawing.Size(75, 17);
            this.chbGradient.TabIndex = 1;
            this.chbGradient.Text = "s_gradient";
            this.chbGradient.UseVisualStyleBackColor = true;
            this.chbGradient.CheckedChanged += new System.EventHandler(this.chbGradient_CheckedChanged);
            // 
            // labEnd
            // 
            this.labEnd.BackColor = System.Drawing.Color.White;
            this.labEnd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labEnd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labEnd.Location = new System.Drawing.Point(0, 35);
            this.labEnd.Name = "labEnd";
            this.labEnd.Size = new System.Drawing.Size(45, 39);
            this.labEnd.TabIndex = 2;
            this.labEnd.Click += new System.EventHandler(this.labStart_Click);
            // 
            // btnExchange
            // 
            this.btnExchange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExchange.Location = new System.Drawing.Point(51, 23);
            this.btnExchange.Name = "btnExchange";
            this.btnExchange.Size = new System.Drawing.Size(123, 23);
            this.btnExchange.TabIndex = 3;
            this.btnExchange.Text = "s_exchange";
            this.btnExchange.UseVisualStyleBackColor = true;
            this.btnExchange.Click += new System.EventHandler(this.btnExchange_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Location = new System.Drawing.Point(51, 51);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(123, 23);
            this.btnCopy.TabIndex = 4;
            this.btnCopy.Text = "s_copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.FullOpen = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "s_angle";
            // 
            // trackBarAngle
            // 
            this.trackBarAngle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarAngle.LargeChange = 9;
            this.trackBarAngle.Location = new System.Drawing.Point(51, 78);
            this.trackBarAngle.Maximum = 36;
            this.trackBarAngle.Name = "trackBarAngle";
            this.trackBarAngle.Size = new System.Drawing.Size(123, 45);
            this.trackBarAngle.TabIndex = 6;
            this.trackBarAngle.ValueChanged += new System.EventHandler(this.trackBarAngle_ValueChanged);
            // 
            // BackgroundColorFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.trackBarAngle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnExchange);
            this.Controls.Add(this.labEnd);
            this.Controls.Add(this.chbGradient);
            this.Controls.Add(this.labStart);
            this.Name = "BackgroundColorFrame";
            this.Size = new System.Drawing.Size(177, 130);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAngle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labStart;
        private System.Windows.Forms.CheckBox chbGradient;
        private System.Windows.Forms.Label labEnd;
        private System.Windows.Forms.Button btnExchange;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBarAngle;
    }
}
