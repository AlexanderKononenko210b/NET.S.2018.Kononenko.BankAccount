using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interface.Enum;
using BLL.Interface.Interfaces;
using BLL.Mappers;
using DAL.Interface.DTO;

namespace BLL.Factories
{
    /// <summary>
    /// Factory for create new instance typeof Account
    /// </summary>
    public static class AccountFactory
    {
        /// <summary>
        /// Create instance on AccountTypeDto information
        /// </summary>
        /// <param name="type">AccountTypeDto</param>
        /// <returns>new instance AccountTypeDto</returns>
        public static Account Create(AccountTypeDto type)
        {
            switch (type)
            {
                case AccountTypeDto.Base:
                    return new BaseAccount();
                case AccountTypeDto.Silver:
                    return new SilverAccount();
                case AccountTypeDto.Gold:
                    return new GoldAccount();
                case AccountTypeDto.Platinum:
                    return new PlatinumAccount();
                default:
                    throw new InvalidOperationException($"Unknown type {nameof(type)} for create");
            }
        }

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
                    throw new InvalidOperationException($"Unknown type {nameof(type)} for create");
            }
        }

        /// <summary>
        /// Create instance on AccountDto information
        /// </summary>
        /// <param name="type">AccountDto instance</param>
        /// <returns>new instance type Account</returns>
        public static Account Create(AccountDto type)
        {
            switch (type.AccountType)
            {
                case AccountTypeDto.Base:
                    return new BaseAccount(type);
                case AccountTypeDto.Silver:
                    return new SilverAccount(type);
                case AccountTypeDto.Gold:
                    return new GoldAccount(type);
                case AccountTypeDto.Platinum:
                    return new PlatinumAccount(type);
                default:
                    throw new InvalidOperationException($"Unknown type {nameof(type)} for create");
            }
        }

        /// <summary>
        /// Create instance on AccountType information, PersonalInfo and IAccountNumberCreateService
        /// </summary>
        /// <param name="type">AccountType type</param>
        /// <param name="info">PersonalInfo instance</param>
        /// <param name="creator">IAccountNumberCreateService instance</param>
        /// <returns>new instance Account</returns>
        public static Account Create(AccountType type, PersonalInfo info, IAccountNumberCreateService creator)
        {
            switch (type)
            {
                case AccountType.Base:
                    return new BaseAccount(info, creator);
                case AccountType.Silver:
                    return new SilverAccount(info, creator);
                case AccountType.Gold:
                    return new GoldAccount(info, creator);
                case AccountType.Platinum:
                    return new PlatinumAccount(info, creator);
                default:
                    throw new InvalidOperationException($"Unknown type {nameof(type)} for create");
            }
        }
    }
}
