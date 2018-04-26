using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilRouge.MVC.Services;
using FilRouge.MVC.ViewModels;
using FilRouge.MVC.ViewModels.Maps;

namespace FilRouge.MVC.Controllers
{
	public class QuizzController : Controller
	{
		private readonly QuizzService _quizzService = new QuizzService();
		private readonly QuestionService _questionService = new QuestionService();
		private readonly ReferencesService _referencesService = new ReferencesService();
		private readonly DifficultyServices _difficultyServices = new DifficultyServices();
		private readonly TechnologiesService _technologiesService = new TechnologiesService();

		// GET: Quizz
		public ActionResult Index()
		{
			return View();
		}
		public ActionResult Quizz()
		{
			var quizzs = _quizzService.GetAllQuizz();

			return View("Quizz", quizzs);
		}

		/// <summary>
		/// Get:   Création d'un Quizz
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Create()
		{

			var technologiesListItem = _technologiesService.GetListItemsTechnologies();
			var difficultiesListItem = _difficultyServices.GetListItemsDifficulties();

			ViewBag.Difficulties = difficultiesListItem;
			ViewBag.Technologies = technologiesListItem;

			return View(new QuizzViewModel());
		}

		/// <summary>
		/// Post : Création d'un Quizz
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Create(QuizzViewModel quizzViewModel)
		{

			var technologiesListItem = _technologiesService.GetListItemsTechnologies();
			var difficultiesListItem = _difficultyServices.GetListItemsDifficulties();

			ViewBag.Difficulties = difficultiesListItem;
			ViewBag.Technologies = technologiesListItem;


			var masterID = 1;
			if (ModelState.IsValid)
			{
				var quizz = _quizzService.CreateQuizz(quizzViewModel, masterID);
				//return RedirectToAction("Index", "Home");
			}

			return View(quizzViewModel);
		}

			[HttpGet]
		public ActionResult Test(int id)
		{
			QuizzViewModel quizz = _quizzService.GetQuizz(id);
			if (ViewBag.ListQuestion == null)
			{
				ViewBag.ListQuestion = _questionService.GetQuestionsByQuizzId(quizz.QuizzId);
			}

			//ViewBag.ReponseByQuestionId = _questionService.GetReponseByQuestionId();
			//System.Tuple<>
			//ViewBag.nextIndex = index + 1;

			return View("QuizzAnswers", quizz);
		}

		/// <summary>
		/// Post : Création d'un Quizz
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Test(int quizzId, int index = 0)
		{
			var quizz = _quizzService.GetQuizz(quizzId);
			if (ViewBag.QuestionListByQuizz == null)
			{
				ViewBag.QuestionListByQuizz = _questionService.GetQuestionsByQuizzId(quizz.QuizzId);
			}
			
			//ViewBag.ReponseByQuestionId = _questionService.GetReponseByQuestionId()
			//System.Tuple<>
			//ViewBag.nextIndex = index + 1;

			return View("Test", "Quizz", quizz);
			
		}
	
		/// <summary>
		/// Retourne la vue partiel et et le questionViewmodel 
		/// </summary>
		/// <param name="quizzId"></param>
		/// <returns></returns>
		[AcceptVerbs("GET", "POST")]
		public PartialViewResult QuestionQuizz(int quizzId)
		{
			var nextQuestion = _quizzService.GetNextQuestion(quizzId);

			// TODO sauvegarder la reponse saisie par l'user (le ViewModel qui contient une reponse à une question)
			// 2eme recuperer le nouveau ViewModel qui contient une reponse a une question par l'id de la question

			// retourner le viewModel qui contient une reponse par l'id de la question
			return PartialView("_ReponseQuestionQuizz", nextQuestion);
		}
	}
}
