using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Factories;
using BLL.Interface.Entities;
using BLL.Interface.Enum;
using BLL.Interface.Interfaces;
using BLL.Mappers;
using BLL.Validators;
using DAL.Interface.DTO;
using DAL.Interface.Interfaces;

namespace BLL.ServiceImplementation
{
    /// <summary>
    /// Service for work with account
    /// </summary>
    public class AccountService : IAccountService
    {
        #region Fields

        private IRepository<AccountDto> repository;

        #endregion

        #region Constructors

        public AccountService(IRepository<AccountDto> repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Public Api

        /// <summary>
        /// Open account 
        /// </summary>
        /// <param name="type">type account</param>
        /// <param name="info">personal info</param>
        /// <param name="creator">type account</param>
        /// <returns>new account</returns>
        public Account OpenAccount(AccountType type, PersonalInfo info, IAccountNumberCreateService creator)
        {
            Check.NotNull(info);

            Check.NotNull(creator);

            var account = AccountFactory.Create(type, info, creator);

            var accountDto = account.AccountToAccountDto();

            var result = repository.Add(accountDto);

            if (result == null)
                throw new InvalidOperationException($"Add new Account.Dto is not valid");

            return account;
        }

        /// <summary>
        /// Transfer money between two accounts
        /// </summary>
        /// <param name="first">account for withdraw</param>
        /// <param name="second">account for deposit</param>
        /// <param name="transfer">transfer value</param>
        /// <returns>balance account with transfer money</returns>
        public bool Transfer(Account first, Account second, decimal transfer)
        {
            Check.NotNull(first);

            Check.NotNull(second);

            first.TransferValidator(second, transfer);

            first.WithDraw(transfer);

            second.Deposit(transfer);

            var firstDto = first.AccountToAccountDto();

            var result = repository.Update(firstDto);

            if (result == null)
                throw new InvalidOperationException($"Update after withdraw in transfer between two Accounts is not valid");

            var secondDto = second.AccountToAccountDto();

            result = repository.Update(secondDto);

            if (result == null)
                throw new InvalidOperationException($"Update after deposit in transfer between two Accounts is not valid");

            return true;
        }

        /// <summary>
        /// Method for delete account
        /// </summary>
        /// <param name="account">account for close</param>
        /// <returns></returns>
        public bool Close(Account account)
        {
            Check.NotNull(account);

            account.CloseValidator();

            account.Close();

            var accountDto = account.AccountToAccountDto();

            var result = repository.Update(accountDto);

            if (result == null)
                throw new InvalidOperationException($"Update after Close Account is not valid");

            return account.IsClosed;
        }

        /// <summary>
        /// Method for deposit money on the account
        /// </summary>
        /// <param name="account">account for operation</param>
        /// <param name="deposit">deposit money</param>
        public decimal DepositAccount(Account account, decimal deposit)
        {
            Check.NotNull(account);

            account.Deposit(deposit);

            var accountDto = account.AccountToAccountDto();

            var resultSave = repository.Update(accountDto);

            if (resultSave == null)
                throw new InvalidOperationException($"Update after Diposit Account is not valid");

            return account.Balance;
        }

        /// <summary>
        /// WithDraw money account
        /// </summary>
        /// <param name="account">account for operation</param>
        /// <param name="withdraw">withdraw value</param>
        /// <returns>new balance</returns>
        public decimal WithDrawAccount(Account account, decimal withdraw)
        {
            Check.NotNull(account);

            account.WithDrawValidator(withdraw);

            account.WithDraw(withdraw);

            var accountDto = account.AccountToAccountDto();

            var resultSave = repository.Update(accountDto);

            if (resultSave == null)
                throw new InvalidOperationException($"Update after WithDraw Account is not valid");

            return account.Balance;
        }

        /// <summary>
        /// Get all accounts in repository
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Account> GetAll()
        {
            List<Account> accounts = new List<Account>();

            foreach (var item in repository.GetAll())
            {
                accounts.Add(item.AccountDtoToAccount());
            }

            return accounts;
        }

        /// <summary>
        /// Get account by number
        /// </summary>
        /// <returns>instance type account</returns>
        public Account GetByNumber(string number)
        {
            if (number == null)
                throw new ArgumentNullException($"Argument {nameof(number)} is null");

            var accountDto = repository.Get(number);

            var account = accountDto.AccountDtoToAccount();

            return account;
        }

        #endregion
    }
}
