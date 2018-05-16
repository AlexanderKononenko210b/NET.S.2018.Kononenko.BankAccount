using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
        #region Fields

        private readonly AccountContext context;

        private bool isDisposed;

        #endregion

        #region Constructors

        public AccountRepository(AccountContext context)
            : base(context)
        {
            this.context = context;
        }

        #endregion

        #region Public Api

        /// <summary>
        /// Get account by number
        /// </summary>
        /// <param name="number">value number</param>
        /// <returns>instance type AccountDto</returns>
        public AccountDto Get(string number)
        {
            if (isDisposed)
                throw new ObjectDisposedException($"Context {nameof(context)} is disposed");

            var accountModel = context.Set<AccountDbModel>().SingleOrDefault(item => item.NumberOfAccount == number);

            var resultGet = Mapper<AccountDbModel, AccountDto>.Map(accountModel);

            return resultGet;
        }

        /// <summary>
        /// Get all account from danabase
        /// </summary>
        /// <returns>account`s instances</returns>
        public IEnumerable<AccountDto> GetAll()
        {
            if (isDisposed)
                throw new ObjectDisposedException($"Context {nameof(context)} is disposed");

            foreach (AccountDbModel item in context.Set<AccountDbModel>().AsNoTracking())
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
            Check.NotNull(account);

            if (isDisposed)
                throw new ObjectDisposedException($"Context {nameof(context)} is disposed");

            var accountForAdd = Mapper<AccountDto, AccountDbModel>.Map(account);

            var accountFind = context.Accounts.SingleOrDefault(item => item.NumberOfAccount
                .Equals(accountForAdd.NumberOfAccount, StringComparison.CurrentCulture));

            if (accountFind != null)
                throw new ExistInDatabaseException($"Account with the same number " +
                                                   $"{accountForAdd.NumberOfAccount} already exist in database");

            var resultAdd = context.Accounts.Add(accountForAdd);

            context.SaveChanges();

            var resultDto = Mapper<AccountDbModel, AccountDto>.Map(resultAdd);

            return resultDto;
        }

        #endregion

        #region Disposable

        /// <summary>
        /// If unmanage resources are not release (isDisposed = false)
        /// set isDisposed in true and call method Dispose with false parameter
        /// </summary>
        ~AccountRepository()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                base.Dispose(false);
            }
        }

        #endregion
    }
}
