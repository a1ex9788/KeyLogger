using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace RegisterItAll.Managers
{
    public static class FilesManager
    {
        private static string WorkingDirectory = Directory.GetCurrentDirectory();
        private static string LogsPath = Path.Combine(WorkingDirectory, "logs");
        private static string LogsPrefix = "log_";
        private static string ScreenshotsPath = Path.Combine(WorkingDirectory, "screenshots");
        private static string ScreenshotsPrefix = "screenshot_";

        public static void SaveLog(string log)
        {
            string logFileName = $"{LogsPrefix}{GetDateTimeSufix()}.txt";
            string logFilePath = Path.Combine(LogsPath, logFileName);

            if (!Directory.Exists(LogsPath))
            {
                Directory.CreateDirectory(LogsPath);
            }

            File.AppendAllText(logFilePath, log);
        }

        public static string GetLogsFile()
        {
            string logsFileName = $"logsFile_{GetDateTimeSufix()}.txt";
            string logsFilePath = Path.Combine(LogsPath, logsFileName);
            string logsFileDirectory = logsFilePath.Substring(0, logsFilePath.Length - 4);
            string logs = string.Empty;

            if (!Directory.Exists(logsFileDirectory))
            {
                Directory.CreateDirectory(logsFileDirectory);
            }

            foreach (string logFile in Directory.GetFiles(LogsPath, $"*{LogsPrefix}*").OrderBy(f => Path.GetFileName(f)))
            {
                logs += File.ReadAllText(logFile);

                File.Move(logFile, Path.Combine(logsFileDirectory, Path.GetFileName(logFile)));
            }

            File.WriteAllText(logsFilePath, logs);

            return logsFilePath;
        }

        public static void RemoveLogsFile(string logsFilePath)
        {
            if (!File.Exists(logsFilePath))
            {
                return;
            }

            File.Delete(logsFilePath);

            Directory.Delete(logsFilePath.Substring(0, logsFilePath.Length - 4), recursive: true);
        }

        public static void SaveScreenshot(Bitmap bitmap)
        {
            string screenshotName = $"{ScreenshotsPrefix}{GetDateTimeSufix()}.jpeg";
            string screenshotPath = Path.Combine(ScreenshotsPath, screenshotName);

            if (!Directory.Exists(ScreenshotsPath))
            {
                Directory.CreateDirectory(ScreenshotsPath);
            }

            bitmap.Save(screenshotPath, ImageFormat.Jpeg);
        }

        public static IEnumerable<string> GetScreenshots()
        {
            return Directory.GetFiles(ScreenshotsPath, $"*{ScreenshotsPrefix}*");
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