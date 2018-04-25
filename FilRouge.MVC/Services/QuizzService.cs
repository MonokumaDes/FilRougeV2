using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using FilRouge.MVC.Entities;

namespace FilRouge.MVC.Services
{
    /// <summary>
    /// Services liés au quizz, pdf, gestion, mails, CRUD...
    /// </summary>
    public class QuizzService 
    {
        #region Properties

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
        public Quizz GetQuizz(int id)
        {
            Quizz fluentQuery = new Quizz();
            FilRougeDBContext db = new FilRougeDBContext();
            try
            {
                fluentQuery = db.Quizz.Single(e => e.QuizzId == id);
                if(fluentQuery == null)
                {
                    throw new WrongIdQuizz("L'id saisie n'existe pas");
                }
                db.Dispose();                
            }
            catch(FormatException)
            {
                db.Dispose();
                Console.WriteLine("Veuillez saisir un id valide");
            }
            return fluentQuery;
        }
        /// <summary>
        /// Fonction retournant tous les quizz dans une liste de Quizz
        /// Fonctionne avec une fluentQuerry
        /// </summary>
        /// <returns>Retourne tous les quizz</returns>
        public List<Quizz> GetAllQuizz()
        {
            List<Quizz> desQuizz = new List<Quizz>();
            FilRougeDBContext db = new FilRougeDBContext();
            try
            {   
                IQueryable<Quizz> fluentQuery = db.Quizz.Select(e => e);
                if(fluentQuery.Count() == 0)
                {
                    throw new ListQuizzEmpty("La liste des quizz est vide");
                }
                foreach (var item in fluentQuery)
                {
                    desQuizz.Add(item);
                }
                db.Dispose();
            }
            catch (ListQuizzEmpty)
            {
                db.Dispose();
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
        public static List<Questions> AddQuestionToQuizz(bool questionlibre, int nombrequestions, int technoid, int difficultymasterid)
        {
            Random rand = new Random();
            List<Questions> sortedQuestionsQuizz = new List<Questions>();

            FilRougeDBContext db = new FilRougeDBContext();
            try
            {
                int nbrTotalQuestions = db.Questions.Select(e => e).Count();
                IQueryable<Questions> AllQuestionsByTechno = db.Questions.Where(e => e.TechnologyId == technoid && e.Active);
                IQueryable<DifficultyRate> RatesQuizz = db.DifficultyRates.Where(e => e.DifficultyMasterId == difficultymasterid);

                foreach (var rate in RatesQuizz)
                {//Pour gérer la répartition des questions dans le quizz
                    for (int i = 0; i < Math.Floor(nombrequestions * rate.Rate); i++)
                    {
                        foreach (var question in AllQuestionsByTechno)
                        {//Vérification par id de la présence d'une question
                            if(!(sortedQuestionsQuizz.Contains(question)))
                            {
                                sortedQuestionsQuizz.Add(question);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                db.Dispose();
            }
           

            return sortedQuestionsQuizz;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="difficultyid"></param>
        /// <param name="technoid"></param>
        /// <param name="userid"></param>
        /// <param name="nomuser"></param>
        /// <param name="prenomuser"></param>
        /// <param name="questionlibre"></param>
        /// <param name="nombrequestions"></param>
        public static void CreateQuizz(int userid, int difficultymasterid, int technoid,string nomuser, string prenomuser, bool questionlibre, int nombrequestions)
        {
            List<Questions> questionsQuizz = AddQuestionToQuizz(questionlibre, nombrequestions, technoid, difficultymasterid);
            //int timer = 0;
            FilRougeDBContext db = new FilRougeDBContext();
           /* try
            {
                Contact creatingQuizzContact = db.Contact.Single(e => e.UserId == userid);
                Difficulty difficultyQuizz = db.Difficulties.Single(e => e.DifficultyId == difficultymasterid);
                Technology technoQuizz = db.Technologies.Single(e => e.TechnoId == technoid);
                
                Quizz unQuizz = new Quizz
                {
                    ContactId = userid,
                    DifficultyId = difficultymasterid,
                    TechnologyId = technoid,
                    Timer = timer,
                    PrenomUser = prenomuser,
                    NomUser = nomuser,
                    NombreQuestion = nombrequestions,
                    EtatQuizz = 0,
                    QuestionLibre = questionlibre,
                    Contact = creatingQuizzContact,
                    Difficulty = difficultyQuizz,
                    Questions = questionsQuizz,
                    Technology = technoQuizz
                };
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
                db.Dispose();
            }*/
        }
        #endregion
    }
}
