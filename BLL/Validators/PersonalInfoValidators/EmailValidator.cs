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
    /// Validator for check email
    /// </summary>
    public class EmailValidator : IStringValidator
    {
        /// <summary>
        /// Method for validate email
        /// </summary>
        /// <param name="email">input email</param>
        /// <returns>tuple consist bool result check and string information</returns>
        public Tuple<bool, string> IsValid(string email)
        {
            string regex = @"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$";

            if (!Regex.IsMatch(email, regex))
                return Tuple.Create(false, $"{email} is not correct!");

            return Tuple.Create(true, $"{email} is valid");
        }
    }
}
