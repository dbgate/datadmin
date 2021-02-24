namespace DatAdmin
{
    partial class AddQueryToFavoriteForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtDesign = new System.Windows.Forms.RadioButton();
            this.rbtLinkToFile = new System.Windows.Forms.RadioButton();
            this.rbtQueryText = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbtExecute = new System.Windows.Forms.RadioButton();
            this.rbtOpen = new System.Windows.Forms.RadioButton();
            this.addToFavoritesFrame1 = new DatAdmin.AddToFavoritesFrame();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtDesign);
            this.groupBox1.Controls.Add(this.rbtLinkToFile);
            this.groupBox1.Controls.Add(this.rbtQueryText);
            this.groupBox1.Location = new System.Drawing.Point(26, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 99);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "s_what_save";
            // 
            // rbtDesign
            // 
            this.rbtDesign.AutoSize = true;
            this.rbtDesign.Location = new System.Drawing.Point(7, 68);
            this.rbtDesign.Name = "rbtDesign";
            this.rbtDesign.Size = new System.Drawing.Size(67, 17);
            this.rbtDesign.TabIndex = 2;
            this.rbtDesign.Text = "s_design";
            this.rbtDesign.UseVisualStyleBackColor = true;
            // 
            // rbtLinkToFile
            // 
            this.rbtLinkToFile.AutoSize = true;
            this.rbtLinkToFile.Location = new System.Drawing.Point(7, 44);
            this.rbtLinkToFile.Name = "rbtLinkToFile";
            this.rbtLinkToFile.Size = new System.Drawing.Size(86, 17);
            this.rbtLinkToFile.TabIndex = 1;
            this.rbtLinkToFile.Text = "s_link_to_file";
            this.rbtLinkToFile.UseVisualStyleBackColor = true;
            // 
            // rbtQueryText
            // 
            this.rbtQueryText.AutoSize = true;
            this.rbtQueryText.Checked = true;
            this.rbtQueryText.Location = new System.Drawing.Point(7, 20);
            this.rbtQueryText.Name = "rbtQueryText";
            this.rbtQueryText.Size = new System.Drawing.Size(85, 17);
            this.rbtQueryText.TabIndex = 0;
            this.rbtQueryText.TabStop = true;
            this.rbtQueryText.Text = "s_query_text";
            this.rbtQueryText.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtExecute);
            this.groupBox2.Controls.Add(this.rbtOpen);
            this.groupBox2.Location = new System.Drawing.Point(237, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 72);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "s_action";
            // 
            // rbtExecute
            // 
            this.rbtExecute.AutoSize = true;
            this.rbtExecute.Location = new System.Drawing.Point(8, 42);
            this.rbtExecute.Name = "rbtExecute";
            this.rbtExecute.Size = new System.Drawing.Size(74, 17);
            this.rbtExecute.TabIndex = 1;
            this.rbtExecute.Text = "s_execute";
            this.rbtExecute.UseVisualStyleBackColor = true;
            // 
            // rbtOpen
            // 
            this.rbtOpen.AutoSize = true;
            this.rbtOpen.Checked = true;
            this.rbtOpen.Location = new System.Drawing.Point(8, 19);
            this.rbtOpen.Name = "rbtOpen";
            this.rbtOpen.Size = new System.Drawing.Size(60, 17);
            this.rbtOpen.TabIndex = 0;
            this.rbtOpen.TabStop = true;
            this.rbtOpen.Text = "s_open";
            this.rbtOpen.UseVisualStyleBackColor = true;
            // 
            // addToFavoritesFrame1
            // 
            this.addToFavoritesFrame1.Favorite = null;
            this.addToFavoritesFrame1.FavoriteName = "";
            this.addToFavoritesFrame1.Location = new System.Drawing.Point(26, 139);
            this.addToFavoritesFrame1.Name = "addToFavoritesFrame1";
            this.addToFavoritesFrame1.Size = new System.Drawing.Size(411, 49);
            this.addToFavoritesFrame1.TabIndex = 2;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(281, 222);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "s_ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(362, 222);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "s_cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // AddQueryToFavoriteForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(449, 257);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.addToFavoritesFrame1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddQueryToFavoriteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_add_to_favorites";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtDesign;
        private System.Windows.Forms.RadioButton rbtLinkToFile;
        private System.Windows.Forms.RadioButton rbtQueryText;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbtExecute;
        private System.Windows.Forms.RadioButton rbtOpen;
        private AddToFavoritesFrame addToFavoritesFrame1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}