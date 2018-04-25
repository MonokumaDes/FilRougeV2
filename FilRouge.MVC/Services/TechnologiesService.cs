using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilRouge.MVC.Entities;

namespace FilRouge.MVC.Services
{
	public class TechnologiesService
	{
		/// <summary>
		/// Methode d'ajout d'une technologie
		/// </summary>
		/// <param name="technology"></param>
		public void AddTechnology(Technology technology)
		{

			using (var dbContext = new FilRougeDBContext())
			{
				dbContext.Technologies.Add(technology);
				dbContext.SaveChanges();
			}
		}
		/// <summary>
		/// Retourne la technologie par son "Id"
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Technology GetTechnology(int? id)
		{
			var technolgie = new Technology();
			using (var dbContext = new FilRougeDBContext())
			{

				technolgie = dbContext.Technologies.Find(id);
			}

			return technolgie;
		}

		/// <summary>
		/// Retourne la liste des technologies
		/// </summary>
		/// <returns></returns>
		public List<Technology> GetAllTechnologies()
		{
			var listTechnologies = new List<Technology>();
			using (var dbContext = new FilRougeDBContext())
			{

				listTechnologies = dbContext.Technologies.ToList();
			}

			return listTechnologies;
		}

		/// <summary>
		/// Edition d'une technologie par son "Id"
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Technology EditTechnology(Technology technology)
		{
			var technolgie = new Technology();
			using (var dbContext = new FilRougeDBContext())
			{

				dbContext.Entry(technology).State = EntityState.Modified;
				dbContext.SaveChanges();
			}

			return technolgie;
		}
		/// <summary>
		/// Suppression d'une technologie par son id
		/// </summary>
		/// <param name="id"></param>
		public void RemoveTechnology(int id)
		{
			var technologie = new Technology();
			using (var dbContext = new FilRougeDBContext())
			{

				technologie = dbContext.Technologies.Find(id);
				dbContext.Technologies.Remove(technologie);
				dbContext.SaveChanges();
			}
		}


		/// <summary>
		/// Retourne une liste d'item Technologie pour le viewbag
		/// </summary>
		/// <returns></returns>
		public List<SelectListItem> GetListItemsTechnologies()
		{

			var technologiesListItem = new List<SelectListItem>();


			using (var dbContext = new FilRougeDBContext())
			{
				var technologies = dbContext.Technologies;

				foreach (var technology in technologies)
				{
                    technologiesListItem.Add(new SelectListItem()
                    {
                        Text = technology.TechnoName,
                        Value = technology.TechnoId.ToString()
                    });
				}

				return technologiesListItem;
			}
		}
	}
}