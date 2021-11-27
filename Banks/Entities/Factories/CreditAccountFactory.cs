using System;
using Banks.Entities.Accounts;
using Banks.Services;
using Banks.Tools;

namespace Banks.Entities.Factories
{
    public class CreditAccountFactory : IFactory
    {
        private Bank _bank;

        public AbstractAccount CreateAccount(Client client)
        {
            if (client == null)
            {
                throw new BankException("client == null!");
            }

            return new CreditAccount(
                client,
                DateTime.Today.AddYears(_bank.YearsLiveOfAccounts),
                _bank.CreditCommission);
        }

        public void AttachBank(Bank bank)
        {
            _bank = bank;
        }
    }
}