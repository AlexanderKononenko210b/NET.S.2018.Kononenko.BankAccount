using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Service;
using DAL.Interface.DTO;
using DAL.Interface.Interfaces;
using Moq;
using NUnit.Framework;

namespace BLL.Tests.NUnitTests
{
    /// <summary>
    /// NUnit test account
    /// </summary>
    public class PersonalInfoNUnitTests
    {
        #region Fields

        private string numberAccount = "40512100790000000004";

        private PersonalInfo personalInfo;

        private PersonalInfoDto personalInfoDto;

        private AccountDto accountDto;

        #endregion

        #region Initializator

        [SetUp]
        public void Initialize()
        {
            personalInfo = new PersonalInfo
            {
                FirstName = "Fedor",
                LastName = "Bondarchuk",
                Passport = "RT1234136",
                Email = "bondarchuk@gmail.com"
            };

            personalInfoDto = new PersonalInfoDto
            {
                FirstName = "Fedor",
                LastName = "Bondarchuk",
                Passport = "RT1234136",
                Email = "bondarchuk@gmail.com"
            };

            accountDto = new AccountDto
            {
                AccountType = AccountTypeDto.Base,
                Balance = new decimal(10),
                BenefitPoints = 1,
                IsClosed = false,
                NumberOfAccount = numberAccount,
                PersonalInfo = personalInfoDto
            };
        }

        #endregion

        #region Tests

        /// <summary>
        /// Test create user`s personal info with valid data
        /// </summary>
        [TestCase]
        public void Create_PersonalInfo_With_Valid_Data()
        {
            var userService = new UserService();

            var user = userService.Create("Fedor", "Bondarchuk", "RT1234136", "bondarchuk@gmail.com");

            Assert.AreEqual("Fedor", user.FirstName);
            Assert.AreEqual("Bondarchuk", user.LastName);
            Assert.AreEqual("RT1234136", user.Passport);
            Assert.AreEqual("bondarchuk@gmail.com", user.Email);
        }

        /// <summary>
        /// Test create user`s personal info with not valid data (FirstName is null)
        /// </summary>
        [TestCase]
        public void Create_PersonalInfo_If_Input_FirstName_Is_Null()
        {
            var userService = new UserService();

            Assert.Throws<ArgumentNullException>(() =>
                userService.Create(null, "Bondarchuk", "RT1234136", "bondarchuk@gmail.com"));
        }

        /// <summary>
        /// Test create user`s personal info with not valid data (FirstName is empty)
        /// </summary>
        [TestCase]
        public void Create_PersonalInfo_If_Input_FirstName_Is_Empty()
        {
            var userService = new UserService();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                userService.Create("", "Bondarchuk", "RT1234136", "bondarchuk@gmail.com"));
        }

        /// <summary>
        /// Test create user`s personal info with not valid data (FirstName is WhiteSpace)
        /// </summary>
        [TestCase]
        public void Create_PersonalInfo_If_Input_FirstName_Is_WhiteSpace()
        {
            var userService = new UserService();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                userService.Create(" ", "Bondarchuk", "RT1234136", "bondarchuk@gmail.com"));
        }

        /// <summary>
        /// Test create user`s personal info with not valid data (FirstName is not according business rules)
        /// </summary>
        [TestCase]
        public void Create_PersonalInfo_If_Input_FirstName_Is_Not_According_BLL_Rule()
        {
            var userService = new UserService();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                userService.Create("1234", "Bondarchuk", "RT1234136", "bondarchuk@gmail.com"));
        }

        /// <summary>
        /// Test create user`s personal info with not valid data (LastName is not according business rules)
        /// </summary>
        [TestCase]
        public void Create_PersonalInfo_If_Input_LastName_Is_Not_According_BLL_Rule()
        {
            var userService = new UserService();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                userService.Create("Fedor", "bondarchuk", "RT1234136", "bondarchuk@gmail.com"));
        }

        /// <summary>
        /// Test create user`s personal info with not valid data (Pasport number is not according business rules)
        /// </summary>
        [TestCase]
        public void Create_PersonalInfo_If_Input_Pasport_Number_Is_Not_According_BLL_Rule()
        {
            var userService = new UserService();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                userService.Create("Fedor", "bondarchuk", "RT12341", "bondarchuk@gmail.com"));
        }

        /// <summary>
        /// Test create user`s personal info with not valid data (Email is not according business rules)
        /// </summary>
        [TestCase]
        public void Create_PersonalInfo_If_Input_Email_Is_Not_According_BLL_Rule()
        {
            var userService = new UserService();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                userService.Create("Fedor", "bondarchuk", "RT1234136", "bondarchukgmail.com"));
        }

        #endregion
    }
}
