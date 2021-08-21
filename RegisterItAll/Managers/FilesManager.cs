using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace RegisterItAll.Managers
{
    public static class FilesManager
    {
        private const string logsFile = "logs.txt";
        private const string screenshotsPrefix = "screenshot_";

        public static void SaveLog(string log)
        {
            File.AppendAllText(logsFile, log);
        }

        public static string GetLogs()
        {
            if (!File.Exists(logsFile))
            {
                return null;
            }

            return File.ReadAllText(logsFile);
        }

        public static void RemoveLogs()
        {
            if (!File.Exists(logsFile))
            {
                return;
            }

            File.Delete(logsFile);
        }

        public static void SaveScreenshot(Bitmap bitmap)
        {
            // Example: screenshot_21.08.2021_15.44.52.jpeg
            string snapshotName = $"{screenshotsPrefix}{DateTime.Now.ToString().Replace('/', '.').Replace(' ', '_').Replace(':', '.')}.jpeg";

            bitmap.Save(snapshotName, ImageFormat.Jpeg);
        }

        public static IEnumerable<string> GetScreenshots()
        {
            return Directory.GetFiles(Directory.GetCurrentDirectory(), $"*{screenshotsPrefix}*");
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