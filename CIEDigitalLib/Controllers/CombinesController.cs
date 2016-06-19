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
    public class CombinesController : BaseController<Combine>
    {
        private readonly CIEDigitalEntities db = new CIEDigitalEntities();

        //// GET: Combines
        //public async Task<ActionResult> Index()
        //{
        //    var combines = db.Combines.Include(c => c.Position);
        //    return View(await combines.ToListAsync());
        //}

        // GET: Combines/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var combine = await db.Combines.FindAsync(id);
            if (combine == null)
            {
                return HttpNotFound();
            }
            return View(combine);
        }

        // GET: Combines/Create
        public ActionResult Create()
        {
            ViewBag.PositionID = new SelectList(db.PlayerPositions, "ID", "ShortName");
            return View();
        }

        // POST: Combines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(
                Include =
                    "ID,Arms,Bench,Broad,College,FirstName,FourtyYardDash,Hands,HeightFeet,HeightInches,HeightInchesTotal,Name,NFLGrade,LastName,Pick,PickRound,PickTotal,PositionID,Round,TenYardDash,ThreeCone,TwentyYardDash,TwentyYardShuttle,Vertical,WeightPounds,Wonderlic,Year"
                )] Combine combine)
        {
            if (ModelState.IsValid)
            {
                db.Combines.Add(combine);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PositionID = new SelectList(db.PlayerPositions, "ID", "ShortName", combine.PositionID);
            return View(combine);
        }

        // GET: Combines/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var combine = await db.Combines.FindAsync(id);
            if (combine == null)
            {
                return HttpNotFound();
            }
            ViewBag.PositionID = new SelectList(db.PlayerPositions, "ID", "ShortName", combine.PositionID);
            return View(combine);
        }

        // POST: Combines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(
                Include =
                    "ID,Arms,Bench,Broad,College,FirstName,FourtyYardDash,Hands,HeightFeet,HeightInches,HeightInchesTotal,Name,NFLGrade,LastName,Pick,PickRound,PickTotal,PositionID,Round,TenYardDash,ThreeCone,TwentyYardDash,TwentyYardShuttle,Vertical,WeightPounds,Wonderlic,Year"
                )] Combine combine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(combine).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PositionID = new SelectList(db.PlayerPositions, "ID", "ShortName", combine.PositionID);
            return View(combine);
        }

        // GET: Combines/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var combine = await db.Combines.FindAsync(id);
            if (combine == null)
            {
                return HttpNotFound();
            }
            return View(combine);
        }

        // POST: Combines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var combine = await db.Combines.FindAsync(id);
            db.Combines.Remove(combine);
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