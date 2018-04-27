    using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FilRouge.MVC.Entities;
using FilRouge.MVC.Services;
using FilRouge.MVC.ViewModels.Maps;

namespace FilRouge.MVC.Controllers
{
    [Authorize]
    public class DifficultyRatesController : Controller
    {
        private FilRougeDBContext db = new FilRougeDBContext();
        private readonly DifficultyRatesServices _difficultyRatesServices = new DifficultyRatesServices();

        // GET: DifficultyRates
        [AllowAnonymous]
        public ActionResult Index()
        {
            var difficultyRates = _difficultyRatesServices.GetAllDifficultyRates();
            return View(difficultyRates);
        }

        // GET: DifficultyRates/Details/5
        public ActionResult Details(int idMaster, int id)
        {

            return View(_difficultyRatesServices.GetDifficultyRateById(idMaster, id));
        }

        // GET: DifficultyRates/Create
        public ActionResult Create()
        {
            ViewBag.DifficultyId = new SelectList(db.Difficulties, "DifficultyId", "DifficultyName");
            ViewBag.DifficultyMasterId = new SelectList(db.Difficulties, "DifficultyId", "DifficultyName");
            return View();
        }

        // POST: DifficultyRates/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DifficultyMasterId,DifficultyId,Rate")] DifficultyRate difficultyRate)
        {
            if (ModelState.IsValid)
            {
                if(difficultyRate.Rate > 1)
                {
                    difficultyRate.Rate = difficultyRate.Rate / 100;
                }
                db.DifficultyRates.Add(difficultyRate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DifficultyId = new SelectList(db.Difficulties, "DifficultyId", "DifficultyName", difficultyRate.DifficultyId);
            ViewBag.DifficultyMasterId = new SelectList(db.Difficulties, "DifficultyId", "DifficultyName", difficultyRate.DifficultyMasterId);
            return View(difficultyRate);
        }

        // GET: DifficultyRates/Edit/2?idMaster=1
        public ActionResult Edit(int idMaster, int id)
        {
            DifficultyRate difficultyRate = db.DifficultyRates.Find(idMaster, id);
            if (difficultyRate == null)
            {
                return HttpNotFound();
            }
            ViewBag.DifficultyId = new SelectList(db.Difficulties, "DifficultyId", "DifficultyName", difficultyRate.DifficultyId);
            ViewBag.DifficultyMasterId = new SelectList(db.Difficulties, "DifficultyId", "DifficultyName", difficultyRate.DifficultyMasterId);
            var difficultyViewModel = difficultyRate.MapToDifficultyRateViewModel();
            return View(difficultyViewModel);
        }

        // POST: DifficultyRates/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DifficultyMasterId,DifficultyId,Rate")] DifficultyRate difficultyRate)
        {
            if (ModelState.IsValid)
            {
                if (difficultyRate.Rate > 1)
                {
                    difficultyRate.Rate = difficultyRate.Rate / 100;
                }
                db.Entry(difficultyRate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DifficultyId = new SelectList(db.Difficulties, "DifficultyId", "DifficultyName", difficultyRate.DifficultyId);
            ViewBag.DifficultyMasterId = new SelectList(db.Difficulties, "DifficultyId", "DifficultyName", difficultyRate.DifficultyMasterId);
            return View(difficultyRate);
        }

        // GET: DifficultyRates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DifficultyRate difficultyRate = db.DifficultyRates.Find(id);
            if (difficultyRate == null)
            {
                return HttpNotFound();
            }
            return View(difficultyRate);
        }

        // POST: DifficultyRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DifficultyRate difficultyRate = db.DifficultyRates.Find(id);
            db.DifficultyRates.Remove(difficultyRate);
            db.SaveChanges();
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
