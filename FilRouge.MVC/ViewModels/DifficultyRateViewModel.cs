using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilRouge.MVC.ViewModels
{
	public class DifficultyRateViewModel
	{
        [Required]
        public int DifficultyMasterId { get; set; }
        [Required]
        public int DifficultyId { get; set; }
        [Required]
        public decimal Rate { get; set; }
    }
}