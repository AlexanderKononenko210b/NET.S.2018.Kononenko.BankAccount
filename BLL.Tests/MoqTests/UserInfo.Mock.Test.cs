using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Service;
using DAL.Interface.Dto;
using DAL.Interface.Interfaces;
using Moq;
using NUnit.Framework;

namespace BLL.Tests.MoqTests
{
    /// <summary>
    /// NUnit test account
    /// </summary>
    public class UserInfoMockTests
    {
        #region Fields

        private Mock<IUnitOfWork> mockUnitOfWork;

        private Mock<IUserRepository> mockUserInfo;

        private string numberAccount = "40512100790000000004";

        private UserInfo userInfo;

        private UserInfoDto userInfoDto;

        private AccountDto accountDto;

        #endregion

        #region Initializator

        [SetUp]
        public void Initialize()
        {
            mockUserInfo = new Mock<IUserRepository>();

            mockUnitOfWork = new Mock<IUnitOfWork>();

            userInfo = new UserInfo
            {
                Id = 1,
                FirstName = "Fedor",
                LastName = "Bondarchuk",
                Passport = "RT1234136",
                Email = "bondarchuk@gmail.com"
            };

            userInfoDto = new UserInfoDto
            {
                Id = 1,
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
                UserId = 1
            };
        }

        #endregion

        #region Tests

        /// <summary>
        /// Test create user`s user info with valid data
        /// </summary>
        [TestCase]
        public void Create_UserInfo_With_Valid_Data()
        {
            this.mockUnitOfWork.Setup(item => item.UserRepository.Add(It.IsAny<UserInfoDto>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.UserRepository.Get(It.IsAny<int>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.Commit());

            var userService = new UserService(mockUnitOfWork.Object);

            var user = userService.Create("Fedor", "Bondarchuk", "RT1234136", "bondarchuk@gmail.com");

            Assert.AreEqual("Fedor", user.FirstName);
            Assert.AreEqual("Bondarchuk", user.LastName);
            Assert.AreEqual("RT1234136", user.Passport);
            Assert.AreEqual("bondarchuk@gmail.com", user.Email);
        }

        /// <summary>
        /// Test create user`s user info with not valid data (FirstName is null)
        /// </summary>
        [TestCase]
        public void Create_UserInfo_If_Input_FirstName_Is_Null()
        {
            this.mockUnitOfWork.Setup(item => item.UserRepository.Add(It.IsAny<UserInfoDto>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.UserRepository.Get(It.IsAny<int>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.Commit());

            var userService = new UserService(mockUnitOfWork.Object);

            Assert.Throws<ArgumentNullException>(() =>
                userService.Create(null, "Bondarchuk", "RT1234136", "bondarchuk@gmail.com"));
        }

        /// <summary>
        /// Test create user`s user info with not valid data (FirstName is empty)
        /// </summary>
        [TestCase]
        public void Create_UserInfo_If_Input_FirstName_Is_Empty()
        {
            this.mockUnitOfWork.Setup(item => item.UserRepository.Add(It.IsAny<UserInfoDto>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.UserRepository.Get(It.IsAny<int>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.Commit());

            var userService = new UserService(mockUnitOfWork.Object);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                userService.Create("", "Bondarchuk", "RT1234136", "bondarchuk@gmail.com"));
        }

        /// <summary>
        /// Test create user`s user info with not valid data (FirstName is WhiteSpace)
        /// </summary>
        [TestCase]
        public void Create_UserInfo_If_Input_FirstName_Is_WhiteSpace()
        {
            this.mockUnitOfWork.Setup(item => item.UserRepository.Add(It.IsAny<UserInfoDto>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.UserRepository.Get(It.IsAny<int>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.Commit());

            var userService = new UserService(mockUnitOfWork.Object);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                userService.Create(" ", "Bondarchuk", "RT1234136", "bondarchuk@gmail.com"));
        }

        /// <summary>
        /// Test create user`s user info with not valid data (FirstName is not according business rules)
        /// </summary>
        [TestCase]
        public void Create_UserInfo_If_Input_FirstName_Is_Not_According_BLL_Rule()
        {
            this.mockUnitOfWork.Setup(item => item.UserRepository.Add(It.IsAny<UserInfoDto>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.UserRepository.Get(It.IsAny<int>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.Commit());

            var userService = new UserService(mockUnitOfWork.Object);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                userService.Create("1234", "Bondarchuk", "RT1234136", "bondarchuk@gmail.com"));
        }

        /// <summary>
        /// Test create user`s user info with not valid data (LastName is not according business rules)
        /// </summary>
        [TestCase]
        public void Create_UserInfo_If_Input_LastName_Is_Not_According_BLL_Rule()
        {
            this.mockUnitOfWork.Setup(item => item.UserRepository.Add(It.IsAny<UserInfoDto>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.UserRepository.Get(It.IsAny<int>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.Commit());

            var userService = new UserService(mockUnitOfWork.Object);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                userService.Create("Fedor", "bondarchuk", "RT1234136", "bondarchuk@gmail.com"));
        }

        /// <summary>
        /// Test create user`s user info with not valid data (Pasport number is not according business rules)
        /// </summary>
        [TestCase]
        public void Create_UserInfo_If_Input_Pasport_Number_Is_Not_According_BLL_Rule()
        {
            this.mockUnitOfWork.Setup(item => item.UserRepository.Add(It.IsAny<UserInfoDto>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.UserRepository.Get(It.IsAny<int>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.Commit());

            var userService = new UserService(mockUnitOfWork.Object);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                userService.Create("Fedor", "bondarchuk", "RT12341", "bondarchuk@gmail.com"));
        }

        /// <summary>
        /// Test create user`s user info with not valid data (Email is not according business rules)
        /// </summary>
        [TestCase]
        public void Create_UserInfo_If_Input_Email_Is_Not_According_BLL_Rule()
        {
            this.mockUnitOfWork.Setup(item => item.UserRepository.Add(It.IsAny<UserInfoDto>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.UserRepository.Get(It.IsAny<int>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.Commit());

            var userService = new UserService(mockUnitOfWork.Object);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                userService.Create("Fedor", "bondarchuk", "RT1234136", "bondarchukgmail.com"));
        }

        #endregion
    }
}
