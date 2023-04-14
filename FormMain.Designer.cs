namespace AudioSplit
{
    partial class FormMain
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
            this.btnClear = new System.Windows.Forms.Button();
            this.txtTotalDuration = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnBrowseOutputFolder = new System.Windows.Forms.Button();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtOutputFilenameTemplate = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbOutputFormat = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpExcludeStop = new System.Windows.Forms.DateTimePicker();
            this.dtpExcludeStart = new System.Windows.Forms.DateTimePicker();
            this.chkExclude = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.udSplitSeconds = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.udSplitMinutes = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.udSplitHours = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.udSplitDays = new System.Windows.Forms.NumericUpDown();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBrowseInputFiles = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvInputFiles = new System.Windows.Forms.DataGridView();
            this.chkSplit = new System.Windows.Forms.CheckBox();
            this.btnBrowseExcludeFolder = new System.Windows.Forms.Button();
            this.txtExcludeFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.chkStartOnHour = new System.Windows.Forms.CheckBox();
            this.lblExampleOuputFilename = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cbChannels = new System.Windows.Forms.ComboBox();
            this.chkRemoveXingHeader = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtInputTemplate = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtSiteName = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectInputfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openlogFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showdateInternationalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTimeIn24HourFormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writelogFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.templateHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblResolvedExcludeFolder = new System.Windows.Forms.Label();
            this.lblResolvedOutputFolder = new System.Windows.Forms.Label();
            this.lblParseMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.udSplitSeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSplitMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSplitHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSplitDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInputFiles)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(180, 51);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(84, 33);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtTotalDuration
            // 
            this.txtTotalDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalDuration.Location = new System.Drawing.Point(801, 298);
            this.txtTotalDuration.Name = "txtTotalDuration";
            this.txtTotalDuration.ReadOnly = true;
            this.txtTotalDuration.Size = new System.Drawing.Size(122, 26);
            this.txtTotalDuration.TabIndex = 8;
            this.txtTotalDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(681, 301);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(110, 20);
            this.label12.TabIndex = 7;
            this.label12.Text = "Total duration:";
            // 
            // btnRun
            // 
            this.btnRun.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRun.Location = new System.Drawing.Point(430, 877);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(84, 33);
            this.btnRun.TabIndex = 47;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnBrowseOutputFolder
            // 
            this.btnBrowseOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseOutputFolder.Location = new System.Drawing.Point(839, 674);
            this.btnBrowseOutputFolder.Name = "btnBrowseOutputFolder";
            this.btnBrowseOutputFolder.Size = new System.Drawing.Size(84, 33);
            this.btnBrowseOutputFolder.TabIndex = 38;
            this.btnBrowseOutputFolder.Text = "Browse...";
            this.btnBrowseOutputFolder.UseVisualStyleBackColor = true;
            this.btnBrowseOutputFolder.Click += new System.EventHandler(this.btnBrowseOutputFolder_Click);
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputFolder.Location = new System.Drawing.Point(228, 677);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(604, 26);
            this.txtOutputFolder.TabIndex = 37;
            this.txtOutputFolder.Validated += new System.EventHandler(this.txtOutputFolder_Validated);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 680);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 20);
            this.label11.TabIndex = 36;
            this.label11.Text = "Output folder:";
            // 
            // txtOutputFilenameTemplate
            // 
            this.txtOutputFilenameTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputFilenameTemplate.Location = new System.Drawing.Point(228, 801);
            this.txtOutputFilenameTemplate.Name = "txtOutputFilenameTemplate";
            this.txtOutputFilenameTemplate.Size = new System.Drawing.Size(604, 26);
            this.txtOutputFilenameTemplate.TabIndex = 45;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 804);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(196, 20);
            this.label10.TabIndex = 44;
            this.label10.Text = "Output file name template:";
            // 
            // cbOutputFormat
            // 
            this.cbOutputFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbOutputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOutputFormat.FormattingEnabled = true;
            this.cbOutputFormat.Location = new System.Drawing.Point(228, 594);
            this.cbOutputFormat.Name = "cbOutputFormat";
            this.cbOutputFormat.Size = new System.Drawing.Size(136, 28);
            this.cbOutputFormat.TabIndex = 32;
            this.cbOutputFormat.SelectedIndexChanged += new System.EventHandler(this.cbOutputFormat_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 597);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(112, 20);
            this.label9.TabIndex = 31;
            this.label9.Text = "Output format:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(368, 553);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 20);
            this.label8.TabIndex = 29;
            this.label8.Text = "to";
            // 
            // dtpExcludeStop
            // 
            this.dtpExcludeStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpExcludeStop.CustomFormat = "HH:mm:ss";
            this.dtpExcludeStop.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpExcludeStop.Location = new System.Drawing.Point(399, 550);
            this.dtpExcludeStop.Name = "dtpExcludeStop";
            this.dtpExcludeStop.ShowUpDown = true;
            this.dtpExcludeStop.Size = new System.Drawing.Size(130, 26);
            this.dtpExcludeStop.TabIndex = 30;
            // 
            // dtpExcludeStart
            // 
            this.dtpExcludeStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpExcludeStart.CustomFormat = "HH:mm:ss";
            this.dtpExcludeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpExcludeStart.Location = new System.Drawing.Point(228, 550);
            this.dtpExcludeStart.Name = "dtpExcludeStart";
            this.dtpExcludeStart.ShowUpDown = true;
            this.dtpExcludeStart.Size = new System.Drawing.Size(130, 26);
            this.dtpExcludeStart.TabIndex = 28;
            // 
            // chkExclude
            // 
            this.chkExclude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkExclude.AutoSize = true;
            this.chkExclude.Location = new System.Drawing.Point(18, 554);
            this.chkExclude.Name = "chkExclude";
            this.chkExclude.Size = new System.Drawing.Size(200, 24);
            this.chkExclude.TabIndex = 27;
            this.chkExclude.Text = "Exclude data between: ";
            this.chkExclude.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(603, 469);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 20);
            this.label7.TabIndex = 24;
            this.label7.Text = "Seconds:";
            // 
            // udSplitSeconds
            // 
            this.udSplitSeconds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.udSplitSeconds.Location = new System.Drawing.Point(681, 466);
            this.udSplitSeconds.Name = "udSplitSeconds";
            this.udSplitSeconds.Size = new System.Drawing.Size(51, 26);
            this.udSplitSeconds.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(468, 469);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 20);
            this.label6.TabIndex = 22;
            this.label6.Text = "Minutes:";
            // 
            // udSplitMinutes
            // 
            this.udSplitMinutes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.udSplitMinutes.Location = new System.Drawing.Point(540, 466);
            this.udSplitMinutes.Name = "udSplitMinutes";
            this.udSplitMinutes.Size = new System.Drawing.Size(51, 26);
            this.udSplitMinutes.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(345, 469);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 20);
            this.label5.TabIndex = 20;
            this.label5.Text = "Hours:";
            // 
            // udSplitHours
            // 
            this.udSplitHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.udSplitHours.Location = new System.Drawing.Point(402, 466);
            this.udSplitHours.Name = "udSplitHours";
            this.udSplitHours.Size = new System.Drawing.Size(51, 26);
            this.udSplitHours.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(228, 469);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Days:";
            // 
            // udSplitDays
            // 
            this.udSplitDays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.udSplitDays.Location = new System.Drawing.Point(279, 466);
            this.udSplitDays.Name = "udSplitDays";
            this.udSplitDays.Size = new System.Drawing.Size(51, 26);
            this.udSplitDays.TabIndex = 19;
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpStartTime.CustomFormat = "HH:mm:ss";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpStartTime.Location = new System.Drawing.Point(363, 416);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.ShowUpDown = true;
            this.dtpStartTime.Size = new System.Drawing.Size(130, 26);
            this.dtpStartTime.TabIndex = 16;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpStartDate.CustomFormat = "yyyy-MM-dd";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(228, 416);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(127, 26);
            this.dtpStartDate.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 419);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "Start date and time:";
            // 
            // btnBrowseInputFiles
            // 
            this.btnBrowseInputFiles.Location = new System.Drawing.Point(282, 51);
            this.btnBrowseInputFiles.Name = "btnBrowseInputFiles";
            this.btnBrowseInputFiles.Size = new System.Drawing.Size(84, 33);
            this.btnBrowseInputFiles.TabIndex = 3;
            this.btnBrowseInputFiles.Text = "Browse...";
            this.btnBrowseInputFiles.UseVisualStyleBackColor = true;
            this.btnBrowseInputFiles.Click += new System.EventHandler(this.btnBrowseInputFiles_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Input files:";
            // 
            // dgvInputFiles
            // 
            this.dgvInputFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInputFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInputFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInputFiles.Location = new System.Drawing.Point(15, 89);
            this.dgvInputFiles.Name = "dgvInputFiles";
            this.dgvInputFiles.RowHeadersWidth = 62;
            this.dgvInputFiles.RowTemplate.Height = 24;
            this.dgvInputFiles.Size = new System.Drawing.Size(906, 202);
            this.dgvInputFiles.TabIndex = 6;
            this.dgvInputFiles.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvInputFiles_CellFormatting);
            // 
            // chkSplit
            // 
            this.chkSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkSplit.AutoSize = true;
            this.chkSplit.Location = new System.Drawing.Point(18, 469);
            this.chkSplit.Name = "chkSplit";
            this.chkSplit.Size = new System.Drawing.Size(198, 24);
            this.chkSplit.TabIndex = 17;
            this.chkSplit.Text = "Split into files of length:";
            this.chkSplit.UseVisualStyleBackColor = true;
            // 
            // btnBrowseExcludeFolder
            // 
            this.btnBrowseExcludeFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseExcludeFolder.Location = new System.Drawing.Point(838, 736);
            this.btnBrowseExcludeFolder.Name = "btnBrowseExcludeFolder";
            this.btnBrowseExcludeFolder.Size = new System.Drawing.Size(84, 33);
            this.btnBrowseExcludeFolder.TabIndex = 42;
            this.btnBrowseExcludeFolder.Text = "Browse...";
            this.btnBrowseExcludeFolder.UseVisualStyleBackColor = true;
            this.btnBrowseExcludeFolder.Click += new System.EventHandler(this.btnBrowseExcludeFolder_Click);
            // 
            // txtExcludeFolder
            // 
            this.txtExcludeFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExcludeFolder.Location = new System.Drawing.Point(228, 739);
            this.txtExcludeFolder.Name = "txtExcludeFolder";
            this.txtExcludeFolder.Size = new System.Drawing.Size(604, 26);
            this.txtExcludeFolder.TabIndex = 41;
            this.txtExcludeFolder.Validated += new System.EventHandler(this.txtExcludeFolder_Validated);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 742);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 20);
            this.label3.TabIndex = 40;
            this.label3.Text = "Exclude folder:";
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveUp.Location = new System.Drawing.Point(386, 51);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(31, 33);
            this.btnMoveUp.TabIndex = 4;
            this.btnMoveUp.Text = "▲";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveDown.Location = new System.Drawing.Point(424, 51);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(31, 33);
            this.btnMoveDown.TabIndex = 5;
            this.btnMoveDown.Text = "▼";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // chkStartOnHour
            // 
            this.chkStartOnHour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkStartOnHour.AutoSize = true;
            this.chkStartOnHour.Location = new System.Drawing.Point(18, 511);
            this.chkStartOnHour.Name = "chkStartOnHour";
            this.chkStartOnHour.Size = new System.Drawing.Size(187, 24);
            this.chkStartOnHour.TabIndex = 26;
            this.chkStartOnHour.Text = "Start files on the hour";
            this.chkStartOnHour.UseVisualStyleBackColor = true;
            // 
            // lblExampleOuputFilename
            // 
            this.lblExampleOuputFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblExampleOuputFilename.AutoEllipsis = true;
            this.lblExampleOuputFilename.Location = new System.Drawing.Point(228, 834);
            this.lblExampleOuputFilename.Name = "lblExampleOuputFilename";
            this.lblExampleOuputFilename.Size = new System.Drawing.Size(604, 22);
            this.lblExampleOuputFilename.TabIndex = 46;
            this.lblExampleOuputFilename.Text = "Output filename";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(18, 638);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 20);
            this.label14.TabIndex = 34;
            this.label14.Text = "Channels:";
            // 
            // cbChannels
            // 
            this.cbChannels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbChannels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChannels.FormattingEnabled = true;
            this.cbChannels.Location = new System.Drawing.Point(228, 635);
            this.cbChannels.Name = "cbChannels";
            this.cbChannels.Size = new System.Drawing.Size(136, 28);
            this.cbChannels.TabIndex = 35;
            this.cbChannels.SelectedIndexChanged += new System.EventHandler(this.cbChannels_SelectedIndexChanged);
            // 
            // chkRemoveXingHeader
            // 
            this.chkRemoveXingHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkRemoveXingHeader.AutoSize = true;
            this.chkRemoveXingHeader.Location = new System.Drawing.Point(410, 598);
            this.chkRemoveXingHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkRemoveXingHeader.Name = "chkRemoveXingHeader";
            this.chkRemoveXingHeader.Size = new System.Drawing.Size(184, 24);
            this.chkRemoveXingHeader.TabIndex = 33;
            this.chkRemoveXingHeader.Text = "Remove Xing header";
            this.chkRemoveXingHeader.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(19, 338);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(160, 20);
            this.label15.TabIndex = 9;
            this.label15.Text = "Input name template:";
            // 
            // txtInputTemplate
            // 
            this.txtInputTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInputTemplate.Location = new System.Drawing.Point(228, 335);
            this.txtInputTemplate.Name = "txtInputTemplate";
            this.txtInputTemplate.Size = new System.Drawing.Size(604, 26);
            this.txtInputTemplate.TabIndex = 10;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(19, 375);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(85, 20);
            this.label16.TabIndex = 12;
            this.label16.Text = "Site name:";
            // 
            // txtSiteName
            // 
            this.txtSiteName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSiteName.Location = new System.Drawing.Point(228, 372);
            this.txtSiteName.Name = "txtSiteName";
            this.txtSiteName.Size = new System.Drawing.Size(286, 26);
            this.txtSiteName.TabIndex = 13;
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(938, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectInputfileToolStripMenuItem,
            this.openlogFileToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // selectInputfileToolStripMenuItem
            // 
            this.selectInputfileToolStripMenuItem.Name = "selectInputfileToolStripMenuItem";
            this.selectInputfileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.selectInputfileToolStripMenuItem.Size = new System.Drawing.Size(299, 34);
            this.selectInputfileToolStripMenuItem.Text = "Select input &file";
            this.selectInputfileToolStripMenuItem.Click += new System.EventHandler(this.selectInputfileToolStripMenuItem_Click);
            // 
            // openlogFileToolStripMenuItem
            // 
            this.openlogFileToolStripMenuItem.Name = "openlogFileToolStripMenuItem";
            this.openlogFileToolStripMenuItem.Size = new System.Drawing.Size(299, 34);
            this.openlogFileToolStripMenuItem.Text = "Open &log file";
            this.openlogFileToolStripMenuItem.Click += new System.EventHandler(this.openlogFileToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(299, 34);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Checked = true;
            this.optionsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showdateInternationalToolStripMenuItem,
            this.showTimeIn24HourFormatToolStripMenuItem,
            this.writelogFileToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(92, 29);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // showdateInternationalToolStripMenuItem
            // 
            this.showdateInternationalToolStripMenuItem.CheckOnClick = true;
            this.showdateInternationalToolStripMenuItem.Name = "showdateInternationalToolStripMenuItem";
            this.showdateInternationalToolStripMenuItem.Size = new System.Drawing.Size(343, 34);
            this.showdateInternationalToolStripMenuItem.Text = "Show &date as yyyy-mm-dd";
            this.showdateInternationalToolStripMenuItem.CheckedChanged += new System.EventHandler(this.showdateInternationalToolStripMenuItem_CheckedChanged);
            // 
            // showTimeIn24HourFormatToolStripMenuItem
            // 
            this.showTimeIn24HourFormatToolStripMenuItem.CheckOnClick = true;
            this.showTimeIn24HourFormatToolStripMenuItem.Name = "showTimeIn24HourFormatToolStripMenuItem";
            this.showTimeIn24HourFormatToolStripMenuItem.Size = new System.Drawing.Size(343, 34);
            this.showTimeIn24HourFormatToolStripMenuItem.Text = "Show &time in 24 hour format";
            this.showTimeIn24HourFormatToolStripMenuItem.CheckedChanged += new System.EventHandler(this.showTimeIn24HourFormatToolStripMenuItem_CheckedChanged);
            // 
            // writelogFileToolStripMenuItem
            // 
            this.writelogFileToolStripMenuItem.CheckOnClick = true;
            this.writelogFileToolStripMenuItem.Name = "writelogFileToolStripMenuItem";
            this.writelogFileToolStripMenuItem.Size = new System.Drawing.Size(343, 34);
            this.writelogFileToolStripMenuItem.Text = "Write &log file";
            this.writelogFileToolStripMenuItem.Click += new System.EventHandler(this.writelogFileToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.templateHelpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // templateHelpToolStripMenuItem
            // 
            this.templateHelpToolStripMenuItem.Name = "templateHelpToolStripMenuItem";
            this.templateHelpToolStripMenuItem.Size = new System.Drawing.Size(316, 34);
            this.templateHelpToolStripMenuItem.Text = "&Template Quick Reference";
            this.templateHelpToolStripMenuItem.Click += new System.EventHandler(this.nameTemplateHelpToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(316, 34);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // lblResolvedExcludeFolder
            // 
            this.lblResolvedExcludeFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResolvedExcludeFolder.AutoEllipsis = true;
            this.lblResolvedExcludeFolder.Location = new System.Drawing.Point(228, 772);
            this.lblResolvedExcludeFolder.Name = "lblResolvedExcludeFolder";
            this.lblResolvedExcludeFolder.Size = new System.Drawing.Size(604, 22);
            this.lblResolvedExcludeFolder.TabIndex = 43;
            this.lblResolvedExcludeFolder.Text = "Exclude folder";
            // 
            // lblResolvedOutputFolder
            // 
            this.lblResolvedOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResolvedOutputFolder.AutoEllipsis = true;
            this.lblResolvedOutputFolder.Location = new System.Drawing.Point(228, 710);
            this.lblResolvedOutputFolder.Name = "lblResolvedOutputFolder";
            this.lblResolvedOutputFolder.Size = new System.Drawing.Size(604, 22);
            this.lblResolvedOutputFolder.TabIndex = 39;
            this.lblResolvedOutputFolder.Text = "Output folder";
            // 
            // lblParseMessage
            // 
            this.lblParseMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblParseMessage.ForeColor = System.Drawing.Color.Red;
            this.lblParseMessage.Location = new System.Drawing.Point(838, 338);
            this.lblParseMessage.Name = "lblParseMessage";
            this.lblParseMessage.Size = new System.Drawing.Size(85, 22);
            this.lblParseMessage.TabIndex = 11;
            this.lblParseMessage.Text = "Error";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 926);
            this.Controls.Add(this.lblParseMessage);
            this.Controls.Add(this.lblResolvedOutputFolder);
            this.Controls.Add(this.lblResolvedExcludeFolder);
            this.Controls.Add(this.txtSiteName);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtInputTemplate);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.chkRemoveXingHeader);
            this.Controls.Add(this.cbChannels);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblExampleOuputFilename);
            this.Controls.Add(this.chkStartOnHour);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnBrowseExcludeFolder);
            this.Controls.Add(this.txtExcludeFolder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkSplit);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtTotalDuration);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnBrowseOutputFolder);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtOutputFilenameTemplate);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cbOutputFormat);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtpExcludeStop);
            this.Controls.Add(this.dtpExcludeStart);
            this.Controls.Add(this.chkExclude);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.udSplitSeconds);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.udSplitMinutes);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.udSplitHours);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.udSplitDays);
            this.Controls.Add(this.dtpStartTime);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBrowseInputFiles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvInputFiles);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(950, 912);
            this.Name = "FormMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "AudioSplit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.udSplitSeconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSplitMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSplitHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSplitDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInputFiles)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtTotalDuration;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnBrowseOutputFolder;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtOutputFilenameTemplate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbOutputFormat;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpExcludeStop;
        private System.Windows.Forms.DateTimePicker dtpExcludeStart;
        private System.Windows.Forms.CheckBox chkExclude;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown udSplitSeconds;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown udSplitMinutes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown udSplitHours;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown udSplitDays;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBrowseInputFiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvInputFiles;
        private System.Windows.Forms.CheckBox chkSplit;
        private System.Windows.Forms.Button btnBrowseExcludeFolder;
        private System.Windows.Forms.TextBox txtExcludeFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.CheckBox chkStartOnHour;
        private System.Windows.Forms.Label lblExampleOuputFilename;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbChannels;
        private System.Windows.Forms.CheckBox chkRemoveXingHeader;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtInputTemplate;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtSiteName;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectInputfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openlogFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showdateInternationalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showTimeIn24HourFormatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writelogFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem templateHelpToolStripMenuItem;
        private System.Windows.Forms.Label lblResolvedExcludeFolder;
        private System.Windows.Forms.Label lblResolvedOutputFolder;
        private System.Windows.Forms.Label lblParseMessage;
    }
}

