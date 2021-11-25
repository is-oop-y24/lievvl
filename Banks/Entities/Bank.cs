using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Banks.Builders;
using Banks.Services;
using Banks.Tools;

namespace Banks.Entities
{
    public class Bank : IPublisher
    {
        private int _id;
        private List<Client> _listOfClients;
        private List<ISubscriber> _listOfSubscribedClients;
        private IClientBuilder _clientBuilder;
        private ICreator _currentCreator;
        private float _debitInterestRate;
        private float _depositInterestRate;
        private float _creditCommission;
        private float _fishyLimit;
        private int _yearsLiveOfAccounts;
        private Timer _timerCalculateInterests;
        private IHandler _depositHandler;
        private ICommand _currentCommand;

        internal Bank(int id)
        {
            _timerCalculateInterests = new Timer(new TimerCallback(CalculateInterests), 1, 0, 86400000);
            _clientBuilder = new ClientBuilder(this);
            _id = id;
            _listOfClients = new List<Client>();
            _listOfSubscribedClients = new List<ISubscriber>();
        }

        public int Id { get => _id; }

        public IClientBuilder ClientBuilder
        {
            get => _clientBuilder;
        }

        public float DebitInterestRate
        {
            get => _debitInterestRate;
            internal set
            {
                _debitInterestRate = value;
            }
        }

        public float DepositInterestRate
        {
            get => _depositInterestRate;
            internal set
            {
                _depositInterestRate = value;
            }
        }

        public IHandler DepositHandler
        {
            get => _depositHandler;
            internal set
            {
                _depositHandler = value;
            }
        }

        public float CreditCommission
        {
            get => _creditCommission;
            internal set
            {
                _creditCommission = value;
            }
        }

        public float FishyLimit
        {
            get => _fishyLimit;
            internal set
            {
                _fishyLimit = value;
            }
        }

        public int YearsLiveOfAccounts
        {
            get => _yearsLiveOfAccounts;
            internal set
            {
                _yearsLiveOfAccounts = value;
            }
        }

        public void Subscribe(ISubscriber subscriber)
        {
            _listOfSubscribedClients.Add(subscriber);
        }

        public void Unsubscribe(ISubscriber subscriber)
        {
            if (subscriber == null)
            {
                throw new BankException("ISubscriber is null!");
            }

            _listOfSubscribedClients.Remove(subscriber);
        }

        public void Inform(string message)
        {
            foreach (ISubscriber subscriber in _listOfSubscribedClients)
            {
                subscriber.Update(message);
            }
        }

        public void SetAccountCreator(ICreator accountCreator)
        {
            accountCreator.AttachBank(this);
            _currentCreator = accountCreator;
        }

        public void CreateAccount(Client client)
        {
            bool isClientAtBank = _listOfClients.Any(c => c == client);
            if (!isClientAtBank)
            {
                throw new BankException("Client not at Bank!");
            }

            client.AddAccount(_currentCreator.CreateAccount(client));
        }

        public void SetCommand(ICommand command)
        {
            _currentCommand = command;
        }

        public void ExecuteCommand()
        {
            _currentCommand?.Execute();
        }

        internal void AddClient(Client client)
        {
            _listOfClients.Add(client);
            _listOfSubscribedClients.Add(client);
        }

        internal void CalculateInterests(object obj)
        {
            foreach (Client client in _listOfClients)
            {
                foreach (AbstractAccount account in client.ListOfAccounts)
                {
                    account.CalculateInterest();
                }
            }
        }

        internal void PayInterests()
        {
            foreach (Client client in _listOfClients)
            {
                foreach (AbstractAccount account in client.ListOfAccounts)
                {
                    account.PayInterest();
                }
            }
        }
    }
}