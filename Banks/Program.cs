using System;
using Banks.Entities;
using Banks.Entities.Command;
using Banks.Entities.DepositHandlers;
using Banks.Entities.Factories;
using Banks.Services;

namespace Banks
{
    internal static class Program
    {
        private static void WelcomeInfo()
        {
            Console.WriteLine("Welcome to CentralBank Menu:");
            Console.WriteLine("1 - Select Bank");
            Console.WriteLine("2 - Create Bank");
            Console.WriteLine("3 - Pay Interests");
            Console.WriteLine("4 - Use King Crimson");
            Console.WriteLine("5 - Open selected BankMenu");
            Console.WriteLine("QUIT - terminate");
        }

        private static IHandler CreationIHandler()
        {
            IHandler resultHandler = new UsualDepositHandler(-1, 0);
            IHandler lastHandler = resultHandler;
            while (true)
            {
                Console.WriteLine("Input uppersum");
                double upperSum = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Input interest rate");
                double interestRate = Convert.ToDouble(Console.ReadLine());
                lastHandler = lastHandler.SetNext(new UsualDepositHandler(upperSum, interestRate));
                Console.WriteLine("Stop creation? (y - stop)");
                if (Console.ReadLine() == "y")
                {
                    break;
                }
            }

            Console.WriteLine("Input last interest rate");
            lastHandler.SetNext(new LastDepositHandler(Convert.ToDouble(Console.ReadLine())));
            return resultHandler;
        }

        private static void WelcomeBankInfo()
        {
            Console.WriteLine("Welcome to Bank menu!");
            Console.WriteLine("1 - Select Client");
            Console.WriteLine("2 - Create Client");
            Console.WriteLine("3 - Set Command");
            Console.WriteLine("4 - Execute Command");
            Console.WriteLine("5 - Set AccountCreator");
            Console.WriteLine("6 - Execute AccountCreator");
            Console.WriteLine("7 - Go to Client's accountFrom");
            Console.WriteLine("8 - Select Client's accountTo");
            Console.WriteLine("QUIT - quit");
        }

        private static void BankMenu(Bank bank)
        {
            Client client = null;
            AbstractAccount accountFrom = null;
            AbstractAccount accountTo = null;
            ICommand command = null;
            while (true)
            {
                WelcomeBankInfo();
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                    {
                        Console.WriteLine("Input client's Id");
                        int id = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            client = bank.ListOfClients[id];
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Invalid client's Id!");
                            Console.WriteLine(e);
                            break;
                        }

                        Console.WriteLine("Selected client");
                        break;
                    }

                    case "2":
                    {
                        IClientBuilder clientBuilder = bank.ClientBuilder;
                        Console.WriteLine("Input Name");
                        clientBuilder.SetName(Console.ReadLine());
                        Console.WriteLine("Input Surname");
                        clientBuilder.SetSurname(Console.ReadLine());
                        Console.WriteLine("Do you wish to input address? (y - yes)");
                        if (Console.ReadLine() == "y")
                        {
                            Console.WriteLine("Input address");
                            clientBuilder.SetAddress(Console.ReadLine());
                        }

                        Console.WriteLine("Do you wish to input passport? (y - yes)");
                        if (Console.ReadLine() == "y")
                        {
                            Console.WriteLine("Input passport");
                            clientBuilder.SetPassport(Console.ReadLine());
                        }

                        Client c = clientBuilder.GetClient();
                        Console.WriteLine($"Created client, id = {c.Id}");
                        break;
                    }

                    case "3":
                    {
                        Console.WriteLine("Input type of command (transfer, refill, withdraw)");
                        string inp = Console.ReadLine();
                        Console.WriteLine("Input money");
                        double money = Convert.ToDouble(Console.ReadLine());

                        if (inp == "transfer")
                        {
                            command = new TransferCommand(accountFrom, accountTo, money);
                            Console.WriteLine("Set transfer command");
                        }
                        else if (inp == "refill")
                        {
                            command = new RefillCommand(accountTo, money);
                            Console.WriteLine("Set refill command");
                        }
                        else if (inp == "withdraw")
                        {
                            command = new WithdrawCommand(accountFrom, money);
                            Console.WriteLine("Set withdraw command");
                        }
                        else
                        {
                            Console.WriteLine("Incorrect input!");
                        }

                        break;
                    }

                    case "4":
                    {
                        command.Execute();
                        Console.WriteLine("Executed command");
                        break;
                    }

                    case "5":
                    {
                        Console.WriteLine("Input account type (debit, deposit, credit)");
                        string inp = Console.ReadLine();
                        if (inp == "credit")
                        {
                            bank.SetAccountCreator(new CreditAccountFactory());
                        }
                        else if (inp == "debit")
                        {
                            bank.SetAccountCreator(new DebitAccountFactory());
                        }
                        else if (inp == "deposit")
                        {
                            bank.SetAccountCreator(new DepositAccountFactory());
                        }
                        else
                        {
                            Console.WriteLine("Incorrect input!");
                        }

                        break;
                    }

                    case "6":
                    {
                        bank.CreateAccount(client);
                        Console.WriteLine($"Created account, attached to client Id = {client.Id}");
                        break;
                    }

                    case "7":
                    {
                        if (client == null)
                        {
                            Console.WriteLine("Client = null!");
                            break;
                        }

                        Console.WriteLine("Input client's account number");
                        int n = Convert.ToInt32(Console.ReadLine());
                        accountFrom = client.ListOfAccounts[n - 1];
                        break;
                    }

                    case "8":
                    {
                        if (client == null)
                        {
                            Console.WriteLine("Client = null!");
                            break;
                        }

                        Console.WriteLine("Input client's account number");
                        int n = Convert.ToInt32(Console.ReadLine());
                        accountTo = client.ListOfAccounts[n - 1];
                        break;
                    }
                }

