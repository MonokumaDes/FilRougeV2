using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilRouge.MVC.ViewModels
{
	public class QuizzViewModel
	{
		[Required]
		public int QuizzId { get; set; }
		[Display(Name = "Prénom :")]
		public string PrenomUser { get; set; }
		[Display(Name = "Nom : ")]
		public string NomUser { get; set; }
		[Display(Name = "Question libre?")]
		public bool QuestionLibre { get; set; }
		[Display(Name = "Nombre de question : ")]
		public int NombreQuestion { get; set; }
		public int TechnologyId { get; set; }
		public int DifficultyId { get; set; }
		public int EtatQuizz { get; set; }
		public int Timer { get; set; }
		public List<int> QuestionId { get; set; }
		public int Index { get; set; }
	}
}