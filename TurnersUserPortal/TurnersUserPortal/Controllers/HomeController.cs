using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Turners.UserPortal.Service;

namespace TurnersUserPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        public ActionResult Index()
        {
            //var users = _userService.GetUsers("andrew", null);
            //return View(users);
            return View();
        }

        [HttpPost]
        public ActionResult Search(string userName, string departmentName)
        {
            var users = _userService.GetUsers(userName, departmentName);
            return View("Index", users);
        }

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
    }
}