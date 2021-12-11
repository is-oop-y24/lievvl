using BackupsExtra.Services;

namespace BackupsExtra.Loggers
{
    public class UsualLazyLogger : ILogger
    {
        public void Log(string message)
        {
            return;
        }

        public void SetHandler(ILoggerHandler prefixHandler)
        {
            return;
        }
    }
}