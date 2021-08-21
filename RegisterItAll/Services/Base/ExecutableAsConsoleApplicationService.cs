using System;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace RegisterItAll.Services.Base
{
    public abstract class ExecutableAsConsoleApplicationService : ServiceBase
    {
        public async Task ExecuteAsConsoleApplication(string[] args)
        {
            await Task.Delay(1);

            this.OnStart(args);

            Console.ReadLine();

            this.OnStop();
        }
    }
}