#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2011 ZScreen Developers

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v2)

using System.ComponentModel;
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

    public enum EmailProtocol
    {
        [Description("SMTP")]
        Smtp,
        [Description("IMAP")]
        Imap
    }
}