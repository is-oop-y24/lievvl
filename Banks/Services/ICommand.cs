namespace Banks.Services
{
    public interface ICommand
    {
        public double GetMoney();
        internal void Execute();
        public void DenyCommand();
    }
}