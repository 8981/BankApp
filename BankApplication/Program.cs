using System;
using BankLibrary;

namespace BankApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank<Account> bank = new Bank<Account>("SolutionBank");
            bool alive = true;
            while(alive)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkBlue;//Otput komands list blue color
                Console.WriteLine("1. Open account \t 2. Output monet \t 3. Add to account");
                Console.WriteLine("4. Clouse account \t 5. lose the day \t 6. Exit from programm"); 
                Console.WriteLine("Enter the item number: ");
                Console.ForegroundColor = color;
                try
                { 
                    int command = Convert.ToInt32(Console.ReadLine()); 

                    switch (command)
                    {
                        case 1:
                            OpenAccount(bank);
                            break;
                        case 2:
                            Withdraw(bank);
                            break;
                        case 3:
                            Put(bank);
                            break;
                        case 4:
                            CloseAccount(bank);
                            break;
                        case 5:
                            break;
                        case 6:
                            alive = false;
                            continue;
                    }
                    bank.CalculatePercentage();
                }
                catch (Exception ex)
                {
                    //output red color mesage about error
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }
            }
        }

        private static void OpenAccount(Bank<Account> bank)
        {
            Console.WriteLine("Enter summ for creat account: ");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Choise type of account: 1. On demand 2. Deposit");
            AccountType accountType;

            int type = Convert.ToInt32(Console.ReadLine());

            if (type == 2) 
                accountType = AccountType.Deposit;
            else
                accountType = AccountType.Ordinary;

            bank.Open(accountType,
                sum,
                AddSumHandler,  // обработчик добавления средств на счет
                WithdrawSumHandler, // обработчик вывода средств
                (o, e) => Console.WriteLine(e.Message), // обработчик начислений процентов в виде лямбда-выражения
                CloseAccountHandler, // обработчик закрытия счета
                OpenAccountHandler);// обработчик открытия счета
        }

        private static void Withdraw(Bank<Account> bank)
        {
            Console.WriteLine("Enter summ for output monet: ");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter account Id: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Withdraw(sum, id);
        }

        private static void Put(Bank<Account> bank)
        {
            Console.WriteLine("Enter summ for add money to account: ");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter account Id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            bank.Put(sum, id);
        }

        private static void CloseAccount(Bank<Account> bank)
        {
            Console.WriteLine("Enter account id for close: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Close(id);
        }

        // event handlers of the Account class
        // account opening handler
        private static void OpenAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        //handler adding money to account
        private static void AddSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        //handler output money
        private static void WithdrawSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
            if (e.Sum > 0)
                Console.WriteLine("Let's go spend money");
        }

        //handler close account
        private static void CloseAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
