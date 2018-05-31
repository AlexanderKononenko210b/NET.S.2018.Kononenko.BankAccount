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
    /// Attribute for check valid passport name
    /// </summary>
    public class PassportNumberValidAttribute : RequiredAttribute
    {
        public PassportNumberValidAttribute()
        {
            ErrorMessage = Resources.PassportNumberIsNotValid;
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
                if (CheckPassport((string)value))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Method for validate number of passport
        /// </summary>
        /// <param name="numberPassport">input number passport</param>
        /// <returns>value string for check if check valid</returns>
        private static bool CheckPassport(string numberPassport)
        {
            string regex = @"[A-Z]{2}\d{7}";

            if (!Regex.IsMatch(numberPassport, regex))
            {
                return true;
            }
            return false;
        }
    }
}