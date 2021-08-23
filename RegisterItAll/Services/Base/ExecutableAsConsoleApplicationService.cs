using System;
using System.ServiceProcess;

namespace RegisterItAll.Services.Base
{
    public abstract class ExecutableAsConsoleApplicationService : ServiceBase
    {
        public void ExecuteAsConsoleApplication(string[] args)
        {
            this.OnStart(args);

            Console.ReadLine();

            this.OnStop();
        }
    }
}