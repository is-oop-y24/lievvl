using Banks.Entities;

namespace Banks.Services
{
    public interface IFactory
    {
        AbstractAccount CreateAccount(Bank bank, Client client);
    }
}