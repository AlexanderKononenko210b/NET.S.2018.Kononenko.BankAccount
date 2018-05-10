using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    /// <summary>
    /// Dto model account
    /// </summary>
    public class AccountDto
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
        /// Get or set personal info
        /// </summary>
        public  PersonalInfoDto PersonalInfo { get; set; } = new PersonalInfoDto();
    }
}
