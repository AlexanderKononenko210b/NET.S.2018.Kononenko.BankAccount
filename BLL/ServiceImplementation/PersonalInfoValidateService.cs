using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Interfaces;

namespace BLL.ServiceImplementation
{
    /// <summary>
    /// Service for validate input personal information
    /// </summary>
    public class PersonalInfoValidateService : IVerifyPersonalInfo<IStringValidator>
    {
        /// <summary>
        /// Method for validate input type string personal information
        /// </summary>
        /// <param name="inputInfo">input information</param>
        /// <param name="strategyValidate">enumerable rules for validate</param>
        /// <returns>tuple with information about result validate</returns>
        public Tuple<bool, string> IsVerify(string inputInfo, IEnumerable<IStringValidator> strategyValidate)
        {
            foreach (IStringValidator item in strategyValidate)
            {
                var result = item.IsValid(inputInfo);

                if (!result.Item1)
                    return Tuple.Create(false, result.Item2);
            }

            return Tuple.Create(true, $"Input value {nameof(inputInfo)} is valid");
        }
    }
}
