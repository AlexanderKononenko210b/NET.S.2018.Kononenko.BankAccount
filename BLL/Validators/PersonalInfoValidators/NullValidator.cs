using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Interfaces;

namespace BLL.Validators.PersonalInfoValidators
{
    /// <summary>
    /// Validator for check null input
    /// </summary>
    public class NullValidator : IStringValidator
    {
        /// <summary>
        /// Method for check null input
        /// </summary>
        /// <param name="inputTypeString">input type string</param>
        /// <returns>tuple consist bool result check and string information</returns>
        public Tuple<bool, string> IsValid(string inputTypeString)
        {
            if (inputTypeString == null)
                return Tuple.Create(false, $"Argument {inputTypeString} is null");

            return Tuple.Create(true, $"{inputTypeString} is valid");
        }
    }
}
