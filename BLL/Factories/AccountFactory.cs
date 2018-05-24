using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Dto;
using BLL.Interface.Entities;
using BLL.Interface.Enum;
using BLL.Interface.Interfaces;
using BLL.Mappers;
using DAL.Interface.Dto;

namespace BLL.Factories
{
    /// <summary>
    /// Factory for create new instance typeof Account
    /// </summary>
    public static class AccountFactory
    {
        /// <summary>
        /// Create instance on AccountType information
        /// </summary>
        /// <param name="type">AccountType</param>
        /// <returns>new instance Account</returns>
        public static Account Create(AccountType type)
        {
            switch (type)
            {
                case AccountType.Base:
                    return new BaseAccount();
                case AccountType.Silver:
                    return new SilverAccount();
                case AccountType.Gold:
                    return new GoldAccount();
                case AccountType.Platinum:
                    return new PlatinumAccount();
                default:
                    throw new InvalidOperationException($"Unknown type {type} for create account");
            }
        }

        /// <summary>
        /// Create instance on AccountDto information
        /// </summary>
        /// <param name="accountDto">AccountDto instance</param>
        /// <returns>new instance type Account</returns>
        public static Account Create(AccountDto accountDto)
        {
            switch (accountDto.AccountType)
            {
                case AccountTypeDto.Base:
                    return new BaseAccount(accountDto);
                case AccountTypeDto.Silver:
                    return new SilverAccount(accountDto);
                case AccountTypeDto.Gold:
                    return new GoldAccount(accountDto);
                case AccountTypeDto.Platinum:
                    return new PlatinumAccount(accountDto);
                default:
                    throw new InvalidOperationException($"Unknown type {accountDto} for create account");
            }
        }

        /// <summary>
        /// Create instance on AccountDto information
        /// </summary>
        /// <param name="accountDto">AccountDto instance</param>
        /// <returns>new instance type Account</returns>
        public static Account Create(AccountViewDto accountViewDto)
        {
            AccountType type;

            try
            {
                type = (AccountType) Enum.Parse(typeof(AccountType), accountViewDto.AccountType);
            }
            catch (ArgumentException e)
            {
                throw new InvalidDataException(e.Message);
            }
            catch (OverflowException e)
            {
                throw new InvalidDataException(e.Message);
            }
            
            switch (type)
            {
                case AccountType.Base:
                    return new BaseAccount(accountViewDto);
                case AccountType.Silver:
                    return new SilverAccount(accountViewDto);
                case AccountType.Gold:
                    return new GoldAccount(accountViewDto);
                case AccountType.Platinum:
                    return new PlatinumAccount(accountViewDto);
                default:
                    throw new InvalidOperationException($"Unknown type {accountViewDto} for create account");
            }
        }

        /// <summary>
        /// Create instance on AccountType information, userInfo and IAccountNumberCreateService
        /// </summary>
        /// <param name="type">AccountType type</param>
        /// <param name="userId">id user</param>
        /// <param name="creator">IAccountNumberCreateService instance</param>
        /// <returns>new instance Account</returns>
        public static Account Create(AccountType type, int userId, IAccountNumberCreateService creator)
        {
            switch (type)
            {
                case AccountType.Base:
                    return new BaseAccount(userId, creator);
                case AccountType.Silver:
                    return new SilverAccount(userId, creator);
                case AccountType.Gold:
                    return new GoldAccount(userId, creator);
                case AccountType.Platinum:
                    return new PlatinumAccount(userId, creator);
                default:
                    throw new InvalidOperationException($"Unknown type {type} for create account");
            }
        }
    }
}
