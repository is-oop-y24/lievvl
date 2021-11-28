using System;
using Banks.Services;
using Banks.Tools;

namespace Banks.Entities.Accounts
{
    public class CreditAccount : AbstractAccount
    {
        private double _commission;
        public CreditAccount(Client client, DateTime expiredDate, double commission)
            : base(client, expiredDate)
        {
            _commission = commission;
        }

        internal override void CalculateInterest()
        {
            if (Money < 0)
            {
                InterestMoney -= _commission;
            }
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
            if (money < 0)
            {
                throw new BankException("Money cannot be < 0!");
            }

            SetMoney(Money - money);
        }
    }
}