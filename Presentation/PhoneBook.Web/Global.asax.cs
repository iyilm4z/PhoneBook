using System.Web.Mvc;
using System.Web.Routing;
using FluentValidation.Mvc;
using PhoneBook.Core.Engine;
using PhoneBook.Web.Framework;

namespace PhoneBook.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "PhoneBook.Web.Controllers" }
            );
        }

        protected void Application_Start()
        {
            EngineContext.Initialize();
            FluentValidationModelValidatorProvider.Configure();

            var dbSeeder = new DbSeeder();
            dbSeeder.Seed();

            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
        }
    }
}
