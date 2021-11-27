﻿using System;
using Banks.Entities.Accounts;
using Banks.Services;
using Banks.Tools;

namespace Banks.Entities.Factories
{
    public class CreditAccountFactory : IFactory
    {
        public AbstractAccount CreateAccount(Bank bank, Client client)
        {
            if (client == null)
            {
                throw new BankException("client == null!");
            }

            return new CreditAccount(
                client,
                DateTime.Today.AddYears(bank.YearsLiveOfAccounts),
                bank.CreditCommission);
        }
    }
}