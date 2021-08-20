using System;
using System.ServiceProcess;

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
            while (true)
            {
                Console.WriteLine("I'm working.");
            }
        }

        protected override void OnStop()
        {
        }
    }
}
