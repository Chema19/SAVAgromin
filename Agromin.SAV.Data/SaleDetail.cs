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
    
    public partial class SaleDetail
    {
        public int SaleDetailId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Status { get; set; }
        public Nullable<int> SaleId { get; set; }
        public Nullable<int> ProductBrandId { get; set; }
        public Nullable<decimal> Total { get; set; }
    
        public virtual ProductBrand ProductBrand { get; set; }
        public virtual Sale Sale { get; set; }
    }
}
