using System;
using System.Web.Mvc;

namespace ControllersAndActions.Controllers
{
    public class ExampleController : Controller
    {
        // GET: Example
        public ViewResult Index()
        {
            //DateTime date = DateTime.Now;
            ViewBag.Message = TempData["Message"];
            ViewBag.Date = TempData["Date"];
            return View();
        }

        //public RedirectResult Redirect()
        //{
        //    //return Redirect("/Example/Index");
        //    return RedirectPermanent("/Example/Index");
        //}

        //public RedirectToRouteResult Redirect()
        //{
        //    return RedirectToRoute(new
        //    {
        //        controller = "Example",
        //        action = "Index",
        //        ID = "MyID"
        //    });
        //}

        public RedirectToRouteResult Redirect()
        {
            TempData["Message"] = "Hello";
            TempData["Date"] = DateTime.Now;
            return RedirectToAction("Index");
        }

        public HttpStatusCodeResult StatusCode()
        {
            return new HttpStatusCodeResult(404, "URL cannot be serviced");
        }

        public HttpStatusCodeResult StatusCode401()
        {
            return new HttpUnauthorizedResult();
        }
    }
}