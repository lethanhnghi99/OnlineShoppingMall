using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShoppingMall.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            HttpCookie cookie = HttpContext.Request.Cookies.Get("userName");

            if (cookie == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Login" }));
            }
            base.OnActionExecuted(filterContext);
        }
    }
}