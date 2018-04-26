using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilRouge.MVC.Entities;

namespace FilRouge.MVC.ViewModels.Maps
{
	public static class QuizzMap
	{
		#region Mapping Question / QuestionViewModel
		/// <summary>
		///  Convertir/ mapper une "quizz" en  ViewModel "quizzViewModel"
		/// </summary>
		/// <param name="quizz"></param>
		/// <returns></returns>
		public static QuizzViewModel MapToQuizzViewModel(this Quizz quizz)
		{
			var quizzViewModel = new QuizzViewModel();
			if (quizz == null)
				return quizzViewModel;
			quizzViewModel = new QuizzViewModel
			{
				QuizzId = quizz.QuizzId,
				EtatQuizz = quizz.EtatQuizz,
				NombreQuestion = quizz.NombreQuestion,
				NomUser = quizz.NomUser,
				PrenomUser = quizz.PrenomUser,
				TechnologyId = quizz.TechnologyId,
				DifficultyId = quizz.DifficultyId,
				QuestionLibre = quizz.QuestionLibre,
				Timer = quizz.Timer,
				QuestionId = quizz.Questions.Select(o => o.QuestionId).ToList()

				//TODO	Rajouter difficultiesID quand pret

			};
			return quizzViewModel;

		}

		/// <summary>
		///  Convertir/ mapper une "QuizzViewModel" en "quizz"
		/// </summary>
		/// <param name="quizzViewModel"></param>
		/// <returns></returns>
		public static Quizz MapToQuizz(this QuizzViewModel quizzViewModel)
		{
			var quizz = new Quizz();
			if (quizzViewModel == null)
				return quizz;
			quizz = new Quizz
			{
				QuizzId = quizzViewModel.QuizzId,
				EtatQuizz = quizzViewModel.EtatQuizz,
				NombreQuestion = quizzViewModel.NombreQuestion,
				NomUser = quizzViewModel.NomUser,
				PrenomUser = quizzViewModel.PrenomUser,
				TechnologyId = quizzViewModel.TechnologyId,
				DifficultyId = quizzViewModel.DifficultyId,

			};
			return quizz;

		}

		#endregion
	}
}
