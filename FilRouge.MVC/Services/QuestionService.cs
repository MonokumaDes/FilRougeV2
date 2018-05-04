using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using FilRouge.MVC.Entities;
using FilRouge.MVC.ViewModels;
using FilRouge.MVC.ViewModels.Maps;

namespace FilRouge.MVC.Services
{
	public class QuestionService
	{
		private ReferencesService _referencesService = new ReferencesService();
		private readonly DifficultyServices _difficultyServices = new DifficultyServices();
		private readonly TechnologiesService _technologiesService = new TechnologiesService();
		private readonly TypeQuestionsService _typeQuestionsService = new TypeQuestionsService();

		#region CRUD Question 
		/// <summary>
		/// Ajouter une question dans la BDD
		/// </summary>
		/// <param name="questionsViewModel"></param>
		/// <returns></returns>
		public int AddQuestion(QuestionViewModel questionsViewModel)
		{
			var id = 0;
			using (var dbContext = new FilRougeDBContext())
			{
				var question = questionsViewModel.MapToQuestion();

				dbContext.Questions.Add(question);
				dbContext.SaveChanges();
				id = question.QuestionId;
			}

			return id;
		}

		/// <summary>
		/// Retourne une question par son "ID"
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public QuestionViewModel GetQuestion(int? id)
		{
			var question = new Questions();

			using (var dbContext = new FilRougeDBContext())
			{
				question = dbContext.Questions
                    .Include(e => e.Technology)
                    .Include(q => q.TypeQuestion).Include(q => q.Difficulty).Include(q => q.Reponses)
                    .Where(q => q.QuestionId == id)
                    .Select(q => q).First();
			}
			return question.MapToQuestionsViewModel();
		}

		/// <summary>
		/// Edition d'une question 
		/// </summary>
		/// <param name="questionViewModel"></param>
		/// <returns></returns>
		public int EditQuestion(QuestionViewModel questionViewModel)
		{
			var id = 0;
			using (var dbContext = new FilRougeDBContext())
			{
				var question = dbContext.Questions.Find(questionViewModel.QuestionId);
				question.Commentaire = questionViewModel.Commentaire;
				question.Content = questionViewModel.Content;
				question.Active = questionViewModel.Active;
				question.DifficultyId = questionViewModel.Difficulty.DifficultyId;
                //question.QuestionTypeId = questionViewModel.QuestionType.TypeQuestionId;
                question.QuestionTypeId = (int)questionViewModel.AnswerType;
				question.TechnologyId = questionViewModel.Technology.TechnoId;

				dbContext.Entry(question).State = EntityState.Modified;
				dbContext.SaveChanges();

				id = question.QuestionId;
			}

			return id;
		}

		/// <summary>
		/// Retourne la list des questions
		/// </summary>
		/// <returns></returns>
		public List<QuestionViewModel> GetAllQuestions()
		{
			var questionViewModels = new List<QuestionViewModel>();
			using (var dbContext = new FilRougeDBContext())
			{
				var questionEntities = dbContext.Questions.ToList();
				foreach (var question in questionEntities)
				{
					questionViewModels.Add(question.MapToQuestionsViewModel());
				}
			}

			return questionViewModels;
		}
		/// <summary>
		/// Retourne la liste des questions d'un Quizz
		///** En_cours**
		/// </summary>
		/// <param name="quizzId"></param>
		/// <returns></returns>
		public List<QuestionViewModel> GetQuestionsByQuizzId(int quizzId)
		{
			var questionViewModels = new List<QuestionViewModel>();

			using (var dbContext = new FilRougeDBContext())
			{
				var questionEntities = dbContext.Quizz.Find(quizzId).Questions;

					foreach (var question in questionEntities)
				{
					questionViewModels.Add(question.MapToQuestionsViewModel());
				}
				
			}

			return questionViewModels;
		}

		public int AddReponsesToQuestion(int id ,List<Reponses> reponses)
		{
			using (var db = new FilRougeDBContext())
			{
				var questionAddReponse = db.Questions.Find(id);

				questionAddReponse.Reponses = reponses; // ajout de la liste des réponses à la question
				int nbRes = db.SaveChanges();
				return nbRes;
			}
		}

        public int EditReponsesToQuestion(int id, List<Reponses> reponses)
        {
            using (var db = new FilRougeDBContext())
            {
                var questionEditReponses = db.Questions.Find(id).Reponses.ToList();

                for(int i = 0; i < questionEditReponses.Count; i++)
                {
                    questionEditReponses[i].Content = reponses[i].Content;
                    questionEditReponses[i].TrueReponse = reponses[i].TrueReponse;
                }
                int nbRes = db.SaveChanges();
                return nbRes;
            }
        }
		#endregion
	}
}