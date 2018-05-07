using System;
using System.Collections.Generic;
using System.Linq;
using FilRouge.MVC.Entities;
using FilRouge.MVC.ViewModels;
using System.Data.Entity;
using FilRouge.MVC.ViewModels.Maps;
using FilRouge.MVC.Models;

namespace FilRouge.MVC.Services
{

	/// <summary>
	/// Services liés au quizz, pdf, gestion, mails, CRUD...
	/// </summary>
	public class QuizzService
	{
        private QuestionService _questionService = new QuestionService();

        #region Properties
        private static Random random = new Random();
		#endregion
		/// <summary>
		/// Constructeur de la classe de QuizzService
		/// </summary>
		public QuizzService() { } //Constructeur

		#region Methods        
		/// <summary>
		/// Méthode permettant d'obtenir un quizz en fonction de son id
		/// Fonctionne avec une fluentQuerry
		/// </summary>
		/// <param name="id">l'ID du quizz (sa clé primaire)</param>
		/// <returns>Retourne un objet Quizz</returns>
		public QuizzViewModel GetQuizz(int id)
		{
			using (FilRougeDBContext db = new FilRougeDBContext())
			{
				var quizz = db.Quizz.Single(e => e.QuizzId == id);
				return quizz.MapToQuizzViewModel();
			}
		}

        public QuizzAnswerReponsesViewModel GetQuizzAnswer(int id)
        {
            using (FilRougeDBContext db = new FilRougeDBContext())
            {
                var quizz = db.Quizz.Include(q => q.Questions)
                                    .Include(q => q.Technology)
                                    .Include(q => q.Difficulty)
                                    .Include(q => q.Contact)
                                    .Single(e => e.QuizzId == id).MapToQuizzAnswerReponsesViewModel();

                return quizz;
            }
        }
        public List<UserReponse> GetQuizzUserAnswer(int id)
        {
            using (FilRougeDBContext db = new FilRougeDBContext())
            {
                var userReponse = db.UserReponse.Include(u => u.Reponse).Where(q => q.QuizzId == id).ToList();

                return userReponse;
            }
        }

        public SortedList<string , int> GetScore(int quizzId)
        {
            var resumeTotaux = new SortedList<string, int>();
            int total = 0;
            int totalChoixLibre = 0;

            var questions = _questionService.GetQuestionsByQuizzId(quizzId);
            var userReponses = GetQuizzUserAnswer(quizzId);
            var questionChoixMultiAndUnique = questions.Where(q => q.AnswerType == AnswerTypeEnum.ChoixUnique || q.AnswerType == AnswerTypeEnum.ChoixMultiple);


                foreach (var question in questionChoixMultiAndUnique)
                {
                    // recup reponse candidat
                    var ReponsesCandidat = userReponses.Where(u => u.Reponse.QuestionId == question.QuestionId).ToList ();
                    // recup les bonnes reponses de la question
                    var BonneReponses = question.Reponses.Where(r => r.TrueReponse == true).ToList();
                    var statusReponse = true;
                    // verifie le compte entre le nb de bonne reponse et le nb de reponse candidat
                    if (ReponsesCandidat.Count() == BonneReponses.Count())
                    {

                        foreach (var rep in ReponsesCandidat)
                        {
                            var verif = BonneReponses.FirstOrDefault(q => q.ReponseId == rep.ReponseId);
                            if (verif == null)
                            {
                                statusReponse = false;
                            }
                        }
                        if (statusReponse == true)
                        {
                           total++;
                        }
                    }
                }

            var questionLibre = questions.Count() - questionChoixMultiAndUnique.Count();
            resumeTotaux.Add("totalReponseChoix", total);
            resumeTotaux.Add("totalQuestionLibre", questionLibre);
            resumeTotaux.Add("totalQCM", questionChoixMultiAndUnique.Count());



            return resumeTotaux;
        }
       

