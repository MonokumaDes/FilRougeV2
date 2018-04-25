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

namespace FilRouge.MVC.Controllers
{
    [Authorize]
	public class TechnologiesController : Controller
	{
		private TechnologiesService _technologies = new TechnologiesService();

        [AllowAnonymous]
		// GET: Technologies
		public ActionResult Index()
		{
			return View(_technologies.GetAllTechnologies());
		}

		// GET: Technologies/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Technology technology = _technologies.GetTechnology(id);
			if (technology == null)
			{
				return HttpNotFound();
			}
			return View(technology);
		}

		// GET: Technologies/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Technologies/Create
		// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
		// plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "TechnoId,TechnoName,Active")] Technology technology)
		{
			if (ModelState.IsValid)
			{

				_technologies.AddTechnology(technology);
				return RedirectToAction("Index", "Home");
			}

			return View(technology);
		}

		// GET: Technologies/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Technology technology = _technologies.GetTechnology(id);
			if (technology == null)
			{
				return HttpNotFound();
			}
			return View(technology);
		}

		// POST: Technologies/Edit/5
		// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
		// plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "TechnoId,TechnoName,Active")] Technology technology)
		{
			if (ModelState.IsValid)
			{
				_technologies.EditTechnology(technology);
				return RedirectToAction("Index");
			}
			return View(technology);
		}

		// GET: Technologies/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Technology technology = _technologies.GetTechnology(id);
			if (technology == null)
			{
				return HttpNotFound();
			}
			return View(technology);
		}

		// POST: Technologies/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			_technologies.RemoveTechnology(id);

			return RedirectToAction("Index");
		}

		//protected override void Dispose(bool disposing)
		//{
		//    if (disposing)
		//    {
		//        db.Dispose();
		//    }
		//    base.Dispose(disposing);
		//}
	}
}
