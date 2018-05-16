using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DbModels
{
    /// <summary>
    /// Class describe account`s type
    /// </summary>
    public class AccountTypeDbModel : Entity
    {
        public string Type { get; set; }

        /// <summary>
        /// Navigation property to AccountDbModel
        /// </summary>
        public virtual ICollection<AccountDbModel> Accounts { get; set; }
    }
}
