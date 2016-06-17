using System.Web.Mvc;
using ControllersAndActions.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControllersAndActions.Tests
{
    [TestClass]
    public class ActionTests
    {
        [TestMethod]
        public void ControllerTest()
        {
            ExampleController target = new ExampleController();
            //ViewResult result = target.Index();
            //RedirectResult result = target.Redirect();
            RedirectToRouteResult result = target.Redirect();
            //Assert.AreEqual("Homepage", result.ViewName);
            //Assert.IsTrue(result.Permanent);
            //Assert.AreEqual("/Example/Index", result.Url);

            Assert.IsFalse(result.Permanent);
            //Assert.AreEqual("Example", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            //Assert.AreEqual("MyID", result.RouteValues["ID"]);

            HttpStatusCodeResult result2 = target.StatusCode401();
            Assert.AreEqual(401, result2.StatusCode);


        }

        [TestMethod]
        public void ViewSelectionTest()
        {
            ExampleController target = new ExampleController();
            ViewResult result = target.Index();
            Assert.AreEqual("", result.ViewName);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(System.DateTime));
        }
        
    }
}
