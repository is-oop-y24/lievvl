using Banks.Services;

namespace Banks.Entities.DepositHandlers
{
    public class UsualDepositHandler : IHandler
    {
        private IHandler _nextHandler;
        private double _upperSum;
        private double _interestRate;

        public UsualDepositHandler(double upperSum, double interestRate)
        {
            _upperSum = upperSum;
            _interestRate = interestRate;
        }

        public void Handle(AbstractAccount account)
        {
            if (account.Money <= _upperSum)
            {
                account.InterestMoney += account.Money * _interestRate;
                return;
            }

            _nextHandler.Handle(account);
        }

        public IHandler SetNext(IHandler nextHandler)
        {
            _nextHandler = nextHandler;
            return nextHandler;
        }
    }
}