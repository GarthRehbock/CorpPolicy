//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CorpPolicy.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bank
    {
        public Bank()
        {
            this.PolicyBanks = new HashSet<PolicyBank>();
        }
    
        public byte BankID { get; set; }
        public string BankName { get; set; }
        public string Status { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime Changed { get; set; }
        public string ChangedBy { get; set; }
    
        public virtual ICollection<PolicyBank> PolicyBanks { get; set; }
    }
}