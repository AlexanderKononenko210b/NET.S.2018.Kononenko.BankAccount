using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Models
{
    /// <summary>
    /// Model describe account in UI level
    /// </summary>
    [DisplayName("New Account")]
    public class AccountViewModel
    {
        #region Public Api

        /// <summary>
        /// Get and Set Id
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// Get and Set account type
        /// </summary>
        [DisplayName("Type")]
        public string AccountType { get; set; }

        /// <summary>
        /// Get and Set number bank account
        /// </summary>
        [DisplayName("Number")]
        public string NumberOfAccount { get; set; }

        /// <summary>
        /// Get and Set balance Account
        /// </summary>
        [DisplayName("Balance")]
        public string Balance { get; set; }

        /// <summary>
        /// Get and Set is delete
        /// </summary>
        [DisplayName("State")]
        //[Range(typeof(bool), "true", "true", ErrorMessage = "You must check for close account")]
        [Remote("CloseStateValid", "Validation")]
        public bool IsClosed { get; set; }

        /// <summary>
        /// Get and Set bonus points
        /// </summary>
        [DisplayName("Benefit points")]
        public string BenefitPoints { get; set; }

        /// <summary>
        /// Get and Set user info
        /// </summary>
        [DisplayName("User info")]
        public int UserId { get; set; }

        #endregion
    }
}