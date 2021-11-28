using System;
using Banks.Services;

namespace Banks.Entities.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string information)
        {
            Console.WriteLine(information);
        }
    }
}