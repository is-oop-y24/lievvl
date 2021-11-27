using Banks.Services;
using Banks.Tools;

namespace Banks.Entities.Command
{
    public class WithdrawCommand : ICommand
    {
        private AbstractAccount _accountFrom;
        private double _money;
        private bool _denied;

        public WithdrawCommand(AbstractAccount accountFrom, double money)
        {
            if (accountFrom == null)
            {
                throw new BankException("accountFrom = null!");
            }

            if (accountFrom.Client.IsAccountFishy)
            {
                if (accountFrom.Client.AttachedBank.FishyLimit < money)
                {
                    throw new BankException("Command cannot be executed because money < fishyLimit!");
                }
            }

            _accountFrom = accountFrom;
            _money = money;
            _denied = true;
        }

        public double GetMoney()
        {
            return _money;
        }

        public void Execute()
        {
            _denied = false;
            _accountFrom.DecreaseMoney(_money);
            _accountFrom.AddCommand(this);
        }

        public void DenyCommand()
        {
            if (_denied)
            {
                throw new BankException("Operation was already denied/wasn't executed!");
            }

            _accountFrom.RemoveCommand(this);
            _accountFrom.AddMoney(_money);
            _denied = true;
        }
    }
}