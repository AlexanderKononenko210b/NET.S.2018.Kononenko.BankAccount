﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Dto;
using BLL.Interface.Entities;
using DAL.Interface.Dto;
using BLL.Factories;
using BLL.Validators;

namespace BLL.Mappers
{
    /// <summary>
    /// Mapper from Domain model to DalDto model and receive
    /// from ViewDto to Domain model and receive
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

        /// <summary>
        /// Mapping source from type TSource to type Toutput
        /// </summary>
        /// <param name="source">instance source for mapping</param>
        /// <returns>instance result mapping</returns>
        public static TOutput MapView(TSourse source)
        {
            Check.NotNull((dynamic)source);

            return MapView((dynamic)source);
        }

        #endregion

        #region Map BLL model`s to UI model`s and resive

        /// <summary>
        /// Mapper from Account to AccountViewDto
        /// </summary>
        /// <param name="account">Account</param>
        /// <returns>instance type AccountDto after mapping</returns>
        private static AccountViewDto MapView(Account account)
        {
            var accountViewDto = new AccountViewDto();

            accountViewDto.Id = account.Id;
            accountViewDto.AccountType = account.AccountType.ToString();
            accountViewDto.Balance = account.Balance;
            accountViewDto.BenefitPoints = account.BenefitPoints;
            accountViewDto.IsClosed = account.IsClosed;
            accountViewDto.NumberOfAccount = account.NumberOfAccount;
            accountViewDto.UserId = account.UserId;

            return accountViewDto;
        }

        /// <summary>
        /// Mapper from AccountViewDto to Account
        /// </summary>
        /// <param name="accountViewDto">instance AccountDto</param>
        /// <returns></returns>
        private static Account MapView(AccountViewDto accountViewDto)
        {
            var account = AccountFactory.Create(accountViewDto);

            return account;
        }

        /// <summary>
        /// Mapper from userInfo to UserViewDto
        /// </summary>
        /// <param name="userInfo">information abount User</param>
        /// <returns>Dto model information about User</returns>
        private static UserViewDto MapView(UserInfo userInfo)
        {
            var userViewDto = new UserViewDto
            {
                Id = userInfo.Id,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                Passport = userInfo.Passport,
                Email = userInfo.Email
            };

            return userViewDto;
        }

        /// <summary>
        /// Mapper from UserViewDto to userInfo
        /// </summary>
        /// <param name="userViewDto">information abount User from UI</param>
        /// <returns>information about User</returns>
        private static UserInfo MapView(UserViewDto userViewDto)
        {
            var userInfo = new UserInfo
            {
                Id = userViewDto.Id,
                FirstName = userViewDto.FirstName,
                LastName = userViewDto.LastName,
                Passport = userViewDto.Passport,
                Email = userViewDto.Email
            };

            return userInfo;
        }

        #endregion

        #region Map Dall model`s to BLL model`s and resive

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
