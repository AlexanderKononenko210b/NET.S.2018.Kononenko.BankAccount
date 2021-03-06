﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DbModels;

namespace DAL.Interface.Dto
{
    /// <summary>
    /// Dto model account
    /// </summary>
    public class AccountDto : Entity
    {
        /// <summary>
        /// Get account type
        /// </summary>
        public AccountTypeDto AccountType { get; set; }

        /// <summary>
        /// Get number bank account
        /// </summary>
        public string NumberOfAccount { get; set; }

        /// <summary>
        /// Get balance Account
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Get or set is delete
        /// </summary>
        public bool IsClosed { get; set; }

        /// <summary>
        /// Get or set bonus points
        /// </summary>
        public int BenefitPoints { get; set; }

        /// <summary>
        /// Get or set user info
        /// </summary>
        public int UserId { get; set; }
    }
}
