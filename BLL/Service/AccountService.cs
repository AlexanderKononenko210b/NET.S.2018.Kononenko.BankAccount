using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Exceptions;
using BLL.Factories;
using BLL.Interface.Entities;
using BLL.Interface.Enum;
using BLL.Interface.Interfaces;
using BLL.Mappers;
using BLL.Validators;
using DAL.Interface.Dto;
using DAL.Interface.Interfaces;

namespace BLL.Service
{
    /// <summary>
    /// Service for work with account
    /// </summary>
    public class AccountService : IAccountService
    {
        #region Fields

        private IUnitOfWork unitOfWork;

        private IAccountRepository accountRepository;

        private IUserService userService;

        #endregion

        #region Constructors

        public AccountService(IUnitOfWork unitOfWork, IAccountRepository  accountRepository,
            IUserService userService)
        {
            this.unitOfWork = unitOfWork;

            this.accountRepository = accountRepository;

            this.userService = userService;
        }

        #endregion

        #region Public Api

        /// <summary>
        /// Open account 
        /// </summary>
        /// <param name="type">type account</param>
        /// <param name="userId">user Id</param>
        /// <param name="creator">type account</param>
        /// <returns>new account</returns>
        public Account OpenAccount(AccountType type, int userId, IAccountNumberCreateService creator)
        {
            Check.NotNull(creator);

            var user = userService.Get(userId);

            if (user == null)
                throw new ExistInDatabaseException($"User with Id : {userId} is not exist in database");

            var account = AccountFactory.Create(type, userId, creator);

            var accountDto = Mapper<Account, AccountDto>.Map(account);

            var result = accountRepository.Add(accountDto);

            if (result == null)
                throw new InvalidOperationException($"Add new AccountDto is not valid");

            return Mapper<AccountDto, Account>.Map(result);
        }

        /// <summary>
        /// Transfer money between two accounts
        /// </summary>
        /// <param name="first">account for withdraw</param>
        /// <param name="second">account for deposit</param>
        /// <param name="transfer">transfer value</param>
        /// <returns>Item1 - account for deposit, Item2 - account for withdraw</returns>
        public (Account, Account) Transfer(Account first, Account second, decimal transfer)
        {
            Check.NotNull(first);

            Check.NotNull(second);

            first.TransferValidator(second, transfer);

            first.WithDraw(transfer);

            second.Deposit(transfer);

            var firstDto = Mapper<Account, AccountDto>.Map(first);

            var resultDeposit = accountRepository.Update(firstDto);

            if (resultDeposit == null || resultDeposit.Balance != first.Balance)
                throw new InvalidOperationException($"Update after withdraw in transfer between two Accounts is not valid");

            var secondDto = Mapper<Account, AccountDto>.Map(second);

            var resultWithDraw = accountRepository.Update(secondDto);

            if (resultWithDraw == null || resultWithDraw.Balance != second.Balance)
                throw new InvalidOperationException($"Update after deposit in transfer between two Accounts is not valid");

            unitOfWork.Commit();

            return (Mapper<AccountDto, Account>.Map(resultDeposit), Mapper<AccountDto, Account>.Map(resultWithDraw));
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

            var accountDto = Mapper<Account, AccountDto>.Map(account);

            var result = accountRepository.Update(accountDto);

            if (result == null || result.IsClosed != account.IsClosed)
                throw new InvalidOperationException($"Update after Close Account is not valid");

            unitOfWork.Commit();

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

            var accountDto = Mapper<Account, AccountDto>.Map(account);

            var resultSave = accountRepository.Update(accountDto);

            if (resultSave == null || resultSave.Balance != account.Balance)
                throw new InvalidOperationException($"Update after Diposit Account is not valid");

            unitOfWork.Commit();

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

            var accountDto = Mapper<Account, AccountDto>.Map(account);

            var resultSave = accountRepository.Update(accountDto);

            if (resultSave == null || resultSave.Balance != account.Balance)
                throw new InvalidOperationException($"Update after WithDraw Account is not valid");

            unitOfWork.Commit();

            return account.Balance;
        }

        /// <summary>
        /// Get all accounts in repository
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Account> GetAll()
        {
            foreach (var item in accountRepository.GetAll())
            {
                yield return Mapper<AccountDto, Account>.Map(item);
            }
        }

        /// <summary>
        /// Get account by number
        /// </summary>
        /// <returns>instance type account</returns>
        public Account GetByNumber(string number)
        {
            if (number == null)
                throw new ArgumentNullException($"Argument {nameof(number)} is null");

            var accountDto = accountRepository.Get(number);

            var account = Mapper<AccountDto, Account>.Map(accountDto);

            return account;
        }

        #endregion
    }
}
