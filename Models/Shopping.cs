using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShoppingMall.Models
{
    public class ShippingDetail
    {
        public int ShippingDetailID { get; set; }
        [Required]
        public Nullable<int> MemberID { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public Nullable<int> OrderID { get; set; }
        public Nullable<decimal> AmountPaid { get; set; }
        [Required]
        public string PaymentType { get; set; }
    }
}