namespace Banks.Services
{
    public interface ICommand
    {
        public float GetMoney();
        internal void Execute();
        public void DenyCommand();
    }
}