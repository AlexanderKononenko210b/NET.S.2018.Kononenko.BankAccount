using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using MvcPL.Infrastructure.Attributs;

namespace MvcPL.Models
{
    public class CreateAccountViewModel
    {
        /// <summary>
        /// Get and Set account type
        /// </summary>
        [DisplayName("Type")]
        public string AccountType { get; set; }

        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        /// <value>The first name.</value>
        [DisplayName("First name")]
        [Required(ErrorMessage = "Please enter first name")]
        //[FirstNameValid]
        [Remote("FirstNameValid", "Validation")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        /// <value>The last name.</value>
        [DisplayName("Last name")]
        [Required(ErrorMessage = "Please enter last name")]
        //[LastNameValid]
        [Remote("LastNameValid", "Validation")]
        public string LastName { get; set; }


        /// <summary>
        /// Gets or sets the passport number
        /// </summary>
        /// <value>The phone.</value>
        [DisplayName("Passport number")]
        [Required(ErrorMessage = "Please enter passport number")]
        //[PassportNumberValid]
        [Remote("PassportValid", "Validation")]
        public string Passport { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        /// <value>The phone.</value>
        [DisplayName("Email")]
        [Required(ErrorMessage = "Please enter email")]
        //[EmailValid]
        [Remote("EmailValid", "Validation")]
        public string Email { get; set; }
    }
}