using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilRouge.MVC.Entities;
using FilRouge.MVC.Services;

namespace FilRouge.MVC.ViewModels.Maps
{
    public static class QuizzAnswerReponsesMap
    {
        
        public static QuizzAnswerReponsesViewModel MapToQuizzAnswerReponsesViewModel(this Quizz quizz)
        {
           //QuizzService _quizzService = new QuizzService();
            //var userReponse = _quizzService.GetQuizzUserAnswer(quizz.QuizzId);

            var quizzAnswerReponsesViewModel = new QuizzAnswerReponsesViewModel();
            if (quizz == null)
                return quizzAnswerReponsesViewModel;

            quizzAnswerReponsesViewModel = new QuizzAnswerReponsesViewModel
            {
                QuizzId = quizz.QuizzId,
                ContactName = quizz.Contact.Name,
                ContactEmail = quizz.Contact.Email,
                DifficultyName = quizz.Difficulty.DifficultyName,
                TechnologyName = quizz.Technology.TechnoName,
                //UserReponses = userReponse,
                EtatQuizz = quizz.EtatQuizz,
                NombreQuestion = quizz.NombreQuestion,
                NomUser = quizz.NomUser,
                PrenomUser = quizz.PrenomUser,
                QuestionLibre = quizz.QuestionLibre,
                Questions = quizz.Questions.Select(q => q.MapToQuestionsViewModel()).ToList()
                //Technology = quizz.Technology
            };

            return quizzAnswerReponsesViewModel;

        }
    }
}