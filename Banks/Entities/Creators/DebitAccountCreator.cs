using System;
using Banks.Entities.Accounts;
using Banks.Services;

namespace Banks.Entities.Creators
{
    public class DebitAccountCreator : ICreator
    {
        private Bank _bank;

        public DebitAccountCreator()
        {
        }

        public void AttachBank(Bank bank)
        {
            _bank = bank;
        }

        public AbstractAccount CreateAccount(Client client)
        {
            return new DebitAccount(
                client,
                DateTime.Today.AddYears(_bank.YearsLiveOfAccounts),
                _bank.DebitInterestRate);
        }
    }
}