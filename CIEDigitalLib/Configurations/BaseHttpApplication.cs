using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CIEDigitalLib.Binders;
using CIEDigitalLib.Search;

namespace CIEDigitalLib.Configurations
{
    public class BaseHttpApplication : HttpApplication
    {
        protected void App_Start()
        {
            ModelBinders.Binders[typeof (AbstractSearch)] = new AbstractSearchModelBinder();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AreaRegistration.RegisterAllAreas();
        }
    }
}