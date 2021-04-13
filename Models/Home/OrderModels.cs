using OnlineShoppingMall.DAL;
using OnlineShoppingMall.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using PagedList.Mvc;

namespace OnlineShoppingMall.Models.Home
{
    public class OrderModel
    {
        public int InvoiceID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int? Quantity { get; set; }
        public string ProductImage { get; set; }
        public decimal? Price { get; set; }
    }
}