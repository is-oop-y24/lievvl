using Banks.Entities;
using Banks.Services;

namespace Banks.Builders
{
    public class BankBuilder : IBankBuilder
    {
        private int _id;
        private Bank _bank;

        internal BankBuilder()
        {
            _id = 0;
            _bank = new Bank(_id);
        }

        public void Reset()
        {
            _bank = new Bank(_id);
        }

        public void SetDebitInterestRate(double interestRate)
        {
            _bank.DebitInterestRate = interestRate;
        }

        public void SetDepositHandler(IHandler depositHandler)
        {
            _bank.DepositHandler = depositHandler;
        }

        public void SetCreditCommission(double commission)
        {
            _bank.CreditCommission = commission;
        }

        public void SetYearsLiveOfAccount(int years)
        {
            _bank.YearsLiveOfAccounts = years;
        }

        public void SetFishyLimit(double fishyLimit)
        {
            _bank.FishyLimit = fishyLimit;
        }

        public Bank GetBank()
        {
            Bank result = _bank;
            _id++;
            Reset();
            return result;
        }
    }
}