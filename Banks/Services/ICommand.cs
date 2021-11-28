namespace Banks.Services
{
    public interface ICommand
    {
        double GetMoney();
        void Execute();
        void DenyCommand();
    }
}