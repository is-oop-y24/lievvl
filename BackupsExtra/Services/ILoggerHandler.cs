namespace BackupsExtra.Services
{
    public interface ILoggerHandler
    {
        void SetNext(ILoggerHandler nextHandler);
        string Handle();
    }
}