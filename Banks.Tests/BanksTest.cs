using Banks.Entities;
using Banks.Entities.Command;
using Banks.Entities.DepositHandlers;
using Banks.Entities.Factories;
using Banks.Services;
using Banks.Tools;
using NUnit.Framework;

namespace Banks.Tests
{
    public class Tests
    {
        private CentralBank centralBank;
        private Bank bank1;
        private Client client1;
        private Client client2;
        private AbstractAccount account1;
        private AbstractAccount account2;
        private AbstractAccount account3;
        private ICommand command1;
        private ICommand command2;
        private ICommand command3;
        private ICommand command4;

        [SetUp]
        public void Setup()
        {
            centralBank = CentralBank.GetSingleton();
            IBankBuilder bankBuilder = centralBank.BankBuilder;
            bankBuilder.SetCreditCommission(1000);
            bankBuilder.SetDebitInterestRate(0.01);
            bankBuilder.SetYearsLiveOfAccount(2);
            IHandler depositHandler = new UsualDepositHandler(1001, 0.01);
            depositHandler.SetNext(new LastDepositHandler(0.02));
            bankBuilder.SetDepositHandler(depositHandler);
            bankBuilder.SetFishyLimit(500);
            bank1 = bankBuilder.GetBank();
            centralBank.AddBank(bank1);

            IClientBuilder clientBuilder = bank1.ClientBuilder;
            clientBuilder.SetName("Fishy");
            clientBuilder.SetSurname("Fishy");
            client1 = clientBuilder.GetClient();
            clientBuilder.SetName("Investor");
            clientBuilder.SetSurname("Investor");
            clientBuilder.SetAddress("l");
            clientBuilder.SetPassport("0000 000000");
            client2 = clientBuilder.GetClient();

            bank1.SetAccountCreator(new DebitAccountFactory());
            account1 = bank1.CreateAccount(client1);
            bank1.SetAccountCreator(new DepositAccountFactory());
            account2 = bank1.CreateAccount(client2);
            bank1.SetAccountCreator(new CreditAccountFactory());
            account3 = bank1.CreateAccount(client2);

            command1 = new RefillCommand(account1, 400);
            command2 = new RefillCommand(account1, 400);
            command3 = new RefillCommand(account2, 1000);
            command4 = new RefillCommand(account3, 2000);
            command1.Execute();
            command2.Execute();
            command3.Execute();
            command4.Execute();
        }

        [Test]
        public void TryToTransactionWithFishyAccount_CatchException()
        {
            Assert.Catch<BankException>(() =>
                {
                    new TransferCommand(account1, account3, 600);
                }
            );
        }

        [Test]
        public void SetPassportAddress_TryToTransferWithAccount()
        {
            client1.Address = "l";
            client1.Passport = "1111 111111";
            ICommand command = new TransferCommand(account1, account3, 600);
            command.Execute();
            Assert.AreEqual(200, account1.Money);
            Assert.AreEqual(2600, account3.Money);
        }

        [Test]
        public void TryToWithdrawDeposit_CatchException()
        {
            Assert.Catch<BankException>(() =>
            {
                var command = new WithdrawCommand(account2, 1);
                command.Execute();
            }
            );
        }

        [Test]
        public void DenyCommand1_DenyCommand2()
        {
            command1.DenyCommand();
            command2.DenyCommand();
            Assert.AreEqual(0, account1.Money);
        }

        [Test]
        public void DenyCommand1Twice_CatchException()
        {
            Assert.Catch<BankException>(() =>
                {
                    command1.DenyCommand();
                    command1.DenyCommand();
                }
            );
        }

        [Test]
        public void UseKingCrimson_PayInterest_CheckDepositAccount_UseKingCrimson_CheckDepositAccount()
        {
            centralBank.CalculateInterests(1);
            centralBank.PayInterests();
            Assert.AreEqual(1010, account2.Money);
            
            centralBank.CalculateInterests(1);
            centralBank.PayInterests();
            Assert.AreEqual(1030.2, account2.Money);
        }

        [Test]
        public void Withdraw3000FromCredit_UseKingCrimson_Check()
        {
            ICommand command = new WithdrawCommand(account3, 3000);
            command.Execute();

            centralBank.CalculateInterests(1);
            centralBank.PayInterests();

            Assert.AreEqual(-2000, account3.Money);
        }
    }
}