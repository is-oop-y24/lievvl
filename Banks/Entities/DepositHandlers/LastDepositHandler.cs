using Banks.Services;
using Banks.Tools;

namespace Banks.Entities.DepositHandlers
{
    public class LastDepositHandler : IHandler
    {
        private double _interestRate;

        public LastDepositHandler(double interestRate)
        {
            _interestRate = interestRate;
        }

        public void Handle(AbstractAccount account)
        {
            account.InterestMoney += account.Money * _interestRate;
        }

        public IHandler SetNext(IHandler nextHandler)
        {
            throw new BankException("LastDepositHandler cannot set next handler!");
        }
    }
}