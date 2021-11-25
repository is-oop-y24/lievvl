using System;
using Banks.Entities.Accounts;
using Banks.Services;

namespace Banks.Entities.Creators
{
    public class CreditAccountCreator : ICreator
    {
        private Bank _bank;

        public CreditAccountCreator()
        {
        }

        public AbstractAccount CreateAccount(Client client)
        {
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