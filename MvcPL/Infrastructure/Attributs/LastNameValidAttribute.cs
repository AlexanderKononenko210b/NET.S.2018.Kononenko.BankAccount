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
    /// Attribute for check valid last name
    /// </summary>
    public class LastNameValidAttribute : RequiredAttribute
    {
        public LastNameValidAttribute()
        {
            ErrorMessage = Resources.LastNameIsNotValid;
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
                if (CheckLastName((string) value))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Method for validate last name
        /// </summary>
        /// <param name="lastName">input last name</param>
        /// <returns>value string for check if check valid</returns>
        private static bool CheckLastName(string lastName)
        {
            string regex = @"^[A-Z]{1}[a-z]{1,150}";

            if (!Regex.IsMatch(lastName, regex))
            {
                return true;
            }
            return false;
        }
    }
}