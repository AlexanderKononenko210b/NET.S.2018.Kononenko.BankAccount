using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Interface.Interfaces;

namespace DAL.Repositories
{
    /// <summary>
    /// Class for unit of work with repository
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Fields

        private DbContext context;

        private IAccountRepository accountRepository;

        private IUserRepository userRepository;

        private bool isDisposed;

        #endregion

        #region Constructors

        public UnitOfWork(DbContext context,
            IAccountRepository accountRepository,
            IUserRepository userRepository)
        {
            this.context = context;
            this.accountRepository = accountRepository;
            this.userRepository = userRepository;
        }

        #endregion

        #region Public Api

        public IAccountRepository AccountRepository => accountRepository;

        public IUserRepository UserRepository => userRepository;

        /// <summary>
        /// Method for call context`s method SaveChanges()
        /// and save changew in database
        /// </summary>
        public void Commit()
        {
            context.SaveChanges();
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Finalizator with check state field isDisposed
        /// if true - context is dispose, false - not dispose and
        /// set flag isDisposed in true and call method Disposed with false argument
        /// </summary>
        ~UnitOfWork()
        {
            if (isDisposed)
            {
                isDisposed = true;

                Dispose(false);
            }
        }

        /// <summary>
        /// Implement interface IDisposable
        /// Check state field isDisposed
        /// if true - context is dispose, false - not dispose and
        /// set flag isDisposed in true and call method Disposed with true argument
        /// and call CLR not call finalizator for specifiy object
        /// </summary>
        public void Dispose()
        {
            if (isDisposed)
            {
                isDisposed = true;

                Dispose(true);

                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Virtual method for dispose unmanaged resource (context)
        /// </summary>
        /// <param name="disposing">flag for determine where call method</param>
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
