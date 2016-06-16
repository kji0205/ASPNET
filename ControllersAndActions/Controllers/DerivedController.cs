using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ControllersAndActions.Infrastructure;

namespace ControllersAndActions.Controllers
{
    public class DerivedController : Controller
    {
        // GET: Derived
        public ActionResult Index()
        {
            ViewBag.Message = "Hello from the DerivedController Index method";
            return View("MyView");
        }

        public ActionResult RenameProduct(string page)
        {
            string userName = User.Identity.Name;
            string serverName = Server.MachineName;
            string clientIP = Request.UserHostAddress;
            DateTime dataStamp = HttpContext.Timestamp;

            string oldProductName = Request.Form["OldName"];
            string newProductName = Request.Form["NewName"];

            return View("MyView");
        }

        public ActionResult ProductOutput()
        {
            if (Server.MachineName=="JIMMY")
            {
                Response.Redirect("/Basic/Index");
                return new CustomRedirectResult { Url = "/Basic/Index" };
            }
            else
            {
                Response.Write("Controller: Derived, Action: ProduceOutput");
                return null;
            }
        }

        public ActionResult ProduceOutput()
        {
            //return new RedirectResult("/Basic/Index");
            return Redirect("/Basic/Index");
        }

        public HttpNotFoundResult Test()
        {
            return null;
        }
    }
}