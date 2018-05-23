using System;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Enum;
using BLL.Interface.Interfaces;
using DependencyResolver;
using Ninject;

namespace ConsolePL
{
    class Program
    {
        private static readonly IKernel resolver;

        static Program()
        {
            resolver = new StandardKernel();
            resolver.ConfigurateResolverConsole();
        }

        static void Main(string[] args)
        {
            IAccountService accountService = resolver.Get<IAccountService>();
            IUserService userService = resolver.Get<IUserService>();
            IAccountNumberCreateService creator = resolver.Get<IAccountNumberCreateService>();

            Console.WriteLine("All accounts in database");
            Console.WriteLine();

            foreach (var item in accountService.GetAll())
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();

            try
            {
                #region Test add new account

                var person = userService.Create("Fedor", "Bondarchuk", "RT1234134", "bondarchuk@gmail.com");

                accountService.OpenAccount(AccountType.Base, person.Id, creator);
                accountService.OpenAccount(AccountType.Silver, person.Id, creator);
                accountService.OpenAccount(AccountType.Gold, person.Id, creator);
                accountService.OpenAccount(AccountType.Platinum, person.Id, creator);

                var person2 = userService.Create("Fedor", "Volkov", "RT1234134", "volkov@gmail.com");

                accountService.OpenAccount(AccountType.Base, person2.Id, creator);
                accountService.OpenAccount(AccountType.Silver, person2.Id, creator);
                accountService.OpenAccount(AccountType.Gold, person2.Id, creator);
                accountService.OpenAccount(AccountType.Platinum, person2.Id, creator);

                Console.WriteLine("After add in collection");
                Console.WriteLine();

                foreach (var item in accountService.GetAll())
                {
                    Console.WriteLine(item);
                }

                #endregion

                #region Test deposit and withdraw

                var account = accountService.GetByNumber("40512100790000000004");

                Console.WriteLine($"Befor withdraw Balance : {account.Balance}, BenefitPoints : {account.BenefitPoints}");

                accountService.WithDrawAccount(account, 100);

                account = accountService.GetByNumber("40512100790000000004");

                Console.WriteLine($"After withdraw 100 Balance : {account.Balance}, BenefitPoints : {account.BenefitPoints}");

                accountService.DepositAccount(account, 200);

                account = accountService.GetByNumber("40512100790000000004");

                Console.WriteLine($"After deposite 200 Balance : {account.Balance}, BenefitPoints : {account.BenefitPoints}");

                #endregion

                #region Test transfer

                var accountWithDrawTransfer = accountService.GetByNumber("40512100790000000003");

                var accountDepositTransfer = accountService.GetByNumber("40512100790000000004");

                Console.WriteLine($"Before transfer balance account for withdraw {accountWithDrawTransfer.Balance}" +
                                  $"account for deposit {accountDepositTransfer.Balance}");

                var resultTransfer = accountService.Transfer(accountWithDrawTransfer, accountDepositTransfer, 50);

                Console.WriteLine($"After transfer balance account for withdraw {resultTransfer.Item1.Balance}" +
                                  $"account for deposit {resultTransfer.Item2.Balance}");

                #endregion

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
