namespace DatAdmin
{
    partial class RunProcessForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunProcessForm));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnStopProcess = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labState = new System.Windows.Forms.Label();
            this.cbxSendError = new System.Windows.Forms.CheckBox();
            this.labJob = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.labCurWork = new System.Windows.Forms.TextBox();
            this.labDuration = new System.Windows.Forms.TextBox();
            this.labProcessName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.messageLogFrame1 = new DatAdmin.MessageLogFrame();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnStopProcess);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 403);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(593, 47);
            this.panel1.TabIndex = 11;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(450, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(129, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "s_on_background";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnStopProcess
            // 
            this.btnStopProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStopProcess.Image = global::DatAdmin.StdIcons.stop;
            this.btnStopProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStopProcess.Location = new System.Drawing.Point(308, 12);
            this.btnStopProcess.Name = "btnStopProcess";
            this.btnStopProcess.Size = new System.Drawing.Size(136, 23);
            this.btnStopProcess.TabIndex = 5;
            this.btnStopProcess.Text = "s_stop_process";
            this.btnStopProcess.UseVisualStyleBackColor = true;
            this.btnStopProcess.Click += new System.EventHandler(this.btcancel_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.labState);
            this.panel2.Controls.Add(this.cbxSendError);
            this.panel2.Controls.Add(this.labJob);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.labCurWork);
            this.panel2.Controls.Add(this.labDuration);
            this.panel2.Controls.Add(this.labProcessName);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(593, 105);
            this.panel2.TabIndex = 12;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(130, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 32;
            this.pictureBox1.TabStop = false;
            // 
            // labState
            // 
            this.labState.AutoSize = true;
            this.labState.Location = new System.Drawing.Point(152, 35);
            this.labState.Name = "labState";
            this.labState.Size = new System.Drawing.Size(35, 13);
            this.labState.TabIndex = 31;
            this.labState.Text = "label6";
            // 
            // cbxSendError
            // 
            this.cbxSendError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxSendError.AutoSize = true;
            this.cbxSendError.Checked = true;
            this.cbxSendError.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxSendError.Location = new System.Drawing.Point(360, 79);
            this.cbxSendError.Name = "cbxSendError";
            this.cbxSendError.Size = new System.Drawing.Size(132, 17);
            this.cbxSendError.TabIndex = 30;
            this.cbxSendError.Text = "s_allow_to_send_error";
            this.cbxSendError.UseVisualStyleBackColor = true;
            this.cbxSendError.Visible = false;
            // 
            // labJob
            // 
            this.labJob.Location = new System.Drawing.Point(130, 76);
            this.labJob.Name = "labJob";
            this.labJob.ReadOnly = true;
            this.labJob.Size = new System.Drawing.Size(160, 20);
            this.labJob.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "s_job";
            // 
            // labCurWork
            // 
            this.labCurWork.Location = new System.Drawing.Point(130, 54);
            this.labCurWork.Name = "labCurWork";
            this.labCurWork.ReadOnly = true;
            this.labCurWork.Size = new System.Drawing.Size(160, 20);
            this.labCurWork.TabIndex = 27;
            // 
            // labDuration
            // 
            this.labDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labDuration.Location = new System.Drawing.Point(419, 32);
            this.labDuration.Name = "labDuration";
            this.labDuration.ReadOnly = true;
            this.labDuration.Size = new System.Drawing.Size(160, 20);
            this.labDuration.TabIndex = 26;
            // 
            // labProcessName
            // 
            this.labProcessName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labProcessName.Location = new System.Drawing.Point(130, 9);
            this.labProcessName.Name = "labProcessName";
            this.labProcessName.ReadOnly = true;
            this.labProcessName.Size = new System.Drawing.Size(449, 20);
            this.labProcessName.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "s_state";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "s_curwork";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(357, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "s_duration";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "s_name";
            // 
            // messageLogFrame1
            // 
            this.messageLogFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageLogFrame1.Location = new System.Drawing.Point(0, 105);
            this.messageLogFrame1.Name = "messageLogFrame1";
            this.messageLogFrame1.Size = new System.Drawing.Size(593, 298);
            this.messageLogFrame1.Source = null;
            this.messageLogFrame1.Style = DatAdmin.MessageFrameStyle.List;
            this.messageLogFrame1.TabIndex = 13;
            // 
            // RunProcessForm
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(593, 450);
            this.Controls.Add(this.messageLogFrame1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RunProcessForm";
            this.Text = "s_run_process";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RunProcessForm_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnStopProcess;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox labProcessName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox labCurWork;
        private System.Windows.Forms.TextBox labDuration;
        private DatAdmin.MessageLogFrame messageLogFrame1;
        private System.Windows.Forms.TextBox labJob;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbxSendError;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labState;
    }
}