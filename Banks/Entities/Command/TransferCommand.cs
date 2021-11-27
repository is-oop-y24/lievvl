using Banks.Services;
using Banks.Tools;

namespace Banks.Entities.Command
{
    public class TransferCommand : ICommand
    {
        private AbstractAccount _accountFrom;
        private AbstractAccount _accountTo;
        private bool _denied;
        private double _money;

        public TransferCommand(AbstractAccount accountFrom, AbstractAccount accountTo, double money)
        {
            if (accountFrom == null || accountTo == null)
            {
                throw new BankException("Received null instead of accountFrom/accountTo");
            }

            if (accountFrom.Client.IsAccountFishy)
            {
                if (accountFrom.Client.AttachedBank.FishyLimit < money)
                {
                    throw new BankException("Command cannot be executed because money < fishyLimit!");
                }
            }

            if (accountTo.Client.IsAccountFishy)
            {
                if (accountTo.Client.AttachedBank.FishyLimit < money)
                {
                    throw new BankException("Command cannot be executed because money < fishyLimit!");
                }
            }

            _accountFrom = accountFrom;
            _accountTo = accountTo;
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
            _accountTo.AddMoney(_money);
            _accountFrom.AddCommand(this);
            _accountTo.AddCommand(this);
        }

        public void DenyCommand()
        {
            if (_denied)
            {
                throw new BankException("Operation was already denied/wasn't executed!");
            }

            _accountFrom.RemoveCommand(this);
            _accountTo.RemoveCommand(this);
            _accountFrom.AddMoney(_money);
            _accountTo.DecreaseMoney(_money);
            _denied = true;
        }
    }
}