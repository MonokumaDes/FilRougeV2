using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FilRouge.Web.ViewModels
{
	public class DifficultyMasterViewModel
	{
        public int DiffMasterId { get; set; }
        [Required]
        [Display(Name ="Difficulté")]
        public string DiffMasterName { get; set; }
    }
}