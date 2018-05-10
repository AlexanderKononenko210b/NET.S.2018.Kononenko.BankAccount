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
    /// Account type Silver
    /// </summary>
    public class SilverAccount : Account
    {
        #region Consts

        private const float COEFFICIENT_BENEFIT = 1.1f;

        #endregion

        #region Fields

        private readonly AccountType accountType = AccountType.Silver;

        #endregion

        #region Constructors

        internal SilverAccount() { }

        internal SilverAccount(AccountDto accountDto)
            : base(accountDto) { }

        internal SilverAccount(PersonalInfo info, IAccountNumberCreateService creator)
            : base(info, creator) { }

        #endregion

        #region Public Api

        /// <summary>
        /// Get or set type bank account
        /// </summary>
        public override AccountType AccountType => accountType;

        /// <summary>
        /// Implement Equals for object`s type BaseAccount
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(SilverAccount other)
        {
            if (ReferenceEquals(null, other)) return false;

            if (ReferenceEquals(this, other)) return true;

            if (this.AccountType != other.AccountType || this.NumberOfAccount != other.NumberOfAccount
                || this.IsClosed != other.IsClosed || this.PersonalInfo != other.PersonalInfo
                || this.Balance != other.Balance || this.BenefitPoints != other.BenefitPoints)
                return false;

            return true;
        }

        /// <summary>
        /// Override Equals inheritance by object
        /// </summary>
        /// <param name="obj">object for equals operation</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;

            if (ReferenceEquals(this, obj)) return true;

            if (this.GetType() != obj.GetType()) return false;

            return this.Equals((SilverAccount)obj);
        }

        /// <summary>
        /// Override GetHashCode because override Equals
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return this.AccountType.GetHashCode() + this.NumberOfAccount.GetHashCode()
                   + this.IsClosed.GetHashCode() + this.PersonalInfo.GetHashCode()
                   + this.Balance.GetHashCode() + this.BenefitPoints.GetHashCode();
        }

        /// <summary>
        /// Override virtual method ToString
        /// </summary>
        /// <returns>instance in string representation</returns>
        public override string ToString()
        {
            return $"Type account: {this.AccountType} {base.ToString()}";
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Calculate benefit deposit
        /// </summary>
        /// <param name="deposit">deposit value</param>
        /// <returns>new benefit value</returns>
        protected override int CalculateBenefitDeposit(decimal deposit)
        {
            this.BenefitPoints += (int)(deposit * new decimal(COEFFICIENT_BENEFIT));

            return this.BenefitPoints;
        }

        /// <summary>
        /// Calculate benefit withdraw
        /// </summary>
        /// <param name="withdraw">withdraw value</param>
        /// <returns>new benefit value</returns>
        protected override int CalculateBenefitWithDraw(decimal withdraw)
        {
            this.BenefitPoints -= (int)(withdraw / new decimal(COEFFICIENT_BENEFIT));

            return this.BenefitPoints >= 0 ? this.BenefitPoints : 0;
        }

        /// <summary>
        /// Mark account as close
        /// </summary>
        /// <returns>return state account</returns>
        protected override bool CloseAccount()
        {
            this.IsClosed = true;

            return this.IsClosed;
        }

        /// <summary>
        /// Deposit money in Account
        /// </summary>
        /// <param name="deposit">deposit value</param>
        /// <returns>new balance</returns>
        protected override decimal DepositMoney(decimal deposit)
        {
            this.Balance += deposit;

            this.BenefitPoints = CalculateBenefitDeposit(deposit);

            return this.Balance;
        }

        /// <summary>
        /// WithDraw money in Account
        /// </summary>
        /// <param name="withdraw">withdraw value</param>
        /// <returns>new balance</returns>
        protected override decimal WithDrawMoney(decimal withdraw)
        {
            this.Balance -= withdraw;

            this.BenefitPoints -= CalculateBenefitWithDraw(withdraw);

            return this.Balance;
        }

        #endregion
    }
}
