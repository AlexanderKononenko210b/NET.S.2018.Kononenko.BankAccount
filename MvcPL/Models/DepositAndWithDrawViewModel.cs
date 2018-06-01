using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Models
{
    /// <summary>
    /// Model for view deposit and withdraw operation
    /// </summary>
    public class DepositAndWithDrawViewModel
    {
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Please enter value")]
        [Remote("DecimalValueValid", "Validation")]
        public string Value { get; set; }
    }
}