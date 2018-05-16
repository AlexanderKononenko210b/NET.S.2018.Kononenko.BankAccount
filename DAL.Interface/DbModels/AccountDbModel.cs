using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DbModels
{
    /// <summary>
    /// Class describe entity account in database
    /// </summary>
    public class AccountDbModel : Entity
    {
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
        /// Get account type
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Get or set user info
        /// </summary>
        public int UserId { get; set; }

        public virtual UserInfoDbModel User { get; set; }

        public virtual AccountTypeDbModel Type { get; set; }
    }
}
