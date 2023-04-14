using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
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
    /// You should create only once instance of this class. Pass this instance
    /// to any class or method that needs to access the settings.
    /// </para>
    /// </remarks>
    public class Settings : SimpleSettings.SettingsBase
    {
        // Properties that are serialized, and trigger the PropertyChanged events.

        // AutoExcludeFolder and AutoOutputFolder properties were used in AudioSplit
        // version before 3.0. It has been replaced in version 3.0 and later by
        // template variables in the ExcludeFolder and OutputFolder properties.
        // In order to provide a smooth upgrade experience, we define methods
        // ShouldSerializeAutoExcludeFolder and ShouldSerializeAutoOutputFolder.
        // These methods always return false. This causes the AutoExcludeFolder and
        // AutoOutputFolder properties to not be serialized, but still allow them to 
        // be serialized. 
        // In the Load method below, after deserialization we check the values of the
        // AutoExcludeFolder and AutoOutputFolder properties. If true, we set the 
        // OuputFolder and/or ExcludeFolder properties to equivalent values using 
        // template variables.
        private bool autoExcludeFolderValue = false;
        public bool AutoExcludeFolder
        {
            get { return autoExcludeFolderValue; }
            set { SetProperty(ref autoExcludeFolderValue, value, true); }
        }
        public bool ShouldSerializeAutoExcludeFolder() { return false; }

        private bool autoOutputFolderValue = false;
        public bool AutoOutputFolder
        {
            get { return autoOutputFolderValue; }
            set { SetProperty(ref autoOutputFolderValue, value, true); }
        }
        public bool ShouldSerializeAutoOutputFolder() { return false; }

        private bool excludeDataValue = false;
        public bool ExcludeData
        {
            get { return excludeDataValue; }
            set { SetProperty(ref excludeDataValue, value, true); }
        }

        private string excludeFolderValue = "@OutDir\\Exclude";
        public string ExcludeFolder
        {
            get { return excludeFolderValue; }
            set { SetProperty(ref excludeFolderValue, value, true); }
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

        private Point formMainLocationValue;
        public Point FormMainLocation
        {
            get { return formMainLocationValue; }
            set { SetProperty(ref formMainLocationValue, value, true); }
        }

        private Size formMainSizeValue;
        public Size FormMainSize
        {
            get { return formMainSizeValue; }
            set { SetProperty(ref formMainSizeValue, value, true); }
        }

        private Point formTemplateHelpLocationValue;
        public Point FormTemplateHelpLocation
        {
            get { return formTemplateHelpLocationValue; }
            set { SetProperty(ref formTemplateHelpLocationValue, value, true); }
        }

        private Size formTemplateHelpSizeValue;
        public Size FormTemplateHelpSize
        {
            get { return formTemplateHelpSizeValue; }
            set { SetProperty(ref formTemplateHelpSizeValue, value, true); }
        }

        private string inputNameTemplateValue = "@Site_@yyyy@MM@dd_@HH@mm@ss";
        public string InputNameTemplate
        {
            get { return inputNameTemplateValue; }
            set { SetProperty(ref inputNameTemplateValue, value, true); }
        }

        private string outputChannelsValue = "Stereo";
        public string OutputChannels
        {
            get { return outputChannelsValue; }
            set { SetProperty(ref outputChannelsValue, value, true); }
        }

        private string outputFileTemplateValue = "@Site_@yyyy@MM@dd_@HH@mm@ss";
        public string OutputFileTemplate
        {
            get { return outputFileTemplateValue; }
            set { SetProperty(ref outputFileTemplateValue, value, true); }
        }

        private string outputFolderValue = "@InDir\\..\\Hourly";
        public string OutputFolder
        {
            get { return outputFolderValue; }
            set { SetProperty(ref outputFolderValue, value, true); }
        }

        private string outputFormatValue = "wav";
        public string OutputFormat
        {
            get { return outputFormatValue; }
            set { SetProperty(ref outputFormatValue, value, true); }
        }

        private bool removeXingHeaderValue = false;
        public bool RemoveXingHeader
        {
            get { return removeXingHeaderValue; }
            set { SetProperty(ref removeXingHeaderValue, value, true); }
        }

        private bool showDateInternationalValue = true;
        public bool ShowDateInternational
        {
            get { return showDateInternationalValue; }
            set { SetProperty(ref showDateInternationalValue, value, true); }
        }

        private bool showTime24Hour = true;
        public bool ShowTime24Hour
        {
            get { return showTime24Hour; }
            set { SetProperty(ref showTime24Hour, value, true); }
        }

        private string siteNameValue = "";
        public string SiteName
        {
            get { return siteNameValue; }
            set { SetProperty(ref siteNameValue, value, true); }
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

        private bool splitEnabledValue = true;
        public bool SplitEnabled
        {
            get { return splitEnabledValue; }
            set { SetProperty(ref splitEnabledValue, value, true); }
        }

        private DateTime startDateValue = DateTime.Today;
        public DateTime StartDate
        {
            get { return startDateValue; }
            set { SetProperty(ref startDateValue, value, true); }
        }

        private bool startSplitOnHourValue = true;
        public bool StartSplitOnHour
        {
            get { return startSplitOnHourValue; }
            set { SetProperty(ref startSplitOnHourValue, value, true); }
        }

        private DateTime startTimeValue = DateTime.Today;
        public DateTime StartTime
        {
            get { return startTimeValue; }
            set { SetProperty(ref startTimeValue, value, true); }
        }

        private bool writeLogFileValue = false;
        public bool WriteLogFile
        {
            get { return writeLogFileValue; }
            set { SetProperty(ref writeLogFileValue, value, true); }
        }

        // Properties which are not persisted, but trigger property changed.
        private bool chkRemoveXingHeaderEnabledValue = false;
        [XmlIgnore]
        public bool ChkRemoveXingHeaderEnabled
        {
            get { return chkRemoveXingHeaderEnabledValue; }
            set { SetProperty(ref chkRemoveXingHeaderEnabledValue, value, true); }
        }

        private bool excludeFolderEnabledValue = false;
        [XmlIgnore]
        public bool ExcludeFolderEnabled
        {
            get { return excludeFolderEnabledValue; }
            set { SetProperty(ref excludeFolderEnabledValue, value, true); }
        }

        private bool excludeTimesEnabledValue = false;
        [XmlIgnore]
        public bool ExcludeTimesEnabled
        {
            get { return excludeTimesEnabledValue; }
            set { SetProperty(ref excludeTimesEnabledValue, value, true); }
        }

        // Contains the full path to the folder (without the filename) of the first entry
        // in the Input Files table.
        // This is set by FormMain whenever the input files table changes.
        private string inDirValue = "";
        [XmlIgnore]
        public string InDir
        {
            get { return inDirValue; }
            set { SetProperty(ref inDirValue, value, true); }
        }

        // This is set by FormMain, depending on whether the input has multiple channels.
        private bool outputChannelsEnabledValue = true;
        [XmlIgnore]
        public bool OutputChannelsEnabled
        {
            get { return outputChannelsEnabledValue; }
            set { SetProperty(ref outputChannelsEnabledValue, value, true); }
        }

        // Methods
        // Load, Save, and SaveIfChanged methods must be defined.
        public static Settings Load()
        {
            Settings settings = SimpleSettings.SettingsBase.Load<Settings>();
            if (settings != null)
            {
                // Upgrade settings. If AutoExcludeFolder and/or AutoOutputFolder are set, set the
                // equivalent template based folder values.
                if (settings.AutoExcludeFolder)
                {
                    settings.AutoExcludeFolder = false;
                    settings.ExcludeFolder = "@OutDir\\Exclude";
                }
                if (settings.AutoOutputFolder)
                {
                    settings.AutoOutputFolder = false;
                    settings.OutputFolder = "@InDir\\..\\Hourly";
                }
            }
            return settings;
        }
        public void Save()
        {
            base.Save(typeof(Settings));
        }
        public void SaveIfChanged()
        {
            base.SaveIfChanged(typeof(Settings));
        }

        /// <summary>
        /// Raise the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property that was changed.</param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(OutputFormat))
            {
                ChkRemoveXingHeaderEnabled = OutputFormat == "mp3";
            }
            if (propertyName == nameof(ExcludeData))
            {
                ExcludeFolderEnabled = ExcludeData;
            }
            if (propertyName == nameof(SplitEnabled) ||
                propertyName == nameof(ExcludeData))
            {
                ExcludeTimesEnabled = SplitEnabled && ExcludeData;
            }
        }
    }
}
