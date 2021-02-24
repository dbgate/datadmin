namespace DatAdmin
{
    partial class FileNameEditorFrame
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
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogExe = new System.Windows.Forms.OpenFileDialog();
            this.addonSelectFrame1 = new DatAdmin.AddonSelectFrame();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // addonSelectFrame1
            // 
            this.addonSelectFrame1.AddonTypeName = "fileplace";
            this.addonSelectFrame1.AllowSwitchDesign = false;
            this.addonSelectFrame1.CanSaveAsTemplate = false;
            this.addonSelectFrame1.CompactDesign = true;
            this.addonSelectFrame1.DefaultAddonHolder = "filesystem";
            this.addonSelectFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addonSelectFrame1.Location = new System.Drawing.Point(0, 0);
            this.addonSelectFrame1.Name = "addonSelectFrame1";
            this.addonSelectFrame1.ShowInfoFrame = true;
            this.addonSelectFrame1.Size = new System.Drawing.Size(495, 340);
            this.addonSelectFrame1.TabIndex = 0;
            this.addonSelectFrame1.TypeTitle = "s_type";
            this.addonSelectFrame1.ChangedSelectedObject += new System.EventHandler(this.addonSelectFrame1_ChangedSelectedObject);
            this.addonSelectFrame1.FilterAddon += new DatAdmin.FilterAddonEvent(this.addonSelectFrame1_FilterAddon);
            // 
            // FileNameEditorFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addonSelectFrame1);
            this.Name = "FileNameEditorFrame";
            this.Size = new System.Drawing.Size(495, 340);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialogExe;
        private AddonSelectFrame addonSelectFrame1;
    }
}
