namespace DatAdmin
{
    partial class DataStoreFrame
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
            this.addonSelectFrame1 = new DatAdmin.AddonSelectFrame();
            this.propertyFrame1 = new DatAdmin.PropertyFrame();
            this.SuspendLayout();
            // 
            // addonSelectFrame1
            // 
            this.addonSelectFrame1.AddonTypeName = "datastore";
            this.addonSelectFrame1.AllowSwitchDesign = true;
            this.addonSelectFrame1.CanSaveAsTemplate = true;
            this.addonSelectFrame1.CompactDesign = true;
            this.addonSelectFrame1.DefaultAddonHolder = "csv";
            this.addonSelectFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addonSelectFrame1.Location = new System.Drawing.Point(0, 0);
            this.addonSelectFrame1.Name = "addonSelectFrame1";
            this.addonSelectFrame1.ShowInfoFrame = true;
            this.addonSelectFrame1.Size = new System.Drawing.Size(420, 380);
            this.addonSelectFrame1.TabIndex = 0;
            this.addonSelectFrame1.TypeTitle = "s_type";
            this.addonSelectFrame1.CreateSpecialItem += new DatAdmin.CreateSpecialItemEvent(this.addonSelectFrame1_CreateSpecialItem);
            this.addonSelectFrame1.ChangedSelectedObject += new System.EventHandler(this.addonSelectFrame1_ChangedSelectedObject);
            this.addonSelectFrame1.FilterAddon += new DatAdmin.FilterAddonEvent(this.addonSelectFrame1_FilterAddon);
            this.addonSelectFrame1.GetSpecialItems += new DatAdmin.GetSpecialItemsEvent(this.addonSelectFrame1_GetSpecialItems);
            // 
            // propertyFrame1
            // 
            this.propertyFrame1.CacheCustomEditors = false;
            this.propertyFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyFrame1.Location = new System.Drawing.Point(0, 0);
            this.propertyFrame1.Name = "propertyFrame1";
            this.propertyFrame1.SelectedObject = null;
            this.propertyFrame1.Size = new System.Drawing.Size(420, 380);
            this.propertyFrame1.TabIndex = 1;
            // 
            // DataStoreFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addonSelectFrame1);
            this.Controls.Add(this.propertyFrame1);
            this.Name = "DataStoreFrame";
            this.Size = new System.Drawing.Size(420, 380);
            this.ResumeLayout(false);

        }

        #endregion

        private AddonSelectFrame addonSelectFrame1;
        private PropertyFrame propertyFrame1;

    }
}
