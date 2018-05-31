using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Exceptions;
using BLL.Factories;
using BLL.Interface.Dto;
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

        private IAccountRepository accountRepository;

        private IUserService userService;

        private IAccountNumberCreateService creator;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for Account service
        /// </summary>
        /// <param name="accountRepository">repository for work with entity Account</param>
        /// <param name="userService">service for work with entity User</param>
        /// <param name="creator">service for create number account</param>
        public AccountService(IAccountRepository  accountRepository,
            IUserService userService, IAccountNumberCreateService creator)
        {
            this.accountRepository = accountRepository;

            this.userService = userService;

            this.creator = creator;
        }

        #endregion

        #region Public Api

        /// <summary>
        /// Open account 
        /// </summary>
        /// <param name="type">type account</param>
        /// <param name="userId">user Id</param>
        /// <returns>new account</returns>
        public AccountViewDto OpenAccount(string type, int userId)
        {
            Check.NotNull(type);

            Check.CheckString(type);

            if(userId <= 0)
                throw new ArgumentOutOfRangeException($"User Id must more than 0");
            
            var user = userService.Get(userId);

            if (user == null)
                throw new ExistInDatabaseException($"User with Id : {userId} is not exist in database");

            AccountType typeAccount;

            try
            {
                typeAccount = (AccountType)Enum.Parse(typeof(AccountType), type);
            }
            catch (ArgumentException e)
            {
                throw new InvalidDataException(e.Message);
            }
            catch (OverflowException e)
            {
                throw new InvalidDataException(e.Message);
            }

            var account = AccountFactory.Create(typeAccount, userId, creator);

            var accountDto = Mapper<Account, AccountDto>.Map(account);

            var result = accountRepository.Add(accountDto);

            if (result == null)
                throw new InvalidOperationException($"Add new AccountDto is not valid");

            var accountForView = Mapper<AccountDto, Account>.Map(result);

            return Mapper<Account, AccountViewDto>.MapView(accountForView);
        }

        /// <summary>
        /// Transfer money between two accounts
        /// </summary>
        /// <param name="numberfirst">accounts number for withdraw</param>
        /// <param name="numberSecond">accounts number for deposit</param>
        /// <param name="transfer">transfer value</param>
        /// <returns>Item1 - account for deposit, Item2 - account for withdraw</returns>
        public (AccountViewDto, AccountViewDto) Transfer(string numberfirst, string numberSecond, decimal transfer)
        {
            Check.NotNull(numberfirst);

            var firstViewDto = GetByNumber(numberfirst);

            if (firstViewDto == null)
                throw new InvalidOperationException($"Account with number {numberfirst} does not exist for current user");

            var first = Mapper<AccountViewDto, Account>.MapView(firstViewDto);

            Check.NotNull(numberSecond);

            var secondViewDto = GetByNumber(numberSecond);

            if (secondViewDto == null)
                throw new InvalidOperationException($"Account with number {numberSecond} does not exist for current user");

            var second = Mapper<AccountViewDto, Account>.MapView(secondViewDto);

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

            accountRepository.Commit();

            var firstResult = Mapper<AccountDto, Account>.Map(resultDeposit);

            var secondResult = Mapper<AccountDto, Account>.Map(resultWithDraw);

            return (Mapper<Account, AccountViewDto>.MapView(firstResult), Mapper<Account, AccountViewDto>.MapView(secondResult));
        }

        /// <summary>
        /// Method for delete account
        /// </summary>
        /// <param name="accountViewDto">account view dto for close</param>
        /// <returns>true if account is closed</returns>
        public bool Close(AccountViewDto accountViewDto)
        {
            Check.NotNull(accountViewDto);

            var account = Mapper<AccountViewDto, Account>.MapView(accountViewDto);

            account.CloseValidator();

            account.Close();

            var accountDto = Mapper<Account, AccountDto>.Map(account);

            var result = accountRepository.Update(accountDto);

            if (result == null || result.IsClosed != account.IsClosed)
                throw new InvalidOperationException($"Update after Close Account is not valid");

            accountRepository.Commit();

            return account.IsClosed;
        }

        /// <summary>
        /// Method for deposit money on the account
        /// </summary>
        /// <param name="numberAccount">accounts number</param>
        /// <param name="deposit">deposit money</param>
        public AccountViewDto DepositAccount(string numberAccount, decimal deposit)
        {
            Check.NotNull(numberAccount);

            var accountViewDto = GetByNumber(numberAccount);

            if(accountViewDto == null)
                throw new InvalidOperationException($"Account with number {numberAccount} does not exist for current user");

            var account = Mapper<AccountViewDto, Account>.MapView(accountViewDto);

            account.Deposit(deposit);

            var accountDto = Mapper<Account, AccountDto>.Map(account);

            var resultSave = accountRepository.Update(accountDto);

            if (resultSave == null || resultSave.Balance != account.Balance)
                throw new InvalidOperationException($"Update after Diposit Account is not valid");

            accountRepository.Commit();

            var accountResult = Mapper<AccountDto, Account>.Map(resultSave);

            return Mapper<Account, AccountViewDto>.MapView(accountResult);
        }

        /// <summary>
        /// WithDraw money account
        /// </summary>
        /// <param name="numberAccount">accounts number</param>
        /// <param name="withdraw">withdraw value</param>
        /// <returns>new balance</returns>
        public AccountViewDto WithDrawAccount(string numberAccount, decimal withdraw)
        {
            Check.NotNull(numberAccount);

            var accountViewDto = GetByNumber(numberAccount);

            if (accountViewDto == null)
                throw new InvalidOperationException($"Account with number {numberAccount} does not exist for current user");

            var account = Mapper<AccountViewDto, Account>.MapView(accountViewDto);

            account.WithDrawValidator(withdraw);

            account.WithDraw(withdraw);

            var accountDto = Mapper<Account, AccountDto>.Map(account);

            var resultSave = accountRepository.Update(accountDto);

            if (resultSave == null || resultSave.Balance != account.Balance)
                throw new InvalidOperationException($"Update after WithDraw Account is not valid");

            accountRepository.Commit();

            var accountResult = Mapper<AccountDto, Account>.Map(resultSave);

            return Mapper<Account, AccountViewDto>.MapView(accountResult);
        }

        /// <summary>
        /// Get all accounts in repository
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AccountViewDto> GetAll()
        {
            foreach (var item in accountRepository.GetAll())
            {
                var account = Mapper<AccountDto, Account>.Map(item);

                yield return Mapper<Account, AccountViewDto>
                    .MapView(account);
            }
        }

        /// <summary>
        /// Get account by number
        /// </summary>
        /// <returns>instance type account</returns>
        public AccountViewDto GetByNumber(string number)
        {
            if (number == null)
                throw new ArgumentNullException($"Argument {nameof(number)} is null");

            var accountDto = accountRepository.Get(number);

            var account = Mapper<AccountDto, Account>.Map(accountDto);

            return Mapper<Account, AccountViewDto>.MapView(account);
        }

        /// <summary>
        /// Get AccountViewDto by id
        /// </summary>
        /// <param name="id">identificator account</param>
        /// <returns>instance type AccountViewDto or null</returns>
        public AccountViewDto Get(int id)
        {
            if(id <= 0)
                throw new ArgumentOutOfRangeException($"Account Id must be more than 0");

            var accountDto = accountRepository.Get(id);

            if (accountDto == null) return null;

            var account = Mapper<AccountDto, Account>.Map(accountDto);

            return Mapper<Account, AccountViewDto>.MapView(account);
        }

        public IEnumerable<string> GetAllNumbers(int userId)
        {
            if (userId <= 0)
                throw new ArgumentOutOfRangeException($"User Id must more than 0");

            var numbers = accountRepository.GetNumbers(userId);

            return numbers;
        }

        #endregion
    }
}
