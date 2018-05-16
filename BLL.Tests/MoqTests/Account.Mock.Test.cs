using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Factories;
using BLL.Interface.Entities;
using BLL.Interface.Enum;
using BLL.Interface.Interfaces;
using BLL.Service;
using DAL.Interface.Dto;
using DAL.Interface.Interfaces;
using NUnit.Framework;
using Moq;

namespace BLL.Tests.MoqTests
{
    /// <summary>
    /// Moq test account
    /// </summary>
    [TestFixture]
    public class AccountMockTest
    {
        #region Fields

        private Mock<IAccountRepository> mockAccount;

        private Mock<IUserRepository> mockUserInfo;

        private Mock<IUnitOfWork> mockUnitOfWork;

        private string numberAccount = "40512100790000000001";

        private UserInfo userInfo;

        private UserInfoDto userInfoDto;

        private AccountDto accountDtoFirst;

        private AccountDto accountDtoSecond;

        private AccountDto accountDto;

        #endregion

        #region Initializes

        [SetUp]
        public void Initialize()
        {
            mockAccount = new Mock<IAccountRepository>();

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
                BenefitPoints = 10,
                IsClosed = false,
                NumberOfAccount = numberAccount,
                UserId = 1
            };

            accountDtoFirst = new AccountDto
            {
                AccountType = AccountTypeDto.Base,
                Balance = new decimal(5),
                BenefitPoints = 5,
                IsClosed = false,
                NumberOfAccount = numberAccount,
                UserId = 1
            };

            accountDtoSecond = new AccountDto
            {
                AccountType = AccountTypeDto.Base,
                Balance = new decimal(15),
                BenefitPoints = 15,
                IsClosed = false,
                NumberOfAccount = numberAccount,
                UserId = 1
            };
        }

        #endregion

        #region Test

