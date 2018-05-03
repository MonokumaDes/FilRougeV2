using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FilRouge.MVC.Entities;
using FilRouge.MVC.Services;
using FilRouge.MVC.ViewModels;
using FilRouge.Web.ViewModels.Maps;

namespace FilRouge.MVC.API.Controllers
{
    //[Authorize]
    public class QuizzController : ApiController
    {
        QuizzService _quizzService = new QuizzService();
        QuestionService _questionService = new QuestionService();

        // GET: api/Quizz
        public IHttpActionResult GetAllQuizz()
        {
            try
            {
                List<QuizzViewModel> quizzViewModels = _quizzService.GetAllQuizz();
                return Ok(quizzViewModels);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        // GET: api/Quizz/5
        public IHttpActionResult GetQuizz(int id)
        {
            try
            {
                QuizzViewModel quizzViewModel = _quizzService.GetQuizz(id);
                return Ok(quizzViewModel);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        //GET : api/quizz/{idQuizz}/question
        /// <summary>
        /// Permet de récupérer les informations de la prochaine question
        /// </summary>
        /// <param name="id">id de la question</param>
        /// <returns></returns>
        [Route("api/quizz/{idQuizz}/question")]
        public IHttpActionResult GetNextQuestion(int idQuizz)
        {
            try
            {
                var nextQuestion = _quizzService.GetNextQuestion(idQuizz);
                QuestionViewModel questionViewModel = _questionService.GetQuestion(nextQuestion.QuestionId);

                List<object> lesReponses = new List<object>();

                foreach (var rep in questionViewModel.Reponses)
                {
                    lesReponses.Add( new
                    {
                        rep.ReponseId,
                        rep.Content,
                    }
                        
                    );
                }

                var objetRenvoyer = new
                {
                    questionViewModel.QuestionId,
                    questionViewModel.Content,
                    AnswerType = questionViewModel.AnswerType.ToString(),
                    questionViewModel.Commentaire,               
                    
                    //ok car nouvelle liste qui n'inclut pas un objet question
                    Reponses = lesReponses,

                   //cause des erreurs
                   /*questionViewModel.Difficulty,
                   questionViewModel.Technology,*/
                   //questionViewModel.QuestionType,
                };

                return Ok(objetRenvoyer);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        //POST : api/quizz/{idQuizz}/question
        /// <summary>
        /// Enregistre les reponses du candidat, recoit un JSON qui contient le commentaire et la liste des reponse cocher par le cnadidat
        /// </summary>
        /// <param name="reponsesCandidat"></param>
        /// <param name="idQuizz"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/quizz/{idQuizz}/question")]
        public IHttpActionResult SaveUserResponse(ReponseCandidat reponsesCandidat, int idQuizz)
        {
            try
            {
                _quizzService.SaveUserResponse(reponsesCandidat.Reponses, idQuizz, reponsesCandidat.Commentaire);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        //class a deplacer
        public class ReponseCandidat
        {
            public string Commentaire { get; set; }
            public List<Reponses> Reponses { get; set; }
        }


/*format JSON a envoyer
{
	"Commentaire": "mon commentaire",
	"Reponses": [
		{
			"ReponseId": 1,
			"Content": "16"
		},
		{
			"ReponseId": 2,
			"Content": "12+4"
		},
		{
			"ReponseId": 3,
			"Content": "13"
		},
		{
			"ReponseId": 4,
			"Content": "14"
		}
	]
}

        */
    }
}
