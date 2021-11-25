using Banks.Entities;

namespace Banks.Services
{
    public interface IClientBuilder
    {
        void Reset();

        void SetName(string name);
        void SetSurname(string surname);
        void SetAddress(string address);
        void SetPassport(string passport);

        public Client GetClient();
    }
}