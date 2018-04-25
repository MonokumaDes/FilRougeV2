using FilRouge.MVC.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilRouge.MVC.Services
{
	public class TypeQuestionsService
	{

		public void AddTypeQuestion(TypeQuestion typeQuestion)
		{

			using (var dbContext = new FilRougeDBContext())
			{
				dbContext.TypeQuestion.Add(typeQuestion);
				dbContext.SaveChanges();
			}
		}
		/// <summary>
		/// Retourne le type de question par son "Id"
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public TypeQuestion GetTypeQuestion(int? id)
		{
			var typeQuestion = new TypeQuestion();
			using (var dbContext = new FilRougeDBContext())
			{

				typeQuestion = dbContext.TypeQuestion.Find(id);
			}

			return typeQuestion;
		}
		/// <summary>
		/// Retourne la List de type de question
		/// </summary>
		/// <returns></returns>
		public List<TypeQuestion> GetAllTypeQuestion()
		{
			var listTypeQuestion = new List<TypeQuestion>();
			using (var dbContext = new FilRougeDBContext())
			{

				listTypeQuestion = dbContext.TypeQuestion.ToList();
			}

			return listTypeQuestion;
		}

		/// <summary>
		/// Edition d'un type de question
		/// </summary>
		/// <param name="typeQuestion"></param>
		/// <returns></returns>
		public TypeQuestion EditTypeQuestion(TypeQuestion typeQuestion)
		{

			using (var dbContext = new FilRougeDBContext())
			{

				dbContext.Entry(typeQuestion).State = EntityState.Modified;
				dbContext.SaveChanges();
			}

			return typeQuestion;
		}
		/// <summary>
		/// Suppression d'un type de question
		/// </summary>
		/// <param name="id"></param>
		public void RemoveTypeQuestion(int id)
		{
			var typeQuestion = new TypeQuestion();
			using (var dbContext = new FilRougeDBContext())
			{

				typeQuestion = dbContext.TypeQuestion.Find(id);
				dbContext.TypeQuestion.Remove(typeQuestion);
				dbContext.SaveChanges();
			}

		}

		/// <summary>
		/// Retourne une liste d'item pour les types de question
		/// pour le viewbag
		/// </summary>
		/// <returns></returns>
		public List<SelectListItem> GetListItemQuestionType()
		{
			var questionTypeListItem = new List<SelectListItem>();

			using (var dbContext = new FilRougeDBContext())
			{
				var typeQuestion = dbContext.TypeQuestion;

				foreach (var type in typeQuestion)
				{
					questionTypeListItem.Add(new SelectListItem()
					{
						Text = type.NameType,
						Value = type.TypeQuestionId.ToString()

					});
				}
				return questionTypeListItem;
			}
		}

	}
}