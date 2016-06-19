using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPNET.Models;
using System.Data;
using System.Web.Script.Serialization;

namespace ASPNET.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public string GetJson()
        {

            #region TestData

            DataTable table1 = new DataTable("Module");
            table1.Columns.Add("AreaID");
            table1.Columns.Add("ModuleName");
            table1.Columns.Add("ModuleType");
            table1.Columns.Add("Title");
            table1.Columns.Add("Link");
            //table1.Columns.Add("PrstCD");
            //table1.Columns.Add("PrdTitle");
            table1.Rows.Add(1000, "모듈1", "type1", "모듈 타이틀", "http://example.com");
            table1.Rows.Add(1000, "모듈2", "type2", "모듈 타이틀", "http://example.com");
            table1.Rows.Add(1000, "모듈3", "type3", "모듈 타이틀", "http://example.com");

            DataTable table2 = new DataTable("Product");
            table2.Columns.Add("AreaID");
            table2.Columns.Add("ModuleName");
            table2.Columns.Add("ModuleType");
            table2.Columns.Add("Title");
            table2.Columns.Add("Link");
            table2.Columns.Add("PrstCD");
            table2.Columns.Add("PrdTitle");
            table2.Rows.Add(1000, "모듈1", "type1", "모듈 타이틀", "http://example.com", "P00001", "TV");
            table2.Rows.Add(1000, "모듈1", "type1", "모듈 타이틀", "http://example.com", "P00011", "TV01");
            table2.Rows.Add(2000, "모듈2", "type2", "모듈 타이틀", "http://example.com", "P00002", "Radio");
            table2.Rows.Add(3000, "모듈3", "type3", "모듈 타이틀", "http://example.com", "P00003", "Air");
            table2.Rows.Add(3000, "모듈3", "type3", "모듈 타이틀", "http://example.com", "P00033", "Air-03");

            //DataTable table3 = new DataTable("Banner");
            //table3.Columns.Add("AreaID");
            //table3.Columns.Add("ModuleName");
            //table3.Columns.Add("ModuleType");
            //table3.Columns.Add("Title");
            //table3.Columns.Add("Link");
            //table3.Columns.Add("BannerCD");
            //table3.Columns.Add("BannerTitle");
            //table3.Rows.Add(1000, "모듈1", "type1", "모듈 타이틀", "http://example.com", "B00001", "Banner1");
            //table3.Rows.Add(2000, "모듈2", "type2", "모듈 타이틀", "http://example.com", "B00002", "Banner2");
            //table3.Rows.Add(2000, "모듈2", "type2", "모듈 타이틀", "http://example.com", "B00022", "Banner2-2");
            //table3.Rows.Add(3000, "모듈3", "type3", "모듈 타이틀", "http://example.com", "B00003", "Banner3");

            // Create a DataSet.
            DataSet DS = new DataSet("Module");
            DS.Tables.Add(table1);
            DS.Tables.Add(table2);
            //DS.Tables.Add(table3);
            DS.Namespace = "y";
            DS.Prefix = "x";

            #endregion

            
            
            string result = "";


            // 모듈 정보 로드
            //if (DS.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            //    {
            //        var module = new Module
            //        {
            //            AreaID = Convert.ToInt32(DS.Tables[0].Rows[i]["AreaID"]),
            //            ModuleName = DS.Tables[0].Rows[i]["ModuleName"].ToString(),
            //            ModuleType = DS.Tables[0].Rows[i]["ModuleType"].ToString(),
            //            Title = DS.Tables[0].Rows[i]["Title"].ToString(),
            //            Link = DS.Tables[0].Rows[i]["Link"].ToString(),

            //        };
            //        moduleList.Add(module);
            //    }
            //}

            DataTable D1 = new DataTable();
            DataTable D2 = new DataTable();

            D1 = DS.Tables[1];
            //D2 = DS.Tables[2];

            List<Module> query = (
                from item in D1.AsEnumerable()    //.Select(x => new { ModuleType = x.Field<string>("ModuleType") }).Distinct()
                join item2 in D2.AsEnumerable()
                on item.Field<string>("ModuleType") equals item2.Field<string>("ModuleType") into obj
                from r in obj.DefaultIfEmpty()
                group item by new {
                    AreaID = item.Field<string>("AreaID"),
                    ModuleName = item.Field<string>("ModuleName"),
                    ModuleType = item.Field<string>("ModuleType"),
                    Title = item.Field<string>("Title"),
                    Link = item.Field<string>("Link")
                } into g
                select new Module
                {
                    AreaID = Convert.ToInt32(g.Key.AreaID),
                    ModuleName = g.Key.ModuleName,
                    ModuleType = g.Key.ModuleType,
                    Title = g.Key.Title,
                    Link = g.Key.Link,
                }).Distinct().ToList();

            //select new Module
            //{
            //    AreaID = Convert.ToInt32(item.Field<string>("AreaID")),
            //    ModuleName = item.Field<string>("ModuleName"),
            //    ModuleType = item.Field<string>("ModuleType"),
            //    Title = item.Field<string>("Title"),
            //    Link = item.Field<string>("Link")
            //}).ToList();


            var moduleList = new List<Module>();
            moduleList = query;


            // 상품 or 배너
            if (D1.Rows.Count > 0)
            {
                for (int i = 0; i < moduleList.Count; i++) 
                {
                    var pList = new List<Module.Product>();
                    for (int j = 0; j < D1.Rows.Count; j++)
                    {
                        if (moduleList[i].AreaID.ToString() == D1.Rows[j]["AreaID"].ToString())
                        {
                            if (moduleList[i].ModuleType.ToString() == D1.Rows[j]["ModuleType"].ToString())
                            {
                                var product = new Module.Product
                                {
                                    PrstCD = D1.Rows[j]["PrstCD"].ToString(),
                                    PrdTitle = D1.Rows[j]["PrdTitle"].ToString(),
                                };
                                pList.Add(product);
                            }
                        }
                    }
                    moduleList[i].ProductList = pList;
                }
            }

            // 상품 or 배너
            if (D2.Rows.Count > 0)
            {
                for (int i = 0; i < moduleList.Count; i++) 
                {
                    var bList = new List<Module.Banner>();
                    for (int j = 0; j < D2.Rows.Count; j++)
                    {
                        if (moduleList[i].AreaID.ToString() == D2.Rows[j]["AreaID"].ToString())
                        {
                            if (moduleList[i].ModuleType.ToString() == D2.Rows[j]["ModuleType"].ToString())
                            {
                                var banner = new Module.Banner
                                {
                                    BannerCD = D2.Rows[j]["BannerCD"].ToString(),
                                    BannerTitle = D2.Rows[j]["BannerTitle"].ToString(),
                                };
                                bList.Add(banner);
                            }
                        }
                    }
                    moduleList[i].BannerList = bList;
                }
            }

            
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            result = serializer.Serialize(moduleList);

            return result;
        }
    }
}