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
            ViewResult result = target.Index();
            Assert.AreEqual("Homepage", result.ViewName);

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
