using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    /// <summary>
    /// Model describe account in UI level
    /// </summary>
    public class AccountViewModel
    {
        #region Public Api

        /// <summary>
        /// Get and Set Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Get and Set account type
        /// </summary>
        public string AccountType { get; set; }

        /// <summary>
        /// Get and Set number bank account
        /// </summary>
        public string NumberOfAccount { get; set; }

        /// <summary>
        /// Get and Set balance Account
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Get and Set is delete
        /// </summary>
        public bool IsClosed { get; set; }

        /// <summary>
        /// Get and Set bonus points
        /// </summary>
        public int BenefitPoints { get; set; }

        /// <summary>
        /// Get and Set user info
        /// </summary>
        public int UserId { get; set; }

        #endregion
    }
}