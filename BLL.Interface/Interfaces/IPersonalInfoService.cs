using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Interface.Interfaces
{
    /// <summary>
    /// Interface for operation with personal info
    /// </summary>
    public interface IPersonalInfoService
    {
        /// <summary>
        /// Create new PersonalInfo with verify input information
        /// </summary>
        /// <param name="firstName">first name</param>
        /// <param name="lastName">last name</param>
        /// <param name="passport">number passport</param>
        /// <param name="email">email</param>
        /// <param name="strategy">strategy verify every string input</param>
        /// <returns>new Tuple where PersonalInfo - new instance or null and string - message
        /// about result operation</returns>
        Tuple<PersonalInfo, string> Create(string firstName, string lastName,
            string passport, string email, IVerifyPersonalInfo<IStringValidator> strategy);
    }
}
