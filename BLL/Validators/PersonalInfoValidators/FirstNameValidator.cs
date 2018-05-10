using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BLL.Interface.Interfaces;

namespace BLL.Validators.PersonalInfoValidators
{
    /// <summary>
    /// Validator for check first name
    /// </summary>
    public class FirstNameValidator : IStringValidator
    {
        /// <summary>
        /// Method for validate first name
        /// </summary>
        /// <param name="firstName">input first name</param>
        /// <returns>tuple consist bool result check and string information</returns>
        public Tuple<bool, string> IsValid(string firstName)
        {
            string regex = @"^[A-Z]{1}[a-z]{1,15}";

            if (!Regex.IsMatch(firstName, regex))
                return Tuple.Create(false, $"{firstName} is not correct! It has more than 1 and less or equal letters. Example Ivan");

            return Tuple.Create(true, $"{firstName} is valid");
        }
    }
}
