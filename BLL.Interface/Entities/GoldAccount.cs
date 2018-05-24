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
    /// Account type Gold
    /// </summary>
    public class GoldAccount : Account
    {
        #region Consts

        private const float COEFFICIENT_BENEFIT = 1.2f;

        #endregion

        #region Fields

        private readonly AccountType accountType = AccountType.Gold;

        #endregion

        #region Constructors

        internal GoldAccount() { }

        internal GoldAccount(AccountDto accountDto)
            : base(accountDto) { }

        internal GoldAccount(AccountViewDto accountViewDto)
            : base(accountViewDto) { }

        internal GoldAccount(int userId, IAccountNumberCreateService creator)
            : base(userId, creator) { }

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
        public bool Equals(GoldAccount other)
        {
            if (ReferenceEquals(null, other)) return false;

            if (ReferenceEquals(this, other)) return true;

            if (this.AccountType != other.AccountType || this.NumberOfAccount != other.NumberOfAccount
                || this.IsClosed != other.IsClosed || this.UserId != other.UserId
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

            return this.Equals((GoldAccount)obj);
        }

        /// <summary>
        /// Override GetHashCode because override Equals
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return this.AccountType.GetHashCode() + this.NumberOfAccount.GetHashCode()
                   + this.IsClosed.GetHashCode() + this.UserId.GetHashCode()
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
            if (deposit <= 0)
                throw new ArgumentOutOfRangeException($"Deposit {nameof(deposit)} must to be more than 0");

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

        #endregion
    }
}
