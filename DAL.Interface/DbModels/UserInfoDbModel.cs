using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DbModels
{
    /// <summary>
    /// Class describe User account
    /// </summary>
    public class UserInfoDbModel : Entity, IEquatable<UserInfoDbModel>
    {
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
        /// Gets or sets the account Id
        /// </summary>
        /// <value>The account id</value>
        public int AccountId { get; set; }

        /// <summary>
        /// Navigation property to AccountDbModel
        /// </summary>
        public virtual ICollection<AccountDbModel> Accounts { get; set; }

        /// <summary>
        /// Method for equals two instances type UserInfoDbModel
        /// </summary>
        /// <param name="other">instance type UserInfoDbModel</param>
        /// <returns>result equals</returns>
        public bool Equals(UserInfoDbModel other)
        {
            if (ReferenceEquals(null, other)) return false;

            if (ReferenceEquals(this, other)) return true;

            if (this.FirstName != other.FirstName ||
                this.LastName != other.LastName ||
                this.Passport != other.Passport ||
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

            return Equals((UserInfoDbModel)obj);
        }

        /// <summary>
        /// Override GetHashCode for type UserInfoDbModel
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.FirstName.GetHashCode() + this.LastName.GetHashCode() +
                   this.Passport.GetHashCode() + this.Email.GetHashCode();
        }
    }
}
