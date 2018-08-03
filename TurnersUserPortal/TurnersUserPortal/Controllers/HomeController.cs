using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Turners.UserPortal.Models;
using Turners.UserPortal.Service;

namespace TurnersUserPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersService _userService;
        private readonly IBranchesService _branchesService;

        public HomeController(IUsersService userService, IBranchesService branchesService)
        {
            _userService = userService;
            _branchesService = branchesService;
        }
        public async Task<ActionResult> Index()
        {
            UserSearchViewModel model = await SetupUserSearchViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Search(string userName, string departmentName)
        {
            var users = _userService.GetUsers(userName, departmentName);
            var model = await SetupUserSearchViewModel();
            model.Users = users;
            model.DepartmentAddress = _branchesService.GetBranchByName(departmentName)?.Address.Trim('"')??null;
            return View("Index", model);
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
        private async Task<UserSearchViewModel> SetupUserSearchViewModel()
        {
            var model = new UserSearchViewModel();

            var branches = await _branchesService.GetAllBranches();

            model.Departments = branches.Select(x => new SelectListItem() { Text = x.Name, Value = x.Name }).ToList();

            model.Departments.Insert(0, new SelectListItem() { Text = "Select a branch", Value = "" });
            return model;
        }

    }
}