using RegisterItAll.Services.Base;
using System;
using System.Threading.Tasks;

namespace RegisterItAll.Services
{
    public partial class ScreenCapturerService : ExecutableAsConsoleApplicationService
    {
        protected override void OnStart(string[] args)
        {
            while (true)
            {
                Console.WriteLine("-");

                Task.Delay(50);
            }
        }
    }
}