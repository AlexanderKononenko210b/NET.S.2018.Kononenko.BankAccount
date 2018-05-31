using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using MvcPL.Properties;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Validators
{
    public static class ExplicitValidators
    {
        #region Public Api

        public static void CreateAccountValidator(CreateAccountViewModel model, Controller controller)
        {
            if (NotNull(model.FirstName))
                controller.ModelState.AddModelError("FirstName", "Please enter first name");
            else if(EmptyString(model.FirstName))
                controller.ModelState.AddModelError("FirstName", $"First name {Resources.EmptyString}");
            else if(WhiteSpaceString(model.FirstName))
                controller.ModelState.AddModelError("FirstName", $"First name {Resources.WhiteSpaceString}");
            else if(CheckFirstName(model.FirstName))
                controller.ModelState.AddModelError("FirstName", $"{Resources.FirstNameIsNotValid}");

            if (NotNull(model.LastName))
                controller.ModelState.AddModelError("FirstName", "Please enter last name");
            else if (EmptyString(model.LastName))
                controller.ModelState.AddModelError("FirstName", $"Last name {Resources.EmptyString}");
            else if (WhiteSpaceString(model.LastName))
                controller.ModelState.AddModelError("LastName", $"Last name {Resources.WhiteSpaceString}");
            else if (CheckLastName(model.LastName))
                controller.ModelState.AddModelError("LastName", $"{Resources.LastNameIsNotValid}");

            if (NotNull(model.Passport))
                controller.ModelState.AddModelError("FirstName", "Please enter number passport");
            else if (EmptyString(model.Passport))
                controller.ModelState.AddModelError("FirstName", $"Passport number {Resources.EmptyString}");
            else if (WhiteSpaceString(model.Passport))
                controller.ModelState.AddModelError("Passport", $"Passport number {Resources.WhiteSpaceString}");
            else if (CheckPassport(model.Passport))
                controller.ModelState.AddModelError("Passport", $"{Resources.PassportNumberIsNotValid}");

            if (NotNull(model.Email))
                controller.ModelState.AddModelError("FirstName", "Please enter email");
            else if (EmptyString(model.Email))
                controller.ModelState.AddModelError("FirstName", $"Email {Resources.EmptyString}");
            else if (WhiteSpaceString(model.Email))
                controller.ModelState.AddModelError("Email", $"Email {Resources.WhiteSpaceString}");
            else if (CheckEmail(model.Email))
                controller.ModelState.AddModelError("Email", $"{Resources.EmailIsNotValid}");
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Check reference argument on null 
        /// </summary>
        /// <typeparam name="T">reference type</typeparam>
        /// <param name="value">value for check</param>
        /// <returns>value</returns>
        private static bool NotNull<T>(T value) where T : class
        {
            if (value == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check nullable argument on null 
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="value">value for check</param>
        /// <returns>value</returns>
        private static bool NotNull<T>(T? value) where T : struct
        {
            if (value == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check whitespace or empty string input
        /// </summary>
        /// <param name="value">value for check</param>
        /// <returns>value</returns>
        private static bool EmptyString(string value)
        {
            if (value == String.Empty)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check whitespace or empty string input
        /// </summary>
        /// <param name="value">value for check</param>
        /// <returns>value</returns>
        private static bool WhiteSpaceString(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method for validate first name
        /// </summary>
        /// <param name="firstName">input first name</param>
        /// <returns>value string for check if check valid</returns>
        private static bool CheckFirstName(string firstName)
        {
            string regex = @"^[A-Z]{1}[a-z]{1,15}";

            if (!Regex.IsMatch(firstName, regex))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method for validate last name
        /// </summary>
        /// <param name="lastName">input last name</param>
        /// <returns>value string for check if check valid</returns>
        private static bool CheckLastName(string lastName)
        {
            string regex = @"^[A-Z]{1}[a-z]{1,150}";

            if (!Regex.IsMatch(lastName, regex))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method for validate number of passport
        /// </summary>
        /// <param name="numberPassport">input number passport</param>
        /// <returns>value string for check if check valid</returns>
        private static bool CheckPassport(string numberPassport)
        {
            string regex = @"[A-Z]{2}\d{7}";

            if (!Regex.IsMatch(numberPassport, regex))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method for validate email
        /// </summary>
        /// <param name="email">input email</param>
        /// <returns>tuple consist bool result check and string information</returns>
        private static bool CheckEmail(string email)
        {
            string regex = @"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$";

            if (!Regex.IsMatch(email, regex))
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}