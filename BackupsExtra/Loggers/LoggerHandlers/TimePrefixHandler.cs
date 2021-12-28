using System;
using BackupsExtra.Services;

namespace BackupsExtra.Loggers.LoggerHandlers
{
    public class TimePrefixHandler : ILoggerHandler
    {
        private ILoggerHandler _nextHandler;

        public TimePrefixHandler()
        {
        }

        public string Handle()
        {
            string prefix = DateTime.Now.ToString();
            if (_nextHandler != null)
            {
                prefix = prefix + _nextHandler.Handle();
            }

            return $"{prefix}: ";
        }

        public void SetNext(ILoggerHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }
    }
}