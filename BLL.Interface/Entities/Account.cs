using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Enum;
using BLL.Interface.Interfaces;
using DAL.Interface.DTO;

namespace BLL.Interface.Entities
{
    /// <summary>
    /// Abstract class Account
    /// </summary>
    public abstract class Account
    {
        #region Consts

        private const decimal MIN_AMOUNT_WITHDRAW = 10;

        private const int START_BENEFIT = 10;

        #endregion Consts

        #region Fields

        private string numberOfAccount;

        private PersonalInfo personalInfo = new PersonalInfo();

        private decimal balance = MIN_AMOUNT_WITHDRAW;

        private bool isClosed;

        private int benefitPoints = START_BENEFIT;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        internal Account() { }

        /// <summary>
        /// Create instance account
        /// </summary>
        internal Account(PersonalInfo personalInfo, IAccountNumberCreateService creator)
        {
            this.personalInfo = personalInfo;

            this.numberOfAccount = creator.GetNumberAccount();
        }

        /// <summary>
        /// Create instance type account from accountDto
        /// </summary>
        /// <param name="accountDto"></param>
        internal Account(AccountDto accountDto)
        {
            this.NumberOfAccount = accountDto.NumberOfAccount;

            this.Balance = accountDto.Balance;

            this.BenefitPoints = accountDto.BenefitPoints;

            this.IsClosed = accountDto.IsClosed;

            if(accountDto.PersonalInfo != null)
                this.PersonalInfo = new PersonalInfo();

            this.PersonalInfo.FirstName = accountDto.PersonalInfo.FirstName;

            this.PersonalInfo.LastName = accountDto.PersonalInfo.LastName;

            this.PersonalInfo.Email = accountDto.PersonalInfo.Email;

            this.PersonalInfo.Passport = accountDto.PersonalInfo.Passport;
        }

        #endregion Constructors

        #region Public Api

        /// <summary>
        /// Get number bank account
        /// </summary>
        public string NumberOfAccount
        {
            get => numberOfAccount;

            protected set => numberOfAccount = value;
        }

        /// <summary>
        /// Get identificator owner Account
        /// </summary>
        public PersonalInfo PersonalInfo
        {
            get => personalInfo;

            protected set => personalInfo = value;
        }

        /// <summary>
        /// Get balance Account
        /// </summary>
        public decimal Balance
        {
            get => balance;

            protected set => balance = value;
        }

        /// <summary>
        /// Get or set is delete
        /// </summary>
        public bool IsClosed
        {
            get => isClosed;

            protected set => isClosed = value;
        }

        /// <summary>
        /// Get or set bonus points
        /// </summary>
        public int BenefitPoints
        {
            get => benefitPoints;

            protected set => benefitPoints = value;
        }

        /// <summary>
        /// Get account type
        /// </summary>
        public abstract AccountType AccountType { get; }

        /// <summary>
        /// Public method for deposit money
        /// </summary>
        /// <param name="deposit">deposit`s value</param>
        /// <returns>Account</returns>
        internal decimal Deposit(decimal deposit)
        {
            return DepositMoney(deposit);
        }

        internal decimal WithDraw(decimal withdraw)
        {
            return WithDrawMoney(withdraw);
        }

        internal bool Close()
        {
            return CloseAccount();
        }

        /// <summary>
        /// Override virtual method ToString
        /// </summary>
        /// <returns>instance in string representation</returns>
        public override string ToString()
        {
            return $"Number account: {this.NumberOfAccount}, Balanse: {this.Balance}"+
            $" Owner: {this.PersonalInfo.FirstName}" +
            $" {this.PersonalInfo.LastName}, Passport: {this.PersonalInfo.Passport}, Email: {this.PersonalInfo.Email}";
        }

        #endregion

        #region Abstract methods

        /// <summary>
        /// Deposit money in Account
        /// </summary>
        /// <param name="deposit">deposit value</param>
        /// <returns>new balance</returns>
        protected abstract decimal DepositMoney(decimal deposit);

        /// <summary>
        /// WithDraw money in Account
        /// </summary>
        /// <param name="withdraw">withdraw value</param>
        /// <returns>new balance</returns>
        protected abstract decimal WithDrawMoney(decimal withdraw);

        /// <summary>
        /// Mark account as close
        /// </summary>
        /// <returns>return state account</returns>
        protected abstract bool CloseAccount();

        /// <summary>
        /// Calculate benefit deposit
        /// </summary>
        /// <param name="deposit">deposit value</param>
        /// <returns>new benefit value</returns>
        protected abstract int CalculateBenefitDeposit(decimal deposit);

        /// <summary>
        /// Calculate benefit withdraw
        /// </summary>
        /// <param name="withdraw">withdraw value</param>
        /// <returns>new benefit value</returns>
        protected abstract int CalculateBenefitWithDraw(decimal withdraw);

        #endregion
    }
}
