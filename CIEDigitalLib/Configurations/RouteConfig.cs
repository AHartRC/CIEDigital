using System.Web.Mvc;
using System.Web.Routing;

namespace CIEDigitalLib.Configurations
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("robots.txt");

            routes.MapRoute("Default",
                "{controller}/{action}/{id}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                new[] {"CIEDigitalLib.Controllers"});
        }
    }
}