using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FilRouge.MVC.Entities;

namespace FilRouge.MVC.ViewModels
{
    public class QuizzAnswerReponsesViewModel
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

        [Display(Name = "Technologie :")]
        public string TechnologyName { get; set; }
        [Display(Name = "Difficulté :")]
        public string DifficultyName { get; set; }
        [Display(Name = "Etat :")]
        public int EtatQuizz { get; set; }
        public int Timer { get; set; }
        public List<QuestionViewModel> Questions { get; set; }

        // public List<UserReponse> UserReponses { get; set; }

        [Display(Name = "Nom contact :")]
        public string ContactName { get; set; }
        [Display(Name = "Email contact :")]
        public string ContactEmail { get; set; }
    }
}