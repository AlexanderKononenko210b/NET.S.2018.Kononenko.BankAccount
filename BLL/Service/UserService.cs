using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using BLL.Mappers;
using BLL.Validators;
using DAL.Interface.Dto;
using DAL.Interface.Interfaces;

namespace BLL.Service
{
    /// <summary>
    /// Service for create new instance userInfo
    /// </summary>
    public class UserService : IUserService
    {
        #region Fields

        private IUnitOfWork unitOfWork;

        #endregion

        #region Constructors

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Api

        /// <summary>
        /// Create new userInfo about user with verify input information
        /// </summary>
        /// <param name="firstName">first name</param>
        /// <param name="lastName">last name</param>
        /// <param name="passport">number passport</param>
        /// <param name="email">email</param>
        /// <returns>new instance userInfo</returns>
        public UserInfo Create(string firstName, string lastName, string passport, string email)
        {
            IsVerify(firstName, lastName, passport, email);

            var userInfo = new UserInfo(firstName, lastName, passport, email);

            var userForSave = Mapper<UserInfo, UserInfoDto>.Map(userInfo);

            var userAfterSave = unitOfWork.UserRepository.Add(userForSave);

            unitOfWork.Commit();

            var userSave = Mapper<UserInfoDto, UserInfo>.Map(userAfterSave);

            return userSave;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method for validate input data user
        /// </summary>
        /// <param name="firstName">first name</param>
        /// <param name="lastName">last name</param>
        /// <param name="passport">number passport</param>
        /// <param name="email">email</param>
        private void IsVerify(string firstName, string lastName, string passport, string email)
        {
            Check.NotNull(firstName);
            Check.NotNull(lastName);
            Check.NotNull(passport);
            Check.NotNull(email);

            Check.CheckString(firstName);
            Check.CheckString(lastName);
            Check.CheckString(passport);
            Check.CheckString(email);

            UserValidator.CheckFirstName(firstName);
            UserValidator.CheckLastName(lastName);
            UserValidator.CheckPassport(passport);
            UserValidator.CheckEmail(email);
        }

        #endregion
    }
}
