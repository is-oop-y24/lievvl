using Banks.Entities;

namespace Banks.Services
{
    public interface IBankBuilder
    {
        void Reset();

        void SetDebitInterestRate(float interestRate);
        void SetDepositInterestRate(float interestRate);
        void SetDepositHandler(IHandler depositHandler);
        void SetCreditCommission(float commission);
        void SetFishyLimit(float fishyLimit);

        Bank GetBank();
    }
}