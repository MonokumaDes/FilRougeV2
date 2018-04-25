using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FilRouge.MVC.Entities;

namespace FilRouge.MVC.Services
{
    /// <summary>
    /// Classe ReferencesService permettant d'utiliser les entités associés au Quizz
    /// Difficulté et Technologies
    /// </summary>
    public class ReferencesService
    {
        #region Properties
        #endregion
        /// <summary>
        /// Constructeur de la classe permettant d'utiliser les services associés
        /// </summary>
        public ReferencesService() { }


        #region Methods
        /// <summary>
        /// Cette fonction permet d'obtenir toutes les technologies
        /// Fonctionne avec une fluentQuerry
        /// </summary>
        /// <returns>Retourne une liste d'objets Technologies</returns>
        public List<Technology> GetTechnologies()
        {
            
            List<Technology> desTechnologies = new List<Technology>();
            //FilRougeDBContext db = new FilRougeDBContext();
            using (var db = new FilRougeDBContext())
            {
                desTechnologies = db.Technologies.Select(e => e).ToList();
            }

            return desTechnologies;
        }


	    /// <summary>
		/// Cette méthode permet de récupérer toutes les difficultés
		/// Fonctionne avec une fluentQuerry
		/// </summary>
		/// <returns>Retourne une liste d'objets Diffulties</returns>
		public List<Difficulty> GetDifficulties()
        {
            List<Difficulty> desDifficulties = new List<Difficulty>();

            using (var db = new FilRougeDBContext())
            {
                desDifficulties = db.Difficulties.Select(e => e).ToList();
            }

            /*FilRougeDBContext db = new FilRougeDBContext();
            var fluentQuery = db.Difficulties.Select(e => e);
            foreach (var item in fluentQuery)
            {
                desDifficulties.Add(item);
            }
            db.Dispose();*/
            return desDifficulties;
        }

		#endregion
	}
}
