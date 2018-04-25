using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilRouge.MVC.Entities;

namespace FilRouge.MVC.Services
{
	public class ReponsesServices
	{
        public List<Reponses> GetReponsesById(int id)
        {
            List<Reponses> reponses = new List<Reponses>();

            using (var db = new FilRougeDBContext())
            {
                var repList = db.Reponse.Where(r => r.QuestionId == id).Select(r => r).ToList();

                foreach(var rep in repList)
                {
                    reponses.Add(rep);
                }
            }
            return reponses;
        }
        

	}
}