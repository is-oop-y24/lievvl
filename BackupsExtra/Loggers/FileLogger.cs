using System.IO;
using BackupsExtra.Services;

namespace BackupsExtra.Loggers
{
    public class FileLogger : ILogger
    {
        private StreamWriter _stream;
        private ILoggerHandler _prefixHandler;

        public FileLogger(string logPath)
        {
            if (File.Exists(logPath))
            {
                File.CreateText(logPath);
            }

            _stream = new StreamWriter(logPath);
        }

        public void Log(string message)
        {
            string toWrite = string.Empty;
            if (_prefixHandler != null)
            {
                toWrite = _prefixHandler.Handle();
            }

            toWrite += message;
            _stream.WriteLine(toWrite);
        }

        public void SetHandler(ILoggerHandler prefixHandler)
        {
            _prefixHandler = prefixHandler;
        }
    }
}