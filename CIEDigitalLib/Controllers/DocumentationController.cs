using System.Web.Http;
using System.Web.Mvc;
using CIEDigitalLib.Attributes;
using CIEDigitalLib.Extensions;
using CIEDigitalLib.Models.Description;

namespace CIEDigitalLib.Controllers
{
    /// <summary>
    ///     The controller that will handle requests for the help page.
    /// </summary>
    [CIEDigitalDatabase]
    [EncryptedActionParameter]
    public class DocumentationController : Controller
    {
        private const string ErrorViewName = "Error";

        public DocumentationController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public DocumentationController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }

        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        public ActionResult Api(string apiId)
        {
            if (!string.IsNullOrEmpty(apiId))
            {
                var apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View(ErrorViewName);
        }

        public ActionResult ResourceModel(string modelName)
        {
            if (!string.IsNullOrEmpty(modelName))
            {
                var modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
                ModelDescription modelDescription;
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
                {
                    return View(modelDescription);
                }
            }

            return View(ErrorViewName);
        }
    }
}