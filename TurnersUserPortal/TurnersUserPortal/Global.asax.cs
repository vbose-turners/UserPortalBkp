using Autofac;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Turners.UserPortal.App_Start;

namespace TurnersUserPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyConfig.ConfigureDependencies();
        }

        protected void Application_Error(Object sender, EventArgs args)
        {
            var raisedException = Server.GetLastError();
            
            _logger.Error(raisedException, "Unhandled exception : ", args);

            var section = ConfigurationManager.GetSection("system.web/customErrors") as CustomErrorsSection;
            var customErrorsMode = section.Mode.ToString().ToLower();
            if (customErrorsMode == "on")
            {
                // Clear the error from the server
                Server.ClearError();
                Server.TransferRequest("~/Error");
            }
        }
    }
}
