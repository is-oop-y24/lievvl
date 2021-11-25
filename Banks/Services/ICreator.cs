using Banks.Entities;

namespace Banks.Services
{
    public interface ICreator
    {
        public void AttachBank(Bank bank);
        public AbstractAccount CreateAccount(Client client);
    }
}