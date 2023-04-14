using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioSplit
{
    internal class NameParseResult
    {
        public bool ParseSuccess { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public string SiteName { get; set; } = string.Empty;

        // Dates with leap year are a problem. If you set the day to 29 before you set the year
        // to a leap year, you'll get an exception when trying to construct a DateTime object.
        // To avoid problems with this I keep track of year, month, day, hour, minute, and second
        // as individual variables. 
        public int Year { get; set; } = DateTime.MinValue.Year;
        public int Month { get; set; } = DateTime.MinValue.Month;
        public int Day { get; set; } = DateTime.MinValue.Day;
        public int Hour { get; set; } = DateTime.MinValue.Hour;
        public int Minute { get; set; } = DateTime.MinValue.Minute;
        public int Second { get; set; } = DateTime.MinValue.Second;
        public DateTime Date
        {
            get
            {
                try
                {
                    DateTime dt = new DateTime(Year, Month, Day, 0, 0, 0);
                    return dt;
                }
                catch (Exception)
                {
                    return DateTime.MinValue;
                }
            }
        }
        public DateTime Time
        {
            get
            {
                try
                {
                    DateTime dt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 
                        Hour, Minute, Second);
                    return dt;
                }
                catch (Exception)
                {
                    return DateTime.MinValue;
                }
            }
        }

    }
}
