using ASPNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;

namespace ASPNET.Controllers
{
    public class EssentialToolsController : Controller
    {
        private IValueCalculator calc;
        private Product[] products =
        {
            new Product {Name="Kayak", Category="Watersports", Price=275M },
            new Product {Name="Lifejacket", Category="Watersports", Price=48.95M },
            new Product {Name="Soccer ball", Category="Soccer", Price=19.50M },
            new Product {Name="Corner flag", Category="Soccer", Price=34.95M }
        };

        public EssentialToolsController(IValueCalculator calcParam, IValueCalculator calc2)
        {
            calc = calcParam;
        }

        // GET: EssentialTools
        public ActionResult Index()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
            IValueCalculator calc = ninjectKernel.Get<IValueCalculator>();

            //LinqValueCalculator calc = new LinqValueCalculator();

            ShoppingCart2 cart = new ShoppingCart2(calc) { Products = products };
            decimal totalValue = cart.CalculateProductTotal();
            return View(totalValue);
        }
    }
}