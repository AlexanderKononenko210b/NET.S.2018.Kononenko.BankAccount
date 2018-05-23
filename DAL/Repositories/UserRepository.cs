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
        #region Constructors

        public UserRepository(DbContext context)
            : base(context) { }

        #endregion

        #region Public Api

        /// <summary>
        /// Get all account from database
        /// </summary>
        /// <returns>account`s instances</returns>
        public IEnumerable<UserInfoDto> GetAll()
        {
            foreach (UserInfoDbModel item in this.dbSet.AsNoTracking())
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

            var userForAdd = Mapper<UserInfoDto, UserInfoDbModel>.Map(user);

            var resultFind = this.dbSet.SingleOrDefault(item => item.FirstName == userForAdd.FirstName
                                                     && item.LastName == userForAdd.LastName
                                                     && item.Passport == userForAdd.Passport
                                                     && item.Email == userForAdd.Email);

            if (resultFind == null)
            {
                var resultAdd = this.dbSet.Add(userForAdd);

                this.context.SaveChanges();

                var resultDto = Mapper<UserInfoDbModel, UserInfoDto>.Map(resultAdd);

                return resultDto;
            }

            return Mapper<UserInfoDbModel, UserInfoDto>.Map(resultFind);
        }

        #endregion
    }
}
