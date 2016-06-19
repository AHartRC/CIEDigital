using System.Web.Mvc;
using CIEDigitalLib.Attributes;

namespace CIEDigitalLib.Controllers
{
    [CIEDigitalDatabase]
    [EncryptedActionParameter]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}