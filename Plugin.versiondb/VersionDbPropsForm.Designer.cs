namespace Plugin.versiondb
{
    partial class VersionDbPropsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VersionDbPropsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.cbxDialect = new System.Windows.Forms.ComboBox();
            this.chbForceSingleSchema = new System.Windows.Forms.CheckBox();
            this.chbCreateDefaultVariables = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.infoBoxFrame2 = new DatAdmin.InfoBoxFrame();
            this.infoBoxFrame1 = new DatAdmin.InfoBoxFrame();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.codeEditorSet = new DatAdmin.CodeEditor();
            this.codeEditorGet = new DatAdmin.CodeEditor();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "s_dialect";
            // 
            // cbxDialect
            // 
            this.cbxDialect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDialect.FormattingEnabled = true;
            this.cbxDialect.Location = new System.Drawing.Point(68, 21);
            this.cbxDialect.Name = "cbxDialect";
            this.cbxDialect.Size = new System.Drawing.Size(224, 21);
            this.cbxDialect.TabIndex = 1;
            // 
            // chbForceSingleSchema
            // 
            this.chbForceSingleSchema.AutoSize = true;
            this.chbForceSingleSchema.Location = new System.Drawing.Point(16, 48);
            this.chbForceSingleSchema.Name = "chbForceSingleSchema";
            this.chbForceSingleSchema.Size = new System.Drawing.Size(137, 17);
            this.chbForceSingleSchema.TabIndex = 2;
            this.chbForceSingleSchema.Text = "s_force_single_schema";
            this.chbForceSingleSchema.UseVisualStyleBackColor = true;
            // 
            // chbCreateDefaultVariables
            // 
            this.chbCreateDefaultVariables.AutoSize = true;
            this.chbCreateDefaultVariables.Location = new System.Drawing.Point(16, 71);
            this.chbCreateDefaultVariables.Name = "chbCreateDefaultVariables";
            this.chbCreateDefaultVariables.Size = new System.Drawing.Size(182, 17);
            this.chbCreateDefaultVariables.TabIndex = 7;
            this.chbCreateDefaultVariables.Text = "s_create_default_variables_table";
            this.chbCreateDefaultVariables.UseVisualStyleBackColor = true;
            this.chbCreateDefaultVariables.CheckedChanged += new System.EventHandler(this.chbCreateDefaultVariables_CheckedChanged);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(384, 361);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "s_ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(465, 361);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "s_cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.infoBoxFrame2);
            this.panel1.Controls.Add(this.infoBoxFrame1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.codeEditorSet);
            this.panel1.Controls.Add(this.codeEditorGet);
            this.panel1.Location = new System.Drawing.Point(-3, 106);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(567, 249);
            this.panel1.TabIndex = 12;
            // 
            // infoBoxFrame2
            // 
            this.infoBoxFrame2.InfoText = "s_set_sql_version_desc";
            this.infoBoxFrame2.Location = new System.Drawing.Point(338, 136);
            this.infoBoxFrame2.Name = "infoBoxFrame2";
            this.infoBoxFrame2.Padding = new System.Windows.Forms.Padding(8);
            this.infoBoxFrame2.Size = new System.Drawing.Size(216, 97);
            this.infoBoxFrame2.TabIndex = 15;
            // 
            // infoBoxFrame1
            // 
            this.infoBoxFrame1.InfoText = "s_get_sql_version_desc";
            this.infoBoxFrame1.Location = new System.Drawing.Point(338, 16);
            this.infoBoxFrame1.Name = "infoBoxFrame1";
            this.infoBoxFrame1.Padding = new System.Windows.Forms.Padding(8);
            this.infoBoxFrame1.Size = new System.Drawing.Size(216, 97);
            this.infoBoxFrame1.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "s_set_version_sql";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "s_get_version_sql";
            // 
            // codeEditorSet
            // 
            this.codeEditorSet.Dialect = null;
            this.codeEditorSet.IsReadOnly = false;
            this.codeEditorSet.Language = DatAdmin.CodeLanguage.Sql;
            this.codeEditorSet.Location = new System.Drawing.Point(12, 155);
            this.codeEditorSet.Modified = true;
            this.codeEditorSet.Name = "codeEditorSet";
            this.codeEditorSet.ReadOnly = false;
            this.codeEditorSet.Size = new System.Drawing.Size(309, 78);
            this.codeEditorSet.TabIndex = 11;
            this.codeEditorSet.Text = "codeEditor2";
            // 
            // codeEditorGet
            // 
            this.codeEditorGet.Dialect = null;
            this.codeEditorGet.IsReadOnly = false;
            this.codeEditorGet.Language = DatAdmin.CodeLanguage.Sql;
            this.codeEditorGet.Location = new System.Drawing.Point(12, 35);
            this.codeEditorGet.Modified = true;
            this.codeEditorGet.Name = "codeEditorGet";
            this.codeEditorGet.ReadOnly = false;
            this.codeEditorGet.Size = new System.Drawing.Size(309, 78);
            this.codeEditorGet.TabIndex = 10;
            this.codeEditorGet.Text = "codeEditor1";
            // 
            // VersionDbPropsForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(564, 396);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.chbCreateDefaultVariables);
            this.Controls.Add(this.chbForceSingleSchema);
            this.Controls.Add(this.cbxDialect);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VersionDbPropsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_properties";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxDialect;
        private System.Windows.Forms.CheckBox chbForceSingleSchema;
        private System.Windows.Forms.CheckBox chbCreateDefaultVariables;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private DatAdmin.InfoBoxFrame infoBoxFrame2;
        private DatAdmin.InfoBoxFrame infoBoxFrame1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DatAdmin.CodeEditor codeEditorSet;
        private DatAdmin.CodeEditor codeEditorGet;
    }
}