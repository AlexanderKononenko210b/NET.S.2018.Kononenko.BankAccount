using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Interface.DbModels;

namespace DAL.Initializers
{
    /// <summary>
    /// Initialize database if not exist
    /// </summary>
    public class DatabaseInitializer : CreateDatabaseIfNotExists<AccountContext>
    {
        protected override void Seed(AccountContext context)
        {
            context.AccountTypes.Add(new AccountTypeDbModel {Type = "Base", CreatorId = 1 });
            context.AccountTypes.Add(new AccountTypeDbModel { Type = "Silver", CreatorId = 1 });
            context.AccountTypes.Add(new AccountTypeDbModel { Type = "Gold", CreatorId = 1 });
            context.AccountTypes.Add(new AccountTypeDbModel { Type = "Platinum", CreatorId = 1 });

            context.SaveChanges();

            context.Users.Add(new UserInfoDbModel
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Passport = "RT1234567",
                Email = "ivanov@gmail.com",
                CreatorId = 1
            });

            context.Users.Add(new UserInfoDbModel
            {
                FirstName = "Petr",
                LastName = "Petrov",
                Passport = "RT7654321",
                Email = "petrov@gmail.com",
                CreatorId = 1
            });

            context.Users.Add(new UserInfoDbModel
            {
                FirstName = "Konstantin",
                LastName = "Kotov",
                Passport = "MR1234567",
                Email = "kotov@gmail.com",
                CreatorId = 1
            });

            context.SaveChanges();

            context.Accounts.Add(new AccountDbModel
            {
                NumberOfAccount = "40512100790000000001",
                Balance = 500m,
                BenefitPoints = 550,
                IsClosed = false,
                UserId = 1,
                TypeId = 2,
                CreatorId = 1
            });

            context.Accounts.Add(new AccountDbModel
            {
                NumberOfAccount = "40512100790000000002",
                Balance = 100m,
                BenefitPoints = 120,
                IsClosed = false,
                UserId = 2,
                TypeId = 3,
                CreatorId = 1
            });

            context.Accounts.Add(new AccountDbModel
            {
                NumberOfAccount = "40512100790000000003",
                Balance = 1200m,
                BenefitPoints = 1560,
                IsClosed = false,
                UserId = 3,
                TypeId = 4,
                CreatorId = 1
            });

            context.Accounts.Add(new AccountDbModel
            {
                NumberOfAccount = "40512100790000000004",
                Balance = 620m,
                BenefitPoints = 620,
                IsClosed = false,
                UserId = 2,
                TypeId = 1,
                CreatorId = 1
            });

            context.SaveChanges();
        }
    }
}
