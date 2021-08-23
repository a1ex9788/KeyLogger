using RegisterItAll.Managers;
using RegisterItAll.Services.Base;
using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace RegisterItAll.Services
{
    public partial class ScreenCapturerService : IterativeBehaviourService
    {
        protected override int ExecutionIntervalInSeconds => Convert.ToInt32(ConfigurationManager.AppSettings.Get("ScreenshotFrequencyInSeconds"));

        protected override void ExecuteIterationImplementation()
        {
            Rectangle screen = Screen.GetBounds(new Point(0, 0));

            Bitmap bitmap = new Bitmap(screen.Width, screen.Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            FilesManager.SaveScreenshot(bitmap);
        }
    }
}