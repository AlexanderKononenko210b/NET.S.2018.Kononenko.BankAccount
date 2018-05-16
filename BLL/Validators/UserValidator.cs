using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Validators
{
    /// <summary>
    /// Validator information about user
    /// </summary>
    public static class UserValidator
    {
        /// <summary>
        /// Method for validate first name
        /// </summary>
        /// <param name="firstName">input first name</param>
        /// <returns>value string for check if check valid</returns>
        public static string CheckFirstName(string firstName)
        {
            string regex = @"^[A-Z]{1}[a-z]{1,15}";

            if (!Regex.IsMatch(firstName, regex))
                throw new ArgumentOutOfRangeException($"First name {firstName} is not correct! " +
                                                      $"It has more than 1 and less or equal letters. Example Ivan");

            return firstName;
        }

        /// <summary>
        /// Method for validate last name
        /// </summary>
        /// <param name="lastName">input last name</param>
        /// <returns>value string for check if check valid</returns>
        public static string CheckLastName(string lastName)
        {
            string regex = @"^[A-Z]{1}[a-z]{1,150}";

            if (!Regex.IsMatch(lastName, regex))
                throw new ArgumentOutOfRangeException($"{lastName} is not correct! It has more than 1 and less" +
                                                      $" or equal 150 letters. Example Ivanov");

            return lastName;
        }

        /// <summary>
        /// Method for validate number of passport
        /// </summary>
        /// <param name="numberPassport">input number passport</param>
        /// <returns>value string for check if check valid</returns>
        public static string CheckPassport(string numberPassport)
        {
            string regex = @"[A-Z]{2}\d{7}";

            if (!Regex.IsMatch(numberPassport, regex))
                throw new ArgumentOutOfRangeException($"{numberPassport} is not correct!");

            return numberPassport;
        }

        /// <summary>
        /// Method for validate email
        /// </summary>
        /// <param name="email">input email</param>
        /// <returns>tuple consist bool result check and string information</returns>
        public static (bool, string) CheckEmail(string email)
        {
            string regex = @"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$";

            if (!Regex.IsMatch(email, regex))
                return (false, $"{email} is not correct!");

            return (true, $"{email} is valid");
        }
    }
}
