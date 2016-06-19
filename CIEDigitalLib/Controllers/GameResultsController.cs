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
    public class GameResultsController : BaseController<GameResult>
    {
        private readonly CIEDigitalEntities db = new CIEDigitalEntities();

        //// GET: GameResults
        //public async Task<ActionResult> Index()
        //{
        //    var gameResults = db.GameResults.Include(g => g.AwayTeam).Include(g => g.HomeTeam);
        //    return View(await gameResults.ToListAsync());
        //}

        // GET: GameResults/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var gameResult = await db.GameResults.FindAsync(id);
            if (gameResult == null)
            {
                return HttpNotFound();
            }
            return View(gameResult);
        }

        // GET: GameResults/Create
        public ActionResult Create()
        {
            ViewBag.AwayTeamID = new SelectList(db.Teams, "ID", "Franchise");
            ViewBag.HomeTeamID = new SelectList(db.Teams, "ID", "Franchise");
            return View();
        }

        // POST: GameResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "ID,AwayScore,AwayTeamID,HomeTeamID,HomeScore,KickOff,Season,Week")] GameResult gameResult)
        {
            if (ModelState.IsValid)
            {
                db.GameResults.Add(gameResult);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AwayTeamID = new SelectList(db.Teams, "ID", "Franchise", gameResult.AwayTeamID);
            ViewBag.HomeTeamID = new SelectList(db.Teams, "ID", "Franchise", gameResult.HomeTeamID);
            return View(gameResult);
        }

        // GET: GameResults/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var gameResult = await db.GameResults.FindAsync(id);
            if (gameResult == null)
            {
                return HttpNotFound();
            }
            ViewBag.AwayTeamID = new SelectList(db.Teams, "ID", "Franchise", gameResult.AwayTeamID);
            ViewBag.HomeTeamID = new SelectList(db.Teams, "ID", "Franchise", gameResult.HomeTeamID);
            return View(gameResult);
        }

        // POST: GameResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "ID,AwayScore,AwayTeamID,HomeTeamID,HomeScore,KickOff,Season,Week")] GameResult gameResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gameResult).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AwayTeamID = new SelectList(db.Teams, "ID", "Franchise", gameResult.AwayTeamID);
            ViewBag.HomeTeamID = new SelectList(db.Teams, "ID", "Franchise", gameResult.HomeTeamID);
            return View(gameResult);
        }

        // GET: GameResults/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var gameResult = await db.GameResults.FindAsync(id);
            if (gameResult == null)
            {
                return HttpNotFound();
            }
            return View(gameResult);
        }

        // POST: GameResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var gameResult = await db.GameResults.FindAsync(id);
            db.GameResults.Remove(gameResult);
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