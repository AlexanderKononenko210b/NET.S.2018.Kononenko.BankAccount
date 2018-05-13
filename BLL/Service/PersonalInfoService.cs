using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using BLL.Validators;

namespace BLL.Service
{
    /// <summary>
    /// Service for create new instance PersonalInfo
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// Create new PersonalInfo about user with verify input information
        /// </summary>
        /// <param name="firstName">first name</param>
        /// <param name="lastName">last name</param>
        /// <param name="passport">number passport</param>
        /// <param name="email">email</param>
        /// <returns>new instance PersonalInfo</returns>
        public PersonalInfo Create(string firstName, string lastName, string passport, string email)
        {
            IsVerify(firstName, lastName, passport, email);

            return new PersonalInfo(firstName, lastName, passport, email);
        }

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

            PersonalInfoValidator.CheckFirstName(firstName);
            PersonalInfoValidator.CheckLastName(lastName);
            PersonalInfoValidator.CheckPassport(passport);
            PersonalInfoValidator.CheckEmail(email);
        }
    }
}
