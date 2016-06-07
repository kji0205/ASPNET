using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPNET.Models;

namespace ASPNET.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int hour = DateTime.Now.Hour;
            ViewData["greeting"] = (hour < 12 ? "Good morning" : "Good afternoon");
            return View();
        }

        #region RSVP
        public ViewResult Index2()
        {
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 ? "Good Morning" : "Good Afternoon";
            return View();
        }

        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guestResponse)
        {
            if (ModelState.IsValid)
            {
                return View("Thanks", guestResponse);
            }
            else
            {
                return View();
            }
        }
        #endregion

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region LanguageFeatures
        public string LanguageFeatures()
        {
            return "Navigate to a URL to show an example";
        }

        public ViewResult AutoProperty()
        {
            var myProduct = new Product();
            myProduct.Name = "Kayak";
            string productName = myProduct.Name;
            return View("Result", (object)String.Format("Product name: {0}", productName));
        }

        public ViewResult CreateProduct()
        {
            var myProduct = new Product {
                ProductID=100,Name="Kayak",
                Description="A boat for one person",
                Price=275M, Category="Watersports"
            };
            return View("Result", (object)String.Format("Category: {0}", myProduct.Category));
        }

        public ViewResult CreateCollection()
        {
            string[] stringArray = { "apple", "orange", "plum" };
            List<int> intList = new List<int> { 10, 20, 30, 40 };
            Dictionary<string, int> myDict = new Dictionary<string, int>
            {
                {"apple",10 },{"orange",20 },{"plum",30 }
            };
            return View("Result", (object)stringArray[1]);
        }

        public ViewResult UseExtension()
        {
            var cart = new ShoppingCart {
                Products = new List<Product>
                {
                    new Product {Name="Kayak",Price=275M },
                    new Product {Name="Lifejacket",Price=48.95M },
                    new Product {Name="Soccer ball",Price=19.50M },
                    new Product {Name="Corner flag",Price=34.95M },
                }
            };
            decimal cartTotal = cart.TotalPrices();
            return View("Result", (object)String.Format("Total: {0:c}", cartTotal));
        }

        public ViewResult UseExtensionEnumerable()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name="Kayak", Price=275M },
                    new Product {Name="Lifejacket", Price=48.95M },
                    new Product {Name="Soccer ball", Price=19.50M },
                    new Product {Name="Corner flag", Price=34.95M },
                }
            };
            Product[] productArray =
            {
                new Product {Name="Kayak",Price=275M },
                new Product {Name="Lifejacket",Price=48.95M },
                new Product {Name="Soccer ball",Price=19.50M },
                new Product {Name="Corner flag",Price=34.95M }
            };
            decimal cartTotal = products.TotalPrices2();
            decimal arrayTotal = productArray.TotalPrices2();
            return View("Result", (object)String.Format("Cart Total: {0}, Array Total: {1}", cartTotal, arrayTotal));
        }

        public ViewResult UseFilterExtensionMethod()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name="Kayak", Category="Watersports", Price=275M },
                    new Product {Name="Lifejacket", Category="Watersports", Price=275M },
                    new Product {Name="Soccer ball", Category="Soccer", Price=19.50M },
                    new Product {Name="Corner flag", Category="Soccer", Price=34.95M },
                }
            };
            decimal total = 0;
            foreach (Product prod in products.FilterByCategory("Soccer"))
            {
                total += prod.Price;
            }
            return View("Result", (object)String.Format("Total: {0}", total));
        }

        public ViewResult UseFilterExtensionMethod2()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name="Kayak", Category="Watersports", Price=275M },
                    new Product {Name="Lifejacket", Category="Watersports", Price=48.95M },
                    new Product {Name="Soccer ball", Category="Soccer", Price=19.50M },
                    new Product {Name="Corner flag", Category="Soccer", Price=34.95M },
                }
            };
            //Func<Product, bool> categoryFilter = prod => prod.Category == "Soccer";
            //decimal total = 0;
            //foreach (Product prod in products.Filter(categoryFilter))
            //{
            //    total += prod.Price;
            //}
            
            decimal total = 0;
            foreach (Product prod in products
                .Filter(prod=>prod.Category=="Soccer" || prod.Price > 20))
            {
                total += prod.Price;
            }
            return View("Result", (object)String.Format("Total: {0}", total));
        }

        public ViewResult CreateAnonArray()
        {
            var oddsAndEnds = new[]
            {
                new {Name="MVC", Category="Pattern"},
                new {Name="Hat", Category="Clothing"},
                new {Name="Apple", Category="Fruit"}
            };
            StringBuilder result = new StringBuilder();
            foreach (var item in oddsAndEnds)
            {
                result.Append(item.Name).Append(" ");
            }
            return View("Result", (object)result.ToString());
        }

        public ViewResult FindProducts()
        {
            Product[] products =
            {
                new Product {Name="Kayak", Category="Watersports", Price=275M },
                new Product {Name="Lifejacket", Category="Watersports", Price=48.95M },
                new Product {Name="Soccer ball", Category="Soccer", Price=19.50M },
                new Product {Name="Corner flag", Category="Soccer", Price=34.95M },
            };
            var foundProducts = from match in products
                                orderby match.Price descending
                                select new { match.Name, match.Price };
            var foundProducts2 = products.OrderByDescending(e => e.Price)
                                    .Take(3)
                                    .Select(e => new { e.Name, e.Price });
            var results = products.Sum(e => e.Price);

            products[2] = new Product { Name = "Stadium", Price = 79600M };

            int count = 0;
            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts2)
            {
                result.AppendFormat("Price:{0} ", p.Price).Append(" ");
                if (++count==3)
                {
                    break;
                }
            }
            result.AppendFormat("results:{0} ", results);
            return View("Result", (object)result.ToString());
        }
        #endregion
    }
}