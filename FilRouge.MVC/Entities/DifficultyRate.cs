using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FilRouge.MVC.Entities
{
    public partial class DifficultyRate
    {
        #region Properties
        //Clé composée de l'id d'un quizz et d'une difficulté
        [Key]
        [Column(Order = 0)]
        [ForeignKey("DifficultyMaster")]
        public int DifficultyMasterId { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Difficulty")]
        public int DifficultyId { get; set; }

        public decimal Rate { get; set; }
        #endregion

        #region Associations
        public virtual Difficulty Difficulty { get; set; }
        public virtual Difficulty DifficultyMaster { get; set; }
        #endregion
    }
}
