using System.Web.Http;
using System.Web.Mvc;
using CIEDigitalLib.Configurations;

namespace CIEDigitalLib.Registration
{
    public class HelpPageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "HelpPage"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HelpPage_Default",
                "Help/{controller}/{action}/{apiId}",
                new {area = "HelpPage", controller = "Help", action = "Index", apiId = UrlParameter.Optional},
                new[] {"CIEDigitalLib.Controllers", "CIEDigitalLib.Controllers.API"});

            DocumentationConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}