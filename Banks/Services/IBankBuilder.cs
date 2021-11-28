using Banks.Entities;

namespace Banks.Services
{
    public interface IBankBuilder
    {
        void Reset();

        void SetDebitInterestRate(double interestRate);
        void SetDepositHandler(IHandler depositHandler);
        void SetYearsLiveOfAccount(int years);
        void SetCreditCommission(double commission);
        void SetFishyLimit(double fishyLimit);

        Bank GetBank();
    }
}