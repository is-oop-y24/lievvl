using System;
using System.Collections.Generic;
using Banks.Entities;

namespace Banks.Services
{
    public abstract class AbstractAccount : ISubscriber
    {
        private float _money;
        private Client _client;
        private DateTime _expiredDate;
        private List<ICommand> _listOfOperations;
        private float _interestMoney;

        protected AbstractAccount(Client client, DateTime expiredDate)
        {
            _client = client;
            _expiredDate = expiredDate;
            _money = 0;
            _interestMoney = 0;
            _listOfOperations = new List<ICommand>();
        }

        public DateTime ExpiredDate { get => _expiredDate; }
        public float Money { get => _money; }
        public Client Client { get => _client; }

        public IReadOnlyList<ICommand> Operations { get => _listOfOperations; }

        public float InterestMoney
        {
            get => _interestMoney;
            internal set
            {
                _interestMoney = value;
            }
        }

        public abstract void Update(string message);

        internal void AddCommand(ICommand command)
        {
            _listOfOperations.Add(command);
        }

        internal void RemoveCommand(ICommand command)
        {
            _listOfOperations.Remove(command);
        }

        internal void PayInterest()
        {
            _money += _interestMoney;
            _interestMoney = 0;
        }

        internal abstract void CalculateInterest();

        internal abstract void AddMoney(float money);

        internal abstract void DecreaseMoney(float money);
        protected void SetMoney(float money)
        {
            _money = money;
        }
    }
}