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
            IAccountService service = resolver.Get<IAccountService>();
            IAccountNumberCreateService creator = resolver.Get<IAccountNumberCreateService>();
            IPersonalInfoService personal = resolver.Get<IPersonalInfoService>();
            IVerifyPersonalInfo<IStringValidator> validator = resolver.Get<IVerifyPersonalInfo<IStringValidator>>();
            
            Console.WriteLine("All in collection");
            Console.WriteLine();

            foreach (var item in service.GetAll())
            {
                Console.WriteLine(item);
            }

            try
            {
                //var person = personal.Create("Fedor", "Bondarchuk", "RT1234136", "bondarchuk@gmail.com", validator);

                //if (person.Item1 == null)
                //    Console.WriteLine(person.Item2);

                //service.OpenAccount(AccountType.Base, person.Item1, creator);

                //Console.WriteLine("After add in collection");
                //Console.WriteLine();

                //foreach (var item in service.GetAll())
                //{
                //    Console.WriteLine(item);
                //}

                var account = service.GetByNumber("40512100790000000004");

                if (account == null)
                    Console.WriteLine("Account is absent in database");

                Console.WriteLine($"Befor withdraw Balance : {account.Balance}, BenefitPoints : {account.BenefitPoints}");

                service.WithDrawAccount(account, 100);

                account = service.GetByNumber("40512100790000000004");

                Console.WriteLine($"After withdraw 100 Balance : {account.Balance}, BenefitPoints : {account.BenefitPoints}");

                service.DepositAccount(account, 200);

                account = service.GetByNumber("40512100790000000004");

                Console.WriteLine($"After deposite 200 Balance : {account.Balance}, BenefitPoints : {account.BenefitPoints}");


                foreach (var item in service.GetAll())
                {
                    service.DepositAccount(item, 100);
                }

                Console.WriteLine("After deposit 100");

                foreach (var item in service.GetAll())
                {
                    Console.WriteLine(item);
                }

                foreach (var t in service.GetAll())
                {
                    service.WithDrawAccount(t, 100);
                }

                Console.WriteLine("After withdraw 100");

                foreach (var item in service.GetAll())
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
