using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DbModels;
using DAL.Interface.Dto;
using DAL.Validators;

namespace DAL.Mappers
{
    /// <summary>
    /// Mapper from Dto model to DbModel model in Dal level
    /// </summary>
    public static class Mapper<TSource,TOutput>
    {
        #region Public Api

        /// <summary>
        /// Mapping instance from type TSource to type Toutput 
        /// using exist instance type TOutput
        /// </summary>
        /// <param name="first">instance for mapping</param>
        /// <param name="second">instance source for mapping</param>
        /// <returns>instance result mapping</returns>
        public static TOutput MapToSelf(TOutput first, TSource second)
        {
            Check.NotNull((dynamic)first);

            Check.NotNull((dynamic)second);

            return MapToSelf((dynamic)first, (dynamic)second);
        }

        /// <summary>
        /// Mapping source from type TSource to type Toutput
        /// </summary>
        /// <param name="source">instance source for mapping</param>
        /// <returns>instance result mapping</returns>
        public static TOutput Map(TSource source)
        {
            Check.NotNull((dynamic)source);

            return Map((dynamic)source);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Mapper from AccountDbModel to AccontDto
        /// </summary>
        /// <param name="account">instance type AccountDbModel</param>
        /// <returns>instance type AccountDto</returns>
        private static AccountDto Map(AccountDbModel account)
        {
            Check.NotNull(account);

            var accountDto = new AccountDto
            {
                Id = account.Id,
                NumberOfAccount = account.NumberOfAccount,
                Balance = account.Balance,
                BenefitPoints = account.BenefitPoints,
                IsClosed = account.IsClosed,
                UserId = account.UserId,
                AccountType = (AccountTypeDto)account.TypeId
            };

            return accountDto;

        }

        /// <summary>
        /// Mapper from AccountDto to AccontDbModel
        /// </summary>
        /// <param name="accountDto">instance type AccountDto</param>
        /// <returns>instance type AccountDbModel</returns>
        private static AccountDbModel Map(AccountDto accountDto)
        {
            Check.NotNull(accountDto);

            var accountDbModel = new AccountDbModel()
            {
                Id = accountDto.Id,
                NumberOfAccount = accountDto.NumberOfAccount,
                Balance = accountDto.Balance,
                BenefitPoints = accountDto.BenefitPoints,
                IsClosed = accountDto.IsClosed,
                UserId = accountDto.UserId,
                TypeId = (int)accountDto.AccountType
            };

            return accountDbModel;
        }

        /// <summary>
        /// Mapper from UserInfoDbModel to UserInfoDto
        /// </summary>
        /// <param name="userInfo">instance type UserInfoDbModel</param>
        /// <returns>instance type UserInfoDto</returns>
        private static UserInfoDto Map(UserInfoDbModel userInfo)
        {
            Check.NotNull(userInfo);

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
        /// Mapper from UserInfoDto to UserInfoDbModel
        /// </summary>
        /// <param name="userInfoDto">instance type UserInfoDto</param>
        /// <returns>instance type UserInfoDbModel</returns>
        private static UserInfoDbModel Map(UserInfoDto userInfoDto)
        {
            Check.NotNull(userInfoDto);

            var userInfoDbModel = new UserInfoDbModel
            {
                Id = userInfoDto.Id,
                FirstName = userInfoDto.FirstName,
                LastName = userInfoDto.LastName,
                Passport = userInfoDto.Passport,
                Email = userInfoDto.Email
            };

            return userInfoDbModel;
        }

        /// <summary>
        /// Mapper from AccountDbModel to AccountDbModel using AccountDto
        /// </summary>
        /// <param name="accountDbModel">instance type AccountDbModel</param>
        /// <param name="accountDto">instance type AccountDto</param>
        /// <returns>AccountDbModel after mapping</returns>
        private static AccountDbModel MapToSelf(AccountDbModel accountDbModel,
            AccountDto accountDto)
        {
            Check.NotNull(accountDbModel);

            Check.NotNull(accountDto);

            accountDbModel.Balance = accountDto.Balance;
            accountDbModel.BenefitPoints = accountDto.BenefitPoints;
            accountDbModel.IsClosed = accountDto.IsClosed;
            accountDbModel.NumberOfAccount = accountDto.NumberOfAccount;
            accountDbModel.TypeId = (int)accountDto.AccountType;
            accountDbModel.UserId = accountDto.UserId;

            return accountDbModel;
        }

        #endregion

    }
}
