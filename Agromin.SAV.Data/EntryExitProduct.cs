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
    
    public partial class EntryExitProduct
    {
        public int EntryExitProductId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<System.DateTime> Creation_Date { get; set; }
        public string StatusType { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> ProductBrandId { get; set; }
        public Nullable<int> SaleId { get; set; }
    
        public virtual ProductBrand ProductBrand { get; set; }
        public virtual Sale Sale { get; set; }
        public virtual User User { get; set; }
    }
}
