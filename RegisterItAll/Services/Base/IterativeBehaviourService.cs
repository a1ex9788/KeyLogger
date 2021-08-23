using System;
using System.Timers;

namespace RegisterItAll.Services.Base
{
    public abstract class IterativeBehaviourService : Service
    {
        protected abstract int ExecutionIntervalInSeconds { get; }

        public override void Run()
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