//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineShoppingMall.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_ShippingDetails
    {
        public int ShippingDetailID { get; set; }
        public Nullable<int> MemberID { get; set; }
        public string Address { get; set; }
        public Nullable<int> OrderID { get; set; }
        public Nullable<decimal> AmountPaid { get; set; }
        public string PaymentType { get; set; }
    
        public virtual Tbl_Members Tbl_Members { get; set; }
    }
}
