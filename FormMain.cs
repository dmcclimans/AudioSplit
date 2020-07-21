using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FFmpeg.NET;
using FFmpeg.NET.Events;

namespace AudioSplit
{
    public partial class FormMain : Form
    {
        private Settings Settings { get; set; }
        private DataTable InputFilesTable { get; set; }
        private Engine FFmpeg { get; set; }

        private TimeSpan ProcessingDuration { get; set; } = TimeSpan.Zero;
        // Flag value, indicates the file could not be found.
        private TimeSpan TimeSpanMissing { get; } = new TimeSpan(-1, 0, 0);
        private int MissingFileCount { get; set; } = 0;
       private int EstimatedSplitFileCount { get; set; } = 0;
        // Fraction of the progress bar that will be used by processing the files. The
        // rest of the progress bar is used by the rename process.
        private const double ProcessingProgressFraction = 0.85;

        // Used by the progress bar, to include the seconds of duration processed by the catch-up task.
        private double CatchUpElapsedSeconds { get; set; } = 0.0;

        private bool FormIsLoaded { get; set; } = false;

        private bool ExcludeIsEnabled { get; set; } = false;

        private FormTemplateHelp FormTemplateHelpDlg;

        // Text file used to write log file.
        private StreamWriter LogFile { get; set; }

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Settings = Settings.Load();
            if (Settings.FormMainLocation.X != 0 ||
                  Settings.FormMainLocation.Y != 0)
            {
                this.Location = Settings.FormMainLocation;
            }
            if (Settings.FormMainSize.Height != 0 ||
                  Settings.FormMainSize.Width != 0)
            {
                this.Size = Settings.FormMainSize;
            }

            CreateDataTable();

            // Load output format combobox
            List<string> outputFormats = new List<string>();
            outputFormats.Add("wav");
            outputFormats.Add("flac");
            outputFormats.Add("mp3");
            outputFormats.Add("aif");

            // Load channels combobox
            List<string> outputChannels = new List<string>();
            outputChannels.Add("Stereo");
            outputChannels.Add("Left");
            outputChannels.Add("Right");

            if (!File.Exists(Settings.FfmpegPath))
            {
                MessageBox.Show("Installation error. The program ffmpeg.exe was not found.", "AudioSplit");
                this.Close();
            }
            FFmpeg = new Engine(Settings.FfmpegPath);

            // set up bindings
            dtpStartDate.DataBindings.Add("Value", Settings, "StartDate", true, DataSourceUpdateMode.OnPropertyChanged);
            dtpStartTime.DataBindings.Add("Value", Settings, "StartTime", true, DataSourceUpdateMode.OnPropertyChanged);
            chkSplit.DataBindings.Add("Checked", Settings, "SplitEnabled", true, DataSourceUpdateMode.OnPropertyChanged);
            udSplitDays.DataBindings.Add("Value", Settings, "SplitDurationDays");
            udSplitDays.DataBindings.Add("Enabled", Settings, "SplitEnabled");
            udSplitHours.DataBindings.Add("Value", Settings, "SplitDurationHours");
            udSplitHours.DataBindings.Add("Enabled", Settings, "SplitEnabled");
            udSplitMinutes.DataBindings.Add("Value", Settings, "SplitDurationMinutes");
            udSplitMinutes.DataBindings.Add("Enabled", Settings, "SplitEnabled");
            udSplitSeconds.DataBindings.Add("Value", Settings, "SplitDurationSeconds");
            udSplitSeconds.DataBindings.Add("Enabled", Settings, "SplitEnabled");
            chkStartOnHour.DataBindings.Add("Checked", Settings, "StartSplitOnHour", true, DataSourceUpdateMode.OnPropertyChanged);
            chkStartOnHour.DataBindings.Add("Enabled", Settings, "SplitEnabled");
            chkExclude.DataBindings.Add("Checked", Settings, "ExcludeEnabled", true, DataSourceUpdateMode.OnPropertyChanged);
            chkExclude.DataBindings.Add("Enabled", Settings, "SplitEnabled");
            dtpExcludeStart.DataBindings.Add("Value", Settings, "ExcludeStartTime", true, DataSourceUpdateMode.OnPropertyChanged);
            dtpExcludeStart.DataBindings.Add("Enabled", Settings, "ExcludeTimesEnabled", true, DataSourceUpdateMode.OnPropertyChanged);
            dtpExcludeStop.DataBindings.Add("Value", Settings, "ExcludeStopTime", true, DataSourceUpdateMode.OnPropertyChanged);
            dtpExcludeStop.DataBindings.Add("Enabled", Settings, "ExcludeTimesEnabled", true, DataSourceUpdateMode.OnPropertyChanged);
            chkAutoOutputFolder.DataBindings.Add("Checked", Settings, "AutoOutputFolder", true, DataSourceUpdateMode.OnPropertyChanged);
            chkAutoOutputFolder.CheckedChanged += chkAutoOutputFolder_CheckedChanged;
            txtOutputFolder.DataBindings.Add("Text", Settings, "OutputFolder");
            txtOutputFolder.DataBindings.Add("Enabled", Settings, "OutputFolderEnabled");
            btnBrowseOutputFolder.DataBindings.Add("Enabled", Settings, "OutputFolderEnabled");
            chkAutoExcludeFolder.DataBindings.Add("Checked", Settings, "AutoExcludeFolder", true, DataSourceUpdateMode.OnPropertyChanged);
            chkAutoExcludeFolder.DataBindings.Add("Enabled", Settings, "ChkAutoExcludeEnabled", true, DataSourceUpdateMode.OnPropertyChanged);
            chkAutoExcludeFolder.CheckedChanged += chkAutoExcludeFolder_CheckedChanged;
            txtExcludeFolder.DataBindings.Add("Text", Settings, "ExcludeFolder");
            txtExcludeFolder.DataBindings.Add("Enabled", Settings, "ExcludeFolderEnabled");
            btnBrowseExcludeFolder.DataBindings.Add("Enabled", Settings, "ExcludeFolderEnabled");