        /// <summary>
        /// Fonction retournant tous les quizz dans une liste de Quizz
        /// Fonctionne avec une fluentQuerry
        /// </summary>
        /// <returns>Retourne tous les quizz</returns>
        public List<QuizzViewModel> GetAllQuizz()
		{
			List<QuizzViewModel> desQuizz = new List<QuizzViewModel>();
			using (FilRougeDBContext db = new FilRougeDBContext())
			{

				try
				{
					IEnumerable<Quizz> fluentQuery = db.Quizz.Select(e => e).ToList();
					if (fluentQuery.Count() == 0)
					{
						throw new ListQuizzEmpty("La liste des quizz est vide");
					}
					foreach (var item in fluentQuery)
					{
						desQuizz.Add(item.MapToQuizzViewModel());
					}

				}
				catch (ListQuizzEmpty lqe)
				{
                    throw new ListQuizzEmpty(lqe.Message);
				}
			}

			return desQuizz;
		}

		/// <summary>
		/// Ajouter une question dans le Quizz.
		/// </summary>
		/// <param name="questionsQuizz"></param>
		/// <param name="lesQuestions"></param>
		/// <param name="questionlibre"></param>
		/// <param name="nombrequestions"></param>
		/// <returns></returns>        
		public static List<Questions> AddQuestionToQuizz(bool questionlibre, int nombrequestions, int technoid, int difficultymasterid, FilRougeDBContext db)
		{

			List<Questions> sortedQuestionsQuizz = new List<Questions>();

			try
			{
				int nbrTotalQuestions = db.Questions.Select(e => e).Count();
				IEnumerable<Questions> AllQuestionsByTechno = db.Questions.Where(e => e.TechnologyId == technoid && e.Active).Select(e => e).ToList();
				IEnumerable<DifficultyRate> RatesQuizz = db.DifficultyRates.Where(e => e.DifficultyMasterId == difficultymasterid).Select(e => e).ToList();

				foreach (var rate in RatesQuizz)
				{//Pour gérer la répartition des questions dans le quizz

					var nbrQuestion = Math.Floor(nombrequestions * rate.Rate);

					for (int i = 0; i < nbrQuestion;)
					{
						var mathRand = random.Next(AllQuestionsByTechno.Count() - 1);

						var question = AllQuestionsByTechno.ElementAt(mathRand);

						if (!(sortedQuestionsQuizz.Contains(question)))
						{
							sortedQuestionsQuizz.Add(question);
							i++;
						}
					}
				}

			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}



			return sortedQuestionsQuizz;
		}

		
		/// <summary>
		/// Sauvegarde de la Reponse du User dans la BDD
		/// </summary>
		/// <param name="questionViewModel"></param>
		public void SaveUserResponse(List<Reponses> reponses, int quizzId)
		{
			
			using (FilRougeDBContext db = new FilRougeDBContext())
			{
				//mapping de la question 
				//var question = questionViewModel.MapToQuestion();

				//Boucle pour les reponses du User
				foreach (var reponse in reponses)
				{
					var uneReponseUser = new UserReponse()
					{
						QuizzId = quizzId,
						ReponseId = reponse.ReponseId,
						//TODO Changer pour recuperer la valeur de la reponse
						Valeur = reponse.Content.ToString()
						
					};
					db.UserReponse.Add(uneReponseUser);
				}
				db.SaveChanges();
			}
		}
		/// <summary>
		/// Sauvegarde de la Reponse et du commentaire du User dans la BDD
		/// </summary>
		/// <param name="questionViewModel"></param>
		public void SaveUserResponse(List<Reponses> reponses, int quizzId, string commentaire)
		{

			using (FilRougeDBContext db = new FilRougeDBContext())
			{
				
				//Boucle pour les reponses du User
				foreach (var reponse in reponses)
				{
					var uneReponseUser = new UserReponse()
					{
						QuizzId = quizzId,
						ReponseId = reponse.ReponseId,
						//TODO Changer pour recuperer la valeur de la reponse
						Valeur = reponse.Content.ToString(),
						Commentaire = commentaire,
						EstRepondu = true
					};
					db.UserReponse.Add(uneReponseUser);
				}
				db.SaveChanges();
			}
		}