                if (input == "QUIT")
                {
                    break;
                }
            }
        }

        private static void Main()
        {
            var centralBank = CentralBank.GetSingleton();
            Console.WriteLine("Welcome to OopLabs.Banks!");
            Bank bank = null;
            while (true)
            {
                WelcomeInfo();
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                    {
                        Console.WriteLine("Input bank ID");
                        int id = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            bank = centralBank.ListOfBanks[id];
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Incorrect ID!");
                            Console.WriteLine(e);
                            break;
                        }

                        break;
                    }

                    case "2":
                    {
                        IBankBuilder bankBuilder = centralBank.BankBuilder;
                        Console.WriteLine("Input debit interest rate");
                        bankBuilder.SetDebitInterestRate(Convert.ToDouble(Console.ReadLine()));
                        Console.WriteLine("Set credit commission");
                        bankBuilder.SetCreditCommission(Convert.ToDouble(Console.ReadLine()));
                        Console.WriteLine("Starting Deposit account IHandler creation...");
                        bankBuilder.SetDepositHandler(CreationIHandler());
                        Console.WriteLine("Input FishyLimit");
                        bankBuilder.SetFishyLimit(Convert.ToDouble(Console.ReadLine()));
                        Console.WriteLine("Input YearsLiveOfAccounts");
                        bankBuilder.SetYearsLiveOfAccount(Convert.ToInt32(Console.ReadLine()));
                        Bank b = bankBuilder.GetBank();
                        centralBank.AddBank(b);
                        Console.WriteLine($"Created bank, id = {b.Id}");
                        break;
                    }

                    case "3":
                    {
                        centralBank.PayInterests();
                        Console.WriteLine("Payed interests");
                        break;
                    }

                    case "4":
                    {
                        Console.WriteLine("Input count");
                        int count = Convert.ToInt32(Console.ReadLine());
                        centralBank.CalculateInterests(count);
                        break;
                    }

                    case "5":
                    {
                        if (bank == null)
                        {
                            Console.WriteLine("bank = null!");
                            break;
                        }

                        BankMenu(bank);
                        break;
                    }
                }

                if (input == "QUIT")
                {
                    break;
                }
            }
        }
    }
}
