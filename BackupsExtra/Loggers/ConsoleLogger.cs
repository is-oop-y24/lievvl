using System;
using BackupsExtra.Services;

namespace BackupsExtra.Loggers
{
    public class ConsoleLogger : ILogger
    {
        private ILoggerHandler _prefixHandler;
        public void Log(string message)
        {
            string toWrite = string.Empty;

            if (_prefixHandler != null)
            {
                toWrite += _prefixHandler.Handle();
            }

            toWrite += message;
            Console.WriteLine(toWrite);
        }

        public void SetHandler(ILoggerHandler prefixHandler)
        {
            _prefixHandler = prefixHandler;
        }
    }
}