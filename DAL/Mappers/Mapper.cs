using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;

namespace DAL.Mappers
{
    /// <summary>
    /// Mapper from Domain model to Dto model and receive
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// Mapper from Account to AccountModel
        /// </summary>
        /// <param name="account">AccountDto</param>
        /// <returns>AccountModel</returns>
        public static Account AccountDtoToAccontModel(this AccountDto account)
        {
            var accountModel = new Account
            {
                Balance = account.Balance,
                BenefitPoints = account.BenefitPoints,
                IsClosed = account.IsClosed,
                NumberOfAccount = account.NumberOfAccount,
                User = new User()
                {
                    FirstName = account.PersonalInfo.FirstName,
                    LastName = account.PersonalInfo.LastName,
                    Email = account.PersonalInfo.Email,
                    Passport = account.PersonalInfo.Passport
                },
                Type = new Type() { AccountType = (int)account.AccountType}
            };

            return accountModel;
        }

        /// <summary>
        /// Mapper from AccountModel to AccountDto
        /// </summary>
        /// <param name="accountModel">instance accountModel</param>
        /// <returns>AccountDto</returns>
        public static AccountDto AccontModelToAccountDto(this Account accountModel)
        {
            var accountDto = new AccountDto
            {
                Balance = accountModel.Balance,
                BenefitPoints = accountModel.BenefitPoints,
                IsClosed = accountModel.IsClosed,
                NumberOfAccount = accountModel.NumberOfAccount,
                PersonalInfo = new PersonalInfoDto
                {
                    FirstName = accountModel.User.FirstName,
                    LastName = accountModel.User.LastName,
                    Email = accountModel.User.LastName,
                    Passport = accountModel.User.Passport
                },
                AccountType = (AccountTypeDto)accountModel.Type.AccountType
            };

            return accountDto;
        }

        /// <summary>
        /// Mapper from AccountModel to AccountModel
        /// </summary>
        /// <param name="account">AccountModel</param>
        /// <returns>AccountModel</returns>
        public static Account AccountModelToAccontModel(this Account account, Account otherAccount)
        {
            account.Balance = otherAccount.Balance;
            account.BenefitPoints = otherAccount.BenefitPoints;
            account.IsClosed = otherAccount.IsClosed;
            account.NumberOfAccount = otherAccount.NumberOfAccount;
            account.User.FirstName = otherAccount.User.FirstName;
            account.User.LastName = otherAccount.User.LastName;
            account.User.Email = otherAccount.User.LastName;
            account.User.Passport = otherAccount.User.Passport;
            account.Type.AccountType = otherAccount.Type.AccountType;

            return account;
        }
    }
}
