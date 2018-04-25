using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace FilRouge.MVC.Entities
{
    public partial class UserReponse
    {
        #region Proporties
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Quizz")]
        public int QuizzId { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Reponse")]
        public int ReponseId { get; set; }
        public string Valeur { get; set; }
        #endregion
        #region Association
        public Quizz Quizz { get; set; }
        public Reponses Reponse{ get; set; }
        #endregion
    }
}
