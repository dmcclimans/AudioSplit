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
using System.Xml.Linq;
using FolderSelect;
using CenteredMessageBox;
using System.Reflection;
using System.CodeDom.Compiler;

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

        // The most recently written log filename.
        private string MostRecentLogFilename = "";

        // The number of folders on the input path to check against the template.
        // 0 = check filename only
        // 1 = check filename and folder name
        // 2 = check filename, folder name, and parent folder name
        // etc.
        // Currently set to large value to search all the way to the root.
        // But if performance becomes an issue, you could make it smaller 
        // to minimize how many directories you search.
        public const int inputTemplateSearchLevel = int.MaxValue;

        // These are the output and exclude folder names, after template substitution.
        // They are only used by the background task that runs when you click the run button
        private string BGResolvedOutputFolder { get; set; }
        private string BGResolvedExcludeFolder { get; set; }

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            MessageBoxEx.Caption = "AudioSplit";

            try
            {
                Settings = Settings.Load();
            }
            catch (Exception ex)
            {
                // Use MessageBox here, rather than MessageBoxEx, because the form isn't loaded yet.
                // MessageBoxEx will center relative to the form's position, which is 0,0, so it will
                // display the messagebox in the upper left corner. MessageBox will display in the
                // center of the screen, which is better when no form is visible.
                MessageBox.Show(ex.Message, "AudioSplit", MessageBoxButtons.OK);
                Settings = new Settings();
            }

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

            string ffmpegPath = FindPath.FindExePath("ffmpeg.exe");
            if (string.IsNullOrWhiteSpace(ffmpegPath))
            {
                MessageBoxEx.Show(this, "Installation error. The program ffmpeg.exe was not found.");
                this.Close();
            }
            FFmpeg = new Engine(ffmpegPath);

            // Initialize settings in Menu Strip. 
            // Menu strips don't implement data bindings
            writelogFileToolStripMenuItem.Checked = Settings.WriteLogFile;

            // set up bindings
            txtSiteName.DataBindings.Add("Text", Settings, nameof(Settings.SiteName));
            dtpStartDate.DataBindings.Add("Value", Settings, nameof(Settings.StartDate), true, DataSourceUpdateMode.OnPropertyChanged);
            dtpStartTime.DataBindings.Add("Value", Settings, nameof(Settings.StartTime), true, DataSourceUpdateMode.OnPropertyChanged);
            chkSplit.DataBindings.Add("Checked", Settings, nameof(Settings.SplitEnabled), true, DataSourceUpdateMode.OnPropertyChanged);
            udSplitDays.DataBindings.Add("Value", Settings, nameof(Settings.SplitDurationDays));
            udSplitDays.DataBindings.Add("Enabled", Settings, nameof(Settings.SplitEnabled));
            udSplitHours.DataBindings.Add("Value", Settings, nameof(Settings.SplitDurationHours));
            udSplitHours.DataBindings.Add("Enabled", Settings, nameof(Settings.SplitEnabled));
            udSplitMinutes.DataBindings.Add("Value", Settings, nameof(Settings.SplitDurationMinutes));
            udSplitMinutes.DataBindings.Add("Enabled", Settings, nameof(Settings.SplitEnabled));
            udSplitSeconds.DataBindings.Add("Value", Settings, nameof(Settings.SplitDurationSeconds));
            udSplitSeconds.DataBindings.Add("Enabled", Settings, nameof(Settings.SplitEnabled));
            chkStartOnHour.DataBindings.Add("Checked", Settings, nameof(Settings.StartSplitOnHour), true, DataSourceUpdateMode.OnPropertyChanged);
            chkStartOnHour.DataBindings.Add("Enabled", Settings, nameof(Settings.SplitEnabled));
            chkExclude.DataBindings.Add("Checked", Settings, nameof(Settings.ExcludeData), true, DataSourceUpdateMode.OnPropertyChanged);
            chkExclude.DataBindings.Add("Enabled", Settings, nameof(Settings.SplitEnabled));
            dtpExcludeStart.DataBindings.Add("Value", Settings, nameof(Settings.ExcludeStartTime), true, DataSourceUpdateMode.OnPropertyChanged);
            dtpExcludeStart.DataBindings.Add("Enabled", Settings, nameof(Settings.ExcludeTimesEnabled), true, DataSourceUpdateMode.OnPropertyChanged);
            dtpExcludeStop.DataBindings.Add("Value", Settings, nameof(Settings.ExcludeStopTime), true, DataSourceUpdateMode.OnPropertyChanged);
            dtpExcludeStop.DataBindings.Add("Enabled", Settings, nameof(Settings.ExcludeTimesEnabled), true, DataSourceUpdateMode.OnPropertyChanged);
            txtOutputFolder.DataBindings.Add("Text", Settings, nameof(Settings.OutputFolder));
            txtExcludeFolder.DataBindings.Add("Text", Settings, nameof(Settings.ExcludeFolder));
            txtExcludeFolder.DataBindings.Add("Enabled", Settings, nameof(Settings.ExcludeFolderEnabled));
            btnBrowseExcludeFolder.DataBindings.Add("Enabled", Settings, nameof(Settings.ExcludeFolderEnabled));
            chkRemoveXingHeader.DataBindings.Add("Enabled", Settings, nameof(Settings.ChkRemoveXingHeaderEnabled), true, DataSourceUpdateMode.OnPropertyChanged);

            // I can't figure out how to bind the value of the combobox to a property. Probably something
            // simple. Maybe selectedIndex.
            // So I define an eventhandler for selected index and set the property.
            // And I set the value here.
            cbOutputFormat.DataSource = outputFormats;
            int index = cbOutputFormat.FindString(Settings.OutputFormat);
            if (index < 0)
                index = 0;
            cbOutputFormat.SelectedIndex = index;

            chkRemoveXingHeader.DataBindings.Add("Checked", Settings, nameof(Settings.RemoveXingHeader), true, DataSourceUpdateMode.OnPropertyChanged);

            cbChannels.DataBindings.Add("Enabled", Settings, nameof(Settings.OutputChannelsEnabled));
            cbChannels.DataSource = outputChannels;
            index = cbChannels.FindString(Settings.OutputChannels);
            if (index < 0)
                index = 0;
            cbChannels.SelectedIndex = index;

            txtInputTemplate.DataBindings.Add("Text", Settings, nameof(Settings.InputNameTemplate));
            txtOutputFilenameTemplate.DataBindings.Add("Text", Settings, nameof(Settings.OutputFileTemplate));

            showdateInternationalToolStripMenuItem.Checked = Settings.ShowDateInternational;
            SetDateFormat();
            showTimeIn24HourFormatToolStripMenuItem.Checked = Settings.ShowTime24Hour;
            SetTimeFormat();

            Settings.PropertyChanged += Settings_PropertyChanged;

            // On startup, force updates to the parse error message, and folder and file names
            SetInDir();
            ParseInputNameTemplate();
            MakeOutputFolderName();
            MakeExcludeFolderName();
            MakeOutputFilename();

            FormIsLoaded = true;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close the help form, so that it saves it's location to Settings.
            if (FormTemplateHelpDlg != null)
            {
                FormTemplateHelpDlg.Close();
            }

            // Save the form location
            if (this.WindowState == FormWindowState.Minimized || this.WindowState == FormWindowState.Maximized)
            {
                Settings.FormMainLocation = this.RestoreBounds.Location;
                Settings.FormMainSize = this.RestoreBounds.Size;
            }
            else
            {
                Settings.FormMainLocation = this.Location;
                Settings.FormMainSize = this.Size;
            }

            Settings.SaveIfChanged();
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
            InputFilesTable.RowDeleted += InputFilesTable_RowDeleted;
            Settings.InDir = "";
        }

        private async void InputFilesTable_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            if (FormIsLoaded)
            {
                if (e.Column.Ordinal == 0)
                {
                    // User has changed the first row.
                    await UpdateMetadata(e.Row);
                    ComputeInputFileStats();
                    SetInDir();
                }
            }
        }

        private void SetInDir()
        {
            string inDir = string.Empty;
            try
            {
                if (InputFilesTable.Rows.Count > 0)
                {
                    string inputPath = (string)InputFilesTable.Rows[0][0];
                    if (!string.IsNullOrWhiteSpace(inputPath))
                    {
                        inDir = Path.GetDirectoryName(inputPath);
                    }
                }
            }
            catch
            {
                // Ignore exceptions, which occur when the input string is incorrectly formed
                // In that case InDir will be empty string.
            }
            Settings.InDir = inDir;
        }

        private void InputFilesTable_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
            if (FormIsLoaded)
            {
                // User has deleted a row in the datatable
                ComputeInputFileStats();
                SetInDir();
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
                MessageBoxEx.Show(this, ex.Message);
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
                        MessageBoxEx.Show(this, ex.Message);
                    }
                }

                // Then add the durations, as a separate step because this may take perceptible time;
                // And check for missing files.
                await UpdateMetadata(TableLength, InputFilesTable.Rows.Count);

                SetInDir();
            }
        }
        private void selectInputfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnBrowseInputFiles_Click(sender, e);
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
            SetInDir();
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

            // For MP3 format output, we optionally remove the xing header.
            // The Audio2NVSPL program used by NPS seems to get confused by these headers.
            // You could, if you want, also remove the id3v2 header. For this, the options would be:
            //  mp3Options = " -write_xing 0 -id3v2_version 0";
            //  segmentMp3Options = " -segment_format_options write_xing=0:ic3v2_version=0";
            string mp3Options = "";
            string segmentMp3Options = "";
            if (Settings.OutputFormat == "mp3" && Settings.RemoveXingHeader)
            {
                mp3Options = " -write_xing 0";
                segmentMp3Options = " -segment_format_options write_xing=0";
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
                    string OutputFileName = Path.Combine(BGResolvedOutputFolder, "Split#." + Settings.OutputFormat);
                    CatchUpCommandLine =
                        InputFilesList + " -filter_complex \"" +
                        FilterStreamList +
                        "concat=n=" + InputFilesTable.Rows.Count.ToString() +
                        ":v=0:a=1" +
                        ChannelMapString + " [out]\"" +
                        " -map \"[out]\" -t " + CatchUpSplitDuration.TotalSeconds.ToString("0.000") +
                        mp3Options +
                        " \"" + OutputFileName + "\"";
                }
            }

            // Create the command line parameters
            if (Settings.SplitEnabled)
            {
                string OutputFileName = Path.Combine(BGResolvedOutputFolder, "Split%04d." + Settings.OutputFormat);
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
                    " -f segment" +
                    segmentMp3Options +
                    " -segment_time " + SplitDurationTotalSeconds.ToString("0") +
                    " \"" + OutputFileName + "\"";
            }
            else
            {
                string OutputFileName = Path.Combine(BGResolvedOutputFolder, "Split0000." + Settings.OutputFormat);
                SplitCommandLine =
                    InputFilesList + " -filter_complex \"" +
                    FilterStreamList +
                    "concat=n=" + InputFilesTable.Rows.Count.ToString() +
                    ":v=0:a=1" +
                    ChannelMapString + " [out]\"" +
                    " -map \"[out]\" " +
                    mp3Options +
                    " \"" + OutputFileName + "\"";
             }

            CancelTokenSource = new CancellationTokenSource();
            CatchUpElapsedSeconds = 0.0;

            FFmpeg.Progress += OnProgress;
            FFmpeg.Error += OnError;
            FFmpeg.Data += OnData;
            MostRecentLogFilename = "";
            if (Settings.WriteLogFile)
            {
                try
                {
                    string LogFilename = Path.Combine(BGResolvedOutputFolder, "AudioSplit.log");
                    LogFile = new StreamWriter(LogFilename);
                    MostRecentLogFilename = LogFilename;
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

        private async Task RenameOutputFiles(DateTime StartTime)
        {
            // DateTime is a value type (struct), so the addition to
            // StartTime is not passed back to the caller.
            string[] FileList = Directory.GetFiles(BGResolvedOutputFolder, "Split*." + Settings.OutputFormat);
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
                    NewName = Path.Combine(BGResolvedExcludeFolder, NewName + "." + Settings.OutputFormat);
                }
                else
                {
                    NewName = Path.Combine(BGResolvedOutputFolder, NewName + "." + Settings.OutputFormat);
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
                MessageBoxEx.Show(this, "No files to process.");
                return;
            }

            // Check that there is a specified output directory.
            // Create the output directory if necessary.
            if (String.IsNullOrWhiteSpace(Settings.OutputFolder))
            {
                MessageBoxEx.Show(this, "No output folder specified");
                return;
            }
            BGResolvedOutputFolder = FormatFolderName(Settings.OutputFolder,
                    Settings.StartDate.Date.Add(Settings.StartTime.TimeOfDay), false);
            try
            {
                System.IO.Directory.CreateDirectory(BGResolvedOutputFolder);
            }
            catch (Exception ex) 
            {
                MessageBoxEx.Show(this, "Could not create output folder\r\n" + ex.Message);
                return;
            }

            // Check that the output folder is empty
            if (Directory.GetFiles(BGResolvedOutputFolder).Length > 0)
            {
                MessageBoxEx.Show(this, "The output folder must be empty.");
                return;
            }

            if (!CheckOutputFilenameTemplate())
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
                    MessageBoxEx.Show(this, "Split duration must be at least 5 seconds.");
                    return;
                }


                if (Settings.ExcludeData)
                {
                    // Check that there is a specified exclude directory.
                    // Create the exclude directory if necessary.
                    if (String.IsNullOrWhiteSpace(Settings.ExcludeFolder))
                    {
                        MessageBoxEx.Show(this, "No exclude folder specified");
                        return;
                    }
                    BGResolvedExcludeFolder = FormatFolderName(Settings.ExcludeFolder,
                            Settings.StartDate.Date.Add(Settings.StartTime.TimeOfDay), true);
                    try
                    {
                        System.IO.Directory.CreateDirectory(BGResolvedExcludeFolder);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxEx.Show(this, "Could not create exclude folder\r\n" + ex.Message);
                        return;
                    }

                    // Check that the exclude folder is empty
                    if (Directory.GetFiles(BGResolvedExcludeFolder).Length > 0)
                    {
                        MessageBoxEx.Show(this, "The exclude folder must be empty.");
                        return;
                    }

                    // Check that the split duration is less than the exclude duration. Otherwise,
                    // Exclude can never happen.
                    if (ExcludeDurationTotalSeconds <= SplitDurationTotalSeconds)
                    {
                        if (MessageBoxEx.Show(this, "Warning: Exclude duration is smaller than the split duration, so Exclude will not occur.", 
                                MessageBoxButtons.OKCancel) == DialogResult.Cancel)
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
                if (Settings.ExcludeData)
                {
                    if (MessageBoxEx.Show(this, "Warning: Splitting is not enabled so Exclude will not occur.", 
                        MessageBoxButtons.OKCancel) == DialogResult.Cancel)
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
                    if (MessageBoxEx.Show(this, "Warning: Split length is not an even number of hours, so the option to start files on the hour will be ignored.", 
                        MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                else if ((ProcessingDuration.TotalHours < 60) &&
                    (Settings.StartTime.Hour == Settings.StartTime.Add(ProcessingDuration).Hour))
                {
                    if (MessageBoxEx.Show(this, "Warning: Total duration is too short to split on the hour.", 
                        MessageBoxButtons.OKCancel) == DialogResult.Cancel)
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
            try
            {
                await UpdateMetadata(0, InputFilesTable.Rows.Count);

                if (MissingFileCount > 0)
                {
                    MessageBoxEx.Show(this, "One or more of the input files is missing.");
                    return;
                }
            }
            catch (Exception exc)
            {
                MessageBoxEx.Show(this, exc.Message);
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
                    MessageBoxEx.Show(this, exc.Message);
                }
            }

            // Allow computer to sleep again.
            AllowSleep();

            ProcessFilesProgressDialog.Canceled -= ProgressDialog_Canceled;
            ProcessFilesProgressDialog.Close();
            RunTimer.Stop();
            if (wasCanceled)
            {
                MessageBoxEx.Show(this, "Run was canceled");
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
                MessageBoxEx.Show(this, "Error during processing:\r\n" + msg);
            }
            else if (renameCompleted)
            {
                MessageBoxEx.Show(this, "Run completed in " + FormatTimeSpan(RunTimer.Elapsed));
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

        private bool CheckOutputFilenameTemplate()
        {
            // Check for invalid characters in the output template
            // This is not foolproof, but it helps detect problems early.
            // Other checks might include for UNC, drive-path format (such as PRN:), etc.
            Regex containsABadCharacter = new Regex("["
              + Regex.Escape(new string(System.IO.Path.GetInvalidPathChars())) + "]");
            if (containsABadCharacter.IsMatch(Settings.OutputFileTemplate))
            {
                MessageBoxEx.Show(this, "Invalid character in File name Template");
                return false;
            };

            // Check that the output template does not start with "Split"
            if (Settings.OutputFileTemplate.StartsWith("Split", StringComparison.InvariantCultureIgnoreCase))
            {
                MessageBoxEx.Show(this, "The file name template cannot start with \"split\".");
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
                    MessageBoxEx.Show(this, "File name template must contain one or more variables (besides year and month).");
                    return false;
                }
                if (Settings.OutputFileTemplate.Contains("@hh") &&
                    !Settings.OutputFileTemplate.Contains("@tt"))
                {
                    MessageBoxEx.Show(this, "File name template contains @hh but does not include @tt. (Use @HH for 24-hour format of hours.)");
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
                double processedSeconds = e.ProcessedDuration.TotalSeconds;
                processedSeconds = Math.Max(processedSeconds, 0);
                processedSeconds = Math.Min(processedSeconds, ProcessingDuration.TotalSeconds);
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
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
            SetInDir();
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
            SetInDir();
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
            }
        }

        private void txtExcludeFolder_Validated(object sender, EventArgs e)
        {
            if (FormIsLoaded)
            {
                Settings.ExcludeFolder = AddTrailingBackslash(Settings.ExcludeFolder);
            }
        }

        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (FormIsLoaded)
            {
                if (e.PropertyName == nameof(Settings.InDir) ||
                    e.PropertyName == nameof(Settings.InputNameTemplate))
                {
                    ParseInputNameTemplate();
                }

                if (e.PropertyName == nameof(Settings.OutputFileTemplate) ||
                    e.PropertyName == nameof(Settings.InDir) || 
                    e.PropertyName == nameof(Settings.SiteName) ||
                    e.PropertyName == nameof(Settings.OutputFormat) ||
                    e.PropertyName == nameof(Settings.StartDate) ||
                    e.PropertyName == nameof(Settings.StartTime)
                    )
                {
                    MakeOutputFilename();
                }

                if (e.PropertyName == nameof(Settings.OutputFolder) ||
                    e.PropertyName == nameof(Settings.InDir) || 
                    e.PropertyName == nameof(Settings.SiteName) ||
                    e.PropertyName == nameof(Settings.StartDate) ||
                    e.PropertyName == nameof(Settings.StartTime)
                    )
                {
                    MakeOutputFolderName();
                }

                if (e.PropertyName == nameof(Settings.ExcludeFolder) ||
                    e.PropertyName == nameof(Settings.OutputFolder) ||
                    e.PropertyName == nameof(Settings.InDir) || 
                    e.PropertyName == nameof(Settings.SiteName) ||
                    e.PropertyName == nameof(Settings.StartDate) ||
                    e.PropertyName == nameof(Settings.StartTime)
                    )
                {
                    MakeExcludeFolderName();
                }

                if (e.PropertyName == nameof(Settings.OutputFileTemplate))
                {
                    CheckOutputFilenameTemplate();
                }

                if (e.PropertyName == nameof(Settings.ShowDateInternational))
                {
                    SetDateFormat();
                }

                if (e.PropertyName == nameof(Settings.ShowTime24Hour))
                {
                    SetTimeFormat();
                }
            }
        }

        private void MakeOutputFilename()
        {
            string example = FormatOutputFilename(Settings.OutputFileTemplate, 
                Settings.StartDate.Date.Add(Settings.StartTime.TimeOfDay), 0) +
                "." + Settings.OutputFormat;
            lblExampleOuputFilename.Text = example;
        }

        private void MakeOutputFolderName()
        {
            lblResolvedOutputFolder.Text = 
                FormatFolderName(Settings.OutputFolder,
                    Settings.StartDate.Date.Add(Settings.StartTime.TimeOfDay), false);
        }

        private void MakeExcludeFolderName()
        {
            lblResolvedExcludeFolder.Text = FormatFolderName(Settings.ExcludeFolder,
                Settings.StartDate.Date.Add(Settings.StartTime.TimeOfDay), true);
        }

        private string FormatOutputFilename(string template, DateTime StartTime, int fileNumber)
        {
            string Filename = FormatNameSiteDate(template, StartTime);

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
                if (Filename[stop - 1] == '1')
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

        // Process a template string and replace the site name and all date/time
        // variables. Return the resulting string (which may have additional variables
        // yet to be processed)
        private string FormatNameSiteDate(string template, DateTime StartTime)
        {
            string name = template.Replace("@Site", Settings.SiteName);
            // Using DateTime.ToString effectively truncates the ms portion.
            // I want to round to the nearest second, so I add 0.5 seconds to
            // the StartTime.
            // DateTime is a value type so the additional 0.5 seconds does not
            // affect the caller.
            StartTime = StartTime.AddSeconds(0.5);
            name = name.Replace("@yyyy", StartTime.ToString("yyyy"));
            name = name.Replace("@yy", StartTime.ToString("yy"));
            name = name.Replace("@MM", StartTime.ToString("MM"));
            name = name.Replace("@dd", StartTime.ToString("dd"));
            name = name.Replace("@HH", StartTime.ToString("HH"));
            name = name.Replace("@hh", StartTime.ToString("hh"));
            name = name.Replace("@mm", StartTime.ToString("mm"));
            name = name.Replace("@ss", StartTime.ToString("ss"));
            name = name.Replace("@tt", StartTime.ToString("tt"));
            return name;
        }

        private string FormatFolderName(string template, DateTime StartTime, bool allowOutDir)
        {
            string Foldername = FormatNameSiteDate(template, StartTime);

            if (template.IndexOf("@InDir") >= 0 && InputFilesTable.Rows.Count > 0)
            {
                string inputPath = (string)InputFilesTable.Rows[0][0];
                if (!string.IsNullOrWhiteSpace(inputPath))
                {
                    inputPath = Path.GetDirectoryName(inputPath);
                    Foldername = Foldername.Replace("@InDir", inputPath);
                    // The template might have ".." in it. It is tempting to use Path.GetFullPath
                    // to resolve this, yielding a more readable final folder path. However, if 
                    // @InDir does not exist, or is not defined yet (no input files loaded), then
                    // GetFullPath cannot resolve the "..", and it returns a path relative to the
                    // current working directory, which is typically the folder containing the
                    // AudioSplit.exe file. This is confusing to the user. To avoid this,do not
                    // call GetFullPath.
                    // Foldername = Path.GetFullPath(Foldername);
                }
            }

            if (allowOutDir && template.IndexOf("@OutDir") >= 0)
            {
                string outDir = FormatFolderName(Settings.OutputFolder, StartTime, false);
                Foldername = Foldername.Replace("@OutDir", outDir);
                // The template might have ".." in it. It is tempting to use Path.GetFullPath
                // to resolve this, yielding a more readable final folder path. However, if 
                // @OutDir does not exist, or @OutDir is not defined yet (possible if no input
                // files loaded) then GetFullPath cannot resolve the "..", and it returns a path
                // relative to the current working directory, which is typically the folder
                // containing the AudioSplit.exe file. This is confusing to the user. To avoid
                // this, do not call GetFullPath.
                // Foldername = Path.GetFullPath(Foldername);
            }

            // Remove double backlashes. They occur because variables like @Outdir end in a backslash,
            // so a template like "@OutDir\Exclude" results in a double backslash
            return Foldername.Replace(@"\\", @"\");
        }

        private void ParseInputNameTemplate()
        {
            lblParseMessage.Text = string.Empty;
            string message = string.Empty; 
            if (string.IsNullOrWhiteSpace(Settings.InputNameTemplate))
            { 
                return; 
            }

            if (InputFilesTable.Rows.Count == 0)
            {
                return;
            }

            string inputPath = (string)InputFilesTable.Rows[0][0];
            if (string.IsNullOrWhiteSpace(inputPath))
            {
                return;
            }

            string name = Path.GetFileName(inputPath);
            int searchLevel = 0;
            while (searchLevel <= inputTemplateSearchLevel && !string.IsNullOrWhiteSpace(name))
            {
                NameParseResult result = ParseInputNameTemplate(Settings.InputNameTemplate, name);
                if (result.ParseSuccess)
                {
                    if (!string.IsNullOrWhiteSpace(result.SiteName))
                        Settings.SiteName = result.SiteName;
                    if (result.Date != DateTime.MinValue)
                    {
                        Settings.StartDate = result.Date;
                    }
                    if (result.Time != DateTime.MinValue)
                    {
                        Settings.StartTime = result.Time;
                    }
                    return;
                }
                else
                {
                    // I tried to use result.Message to show the user where the error was in their
                    // template string. Unfortunately you get a different message for each filename
                    // and directory that you try to parse, and we don't know which is the right
                    // message to display. I now just display a generic "Failed" message.

                    // Get the parent folder.
                    try
                    {
                        DirectoryInfo parentFolder = Directory.GetParent(inputPath);
                        if (parentFolder == null)
                        {
                            inputPath = name = "";
                        }
                        else
                        {
                            inputPath = parentFolder.FullName;
                            name = parentFolder.Name;
                        }
                    }
                    catch (Exception)
                    {
                        // Could not find a parent directory. Give up.
                        break;
                    }
                }
                searchLevel++;
            }

            // Could not parse the file name or any of the folder names.
            lblParseMessage.Text = "Error";
        }

        private NameParseResult ParseInputNameTemplate(string template, string name)
        {
            NameParseResult result = new NameParseResult();

            int indexName = 0;
            int indexTemplate = 0;
            bool pmFound = false;
            while (indexName < name.Length && indexTemplate < template.Length)
            {
                if (template[indexTemplate] == '@')
                {
                    indexTemplate++;
                    string template2 = template.SubstringWithMaxLength(indexTemplate, 2);
                    string template4 = template.SubstringWithMaxLength(indexTemplate, 4);
                    if (template4 == "yyyy")
                    {
                        string yearString = name.SubstringWithMaxLength(indexName, 4);
                        if (yearString.Length == 4 && int.TryParse(yearString, out int year))
                        {
                            result.Year = year;
                        }
                        else
                        {
                            result.Message = "Invalid year";
                            return result;
                        }
                        indexName += 4;
                        indexTemplate += 4;
                    }
                    else if (template2 == "yy")
                    {
                        string yearString = name.SubstringWithMaxLength(indexName, 2);
                        if (yearString.Length == 2 && int.TryParse(yearString, out int year))
                        {
                            result.Year = (int)(DateTime.Now.Year /100) * 100 + year;
                        }
                        else
                        {
                            result.Message = "Invalid year";
                            return result;
                        }
                        indexName += 2;
                        indexTemplate += 2;
                    }
                    else if (template2 == "MM")
                    {
                        string monthString = name.SubstringWithMaxLength(indexName, 2);
                        if (monthString.Length == 2 && int.TryParse(monthString, out int month))
                        {
                            result.Month = month;
                        }
                        else
                        {
                            result.Message = "Invalid month";
                            return result;
                        }
                        indexName += 2;
                        indexTemplate += 2;
                    }
                    else if (template2 == "dd")
                    {
                        string dayString = name.SubstringWithMaxLength(indexName, 2);
                        if (dayString.Length == 2 && int.TryParse(dayString, out int day))
                        {
                            result.Day = day;
                        }
                        else
                        {
                            result.Message = "Invalid day";
                            return result;
                        }
                        indexName += 2;
                        indexTemplate += 2;
                    }
                    else if (template2 == "HH")
                    {
                        string hour24String = name.SubstringWithMaxLength(indexName, 2);
                        if (hour24String.Length == 2 && int.TryParse(hour24String, out int hour24))
                        {
                            result.Hour = hour24;
                        }
                        else
                        {
                            result.Message = "Invalid hour";
                            return result;
                        }
                        indexName += 2;
                        indexTemplate += 2;
                    }
                    else if (template2 == "hh")
                    {
                        string hour12String = name.SubstringWithMaxLength(indexName, 2);
                        if (hour12String.Length == 2 && int.TryParse(hour12String, out int hour12))
                        {
                            if (pmFound)
                            {
                                if (hour12 < 12)
                                    result.Hour = hour12 + 12;
                                else
                                    result.Hour = hour12;
                            }
                            else
                            {
                                if (hour12 < 12)
                                    result.Hour = hour12;
                                else
                                    result.Hour = 0;
                            }
                        }
                        else
                        {
                            result.Message = "Invalid hour";
                            return result;
                        }
                        indexName += 2;
                        indexTemplate += 2;
                    }
                    else if (template2 == "tt")
                    {
                        string ampmString = name.SubstringWithMaxLength(indexName, 2);
                        if (ampmString.Length == 2)
                        {
                            if (string.Equals(ampmString, "am", StringComparison.OrdinalIgnoreCase)) 
                            {
                                pmFound = false;
                                if (result.Hour == 12)
                                {
                                    result.Hour = 0;
                                }
                            }
                            else if (string.Equals(ampmString, "pm", StringComparison.OrdinalIgnoreCase))
                            {
                                pmFound = true;
                                if (result.Hour > 12)
                                {
                                    result.Hour += 12;
                                }
                            }
                            else
                            {
                                result.Message = "Invalid AM/PM indicator";
                                return result;
                            }
                        }
                        else
                        {
                            result.Message = "Invalid hours";
                            return result;
                        }
                        indexName += 2;
                        indexTemplate += 2;
                    }
                    else if (template2 == "mm")
                    {
                        string minuteString = name.SubstringWithMaxLength(indexName, 2);
                        if (minuteString.Length == 2 && int.TryParse(minuteString, out int minute))
                        {
                            result.Minute= minute;
                        }
                        else
                        {
                            result.Message = "Invalid minutes";
                            return result;
                        }
                        indexName += 2;
                        indexTemplate += 2;
                    }
                    else if (template2 == "ss")
                    {
                        string secondString = name.SubstringWithMaxLength(indexName, 2);
                        if (secondString.Length == 2 && int.TryParse(secondString, out int second))
                        {
                            result.Second = second;
                        }
                        else
                        {
                            result.Message = "Invalid seconds";
                            return result;
                        }
                        indexName += 2;
                        indexTemplate += 2;
                    }
                    else if (template4 == "Site")
                    {
                        indexTemplate += 4;
                        if (indexTemplate >= template.Length)
                        {
                            // Site name at end of template
                            result.SiteName = name.Substring(indexName);
                            indexName = name.Length;
                        }
                        else if (name[indexName] == '@')
                        {
                            result.Message = "Character after @Site cannot be @";
                            return result;
                        }
                        else
                        {
                            char termChar = template[indexTemplate];
                            int termPosition = name.IndexOf(termChar, indexName);
                            if (termPosition < 0)
                            {
                                result.Message = "Could not find end of @Site";
                                return result;
                            }
                            result.SiteName = name.SubstringWithMaxLength(indexName, termPosition - indexName);
                            indexName = termPosition;
                        }
                    }
                }   // end if (template[indexTemplate] == '@')
                else if (template[indexTemplate] == name[indexName]) 
                {
                    indexName++;
                    indexTemplate++;
                }
                else
                {
                    return result;
                }
            }   // end while
            result.ParseSuccess = true;
            return result;
        }

        private void SetDateFormat()
        {
            if (Settings.ShowDateInternational)
            {
                dtpStartDate.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                dtpStartDate.Format = DateTimePickerFormat.Short;
            }
        }

        private void SetTimeFormat()
        {
            if (Settings.ShowTime24Hour)
            {
                dtpStartTime.Format = DateTimePickerFormat.Custom;
                dtpExcludeStart.Format = DateTimePickerFormat.Custom;
                dtpExcludeStop.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                dtpStartTime.Format = DateTimePickerFormat.Time;
                dtpExcludeStart.Format = DateTimePickerFormat.Time;
                dtpExcludeStop.Format = DateTimePickerFormat.Time;
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openlogFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // It would be nice to enable or disable the Open Log File menu item by binding it to
            // a property. But tool strip menu items don't implement a DataBinding property. They
            // do not inherit from control.
            // So we check for whether a log file has been written
            if (!string.IsNullOrEmpty(MostRecentLogFilename))
            {
                if (!File.Exists(MostRecentLogFilename))
                {
                    MessageBoxEx.Show(this, "Log file does not exist.");
                }
                else
                {
                    Process.Start(MostRecentLogFilename);
                }
            }
            else
            {
                string resolvedOutputFolder =
                    FormatFolderName(Settings.OutputFolder,
                        Settings.StartDate.Date.Add(Settings.StartTime.TimeOfDay), false);
                string LogFilename = Path.Combine(resolvedOutputFolder, "AudioSplit.log");
                if (!File.Exists(LogFilename))
                {
                    MessageBoxEx.Show(this, "Log file does not exist.");
                }
                else
                {
                    Process.Start(LogFilename);
                }

            }
        }

        private void writelogFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            Settings.WriteLogFile = item.Checked;
        }

        private void nameTemplateHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormTemplateHelpDlg == null || FormTemplateHelpDlg.IsDisposed)
            {
                FormTemplateHelpDlg = new FormTemplateHelp();
                FormTemplateHelpDlg.Settings = Settings;
            }
            if (!FormTemplateHelpDlg.Visible)
                FormTemplateHelpDlg.Show(this);
            if (FormTemplateHelpDlg.WindowState == FormWindowState.Minimized)
                FormTemplateHelpDlg.WindowState = FormWindowState.Normal;
            FormTemplateHelpDlg.Activate();
        }

        private void showdateInternationalToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            Settings.ShowDateInternational = item.Checked;
        }

        private void showTimeIn24HourFormatToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            Settings.ShowTime24Hour = item.Checked;
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
