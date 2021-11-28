using System;
using Banks.Entities.Accounts;
using Banks.Services;
using Banks.Tools;

namespace Banks.Entities.Factories
{
    public class DepositAccountFactory : IFactory
    {
        public AbstractAccount CreateAccount(Bank bank, Client client)
        {
            if (client == null)
            {
                throw new BankException("client == null!");
            }

            return new DepositAccount(
                client,
                DateTime.Today.AddYears(bank.YearsLiveOfAccounts),
                bank.DepositHandler);
        }
    }
}