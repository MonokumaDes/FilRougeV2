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
    public partial class Difficulty
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DifficultyId { get; set; }
        public string DifficultyName { get; set; }

        #endregion
        #region Associations
        public virtual List<Questions> Questions { get; set; }
        public virtual List<Quizz> Quizzs { get; set; }
        #endregion
    }
}
