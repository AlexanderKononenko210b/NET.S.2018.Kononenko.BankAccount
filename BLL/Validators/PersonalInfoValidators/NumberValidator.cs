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
    /// Class for validat number of passport
    /// </summary>
    public class NumberValidator : IStringValidator
    {
        /// <summary>
        /// Method for validate number of passport
        /// </summary>
        /// <param name="number">input number passport</param>
        /// <returns>tuple consist bool result check and string information</returns>
        public Tuple<bool, string> IsValid(string number)
        {
            string regex = @"[A-Z]{2}\d{7}";

            if (!Regex.IsMatch(number, regex))
                return Tuple.Create(false, $"{number} is not correct!");

            return Tuple.Create(true, $"{number} is valid");
        }
    }
}
