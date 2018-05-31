using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MvcPL.Properties;

namespace MvcPL.Infrastructure.Attributs
{
    /// <summary>
    /// Attribute for check valid email
    /// </summary>
    public class EmailValidAttribute : RequiredAttribute
    {
        public EmailValidAttribute()
        {
            ErrorMessage = Resources.EmailIsNotValid;
        }

        /// <summary>
        /// Override method IsValid
        /// </summary>
        /// <param name="value">input value</param>
        /// <returns>true if model valid</returns>
        public override bool IsValid(object value)
        {
            if (base.IsValid(value))
            {
                if (CheckEmail((string)value))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Method for validate email
        /// </summary>
        /// <param name="email">input email</param>
        /// <returns>tuple consist bool result check and string information</returns>
        private static bool CheckEmail(string email)
        {
            string regex = @"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$";

            if (!Regex.IsMatch(email, regex))
            {
                return true;
            }
            return false;
        }
    }
}