            // I can't figure out how to bind the value of the combobox to a property. Probably something
            // simple. Maybe selectedIndex.
            // So I define an eventhandler for selected index and set the property.
            // And I set the value here.
            cbOutputFormat.DataSource = outputFormats;
            int index = cbOutputFormat.FindString(Settings.OutputFormat);
            if (index < 0)
                index = 0;
            cbOutputFormat.SelectedIndex = index;

            cbChannels.DataBindings.Add("Enabled", Settings, "OutputChannelsEnabled");
            cbChannels.DataSource = outputChannels;
            index = cbChannels.FindString(Settings.OutputChannels);
            if (index < 0)
                index = 0;
            cbChannels.SelectedIndex = index;

            txtOutputTemplate.DataBindings.Add("Text", Settings, "OutputFileTemplate");
            chkWriteLogFile.DataBindings.Add("Checked", Settings, "WriteLogFile", true, DataSourceUpdateMode.OnPropertyChanged);

            Settings.UpdateEnabledProperties();
            Settings.PropertyChanged += Settings_PropertyChanged;
            MakeExample();

            FormIsLoaded = true;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close the help form, so that it saves it's location to Settings.
            if (FormTemplateHelpDlg != null)
            {
                FormTemplateHelpDlg.Close();
            }

            Settings.FormMainLocation = this.Location;
            Settings.FormMainSize = this.Size;
            Settings.Save();
        }

        public void CreateDataTable()
        {
            InputFilesTable = new DataTable("InputFiles");
            InputFilesTable.Columns.Add("File Name",
                System.Type.GetType("System.String"));
            InputFilesTable.Columns.Add("Duration",
                System.Type.GetType("System.TimeSpan"));
            InputFilesTable.Columns.Add("Channels",
                 System.Type.GetType("System.String"));
            dgvInputFiles.DataSource = InputFilesTable;
            dgvInputFiles.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvInputFiles.Columns[1].ReadOnly = true;
            dgvInputFiles.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvInputFiles.Columns[2].Visible = false;
            InputFilesTable.ColumnChanged += InputFilesTable_ColumnChanged;
        }

