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
    
    public partial class PolicyBank
    {
        public int PolicyID { get; set; }
        public bool DebitOrder { get; set; }
        public string AccountName { get; set; }
        public Nullable<byte> AccountTypeID { get; set; }
        public Nullable<byte> BankID { get; set; }
        public string BranchNumber { get; set; }
        public string AccountNumber { get; set; }
        public string Status { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime Changed { get; set; }
        public string ChangedBy { get; set; }
    
        public virtual AccountType AccountType { get; set; }
        public virtual Bank Bank { get; set; }
        public virtual Policy Policy { get; set; }
    }
}
