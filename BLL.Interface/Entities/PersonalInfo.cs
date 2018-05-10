using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    /// <summary>
    /// Class describe personal info
    /// </summary>
    public sealed class PersonalInfo
    {

        #region Fields

        private string firstName;

        private string lastName;

        private string passport;

        private string email;

        #endregion

        #region Constructors

        public PersonalInfo() { }

        public PersonalInfo(string inputFirstName, string inputLastName, string passport, string email)
        {
            this.FirstName = inputFirstName;

            this.LastName = inputLastName;

            this.Passport = passport;

            this.Email = email;
        }

        #endregion

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
