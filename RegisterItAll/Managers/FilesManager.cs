using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace RegisterItAll.Managers
{
    public static class FilesManager
    {
        private static string WorkingDirectory = Directory.GetCurrentDirectory();
        private static string LogsFile = Path.Combine(WorkingDirectory, "logs.txt");
        private static string ScreenshotsPrefix = "screenshot_";

        public static void SaveLog(string log)
        {
            File.AppendAllText(LogsFile, log);
        }

        public static string GetLogsFile()
        {
            return LogsFile;
        }

        public static void RemoveLogsFile()
        {
            if (!File.Exists(LogsFile))
            {
                return;
            }

            File.Delete(LogsFile);
        }

        public static void SaveScreenshot(Bitmap bitmap)
        {
            // Example: screenshot_21.08.2021_15.44.52.jpeg
            string snapshotName = $"{ScreenshotsPrefix}{DateTime.Now.ToString().Replace('/', '.').Replace(' ', '_').Replace(':', '.')}.jpeg";
            string snapshotPath = Path.Combine(WorkingDirectory, snapshotName);

            bitmap.Save(snapshotPath, ImageFormat.Jpeg);
        }

        public static IEnumerable<string> GetScreenshots()
        {
            return Directory.GetFiles(WorkingDirectory, $"*{ScreenshotsPrefix}*");
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
    }
}