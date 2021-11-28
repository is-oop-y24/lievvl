using Banks.Entities;
using Banks.Services;
using Banks.Tools;

namespace Banks.Builders
{
    public class ClientBuilder : IClientBuilder
    {
        private int _id;
        private Client _client;
        private Bank _bank;

        internal ClientBuilder(Bank bank)
        {
            _id = 0;
            _bank = bank;
            Reset();
        }

        public void Reset()
        {
            _client = new Client(_id);
            _client.AttachedBank = _bank;
        }

        public void SetName(string name)
        {
            _client.Name = name;
        }

        public void SetSurname(string surname)
        {
            _client.Surname = surname;
        }

        public void SetAddress(string address)
        {
            _client.Address = address;
        }

        public void SetPassport(string passport)
        {
            _client.Passport = passport;
        }

        public Client GetClient()
        {
            if (_client.Name == null || _client.Surname == null)
            {
                throw new BankException("ClientBuilder hasn't setted Name and Surname!");
            }

            Client result = _client;

            _bank.AddClient(result);

            _id++;
            Reset();
            return result;
        }
    }
}