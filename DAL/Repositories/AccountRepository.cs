using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Exceptions;
using DAL.Interface.DbModels;
using DAL.Interface.Dto;
using DAL.Interface.Interfaces;
using DAL.Mappers;
using DAL.Validators;

namespace DAL.Repositories
{
    /// <summary>
    /// Repository for work with specify for account method
    /// </summary>
    public class AccountRepository : Repository<AccountDto, AccountDbModel>, IAccountRepository
    {
        #region Constructors

        public AccountRepository(DbContext context)
            : base(context){ }

        #endregion

        #region Public Api

        /// <summary>
        /// Get account by number
        /// </summary>
        /// <param name="number">value number</param>
        /// <returns>instance type AccountDto</returns>
        public AccountDto Get(string number)
        {
            if (this.isDisposed)
                throw new ObjectDisposedException($"Context {nameof(context)} is disposed");

            var accountModel = this.dbSet.SingleOrDefault(item => item.NumberOfAccount == number);

            if (accountModel == null)
                throw new ExistInDatabaseException($"Account with the same number {number} is abset in database");

            var resultGet = Mapper<AccountDbModel, AccountDto>.Map(accountModel);

            return resultGet;
        }

        /// <summary>
        /// Get all account from danabase
        /// </summary>
        /// <returns>account`s instances</returns>
        public IEnumerable<AccountDto> GetAll()
        {
            if (this.isDisposed)
                throw new ObjectDisposedException($"Context {nameof(context)} is disposed");

            foreach (AccountDbModel item in this.dbSet.Where(item => item.IsClosed == false).AsNoTracking())
            {
                yield return Mapper<AccountDbModel, AccountDto>.Map(item);
            }
        }

        /// <summary>
        /// Override method Add
        /// </summary>
        /// <param name="account">instance type AccountDto</param>
        /// <returns></returns>
        public override AccountDto Add(AccountDto account)
        {
            if (this.isDisposed)
                throw new ObjectDisposedException($"Context {nameof(context)} is disposed");

            Check.NotNull(account);

            var accountForAdd = Mapper<AccountDto, AccountDbModel>.Map(account);

            var accountFind = this.dbSet.SingleOrDefault(item => item.NumberOfAccount.Equals(accountForAdd.NumberOfAccount, StringComparison.CurrentCulture));

            if (accountFind != null)
                throw new ExistInDatabaseException($"Account with the same number {accountForAdd.NumberOfAccount} already exist in database");

            var resultAdd = this.dbSet.Add(accountForAdd);

            this.Commit();

            var resultDto = Mapper<AccountDbModel, AccountDto>.Map(resultAdd);

            return resultDto;
        }

        /// <summary>
        /// Get all accounts number for specify userId
        /// </summary>
        /// <param name="userId">user</param>
        /// <returns></returns>
        public IEnumerable<string> GetNumbers(int userId)
        {
            if (this.isDisposed)
                throw new ObjectDisposedException($"Context {nameof(context)} is disposed");

            var numbers = this.dbSet.Where(item => item.UserId == userId).Select(item => item.NumberOfAccount);

            return numbers;
        }

        #endregion
    }
}
