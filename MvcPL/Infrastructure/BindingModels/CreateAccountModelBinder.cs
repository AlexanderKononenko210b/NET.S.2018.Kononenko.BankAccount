using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPL.Models;

namespace MvcPL.Infrastructure.BindingModels
{
    public class CreateAccountModelBinder : IModelBinder
    {
        /// <summary>
        /// Bind model CreateAccountViewModel
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var createAccount = new CreateAccountViewModel();

            createAccount.AccountType = FromPostedData<string>(bindingContext, "Type");

            createAccount.FirstName = FromPostedData<string>(bindingContext, "FirstName");

            createAccount.LastName = FromPostedData<string>(bindingContext, "LastName");

            createAccount.Passport = FromPostedData<string>(bindingContext, "Passport");

            createAccount.Email = FromPostedData<string>(bindingContext, "Email");

            return createAccount;
        }

        /// <summary>
        /// Get value using provider
        /// </summary>
        /// <typeparam name="T">type value</typeparam>
        /// <param name="bindingContext">ModelBindingContext</param>
        /// <param name="prefix">name value</param>
        /// <returns>value</returns>
        private T FromPostedData<T>(ModelBindingContext bindingContext, string prefix)
        {
            var provider = bindingContext.ValueProvider;

            var valueResult = provider.GetValue(prefix);

            return (T) valueResult.ConvertTo(typeof(T));
        }
    }
}