using System.ComponentModel.DataAnnotations;

namespace FilRouge.MVC.ViewModels
{
    public class ReponseViewModel
    {
        public int ReponseId { get; set; }

        [Required]
        [Display(Name = "Valeur réponse")]
        public string Content { get; set; }

        [Display(Name = "Bonne réponse?")]
        public bool TrueReponse { get; set; }

    }
}