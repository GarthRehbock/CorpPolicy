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
    
    public partial class Insurer
    {
        public Insurer()
        {
            this.Policies = new HashSet<Policy>();
            this.PolicyDebits = new HashSet<PolicyDebit>();
        }
    
        public short InsurerID { get; set; }
        public string InsurerName { get; set; }
        public bool Active { get; set; }
        public string Status { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime Changed { get; set; }
        public string ChangedBy { get; set; }
    
        public virtual ICollection<Policy> Policies { get; set; }
        public virtual ICollection<PolicyDebit> PolicyDebits { get; set; }
    }
}
