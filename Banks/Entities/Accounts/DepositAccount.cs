using System;
using Banks.Services;
using Banks.Tools;

namespace Banks.Entities.Accounts
{
    public class DepositAccount : AbstractAccount
    {
        private IHandler _depositHandler;
        public DepositAccount(Client client, DateTime expiredDate, IHandler depositHandler)
            : base(client, expiredDate)
        {
            _depositHandler = depositHandler;
        }

        internal override void CalculateInterest()
        {
            if (_depositHandler == null)
            {
                throw new BankException("Not implemented depositHandler!");
            }

            _depositHandler.Handle(this);
        }

        internal override void AddMoney(double money)
        {
            if (money < 0)
            {
                throw new BankException("Money cannot be < 0!");
            }

            SetMoney(Money + money);
        }

        internal override void DecreaseMoney(double money)
        {
            if (ExpiredDate > DateTime.Today)
            {
                throw new BankException("Deposit account cannot withdraw money until its expired!");
            }

            if (Money - money < 0)
            {
                throw new BankException("DepositAccount cannot be negative!");
            }

            SetMoney(Money - money);
        }
    }
}