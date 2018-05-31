using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MvcPL.Infrastructure.Validators;
using MvcPL.Models;
using MvcPL.Properties;

namespace MvcPL.Infrastructure.Attributs
{
    /// <summary>
    /// Attribute check dublicate transfer accounts number
    /// </summary>
    public class CheckDuplicationNumberAttribute : ValidationAttribute
    {
        public CheckDuplicationNumberAttribute()
        {
            ErrorMessage = Resources.DublicatedNumberAccount;
        }

        public override bool IsValid(object value)
        {
            Check.NotNull(value);

            TransferViewModel transferModel = value as TransferViewModel;

            return !transferModel.FirstNumber.Equals(transferModel.SecondNumber, StringComparison.CurrentCulture);
        }
    }
}