        private async void InputFilesTable_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            if (FormIsLoaded)
            {
                if (e.Column.Ordinal == 0)
                {
                    // User has changed a filename.
                    await UpdateMetadata(e.Row);
                    ComputeInputFileStats();
                    UpdateAutoFolders();
                }
            }
        }

        public string AddTrailingBackslash(string folder)
        {
            if (string.IsNullOrWhiteSpace(folder))
            {
                return "";
            }

            if (!folder.EndsWith("\\"))
                folder+= "\\";
            return folder;
        }

        // Format time span as d.hh.mm.ss, but remove any leading zero values. For example if the value is
        // less than an hour, this will return mm.ss. Also, it formats h, m, and s as two digits, except
        // for the first value in the string.
        // It returns "Missing" for TimeSpanMissing.
        private string FormatTimeSpan(TimeSpan timespan)
        {
            if (timespan == TimeSpanMissing)
                return "Missing";

            string s = "";
            if (timespan.Days > 0)
                s = timespan.ToString(@"d\.hh\:mm\:ss");
            else if (timespan.Hours > 0)
                s = timespan.ToString(@"h\:mm\:ss");
            else
                s = timespan.ToString(@"m\:ss");
            return s;
        }

        private void dgvInputFiles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    if (e.Value is TimeSpan)
                    {
                        if ((TimeSpan)e.Value == TimeSpan.Zero)
                        {
                            e.Value = "";
                        }
                        else
                        {
                            e.Value = FormatTimeSpan((TimeSpan)e.Value);
                        }
                    }
                }
                else
                {
                    e.Value = "";
                }
            }
        }

        private async Task<MetaData> GetMetaData(string Filename)
        {
            MetaData metadata = null;
            try
            {
                var File = new MediaFile(Filename);

                metadata = await FFmpeg.GetMetaDataAsync(File);
                if (metadata == null)
                {
                    // Kluge!
                    // I do not know why, but sometimes GetMetaDataAsync returns null.
                    // Seems to happen only on first file in a group of files, and only
                    // some times. I fix this by retrying the call to GetMetaDataAsync
                    metadata = await FFmpeg.GetMetaDataAsync(File);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AudioSplit");
            }
            return metadata;
        }

        private async Task<TimeSpan> GetDuration(string filename)
        {
            MetaData metadata = await GetMetaData(filename);
            return GetDuration(metadata);
        }

        private TimeSpan GetDuration(MetaData metadata)
        {
            if (metadata == null)
                return TimeSpan.Zero;
            else
                return metadata.Duration;
        }

        private async void btnBrowseInputFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Multiselect = true;
            dlg.Filter = "Audio files (*.wav;*.mp3;*.flac;*.aif)|*.wav;*.mp3;*.flac;*.aif|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtTotalDuration.Text = "";

                // Add the file names first, so they all appear.
                // The files in Filenames seem to be in sorted order already, so I just add them.
                int TableLength = InputFilesTable.Rows.Count;
                for (int i = 0; i < dlg.FileNames.Length; i++)
                {
                    try
                    {
                        InputFilesTable.Rows.Add(dlg.FileNames[i], TimeSpan.Zero);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "AudioSplit");
                    }
                }

                // Then add the durations, as a separate step because this may take perceptible time;
                // And check for missing files.
                await UpdateMetadata(TableLength, InputFilesTable.Rows.Count);

                // Update auto folders
                UpdateAutoFolders();
            }
        }

        // Check the InputFilesTable, from startrow to endrow-1.
        // Set the duration and channel for each row.
        // If the file is missing, set the duration to missing, channel to blank.
        // If the file exists but the duration cannot be determined, set the duration to zero.
        // If the file exists but the channel cannot be determined, set the channel to blank.
        // Set the ProcessingDuration to the sum of the durations for all rows (not just start to end-1).
        // Set the MaxChannelCount to 1 if all files are "mono", or 2 otherwise.
        // Return the number of missing files.
        private async Task UpdateMetadata(int startrow, int endrow)
        {
            for (int i = startrow; i < endrow; i++)
            {
                await UpdateMetadata(InputFilesTable.Rows[i]);
            }

            ComputeInputFileStats();
        }

        private async Task UpdateMetadata(DataRow row)
        {
            string filename = (string)row[0];
            if (!File.Exists(filename))
            {
                row[1] = TimeSpanMissing;
                row[2] = "";
            }
            else
            {
                MetaData metadata = await GetMetaData(filename);
                TimeSpan dur = GetDuration(metadata);
                row[1] = dur;
                row[2] = metadata.AudioData.ChannelOutput;
            }
       }

        private void UpdateAutoFolders()
        {
            if (Settings.AutoOutputFolder)
            {
                string outputFolder = "";
                try
                {
                    if (InputFilesTable.Rows.Count > 0)
                    {
                        DirectoryInfo inputFolder = Directory.GetParent((string)InputFilesTable.Rows[0][0]);
                        DirectoryInfo baseFolder = inputFolder.Parent;
                        outputFolder = Path.Combine(baseFolder.FullName, "Hourly");
                        outputFolder = AddTrailingBackslash(outputFolder);
                    }
                }
                catch (Exception)
                { }
                Settings.OutputFolder = outputFolder;
            }
            if (Settings.AutoExcludeFolder)
            {
                string excludeFolder = "";
                try
                {
                    if (!String.IsNullOrWhiteSpace(Settings.OutputFolder))
                    {
                        DirectoryInfo baseFolder = Directory.GetParent(Settings.OutputFolder);
                        excludeFolder = Path.Combine(baseFolder.FullName, "Exclude");
                        excludeFolder = AddTrailingBackslash(excludeFolder);
                    }
                }
                catch ( Exception )
                { }
                Settings.ExcludeFolder = excludeFolder;
            }
        }

        // Compute ProcessingDuration and MissingFileCount.
        // Estimate the SplitFileCount
        // Update the ProcessingDuration textbox.
        private void ComputeInputFileStats()
        {
            ProcessingDuration = TimeSpan.Zero;
            int MaxChannelCount = 1;
            MissingFileCount = 0;
            for (int i=0; i<InputFilesTable.Rows.Count; i++)
            {
                TimeSpan timespan = (TimeSpan)InputFilesTable.Rows[i][1];
                if (timespan == TimeSpanMissing)
                    MissingFileCount++;
                else if (timespan > TimeSpan.Zero)
                    ProcessingDuration += timespan;
                if ((String)InputFilesTable.Rows[i][2] != "mono")
                    MaxChannelCount = 2;
            }
            txtTotalDuration.Text = FormatTimeSpan(ProcessingDuration);
            Settings.OutputChannelsEnabled = (InputFilesTable.Rows.Count == 0 || MaxChannelCount > 1);

            // Rename (and move for excluded files) takes about 0.2 seconds per file.
            // Depends on the computer and disk drive, of course, but this gives us an
            // estimate.
            EstimatedSplitFileCount = (int)Math.Ceiling(ProcessingDuration.TotalSeconds / SplitDurationTotalSeconds);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTotalDuration.Text = "";
            InputFilesTable.Clear();
            ComputeInputFileStats();
            UpdateAutoFolders();
        }

        private void btnBrowseOutputFolder_Click(object sender, EventArgs e)
        {
            var dlg = new FolderSelectDialog();
            if (!string.IsNullOrWhiteSpace(Settings.OutputFolder))
                dlg.InitialDirectory = Settings.OutputFolder;
            dlg.Title = "Select Output Folder";
            if (dlg.Show(this))
            {
                Settings.OutputFolder = AddTrailingBackslash(dlg.FileName);
            }
            UpdateAutoFolders();        // Update exclude folder from output.
        }

        private void btnBrowseExcludeFolder_Click(object sender, EventArgs e)
        {
            var dlg = new FolderSelectDialog();
            if (!string.IsNullOrWhiteSpace(Settings.ExcludeFolder))
                dlg.InitialDirectory = Settings.ExcludeFolder;
            dlg.Title = "Select Exclude Folder";
            if (dlg.Show(this))
            {
                Settings.ExcludeFolder = AddTrailingBackslash(dlg.FileName);
            }
        }

        private double SplitDurationTotalSeconds
        {
            get
            {
                return (((Settings.SplitDurationDays * 24.0) +
                          Settings.SplitDurationHours) * 60.0 +
                          Settings.SplitDurationMinutes) * 60.0 +
                          Settings.SplitDurationSeconds;
            }
        }

        private const double SecondsPerDay = 24.0 * 60.0 * 60.0;
        private double ExcludeDurationTotalSeconds
        {
            get
            {
                double Start = Settings.ExcludeStartTime.TimeOfDay.TotalSeconds;
                double Stop = Settings.ExcludeStopTime.TimeOfDay.TotalSeconds;
                // Check for Exclude time is at night (across midnight boundary)
                if (Stop < Start)
                    Stop += SecondsPerDay;
                return (Stop - Start);
            }
        }

        private async Task ProcessFilesAsync(IProgress<ProcessFilesProgressReport> progress)
        {
            // Create the list of input files, and the filter stream list
            string InputFilesList = "";
            string FilterStreamList = "";
            for (int i = 0; i < InputFilesTable.Rows.Count; i++)
            {
                InputFilesList += string.Format(" -i \"{0}\"", InputFilesTable.Rows[i][0]);
                FilterStreamList += string.Format("[{0}:0]", i);
            }

            string SplitCommandLine = "";
            string CatchUpCommandLine = "";
            TimeSpan CatchUpSplitDuration = TimeSpan.Zero;

            // If we have stereo files, and the user has selected just the left or right channel, 
            // set the channel map string.
            string ChannelMapString = "";
            if (Settings.OutputChannelsEnabled)
            {
                if (Settings.OutputChannels == "Right")
                {
                    ChannelMapString = ", pan=mono|FC=FR";
                }
                else if (Settings.OutputChannels == "Left")
                {
                    ChannelMapString = ", pan=mono|FC=FL";
                }
            }

            if (CatchUpSplit)
            {
                // User has selected the option to start splits on the hour
                TimeSpan timeOfDay = Settings.StartTime.TimeOfDay;
                TimeSpan nextFullHour = TimeSpan.FromHours(Math.Ceiling(timeOfDay.TotalHours));
                CatchUpSplitDuration = nextFullHour - timeOfDay;

                if (CatchUpSplitDuration.TotalSeconds < 1)
                {
                    // Should not happen, this should have been caught during the pre-checks and CatchUpSplit turned off.
                    CatchUpSplit = false;
                }
                else
                {
                    // The output filename is "Split#.wav", so that this will sort before all the "Split0001.wav"
                    // files. That makes the file rename operation work correctly.
                    string OutputFileName = Path.Combine(Settings.OutputFolder, "Split#." + Settings.OutputFormat);
                    CatchUpCommandLine =
                        InputFilesList + " -filter_complex \"" +
                        FilterStreamList +
                        "concat=n=" + InputFilesTable.Rows.Count.ToString() +
                        ":v=0:a=1" +
                        ChannelMapString + " [out]\"" +
                        " -map \"[out]\" -t " + CatchUpSplitDuration.TotalSeconds.ToString("0.000") +
                          " \"" + OutputFileName + "\"";
                }
            }

            // Create the command line parameters
            if (Settings.SplitEnabled)
            {
                string OutputFileName = Path.Combine(Settings.OutputFolder, "Split%04d." + Settings.OutputFormat);
                SplitCommandLine =
                    InputFilesList + " -filter_complex \"" +
                    FilterStreamList +
                    "concat=n=" + InputFilesTable.Rows.Count.ToString() +
                    ":v=0:a=1" +
                    ChannelMapString + " [out]\"" +
                    " -map \"[out]\" ";
                if (CatchUpSplit)
                {
                    SplitCommandLine +=
                        " -ss " + CatchUpSplitDuration.TotalSeconds.ToString("0.000");
                }
                SplitCommandLine +=
                    " -f segment -segment_time " + SplitDurationTotalSeconds.ToString("0") +
                    " \"" + OutputFileName + "\"";
            }
            else
            {
                string OutputFileName = Path.Combine(Settings.OutputFolder, "Split0000." + Settings.OutputFormat);
                SplitCommandLine =
                    InputFilesList + " -filter_complex \"" +
                    FilterStreamList +
                    "concat=n=" + InputFilesTable.Rows.Count.ToString() +
                    ":v=0:a=1" +
                    ChannelMapString + " [out]\"" +
                    " -map \"[out]\" \"" + OutputFileName + "\"";
             }

            CancelTokenSource = new CancellationTokenSource();
            CatchUpElapsedSeconds = 0.0;

            FFmpeg.Progress += OnProgress;
            FFmpeg.Error += OnError;
            FFmpeg.Data += OnData;
            if (Settings.WriteLogFile)
            {
                try
                {
                    string LogFilename = Path.Combine(Settings.OutputFolder, "AudioSplit.log");
                    LogFile = new StreamWriter(LogFilename);
                }
                catch (Exception)
                {
                    // Ignore error in creating log file.
                }
            }
            try
            {
                if (!String.IsNullOrWhiteSpace(CatchUpCommandLine))
                {
                    if (LogFile != null)
                    {
                        LogFile.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " ffmpeg -y " +
                            CatchUpCommandLine);
                    }
                    await FFmpeg.ExecuteAsync(CatchUpCommandLine, CancelTokenSource.Token);
                    CatchUpElapsedSeconds = CatchUpSplitDuration.TotalSeconds;
                }
                if (LogFile != null)
                {
                    LogFile.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " ffmpeg -y " +
                        SplitCommandLine);
                }
                await FFmpeg.ExecuteAsync(SplitCommandLine, CancelTokenSource.Token);
            }
            // No catch block -- exception will propagate up.
            finally
            {
                FFmpeg.Progress -= OnProgress;
                FFmpeg.Error -= OnError;
                FFmpeg.Data -= OnData;
                if (LogFile != null)
                {
                    LogFile.Close();
                    LogFile = null;
                }
            }
        }

        private string FormatOutputFilename(string template, DateTime StartTime, int fileNumber)
        {
            // Using DateTime.ToString effectively truncates the ms portion.
            // I want to round to the nearest second, so I add 0.5 seconds to
            // the StartTime.
            // DateTime is a value type so the additional 0.5 seconds does not
            // affect the caller.
            StartTime = StartTime.AddSeconds(0.5);
            string Filename = template.Replace("@yyyy", StartTime.ToString("yyyy"));
            Filename = Filename.Replace("@yy", StartTime.ToString("yy"));
            Filename = Filename.Replace("@MM", StartTime.ToString("MM"));
            Filename = Filename.Replace("@dd", StartTime.ToString("dd"));
            Filename = Filename.Replace("@HH", StartTime.ToString("HH"));
            Filename = Filename.Replace("@hh", StartTime.ToString("hh"));
            Filename = Filename.Replace("@mm", StartTime.ToString("mm"));
            Filename = Filename.Replace("@ss", StartTime.ToString("ss"));
            Filename = Filename.Replace("@1", (fileNumber + 1).ToString());
            int start = Filename.IndexOf("@0");
            if (start >= 0)
            {
                int stop = start + 1;
                while (stop < Filename.Length &&
                    (Filename[stop] == '0' || Filename[stop] == '1'))
                {
                    stop++;
                }
                int fn = fileNumber;
                if (Filename[stop-1] == '1')
                {
                    fn++;
                }
                int width = stop - start - 1;
                string formatStr = string.Format("D{0}", width);
                string fileNumberStr = fn.ToString(formatStr);
                Filename = Filename.Remove(start, stop - start);
                Filename = Filename.Insert(start, fileNumberStr);
            }
            return Filename;
        }

        private async Task RenameOutputFiles(DateTime StartTime)
        {
            // DateTime is a value type (struct), so the addition to
            // StartTime is not passed back to the caller.
            string[] FileList = Directory.GetFiles(Settings.OutputFolder, "Split*." + Settings.OutputFormat);
            double ExcludeStart = Settings.ExcludeStartTime.TimeOfDay.TotalSeconds;
            double ExcludeStop = Settings.ExcludeStopTime.TimeOfDay.TotalSeconds;
            // Check for Exclude time is at night (across midnight boundary)
            if (ExcludeStop < ExcludeStart)
                ExcludeStop += SecondsPerDay;

            for (int i = 0; i < FileList.Length; i++)
            {
                TimeSpan dur = await GetDuration(FileList[i]);
                string NewName = FormatOutputFilename(Settings.OutputFileTemplate, StartTime, i);
                bool exclude = false;
                if (ExcludeIsEnabled)
                {
                    // Note that FileStop is >= FileStart, so might be in the next day.
                    // We round the file times to seconds, because the user can only specify
                    // seconds for start time and split time, so they expect it to behave
                    // in seconds.  ffmpeg will split to partial seconds (such as 1:00:00.25)
                    // and we keep track of that as we add up the durations; for each split
                    // we round to the nearest second.
                    double FileStart = Math.Round(StartTime.TimeOfDay.TotalSeconds);
                    double FileStop = Math.Round(FileStart + dur.TotalSeconds);
                    if ((FileStart >= ExcludeStart &&
                            FileStop <= ExcludeStop) ||
                        ((FileStart+SecondsPerDay) >= ExcludeStart &&
                            (FileStop+SecondsPerDay) <= ExcludeStop))
                    {
                        exclude = true;
                    }
                }
                if (exclude)
                {
                    NewName = Path.Combine(Settings.ExcludeFolder, NewName + "." + Settings.OutputFormat);
                }
                else
                {
                    NewName = Path.Combine(Settings.OutputFolder, NewName + "." + Settings.OutputFormat);
                }
                File.Move(FileList[i], NewName);
                StartTime += dur;

                double fraction = ProcessingProgressFraction +
                    (double)(i) / FileList.Length * (1 - ProcessingProgressFraction);
                int percent = (int)(fraction * 100.0);
                percent = Math.Min(percent, 99);
                Progress.Report(new ProcessFilesProgressReport(percent, "Renaming",
                    Path.GetFileName(NewName)));
            }
        }

        IProgress<ProcessFilesProgressReport> Progress;
        CancellationTokenSource CancelTokenSource;
        private bool CatchUpSplit = false;

        private async void btnRun_Click(object sender, EventArgs e)
        {
            // Save the settings
            Settings.Save();

            // Check that there are files to process
            if (InputFilesTable.Rows.Count == 0)
            {
                MessageBox.Show("No files to process.", "AudioSplit");
                return;
            }

            // Check that there is a specified output directory.
            // Create the output directory if necessary.
            if (String.IsNullOrWhiteSpace(Settings.OutputFolder))
            {
                MessageBox.Show("No output folder specified", "AudioSplit");
                return;
            }
            System.IO.Directory.CreateDirectory(Settings.OutputFolder);

            // Check that the output folder is empty
            if (Directory.GetFiles(Settings.OutputFolder).Length > 0)
            {
                MessageBox.Show("The output folder must be empty.", "AudioSplit");
                return;
            }

            if (!CheckTemplate())
            {
                return;
            }

            ExcludeIsEnabled = false;
            if (Settings.SplitEnabled)
            {
                // Check that the split duration is at least 5 seconds long. That way we avoid
                // problems with duplicate file names.
                if (SplitDurationTotalSeconds < 5.0)
                {
                    MessageBox.Show("Split duration must be at least 5 seconds.", "AudioSplit");
                    return;
                }


                if (Settings.ExcludeEnabled)
                {
                    // Check that there is a specified exclude directory.
                    // Create the exclude directory if necessary.
                    if (String.IsNullOrWhiteSpace(Settings.ExcludeFolder))
                    {
                        MessageBox.Show("No exclude folder specified", "AudioSplit");
                        return;
                    }
                    System.IO.Directory.CreateDirectory(Settings.ExcludeFolder);

                    // Check that the exclude folder is empty
                    if (Directory.GetFiles(Settings.ExcludeFolder).Length > 0)
                    {
                        MessageBox.Show("The exclude folder must be empty.", "AudioSplit");
                        return;
                    }

                    // Check that the split duration is less than the exclude duration. Otherwise,
                    // Exclude can never happen.
                    if (ExcludeDurationTotalSeconds <= SplitDurationTotalSeconds)
                    {
                        if (MessageBox.Show("Warning: Exclude duration is smaller than the split duration, so Exclude will not occur.", "AudioSplit", MessageBoxButtons.OKCancel) ==
                             DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                    else
                    {
                        ExcludeIsEnabled = true;
                    }
                }
            }
            else // if (!Settings.SplitEnabled)
            {
                if (Settings.ExcludeEnabled)
                {
                    if (MessageBox.Show("Warning: Splitting is not enabled so Exclude will not occur.", "AudioSplit", MessageBoxButtons.OKCancel) ==
                        DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }

            // Decide if we are going to create a catch up split so that we align on the hour
            CatchUpSplit = false;
            if (Settings.StartSplitOnHour)
            {
                if (!Settings.SplitEnabled)
                {
                    // CatchUpSplit = false;
                }
                 else if (Settings.SplitDurationMinutes != 0 || Settings.SplitDurationSeconds != 0)
                {
                    if (MessageBox.Show("Warning: Split length is not an even number of hours, so the option to start files on the hour will be ignored.", "AudioSplit", MessageBoxButtons.OKCancel) ==
                        DialogResult.Cancel)
                    {
                        return;
                    }
                }
                else if ((ProcessingDuration.TotalHours < 60) &&
                    (Settings.StartTime.Hour == Settings.StartTime.Add(ProcessingDuration).Hour))
                {
                    if (MessageBox.Show("Warning: Total duration is too short to split on the hour.", "AudioSplit", MessageBoxButtons.OKCancel) ==
                         DialogResult.Cancel)
                    {
                        return;
                    }
                }
                else if (Settings.StartTime.Minute == 0 && Settings.StartTime.Second == 0)
                {
                    // Start time is already on the hour, so no need to do anything more.
                    // CatchUpSplit = false;
                }
                else
                {
                    CatchUpSplit = true;
                }
            }

            // Update the duration, and check for missing files.
            // Update the auto folders.
            try
            {
                await UpdateMetadata(0, InputFilesTable.Rows.Count);
                UpdateAutoFolders();

                if (MissingFileCount > 0)
                {
                    MessageBox.Show("One or more of the input files is missing.", "AudioSplit");
                    return;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "AudioSplit");
                return;
            }

            var ProcessFilesProgressDialog = new System.Windows.Forms.ProgressDialog
            {
                Title = "AudioSplit"
            };
            ProcessFilesProgressDialog.SetLine(1, "Preparing");
            ProcessFilesProgressDialog.Show(this);
            ProcessFilesProgressDialog.Canceled += ProgressDialog_Canceled;

            this.Enabled = false;

            Stopwatch RunTimer = new Stopwatch();
            RunTimer.Start();

            // The Progress<T> constructor captures our UI context,
            //  so the lambda will be run on the UI thread.
            Progress = new Progress<ProcessFilesProgressReport>
                (report =>
                {
                    ProcessFilesProgressDialog.Value = (uint)report.Percent;
                    ProcessFilesProgressDialog.SetLine(1, report.Line1);
                    ProcessFilesProgressDialog.SetLine(2, report.Line2);
            });

            // Prevent computer from sleeping during processing
            PreventSleep();

            bool wasCanceled = false;
            bool ffmpegCompleted = false;
            ProcessingException = null;
            Task task = null;
            try
            {
                task = ProcessFilesAsync(Progress);
                await task;
                if (ProcessingException == null)
                {
                    ffmpegCompleted = true;
                }
        }
            catch (Exception exc)
            {
                if (task != null && task.IsCanceled)
                {
                    wasCanceled = true;
                }
               else if (ProcessingException == null)
                {
                    ProcessingException = exc;
                }
            }
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " ProcessFilesAsync complete");

            bool renameCompleted = false;
            if (ffmpegCompleted)
            {
                try
                {
                    await RenameOutputFiles(Settings.StartDate.Date.Add(Settings.StartTime.TimeOfDay));
                    renameCompleted = true;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "AudioSplit");
                }
            }

            // Allow computer to sleep again.
            AllowSleep();

            ProcessFilesProgressDialog.Canceled -= ProgressDialog_Canceled;
            ProcessFilesProgressDialog.Close();
            RunTimer.Stop();
            if (wasCanceled)
            {
                MessageBox.Show("Run was canceled", "AudioSplit");
            }
            else if (ProcessingException != null)
            {
                string msg = ProcessingException.Message;
                // Remove beginning of error message such as "[concat @ 00012345678] ".
                if (msg.StartsWith("[concat "))
                {
                    int pos = msg.IndexOf(']');
                    if (pos > 0)
                        msg = msg.Remove(0, pos + 2);
                }
                MessageBox.Show("Error during processing:\r\n" + msg, "AudioSplit");
            }
            else if (renameCompleted)
            {
                MessageBox.Show("Run completed in " + FormatTimeSpan(RunTimer.Elapsed), "AudioSplit");
            }

            this.Enabled = true;
            // Sometimes Windows will jump to another program rather than return focus to
            // the main form. This happens if you close the messagebox before the progress dialog
            // closes. This is a known bug with IProgressDialog based dialogs.
            // You can work around the problem by either explicitly Activating the main form,
            // or by creating a hidden child form (window) with the main form as its parent and
            // the progress dialog and the message boxes as its children. Since I am displaying
            // a message box in all cases, I know my app has the focus, so Activating the main
            // form is the easiest and cleanest solution.
            this.Activate();
        }

        private bool CheckTemplate()
        {
            // Check for invalid characters in the output template
            // This is not foolproof, but it helps detect problems early.
            // Other checks might include for UNC, drive-path format (such as PRN:), etc.
            Regex containsABadCharacter = new Regex("["
              + Regex.Escape(new string(System.IO.Path.GetInvalidPathChars())) + "]");
            if (containsABadCharacter.IsMatch(Settings.OutputFileTemplate))
            {
                MessageBox.Show("Invalid character in File name Template");
                return false;
            };

            // Check that the output template does not start with "Split"
            if (Settings.OutputFileTemplate.StartsWith("Split", StringComparison.InvariantCultureIgnoreCase))
            {
                MessageBox.Show("The file name template cannot start with \"split\".");
                return false;
            }

            // Check that the output template contains variables, so that the file names will be different.
            // Year and month variables don't count, since a single file can't be that long.
            // Again, this is not foolproof, but it helps detect problems early.
            if (Settings.SplitEnabled)
            {
                if (!Settings.OutputFileTemplate.Contains("@dd") &&
                    !Settings.OutputFileTemplate.Contains("@HH") &&
                    !Settings.OutputFileTemplate.Contains("@hh") &&
                    !Settings.OutputFileTemplate.Contains("@mm") &&
                    !Settings.OutputFileTemplate.Contains("@ss") &&
                    !Settings.OutputFileTemplate.Contains("@1") &&
                    !Settings.OutputFileTemplate.Contains("@0")
                    )
                {
                    MessageBox.Show("File name template must contain one or more variables (besides year and month).");
                    return false;
                }
                if (Settings.OutputFileTemplate.Contains("@hh") &&
                    !Settings.OutputFileTemplate.Contains("@tt"))
                {
                    MessageBox.Show("File name template contains @hh but does not include @tt. (Use @HH for 24-hour format of hours.)");
                    return false;
                }
            }
            return true;
        }

        private int OnProgressCount = 0;
        private void OnProgress(object sender, ConversionProgressEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " Progress event " +
                (++OnProgressCount).ToString());
            if (ProcessingDuration != TimeSpan.Zero)
            {
                // We are running in a worker thread created by ffmpeg. So we cannot
                // just update ProcessFileProgressDialog.Value and .Line2.
                double processedSeconds;
                if (e.ProcessedDuration > ProcessingDuration)
                {
                    processedSeconds = ProcessingDuration.TotalSeconds;
                }
                else
                {
                    processedSeconds = e.ProcessedDuration.TotalSeconds;
                }
                processedSeconds += CatchUpElapsedSeconds;
                double fraction = (processedSeconds / ProcessingDuration.TotalSeconds) *
                    ProcessingProgressFraction;
                fraction = Math.Min(fraction, ProcessingProgressFraction);
                int percent = (int)(fraction * 100.0);
                percent = Math.Min(percent, 99);
                string line2 = e.Input?.FileInfo?.Name;
                Progress.Report(new ProcessFilesProgressReport(percent, "Processing", line2));
            }
        }

        Exception ProcessingException;
        private void OnError(object sender, ConversionErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " Error: " + e.Exception.Message);
            ProcessingException = e.Exception;
        }

        private void ProgressDialog_Canceled(object sender, EventArgs e)
        {
            try
            {
                CancelTokenSource.Cancel();
            }
            catch (Exception)
            {
                // Ignore errors in attempt to cancel. You can get a System.AggregateException if you cancel
                // a task that is no longer running. This happens for example when you get an error during
                // file rename, and the user clicks cancel in the progress dialog before they click the
                // OK button on the message box.
            }
        }

        // OnData is called for each line that ffmpeg.exe writes to the error output.
        // If write log file is checked, log the lines sent by ffmpeg.exe.
        private void OnData(object sender, ConversionDataEventArgs e)
        {
            if (LogFile != null)
            {
                LogFile.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " " + e.Data);
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            FormAbout dlg = new FormAbout();
            dlg.ShowDialog();
        }

        public static void PreventSleep()
        {
            SetThreadExecutionState(ExecutionState.EsContinuous | ExecutionState.EsSystemRequired);
        }

        public static void AllowSleep()
        {
            SetThreadExecutionState(ExecutionState.EsContinuous);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern ExecutionState SetThreadExecutionState(ExecutionState esFlags);

        [FlagsAttribute]
        private enum ExecutionState : uint
        {
            EsAwaymodeRequired = 0x00000040,
            EsContinuous = 0x80000000,
            EsDisplayRequired = 0x00000002,
            EsSystemRequired = 0x00000001
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (dgvInputFiles.CurrentCell == null)
                return;
            int selectedRow = dgvInputFiles.CurrentCell.RowIndex;
            if (selectedRow < 1)
                return;
            if (selectedRow >= InputFilesTable.Rows.Count)
                return;

            DataRow newRow = InputFilesTable.NewRow();
            newRow.ItemArray = InputFilesTable.Rows[selectedRow].ItemArray;
            InputFilesTable.Rows.RemoveAt(selectedRow);
            InputFilesTable.Rows.InsertAt(newRow, selectedRow-1);
            dgvInputFiles.CurrentCell = dgvInputFiles.Rows[selectedRow - 1].Cells[0];
            UpdateAutoFolders();
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (dgvInputFiles.CurrentCell == null)
                return;
            int selectedRow = dgvInputFiles.CurrentCell.RowIndex;
            if (selectedRow < 0)
                return;
            if (selectedRow >= InputFilesTable.Rows.Count-1)
                return;

            DataRow newRow = InputFilesTable.NewRow();
            newRow.ItemArray = InputFilesTable.Rows[selectedRow].ItemArray;
            InputFilesTable.Rows.RemoveAt(selectedRow);
            InputFilesTable.Rows.InsertAt(newRow, selectedRow + 1);
            dgvInputFiles.CurrentCell = dgvInputFiles.Rows[selectedRow + 1].Cells[0];
            UpdateAutoFolders();
        }

        private void chkAutoOutputFolder_CheckedChanged(object sender, EventArgs e)
        {
            if (FormIsLoaded)
            {
                if (chkAutoOutputFolder.Checked)
                {
                    UpdateAutoFolders();
                }
            }
        }

        private void chkAutoExcludeFolder_CheckedChanged(object sender, EventArgs e)
        {
            if (FormIsLoaded)
            {
                if (chkAutoExcludeFolder.Checked)
                {
                    UpdateAutoFolders();
                }
            }
        }

        private void cbOutputFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FormIsLoaded)
            {
                Settings.OutputFormat = cbOutputFormat.Text;
            }
        }

        private void cbChannels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FormIsLoaded)
            {
                Settings.OutputChannels= cbChannels.Text;
            }
        }

        private void txtOutputFolder_Validated(object sender, EventArgs e)
        {
            if (FormIsLoaded)
            {
                Settings.OutputFolder = AddTrailingBackslash(Settings.OutputFolder);
                UpdateAutoFolders();
            }
        }

        private void txtExcludeFolder_Validated(object sender, EventArgs e)
        {
            if (FormIsLoaded)
            {
                Settings.ExcludeFolder = AddTrailingBackslash(Settings.ExcludeFolder);
            }
        }

        private void btnTemplateHelp_Click(object sender, EventArgs e)
        {
            if (FormTemplateHelpDlg == null || FormTemplateHelpDlg.IsDisposed)
            {
                FormTemplateHelpDlg = new FormTemplateHelp();
                FormTemplateHelpDlg.Settings = Settings;
            }
            FormTemplateHelpDlg.Show(this);
            FormTemplateHelpDlg.Activate();
        }

        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (FormIsLoaded)
            {
                if (e.PropertyName == nameof(Settings.OutputFileTemplate) ||
                    e.PropertyName == nameof(Settings.OutputFormat) ||
                    e.PropertyName == nameof(Settings.StartDate) ||
                    e.PropertyName == nameof(Settings.StartTime)
                    )
                {
                    MakeExample();
                }

                if (e.PropertyName == nameof(Settings.OutputFileTemplate))
                {
                    CheckTemplate();
                }
            }
         }

        private void MakeExample()
        {
            string example = FormatOutputFilename(Settings.OutputFileTemplate,
                Settings.StartDate.Date.Add(Settings.StartTime.TimeOfDay), 0) +
                "." + Settings.OutputFormat;
            labelExample.Text = example;
        }
    }

    public class ProcessFilesProgressReport
    {
        public ProcessFilesProgressReport(int percent, string line1, string line2)
        {
            Percent = percent;
            Line1 = line1;
            Line2 = line2;
        }

        // Current progress
        public int Percent { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
    }
}
