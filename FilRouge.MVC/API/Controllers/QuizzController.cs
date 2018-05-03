using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FilRouge.MVC.Services;
using FilRouge.MVC.ViewModels;

namespace FilRouge.MVC.API.Controllers
{
    //[Authorize]
    public class QuizzController : ApiController
    {
        QuizzService _quizzService = new QuizzService();

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

    }
}
