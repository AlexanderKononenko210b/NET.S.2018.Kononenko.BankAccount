using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Dto;
using BLL.Interface.Enum;
using BLL.Interface.Interfaces;
using DAL.Interface.Dto;

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

        private int id;

        private string numberOfAccount;

        private int userId;

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
        internal Account(int userId, IAccountNumberCreateService creator)
        {
            this.userId = userId;

            this.numberOfAccount = creator.GetNumberAccount();
        }

        /// <summary>
        /// Create instance type account from accountDto
        /// </summary>
        /// <param name="accountDto"></param>
        internal Account(AccountDto accountDto)
        {
            this.Id = accountDto.Id;

            this.NumberOfAccount = accountDto.NumberOfAccount;

            this.Balance = accountDto.Balance;

            this.BenefitPoints = accountDto.BenefitPoints;

            this.IsClosed = accountDto.IsClosed;

            this.UserId = accountDto.UserId;
        }

        /// <summary>
        /// Create instance type account from accountViewDto
        /// </summary>
        /// <param name="accountViewDto"></param>
        internal Account(AccountViewDto accountViewDto)
        {
            this.Id = accountViewDto.Id;

            this.NumberOfAccount = accountViewDto.NumberOfAccount;

            this.Balance = accountViewDto.Balance;

            this.BenefitPoints = accountViewDto.BenefitPoints;

            this.IsClosed = accountViewDto.IsClosed;

            this.UserId = accountViewDto.UserId;
        }

        #endregion

        #region Public Api

        /// <summary>
        /// Get id account
        /// </summary>
        public int Id
        {
            get => id;

            protected set => id = value;
        }

        /// <summary>
        /// Get number bank account
        /// </summary>
        public string NumberOfAccount
        {
            get => numberOfAccount;

            protected set => numberOfAccount = value;
        }

        /// <summary>
        /// Get identificator User Account
        /// </summary>
        public int UserId
        {
            get => userId;

            protected set => userId = value;
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
        /// <returns>deposit`s value</returns>
        public decimal Deposit(decimal deposit)
        {
            this.Balance += deposit;

            CalculateBenefitDeposit(deposit);

            return this.Balance;
        }

        /// <summary>
        /// Public method for withdraw money
        /// </summary>
        /// <param name="withdraw">withdraw`s value</param>
        /// <returns>withdraw`s value</returns>
        public decimal WithDraw(decimal withdraw)
        {
            this.Balance -= withdraw;

            CalculateBenefitWithDraw(withdraw);

            return this.Balance;
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
            return
                $"Number account: {this.NumberOfAccount}, Balanse: {this.Balance}, BenefitPoints: {this.BenefitPoints}" +
                $" IsClosed : {this.IsClosed}";
        }

        #endregion

        #region Abstract methods

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
