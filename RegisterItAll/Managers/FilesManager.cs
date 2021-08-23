using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace RegisterItAll.Managers
{
    public static class FilesManager
    {
        private static string WorkingDirectory = Directory.GetCurrentDirectory();

        private static string LogsDirectory = Path.Combine(WorkingDirectory, "logs");
        private static string LogsFile = Path.Combine(LogsDirectory, "logs.txt");
        private static string ScreenshotsDirectory = Path.Combine(WorkingDirectory, "screenshots");
        private static string ScreenshotsPrefix = "screenshot_";

        private static object MonitorControl = new object();

        public static void SaveLog(string log)
        {
            Monitor.Enter(MonitorControl);

            if (!Directory.Exists(LogsDirectory))
            {
                Directory.CreateDirectory(LogsDirectory);
            }

            File.AppendAllText(LogsFile, log);

            Monitor.Exit(MonitorControl);
        }

        public static string GetLogsFile()
        {
            string temporalLogsFileName = $"logs_{GetDateTimeSufix()}.txt";
            string temporalLogsFilePath = Path.Combine(LogsDirectory, temporalLogsFileName);

            Monitor.Enter(MonitorControl);

            if (File.Exists(LogsFile))
            {
                File.Move(LogsFile, temporalLogsFilePath);
            }
            else
            {
                if (!Directory.Exists(LogsDirectory))
                {
                    Directory.CreateDirectory(LogsDirectory);
                }

                File.AppendAllText(temporalLogsFilePath, null);
            }

            Monitor.Exit(MonitorControl);

            return temporalLogsFilePath;
        }

        public static void RemoveLogsFile(string temporalLogsFile)
        {
            File.Delete(temporalLogsFile);
        }

        public static string SaveScreenshot(Bitmap bitmap)
        {
            string screenshotName = $"{ScreenshotsPrefix}{GetDateTimeSufix()}.jpeg";
            string screenshotPath = Path.Combine(ScreenshotsDirectory, screenshotName);

            if (!Directory.Exists(ScreenshotsDirectory))
            {
                Directory.CreateDirectory(ScreenshotsDirectory);
            }

            bitmap.Save(screenshotPath, ImageFormat.Jpeg);

            return screenshotName;
        }

        public static IEnumerable<string> GetScreenshots()
        {
            if (!Directory.Exists(ScreenshotsDirectory))
            {
                return new List<string>();
            }

            return Directory.GetFiles(ScreenshotsDirectory, $"*{ScreenshotsPrefix}*");
        }

        public static void RemoveScreenshots(IEnumerable<string> screenshots)
        {
            foreach (string screenshot in screenshots)
            {
                if (!File.Exists(screenshot))
                {
                    continue;
                }

                File.Delete(screenshot);
            }
        }

        private static string GetDateTimeSufix()
        {
            return DateTime.Now.ToString().Replace('/', '.').Replace(' ', '_').Replace(':', '.');
        }
    }
}