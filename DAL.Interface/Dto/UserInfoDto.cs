using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL.Interface.DbModels;

namespace DAL.Interface.Dto
{
    /// <summary>
    /// Class describe user info
    /// </summary>
    public sealed class UserInfoDto : Entity
    {
        #region Public Api

        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }


        /// <summary>
        /// Gets or sets the passport number
        /// </summary>
        /// <value>The phone.</value>
        public string Passport { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        /// <value>The phone.</value>
        public string Email { get; set; }

        #endregion
    }
}
