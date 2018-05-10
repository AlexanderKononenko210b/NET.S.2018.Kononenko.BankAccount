using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Interfaces;

namespace BLL.Validators.PersonalInfoValidators
{
    /// <summary>
    /// Class for check empty input
    /// </summary>
    public class EmptyValidator : IStringValidator
    {
        /// <summary>
        /// Method for check empty input
        /// </summary>
        /// <param name="inputTypeString"></param>
        /// <returns>tuple consist bool result check and string information</returns>
        public Tuple<bool, string> IsValid(string inputTypeString)
        {
            if (inputTypeString == string.Empty)
                return Tuple.Create(false, $"{inputTypeString} is empty ");

            return Tuple.Create(true, $"{inputTypeString} is valid");
        }
    }
}
