using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilRouge.MVC.Entities;

namespace FilRouge.MVC.ViewModels.Maps
{
	public static class QuestionMap
	{
		#region Mapping Question / QuestionViewModel
		/// <summary>
		///  Convertir/ mapper une "question" en  ViewModel "QuestionsViewModel"
		/// </summary>
		/// <param name="question"></param>
		/// <returns></returns>
		public static QuestionViewModel MapToQuestionsViewModel(this Questions question)
		{
			var questionsViewModel = new QuestionViewModel();
			if (question == null)
				return questionsViewModel;
            questionsViewModel = new QuestionViewModel
            {
                QuestionId = question.QuestionId,
                Content = question.Content,
                Commentaire = question.Commentaire,
                Active = question.Active,
                QuestionType = question.TypeQuestion,
                Technology = question.Technology,
                Difficulty = question.Difficulty,
                Reponses = question.Reponses.ToList()
				//TODO	Rajouter difficultiesID quand pret
			};
			return questionsViewModel;

		}

		/// <summary>
		///  Convertir/ mapper une "QuestionsViewModel" en  ViewModel "question"
		/// </summary>
		/// <param name="questionsViewModel"></param>
		/// <returns></returns>
		public static Questions MapToQuestion(this QuestionViewModel questionsViewModel)
		{
			var question = new Questions();
			if (questionsViewModel == null)
				return question;
			question = new Questions
			{
				QuestionId = questionsViewModel.QuestionId,
				Content = questionsViewModel.Content,
				Commentaire = questionsViewModel.Commentaire,
				Active = questionsViewModel.Active,
				TechnologyId = questionsViewModel.Technology.TechnoId,
				QuestionTypeId = questionsViewModel.QuestionType.TypeQuestionId,
				DifficultyId = questionsViewModel.Difficulty.DifficultyId,
                Reponses = questionsViewModel.Reponses
			};
			return question;
		}

		#endregion
	}
}