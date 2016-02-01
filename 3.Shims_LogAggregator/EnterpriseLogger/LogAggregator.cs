namespace Logger.LogAggregator
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text.RegularExpressions;

    public class LogAggregator
    {
        /// <summary>
        /// Returns all log content from log files in a directory that are created within certain date range.
        /// Date of the file is determined by the file name.
        /// </summary>
        /// <param name="logDirPath">Directory to search for log files.</param>
        /// <param name="daysInPast">Used to determine the date ranege as number of days back from today's date.</param>
        /// <returns></returns>
        public string[] AggregateLogs(string logDirPath, int daysInPast)
        {
            var mergedLines = new List<string>();
            var filePaths = Directory.GetFiles(logDirPath, "*.log");
            foreach (var filePath in filePaths)
            {
                if (this.IsInDateRange(filePath, daysInPast))
                {
                    mergedLines.AddRange(File.ReadAllLines(filePath));
                }
            }

            return mergedLines.ToArray();
        }

        /// <summary>
        /// Checks if a given file path is within the date range. File path format must be "{LogName}_yyyMMdd.log"
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="daysInPast"></param>
        /// <returns></returns>
        private bool IsInDateRange(string filePath, int daysInPast)
        {
            string logName = Path.GetFileNameWithoutExtension(filePath);

            var isValidFileName = Regex.IsMatch(logName, @"\d{8}$");
            if (!isValidFileName)
            {
                throw new LogFileException("Invalid file name. The required format is \"{ LogName }_yyyMMdd.log\"");
            }

            if (logName.Length < 8)
            {
                return false;
            }

            string logDayString = logName.Substring(logName.Length - 8, 8);
            DateTime logDay;
            DateTime today = DateTime.Today;
            if (DateTime.TryParseExact(logDayString, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out logDay))
            {
                return logDay.AddDays(daysInPast) >= today;
            }

            return false;
        }
    }
}
