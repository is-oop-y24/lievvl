namespace BackupsExtra.Services
{
    public interface ILogger
    {
        void Log(string message);
        void SetHandler(ILoggerHandler prefixHandler);
    }
}