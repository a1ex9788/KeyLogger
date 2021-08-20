using System;
using System.ServiceProcess;

namespace RegisterItAll
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                RegisterItAllService RegisterItAllService = new RegisterItAllService();

                RegisterItAllService.ExecuteAsConsoleApplication(args);
            }
            else
            {
                ServiceBase[] ServicesToRun;

                ServicesToRun = new ServiceBase[]
                {
                    new RegisterItAllService()
                };

                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}