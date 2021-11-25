using System.Collections.Generic;
using Banks.Builders;
using Banks.Services;

namespace Banks.Entities
{
    public class CentralBank
    {
        private static CentralBank _singleton;
        private IBankBuilder _bankBuilder;
        private List<Bank> _listOfBanks;

        private CentralBank()
        {
            _bankBuilder = new BankBuilder();
            _listOfBanks = new List<Bank>();
        }

        public IReadOnlyList<Bank> ListOfBanks { get => _listOfBanks; }
        public IBankBuilder BankBuilder { get => _bankBuilder; }

        public static CentralBank GetSingleton()
        {
            return _singleton ??= new CentralBank();
        }

        public void AddBank(Bank bank)
        {
            _listOfBanks.Add(bank);
        }

        public void PayInterests()
        {
            foreach (Bank bank in _listOfBanks)
            {
                bank.PayInterests();
            }
        }

        public void KINGU_CRIMSONU(int count)
        {
            for (int i = 0; i < count; i++)
            {
                foreach (Bank bank in _listOfBanks)
                {
                    bank.CalculateInterests(i);
                }
            }
        }
    }
}