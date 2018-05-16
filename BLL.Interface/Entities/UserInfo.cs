using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    /// <summary>
    /// Class describe user info
    /// </summary>
    public sealed class UserInfo : IEquatable<UserInfo>
    {

        #region Fields

        private int id;

        private string firstName;

        private string lastName;

        private string passport;

        private string email;

        #endregion

        #region Constructors

        public UserInfo() { }

        public UserInfo(string inputFirstName, string inputLastName, string passport, string email)
        {
            this.FirstName = inputFirstName;

            this.LastName = inputLastName;

            this.Passport = passport;

            this.Email = email;
        }

        #endregion

        #region Public Api

        /// <summary>
        /// Gets or sets the user id
        /// </summary>
        /// <value>The user id</value>
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

        /// <summary>
        /// Method for equals two instances type UserInfo
        /// </summary>
        /// <param name="other">instance type UserInfo</param>
        /// <returns>result equals</returns>
        public bool Equals(UserInfo other)
        {
            if (ReferenceEquals(null, other)) return false;

            if (ReferenceEquals(this, other)) return true;

            if (this.FirstName != other.FirstName ||
                this.LastName != other.LastName ||
                this.Passport != other.passport ||
                this.Email != other.Email)
                return false;

            return true;
        }

        /// <summary>
        /// Method for equals two instances type object
        /// </summary>
        /// <param name="obj">instance type object</param>
        /// <returns>result equals</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, this)) return false;

            if (ReferenceEquals(this, obj)) return true;

            if (this.GetType() != obj.GetType()) return false;

            return Equals((UserInfo) obj);
        }

        /// <summary>
        /// Override GetHashCode for type UserInfo
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.FirstName.GetHashCode() + this.LastName.GetHashCode() +
                   this.Passport.GetHashCode() + this.Email.GetHashCode();
        }

        #endregion
    }
}
