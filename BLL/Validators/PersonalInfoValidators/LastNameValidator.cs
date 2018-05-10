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
    /// Validator for check last name
    /// </summary>
    public class LastNameValidator : IStringValidator
    {
        /// <summary>
        /// Method for validate last name
        /// </summary>
        /// <param name="lastName">input last name</param>
        /// <returns>tuple consist bool result check and string information</returns>
        public Tuple<bool, string> IsValid(string lastName)
        {
            string regex = @"^[A-Z]{1}[a-z]{1,150}";

            if (!Regex.IsMatch(lastName, regex))
                return Tuple.Create(false, $"{lastName} is not correct! It has more than 1 and less or equal 150 letters. Example Ivanov");

            return Tuple.Create(true, $"{lastName} is valid");
        }
    }
}
