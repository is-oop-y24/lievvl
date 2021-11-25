namespace Banks.Services
{
    public interface IHandler
    {
        IHandler SetNext(IHandler nextHandler);
        void Handle(AbstractAccount account);
    }
}