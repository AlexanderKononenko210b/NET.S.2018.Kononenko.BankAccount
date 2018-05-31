using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Factories;
using BLL.Interface.Dto;
using BLL.Interface.Entities;
using BLL.Interface.Enum;
using BLL.Interface.Interfaces;
using BLL.Mappers;
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

        private Mock<IUserService> mockUserInfo;

        private Mock<IAccountNumberCreateService> mockNumber;

        private string numberFirst = "40512100790000000001";

        private string numberSecond = "40512100790000000002";

        private UserViewDto userViewDto;

        private AccountDto accountDtoFirst;

        private AccountDto accountDtoSecond;

        private AccountDto accountDto;

        #endregion

        #region Initializes

        [SetUp]
        public void Initialize()
        {
            mockAccount = new Mock<IAccountRepository>();

            this.mockAccount.Setup(item => item.Add(It.IsAny<AccountDto>()))
                .Returns(() => accountDto);

            this.mockAccount.Setup(item => item.Get(It.IsAny<string>()))
                .Returns(() => accountDto);

            mockUserInfo = new Mock<IUserService>();

            this.mockUserInfo.Setup(item => item.Get(It.IsAny<int>()))
                .Returns(() => userViewDto);

            mockNumber = new Mock<IAccountNumberCreateService>();

            this.mockNumber.Setup(item => item.GetNumberAccount())
                .Returns(() => numberFirst);

            userViewDto = new UserViewDto
            {
                Id = 1,
                FirstName = "Fedor",
                LastName = "Bondarchuk",
                Passport = "RT1234136",
                Email = "bondarchuk@gmail.com"
            };

            accountDto = new AccountDto
            {
                Id = 1,
                AccountType = AccountTypeDto.Base,
                Balance = new decimal(10),
                BenefitPoints = 10,
                IsClosed = false,
                NumberOfAccount = numberFirst,
                UserId = 1
            };

            accountDtoFirst = new AccountDto
            {
                Id = 1,
                AccountType = AccountTypeDto.Base,
                Balance = new decimal(5),
                BenefitPoints = 5,
                IsClosed = false,
                NumberOfAccount = numberFirst,
                UserId = 1
            };

            accountDtoSecond = new AccountDto
            {
                Id = 1,
                AccountType = AccountTypeDto.Base,
                Balance = new decimal(15),
                BenefitPoints = 15,
                IsClosed = false,
                NumberOfAccount = numberFirst,
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
            var service = new AccountService(mockAccount.Object, mockUserInfo.Object, mockNumber.Object);

            var resultAdd = service.OpenAccount("Base", userViewDto.Id);

            Assert.AreEqual("Base", resultAdd.AccountType);
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
            this.mockAccount.Setup(item => item.Update(It.IsAny<AccountDto>()))
                .Callback(() => HelperDeposit(accountDto, 100))
                .Returns(() => accountDto);

            var service = new AccountService(mockAccount.Object, mockUserInfo.Object, mockNumber.Object);

            var account = service.OpenAccount("Base", userViewDto.Id);

            var resultDeposite = service.DepositAccount(account.NumberOfAccount, 100);

            Assert.AreEqual(110m, resultDeposite.Balance);
            Assert.AreEqual(110, resultDeposite.BenefitPoints);
        }

        /// <summary>
        /// Test deposit to account with valid data
        /// </summary>
        [TestCase]
        public void WithDraw_Account_With_Valid_Data()
        {
            this.mockAccount.Setup(item => item.Update(It.IsAny<AccountDto>()))
                .Callback(() => HelperWithdraw(accountDto, 5))
                .Returns(() => accountDto);

            var service = new AccountService(mockAccount.Object, mockUserInfo.Object, mockNumber.Object);

            var account = service.OpenAccount("Base", userViewDto.Id);

            var resultWithDraw = service.WithDrawAccount(account.NumberOfAccount, 5);

            Assert.AreEqual(5m, resultWithDraw.Balance);
            Assert.AreEqual(5, resultWithDraw.BenefitPoints);
        }

        /// <summary>
        /// Test deposit to account with valid data
        /// </summary>
        [TestCase]
        public void Transfer_Account_With_Valid_Data()
        {
            this.mockAccount.SetupSequence(item => item.Update(It.IsAny<AccountDto>()))
                .Returns(accountDtoFirst)
                .Returns(accountDtoSecond);

            this.mockNumber.Setup(item => item.GetNumberAccount())
                .Returns(() => numberFirst);

            var service = new AccountService(mockAccount.Object, mockUserInfo.Object, mockNumber.Object);

            var accountFirst = service.OpenAccount("Base", userViewDto.Id);

            Assert.AreEqual(10m, accountFirst.Balance);
            Assert.AreEqual(10, accountFirst.BenefitPoints);

            this.mockNumber.Setup(item => item.GetNumberAccount())
                .Returns(() => numberSecond);

            var accountSecond = service.OpenAccount("Base", userViewDto.Id);

            Assert.AreEqual(10m, accountSecond.Balance);
            Assert.AreEqual(10, accountSecond.BenefitPoints);

            var resultTransfer = service.Transfer(accountFirst.NumberOfAccount, accountSecond.NumberOfAccount, 5);

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
            var accountDtoClose = new AccountDto
            {
                Id = 1,
                AccountType = AccountTypeDto.Base,
                Balance = new decimal(0),
                BenefitPoints = 0,
                IsClosed = true,
                NumberOfAccount = numberFirst,
                UserId = 1
            };

            var accountDtoWithdraw = new AccountDto
            {
                Id = 1,
                AccountType = AccountTypeDto.Base,
                Balance = new decimal(0),
                BenefitPoints = 0,
                IsClosed = false,
                NumberOfAccount = numberFirst,
                UserId = 1
            };

            this.mockAccount.SetupSequence(item => item.Update(It.IsAny<AccountDto>()))
                .Returns(accountDtoWithdraw)
                .Returns(accountDtoClose);
                
            var service = new AccountService(mockAccount.Object, mockUserInfo.Object, mockNumber.Object);

            var account = service.OpenAccount("Base", userViewDto.Id);

            var accountViewDto = service.WithDrawAccount(account.NumberOfAccount, 10);

            Assert.AreEqual(0m, accountViewDto.Balance);

            var result = service.Close(accountViewDto);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test close account if value balance more than zero
        /// </summary>
        [TestCase]
        public void Close_Account_If_Balance_More_Than_Zero()
        {
            this.mockAccount.Setup(item => item.Update(It.IsAny<AccountDto>()))
                .Returns(() => accountDto).Callback(() => accountDto.IsClosed = true);

            var service = new AccountService(mockAccount.Object, mockUserInfo.Object, mockNumber.Object);

            var account = service.OpenAccount("Base", userViewDto.Id);

            Assert.Throws<InvalidOperationException>(() => service.Close(account));
        }

        #endregion

        #region Helper

        /// <summary>
        /// Helper method for initialize accountDto instance after deposite
        /// </summary>
        /// <param name="dto">accountDto</param>
        /// <param name="valueDiposit">deposite value</param>
        private void HelperDeposit(AccountDto dto, int valueDiposit)
        {
            dto.Balance += valueDiposit;
            dto.BenefitPoints += valueDiposit;
        }

        /// <summary>
        /// Helper method for initialize accountDto instance after withdraw
        /// </summary>
        /// <param name="dto">accountDto</param>
        /// <param name="valueWithDraw">withDraw value</param>
        private void HelperWithdraw(AccountDto dto, int valueWithDraw)
        {
            dto.Balance -= valueWithDraw;
            dto.BenefitPoints -= valueWithDraw;
        }

        #endregion
    }
}
