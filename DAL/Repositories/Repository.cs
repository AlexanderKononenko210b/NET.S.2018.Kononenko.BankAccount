using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DAL.Exceptions;
using DAL.Interface.DTO;
using DAL.Interface.Interfaces;
using DAL.Mappers;

namespace DAL.Repositories
{
    /// <summary>
    /// Repository for work with database using Entity Framework and an approach DataBaseFirst
    /// </summary>
    public class Repository : IRepository<AccountDto>, IDisposable
    {
        #region Fields

        private readonly AccountEntities context;

        private bool isDisposed;

        #endregion

        #region Constructors

        public Repository(AccountEntities context)
        {
            this.context = context;
        }

        #endregion

        #region Public Api

        /// <summary>
        /// Add new model in database.
        /// First check exist account in database with the same number, if the same
        /// throw new ExistInDatabaseException
        /// Second check exist user in database, if exist write his id in 
        /// field PersonalInfo and assign reference User null, else
        /// add new user in database and write his id in field PersonalInfo and assign reference User in null.
        /// Third check exist type in database and if exist write his id in 
        /// field AccountType, else add new type in database and write his id 
        /// in field AccountType assign reference Type in null.
        /// Fourth add account in database.
        /// </summary>
        /// <param name="model">instance type T</param>
        /// <returns>instance that add</returns>
        public AccountDto Add(AccountDto model)
        {
            if (isDisposed)
                throw new ObjectDisposedException($"Context {nameof(context)} is disposed");

            if (model == null)
                throw new ArgumentNullException($"Argument {nameof(model)} is null");

            var searchAccount = context.Accounts.FirstOrDefault(item =>
                item.NumberOfAccount.Equals(model.NumberOfAccount, StringComparison.CurrentCulture));

            if (searchAccount != null)
                throw new ExistInDatabaseException($"Account {nameof(model)} already exist in database");

            var accountModel = model.AccountDtoToAccontModel();

            var searchUser = context.Users.FirstOrDefault(item => item.FirstName == accountModel.User.FirstName
                && item.LastName == accountModel.User.LastName
                && item.Email == accountModel.User.Email
                && item.Passport == accountModel.User.Passport);

            if (searchUser != null)
            {
                accountModel.PersonalInfo = searchUser.Id;
            }
            else
            {
                var resultAddUser = context.Users.Add(accountModel.User);
                context.SaveChanges();
                accountModel.PersonalInfo = resultAddUser.Id;
            }

            accountModel.User = null;

            var searchType = context.Types.FirstOrDefault(item => item.AccountType == accountModel.Type.AccountType);

            if (searchType != null)
            {
                accountModel.AccountType = searchType.Id;
            }
            else
            {
                var resultAddType = context.Types.Add(accountModel.Type);
                context.SaveChanges();
                accountModel.AccountType = resultAddType.Id;
            }

            accountModel.Type = null;

            var resultAddAccount = context.Accounts.Add(accountModel);

            if (resultAddAccount == null)
                throw new InvalidOperationException($"Account {nameof(model)} does not add in database");

            context.SaveChanges();

            return model;
        }

        /// <summary>
        /// Delete instance from database
        /// </summary>
        /// <param name="model">instance for delete</param>
        /// <returns>instance that delete</returns>
        public AccountDto Delete(AccountDto model)
        {
            if (isDisposed)
                throw new ObjectDisposedException($"Context {nameof(context)} is disposed");

            if (model == null)
                throw new ArgumentNullException($"Argument {nameof(model)} is null");

            var accountModel = model.AccountDtoToAccontModel();

            var searchModel = context.Accounts
                .SingleOrDefault(item => item.NumberOfAccount == model.NumberOfAccount);

            if (searchModel == null)
                throw new ExistInDatabaseException($"Account with number {model.NumberOfAccount} is not exist in database");

            context.Accounts.Remove(searchModel);

            context.SaveChanges();

            return model;
        }

        /// <summary>
        /// Get account by number
        /// </summary>
        /// <param name="number">value number</param>
        /// <returns>instance type AccountDto</returns>
        public AccountDto Get(string number)
        {
            if (isDisposed)
                throw new ObjectDisposedException($"Context {nameof(context)} is disposed");

            var accountModel = context.Accounts.SingleOrDefault(item => item.NumberOfAccount == number);

            var result = accountModel.AccontModelToAccountDto();

            return result;
        }

        /// <summary>
        /// Get all account from danabase
        /// </summary>
        /// <returns>account`s instances</returns>
        public IEnumerable<AccountDto> GetAll()
        {
            if (isDisposed)
                throw new ObjectDisposedException($"Context {nameof(context)} is disposed");

            foreach (Account item in context.Accounts.Include("User"))
            {
                yield return item.AccontModelToAccountDto();
            }
        }

        /// <summary>
        /// Update account in database
        /// </summary>
        /// <param name="model">instance type AccountDto for update</param>
        /// <returns>instance after update</returns>
        public AccountDto Update(AccountDto model)
        {
            if (isDisposed)
                throw new ObjectDisposedException($"Context {nameof(context)} is disposed");

            if (model == null)
                throw new ArgumentNullException($"Argument {nameof(model)} is null");

            var accountFromDb = model.AccountDtoToAccontModel();

            var accountForUpdate = context.Accounts.SingleOrDefault(item => item.NumberOfAccount == accountFromDb.NumberOfAccount);

            accountForUpdate = accountForUpdate.AccountModelToAccontModel(accountFromDb);

            context.Entry(accountForUpdate).State = EntityState.Modified;

            context.SaveChanges();

            return accountForUpdate.AccontModelToAccountDto();
        }

        /// <summary>
        /// Get AccountDto by id
        /// </summary>
        /// <param name="id">identificator</param>
        /// <returns>instance type AccountDto</returns>
        public AccountDto Get(int id)
        {
            if (isDisposed)
                throw new ObjectDisposedException($"Context {nameof(context)} is disposed");

            if (id < 0)
                throw new ArgumentNullException($"Argument {nameof(id)} is not valid");

            var accountDto = context.Accounts.SingleOrDefault(item => item.Id == id);

            if(accountDto == null)
                throw new ExistInDatabaseException($"Account with id = {id} is absent in DataBase");

            return accountDto.AccontModelToAccountDto();
        }

        #endregion

        #region Disposable

        /// <summary>
        /// If unmanage resources are not release (isDisposed = false)
        /// set isDisposed in true and call method Dispose with false parameter
        /// </summary>
        ~Repository()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                Dispose(false);
            }
        }

        /// <summary>
        /// Determine manage unmanage resources
        /// if isDisposed = false set isDisposed in true and call method 
        /// Dispose with true parameter and not call finalizer
        /// </summary>
        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Virtual method for free unmanage resources
        /// </summary>
        /// <param name="disposing">true - method call in determine miner, false - undetermine miner</param>
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
            {
                return;
            }

            context.Dispose();

            isDisposed = true;
        }

        #endregion
    }
}