		/// <summary>
		/// Création d'un Quizz 
		/// </summary>
		/// <param name="difficultyid"></param>
		/// <param name="technoid"></param>
		/// <param name="userid"></param>
		/// <param name="nomuser"></param>
		/// <param name="prenomuser"></param>
		/// <param name="questionlibre"></param>
		/// <param name="nombrequestions"></param>
		public Quizz CreateQuizz(QuizzViewModel quizzViewModel, int difficultymasterid)
		{
			Quizz unQuizz = null;
			using (FilRougeDBContext db = new FilRougeDBContext())
			{
				var quizz = quizzViewModel.MapToQuizz();

				List<Questions> questionsQuizz = AddQuestionToQuizz(quizz.QuestionLibre, quizz.NombreQuestion, quizz.TechnologyId, difficultymasterid, db);
				int timer = quizz.Timer;


				try
				{
					//TODO creer un viewbag des users => authorize create quizz
					// verifier paramètres de création d'un quizz
					Contact creatingQuizzContact = db.Users.Single(e => e.Id == quizz.ContactId);
					Difficulty difficultyQuizz = db.Difficulties.Single(e => e.DifficultyId == difficultymasterid);
					Technology technoQuizz = db.Technologies.Single(e => e.TechnoId == quizz.TechnologyId);

					unQuizz = new Quizz
					{
						ContactId = creatingQuizzContact.Id,
						DifficultyId = difficultymasterid,
						TechnologyId = quizz.TechnologyId,
						Timer = quizz.Timer,
						PrenomUser = quizz.PrenomUser,
						NomUser = quizz.NomUser,
						NombreQuestion = quizz.NombreQuestion,
						EtatQuizz = quizz.EtatQuizz,
						QuestionLibre = quizz.QuestionLibre,
						Contact = creatingQuizzContact,
						Difficulty = difficultyQuizz,
						Questions = questionsQuizz,
						Technology = technoQuizz
					};
					db.Quizz.Add(unQuizz);
					db.SaveChanges();
					db.Dispose();

				}
				catch (AlreadyInTheQuestionsList e)
				{
					Console.WriteLine(e.Message);
					db.Dispose();
				}
				catch (NoQuestionsForYou e)
				{
					Console.WriteLine(e.Message);
					db.Dispose();
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
			return unQuizz;

		}

		/// <summary>
		/// Retourne la première question non validée d'un Quizz
		/// **En-Cours**
		/// </summary>
		/// <param name="quizzId"></param>
		/// <returns></returns>
		public QuestionViewModel GetNextQuestion(int quizzId)
		{
			var question = new QuestionViewModel();

			using (var dbContext = new FilRougeDBContext())
			{
				// On récupère la liste des question Ids d'un Quizz
				var questionIds = dbContext.Quizz.Find(quizzId).Questions.Select(q => q.QuestionId).Distinct();
				// // On récupère la liste des question Ids  qui ont déjà une réponse
				var questionReponduIds = dbContext.UserReponse.Where(q => q.QuizzId == quizzId).Select(u => u.Reponse.QuestionId).Distinct();

				//// On récupère les questions sans réponse 
				var questionSansReponse = questionIds.Where(q => !questionReponduIds.Contains(q)).OrderBy(q => q).FirstOrDefault();

				//recuperer la première question à partir des questions sans réponse
				var questionEntitie = dbContext.Questions.FirstOrDefault(q => q.QuestionId == questionSansReponse);
								
				 question = questionEntitie.MapToQuestionsViewModel();
			}
			return question;
		}

        public int EditQuizz(QuizzViewModel quizzViewModel)
        {
            int id = 0;
            using (var db = new FilRougeDBContext())
            {
                var quizz = db.Quizz.Find(quizzViewModel.QuizzId);

                quizz.NomUser = quizzViewModel.NomUser;
                quizz.PrenomUser = quizzViewModel.PrenomUser;

                db.Entry(quizz).State = EntityState.Modified;
                db.SaveChanges();

                id = quizz.QuizzId;
            }
            return id;                
        }

		#endregion
	}
}
