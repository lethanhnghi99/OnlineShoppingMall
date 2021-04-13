using OnlineShoppingMall.DAL;
using OnlineShoppingMall.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingMall.Controllers
{
    public class HomeController : Controller
    {
        dbQuanlycuahangdienmayEntities1 ctx = new dbQuanlycuahangdienmayEntities1();
        public ActionResult Index(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search, 4, page));
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult CheckoutDetails()
        {
            return View();
        }

        public ActionResult AddToCart(int productId, string url)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = ctx.Tbl_Product.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var count = cart.Count();
                var product = ctx.Tbl_Product.Find(productId);
                for (int i = 0; i < count; i++)
                {
                    if (cart[i].Product.ProductID == productId)
                    {
                        int prevQty = cart[i].Quantity;
                        cart.Remove(cart[i]);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        var prd = cart.Where(x => x.Product.ProductID == productId).SingleOrDefault();
                        if (prd == null)
                        {
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = 1
                            });
                        }
                    }
                }

                Session["cart"] = cart;
            }
            return Redirect(url);
        }

        public ActionResult RemoveFromCart(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            //var product = ctx.Tbl_Product.Find(productId);
            foreach(var item in cart)
            {
                if(item.Product.ProductID==productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            
            Session["cart"] = cart;
            return Redirect("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Complete(Tbl_Invoice invoice)

        {
            var total = 0;
            List<Item> cart = (List<Item>)Session["cart"];
            invoice.Date = DateTime.Now;
            ctx.Tbl_Invoice.Add(invoice);
            //ctx.SaveChanges();

            foreach (var item in cart)
            {
                var detail = new Tbl_InvoiceDetail();
                detail.ProductID = item.Product.ProductID;
                detail.Quantity = item.Quantity;
                detail.InvoiceID = invoice.InvoiceID;
                ctx.Tbl_InvoiceDetail.Add(detail);
                ctx.SaveChanges();
                var totalitem = item.Quantity * Convert.ToInt32(item.Product.Price);
                total = totalitem +total;
            }
            //var inv = ctx.Tbl_Invoice.Find(invoice.InvoiceID);
            invoice.Total = total;
            ctx.SaveChanges();

            return View();
        }
    }
}