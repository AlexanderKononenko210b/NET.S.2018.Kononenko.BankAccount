//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Account
    {
        public int Id { get; set; }
        public int AccountType { get; set; }
        public string NumberOfAccount { get; set; }
        public decimal Balance { get; set; }
        public bool IsClosed { get; set; }
        public int BenefitPoints { get; set; }
        public int PersonalInfo { get; set; }
    
        public virtual Type Type { get; set; }
        public virtual User User { get; set; }
    }
}
