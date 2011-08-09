using System.IO;
using System.Net;
using System.Net.Mail;

namespace UploadersLib.FileUploaders
{
    public class Email
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string FromEmail { get; set; }
        public string Password { get; set; }

        public void Send(string toEmail, string subject, string body, Stream stream, string fileName)
        {
            MailAddress fromAddress = new MailAddress(FromEmail);
            MailAddress toAddress = new MailAddress(toEmail);

            SmtpClient smtp = new SmtpClient
            {
                Host = SmtpServer,
                Port = SmtpPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, Password)
            };

            using (MailMessage message = new MailMessage(fromAddress, toAddress))
            {
                message.Subject = subject;
                message.Body = body;
                stream.Position = 0;
                Attachment attachment = new Attachment(stream, fileName);
                message.Attachments.Add(attachment);

                smtp.Send(message);
            }
        }
    }
}