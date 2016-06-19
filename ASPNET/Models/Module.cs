using System.Collections;
using System.Collections.Generic;

namespace ASPNET.Models
{
    public class Module
    {
        public int AreaID { get; set; }
        public string ModuleName { get; set; }
        public string ModuleType { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }

        public List<Product> ProductList { get; set; } 
        public List<Banner> BannerList { get; set; }

        public class Product {
            public string PrstCD { get; set; }
            public string PrdTitle { get; set; }
        }
        public class Banner {
            public string BannerCD { get; set; }
            public string BannerTitle { get; set; }
        }
    }
}