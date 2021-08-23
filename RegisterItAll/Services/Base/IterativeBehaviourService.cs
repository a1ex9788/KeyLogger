using System;
using System.Timers;

namespace RegisterItAll.Services.Base
{
    public abstract class IterativeBehaviourService : ExecutableAsConsoleApplicationService
    {
        protected abstract int ExecutionIntervalInSeconds { get; }

        protected override void OnStart(string[] args)
        {
            Timer timer = new Timer();
            timer.Interval = ExecutionIntervalInSeconds * 1000;
            timer.Elapsed += new ElapsedEventHandler(this.ExecuteIteration);
            timer.Start();
        }

        private void ExecuteIteration(object sender, ElapsedEventArgs args)
        {
            try
            {
                ExecuteIterationImplementation();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        protected abstract void ExecuteIterationImplementation();
    }
}