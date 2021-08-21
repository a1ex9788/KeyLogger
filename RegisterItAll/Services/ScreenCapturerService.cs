using RegisterItAll.Managers;
using RegisterItAll.Services.Base;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegisterItAll.Services
{
    public partial class ScreenCapturerService : ExecutableAsConsoleApplicationService
    {
        private const int DelaySecons = 5;

        protected override async void OnStart(string[] args)
        {
            while (true)
            {
                try
                {
                    TakeScreenshot();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                await Task.Delay(DelaySecons * 1000);
            }
        }

        private static void TakeScreenshot()
        {
            Rectangle screen = Screen.GetBounds(new Point(0, 0));

            Bitmap bitmap = new Bitmap(screen.Width, screen.Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            FilesManager.SaveScreenshot(bitmap);
        }
    }
}