using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    /// <summary>
    /// Model describe user in UI level
    /// </summary>
    public class UserViewModel
    {
        #region Public Api

        /// <summary>
        /// Get and Set Id
        /// </summary>
        public int Id { get; set; }

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