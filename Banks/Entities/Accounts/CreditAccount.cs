using System;
using Banks.Services;
using Banks.Tools;

namespace Banks.Entities.Accounts
{
    public class CreditAccount : AbstractAccount
    {
        private float _commission;
        public CreditAccount(Client client, DateTime expiredDate, float commission)
            : base(client, expiredDate)
        {
            _commission = commission;
        }

        public override void Update(string message)
        {
            throw new System.NotImplementedException();
        }

        internal override void CalculateInterest()
        {
            if (Money < 0)
            {
                InterestMoney -= _commission;
            }
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

            SetMoney(Money - money);
        }
    }
}