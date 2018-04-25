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
    public partial class Reponses
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReponseId { get; set; }
        public string Content { get; set; }
        public bool TrueReponse { get; set; }
        //Clés étrangères
        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        #endregion
        #region Association
        public virtual Questions Question { get; set; }
        #endregion
    }
}
