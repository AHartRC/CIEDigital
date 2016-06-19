using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using CIEDigitalLib.Attributes;
using CIEDigitalLib.Models.Binding;
using CIEDigitalLib.Models.Context;

namespace CIEDigitalLib.Controllers
{
    [EncryptedActionParameter]
    public class WeatherController : BaseController<Weather>
    {
        private readonly CIEDigitalEntities db = new CIEDigitalEntities();

        //// GET: Weather
        //public async Task<ActionResult> Index()
        //{
        //    var weathers = db.Weathers.Include(w => w.AwayTeam).Include(w => w.HomeTeam);
        //    return View(await weathers.ToListAsync());
        //}

        // GET: Weather/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var weather = await db.Weathers.FindAsync(id);
            if (weather == null)
            {
                return HttpNotFound();
            }
            return View(weather);
        }

        // GET: Weather/Create
        public ActionResult Create()
        {
            ViewBag.AwayTeamID = new SelectList(db.Teams, "ID", "Franchise");
            ViewBag.HomeTeamID = new SelectList(db.Teams, "ID", "Franchise");
            return View();
        }

        // POST: Weather/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(
                Include =
                    "ID,AwayScore,AwayTeamID,Date,Description,GameID,Humidity,HomeScore,HomeTeamID,Temperature,WindChill,WindMPH"
                )] Weather weather)
        {
            if (ModelState.IsValid)
            {
                db.Weathers.Add(weather);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AwayTeamID = new SelectList(db.Teams, "ID", "Franchise", weather.AwayTeamID);
            ViewBag.HomeTeamID = new SelectList(db.Teams, "ID", "Franchise", weather.HomeTeamID);
            return View(weather);
        }

        // GET: Weather/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var weather = await db.Weathers.FindAsync(id);
            if (weather == null)
            {
                return HttpNotFound();
            }
            ViewBag.AwayTeamID = new SelectList(db.Teams, "ID", "Franchise", weather.AwayTeamID);
            ViewBag.HomeTeamID = new SelectList(db.Teams, "ID", "Franchise", weather.HomeTeamID);
            return View(weather);
        }

        // POST: Weather/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(
                Include =
                    "ID,AwayScore,AwayTeamID,Date,Description,GameID,Humidity,HomeScore,HomeTeamID,Temperature,WindChill,WindMPH"
                )] Weather weather)
        {
            if (ModelState.IsValid)
            {
                db.Entry(weather).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AwayTeamID = new SelectList(db.Teams, "ID", "Franchise", weather.AwayTeamID);
            ViewBag.HomeTeamID = new SelectList(db.Teams, "ID", "Franchise", weather.HomeTeamID);
            return View(weather);
        }

        // GET: Weather/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var weather = await db.Weathers.FindAsync(id);
            if (weather == null)
            {
                return HttpNotFound();
            }
            return View(weather);
        }

        // POST: Weather/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var weather = await db.Weathers.FindAsync(id);
            db.Weathers.Remove(weather);
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