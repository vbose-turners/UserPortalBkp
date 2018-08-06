using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Turners.UserPortal.Domain;

namespace Turners.UserPortal.Models
{
    public class UserSearchViewModel
    {
        public const string Search = "Search";
        public const string Reset = "Reset" ;
        public const string SubmitActionKey = "submitAction";

        public List<User> Users { get; set; }

        [DisplayName("User Name")]
        public string  UserName { get; set; }

        [DisplayName("Department")]
        public string DepartmentName { get; set; }

        public string DepartmentAddress { get; set; }

        public List<SelectListItem> Departments { get; set; }
    }
}