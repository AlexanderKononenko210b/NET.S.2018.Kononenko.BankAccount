using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MvcPL.Infrastructure.Attributs;

namespace MvcPL.Models
{
    /// <summary>
    /// Model for information about transfer
    /// </summary>
    [CheckDuplicationNumber]
    public class TransferViewModel
    {
        public string FirstNumber { get; set; }

        public string SecondNumber { get; set; }

        [Required(ErrorMessage = "Please enter transfer value")]
        [Remote("DecimalValueValid", "Validation")]
        public string Value { get; set; }
    }
}