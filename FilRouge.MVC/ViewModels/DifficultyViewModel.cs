using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilRouge.MVC.ViewModels
{
	public class DifficultyViewModel
	{
        public int DifficultyId { get; set; }
        [Required]
        [Display(Name = "Difficulté")]
        public string DifficultyName { get; set; }
    }
}