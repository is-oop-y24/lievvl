using System;
using Banks.Services;
using Banks.Tools;

namespace Banks.Entities.Accounts
{
    public class DebitAccount : AbstractAccount
    {
        private float _interestRate;
        public DebitAccount(Client client, DateTime expiredDate, float interestRate)
            : base(client, expiredDate)
        {
            _interestRate = interestRate;
        }

        public override void Update(string message)
        {
            throw new System.NotImplementedException();
        }

        internal override void CalculateInterest()
        {
            InterestMoney += Money * _interestRate;
        }

        internal override void AddMoney(float money)
        {
            if (money < 0)
            {
                throw new BankException("Money cannot be < 0!");
            }

            SetMoney(Money + money);
        }

        internal override void DecreaseMoney(float money)
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