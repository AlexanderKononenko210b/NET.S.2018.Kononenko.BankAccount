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
    /// Attribute for check valid first name
    /// </summary>
    public class FirstNameValidAttribute : RequiredAttribute
    {
        public FirstNameValidAttribute()
        {
            ErrorMessage = Resources.FirstNameIsNotValid;
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
                if (CheckFirstName((string) value))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Method for validate first name
        /// </summary>
        /// <param name="firstName">input first name</param>
        /// <returns>value string for check if check valid</returns>
        private static bool CheckFirstName(string firstName)
        {
            string regex = @"^[A-Z]{1}[a-z]{1,15}";

            if (!Regex.IsMatch(firstName, regex))
            {
                return true;
            }
            return false;
        }
    }
}