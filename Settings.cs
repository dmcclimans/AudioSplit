using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace AudioSplit
{
    /// <summary>
    /// Settings values.
    /// Implement INotifyPropertyChanged.
    /// Persist settings to xml file.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class will persist the settings data to an XML file. It will attempt to
    /// save the settings in the file "AudioSplitSettings.xml"
    /// in the folder of the executable. However, if that folder is not writable by the
    /// program (which might be the case if the user has installed it to a folder in
    /// Program Files), then it will save the data to
    /// Documents\AudioSplit\AudioSplitSettings.xml.
    /// </para>
    /// <para>
    /// Conceptually this class could be a static class without any instantiation. But the .net
    /// XMLSerializer will not serialize a static class, it will only work with instances.
    /// </para>
    /// </remarks>
    public class Settings : INotifyPropertyChanged
    {
        /// <summary>
        /// Event raised when a property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public const string Filename = "AudioSplitSettings.xml";
        public const string Folder = "AudioSplit";

        // Properties that are serialized, but do not trigger PropertyChanged event
        public Point FormMainLocation { get; set; }
        public Size FormMainSize { get; set; }
        public Point FormTemplateHelpLocation { get; set; }
        public Size FormTemplateHelpSize { get; set; }

        // Properties that are serialized, and trigger the PropertyChanged events.
        private DateTime startDateValue = DateTime.Today;
        public DateTime StartDate
        {
            get { return startDateValue; }
            set { SetProperty(ref startDateValue, value, true); }
        }

        private DateTime startTimeValue = DateTime.Today;
        public DateTime StartTime
        {
            get { return startTimeValue; }
            set { SetProperty(ref startTimeValue, value, true); }
        }

        private bool splitEnabledValue = true;
        public bool SplitEnabled
        {
            get { return splitEnabledValue; }
            set { SetProperty(ref splitEnabledValue, value, true); }
        }

        private int splitDurationDaysValue = 0;
        public int SplitDurationDays
        {
            get { return splitDurationDaysValue; }
            set { SetProperty(ref splitDurationDaysValue, value, true); }
        }

        private int splitDurationHoursValue = 1;
        public int SplitDurationHours
        {
            get { return splitDurationHoursValue; }
            set { SetProperty(ref splitDurationHoursValue, value, true); }
        }

        private int splitDurationMinutesValue = 0;
        public int SplitDurationMinutes
         {
            get { return splitDurationMinutesValue; }
            set { SetProperty(ref splitDurationMinutesValue, value, true); }
        }

        private int splitDurationSecondsValue = 0;
        public int SplitDurationSeconds
        {
            get { return splitDurationSecondsValue; }
            set { SetProperty(ref splitDurationSecondsValue, value, true); }
        }

        private bool startSplitOnHourValue = true;
        public bool StartSplitOnHour
        {
            get { return startSplitOnHourValue; }
            set { SetProperty(ref startSplitOnHourValue, value, true); }
        }

        private bool enableExcludedValue = false;
        public bool ExcludeEnabled
        {
            get { return enableExcludedValue; }
            set { SetProperty(ref enableExcludedValue, value, true); }
        }

        private DateTime excludeStartTimeValue = DateTime.Today + new TimeSpan(7,0,0);
        public DateTime ExcludeStartTime
        {
            get { return excludeStartTimeValue; }
            set { SetProperty(ref excludeStartTimeValue, value, true); }
        }

        private DateTime excludeStopTimeValue = DateTime.Today + new TimeSpan(18,0,0);
        public DateTime ExcludeStopTime
        {
            get { return excludeStopTimeValue; }
            set { SetProperty(ref excludeStopTimeValue, value, true); }
        }

        private bool autoOutputFolderValue = true;
        public bool AutoOutputFolder
        {
            get { return autoOutputFolderValue; }
            set { SetProperty(ref autoOutputFolderValue, value, true); }
        }

        private string outputFolderValue = "";
        public string OutputFolder
        {
            get { return outputFolderValue; }
            set { SetProperty(ref outputFolderValue, value, true); }
        }

        private bool autoExcludeFolderValue = true;
        public bool AutoExcludeFolder
        {
            get { return autoExcludeFolderValue; }
            set { SetProperty(ref autoExcludeFolderValue, value, true); }
        }

        private string excludeFolderValue = "";
        public string ExcludeFolder
        {
            get { return excludeFolderValue; }
            set { SetProperty(ref excludeFolderValue, value, true); }
        }

        private string outputFormatValue = "wav";
        public string OutputFormat
        {
            get { return outputFormatValue; }
            set { SetProperty(ref outputFormatValue, value, true); }
        }

        private string outputFileTemplateValue = "RRRD-01_@yyyy@MM@dd_@HH@mm@ss";
        public string OutputFileTemplate
        {
            get { return outputFileTemplateValue; }
            set { SetProperty(ref outputFileTemplateValue, value, true); }
        }

        private bool writeLogFileValue = false;
        public bool WriteLogFile
        {
            get { return writeLogFileValue; }
            set { SetProperty(ref writeLogFileValue, value, true); }
        }

        // Properties which are not persisted, but trigger property changed.
        private bool outputFolderEnableValue = true;
        [XmlIgnore]
        public bool OutputFolderEnabled
        {
            get { return outputFolderEnableValue; }
            set { SetProperty(ref outputFolderEnableValue, value, true); }
        }

        private bool excludeTimesEnabledValue = true;
        [XmlIgnore]
        public bool ExcludeTimesEnabled
        {
            get { return excludeTimesEnabledValue; }
            set { SetProperty(ref excludeTimesEnabledValue, value, true); }
        }

        private bool chkAutoExcludeEnabledValue = true;
        [XmlIgnore]
        public bool ChkAutoExcludeEnabled
        {
            get { return chkAutoExcludeEnabledValue; }
            set { SetProperty(ref chkAutoExcludeEnabledValue, value, true); }
        }

        private bool excludeFolderEnableValue = true;
        [XmlIgnore]
        public bool ExcludeFolderEnabled
        {
            get { return excludeFolderEnableValue; }
            set { SetProperty(ref excludeFolderEnableValue, value, true); }
        }

        public void UpdateEnabledProperties()
        {
            ExcludeTimesEnabled = SplitEnabled && ExcludeEnabled;
            OutputFolderEnabled = !AutoOutputFolder;
            ChkAutoExcludeEnabled = ExcludeEnabled;
            ExcludeFolderEnabled = !AutoExcludeFolder && ExcludeEnabled;
        }

        // Properties which are not persisted nor trigger property changed.
        private static string _ExeFolder = "";
        [XmlIgnore]
        public static string ExeFolder
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_ExeFolder))
                {
                    _ExeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                }
                return _ExeFolder;
            }
        }

        [XmlIgnore]
        public static string FfmpegPath
        {
            get
            {
                return Path.Combine(Settings.ExeFolder, "ffmpeg.exe");
            }
        }

        private static string _SettingsFolder = "";
        [XmlIgnore]
        public static string SettingsFolder
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_SettingsFolder))
                {
                    _SettingsFolder = Path.GetDirectoryName(Application.ExecutablePath);
                    if (!IsFolderWritable(_SettingsFolder))
                    {
                        _SettingsFolder =
                            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        _SettingsFolder = Path.Combine(_SettingsFolder, Folder);
                    }

                }
                return _SettingsFolder;
            }
        }

        public static Settings Load()
        {
            string path = SettingsFolder;
            if (path.Length == 0 || !Directory.Exists(path))
            {
                return new Settings();
            }
            path = Path.Combine(path, Filename);
            if (path.Length == 0 || !File.Exists(path))
            {
                return new Settings();
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                TextReader reader = new StreamReader(path);

                Settings mySettings = (Settings)serializer.Deserialize(reader);
                reader.Close();

                return mySettings;
            }
            catch (Exception e1)
            {
                // We failed to deserialize a Settings object
                string msg = "Error loading settings from " + path;
                msg += " -- " + e1.ToString();
                MessageBox.Show(msg);
                return new Settings();
            }
         }

        public void Save()
        {
            try
            {
                string path = SettingsFolder;
                // Ensure destination folder exists
                System.IO.Directory.CreateDirectory(path);
                path = Path.Combine(path, Filename);
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                TextWriter writer = new StreamWriter(path);
                serializer.Serialize(writer, this);
                writer.Close();
            }
            catch (Exception e)
            {
                string msg = "Error saving settings - " + e.ToString();
                MessageBox.Show(msg);
            }
        }

        public static bool IsFolderWritable(string folderpath)
        {
            try
            {
                string filename = Guid.NewGuid().ToString() + ".txt";
                System.IO.File.Create(folderpath + filename).Close();
                System.IO.File.Delete(folderpath + filename);
            }
            catch (System.UnauthorizedAccessException)
            {
                return false;
            }

            return true;
        }

        private bool _isChanged = false;
        /// <summary>
        /// Gets the object's changed status
        /// </summary>
        /// <value>True if the object's content has changed since the last call to AcceptChanges().</value>
        [XmlIgnore]
        public bool IsChanged
        {
            get { return _isChanged; }
            set { SetProperty(ref _isChanged, value, false); }
        }

        /// <summary>
        /// Resets the object’s state to unchanged by accepting the modifications.
        /// </summary>
        public void AcceptChanges()
        {
            IsChanged = false;
        }

        /// <summary>
        /// Helper function to implement a property setter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldValue">The backing field for the property</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="markChanged">If true, and the property has changed, will mark the object as changed</param>
        /// <returns>True if the property changed</returns>
        /// <param name="propertyName">The name of the property</param>
        /// <remarks>
        /// If the property value has changed raises the PropertyChanged event.
        /// Marks the property as changed only if <paramref name="markChanged"/> is true and the property value has changed.
        /// The <paramref name="markChanged"/> parameter is normally the inverse of the XmlIgore attribute.
        /// </remarks>
        protected bool SetProperty<T>(ref T oldValue, T newValue, bool markChanged, [CallerMemberName] string propertyName = "")
            where T : System.IEquatable<T>
        {
            if (oldValue != null && oldValue.Equals(newValue))
                return false;
            oldValue = newValue;
            OnPropertyChanged(propertyName);
            if (markChanged)
                IsChanged = true;
            UpdateEnabledProperties();
            return true;
        }

        /// <summary>
        /// Raise the PropertyChanged event.
        /// </summary>
        /// <param name="name">Name of the property that was changed.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
