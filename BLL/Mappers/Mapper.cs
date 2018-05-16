using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using DAL.Interface.Dto;
using BLL.Factories;
using BLL.Validators;

namespace BLL.Mappers
{
    /// <summary>
    /// Mapper from Domain model to Dto model and receive
    /// </summary>
    public static class Mapper<TSourse, TOutput>
    {
        #region Public Api

        /// <summary>
        /// Mapping source from type TSource to type Toutput
        /// </summary>
        /// <param name="source">instance source for mapping</param>
        /// <returns>instance result mapping</returns>
        public static TOutput Map(TSourse source)
        {
            Check.NotNull((dynamic)source);

            return Map((dynamic)source);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Mapper from Account to AccountDto
        /// </summary>
        /// <param name="account">Account</param>
        /// <returns>instance type AccountDto after mapping</returns>
        private static AccountDto Map(Account account)
        {
            var accountDto = new AccountDto();

            accountDto.Id = account.Id;
            accountDto.AccountType = (AccountTypeDto)account.AccountType;
            accountDto.Balance = account.Balance;
            accountDto.BenefitPoints = account.BenefitPoints;
            accountDto.IsClosed = account.IsClosed;
            accountDto.NumberOfAccount = account.NumberOfAccount;
            accountDto.UserId = account.UserId;

            return accountDto;
        }

        /// <summary>
        /// Mapper from AccountDto to Account
        /// </summary>
        /// <param name="accountDto">instance AccountDto</param>
        /// <returns></returns>
        private static Account Map(AccountDto accountDto)
        {
            var account = AccountFactory.Create(accountDto);

            return account;
        }

        /// <summary>
        /// Mapper from userInfo to UserInfoDto
        /// </summary>
        /// <param name="userInfo">information abount User</param>
        /// <returns>Dto model information about User</returns>
        private static UserInfoDto Map(UserInfo userInfo)
        {
            var userInfoDto = new UserInfoDto
            {
                Id = userInfo.Id,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                Passport = userInfo.Passport,
                Email = userInfo.Email
            };

            return userInfoDto;
        }

        /// <summary>
        /// Mapper from UserInfoDto to userInfo
        /// </summary>
        /// <param name="userInfoDto">information abount User</param>
        /// <returns>user information about User</returns>
        private static UserInfo Map(UserInfoDto userInfoDto)
        {
            var userInfo = new UserInfo
            {
                Id = userInfoDto.Id,
                FirstName = userInfoDto.FirstName,
                LastName = userInfoDto.LastName,
                Passport = userInfoDto.Passport,
                Email = userInfoDto.Email
            };

            return userInfo;
        }

        #endregion

    }
}
