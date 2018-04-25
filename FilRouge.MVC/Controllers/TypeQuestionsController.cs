using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FilRouge.MVC.Entities;

namespace FilRouge.MVC.ViewModels
{
    [Authorize]
    public class TypeQuestionsController : Controller
    {
        private FilRougeDBContext db = new FilRougeDBContext();

        // GET: TypeQuestions
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.TypeQuestion.ToList());
        }

        // GET: TypeQuestions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeQuestion typeQuestion = db.TypeQuestion.Find(id);
            if (typeQuestion == null)
            {
                return HttpNotFound();
            }
            return View(typeQuestion);
        }

        // GET: TypeQuestions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeQuestions/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TypeQuestionId,NameType")] TypeQuestion typeQuestion)
        {
            if (ModelState.IsValid)
            {
                db.TypeQuestion.Add(typeQuestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeQuestion);
        }

        // GET: TypeQuestions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeQuestion typeQuestion = db.TypeQuestion.Find(id);
            if (typeQuestion == null)
            {
                return HttpNotFound();
            }
            return View(typeQuestion);
        }

        // POST: TypeQuestions/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TypeQuestionId,NameType")] TypeQuestion typeQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeQuestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeQuestion);
        }

        // GET: TypeQuestions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeQuestion typeQuestion = db.TypeQuestion.Find(id);
            if (typeQuestion == null)
            {
                return HttpNotFound();
            }
            return View(typeQuestion);
        }

        // POST: TypeQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeQuestion typeQuestion = db.TypeQuestion.Find(id);
            db.TypeQuestion.Remove(typeQuestion);
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
