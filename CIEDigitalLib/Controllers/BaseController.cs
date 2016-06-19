using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CIEDigitalLib.Extensions;
using CIEDigitalLib.Models.Context;
using CIEDigitalLib.Models.View;
using CIEDigitalLib.Search;

namespace CIEDigitalLib.Controllers
{
    public class BaseController<T> : Controller where T : class
    {
        private readonly CIEDigitalEntities db = new CIEDigitalEntities();
        // GET: Positions
        public ActionResult Index(Paging paging, ICollection<AbstractSearch> searchCriteria)
        {
            if (searchCriteria == null || !searchCriteria.Any())
            {
                searchCriteria = typeof(T).GetDefaultSearchCriteria();
            }

            var data = db.GetQuery<T>(true).ApplySearchCriteria(searchCriteria).GetPagedResult(paging);

            var model = new PagedSearchViewModel<T>
            {
                Data = data,
                SearchCriteria = searchCriteria
            };

            return View(model);
        }
    }
}
