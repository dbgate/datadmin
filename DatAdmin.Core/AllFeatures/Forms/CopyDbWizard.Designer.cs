namespace DatAdmin
{
    partial class CopyDbWizard
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
            DatAdmin.TreeBehaviour treeBehaviour2 = new DatAdmin.TreeBehaviour();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CopyDbWizard));
            DatAdmin.TreeBehaviour treeBehaviour1 = new DatAdmin.TreeBehaviour();
            this.wizard1 = new Gui.Wizard.Wizard();
            this.wpFinish = new Gui.Wizard.WizardPage();
            this.btnExportAsJob = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.labProperties = new System.Windows.Forms.TextBox();
            this.labTarget = new System.Windows.Forms.TextBox();
            this.labSource = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.header4 = new Gui.Wizard.Header();
            this.wpDataProps = new Gui.Wizard.WizardPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRemoveAllTables = new System.Windows.Forms.Button();
            this.btnRemoveTable = new System.Windows.Forms.Button();
            this.btnAddAllTables = new System.Windows.Forms.Button();
            this.btnAddTable = new System.Windows.Forms.Button();
            this.lbxSelectedTables = new System.Windows.Forms.ListBox();
            this.lbxAllTables = new System.Windows.Forms.ListBox();
            this.btnMoveTableDown = new System.Windows.Forms.Button();
            this.btnMoveTableUp = new System.Windows.Forms.Button();
            this.infoBoxFrameNoStructureWarning = new DatAdmin.InfoBoxFrame();
            this.tableCopyOptionsFrame1 = new DatAdmin.TableCopyOptionsFrame();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbtCopyUnselected = new System.Windows.Forms.RadioButton();
            this.rbtCopySelected = new System.Windows.Forms.RadioButton();
            this.rbtCopyAll = new System.Windows.Forms.RadioButton();
            this.rbtCopyNone = new System.Windows.Forms.RadioButton();
            this.header5 = new Gui.Wizard.Header();
            this.wpStructProps = new Gui.Wizard.WizardPage();
            this.chbCopyStructure = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbOriginalSchema = new System.Windows.Forms.RadioButton();
            this.cbxExplicitSchema = new System.Windows.Forms.ComboBox();
            this.rbExplicitSchema = new System.Windows.Forms.RadioButton();
            this.rbDefaultSchema = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.treObjects = new DatAdmin.DATreeView();
            this.header1 = new Gui.Wizard.Header();
            this.wpTarget = new Gui.Wizard.WizardPage();
            this.addonSelectFrame1 = new DatAdmin.AddonSelectFrame();
            this.header3 = new Gui.Wizard.Header();
            this.wpSource = new Gui.Wizard.WizardPage();
            this.treSource = new DatAdmin.DATreeView();
            this.header2 = new Gui.Wizard.Header();
            this.wizard1.SuspendLayout();
            this.wpFinish.SuspendLayout();
            this.wpDataProps.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.wpStructProps.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.wpTarget.SuspendLayout();
            this.wpSource.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizard1
            // 
            this.wizard1.Controls.Add(this.wpTarget);
            this.wizard1.Controls.Add(this.wpSource);
            this.wizard1.Controls.Add(this.wpStructProps);
            this.wizard1.Controls.Add(this.wpDataProps);
            this.wizard1.Controls.Add(this.wpFinish);
            this.wizard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizard1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizard1.Location = new System.Drawing.Point(0, 0);
            this.wizard1.Name = "wizard1";
            this.wizard1.PageIndex = 1;
            this.wizard1.Pages.AddRange(new Gui.Wizard.WizardPage[] {
            this.wpSource,
            this.wpTarget,
            this.wpStructProps,
            this.wpDataProps,
            this.wpFinish});
            this.wizard1.Size = new System.Drawing.Size(629, 550);
            this.wizard1.TabIndex = 0;
            this.wizard1.OnTranslate += new Gui.Wizard.TranslateEventHandler(this.wizard1_OnTranslate);
            // 
            // wpFinish
            // 
            this.wpFinish.Controls.Add(this.btnExportAsJob);
            this.wpFinish.Controls.Add(this.label5);
            this.wpFinish.Controls.Add(this.labProperties);
            this.wpFinish.Controls.Add(this.labTarget);
            this.wpFinish.Controls.Add(this.labSource);
            this.wpFinish.Controls.Add(this.label4);
            this.wpFinish.Controls.Add(this.label3);
            this.wpFinish.Controls.Add(this.header4);
            this.wpFinish.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpFinish.IsFinishPage = true;
            this.wpFinish.Location = new System.Drawing.Point(0, 0);
            this.wpFinish.Name = "wpFinish";
            this.wpFinish.Size = new System.Drawing.Size(629, 502);
            this.wpFinish.TabIndex = 4;
            this.wpFinish.CloseFromNext += new Gui.Wizard.PageEventHandler(this.wpFinish_CloseFromNext);
            this.wpFinish.ShowFromNext += new System.EventHandler(this.wpFinish_ShowFromNext);
            // 
            // btnExportAsJob
            // 
            this.btnExportAsJob.Location = new System.Drawing.Point(402, 459);
            this.btnExportAsJob.Name = "btnExportAsJob";
            this.btnExportAsJob.Size = new System.Drawing.Size(215, 23);
            this.btnExportAsJob.TabIndex = 8;
            this.btnExportAsJob.Text = "s_export_as_job";
            this.btnExportAsJob.UseVisualStyleBackColor = true;
            this.btnExportAsJob.Click += new System.EventHandler(this.btnExportAsJob_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "s_properties";
            // 
            // labProperties
            // 
            this.labProperties.Location = new System.Drawing.Point(119, 168);
            this.labProperties.Multiline = true;
            this.labProperties.Name = "labProperties";
            this.labProperties.ReadOnly = true;
            this.labProperties.Size = new System.Drawing.Size(498, 247);
            this.labProperties.TabIndex = 6;
            // 
            // labTarget
            // 
            this.labTarget.Location = new System.Drawing.Point(119, 112);
            this.labTarget.Name = "labTarget";
            this.labTarget.ReadOnly = true;
            this.labTarget.Size = new System.Drawing.Size(498, 21);
            this.labTarget.TabIndex = 5;
            // 
            // labSource
            // 
            this.labSource.Location = new System.Drawing.Point(119, 84);
            this.labSource.Name = "labSource";
            this.labSource.ReadOnly = true;
            this.labSource.Size = new System.Drawing.Size(498, 21);
            this.labSource.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "s_target";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "s_source";
            // 
            // header4
            // 
            this.header4.BackColor = System.Drawing.SystemColors.Control;
            this.header4.CausesValidation = false;
            this.header4.Description = "s_finish_copydb_desc";
            this.header4.Dock = System.Windows.Forms.DockStyle.Top;
            this.header4.Image = ((System.Drawing.Image)(resources.GetObject("header4.Image")));
            this.header4.Location = new System.Drawing.Point(0, 0);
            this.header4.Name = "header4";
            this.header4.Size = new System.Drawing.Size(629, 64);
            this.header4.TabIndex = 1;
            this.header4.Title = "s_finish_copydb";
            // 
            // wpDataProps
            // 
            this.wpDataProps.Controls.Add(this.label6);
            this.wpDataProps.Controls.Add(this.label1);
            this.wpDataProps.Controls.Add(this.btnRemoveAllTables);
            this.wpDataProps.Controls.Add(this.btnRemoveTable);
            this.wpDataProps.Controls.Add(this.btnAddAllTables);
            this.wpDataProps.Controls.Add(this.btnAddTable);
            this.wpDataProps.Controls.Add(this.lbxSelectedTables);
            this.wpDataProps.Controls.Add(this.lbxAllTables);
            this.wpDataProps.Controls.Add(this.btnMoveTableDown);
            this.wpDataProps.Controls.Add(this.btnMoveTableUp);
            this.wpDataProps.Controls.Add(this.infoBoxFrameNoStructureWarning);
            this.wpDataProps.Controls.Add(this.tableCopyOptionsFrame1);
            this.wpDataProps.Controls.Add(this.groupBox3);
            this.wpDataProps.Controls.Add(this.header5);
            this.wpDataProps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpDataProps.IsFinishPage = false;
            this.wpDataProps.Location = new System.Drawing.Point(0, 0);
            this.wpDataProps.Name = "wpDataProps";
            this.wpDataProps.Size = new System.Drawing.Size(629, 502);
            this.wpDataProps.TabIndex = 5;
            this.wpDataProps.ShowFromBack += new System.EventHandler(this.wpDataProps_ShowFromBack);
            this.wpDataProps.ShowFromNext += new System.EventHandler(this.wpDataProps_ShowFromNext);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(326, 275);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "s_selected_tables";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 275);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "s_all_tables";
            // 
            // btnRemoveAllTables
            // 
            this.btnRemoveAllTables.Location = new System.Drawing.Point(213, 399);
            this.btnRemoveAllTables.Name = "btnRemoveAllTables";
            this.btnRemoveAllTables.Size = new System.Drawing.Size(111, 23);
            this.btnRemoveAllTables.TabIndex = 17;
            this.btnRemoveAllTables.Text = "<<";
            this.btnRemoveAllTables.UseVisualStyleBackColor = true;
            this.btnRemoveAllTables.Click += new System.EventHandler(this.btnRemoveAllTables_Click);
            // 
            // btnRemoveTable
            // 
            this.btnRemoveTable.Location = new System.Drawing.Point(213, 370);
            this.btnRemoveTable.Name = "btnRemoveTable";
            this.btnRemoveTable.Size = new System.Drawing.Size(110, 23);
            this.btnRemoveTable.TabIndex = 16;
            this.btnRemoveTable.Text = "<";
            this.btnRemoveTable.UseVisualStyleBackColor = true;
            this.btnRemoveTable.Click += new System.EventHandler(this.btnRemoveTable_Click);
            // 
            // btnAddAllTables
            // 
            this.btnAddAllTables.Location = new System.Drawing.Point(212, 323);
            this.btnAddAllTables.Name = "btnAddAllTables";
            this.btnAddAllTables.Size = new System.Drawing.Size(111, 23);
            this.btnAddAllTables.TabIndex = 15;
            this.btnAddAllTables.Text = ">>";
            this.btnAddAllTables.UseVisualStyleBackColor = true;
            this.btnAddAllTables.Click += new System.EventHandler(this.btnAddAllTables_Click);
            // 
            // btnAddTable
            // 
            this.btnAddTable.Location = new System.Drawing.Point(212, 294);
            this.btnAddTable.Name = "btnAddTable";
            this.btnAddTable.Size = new System.Drawing.Size(111, 23);
            this.btnAddTable.TabIndex = 14;
            this.btnAddTable.Text = ">";
            this.btnAddTable.UseVisualStyleBackColor = true;
            this.btnAddTable.Click += new System.EventHandler(this.btnAddTable_Click);
            // 
            // lbxSelectedTables
            // 
            this.lbxSelectedTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbxSelectedTables.FormattingEnabled = true;
            this.lbxSelectedTables.Location = new System.Drawing.Point(329, 294);
            this.lbxSelectedTables.Name = "lbxSelectedTables";
            this.lbxSelectedTables.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbxSelectedTables.Size = new System.Drawing.Size(173, 199);
            this.lbxSelectedTables.TabIndex = 13;
            this.lbxSelectedTables.DoubleClick += new System.EventHandler(this.btnRemoveTable_Click);
            // 
            // lbxAllTables
            // 
            this.lbxAllTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbxAllTables.FormattingEnabled = true;
            this.lbxAllTables.Location = new System.Drawing.Point(30, 294);
            this.lbxAllTables.Name = "lbxAllTables";
            this.lbxAllTables.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbxAllTables.Size = new System.Drawing.Size(176, 199);
            this.lbxAllTables.TabIndex = 12;
            this.lbxAllTables.DoubleClick += new System.EventHandler(this.btnAddTable_Click);
            // 
            // btnMoveTableDown
            // 
            this.btnMoveTableDown.Image = global::DatAdmin.StdIcons.down1;
            this.btnMoveTableDown.Location = new System.Drawing.Point(213, 470);
            this.btnMoveTableDown.Name = "btnMoveTableDown";
            this.btnMoveTableDown.Size = new System.Drawing.Size(111, 23);
            this.btnMoveTableDown.TabIndex = 11;
            this.btnMoveTableDown.Text = "s_move_down";
            this.btnMoveTableDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMoveTableDown.UseVisualStyleBackColor = true;
            this.btnMoveTableDown.Click += new System.EventHandler(this.btnMoveTableDown_Click);
            // 
            // btnMoveTableUp
            // 
            this.btnMoveTableUp.Enabled = false;
            this.btnMoveTableUp.Image = global::DatAdmin.StdIcons.up1;
            this.btnMoveTableUp.Location = new System.Drawing.Point(213, 441);
            this.btnMoveTableUp.Name = "btnMoveTableUp";
            this.btnMoveTableUp.Size = new System.Drawing.Size(111, 23);
            this.btnMoveTableUp.TabIndex = 10;
            this.btnMoveTableUp.Text = "s_move_up";
            this.btnMoveTableUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMoveTableUp.UseVisualStyleBackColor = true;
            this.btnMoveTableUp.Click += new System.EventHandler(this.btnMoveTableUp_Click);
            // 
            // infoBoxFrameNoStructureWarning
            // 
            this.infoBoxFrameNoStructureWarning.InfoText = "s_no_structure_warning";
            this.infoBoxFrameNoStructureWarning.Location = new System.Drawing.Point(310, 181);
            this.infoBoxFrameNoStructureWarning.Name = "infoBoxFrameNoStructureWarning";
            this.infoBoxFrameNoStructureWarning.Padding = new System.Windows.Forms.Padding(8);
            this.infoBoxFrameNoStructureWarning.Size = new System.Drawing.Size(268, 91);
            this.infoBoxFrameNoStructureWarning.TabIndex = 9;
            this.infoBoxFrameNoStructureWarning.Visible = false;
            // 
            // tableCopyOptionsFrame1
            // 
            this.tableCopyOptionsFrame1.Location = new System.Drawing.Point(320, 80);
            this.tableCopyOptionsFrame1.Name = "tableCopyOptionsFrame1";
            this.tableCopyOptionsFrame1.Size = new System.Drawing.Size(249, 94);
            this.tableCopyOptionsFrame1.TabIndex = 6;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbtCopyUnselected);
            this.groupBox3.Controls.Add(this.rbtCopySelected);
            this.groupBox3.Controls.Add(this.rbtCopyAll);
            this.groupBox3.Controls.Add(this.rbtCopyNone);
            this.groupBox3.Location = new System.Drawing.Point(12, 80);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(268, 124);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "s_what_copy";
            // 
            // rbtCopyUnselected
            // 
            this.rbtCopyUnselected.AutoSize = true;
            this.rbtCopyUnselected.Location = new System.Drawing.Point(7, 93);
            this.rbtCopyUnselected.Name = "rbtCopyUnselected";
            this.rbtCopyUnselected.Size = new System.Drawing.Size(131, 17);
            this.rbtCopyUnselected.TabIndex = 3;
            this.rbtCopyUnselected.TabStop = true;
            this.rbtCopyUnselected.Text = "s_all_except_selected";
            this.rbtCopyUnselected.UseVisualStyleBackColor = true;
            this.rbtCopyUnselected.CheckedChanged += new System.EventHandler(this.rbtCopyXXX_CheckedChanged);
            // 
            // rbtCopySelected
            // 
            this.rbtCopySelected.AutoSize = true;
            this.rbtCopySelected.Location = new System.Drawing.Point(7, 69);
            this.rbtCopySelected.Name = "rbtCopySelected";
            this.rbtCopySelected.Size = new System.Drawing.Size(111, 17);
            this.rbtCopySelected.TabIndex = 2;
            this.rbtCopySelected.Text = "s_selected_tables";
            this.rbtCopySelected.UseVisualStyleBackColor = true;
            this.rbtCopySelected.CheckedChanged += new System.EventHandler(this.rbtCopyXXX_CheckedChanged);
            // 
            // rbtCopyAll
            // 
            this.rbtCopyAll.AutoSize = true;
            this.rbtCopyAll.Location = new System.Drawing.Point(7, 45);
            this.rbtCopyAll.Name = "rbtCopyAll";
            this.rbtCopyAll.Size = new System.Drawing.Size(81, 17);
            this.rbtCopyAll.TabIndex = 1;
            this.rbtCopyAll.Text = "s_all_tables";
            this.rbtCopyAll.UseVisualStyleBackColor = true;
            this.rbtCopyAll.CheckedChanged += new System.EventHandler(this.rbtCopyXXX_CheckedChanged);
            // 
            // rbtCopyNone
            // 
            this.rbtCopyNone.AutoSize = true;
            this.rbtCopyNone.Checked = true;
            this.rbtCopyNone.Location = new System.Drawing.Point(7, 21);
            this.rbtCopyNone.Name = "rbtCopyNone";
            this.rbtCopyNone.Size = new System.Drawing.Size(60, 17);
            this.rbtCopyNone.TabIndex = 0;
            this.rbtCopyNone.TabStop = true;
            this.rbtCopyNone.Text = "s_none";
            this.rbtCopyNone.UseVisualStyleBackColor = true;
            this.rbtCopyNone.CheckedChanged += new System.EventHandler(this.rbtCopyXXX_CheckedChanged);
            // 
            // header5
            // 
            this.header5.BackColor = System.Drawing.SystemColors.Control;
            this.header5.CausesValidation = false;
            this.header5.Description = "s_configure_data_copy_properties";
            this.header5.Dock = System.Windows.Forms.DockStyle.Top;
            this.header5.Image = ((System.Drawing.Image)(resources.GetObject("header5.Image")));
            this.header5.Location = new System.Drawing.Point(0, 0);
            this.header5.Name = "header5";
            this.header5.Size = new System.Drawing.Size(629, 64);
            this.header5.TabIndex = 0;
            this.header5.Title = "s_data_copy_properties";
            // 
            // wpStructProps
            // 
            this.wpStructProps.Controls.Add(this.chbCopyStructure);
            this.wpStructProps.Controls.Add(this.groupBox2);
            this.wpStructProps.Controls.Add(this.label2);
            this.wpStructProps.Controls.Add(this.treObjects);
            this.wpStructProps.Controls.Add(this.header1);
            this.wpStructProps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpStructProps.IsFinishPage = false;
            this.wpStructProps.Location = new System.Drawing.Point(0, 0);
            this.wpStructProps.Name = "wpStructProps";
            this.wpStructProps.Size = new System.Drawing.Size(629, 502);
            this.wpStructProps.TabIndex = 3;
            this.wpStructProps.ShowFromBack += new System.EventHandler(this.wpStructProps_ShowFromBack);
            this.wpStructProps.ShowFromNext += new System.EventHandler(this.wpProps_ShowFromNext);
            // 
            // chbCopyStructure
            // 
            this.chbCopyStructure.AutoSize = true;
            this.chbCopyStructure.Checked = true;
            this.chbCopyStructure.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbCopyStructure.Location = new System.Drawing.Point(25, 70);
            this.chbCopyStructure.Name = "chbCopyStructure";
            this.chbCopyStructure.Size = new System.Drawing.Size(110, 17);
            this.chbCopyStructure.TabIndex = 6;
            this.chbCopyStructure.Text = "s_copy_structure";
            this.chbCopyStructure.UseVisualStyleBackColor = true;
            this.chbCopyStructure.CheckedChanged += new System.EventHandler(this.chbCopyStructure_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbOriginalSchema);
            this.groupBox2.Controls.Add(this.cbxExplicitSchema);
            this.groupBox2.Controls.Add(this.rbExplicitSchema);
            this.groupBox2.Controls.Add(this.rbDefaultSchema);
            this.groupBox2.Location = new System.Drawing.Point(349, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(212, 133);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "s_target_schema";
            // 
            // rbOriginalSchema
            // 
            this.rbOriginalSchema.AutoSize = true;
            this.rbOriginalSchema.Location = new System.Drawing.Point(17, 96);
            this.rbOriginalSchema.Name = "rbOriginalSchema";
            this.rbOriginalSchema.Size = new System.Drawing.Size(93, 17);
            this.rbOriginalSchema.TabIndex = 3;
            this.rbOriginalSchema.Text = "s_use_original";
            this.rbOriginalSchema.UseVisualStyleBackColor = true;
            // 
            // cbxExplicitSchema
            // 
            this.cbxExplicitSchema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxExplicitSchema.FormattingEnabled = true;
            this.cbxExplicitSchema.Location = new System.Drawing.Point(28, 68);
            this.cbxExplicitSchema.Name = "cbxExplicitSchema";
            this.cbxExplicitSchema.Size = new System.Drawing.Size(142, 21);
            this.cbxExplicitSchema.TabIndex = 2;
            // 
            // rbExplicitSchema
            // 
            this.rbExplicitSchema.AutoSize = true;
            this.rbExplicitSchema.Location = new System.Drawing.Point(17, 45);
            this.rbExplicitSchema.Name = "rbExplicitSchema";
            this.rbExplicitSchema.Size = new System.Drawing.Size(76, 17);
            this.rbExplicitSchema.TabIndex = 1;
            this.rbExplicitSchema.Text = "s_use_this";
            this.rbExplicitSchema.UseVisualStyleBackColor = true;
            this.rbExplicitSchema.CheckedChanged += new System.EventHandler(this.rbExplicitSchema_CheckedChanged);
            // 
            // rbDefaultSchema
            // 
            this.rbDefaultSchema.AutoSize = true;
            this.rbDefaultSchema.Checked = true;
            this.rbDefaultSchema.Location = new System.Drawing.Point(17, 20);
            this.rbDefaultSchema.Name = "rbDefaultSchema";
            this.rbDefaultSchema.Size = new System.Drawing.Size(93, 17);
            this.rbDefaultSchema.TabIndex = 0;
            this.rbDefaultSchema.TabStop = true;
            this.rbDefaultSchema.Text = "s_use_default";
            this.rbDefaultSchema.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "s_choose_objects";
            // 
            // treObjects
            // 
            this.treObjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treObjects.Location = new System.Drawing.Point(25, 118);
            this.treObjects.Name = "treObjects";
            this.treObjects.Root = null;
            this.treObjects.RootPath = null;
            this.treObjects.Size = new System.Drawing.Size(288, 365);
            this.treObjects.TabIndex = 1;
            this.treObjects.TreeBehaviour = treeBehaviour2;
            this.treObjects.TreeStyle = DatAdmin.TreeStyle.SelectMany;
            // 
            // header1
            // 
            this.header1.BackColor = System.Drawing.SystemColors.Control;
            this.header1.CausesValidation = false;
            this.header1.Description = "s_configure_structure_copy_properties";
            this.header1.Dock = System.Windows.Forms.DockStyle.Top;
            this.header1.Image = ((System.Drawing.Image)(resources.GetObject("header1.Image")));
            this.header1.Location = new System.Drawing.Point(0, 0);
            this.header1.Name = "header1";
            this.header1.Size = new System.Drawing.Size(629, 64);
            this.header1.TabIndex = 0;
            this.header1.Title = "s_structure_copy_properties";
            // 
            // wpTarget
            // 
            this.wpTarget.Controls.Add(this.addonSelectFrame1);
            this.wpTarget.Controls.Add(this.header3);
            this.wpTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpTarget.IsFinishPage = false;
            this.wpTarget.Location = new System.Drawing.Point(0, 0);
            this.wpTarget.Name = "wpTarget";
            this.wpTarget.Size = new System.Drawing.Size(629, 502);
            this.wpTarget.TabIndex = 2;
            this.wpTarget.CloseFromNext += new Gui.Wizard.PageEventHandler(this.wpTarget_CloseFromNext);
            // 
            // addonSelectFrame1
            // 
            this.addonSelectFrame1.AddonTypeName = "dbwriter";
            this.addonSelectFrame1.AllowSwitchDesign = true;
            this.addonSelectFrame1.AutoScroll = true;
            this.addonSelectFrame1.CanSaveAsTemplate = false;
            this.addonSelectFrame1.CompactDesign = false;
            this.addonSelectFrame1.DefaultAddonHolder = "dbk_writer";
            this.addonSelectFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addonSelectFrame1.Location = new System.Drawing.Point(0, 64);
            this.addonSelectFrame1.Name = "addonSelectFrame1";
            this.addonSelectFrame1.ShowInfoFrame = true;
            this.addonSelectFrame1.Size = new System.Drawing.Size(629, 438);
            this.addonSelectFrame1.TabIndex = 2;
            this.addonSelectFrame1.TypeTitle = "s_type";
            this.addonSelectFrame1.CreateSpecialItem += new DatAdmin.CreateSpecialItemEvent(this.addonSelectFrame1_CreateSpecialItem);
            this.addonSelectFrame1.CreatedObject += new System.EventHandler(this.addonSelectFrame1_CreatedObject);
            this.addonSelectFrame1.GetSpecialItems += new DatAdmin.GetSpecialItemsEvent(this.addonSelectFrame1_GetSpecialItems);
            // 
            // header3
            // 
            this.header3.BackColor = System.Drawing.SystemColors.Control;
            this.header3.CausesValidation = false;
            this.header3.Description = "s_configure_target_properties";
            this.header3.Dock = System.Windows.Forms.DockStyle.Top;
            this.header3.Image = ((System.Drawing.Image)(resources.GetObject("header3.Image")));
            this.header3.Location = new System.Drawing.Point(0, 0);
            this.header3.Name = "header3";
            this.header3.Size = new System.Drawing.Size(629, 64);
            this.header3.TabIndex = 1;
            this.header3.Title = "s_target";
            // 
            // wpSource
            // 
            this.wpSource.Controls.Add(this.treSource);
            this.wpSource.Controls.Add(this.header2);
            this.wpSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpSource.IsFinishPage = false;
            this.wpSource.Location = new System.Drawing.Point(0, 0);
            this.wpSource.Name = "wpSource";
            this.wpSource.Size = new System.Drawing.Size(629, 502);
            this.wpSource.TabIndex = 1;
            this.wpSource.CloseFromNext += new Gui.Wizard.PageEventHandler(this.wpSource_CloseFromNext);
            // 
            // treSource
            // 
            this.treSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treSource.Location = new System.Drawing.Point(12, 82);
            this.treSource.Name = "treSource";
            this.treSource.Root = null;
            this.treSource.RootPath = null;
            this.treSource.Size = new System.Drawing.Size(597, 404);
            this.treSource.TabIndex = 2;
            this.treSource.TreeBehaviour = treeBehaviour1;
            this.treSource.TreeStyle = DatAdmin.TreeStyle.SelectOne;
            // 
            // header2
            // 
            this.header2.BackColor = System.Drawing.SystemColors.Control;
            this.header2.CausesValidation = false;
            this.header2.Description = "s_configure_source_properties";
            this.header2.Dock = System.Windows.Forms.DockStyle.Top;
            this.header2.Image = ((System.Drawing.Image)(resources.GetObject("header2.Image")));
            this.header2.Location = new System.Drawing.Point(0, 0);
            this.header2.Name = "header2";
            this.header2.Size = new System.Drawing.Size(629, 64);
            this.header2.TabIndex = 1;
            this.header2.Title = "s_source";
            // 
            // CopyDbWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 550);
            this.Controls.Add(this.wizard1);
            this.Name = "CopyDbWizard";
            this.Text = "s_copy_database";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CopyDbWizard_FormClosed);
            this.wizard1.ResumeLayout(false);
            this.wpFinish.ResumeLayout(false);
            this.wpFinish.PerformLayout();
            this.wpDataProps.ResumeLayout(false);
            this.wpDataProps.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.wpStructProps.ResumeLayout(false);
            this.wpStructProps.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.wpTarget.ResumeLayout(false);
            this.wpSource.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Gui.Wizard.Wizard wizard1;
        private Gui.Wizard.WizardPage wpSource;
        private Gui.Wizard.WizardPage wpTarget;
        private Gui.Wizard.Header header2;
        private DATreeView treSource;
        private Gui.Wizard.Header header3;
        private Gui.Wizard.WizardPage wpStructProps;
        private Gui.Wizard.Header header1;
        private System.Windows.Forms.Label label2;
        private DATreeView treObjects;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbExplicitSchema;
        private System.Windows.Forms.RadioButton rbDefaultSchema;
        private System.Windows.Forms.RadioButton rbOriginalSchema;
        private System.Windows.Forms.ComboBox cbxExplicitSchema;
        private Gui.Wizard.Header header4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox labProperties;
        private System.Windows.Forms.TextBox labTarget;
        private System.Windows.Forms.TextBox labSource;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnExportAsJob;
        private Gui.Wizard.WizardPage wpFinish;
        private AddonSelectFrame addonSelectFrame1;
        private System.Windows.Forms.CheckBox chbCopyStructure;
        private Gui.Wizard.WizardPage wpDataProps;
        private Gui.Wizard.Header header5;
        private System.Windows.Forms.GroupBox groupBox3;
        private TableCopyOptionsFrame tableCopyOptionsFrame1;
        private System.Windows.Forms.RadioButton rbtCopySelected;
        private System.Windows.Forms.RadioButton rbtCopyAll;
        private System.Windows.Forms.RadioButton rbtCopyNone;
        private System.Windows.Forms.RadioButton rbtCopyUnselected;
        private InfoBoxFrame infoBoxFrameNoStructureWarning;
        private System.Windows.Forms.Button btnMoveTableDown;
        private System.Windows.Forms.Button btnMoveTableUp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRemoveAllTables;
        private System.Windows.Forms.Button btnRemoveTable;
        private System.Windows.Forms.Button btnAddAllTables;
        private System.Windows.Forms.Button btnAddTable;
        private System.Windows.Forms.ListBox lbxSelectedTables;
        private System.Windows.Forms.ListBox lbxAllTables;
    }
}