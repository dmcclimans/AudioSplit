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
            this.txtOutputTemplate = new System.Windows.Forms.TextBox();
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
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.chkStartOnHour = new System.Windows.Forms.CheckBox();
            this.chkAutoOutputFolder = new System.Windows.Forms.CheckBox();
            this.chkAutoExcludeFolder = new System.Windows.Forms.CheckBox();
            this.btnTemplateHelp = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.labelExample = new System.Windows.Forms.Label();
            this.chkWriteLogFile = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbChannels = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.udSplitSeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSplitMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSplitHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSplitDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInputFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(188, 9);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(56, 19);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtTotalDuration
            // 
            this.txtTotalDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalDuration.Location = new System.Drawing.Point(532, 202);
            this.txtTotalDuration.Margin = new System.Windows.Forms.Padding(2);
            this.txtTotalDuration.Name = "txtTotalDuration";
            this.txtTotalDuration.ReadOnly = true;
            this.txtTotalDuration.Size = new System.Drawing.Size(83, 20);
            this.txtTotalDuration.TabIndex = 11;
            this.txtTotalDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(452, 204);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Total duration:";
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRun.Location = new System.Drawing.Point(10, 500);
            this.btnRun.Margin = new System.Windows.Forms.Padding(2);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(56, 19);
            this.btnRun.TabIndex = 42;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnBrowseOutputFolder
            // 
            this.btnBrowseOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseOutputFolder.Location = new System.Drawing.Point(558, 317);
            this.btnBrowseOutputFolder.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowseOutputFolder.Name = "btnBrowseOutputFolder";
            this.btnBrowseOutputFolder.Size = new System.Drawing.Size(56, 19);
            this.btnBrowseOutputFolder.TabIndex = 29;
            this.btnBrowseOutputFolder.Text = "Browse...";
            this.btnBrowseOutputFolder.UseVisualStyleBackColor = true;
            this.btnBrowseOutputFolder.Click += new System.EventHandler(this.btnBrowseOutputFolder_Click);
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputFolder.Location = new System.Drawing.Point(200, 317);
            this.txtOutputFolder.Margin = new System.Windows.Forms.Padding(2);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(354, 20);
            this.txtOutputFolder.TabIndex = 28;
            this.txtOutputFolder.Validated += new System.EventHandler(this.txtOutputFolder_Validated);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 319);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Output folder:";
            // 
            // txtOutputTemplate
            // 
            this.txtOutputTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputTemplate.Location = new System.Drawing.Point(150, 422);
            this.txtOutputTemplate.Margin = new System.Windows.Forms.Padding(2);
            this.txtOutputTemplate.Name = "txtOutputTemplate";
            this.txtOutputTemplate.Size = new System.Drawing.Size(404, 20);
            this.txtOutputTemplate.TabIndex = 37;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 424);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 13);
            this.label10.TabIndex = 36;
            this.label10.Text = "File name template:";
            // 
            // cbOutputFormat
            // 
            this.cbOutputFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbOutputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOutputFormat.FormattingEnabled = true;
            this.cbOutputFormat.Location = new System.Drawing.Point(150, 369);
            this.cbOutputFormat.Margin = new System.Windows.Forms.Padding(2);
            this.cbOutputFormat.Name = "cbOutputFormat";
            this.cbOutputFormat.Size = new System.Drawing.Size(92, 21);
            this.cbOutputFormat.TabIndex = 35;
            this.cbOutputFormat.SelectedIndexChanged += new System.EventHandler(this.cbOutputFormat_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 371);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 34;
            this.label9.Text = "Output format:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(241, 294);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "to";
            // 
            // dtpExcludeStop
            // 
            this.dtpExcludeStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpExcludeStop.CustomFormat = "HH:mm:ss";
            this.dtpExcludeStop.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpExcludeStop.Location = new System.Drawing.Point(262, 292);
            this.dtpExcludeStop.Margin = new System.Windows.Forms.Padding(2);
            this.dtpExcludeStop.Name = "dtpExcludeStop";
            this.dtpExcludeStop.ShowUpDown = true;
            this.dtpExcludeStop.Size = new System.Drawing.Size(87, 20);
            this.dtpExcludeStop.TabIndex = 25;
            // 
            // dtpExcludeStart
            // 
            this.dtpExcludeStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpExcludeStart.CustomFormat = "HH:mm:ss";
            this.dtpExcludeStart.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpExcludeStart.Location = new System.Drawing.Point(150, 292);
            this.dtpExcludeStart.Margin = new System.Windows.Forms.Padding(2);
            this.dtpExcludeStart.Name = "dtpExcludeStart";
            this.dtpExcludeStart.ShowUpDown = true;
            this.dtpExcludeStart.Size = new System.Drawing.Size(87, 20);
            this.dtpExcludeStart.TabIndex = 23;
            // 
            // chkExclude
            // 
            this.chkExclude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkExclude.AutoSize = true;
            this.chkExclude.Location = new System.Drawing.Point(10, 293);
            this.chkExclude.Margin = new System.Windows.Forms.Padding(2);
            this.chkExclude.Name = "chkExclude";
            this.chkExclude.Size = new System.Drawing.Size(138, 17);
            this.chkExclude.TabIndex = 22;
            this.chkExclude.Text = "Exclude data between: ";
            this.chkExclude.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(400, 239);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Seconds:";
            // 
            // udSplitSeconds
            // 
            this.udSplitSeconds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.udSplitSeconds.Location = new System.Drawing.Point(452, 237);
            this.udSplitSeconds.Margin = new System.Windows.Forms.Padding(2);
            this.udSplitSeconds.Name = "udSplitSeconds";
            this.udSplitSeconds.Size = new System.Drawing.Size(34, 20);
            this.udSplitSeconds.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(310, 239);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Minutes:";
            // 
            // udSplitMinutes
            // 
            this.udSplitMinutes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.udSplitMinutes.Location = new System.Drawing.Point(358, 237);
            this.udSplitMinutes.Margin = new System.Windows.Forms.Padding(2);
            this.udSplitMinutes.Name = "udSplitMinutes";
            this.udSplitMinutes.Size = new System.Drawing.Size(34, 20);
            this.udSplitMinutes.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(228, 239);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Hours:";
            // 
            // udSplitHours
            // 
            this.udSplitHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.udSplitHours.Location = new System.Drawing.Point(266, 237);
            this.udSplitHours.Margin = new System.Windows.Forms.Padding(2);
            this.udSplitHours.Name = "udSplitHours";
            this.udSplitHours.Size = new System.Drawing.Size(34, 20);
            this.udSplitHours.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(150, 239);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Days:";
            // 
            // udSplitDays
            // 
            this.udSplitDays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.udSplitDays.Location = new System.Drawing.Point(184, 237);
            this.udSplitDays.Margin = new System.Windows.Forms.Padding(2);
            this.udSplitDays.Name = "udSplitDays";
            this.udSplitDays.Size = new System.Drawing.Size(34, 20);
            this.udSplitDays.TabIndex = 14;
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpStartTime.CustomFormat = "HH:mm:ss";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpStartTime.Location = new System.Drawing.Point(244, 205);
            this.dtpStartTime.Margin = new System.Windows.Forms.Padding(2);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.ShowUpDown = true;
            this.dtpStartTime.Size = new System.Drawing.Size(87, 20);
            this.dtpStartTime.TabIndex = 9;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpStartDate.CustomFormat = "yyyy/MM/dd";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(150, 205);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(90, 20);
            this.dtpStartDate.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 207);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Start date and time:";
            // 
            // btnBrowseInputFiles
            // 
            this.btnBrowseInputFiles.Location = new System.Drawing.Point(120, 9);
            this.btnBrowseInputFiles.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowseInputFiles.Name = "btnBrowseInputFiles";
            this.btnBrowseInputFiles.Size = new System.Drawing.Size(56, 19);
            this.btnBrowseInputFiles.TabIndex = 1;
            this.btnBrowseInputFiles.Text = "Browse...";
            this.btnBrowseInputFiles.UseVisualStyleBackColor = true;
            this.btnBrowseInputFiles.Click += new System.EventHandler(this.btnBrowseInputFiles_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input files:";
            // 
            // dgvInputFiles
            // 
            this.dgvInputFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInputFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInputFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInputFiles.Location = new System.Drawing.Point(10, 32);
            this.dgvInputFiles.Margin = new System.Windows.Forms.Padding(2);
            this.dgvInputFiles.Name = "dgvInputFiles";
            this.dgvInputFiles.RowTemplate.Height = 24;
            this.dgvInputFiles.Size = new System.Drawing.Size(604, 158);
            this.dgvInputFiles.TabIndex = 6;
            this.dgvInputFiles.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvInputFiles_CellFormatting);
            // 
            // chkSplit
            // 
            this.chkSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkSplit.AutoSize = true;
            this.chkSplit.Location = new System.Drawing.Point(10, 238);
            this.chkSplit.Margin = new System.Windows.Forms.Padding(2);
            this.chkSplit.Name = "chkSplit";
            this.chkSplit.Size = new System.Drawing.Size(134, 17);
            this.chkSplit.TabIndex = 12;
            this.chkSplit.Text = "Split into files of length:";
            this.chkSplit.UseVisualStyleBackColor = true;
            // 
            // btnBrowseExcludeFolder
            // 
            this.btnBrowseExcludeFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseExcludeFolder.Location = new System.Drawing.Point(558, 342);
            this.btnBrowseExcludeFolder.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowseExcludeFolder.Name = "btnBrowseExcludeFolder";
            this.btnBrowseExcludeFolder.Size = new System.Drawing.Size(56, 19);
            this.btnBrowseExcludeFolder.TabIndex = 33;
            this.btnBrowseExcludeFolder.Text = "Browse...";
            this.btnBrowseExcludeFolder.UseVisualStyleBackColor = true;
            this.btnBrowseExcludeFolder.Click += new System.EventHandler(this.btnBrowseExcludeFolder_Click);
            // 
            // txtExcludeFolder
            // 
            this.txtExcludeFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExcludeFolder.Location = new System.Drawing.Point(200, 342);
            this.txtExcludeFolder.Margin = new System.Windows.Forms.Padding(2);
            this.txtExcludeFolder.Name = "txtExcludeFolder";
            this.txtExcludeFolder.Size = new System.Drawing.Size(354, 20);
            this.txtExcludeFolder.TabIndex = 32;
            this.txtExcludeFolder.Validated += new System.EventHandler(this.txtExcludeFolder_Validated);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 344);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Exclude folder:";
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(558, 9);
            this.btnAbout.Margin = new System.Windows.Forms.Padding(2);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(56, 19);
            this.btnAbout.TabIndex = 5;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveUp.Location = new System.Drawing.Point(257, 9);
            this.btnMoveUp.Margin = new System.Windows.Forms.Padding(2);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(18, 19);
            this.btnMoveUp.TabIndex = 3;
            this.btnMoveUp.Text = "▲";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveDown.Location = new System.Drawing.Point(280, 9);
            this.btnMoveDown.Margin = new System.Windows.Forms.Padding(2);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(18, 19);
            this.btnMoveDown.TabIndex = 4;
            this.btnMoveDown.Text = "▼";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // chkStartOnHour
            // 
            this.chkStartOnHour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkStartOnHour.AutoSize = true;
            this.chkStartOnHour.Location = new System.Drawing.Point(10, 265);
            this.chkStartOnHour.Margin = new System.Windows.Forms.Padding(2);
            this.chkStartOnHour.Name = "chkStartOnHour";
            this.chkStartOnHour.Size = new System.Drawing.Size(126, 17);
            this.chkStartOnHour.TabIndex = 21;
            this.chkStartOnHour.Text = "Start files on the hour";
            this.chkStartOnHour.UseVisualStyleBackColor = true;
            // 
            // chkAutoOutputFolder
            // 
            this.chkAutoOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAutoOutputFolder.AutoSize = true;
            this.chkAutoOutputFolder.Location = new System.Drawing.Point(150, 318);
            this.chkAutoOutputFolder.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chkAutoOutputFolder.Name = "chkAutoOutputFolder";
            this.chkAutoOutputFolder.Size = new System.Drawing.Size(48, 17);
            this.chkAutoOutputFolder.TabIndex = 27;
            this.chkAutoOutputFolder.Text = "Auto";
            this.chkAutoOutputFolder.UseVisualStyleBackColor = true;
            // 
            // chkAutoExcludeFolder
            // 
            this.chkAutoExcludeFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAutoExcludeFolder.AutoSize = true;
            this.chkAutoExcludeFolder.Location = new System.Drawing.Point(150, 343);
            this.chkAutoExcludeFolder.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chkAutoExcludeFolder.Name = "chkAutoExcludeFolder";
            this.chkAutoExcludeFolder.Size = new System.Drawing.Size(48, 17);
            this.chkAutoExcludeFolder.TabIndex = 31;
            this.chkAutoExcludeFolder.Text = "Auto";
            this.chkAutoExcludeFolder.UseVisualStyleBackColor = true;
            // 
            // btnTemplateHelp
            // 
            this.btnTemplateHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTemplateHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTemplateHelp.Location = new System.Drawing.Point(558, 422);
            this.btnTemplateHelp.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnTemplateHelp.Name = "btnTemplateHelp";
            this.btnTemplateHelp.Size = new System.Drawing.Size(17, 20);
            this.btnTemplateHelp.TabIndex = 38;
            this.btnTemplateHelp.Text = "?";
            this.btnTemplateHelp.UseVisualStyleBackColor = true;
            this.btnTemplateHelp.Click += new System.EventHandler(this.btnTemplateHelp_Click);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 447);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(95, 13);
            this.label13.TabIndex = 39;
            this.label13.Text = "Example file name:";
            // 
            // labelExample
            // 
            this.labelExample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelExample.Location = new System.Drawing.Point(150, 447);
            this.labelExample.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelExample.Name = "labelExample";
            this.labelExample.Size = new System.Drawing.Size(404, 13);
            this.labelExample.TabIndex = 40;
            this.labelExample.Text = "example";
            // 
            // chkWriteLogFile
            // 
            this.chkWriteLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkWriteLogFile.AutoSize = true;
            this.chkWriteLogFile.Location = new System.Drawing.Point(10, 470);
            this.chkWriteLogFile.Name = "chkWriteLogFile";
            this.chkWriteLogFile.Size = new System.Drawing.Size(84, 17);
            this.chkWriteLogFile.TabIndex = 41;
            this.chkWriteLogFile.Text = "Write log file";
            this.chkWriteLogFile.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 398);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 13);
            this.label14.TabIndex = 43;
            this.label14.Text = "Channels:";
            // 
            // cbChannels
            // 
            this.cbChannels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbChannels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChannels.FormattingEnabled = true;
            this.cbChannels.Location = new System.Drawing.Point(150, 396);
            this.cbChannels.Margin = new System.Windows.Forms.Padding(2);
            this.cbChannels.Name = "cbChannels";
            this.cbChannels.Size = new System.Drawing.Size(92, 21);
            this.cbChannels.TabIndex = 44;
            this.cbChannels.SelectedIndexChanged += new System.EventHandler(this.cbChannels_SelectedIndexChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 531);
            this.Controls.Add(this.cbChannels);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.chkWriteLogFile);
            this.Controls.Add(this.labelExample);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnTemplateHelp);
            this.Controls.Add(this.chkAutoExcludeFolder);
            this.Controls.Add(this.chkAutoOutputFolder);
            this.Controls.Add(this.chkStartOnHour);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnAbout);
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
            this.Controls.Add(this.txtOutputTemplate);
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
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(641, 509);
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
        private System.Windows.Forms.TextBox txtOutputTemplate;
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
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.CheckBox chkStartOnHour;
        private System.Windows.Forms.CheckBox chkAutoOutputFolder;
        private System.Windows.Forms.CheckBox chkAutoExcludeFolder;
        private System.Windows.Forms.Button btnTemplateHelp;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labelExample;
        private System.Windows.Forms.CheckBox chkWriteLogFile;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbChannels;
    }
}

