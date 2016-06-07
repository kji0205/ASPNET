using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPNET.TestingDemo.Models;

namespace ASPNET.Controllers
{
    public class AdminController : Controller
    {
        private IUserRepository repository;
        
        public AdminController(IUserRepository repo)
        {
            repository = repo;
        }    

        public ActionResult ChangeLoginName(String oldName, string newName)
        {
            User user = repository.FetchByLoginName(oldName);
            user.LoginName = newName;
            repository.SubmitChanges();
            return View();
        }
    }
}