using System.Collections.Generic;
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
        private bool _isAccountFishy;
        private List<AbstractAccount> _clientsAccounts;

        public Client(int id)
        {
            _id = id;
            _name = null;
            _surname = null;
            _address = null;
            _passport = null;
            _isAccountFishy = true;
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
            get => _isAccountFishy;
            internal set
            {
                _isAccountFishy = value;
            }
        }

        public IReadOnlyList<AbstractAccount> ListOfAccounts
        {
            get => _clientsAccounts;
        }

        public void Update(string message)
        {
            throw new System.NotImplementedException();
        }

        internal void AddAccount(AbstractAccount account)
        {
            _clientsAccounts.Add(account);
        }

        private void CheckFishyStatus()
        {
            if (_isAccountFishy)
            {
                if (_address != null && _passport != null)
                {
                    _isAccountFishy = false;
                }
            }
        }
    }
}