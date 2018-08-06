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
        public async Task<ActionResult> Search(UserSearchViewModel model)
        {
            var submitAction = Request.Form.Get(UserSearchViewModel.SubmitActionKey)??string.Empty;
            
            if (submitAction.Trim().Equals(UserSearchViewModel.Reset, StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Index");
            }
            else
            {
                model = await SetupUserSearchViewModel(model.UserName, model.DepartmentName);
                var users = _userService.GetUsers(model.UserName, model.DepartmentName);
                model.Users = users;
            }
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
        private async Task<UserSearchViewModel> SetupUserSearchViewModel(string userName = null, string departmentName = null)
        {
            var model = new UserSearchViewModel();

            var branches = await _branchesService.GetAllBranches();

            if (!string.IsNullOrWhiteSpace(userName))
            {
                model.UserName = userName;
            }

            model.Departments = branches.Select(x =>
            {

                var item = new SelectListItem() { Text = x.Name, Value = x.Name };

                if (!string.IsNullOrWhiteSpace(departmentName) && item.Value == departmentName)
                {
                    item.Selected = true;
                    model.DepartmentAddress = x.Address?.Trim('"') ?? null;
                }

                return item;
            }).ToList();

            model.Departments.Insert(0, new SelectListItem() { Text = "Select a branch", Value = "" });
            return model;
        }

    }
}