        /// <summary>
        /// Test create account with valid data
        /// </summary>
        [TestCase]
        public void Open_Account_With_Valid_Data()
        {
            this.mockUnitOfWork.Setup(item => item.Commit());

            this.mockUnitOfWork.Setup(item => item.AccountRepository.Add(It.IsAny<AccountDto>()))
                .Returns(() => accountDto);

            this.mockUnitOfWork.Setup(item => item.UserRepository.Add(It.IsAny<UserInfoDto>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.UserRepository.Get(It.IsAny<int>()))
                .Returns(() => userInfoDto);

            var service = new AccountService(mockUnitOfWork.Object);

            var resultAdd = service.OpenAccount(AccountType.Base, userInfo.Id, new NumberCreateService());

            Assert.AreEqual(AccountType.Base, resultAdd.AccountType);
            Assert.AreEqual("40512100790000000001", resultAdd.NumberOfAccount);
			Assert.AreEqual(10m, resultAdd.Balance);
			Assert.AreEqual(10, resultAdd.BenefitPoints);
			Assert.AreEqual(false, resultAdd.IsClosed);
			Assert.AreEqual(1, resultAdd.UserId);
        }

        /// <summary>
        /// Test deposit to account with valid data
        /// </summary>
        [TestCase]
        public void Deposit_Account_With_Valid_Data()
        {
            this.mockUnitOfWork.Setup(item => item.AccountRepository.Update(It.IsAny<AccountDto>()))
                .Returns(() => accountDto).Callback(() => accountDto.Balance += 100);

            this.mockUnitOfWork.Setup(item => item.UserRepository.Add(It.IsAny<UserInfoDto>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.UserRepository.Get(It.IsAny<int>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.Commit());

            var service = new AccountService(mockUnitOfWork.Object);

            var account = AccountFactory.Create(AccountType.Base, userInfo.Id, new NumberCreateService());

            var resultDeposite = service.DepositAccount(account, 100);

            Assert.AreEqual(110m, resultDeposite);
			Assert.AreEqual(110m, account.Balance);
            Assert.AreEqual(110, account.BenefitPoints);
        }

        /// <summary>
        /// Test deposit to account with valid data
        /// </summary>
        [TestCase]
        public void WithDraw_Account_With_Valid_Data()
        {
            this.mockUnitOfWork.Setup(item => item.AccountRepository.Update(It.IsAny<AccountDto>()))
                .Returns(() => accountDto).Callback(() => accountDto.Balance -= 5);

            this.mockUnitOfWork.Setup(item => item.UserRepository.Add(It.IsAny<UserInfoDto>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.UserRepository.Get(It.IsAny<int>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.Commit());

            var service = new AccountService(mockUnitOfWork.Object);

            var account = AccountFactory.Create(AccountType.Base, userInfo.Id, new NumberCreateService());

            var resultWithDraw = service.WithDrawAccount(account, 5);

            Assert.AreEqual(5m, resultWithDraw);
            Assert.AreEqual(5m, account.Balance);
            Assert.AreEqual(5, account.BenefitPoints);
        }

        /// <summary>
        /// Test deposit to account with valid data
        /// </summary>
        [TestCase]
        public void Transfer_Account_With_Valid_Data()
        {
            this.mockUnitOfWork.SetupSequence(item => item.AccountRepository.Update(It.IsAny<AccountDto>()))
                .Returns(accountDtoFirst)
                .Returns(accountDtoSecond);

            this.mockUnitOfWork.Setup(item => item.UserRepository.Add(It.IsAny<UserInfoDto>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.UserRepository.Get(It.IsAny<int>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.Commit());

            var service = new AccountService(mockUnitOfWork.Object);

            var accountFirst = AccountFactory.Create(AccountType.Base, userInfo.Id, new NumberCreateService());

            Assert.AreEqual(10m, accountFirst.Balance);
            Assert.AreEqual(10, accountFirst.BenefitPoints);

            var accountSecond = AccountFactory.Create(AccountType.Base, userInfo.Id, new NumberCreateService());

            Assert.AreEqual(10m, accountSecond.Balance);
            Assert.AreEqual(10, accountSecond.BenefitPoints);

            var resultTransfer = service.Transfer(accountFirst, accountSecond, 5);
			
            Assert.AreEqual(5m, resultTransfer.Item1.Balance);
            Assert.AreEqual(5, resultTransfer.Item1.BenefitPoints);
            Assert.AreEqual(15m, resultTransfer.Item2.Balance);
            Assert.AreEqual(15, resultTransfer.Item2.BenefitPoints);
        }

        /// <summary>
        /// Test close account with valid data
        /// </summary>
        [TestCase]
        public void Close_Account()
        {
            accountDto.IsClosed = true;

            accountDto.Balance = 0;

            this.mockUnitOfWork.Setup(item => item.AccountRepository.Update(It.IsAny<AccountDto>()))
                .Returns(() => accountDto);

            this.mockUnitOfWork.Setup(item => item.UserRepository.Add(It.IsAny<UserInfoDto>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.UserRepository.Get(It.IsAny<int>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.Commit());

            var service = new AccountService(mockUnitOfWork.Object);

            var account = AccountFactory.Create(AccountType.Base, userInfo.Id, new NumberCreateService());

            var addiction = service.WithDrawAccount(account, 10);

			Assert.AreEqual(0m, account.Balance);

            var result = service.Close(account);

            Assert.IsTrue(account.IsClosed);
        }

        /// <summary>
        /// Test close account if value balance more than zero
        /// </summary>
        [TestCase]
        public void Close_Account_If_Balance_More_Than_Zero()
        {
            this.mockUnitOfWork.Setup(item => item.AccountRepository.Update(It.IsAny<AccountDto>()))
                .Returns(() => accountDto).Callback(() => accountDto.IsClosed = true);

            this.mockUnitOfWork.Setup(item => item.UserRepository.Add(It.IsAny<UserInfoDto>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.UserRepository.Get(It.IsAny<int>()))
                .Returns(() => userInfoDto);

            this.mockUnitOfWork.Setup(item => item.Commit());

            var service = new AccountService(mockUnitOfWork.Object);

            var account = AccountFactory.Create(AccountType.Base, userInfo.Id, new NumberCreateService());

            Assert.Throws<InvalidOperationException>(() => service.Close(account));
        }

        #endregion
    }
}
