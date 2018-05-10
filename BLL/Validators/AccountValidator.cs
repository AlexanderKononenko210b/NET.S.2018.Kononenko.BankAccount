using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Validators
{
    /// <summary>
    /// Validator for check condition`s with action with account
    /// </summary>
    public static class AccountValidator
    {
        #region Consts

        private const decimal MAX_AMOUNT_DEPOSIT = 100000;

        private const decimal MIN_AMOUNT_STATE_BANK_ACCOUNT = 0;

        #endregion

        #region Public Api

        /// <summary>
        /// Validate when deposit money
        /// </summary>
        /// <param name="account">account for deposit money</param>
        /// <param name="deposit">deposit money</param>
        public static void DepositValidator(this Account account, decimal deposit)
        {
            if (account.IsClosed)
                throw new InvalidOperationException($"Account {nameof(account)}is closed");

            if (deposit <= 0)
                throw new ArgumentOutOfRangeException($"Deposit {nameof(deposit)} must to be more than 0");

            if (deposit > MAX_AMOUNT_DEPOSIT)
                throw new ArgumentOutOfRangeException($"Deposit summ {deposit} more than max value");
        }

        /// <summary>
        /// Validate when withdraw money
        /// </summary>
        /// <param name="account">account for withdraw money</param>
        /// <param name="withdraw">withdraw money</param>
        public static void WithDrawValidator(this Account account, decimal withdraw)
        {
            if (account.IsClosed)
                throw new InvalidOperationException($"Account is closed");

            if (withdraw <= 0)
                throw new ArgumentOutOfRangeException($"WithDraw {nameof(withdraw)} must to be more than 0");

            if (withdraw > account.Balance)
                throw new InvalidOperationException($"WithDraw amount {nameof(withdraw)} less than balance");

            if (account.Balance - withdraw < MIN_AMOUNT_STATE_BANK_ACCOUNT)
                throw new InvalidOperationException($"Remainder in Account after operation with {nameof(withdraw)} expected is not valid");
        }

        /// <summary>
        /// Validate when transfer money
        /// </summary>
        /// <param name="first">account for withdraw money</param>
        /// <param name="second">account for deposit money</param>
        /// <param name="transfer">transfer money</param>
        public static void TransferValidator(this Account first, Account second, decimal transfer)
        {
            if (first.IsClosed)
                throw new InvalidOperationException($"Account {nameof(first)} is closed");

            if (first.IsClosed)
                throw new InvalidOperationException($"Account {nameof(second)} is closed");

            if (transfer <= 0)
                throw new ArgumentOutOfRangeException($"Deposit {nameof(transfer)} must to be more than 0");

            if (transfer > first.Balance)
                throw new ArgumentOutOfRangeException($"WithDraw amount {nameof(transfer)} less than balance");

            if (first.Balance - transfer < MIN_AMOUNT_STATE_BANK_ACCOUNT)
                throw new InvalidOperationException($"Remainder in Account after operation with {nameof(transfer)} expected is not valid");

            if (transfer > MAX_AMOUNT_DEPOSIT)
                throw new InvalidOperationException($"Deposit summ {transfer} more than max value");
        }

        /// <summary>
        /// Validate operation close account
        /// </summary>
        /// <param name="account">account for close</param>
        public static void CloseValidator(this Account account)
        {
            if(account.Balance < 0)
                throw new InvalidOperationException($"Balance {nameof(account)} is less zero");

            if(account.Balance > 0)
                throw new InvalidOperationException($"Balance {nameof(account)} is more than zero");
        }

        #endregion
    }
}
