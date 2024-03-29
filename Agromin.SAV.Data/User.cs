//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Agromin.SAV.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.EntryExitProduct = new HashSet<EntryExitProduct>();
            this.Sale = new HashSet<Sale>();
            this.SalePay = new HashSet<SalePay>();
        }
    
        public int UserId { get; set; }
        public string Names { get; set; }
        public string Last_Names { get; set; }
        public string Credential { get; set; }
        public string Password { get; set; }
        public string Sex { get; set; }
        public string Identity_Document { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public System.DateTime Creation_date { get; set; }
        public Nullable<System.DateTime> Update_date { get; set; }
        public string Status { get; set; }
        public string Master { get; set; }
        public int LocalId { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public string Phone { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual District District { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EntryExitProduct> EntryExitProduct { get; set; }
        public virtual Local Local { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sale { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalePay> SalePay { get; set; }
    }
}
