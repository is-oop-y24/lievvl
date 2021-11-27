using Banks.Entities;

namespace Banks.Services
{
    public interface IFactory
    {
        void AttachBank(Bank bank);
        AbstractAccount CreateAccount(Client client);
    }
}