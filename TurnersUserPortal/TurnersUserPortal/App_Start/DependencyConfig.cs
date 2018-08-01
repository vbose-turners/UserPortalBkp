using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Turners.UserPortal.Repository;
using Turners.UserPortal.Service;
using TurnersUserPortal;

namespace Turners.UserPortal.App_Start
{
    public class DependencyConfig
    {
        public static void ConfigureDependencies()
        {
            var builder = new ContainerBuilder();

            // Register dependencies in controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<ActiveDirectoryRepository>().As<IUserRepository>();
            builder.RegisterType<UserService>().As<IUserService>();

            var container = builder.Build();

            // Set MVC DI resolver to use Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}