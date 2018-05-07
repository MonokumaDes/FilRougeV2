using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilRouge.MVC.Entities;
using FilRouge.MVC.Models;
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
        private readonly ContactService _contactService = new ContactService();

		// GET: Quizz
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
            var contactListItem = _contactService.GetListItemContact();

			ViewBag.Difficulties = difficultiesListItem;
			ViewBag.Technologies = technologiesListItem;
            ViewBag.Contact = contactListItem;

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
                quizzViewModel.EtatQuizz = 0;
                quizzViewModel.Timer = 0;
				var quizz = _quizzService.CreateQuizz(quizzViewModel, masterID);
				return RedirectToAction("Quizz", "Quizz");
			}

			return View(quizzViewModel);
		}

		/// <summary>
		/// Get : quizz pour le test
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Test(int id)
		{
			QuizzViewModel quizz = _quizzService.GetQuizz(id);
			if (ViewBag.ListQuestion == null)
			{
				ViewBag.ListQuestion = _questionService.GetQuestionsByQuizzId(quizz.QuizzId);
			}

			return View("QuizzAnswers", quizz);
		}

		/// <summary>
		///  Post : Création d'un Quizz
		/// </summary>
		/// <param name="quizzId"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Test(int quizzId, int index = 0)
		{
			var quizz = _quizzService.GetQuizz(quizzId);
			if (ViewBag.QuestionListByQuizz == null)
			{
				ViewBag.QuestionListByQuizz = _questionService.GetQuestionsByQuizzId(quizz.QuizzId);
			}
			
			return View("Test", "Quizz", quizz);
			
		}

		/// <summary>
		///  Retourne la vue partiel et et le questionViewmodel 
		/// </summary>
		/// <param name="quizzId"></param>
		/// <param name="question"></param>
		/// <returns></returns>
		[AcceptVerbs("GET", "POST")]
		public PartialViewResult QuestionQuizz(int quizzId , QuestionViewModel question)
		{
			
			#region Sauvegarde de la reponse User				

			if (question.QuestionId != 0)
			{
				// recupère la valeur questionId de la requête actuelle
				question.QuestionId = Int32.Parse(Request.Form.GetValues("QId")[0]);

				List<Reponses> reponsesUser = new List<Reponses>();
				// recupere la question en cours 
				var questionQui = _questionService.GetQuestion(question.QuestionId);

				// Si la question est une saisie libre
				if (questionQui.AnswerType == AnswerTypeEnum.SaisieLibre)				{

					// Valeur de la textaera
					var reponseLibre = Request.Form.GetValues("reponseLibre")[0];

					Reponses rLibre = new Reponses();

					// Set les valeurs de la réponse 
					rLibre.QuestionId = question.QuestionId;
					rLibre.Content = reponseLibre;
					rLibre.ReponseId = questionQui.Reponses[0].ReponseId;

					reponsesUser.Add(rLibre);
				}

				else
				{
					foreach (var reponse in questionQui.Reponses)
					{
						//pour chaque reponse on verifie si la checkbox est cochée
						var reponseChecked = Request.Form.GetValues("BonneReponse" + reponse.ReponseId)[0];

						// Si true, on sauvegarde la reponse de l'utilisateur dans la liste
						if (reponseChecked.ToLower() == "true")
						{
							reponse.QuestionId = question.QuestionId;
							reponsesUser.Add(reponse);
						}

					}
				}
			
				// Sauvergarde les reponse du User
				_quizzService.SaveUserResponse(reponsesUser, quizzId);				
			}
			#endregion

			var nextQuestion = _quizzService.GetNextQuestion(quizzId);

			// Check si la question suivante est nulle
			if (nextQuestion.QuestionId == 0)
			{
				// retourner la vue de fin de quizz
				return PartialView("_FinQuizz");
			}			

			// retourner le viewModel qui contient une reponse par l'id de la question
			return PartialView("_ReponseQuestionQuizz", nextQuestion);
		}

		/// <summary>
		/// Get  : Edition d'un quizz via son Id
		/// </summary>
		/// <param name="quizzId"></param>
		/// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int quizzId)
        {
            var quizz = _quizzService.GetQuizz(quizzId);

            return View(quizz);
        }

		/// <summary>
		///  Post  : Edition d'un quizz via son Id
		/// </summary>
		/// <param name="quizzViewModel"></param>
		/// <returns></returns>
		[HttpPost]
        public ActionResult Edit(QuizzViewModel quizzViewModel)
        {

            if(ModelState.IsValid)
            {
                _quizzService.EditQuizz(quizzViewModel);
                return RedirectToAction("Quizz", "Quizz");
            }
            return View(quizzViewModel);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            QuizzAnswerReponsesViewModel quizz = _quizzService.GetQuizzAnswer(id);
            ViewBag.UserReponses = _quizzService.GetQuizzUserAnswer(id);

            ViewBag.Scores = _quizzService.GetScore(id);

            return View(quizz);
        }

    }
}
