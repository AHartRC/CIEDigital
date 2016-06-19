//using System.Web.Http;
//using System.Web.Http.OData.Builder;
//using System.Web.Http.OData.Extensions;
//using CIEDigitalLib.Models.Binding;
//using Microsoft.Owin.Security.OAuth;
//using Newtonsoft.Json;

//namespace CIEDigitalLib.Configurations
//{
//    public static class WebApiConfig
//    {
//        public static void Register(HttpConfiguration config)
//        {
//            var builder = new ODataConventionModelBuilder();
//            builder.EntitySet<Combine>("Combines");
//            builder.EntitySet<GameResult>("Game Results");
//            builder.EntitySet<Organization>("Organizations");
//            builder.EntitySet<Play>("Plays");
//            builder.EntitySet<Player>("Players");
//            builder.EntitySet<PlayerPosition>("Positions");
//            builder.EntitySet<TeamName>("Team Names");
//            builder.EntitySet<Team>("Teams");
//            builder.EntitySet<Weather>("Weather");
//            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

//            // Web API configuration and services
//            // Configure Web API to use only bearer token authentication.
//            config.SuppressDefaultHostAuthentication();

//            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling =
//                ReferenceLoopHandling.Serialize;
//            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling =
//                PreserveReferencesHandling.Objects;

//            //config.EnableCors();

//            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

//            // Web API routes
//            config.MapHttpAttributeRoutes();

//            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{action}/{id}",
//                new {id = RouteParameter.Optional});
//        }
//    }
//}