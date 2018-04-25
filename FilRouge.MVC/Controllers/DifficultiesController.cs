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
    public class DifficultiesController : Controller
    {
        private DifficultyServices _difficultyServices = new DifficultyServices();

        // GET: Difficulties
        [AllowAnonymous]
        public ActionResult Index()
        {
            var difficulty = _difficultyServices.GetAllDifficulties();
            return View(difficulty);
        }

        // GET: Difficulties/Details/5
        public ActionResult Details(int id)
        {
            var difficulty = _difficultyServices.GetDifficultyById(id);
            if (difficulty == null)
            {
                return HttpNotFound();
            }
            return View(difficulty);
        }

        // GET: Difficulties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Difficulties/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DifficultyId,DifficultyName")] Difficulty difficulty)
        {
            if (ModelState.IsValid)
            {
                _difficultyServices.AddDifficulty(difficulty);
                return RedirectToAction("Index");
            }

            return View(difficulty);
        }

        // GET: Difficulties/Edit/5
        public ActionResult Edit(int id)
        {
            var difficulty = _difficultyServices.GetDifficultyById(id);
            return View(difficulty);
        }

        // POST: Difficulties/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DifficultyId,DifficultyName")] Difficulty difficulty)
        {
            if (ModelState.IsValid)
            {
                _difficultyServices.EditDifficulty(difficulty);
                return RedirectToAction("Index");
            }
            return View(difficulty);
        }
    }
}
