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
    
    public partial class AccountType
    {
        public AccountType()
        {
            this.PolicyBanks = new HashSet<PolicyBank>();
        }
    
        public byte AccountTypeID { get; set; }
        public string AccountTypeName { get; set; }
        public string Status { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime Changed { get; set; }
        public string ChangedBy { get; set; }
    
        public virtual ICollection<PolicyBank> PolicyBanks { get; set; }
    }
}
