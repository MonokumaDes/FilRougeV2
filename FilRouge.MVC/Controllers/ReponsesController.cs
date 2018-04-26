using System.Collections.Generic;
using System.Web.Mvc;
using FilRouge.MVC.Entities;
using FilRouge.MVC.Services;
using FilRouge.MVC.ViewModels;
using FilRouge.MVC.ViewModels.Maps;
using FilRouge.Web.ViewModels;
using FilRouge.Web.ViewModels.Maps;

namespace FilRouge.Web.Controllers
{
    public class ReponsesController : Controller
    {
        private QuestionService _questionService = new QuestionService();
        private ReponsesServices _reponseService = new ReponsesServices();

        // GET: Reponse/Create/id
        [HttpGet]
        public ActionResult Create(int id)
        {
            var question = _questionService.GetQuestion(id);
            ViewBag.Question = question;
            return View(new ReponseViewModel());
        }

        // POST: Reponses/Create/id
        [HttpPost]
        public ActionResult Create(int id, FormCollection collection)
        {
            var question = _questionService.GetQuestion(id).MapToQuestion();
            ViewBag.Question = question;

            List<Reponses> reponses = new List<Reponses>();

            //récupération des 4 choix et l'ajouter dans liste "reponses"
            for (int i = 0; i <= 3; i++)
            {
                Reponses reponse = new Reponses
                {
                    QuestionId = question.QuestionId,
                    Content = collection.GetValue("[" + i + "].Reponse"+ i).AttemptedValue,
                    TrueReponse = collection.GetValue("[" + i + "].BonneReponse" + i).AttemptedValue == "false" ? false : true //prend false si pas cocher
                };
                if(reponse.Content != "")
                    reponses.Add(reponse);
            }

			/* /!\ impossible d'utiliser la méthode Edit du serviceQuestion, car le "ViewModel" et le "Mapping" ne prend
            ** pas en compte la liste des réponses */
			//using (var db = new FilRougeDBContext())
			//{
			//    var questionAddReponse = db.Questions.Find(id);

			//    questionAddReponse.Reponses = reponses; // ajout de la liste des réponses à la question
			//    int nbRes = db.SaveChanges();
			//    if (nbRes > 0)
			//    {
			//        return RedirectToAction("Questions", "Questions");
			//    }
			//}

			var nbRes = _questionService.AddReponsesToQuestion(id, reponses);
			if (nbRes > 0)
			{
				return RedirectToAction("Questions", "Questions");
			}

			return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var reponsesViewModel = new List<ReponseViewModel>();
            var question = _questionService.GetQuestion(id);
            foreach(var rep in question.Reponses)
            {
                reponsesViewModel.Add(rep.MapToReponseViewModel());
            }
            
            ViewBag.Question = question;

            return View(reponsesViewModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var question = _questionService.GetQuestion(id);
            ViewBag.Question = question;

            List<Reponses> reponses = _reponseService.GetReponsesById(id);

            //récupération des 4 choix et l'ajouter dans liste "reponses"
            for (int i = 0; i < reponses.Count; i++)
            {
                reponses[i].QuestionId = question.QuestionId;
                reponses[i].Content = collection.GetValue("[" + i + "].Reponse" + reponses[i].ReponseId).AttemptedValue;
                reponses[i].TrueReponse = collection.GetValue("[" + i + "].BonneReponse" + reponses[i].ReponseId).AttemptedValue == "false" ? false: true;
            }

            question.MapToQuestion();

            var nbRes = _questionService.EditReponsesToQuestion(id, reponses);
            if (nbRes > 0)
            {
                return RedirectToAction("Questions", "Questions");
            }
            return View();
        }

    }
}