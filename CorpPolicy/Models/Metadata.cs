using System;
using System.ComponentModel.DataAnnotations;

namespace CorpPolicy.Models
{
    public class ClientMetadata
    {
        [StringLength(60)]
        public string Name;
        [StringLength(5)]
        public string Initials;
        [StringLength(15)]
        public string Title;
        [StringLength(30)]
        public string Salutation;
        [StringLength(10)]
        public string VatNo;
        [StringLength(30)]
        public string Address;
        [StringLength(30)]
        public string Address1;
        [StringLength(30)]
        public string Address2;
        [StringLength(30)]
        public string Address3;
        [StringLength(4)]
        public string PostalCode;
        [StringLength(1)]
        public string Language;
        [StringLength(30)]
        public string IdentityNo;
        [StringLength(15)]
        public string BusinessPhone;
        [StringLength(15)]
        public string HomePhone;
        [StringLength(15)]
        public string Fax;
        [StringLength(15)]
        public string CellPhone;
        [StringLength(30)]
        public string Contact;
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> CancelledDate;
    }
}