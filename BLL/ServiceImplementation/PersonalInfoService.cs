using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using BLL.Validators.PersonalInfoValidators;

namespace BLL.ServiceImplementation
{
    /// <summary>
    /// Service for create new instance PersonalInfo
    /// </summary>
    public class PersonalInfoService : IPersonalInfoService
    {
        /// <summary>
        /// Create new PersonalInfo with verify input information
        /// </summary>
        /// <param name="firstName">first name</param>
        /// <param name="lastName">last name</param>
        /// <param name="passport">number passport</param>
        /// <param name="email">email</param>
        /// <returns>new Tuple where PersonalInfo - new instance or null and string - message
        /// about result operation</returns>
        public Tuple<PersonalInfo, string> Create(string firstName, string lastName, string passport, string email,
            IVerifyPersonalInfo<IStringValidator> strategy)
        {
            var verifyFirstName = strategy.IsVerify(firstName, 
                new List<IStringValidator>{ new NullValidator(), new EmptyValidator(), new FirstNameValidator()});

            if (!verifyFirstName.Item1)
                return new Tuple<PersonalInfo, string>(null, verifyFirstName.Item2);

            var verifyLastName = strategy.IsVerify(lastName,
                new List<IStringValidator> { new NullValidator(), new EmptyValidator(), new LastNameValidator() });

            if (!verifyLastName.Item1)
                return new Tuple<PersonalInfo, string>(null, verifyLastName.Item2);

            var verifyPassport = strategy.IsVerify(passport,
                new List<IStringValidator> { new NullValidator(), new EmptyValidator(), new NumberValidator() });

            if (!verifyPassport.Item1)
                return new Tuple<PersonalInfo, string>(null, verifyPassport.Item2);

            var verifyEmail = strategy.IsVerify(email,
                new List<IStringValidator> { new NullValidator(), new EmptyValidator(), new EmailValidator() });

            if (!verifyEmail.Item1)
                return new Tuple<PersonalInfo, string>(null, verifyEmail.Item2);

            return new Tuple<PersonalInfo, string>(new PersonalInfo(firstName, lastName, passport, email),
                "Personal info create succesfully");
        }
    }
}
