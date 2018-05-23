using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Exceptions;
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

        private IUserRepository userRepository;

        #endregion

        #region Constructors

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            this.unitOfWork = unitOfWork;

            this.userRepository = userRepository;
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

            var userAfterSave = userRepository.Add(userForSave);
            
            var userSave = Mapper<UserInfoDto, UserInfo>.Map(userAfterSave);

            return userSave;
        }

        /// <summary>
        /// Get method instance type user
        /// </summary>
        /// <param name="id">identificator</param>
        /// <returns>instance type UserInfo</returns>
        public UserInfo Get(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException($"Argument {id} is not valid");

            var resultFind = userRepository.Get(id);

            if(resultFind == null)
                throw new ExistInDatabaseException($"User with with the same id is absent in database");

            var user = Mapper<UserInfoDto, UserInfo>.Map(resultFind);

            return user;
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
