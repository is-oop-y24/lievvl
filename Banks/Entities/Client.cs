using System.Collections.Generic;
using Banks.Entities.Loggers;
using Banks.Services;
using Banks.Tools;

namespace Banks.Entities
{
    public class Client : ISubscriber
    {
        private int _id;
        private string _name;
        private string _surname;
        private string _address;
        private string _passport;
        private Bank _bank;
        private ILogger _logger;
        private List<AbstractAccount> _clientsAccounts;

        public Client(int id)
        {
            _id = id;
            _name = null;
            _surname = null;
            _address = null;
            _passport = null;
            _logger = new LazyLogger();
            _clientsAccounts = new List<AbstractAccount>();
        }

        public int Id
        {
            get => _id;
        }

        public string Name
        {
            get => _name;
            internal set
            {
                if (value == null)
                {
                    throw new BankException("Name of client cannot be null!");
                }

                _name = value;
            }
        }

        public string Surname
        {
            get => _surname;
            internal set
            {
                if (value == null)
                {
                    throw new BankException("Surname of client cannot be null!");
                }

                _surname = value;
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                CheckFishyStatus();
            }
        }

        public string Passport
        {
            get => _passport;
            set
            {
                _passport = value;
                CheckFishyStatus();
            }
        }

        public Bank AttachedBank
        {
            get => _bank;
            internal set
            {
                _bank = value;
            }
        }

        public bool IsAccountFishy
        {
            get
            {
                return CheckFishyStatus();
            }
        }

        public IReadOnlyList<AbstractAccount> ListOfAccounts
        {
            get => _clientsAccounts;
        }

        public void AttachLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void Update(string message)
        {
            _logger.Log(message);
        }

        internal void AddAccount(AbstractAccount account)
        {
            _clientsAccounts.Add(account);
        }

        private bool CheckFishyStatus()
        {
            if (_address != null && _passport != null)
            {
                return false;
            }

            return true;
        }
    }
}