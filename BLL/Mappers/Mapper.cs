using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using DAL.Interface.DTO;
using BLL.Factories;
using BLL.ServiceImplementation;

namespace BLL.Mappers
{
    /// <summary>
    /// Mapper from Domain model to Dto model and receive
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// Mapper from Account to AccountDto
        /// </summary>
        /// <param name="account">Account</param>
        /// <returns></returns>
        public static AccountDto AccountToAccountDto(this Account account)
        {
            var accountDto = new AccountDto();

            accountDto.AccountType = (AccountTypeDto)account.AccountType;
            accountDto.Balance = account.Balance;
            accountDto.BenefitPoints = account.BenefitPoints;
            accountDto.IsClosed = account.IsClosed;
            accountDto.NumberOfAccount = account.NumberOfAccount;
            accountDto.PersonalInfo.FirstName = account.PersonalInfo.FirstName;
            accountDto.PersonalInfo.LastName = account.PersonalInfo.LastName;
            accountDto.PersonalInfo.Email = account.PersonalInfo.Email;
            accountDto.PersonalInfo.Passport = account.PersonalInfo.Passport;

            return accountDto;
        }

        /// <summary>
        /// Mapper from AccountDto to Account
        /// </summary>
        /// <param name="accountDto">instance AccountDto</param>
        /// <returns></returns>
        public static Account AccountDtoToAccount(this AccountDto accountDto)
        {
            var account = AccountFactory.Create(accountDto);

            return account;
        }
    }
}
