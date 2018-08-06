using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Turners.UserPortal.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View("Error");
        }
        public ActionResult NotFound()
        {
            return View("NotFoundError");
        }
    }
}