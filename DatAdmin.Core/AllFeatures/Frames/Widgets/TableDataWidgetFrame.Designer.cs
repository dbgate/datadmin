namespace DatAdmin
{
    partial class TableDataWidgetFrame
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
            this.tableDataFrame1 = new DatAdmin.TableDataFrame();
            this.SuspendLayout();
            // 
            // tableDataFrame1
            // 
            this.tableDataFrame1.Detached = false;
            this.tableDataFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableDataFrame1.IsContentVisible = false;
            this.tableDataFrame1.IsLoadingIcon = false;
            this.tableDataFrame1.Location = new System.Drawing.Point(0, 0);
            this.tableDataFrame1.Name = "tableDataFrame1";
            this.tableDataFrame1.Size = new System.Drawing.Size(708, 471);
            this.tableDataFrame1.TabIndex = 0;
            this.tableDataFrame1.TabularData = null;
            this.tableDataFrame1.WinId = null;
            this.tableDataFrame1.ReportError += new DatAdmin.ReportErrorDelegate(this.tableDataFrame1_ReportError);
            // 
            // TableDataWidgetFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableDataFrame1);
            this.Name = "TableDataWidgetFrame";
            this.Size = new System.Drawing.Size(708, 471);
            this.ResumeLayout(false);

        }

        #endregion

        private TableDataFrame tableDataFrame1;
    }
}
