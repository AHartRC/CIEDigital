using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using CIEDigitalLib.Attributes;
using CIEDigitalLib.Extensions;
using CIEDigitalLib.Models.Binding;
using CIEDigitalLib.Models.Context;
using CIEDigitalLib.Models.View;
using CIEDigitalLib.Search;

namespace CIEDigitalLib.Controllers
{
    [EncryptedActionParameter]
    public class PositionsController : BaseController<PlayerPosition>
    {
        private readonly CIEDigitalEntities db = new CIEDigitalEntities();


        //// GET: Positions
        //public ActionResult Index()
        //{
        //    var searchCriteria = typeof(PlayerPosition).GetDefaultSearchCriteria();

        //    var data = db.PlayerPositions.ApplySearchCriteria(searchCriteria).GetPagedResult(new Paging());

        //    var model = new PagedSearchViewModel<PlayerPosition>
        //    {
        //        Data = data,
        //        SearchCriteria = searchCriteria
        //    };

        //    return View(model);
        //}

        //// POST: Positions/5
        //[HttpPost]
        //public ActionResult Index(Paging paging, ICollection<AbstractSearch> searchCriteria)
        //{
        //    if (searchCriteria == null || !searchCriteria.Any())
        //    {
        //        searchCriteria = typeof (PlayerPosition).GetDefaultSearchCriteria();
        //    }

        //    var data = db.PlayerPositions.ApplySearchCriteria(searchCriteria).GetPagedResult(paging);

        //    var model = new PagedSearchViewModel<PlayerPosition>
        //    {
        //        Data = data,
        //        SearchCriteria = searchCriteria
        //    };

        //    return View(model);
        //}

        // GET: Positions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var playerPosition = await db.PlayerPositions.FindAsync(id);
            if (playerPosition == null)
            {
                return HttpNotFound();
            }
            return View(playerPosition);
        }

        // GET: Positions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Positions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ShortName,Name")] PlayerPosition playerPosition)
        {
            if (ModelState.IsValid)
            {
                db.PlayerPositions.Add(playerPosition);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(playerPosition);
        }

        // GET: Positions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var playerPosition = await db.PlayerPositions.FindAsync(id);
            if (playerPosition == null)
            {
                return HttpNotFound();
            }
            return View(playerPosition);
        }

        // POST: Positions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ShortName,Name")] PlayerPosition playerPosition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(playerPosition).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(playerPosition);
        }

        // GET: Positions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var playerPosition = await db.PlayerPositions.FindAsync(id);
            if (playerPosition == null)
            {
                return HttpNotFound();
            }
            return View(playerPosition);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var playerPosition = await db.PlayerPositions.FindAsync(id);
            db.PlayerPositions.Remove(playerPosition);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}