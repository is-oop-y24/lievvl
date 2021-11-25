using Banks.Services;
using Banks.Tools;

namespace Banks.Entities.Command
{
    public class RefillCommand : ICommand
    {
        private AbstractAccount _accountTo;
        private float _money;
        private bool _denied;

        public RefillCommand(AbstractAccount accountTo, float money)
        {
            if (accountTo.Client.IsAccountFishy)
            {
                if (accountTo.Client.AttachedBank.FishyLimit < money)
                {
                    throw new BankException("Command cannot be executed because money > fishyLimit!");
                }
            }

            _accountTo = accountTo;
            _money = money;
            _denied = true;
        }

        public float GetMoney()
        {
            return _money;
        }

        void ICommand.Execute()
        {
            _denied = false;
            _accountTo.AddMoney(_money);
            _accountTo.AddCommand(this);
        }

        public void DenyCommand()
        {
            if (_denied)
            {
                throw new BankException("Operation was already denied/wasn't executed!");
            }

            _accountTo.RemoveCommand(this);
            _accountTo.DecreaseMoney(_money);
        }
    }
}