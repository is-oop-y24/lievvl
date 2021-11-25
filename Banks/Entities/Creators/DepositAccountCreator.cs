using System;
using Banks.Entities.Accounts;
using Banks.Services;

namespace Banks.Entities.Creators
{
    public class DepositAccountCreator : ICreator
    {
        private Bank _bank;

        public DepositAccountCreator()
        {
        }

        public void AttachBank(Bank bank)
        {
            _bank = bank;
        }

        public AbstractAccount CreateAccount(Client client)
        {
            return new DepositAccount(
                client,
                DateTime.Today.AddYears(_bank.YearsLiveOfAccounts),
                _bank.DepositHandler);
        }
    }
}