using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
    /// Class for work with data User account
    /// </summary>
    public class UserRepository : Repository<UserInfoDto, UserInfoDbModel>, IUserRepository
    {
        #region Fields

        private readonly AccountContext context;

        private bool isDisposed;

        #endregion

        #region Constructors

        public UserRepository(AccountContext context)
            : base(context)
        {
            this.context = context;
        }

        #endregion

        #region Public Api

        /// <summary>
        /// Get all account from danabase
        /// </summary>
        /// <returns>account`s instances</returns>
        public IEnumerable<UserInfoDto> GetAll()
        {
            if (isDisposed)
                throw new ObjectDisposedException($"Context {nameof(context)} is disposed");

            foreach (UserInfoDbModel item in context.Set<UserInfoDbModel>())
            {
                yield return Mapper<UserInfoDbModel, UserInfoDto>.Map(item);
            }
        }

        /// <summary>
        /// Override method Add
        /// </summary>
        /// <param name="user">instance type AccountDto</param>
        /// <returns></returns>
        public override UserInfoDto Add(UserInfoDto user)
        {
            Check.NotNull(user);

            if (isDisposed)
                throw new ObjectDisposedException($"Context {nameof(user)} is disposed");

            var userForAdd = Mapper<UserInfoDto, UserInfoDbModel>.Map(user);

            var resultFind = context.Users.SingleOrDefault(item => item.FirstName == userForAdd.FirstName
                                                     && item.LastName == userForAdd.LastName
                                                     && item.Passport == userForAdd.Passport
                                                     && item.Email == userForAdd.Email);

            if (resultFind == null)
            {
                var resultAdd = context.Users.Add(userForAdd);

                context.SaveChanges();

                var resultDto = Mapper<UserInfoDbModel, UserInfoDto>.Map(resultAdd);

                return resultDto;
            }

            return Mapper<UserInfoDbModel, UserInfoDto>.Map(resultFind);
        }

        #endregion

        #region Disposable

        /// <summary>
        /// If unmanage resources are not release (isDisposed = false)
        /// set isDisposed in true and call method Dispose with false parameter
        /// </summary>
        ~UserRepository()
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
