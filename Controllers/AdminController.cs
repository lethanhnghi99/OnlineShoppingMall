using Newtonsoft.Json;
using OnlineShoppingMall.DAL;
using OnlineShoppingMall.Models;
using OnlineShoppingMall.Models.Home;
using OnlineShoppingMall.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingMall.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Index()
        {
            if (HttpContext.Request.Cookies["userName"] != null)
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("userName");
                ViewBag.UserID = cookie.Value;
            }
            else
            {
                ViewBag.UserID = "chua dang nhap";

            }

            return Redirect("/Home/Index"); ;
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Tbl_Login model, string returnUrl)
        {
            dbQuanlycuahangdienmayEntities1 db = new dbQuanlycuahangdienmayEntities1();
            //var dataItem = db.Tbl_Login.Where(x=>x.Username == model.Username && x.Password).First();
            var dataItem = db.Tbl_Login.Where(x => x.Username == model.Username && x.Password == model.Password).SingleOrDefault();
            if (dataItem != null)
            {
                if (dataItem.Role == "user")
                {
                    HttpCookie cookie = new HttpCookie("userName", dataItem.Username);
                    //cookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookie);
                    return Redirect("/");
                }
                else
                {
                    HttpCookie cookie = new HttpCookie("userName", dataItem.Username);
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookie);
                    return Redirect("/Admin/Dashboard");
                }

                //FormsAuthentication.SetAuthCookie(dataItem.Username, false);
                //if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                //    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("//"))
                //{
                //    return Redirect(returnUrl);
                //}
                //else
                //{
                //    return Redirect("/Admin/Dashboard");
                //}

            }
            else
            {
                ModelState.AddModelError("", "Invalid user/pass");
                return View();
            }
        }
        public ActionResult Logout()
        {

            //HttpCookie cookie = new HttpCookie("userName");

            //HttpContext.Response.Cookies.Remove("userName");
            //cookie.Value = null;
            //HttpContext.Response.SetCookie(cookie);

            if (Request.Cookies["userName"] != null)
            {
                var c = new HttpCookie("userName");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            return View("Login");
        }
    }
    public class AdminController : AuthController
    {
        // GET: Admin

        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CategoryID.ToString(), Text = item.CategoryName });
            }
            return list;
        }
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Categories()
        {
            List<Tbl_Category> allcategories = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(allcategories);
        }

        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Tbl_Category tbl)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_Category>().Add(tbl);
            return RedirectToAction("Categories");
        }

        public ActionResult UpdateCategory(int categoryId)
        {
            CategoryDetail cd;
            if(categoryId != null)
            {
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(categoryId)));
            }
            else
            {
                cd = new CategoryDetail();
            }
            return View("UpdateCategory", cd);
        }
        public ActionResult CategoryEdit(int catId)
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(catId));
        }
        [HttpPost]
        public ActionResult CategoryEdit(Tbl_Category tbl)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_Category>().Update(tbl);
            return RedirectToAction("Categories");
        }

        public ActionResult Product()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetProduct());
        }

        public ActionResult ProductEdit(int productId)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstorDefault(productId));
        }
        [HttpPost]
        public ActionResult ProductEdit(Tbl_Product tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                //file is uploaded
                file.SaveAs(path);
            }
            tbl.ProductImage = file != null ? pic : tbl.ProductImage;
            tbl.ModifiedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Product>().Update(tbl);
            return RedirectToAction("Product");
        }

        public ActionResult ProductAdd()
        {
            ViewBag.CategoryList = GetCategory();
            return View();
        }

        [HttpPost]
        public ActionResult ProductAdd(Tbl_Product tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if(file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                //file is uploaded
                file.SaveAs(path);
            }
            tbl.ProductImage = pic;
            tbl.CreateDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Product>().Add(tbl);
            return RedirectToAction("Product");
        }
        [HttpGet]
        public ActionResult Orders()
        {
            dbQuanlycuahangdienmayEntities1 db = new dbQuanlycuahangdienmayEntities1();
            //List<Tbl_Invoice> allinvoice = _unitOfWork.GetRepositoryInstance<Tbl_Invoice>().GetAllRecordsIQueryable().ToList();
            var order = db.Tbl_Invoice.ToList();
            return View(order);
        }
        [HttpGet]
        public ActionResult OrderDetail(int id)
        {
            dbQuanlycuahangdienmayEntities1 db = new dbQuanlycuahangdienmayEntities1();
            //var orderdetail = db.Tbl_InvoiceDetail.Where(x => x.InvoiceID == id).ToList();
            //var orderdetail = db.Tbl_InvoiceDetail.Include(x => x.)
            var orderModels = from a in db.Tbl_InvoiceDetail
                              join b in db.Tbl_Product
                              on a.ProductID equals b.ProductID
                              select new OrderModel()
                              {
                                  ProductID = b.ProductID,
                                  InvoiceID = a.InvoiceID,
                                  Price = b.Price,
                                  ProductImage = b.ProductImage,
                                  ProductName = b.ProductName,
                                  Quantity = a.Quantity
                              };
            var OrderDetail = orderModels.Where(x => x.InvoiceID == id).ToList();        
            return View(OrderDetail);
        }
    }
       
}