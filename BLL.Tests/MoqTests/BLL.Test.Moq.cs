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
using DAL.Interface.DTO;
using DAL.Interface.Interfaces;
using NUnit.Framework;
using Moq;

namespace BLL.Tests.MoqTests
{
    /// <summary>
    /// Moq test account
    /// </summary>
    [TestFixture]
    public class BllMoqTest
    {
        #region Fields

        private Mock<IRepository<AccountDto>> mockRepository;

        private string numberAccount = "40512100790000000001";

        private PersonalInfo personalInfo;

        private PersonalInfoDto personalInfoDto;

        private AccountDto accountDtoFirst;

        private AccountDto accountDtoSecond;

        private AccountDto accountDto;

        #endregion

        #region Initializes

        [SetUp]
        public void Initialize()
        {
            mockRepository = new Mock<IRepository<AccountDto>>();

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

            accountDtoFirst = new AccountDto
            {
                AccountType = AccountTypeDto.Base,
                Balance = new decimal(5),
                BenefitPoints = 1,
                IsClosed = false,
                NumberOfAccount = numberAccount,
                PersonalInfo = personalInfoDto
			};

            accountDtoSecond = new AccountDto
            {
                AccountType = AccountTypeDto.Base,
                Balance = new decimal(15),
                BenefitPoints = 1,
                IsClosed = false,
                NumberOfAccount = numberAccount,
                PersonalInfo = personalInfoDto
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
			this.mockRepository.Setup(item => item.Add(It.IsAny<AccountDto>()))
                .Returns(() => accountDto);

            var service = new AccountService(mockRepository.Object);

            var resultAdd = service.OpenAccount(AccountType.Base, personalInfo, new NumberCreateService());

            Assert.AreEqual(AccountType.Base, resultAdd.AccountType);
            Assert.AreEqual("40512100790000000001", resultAdd.NumberOfAccount);
			Assert.AreEqual(10m, resultAdd.Balance);
			Assert.AreEqual(10, resultAdd.BenefitPoints);
			Assert.AreEqual(false, resultAdd.IsClosed);
			Assert.AreEqual("Fedor", resultAdd.PersonalInfo.FirstName);
            Assert.AreEqual("Bondarchuk", resultAdd.PersonalInfo.LastName);
            Assert.AreEqual("RT1234136", resultAdd.PersonalInfo.Passport);
            Assert.AreEqual("bondarchuk@gmail.com", resultAdd.PersonalInfo.Email);
        }

        /// <summary>
        /// Test deposit to account with valid data
        /// </summary>
        [TestCase]
        public void Deposit_Account_With_Valid_Data()
        {
            this.mockRepository.Setup(item => item.Update(It.IsAny<AccountDto>()))
                .Returns(() => accountDto).Callback(() => accountDto.Balance += 100);

            var service = new AccountService(mockRepository.Object);

            var account = AccountFactory.Create(AccountType.Base, personalInfo, new NumberCreateService());

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
            this.mockRepository.Setup(item => item.Update(It.IsAny<AccountDto>()))
                .Returns(() => accountDto).Callback(() => accountDto.Balance -= 5);

            var service = new AccountService(mockRepository.Object);

            var account = AccountFactory.Create(AccountType.Base, personalInfo, new NumberCreateService());

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
            this.mockRepository.SetupSequence(item => item.Update(It.IsAny<AccountDto>()))
                .Returns(accountDtoFirst)
                .Returns(accountDtoSecond);

            var service = new AccountService(mockRepository.Object);

            var accountFirst = AccountFactory.Create(AccountType.Base, personalInfo, new NumberCreateService());

            Assert.AreEqual(10m, accountFirst.Balance);
            Assert.AreEqual(10, accountFirst.BenefitPoints);

            var accountSecond = AccountFactory.Create(AccountType.Base, personalInfo, new NumberCreateService());

            Assert.AreEqual(10m, accountSecond.Balance);
            Assert.AreEqual(10, accountSecond.BenefitPoints);

            var resultTransfer = service.Transfer(accountFirst, accountSecond, 5);
			
			Assert.IsTrue(resultTransfer);
            Assert.AreEqual(5m, accountFirst.Balance);
            Assert.AreEqual(5, accountFirst.BenefitPoints);
            Assert.AreEqual(15m, accountSecond.Balance);
            Assert.AreEqual(15, accountSecond.BenefitPoints);
        }

        /// <summary>
        /// Test close account with valid data
        /// </summary>
        [TestCase]
        public void Close_Account()
        {
            accountDto.IsClosed = true;

            accountDto.Balance = 0;

            this.mockRepository.Setup(item => item.Update(It.IsAny<AccountDto>()))
                .Returns(() => accountDto);

            var service = new AccountService(mockRepository.Object);

            var account = AccountFactory.Create(AccountType.Base, personalInfo, new NumberCreateService());

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
            this.mockRepository.Setup(item => item.Update(It.IsAny<AccountDto>()))
                .Returns(() => accountDto).Callback(() => accountDto.IsClosed = true);

            var service = new AccountService(mockRepository.Object);

            var account = AccountFactory.Create(AccountType.Base, personalInfo, new NumberCreateService());

            Assert.Throws<InvalidOperationException>(() => service.Close(account));
        }

        #endregion
    }
}
