using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FilRouge.MVC.Entities;
using FilRouge.MVC.Models;

namespace FilRouge.MVC.ViewModels
{
	public class QuestionViewModel
	{
		public int QuestionId { get; set; }
		[Required]
		[Display(Name = "Libellé de la question : ")]
		public string Content { get; set; }
		[Display(Name = "Remarque : ")]
		public string Commentaire { get; set; }
		[Display(Name = "Active ? ")]
		public bool Active { get; set; }

        //[Required]
        public TypeQuestion QuestionType { get; set; }
        [Required]
        public Technology Technology { get; set; }
        [Required]
        public Difficulty Difficulty { get; set; }

        [Required]
        public AnswerTypeEnum AnswerType { get; set; }

        public List<Reponses> Reponses { get; set; }
	}
}