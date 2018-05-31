using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using MvcPL.Properties;

namespace MvcPL.Controllers
{
    public class ValidationController : Controller
    {
        #region Controllers

        /// <summary>
        /// Validation first name
        /// </summary>
        /// <param name="firstName">first name in string type</param>
        /// <returns>json result</returns>
        public JsonResult FirstNameValid(string firstName)
        {
            if (CheckFirstName(firstName))
                return Json($"{Resources.FirstNameIsNotValid}", JsonRequestBehavior.AllowGet);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Validation last name
        /// </summary>
        /// <param name="lastName">last name in string type</param>
        /// <returns>json result</returns>
        public JsonResult LastNameValid(string lastName)
        {
            if (CheckLastName(lastName))
                return Json($"{Resources.LastNameIsNotValid}", JsonRequestBehavior.AllowGet);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Validation passport number
        /// </summary>
        /// <param name="passport">passport number in string type</param>
        /// <returns>json result</returns>
        public JsonResult PassportValid(string passport)
        {
            if (CheckPassport(passport))
                return Json($"{Resources.PassportNumberIsNotValid}", JsonRequestBehavior.AllowGet);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Validation email
        /// </summary>
        /// <param name="email">email in string type</param>
        /// <returns>json result</returns>
        public JsonResult EmailValid(string email)
        {
            if (CheckEmail(email))
                return Json($"{Resources.PassportNumberIsNotValid}", JsonRequestBehavior.AllowGet);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Validation value as Decimal type
        /// </summary>
        /// <param name="transfer">input value</param>
        /// <returns>json result</returns>
        public JsonResult DecimalValueValid(string transfer)
        {
            Decimal value;

            if (!Decimal.TryParse(transfer, out value))
            {
                return Json($"{Resources.InvalidInputValue}", JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Private methods

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