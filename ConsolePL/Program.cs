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
            resolver.ConfigurateResolver();
        }

        static void Main(string[] args)
        {
            IAccountService accountService = resolver.Get<IAccountService>();
            IUserService userService = resolver.Get<IUserService>();
            IAccountNumberCreateService creator = resolver.Get<IAccountNumberCreateService>();

            Console.WriteLine("All in collection");
            Console.WriteLine();

            foreach (var item in accountService.GetAll())
            {
                Console.WriteLine(item);
            }

            try
            {
                //var person = userService.Create("Fedor", "Bondarchuk", "RT1234134", "bondarchuk@gmail.com");

                //accountService.OpenAccount(AccountType.Base, person.Id, creator);
                //accountService.OpenAccount(AccountType.Silver, person.Id, creator);
                //accountService.OpenAccount(AccountType.Gold, person.Id, creator);
                //accountService.OpenAccount(AccountType.Platinum, person.Id, creator);

                //Console.WriteLine("After add in collection");
                //Console.WriteLine();

                //foreach (var item in accountService.GetAll())
                //{
                //    Console.WriteLine(item);
                //}

                var account = accountService.GetByNumber("40512100790000000004");

                if (account == null)
                    Console.WriteLine("Account is absent in database");

                Console.WriteLine($"Befor withdraw Balance : {account.Balance}, BenefitPoints : {account.BenefitPoints}");

                accountService.WithDrawAccount(account, 100);

                account = accountService.GetByNumber("40512100790000000004");

                Console.WriteLine($"After withdraw 100 Balance : {account.Balance}, BenefitPoints : {account.BenefitPoints}");

                accountService.DepositAccount(account, 200);

                account = accountService.GetByNumber("40512100790000000004");

                Console.WriteLine($"After deposite 200 Balance : {account.Balance}, BenefitPoints : {account.BenefitPoints}");


                foreach (var item in accountService.GetAll().ToList())
                {
                    accountService.DepositAccount(item, 100);
                }

                Console.WriteLine("After deposit 100");

                foreach (var item in accountService.GetAll())
                {
                    Console.WriteLine(item);
                }

                foreach (var t in accountService.GetAll().ToList())
                {
                    accountService.WithDrawAccount(t, 100);
                }

                Console.WriteLine("After withdraw 100");

                foreach (var item in accountService.GetAll())
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
