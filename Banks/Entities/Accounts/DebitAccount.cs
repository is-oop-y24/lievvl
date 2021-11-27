using System;
using Banks.Services;
using Banks.Tools;

namespace Banks.Entities.Accounts
{
    public class DebitAccount : AbstractAccount
    {
        private double _interestRate;
        public DebitAccount(Client client, DateTime expiredDate, double interestRate)
            : base(client, expiredDate)
        {
            _interestRate = interestRate;
        }

        internal override void CalculateInterest()
        {
            InterestMoney += Money * _interestRate;
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

            if (Money - money < 0)
            {
                throw new BankException("DebitAccount cannot be negative!");
            }

            SetMoney(Money - money);
        }
    }
}