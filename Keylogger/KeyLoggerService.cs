using System;
using System.ServiceProcess;

namespace Keylogger
{
    public partial class KeyLoggerService : ServiceBase
    {
        public KeyLoggerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnContinue()
        {
            Console.WriteLine("qwegdfg");
        }

        protected override void OnStop()
        {
        }
    }
}