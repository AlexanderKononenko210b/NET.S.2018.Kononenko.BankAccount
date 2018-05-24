using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using BLL.Interface.Dto;
using BLL.Interface.Entities;
using MvcPL.Models;
using MvcPL.Validators;

namespace MvcPL.Mapper
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
        public static TOutput MapView(TSourse source)
        {
            Check.NotNull((dynamic)source);

            return MapView((dynamic)source);
        }

        /// <summary>
        /// Mapping source from type IEnumerable<TSource></TSource> to type IEnumerable<Toutput></Toutput>
        /// </summary>
        /// <param name="source">instance source for mapping</param>
        /// <returns>instance result mapping</returns>
        public static IEnumerable<TOutput> MapView(IEnumerable<TSourse> source)
        {
            Check.NotNull((dynamic)source);

            return MapView((dynamic)source);
        }

        #endregion

        /// <summary>
        /// Mapper from IEnumerable<AccountViewDto></AccountViewDto> to IEnumerable<AccountViewModel></AccountViewModel>
        /// </summary>
        /// <param name="accountViewDto">instance AccountDto</param>
        /// <returns></returns>
        private static IEnumerable<AccountViewModel> MapView(IEnumerable<AccountViewDto> accountViewDto)
        {
            Check.NotNull(accountViewDto);

            foreach (var item in accountViewDto)
            {
                var accountViewModel = new AccountViewModel
                {
                    Id = item.Id,
                    AccountType = item.AccountType,
                    Balance = item.Balance,
                    BenefitPoints = item.BenefitPoints,
                    IsClosed = item.IsClosed,
                    NumberOfAccount = item.NumberOfAccount,
                    UserId = item.UserId
                };

                yield return accountViewModel;
            }
        }

        #region Private methods

        /// <summary>
        /// Mapper from Account to AccountViewDto
        /// </summary>
        /// <param name="account">Account</param>
        /// <returns>instance type AccountDto after mapping</returns>
        private static AccountViewDto MapView(AccountViewModel account)
        {
            var accountViewDto = new AccountViewDto
            {
                Id = account.Id,
                AccountType = account.AccountType,
                Balance = account.Balance,
                BenefitPoints = account.BenefitPoints,
                IsClosed = account.IsClosed,
                NumberOfAccount = account.NumberOfAccount,
                UserId = account.UserId
            };

            return accountViewDto;
        }

        /// <summary>
        /// Mapper from AccountViewDto to Account
        /// </summary>
        /// <param name="accountViewDto">instance AccountDto</param>
        /// <returns></returns>
        private static AccountViewModel MapView(AccountViewDto accountViewDto)
        {
            var accountViewModel = new AccountViewModel
            {
                Id = accountViewDto.Id,
                AccountType = accountViewDto.AccountType,
                Balance = accountViewDto.Balance,
                BenefitPoints = accountViewDto.BenefitPoints,
                IsClosed = accountViewDto.IsClosed,
                NumberOfAccount = accountViewDto.NumberOfAccount,
                UserId = accountViewDto.UserId
            };

            return accountViewModel;
        }

        /// <summary>
        /// Mapper from UserViewModel to UserViewDto
        /// </summary>
        /// <param name="userViewModel">information about User in UI</param>
        /// <returns>ViewDto model information about User</returns>
        private static UserViewDto MapView(UserViewModel userViewModel)
        {
            var userViewDto = new UserViewDto
            {
                Id = userViewModel.Id,
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                Passport = userViewModel.Passport,
                Email = userViewModel.Email
            };

            return userViewDto;
        }

        /// <summary>
        /// Mapper from UserViewDto to userViewModel
        /// </summary>
        /// <param name="userViewDto">information abount User from BLL</param>
        /// <returns>information about User</returns>
        private static UserViewModel MapView(UserViewDto userViewDto)
        {
            var userViewModel = new UserViewModel
            {
                Id = userViewDto.Id,
                FirstName = userViewDto.FirstName,
                LastName = userViewDto.LastName,
                Passport = userViewDto.Passport,
                Email = userViewDto.Email
            };

            return userViewModel;
        }

        #endregion

    }
}